using System.Collections.Generic;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Services
{
  public interface IVSFlyServices
  {
    Task<IEnumerable<BookingM>> GetBookings();
    Task<IEnumerable<FlightM>> GetFlights();
  }
}