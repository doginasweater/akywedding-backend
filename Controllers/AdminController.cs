using akywedding_backend.Models.Database;
using akywedding_backend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace akywedding_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase {
  private readonly WeddingContext _ctx;

  public AdminController(WeddingContext ctx) {
    _ctx = ctx;
  }

  [HttpGet("parties")]
  public IEnumerable<Party> GetParties(string search) =>
    _ctx.parties
      .Include(x => x.guests)
        .ThenInclude(x => x.meal_choice)
      .ToList()
      .Where(x => {
        if (!string.IsNullOrEmpty(search)) {
          return x.guests.Any(y => y.name.ToLower().Contains(search.ToLower()));
        }

        return true;
      });

  [HttpGet("rsvps")]
  public async Task<IEnumerable<AdminRsvpViewModel>> GetRsvps() {
    var query = await _ctx.rsvps
      .Include(x => x.party)
        .ThenInclude(x => x.guests)
          .ThenInclude(x => x.meal_choice)
      .ToListAsync();

    return query.Select(x => new AdminRsvpViewModel {
      rsvp_id = x.id,
      comments = x.comments,
      music = x.music,
      created_at = x.created_at,
      updated_at = x.updated_at,
      guests = x.party.guests.Select(y => new GuestViewModel {
        guest_id = y.id,
        name = y.name,
        is_child = y.is_child,
        is_attending = y.is_attending == true,
        meal_id = y.meal_choice is not null ? y.meal_choice.id : 0,
        dietary_restrictions = y.dietary_restrictions!,
        created_at = y.created_at,
        updated_at = y.updated_at,
      })
    });
  }

  [HttpDelete("rsvp/{id}")]
  public async Task<IActionResult> DeleteRsvp(int id) {
    var rsvp = await _ctx.rsvps.SingleOrDefaultAsync(x => x.id == id);

    if (rsvp is null) {
      return Ok();
    }

    _ctx.rsvps.Remove(rsvp);

    await _ctx.SaveChangesAsync();

    return NoContent();
  }
}