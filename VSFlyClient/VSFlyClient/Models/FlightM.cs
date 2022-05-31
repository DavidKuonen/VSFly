using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyClient.Models
{
  public class FlightM
  {
    public int FlightId { get; set; }
    public string Destination { get; set; }
    public string Departure { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime DestinationTime { get; set; }
    public float BasePrice { get; set; }
    public int Seats { get; set; }
    public int AvailableSeats { get; set; }
  }
}
