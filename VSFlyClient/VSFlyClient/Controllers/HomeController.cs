using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VSFlyClient.Models;
using VSFlyClient.Services;

namespace VSFlyClient.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IVSFlyServices _vsFly;
    public HomeController(ILogger<HomeController> logger, IVSFlyServices vsFly)
    {
      _logger = logger;
      _vsFly = vsFly;
    }

    public async Task<IActionResult> Index()
    {
      var listFlight = await _vsFly.GetFlights();
      return View(listFlight);
    }

    public async Task<IActionResult> Bookings(int id)
    {
      var listBooking = await _vsFly.GetBookings();
      List<BookingM> bookings =  new List<BookingM>();
      foreach(BookingM bm in listBooking)
      {
        if(bm.FlightId == id)
        {
          bookings.Add(bm);
        }
      }
      return View(bookings);
    }

    public async Task<IActionResult> TotalTicketPrice(int id)
    {
      var flight = await _vsFly.GetFlight(id);
      var totalTicketPrice = await _vsFly.GetFlightTotalTicketPrice(id);
      flight.BasePrice = totalTicketPrice;

      return View(flight);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
