using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyClient.Models;
using VSFlyClient.Services;

namespace VSFlyClient.Controllers
{
  public class BookingController : Controller
  {
    private readonly ILogger<BookingController> _logger;
    private readonly IVSFlyServices _vsFly;
     
    public BookingController(ILogger<BookingController> logger, IVSFlyServices vsFly)
    {
      _logger = logger;
      _vsFly = vsFly;
    }
   
    //get highest ID
    public async Task<int> getBookingId()
    {
      var bookings = await _vsFly.GetBookings();
      int bookingId = 0;
      foreach (BookingM bm in bookings)
      {
        if (bookingId < bm.BookingId)
        {
          bookingId = bm.BookingId;
        }
      }
      return bookingId;
    }

    public async Task<IActionResult> Index(int id)
    {
      //highest ID to show client 
      int bookingId = await getBookingId();

      //get ticket price from API
      float price = await _vsFly.GetFlightTicketPrice(id);


      //Create booking to show client before he confirms
      //uses first and lastname from session
      var booking = new BookingM 
      {FlightId = id,Passenger = HttpContext.Session.GetString("_Firstname") + " "+ HttpContext.Session.GetString("_Lastname"), Price = price,BookingId = bookingId+1 };

      return View(booking);
    }

    public async Task<IActionResult> ConfirmBooking(int id)
    {
      var flight = await _vsFly.GetFlight(id);
      float price = await _vsFly.GetFlightTicketPrice(id); 
      //get highest ID
      int bookingId = await getBookingId();
      BookingM booking = new BookingM();

      //Check if passenger exists in API database
      var passengers = await _vsFly.GetPassengers();
      PassengerM realPassenger = null;
      foreach(PassengerM p in passengers)
      {
        if(HttpContext.Session.GetString("_Firstname").Equals(p.Firstname) &&
        HttpContext.Session.GetString("_Lastname").Equals(p.Lastname))
        {
          realPassenger = p;
          booking = new BookingM
          { FlightId = id, Passenger = realPassenger.Firstname + " " + realPassenger.Lastname, Price = price, BookingId = 0 };
          break;
        }
      }
      //otherwise create him 
      if(realPassenger == null)
      {
        PassengerM newPassenger = new PassengerM();

        var firstname = HttpContext.Session.GetString("_Firstname");
        var lastname = HttpContext.Session.GetString("_Lastname");
        newPassenger.Firstname = firstname;
        newPassenger.Lastname = lastname;

        newPassenger = await _vsFly.PostPassenger(newPassenger);

        booking = new BookingM
        { FlightId = id, Passenger = newPassenger.Firstname + " " + newPassenger.Lastname, Price = price, BookingId = 0 };
      }

      var bookingnew = await _vsFly.PostBooking(booking);

      bookingnew.BookingId = bookingId+1;

      flight.AvailableSeats--;
      await _vsFly.UpdateFlight(flight);


      return View(bookingnew);

    }

  }
}
