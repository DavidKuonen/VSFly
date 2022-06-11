using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSFlyAPI.Models;
using VSFlyAPI.Extensions;
using VSFlyDavidIsmael;

namespace VSFlyAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookingsController : ControllerBase
  {
    private readonly VSFlyContext _context;

    public BookingsController(VSFlyContext context)
    {
      _context = context;
    }

    // GET: api/Bookings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingM>>> GetBookingSet()
    {

      var bookingList = await _context.BookingSet.ToListAsync();
      List<BookingM> bookingMs = new List<BookingM>();

      foreach (Booking b in bookingList)
      {
        var bM = b.convertToBookingM();
        bookingMs.Add(bM);
      }

      return bookingMs;
    }

    // GET: api/Bookings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookingM>> GetBooking(int id)
    {
      var booking = await _context.BookingSet.FindAsync(id);

      if (booking == null)
      {
        return NotFound();
      }

      var bookingM = booking.convertToBookingM();
      return bookingM;
    }

    [HttpGet("/api/Bookings/Destination/{destination}")]
    public async Task<IEnumerable<BookingM>> GetBookingsByDestination(string destination)
    {
      var booking = await _context.BookingSet.ToListAsync();
      List<Flight> flights = await _context.FlightSet.ToListAsync();

      List<BookingM> bookingDestination = new List<BookingM>();
      List<FlightM> flightsDestination = new List<FlightM>();
      
      //Get flights of destination
      foreach (Flight f in flights)
      {
        var fM = f.convertToFlightM();
        if (fM.Destination == destination)
        {
          flightsDestination.Add(fM);
        }
      }

      //Check if a booking is for a flight of the destination
      foreach (Booking b in booking)
      {
        foreach(FlightM f in flightsDestination)
        {
          if(b.FlightId == f.FlightId)
          {
            var bM = b.convertToBookingM();
            bookingDestination.Add(bM);
          }
        }
      }


      return bookingDestination;
    }

    // PUT: api/Bookings/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBooking(int id, Booking booking)
    {
      if (id != booking.BookingId)
      {
        return BadRequest();
      }

      _context.Entry(booking).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookingExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Bookings
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Booking>> PostBooking(BookingM booking)
    {
      _context.BookingSet.Add(booking.convertToBooking());
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
    }

    // DELETE: api/Bookings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
      var booking = await _context.BookingSet.FindAsync(id);
      if (booking == null)
      {
        return NotFound();
      }

      _context.BookingSet.Remove(booking);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool BookingExists(int id)
    {
      return _context.BookingSet.Any(e => e.BookingId == id);
    }
  }
}
