using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }

    public async Task<IEnumerable<FlightM>> GetFlights()
    {
      var uri = _baseuri + "Flights";

      var responseString = await _client.GetStringAsync(uri);
      var flightList = JsonConvert.DeserializeObject<IEnumerable<FlightM>>(responseString);

      return flightList;
    }

    public async Task<IEnumerable<BookingM>> GetBookings()
    {
      var uri = _baseuri + "Bookings";

      var responseString = await _client.GetStringAsync(uri);
      var bookingList = JsonConvert.DeserializeObject<IEnumerable<BookingM>>(responseString);

      return bookingList;
    }

  }
}

