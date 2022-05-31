using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyDavidIsmael
{
  public class VSFlyContext : DbContext
  {
    public DbSet<Flight> FlightSet { get; set; }
    public DbSet<Place> PlaceSet { get; set; }
    public DbSet<Passenger> PassengerSet { get; set; }
    public DbSet<Booking> BookingSet { get; set; }

    public VSFlyContext()
    {

    }
    public static string ConnectionString { get; set; } = @"Server=(localDB)\MSSQLLocalDB;Database=VSFlyDavidIsmael;" +
                                                  "Trusted_Connection=True;App=VSFlyDavidIsmael;MultipleActiveResultSets=true";

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseSqlServer(ConnectionString);
    }

    //2 foreign keys of same table -> flight
    //https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table
    //https://stackoverflow.com/questions/55970650/ef-core-may-cause-cycles-or-multiple-cascade-paths
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Flight>()
                  .HasOne(m => m.Destination)
                  .WithMany(t => t.Destinations)
                  .HasForeignKey(m => m.DestinationId)
                  .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Flight>()
                  .HasOne(m => m.Departure)
                  .WithMany(t => t.Departures)
                  .HasForeignKey(m => m.DepartureId)
                  .OnDelete(DeleteBehavior.Restrict);
    } 

  }
}
