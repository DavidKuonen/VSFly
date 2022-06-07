using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyDavidIsmael;

namespace VSFlyAPI.Controllers
{
  [Route("api/[controller]")]
  public class TotalTicketPriceController : Controller
  {
    private readonly VSFlyContext _context;

    public TotalTicketPriceController(VSFlyContext context)
    {
      _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<float>> GetFlightTotalTicketPrice(int id)
    {

      float totalPrice = 0;

      var bookingList = await _context.BookingSet.ToListAsync();

      foreach (Booking b in bookingList)
      {
        if (b.FlightId == id)
        {
          totalPrice += b.Price;
        }
      }

      return totalPrice;
    }

  }
}
