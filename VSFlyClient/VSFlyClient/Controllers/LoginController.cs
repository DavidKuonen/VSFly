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
  public class LoginController : Controller
  {

    private readonly ILogger<LoginController> _logger;
    private readonly IVSFlyServices _vsFly;

    public LoginController(ILogger<LoginController> logger, IVSFlyServices vsFly)
    {
      _logger = logger;
      _vsFly = vsFly;
    }

    public IActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginM login)
    {
      HttpContext.Session.SetString("_Lastname", login.Lastname);
      HttpContext.Session.SetString("_Firstname", login.Firstname);

      //Check if passenger exists in API database
      var passengers = await _vsFly.GetPassengers();
      PassengerM realPassenger = null;
      foreach (PassengerM p in passengers)
      {
        if (login.Firstname.Equals(p.Firstname) &&
       login.Lastname.Equals(p.Lastname))
        {
          realPassenger = p;
          break;
        }
      }
      //otherwise create him 
      if (realPassenger == null)
      {
        PassengerM newPassenger = new PassengerM();

        var firstname = login.Firstname;
        var lastname = login.Lastname;
        newPassenger.Firstname = firstname;
        newPassenger.Lastname = lastname;

        newPassenger = await _vsFly.PostPassenger(newPassenger);

      }
      return RedirectToAction("Index","Home");
    }
  }
  }
