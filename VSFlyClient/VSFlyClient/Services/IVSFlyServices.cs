using System.Collections.Generic;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Services
{
    public interface IVSFlyServices
    {
        void DeleteBooking(int id);
        Task<IEnumerable<FlightM>> GetAvailableFlights();
        Task<IEnumerable<BookingM>> GetBookings();
        Task<IEnumerable<BookingM>> GetBookingsByDestination(string destination);
        Task<float> GetDestinationAverageTicketPrice(string Destination);
        Task<FlightM> GetFlight(int id);
        Task<float> GetFlightTicketPrice(int id);
        Task<float> GetFlightTotalTicketPrice(int id);
        Task<IEnumerable<BookingM>> GetMyBookings(int id);
        Task<IEnumerable<PassengerM>> GetPassengers();
        Task<BookingM> PostBooking(BookingM booking);
        Task<PassengerM> PostPassenger(PassengerM passenger);
        Task<FlightM> UpdateFlight(FlightM flight);
    }
}