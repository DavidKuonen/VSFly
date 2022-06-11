using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyAPI.Extensions
{
  public static class ConverterExtensions
  {

    //this in Parameter allows it to be called as Flight.convertToFlightM(); without having to add anything as Parameter
    public static Models.FlightM convertToFlightM(this VSFlyDavidIsmael.Flight f)
    {
      //To show the Name instead of just the ID of the place
      string Destination = null;
      string Departure = null;
      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();
      foreach (VSFlyDavidIsmael.Place place in ctx.PlaceSet)
      {
        if(place.PlaceId == f.DestinationId)
        {
          Destination = place.Name;
        }
        if(place.PlaceId == f.DepartureId)
        {
          Departure = place.Name;
        }
      }

      Models.FlightM fM = new Models.FlightM();
      fM.FlightId = f.FlightId;
      fM.Destination = Destination;
      fM.Departure = Departure;
      fM.DepartureTime = f.DepartureTime;
      fM.DestinationTime = f.DestinationTime;
      fM.BasePrice = f.BasePrice;
      fM.Seats = f.Seats;
      fM.AvailableSeats = f.AvailableSeats;
      
      return fM;
    }

    public static VSFlyDavidIsmael.Flight convertToFlight(this Models.FlightM fM)
    {
      //To convert from the Name of the place back to the ID
      int DestinationId = 0;
      int DepartureId = 0;
      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();
      foreach (VSFlyDavidIsmael.Place place in ctx.PlaceSet)
      {
        if (place.Name.Equals(fM.Destination))
        {
          DestinationId = place.PlaceId;
        }
        if(place.Name.Equals(fM.Departure))
        {
          DepartureId = place.PlaceId;
        }
      }


      VSFlyDavidIsmael.Flight f = new VSFlyDavidIsmael.Flight();
      f.FlightId = fM.FlightId;
      f.DestinationId = DestinationId;
      f.DepartureId = DepartureId;
      f.DepartureTime = fM.DepartureTime;
      f.DestinationTime = fM.DestinationTime;
      f.BasePrice = fM.BasePrice;
      f.Seats = fM.Seats;
      f.AvailableSeats = fM.AvailableSeats;

      return f;
    }


    public static Models.BookingM convertToBookingM(this VSFlyDavidIsmael.Booking b)
    {
      string name = "";

      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();
      foreach (VSFlyDavidIsmael.Passenger p in ctx.PassengerSet)
      {
        if(p.PassengerId == b.PassengerId)
        {
          name = p.Firstname + " " + p.Lastname;
         // name = $"{p.Firstname} {p.Lastname}";
        }
      }

      Models.BookingM bM = new Models.BookingM();
      bM.BookingId = b.BookingId;
      bM.FlightId = b.FlightId;
      bM.Passenger = name;
      bM.Price = b.Price;

      return bM;
    }

    public static VSFlyDavidIsmael.Booking convertToBooking(this Models.BookingM bM)
    {
      int bmId = 0;
      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();
      foreach(VSFlyDavidIsmael.Passenger p in ctx.PassengerSet)
      {
        if((p.Firstname+" "+p.Lastname).Equals(bM.Passenger))
        {
          bmId = p.PassengerId;
        }
      }

      VSFlyDavidIsmael.Booking b = new VSFlyDavidIsmael.Booking();
      b.BookingId = bM.BookingId;
      b.FlightId = bM.FlightId;
      b.PassengerId = bmId;
      b.Price = bM.Price;

      return b;
    }

    public static VSFlyDavidIsmael.Passenger convertToPassenger(this Models.PassengerM pM)
    {
      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();
      foreach(VSFlyDavidIsmael.Passenger p in ctx.PassengerSet)
      {
        if (p.Firstname.Equals(pM.Firstname) && p.Lastname.Equals(pM.Lastname))
        {
          return p;
        }
      }

      VSFlyDavidIsmael.Passenger pp = new VSFlyDavidIsmael.Passenger { PassengerId = 0, Firstname = pM.Firstname, Lastname = pM.Lastname };
      return pp;
    }

    public static Models.PassengerM convertToPassengerM(this VSFlyDavidIsmael.Passenger p)
    {
      VSFlyDavidIsmael.VSFlyContext ctx = new VSFlyDavidIsmael.VSFlyContext();

      Models.PassengerM pM = new Models.PassengerM();

      pM.PassengerId = p.PassengerId;
      pM.Firstname = p.Firstname;
      pM.Lastname = p.Lastname;

      return pM;
    }

  }
}
