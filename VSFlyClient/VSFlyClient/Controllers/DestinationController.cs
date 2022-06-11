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
  public class DestinationController : Controller
  {
    private readonly ILogger<DestinationController> _logger;
    private readonly IVSFlyServices _vsFly;

    public DestinationController(ILogger<DestinationController> logger, IVSFlyServices vsFly)
    {
      _logger = logger;
      _vsFly = vsFly;
    }

    public IActionResult Index()
    {
      if (HttpContext.Session.GetInt32("_UserId") == null)
      {
        return RedirectToAction("Login", "Login");
      }

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(DestinationInfoM DestinationM)
    {
      if (HttpContext.Session.GetInt32("_UserId") == null)
      {
        return RedirectToAction("Login", "Login");
      }

      float price = await _vsFly.GetDestinationAverageTicketPrice(DestinationM.Destination);
      DestinationM.AveragePrice = price;


      return View(DestinationM);
    }

    public IActionResult DestinationTickets()
    {
      return View();
    }

    [HttpPost]
    public IActionResult DestinationTickets(DestinationInfoM DestinationM)
    {
      return RedirectToAction("DestinationTicketsShown","Destination", new { DestinationM.Destination });
    }

    public async Task<IActionResult> DestinationTicketsShown(string destination)
    {
      if (HttpContext.Session.GetInt32("_UserId") == null)
      {
        return RedirectToAction("Login", "Login");
      }

      var bookings = await _vsFly.GetBookingsByDestination(destination);

      return View(bookings);
    }
  }
}
