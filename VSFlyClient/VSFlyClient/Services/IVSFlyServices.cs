using System.Collections.Generic;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Services
{
  public interface IVSFlyServices
  {
    Task<IEnumerable<BookingM>> GetBookings();
    Task<float> GetDestinationAverageTicketPrice(string Destination);
    Task<FlightM> GetFlight(int id);
    Task<IEnumerable<FlightM>> GetFlights();
    Task<float> GetFlightTicketPrice(int id);
    Task<float> GetFlightTotalTicketPrice(int id);
    Task<IEnumerable<PassengerM>> GetPassengers();
    Task<BookingM> PostBooking(BookingM booking);
    Task<PassengerM> PostPassenger(PassengerM passenger);
    Task<FlightM> UpdateFlight(FlightM flight);
  }
}