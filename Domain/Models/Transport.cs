using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Transport : BaseModel
    {
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        public int IdFlightFk { get; set; }
        public Flight Flights { get; set; }
    }
}