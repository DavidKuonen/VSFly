using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyDavidIsmael;

namespace VSFlyAPI.Controllers
{
  [Route("api/[controller]")]
  public class TicketController : Controller
  {
    private readonly VSFlyContext _context;

    public TicketController(VSFlyContext context)
    {
      _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<float>> GetFlightTicketPrice(int id)
    {
      var flight = await _context.FlightSet.FindAsync(id);

      if (flight == null)
      {
        return NotFound();
      }

      //Calculate free seats in %
      float percentAvailable = 0;
      float price = flight.BasePrice;
      percentAvailable = (((float)flight.AvailableSeats / (float)flight.Seats) * 100);

      //Multiplier based on %
      //80% full or more
      if (percentAvailable <= 20)
            {
        price = price * 1.5f;
      }
      //less than 50% full and less than 1 month away
      else if (percentAvailable > 50 && (DateTime.Now.AddMonths(1) > flight.DestinationTime))
      {
        price = price * 0.7f; 
      }
      //less than 20% full and less than 2 months away
      else if (percentAvailable >= 80 && (DateTime.Now.AddMonths(2) > flight.DestinationTime))
      {
        price = price * 0.8f;
      }

      return price;

    } 
  }
}
