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
  public RsvpViewModel FindParty([FromBody] string name) => new();

  [HttpPost("submit")]
  public IActionResult SubmitRsvp([FromBody] RsvpViewModel rsvp) => Ok();

  [HttpGet("meals")]
  public async Task<IEnumerable<MealOption>> GetMeals() => await _ctx.mealOptions.ToListAsync();
}