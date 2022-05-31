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
    public async Task<ActionResult<Booking>> GetBooking(int id)
    {
      var booking = await _context.BookingSet.FindAsync(id);

      if (booking == null)
      {
        return NotFound();
      }

      return booking;
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
      //Calculate free seats in %
      float percentAvailable = 0;
      float price = 0;
     
      foreach (VSFlyDavidIsmael.Flight f in _context.FlightSet)
      {
        if (f.FlightId == booking.FlightId)
        {
          percentAvailable = (((float)f.AvailableSeats / (float)f.Seats) * 100);
          price = f.BasePrice;
          
        }
      }

      //Multiplier based on %
      switch (percentAvailable)
      {
        //80% full or more
        case float n when (n <= 20):
          price = price * 1.5f;
          break;
        //less than 20% full
        case float n when (n >= 80):
          price = price * 0.8f;
          break;
        //less than 50% full
        case float n when (n > 50):
          price = price * 0.7f;
          break;
      }

      booking.Price = price;
      _context.BookingSet.Add(booking.convertToBooking());
      await _context.SaveChangesAsync();

      //take available seat away
      /*
      foreach (VSFlyDavidIsmael.Flight f in _context.FlightSet)
      {
        if (f.FlightId == booking.FlightId)
        {
          _context.Entry(f).State = EntityState.Modified;
          f.AvailableSeats--;
          try
          {
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {         
              throw;
          }

        } 
      } */

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
