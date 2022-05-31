using System;

namespace VSFlyDavidIsmael
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      var ctx = new VSFlyContext();

      var e = ctx.Database.EnsureCreated();

      if (e)
        Console.WriteLine("Database has been created.");
      else
        Console.WriteLine("Database already exists");

      Console.WriteLine("done.");

      // add a Place
      /*
      Place p = new Place { Name = "Bern"};
      ctx.PlaceSet.Add(p);
      ctx.SaveChanges(); 

      // add a Passenger
      Passenger pa = new Passenger { Firstname = "Bob", Lastname = "Frenchman" };
      ctx.PassengerSet.Add(pa);
      ctx.SaveChanges(); 

      // add a Flight
      
      Flight f = new Flight { BasePrice = 15, Seats = 100, Departure = p, DestinationId=1 };
      ctx.FlightSet.Add(f);
      ctx.SaveChanges(); 
      
      // add a Booking
      Booking b = new Booking { FlightId = 2, PassengerId = 1, Price = 20 };
      ctx.BookingSet.Add(b);
      ctx.SaveChanges(); 
      */

    }
  }
}
