using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyDavidIsmael
{
  public class Passenger
  {
    [Key]
    public int PassengerId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public virtual ICollection<Booking> BookingSet { get; set; }
  }
}
