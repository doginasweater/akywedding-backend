using akywedding_backend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace akywedding_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RsvpController : ControllerBase {
  [HttpPost("findparty")]
  public RsvpViewModel FindParty([FromBody] string name) => new();

  [HttpPost("submit")]
  public IActionResult SubmitRsvp([FromBody] RsvpViewModel rsvp) => Ok();
}