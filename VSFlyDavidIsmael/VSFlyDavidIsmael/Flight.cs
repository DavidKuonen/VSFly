using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyDavidIsmael
{
  //2 foreign key of same table
  //https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table
  //https://stackoverflow.com/questions/55970650/ef-core-may-cause-cycles-or-multiple-cascade-paths
  public class Flight
  {
    [Key]
    public int FlightId { get; set; }
  
    //[ForeignKey("Destination")]
    public int DestinationId { get; set; }
    public virtual Place Destination { get; set; }

    //[ForeignKey("Departure")]
    public int DepartureId { get; set; }
    public virtual Place Departure { get; set; }

    public DateTime DepartureTime { get; set; }
    public DateTime DestinationTime { get; set; }
    public float BasePrice { get; set; }
    public int Seats { get; set; }
    public int AvailableSeats { get; set; }

    public virtual ICollection<Booking> BookingSet { get; set; }
  }
}
