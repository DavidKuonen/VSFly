using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VSFlyClient.Models;

namespace VSFlyClient.Services
{
    public class VSFlyServices : IVSFlyServices
    {
        //readonly: if client is null it will put the new one in, if it there is one it wont put new one in
        //#Singleton
        private readonly HttpClient _client;
        private readonly string _baseuri;

        public VSFlyServices(HttpClient client)
        {
            _client = client;
            _baseuri = "https://localhost:44372/api/";
            _client.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        public async Task<IEnumerable<FlightM>> GetAvailableFlights()
        {
            var uri = _baseuri + "Flights/Available";

            var responseString = await _client.GetStringAsync(uri);
            var bookableFlights = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

            return bookableFlights;
        }

        public async Task<FlightM> GetFlight(int id)
        {
            var uri = _baseuri + "Flights/" + id;
            var responseString = await _client.GetStringAsync(uri);
            var flight = JsonConvert.DeserializeObject<FlightM>(responseString);

            return flight;

        }

        public async Task<FlightM> UpdateFlight(FlightM flight)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                     _baseuri + "Flights/" + flight.FlightId, flight);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            //ReadAsAsync requires Microsoft.AspNet.WebApi.Client from nugget manager
            flight = await response.Content.ReadAsAsync<FlightM>();
            return flight;

        }

        public async Task<IEnumerable<BookingM>> GetBookings()
        {
            var uri = _baseuri + "Bookings";

            var responseString = await _client.GetStringAsync(uri);
            var bookingList = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            return bookingList;
        }

        public async Task<BookingM> PostBooking(BookingM booking)
        {
            var uri = _baseuri + "Bookings";

            booking.BookingId = 0;

            HttpResponseMessage response = await _client.PostAsJsonAsync(
                      uri, booking);
            response.EnsureSuccessStatusCode();

            return booking;

        }

        public async Task<IEnumerable<BookingM>> GetBookingsByDestination(string destination)
        {
            var uri = _baseuri + "Bookings/Destination/" + destination;
            var responseString = await _client.GetStringAsync(uri);

            var bookingsfordestination = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            return bookingsfordestination;
        }

        public async Task<float> GetDestinationAverageTicketPrice(string Destination)
        {

            var uri = _baseuri + "AverageTicketPrice?destination=" + Destination;

            var responseString = await _client.GetStringAsync(uri);
            var average = JsonConvert.DeserializeObject<float>(responseString);

            return average;

        }

        public async Task<float> GetFlightTicketPrice(int id)
        {
            var uri = _baseuri + "Ticket/" + id;

            var responseString = await _client.GetStringAsync(uri);
            var ticketPrice = JsonConvert.DeserializeObject<float>(responseString);

            return ticketPrice;
        }

        public async Task<float> GetFlightTotalTicketPrice(int id)
        {
            var uri = _baseuri + "TotalTicketPrice/" + id;

            var responseString = await _client.GetStringAsync(uri);
            var totalTicketPrice = JsonConvert.DeserializeObject<float>(responseString);

            return totalTicketPrice;
        }

        public async Task<IEnumerable<PassengerM>> GetPassengers()
        {
            var uri = _baseuri + "Passengers";

            var responseString = await _client.GetStringAsync(uri);
            var passengers = JsonConvert.DeserializeObject<IEnumerable<PassengerM>>(responseString);

            return passengers;
        }

        public async Task<PassengerM> PostPassenger(PassengerM passenger)
        {
            var uri = _baseuri + "Passengers";

            HttpResponseMessage response = await _client.PostAsJsonAsync(
                      uri, passenger);
            response.EnsureSuccessStatusCode();

            return passenger;

        }

        public async Task<IEnumerable<BookingM>> GetMyBookings(int id)
        {
            var uri = _baseuri + "Bookings/Passenger/" + id;

            var responseString = await _client.GetStringAsync(uri);
            var bookings = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

            return bookings;
        }

        public async void DeleteBooking(int id)
        {
            var uri = _baseuri + "Bookings/" + id;

            await _client.DeleteAsync(uri);
        }

    }
}

