using akywedding_backend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using akywedding_backend.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace akywedding_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RsvpController : ControllerBase {
  private readonly WeddingContext _ctx;

  public RsvpController(WeddingContext ctx) {
    _ctx = ctx;
  }

  [HttpPost("findparty")]
  public async Task<IActionResult> FindParty(string name) {
    var party = await _ctx.parties
      .Include(x => x.guests)
      .FirstOrDefaultAsync(x => x.guests.Any(y => y.name.ToLower().Contains(name.ToLower())));

    if (party is null) {
      return NotFound();
    }

    return Ok(new RsvpViewModel {
      partyId = party.id,
      guests = party.guests.Select(x => new GuestViewModel {
        guest_id = x.id,
        name = x.name,
        is_child = x.is_child,
      })
    });
  }

  [HttpPost("submit")]
  public async Task<IActionResult> SubmitRsvp(RsvpViewModel rsvp) {
    var party = await _ctx.parties
      .Include(x => x.guests)
        .ThenInclude(x => x.meal_choice)
      .SingleOrDefaultAsync(x => x.id == rsvp.partyId);

    if (party is null) {
      return BadRequest("Party not found");
    }

    var meals = await _ctx.mealOptions.ToArrayAsync();

    foreach (var guest in party.guests) {
      var guestModel = rsvp.guests.SingleOrDefault(x => x.guest_id == guest.id);

      if (guestModel is null) {
        return BadRequest($"Unable to find a response for ${guest.name}");
      }

      try {
        guest.is_attending = guestModel.is_attending;
        guest.dietary_restrictions = guestModel.dietary_restrictions;
        guest.meal_choice = guestModel.is_attending ? meals.Single(x => x.id == guestModel.meal_id) : null;
        guest.updated_at = DateTime.UtcNow;
      } catch (Exception e) {
        return BadRequest("Unable to save guest");
      }
    }

    var rsvpObj = new Rsvp {
      music = rsvp.music,
      comments = rsvp.comments,
      party = party,
      created_at = DateTime.UtcNow,
    };

    try {
      await _ctx.rsvps.AddAsync(rsvpObj);
      await _ctx.SaveChangesAsync();

      return Ok();
    } catch (Exception e) {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("meals")]
  public async Task<IEnumerable<MealViewModel>> GetMeals() =>
    await _ctx.mealOptions
      .Select(x => new MealViewModel {
        meal_id = x.id,
        name = x.name
      })
      .ToListAsync();
}