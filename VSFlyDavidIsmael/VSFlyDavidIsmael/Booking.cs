using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyDavidIsmael
{
  public class Booking
  {
    [Key]
    public int BookingId { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public float Price { get; set; }
    public virtual Flight Flight { get; set; }
    public virtual Passenger Passenger { get; set; }
  }
}
