﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyClient.Models
{
  public class BookingM
  {
    public int BookingId { get; set; }
    public int FlightId { get; set; }
    public string Passenger { get; set; }
    public float Price { get; set; }
  }
}
