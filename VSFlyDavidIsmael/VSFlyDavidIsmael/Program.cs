using System;

namespace VSFlyDavidIsmael
{
  class Program
  {
    static void Main(string[] args)
    {
      var ctx = new VSFlyContext();

      var e = ctx.Database.EnsureCreated();

      if (e)
        Console.WriteLine("Database has been created.");
      else
        Console.WriteLine("Database already exists");

      Console.WriteLine("done.");
    }
  }
}
