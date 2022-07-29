
using Microsoft.AspNetCore.Mvc;

namespace akywedding_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController : ControllerBase {
  [HttpGet]
  public IActionResult Get() => Ok();
}