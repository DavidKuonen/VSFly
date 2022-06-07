using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Controllers
{
  public class LoginController : Controller
  {
    public IActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Login(LoginM login)
    {
      HttpContext.Session.SetString("_Lastname", login.Lastname);
      HttpContext.Session.SetString("_Firstname", login.Firstname);
      return RedirectToAction("Index","Home");
    }
  }
  }
