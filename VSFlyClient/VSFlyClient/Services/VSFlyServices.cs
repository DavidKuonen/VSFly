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

    public async Task<IEnumerable<FlightM>> GetFlights()
    {
      var uri = _baseuri + "Flights";

      var responseString = await _client.GetStringAsync(uri);
      var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

      //only bookable flights will be returned
      List<FlightM> bookableFlights = new List<FlightM>();

      foreach (FlightM f in flightList)
      {
        if (f.AvailableSeats > 0)
        {
          bookableFlights.Add(f);
        }
      }

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
               _baseuri + "Flights/"+flight.FlightId, flight);
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

  }
}

