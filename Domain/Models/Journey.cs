using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Models
{
    public class Journey : BaseModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
        public ICollection<JourneyFlight> JourneyFlights { get; set; }
    }
}