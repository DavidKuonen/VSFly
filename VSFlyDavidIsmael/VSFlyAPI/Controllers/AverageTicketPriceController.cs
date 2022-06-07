using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyAPI.Extensions;
using VSFlyAPI.Models;
using VSFlyDavidIsmael;

namespace VSFlyAPI.Controllers
{
  [Route("api/[controller]")]
  public class AverageTicketPriceController : Controller
  { //For destination, not flight
    private readonly VSFlyContext _context;

    public AverageTicketPriceController(VSFlyContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<float>> GetDestinationAverageTicketPrice(string destination)
    {
      var flightList = await _context.FlightSet.ToListAsync();
      List<FlightM> flight = new List<FlightM>();
      //Get flights of destination
      foreach (Flight f in flightList)
      {
        var fm = f.convertToFlightM(); 
        if (fm.Destination == destination)
        {
          flight.Add(fm);
        }
      }

      if (flight.Count == 0)
      {
        return 0;
      }
      var bookingList = await _context.BookingSet.ToListAsync();
      List<Booking> booking = new List<Booking>();
      float totalPrice = 0;
      int ticketCount = 0;

      //Get bookings for the flights of the destination
      foreach (Booking b in bookingList)
      {
        foreach (FlightM f in flight)
        {
          if (b.FlightId == f.FlightId)
          {
            booking.Add(b);
          }
        }
      }

      if(booking.Count == 0)
      {
        return 0;
      }

      //math
      foreach(Booking b in booking)
      {
        totalPrice += b.Price;
        ticketCount++;
      }

      return (totalPrice / ticketCount);
    }
   

  }
  }
