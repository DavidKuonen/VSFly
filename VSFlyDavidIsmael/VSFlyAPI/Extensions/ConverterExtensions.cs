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
        if (place.Name == fM.Destination)
        {
          DestinationId = place.PlaceId;
        }
        if(place.Name == fM.Departure)
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
      Models.BookingM bM = new Models.BookingM();
      bM.BookingId = b.BookingId;
      bM.FlightId = b.FlightId;
      bM.PassengerId = b.PassengerId;
      bM.Price = b.Price;

      return bM;
    }

    public static VSFlyDavidIsmael.Booking convertToBooking(this Models.BookingM bM)
    {
      VSFlyDavidIsmael.Booking b = new VSFlyDavidIsmael.Booking();
      b.BookingId = bM.BookingId;
      b.FlightId = bM.FlightId;
      b.PassengerId = bM.PassengerId;
      b.Price = bM.Price;

      return b;
    }


  }
}
