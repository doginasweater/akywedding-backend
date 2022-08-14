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
  public async Task<IActionResult> FindParty([FromBody] string name) {
    var guest = await _ctx.guests.FirstOrDefaultAsync(x => x.name.ToLower().Contains(name.ToLower()));

    if (guest is null) {
      return NotFound();
    }

    return Ok(new RsvpViewModel {
      partyId = guest.party.id,
      guests = guest.party.guests.Select(x => new GuestViewModel {
        guest_id = x.id,
        name = x.name
      })
    });
  }

  [HttpPost("submit")]
  public IActionResult SubmitRsvp([FromBody] RsvpViewModel rsvp) => Ok();

  [HttpGet("meals")]
  public async Task<IEnumerable<MealOption>> GetMeals() => await _ctx.mealOptions.ToListAsync();

  [HttpGet("parties")]
  public async Task<IEnumerable<Party>> GetParties() => await _ctx.parties.Include(x => x.guests).ToListAsync();
}