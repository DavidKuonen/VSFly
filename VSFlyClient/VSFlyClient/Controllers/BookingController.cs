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
    public float PriceCalc(FlightM flight)
    {
      //Calculate free seats in %
      float percentAvailable = 0;
      float price = flight.BasePrice;
      percentAvailable = (((float)flight.AvailableSeats / (float)flight.Seats) * 100);

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

      return price;
    }
    //Stupid way to get highest ID, hopefully better solution can be found
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
      var flight = await _vsFly.GetFlight(id);
      //Stupid way to get highest ID, hopefully better solution can be found
      int bookingId = await getBookingId();

     float price = PriceCalc(flight);


      var booking = new BookingM 
      {FlightId = id,PassengerId = 1,Price = price,BookingId = bookingId+1 };

      return View(booking);
    }

    public async Task<IActionResult> ConfirmBooking(int id)
    {
      var flight = await _vsFly.GetFlight(id);
      float price = PriceCalc(flight);
      //Stupid way to get highest ID, hopefully better solution can be found
      int bookingId = await getBookingId();

      var booking = new BookingM
      { FlightId = id, PassengerId = 1, Price = price, BookingId = 0 };

      var bookingnew = await _vsFly.PostBooking(booking);

      bookingnew.BookingId = bookingId+1;

      flight.AvailableSeats--;
      await _vsFly.UpdateFlight(flight);


      return View(bookingnew);

    }

  }
}
