using akywedding_backend.Models.Database;
using akywedding_backend.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace akywedding_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
  private readonly WeddingContext _ctx;

  public AdminController(WeddingContext ctx)
  {
    _ctx = ctx;
  }

  [HttpGet("parties")]
  public IEnumerable<Party> GetParties(string search) =>
    _ctx.parties
      .Include(x => x.guests)
        .ThenInclude(x => x.meal_choice)
      .ToList()
      .Where(x =>
      {
        if (!string.IsNullOrEmpty(search))
        {
          return x.guests.Any(y => y.name.ToLower().Contains(search.ToLower()));
        }

        return true;
      });

  [HttpGet("rsvps")]
  public async Task<IEnumerable<AdminRsvpViewModel>> GetRsvps()
  {
    var query = await _ctx.rsvps
      .Include(x => x.party)
        .ThenInclude(x => x.guests)
          .ThenInclude(x => x.meal_choice)
      .ToListAsync();

    return query.Select(x => new AdminRsvpViewModel
    {
      rsvp_id = x.id,
      comments = x.comments,
      music = x.music,
      created_at = x.created_at,
      updated_at = x.updated_at,
      guests = x.party.guests.Select(y => new GuestViewModel
      {
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
  public async Task<IActionResult> DeleteRsvp(int id)
  {
    var rsvp = await _ctx.rsvps

    .SingleOrDefaultAsync(x => x.id == id);

    if (rsvp is null)
    {
      return Ok();
    }

    _ctx.rsvps.Remove(rsvp);

    await _ctx.SaveChangesAsync();

    return NoContent();
  }


  //Aky's first C# Method :)

  [HttpGet("fix-rsvp")]
  public async Task<IActionResult> FixKevinStevens()
  {
    var rsvp = await _ctx.rsvps
        .Include(x => x.party)
          .ThenInclude(x => x.guests)
          .ThenInclude(x => x.meal_choice)
          .SingleOrDefaultAsync(x => x.id == 1);

    if (rsvp is null)
    {
      return BadRequest();
    }

    foreach (var guest in rsvp.party.guests)
    {
      guest.meal_choice = await _ctx.mealOptions.SingleOrDefaultAsync(x => x.name == "Chicken");
      guest.is_attending = true;
      guest.updated_at = DateTime.UtcNow;
    }

    try
    {
      await _ctx.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  //Aky's SECOND!!! C# method :D

  [HttpGet("meal-counts")]
  public async Task<IActionResult> GetMealCounts()
  {
    var meals = await _ctx.guests
            .Include(x => x.meal_choice)
          .Where(x => x.is_attending == true)
          .ToListAsync();


    var grouped = meals.GroupBy(x => x.meal_choice.id)
    .Select(x => new
    {
      meal_id = x.Key,
      meal_name = x.First().meal_choice.name,
      count = x.Count()
    });

    return Ok(grouped);
  }


  //THE THIRD ONE!!!!!!!!!
  // [HttpGet("attending-numbers")]
  // public IActionResult GetAttendingNumbers()
  // {
  //   return Ok(new
  //   {
  //     attending = _ctx.guests.Where(x => x.is_attending == true).Count(),
  //     not_attending = _ctx.guests.Where(x => x.is_attending == false).Count(),
  //   });
  // }

  //FOUR

  [HttpGet("attending-guests")]
  public async Task<IActionResult> GetAttendingGuests()
  {
    var guests = await _ctx.guests.ToListAsync();

    var attending = guests.Where(x => x.is_attending == true);
    var not_attending = guests.Where(x => x.is_attending == false);

    return Ok(new
    {
      attending = new
      {
        count = attending.Count(),
        guests = attending.Select(x => x.name)
      },
      not_attending = new
      {
        count = not_attending.Count(),
        guests = not_attending.Select(x => x.name),
      }
    });
  }
}


