using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyAPI.Models
{
  public class BookingM
  {
    public int BookingId { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public float Price { get; set; }

  }
}
