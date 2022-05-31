using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSFlyDavidIsmael
{
  // 2 foreign keys of same table
  // https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table
  public class Place
  {
    [Key]
    public int PlaceId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Flight> Destinations { get; set; }
    public virtual ICollection<Flight> Departures { get; set; }
  }
}
