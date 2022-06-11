﻿using Microsoft.AspNetCore.Http;
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
            if (HttpContext.Session.GetInt32("_UserId") == null)
            {
                return RedirectToAction("Login", "Login");
            }


            //highest ID to show client 
            int bookingId = await getBookingId();

            //get ticket price from API
            float price = await _vsFly.GetFlightTicketPrice(id);


            //Create booking to show client before he confirms
            //uses first and lastname from session
            var booking = new BookingM
            { FlightId = id, Passenger = HttpContext.Session.GetString("_Firstname") + " " + HttpContext.Session.GetString("_Lastname"), Price = price, BookingId = bookingId + 1 };

            return View(booking);
        }

        public async Task<IActionResult> ConfirmBooking(int id)
        {
            if (HttpContext.Session.GetInt32("_UserId") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var flight = await _vsFly.GetFlight(id);
            float price = await _vsFly.GetFlightTicketPrice(id);
            //get highest ID
            int bookingId = await getBookingId();

            //Create Booking for User of the Session
            BookingM booking = new BookingM
            { FlightId = id, Passenger = HttpContext.Session.GetString("_Firstname") + " " + HttpContext.Session.GetString("_Lastname"), Price = price, BookingId = 0 };

            var bookingnew = await _vsFly.PostBooking(booking);

            bookingnew.BookingId = bookingId + 1;

            return View(bookingnew);

        }


        public async Task<IActionResult> MyBookings()
        {
            if (HttpContext.Session.GetInt32("_UserId") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var bookings = await _vsFly.GetMyBookings((int)HttpContext.Session.GetInt32("_UserId"));

            return View(bookings);
        }

        public async void Cancel(int id)
        {
            _vsFly.DeleteBooking(id);

            Response.Redirect("../MyBookings");
        }
    }
}
  
  
