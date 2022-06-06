using System.Collections.Generic;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Services
{
  public interface IVSFlyServices
  {
    Task<IEnumerable<BookingM>> GetBookings();
    Task<FlightM> GetFlight(int id);
    Task<IEnumerable<FlightM>> GetFlights();
    Task<BookingM> PostBooking(BookingM booking);
    Task<FlightM> UpdateFlight(FlightM flight);
  }
}