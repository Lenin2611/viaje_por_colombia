using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Flight : BaseModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public ICollection<Transport> Transports { get; set; }
        public ICollection<Journey> Journeys { get; set; } = new HashSet<Journey>();
        public ICollection<JourneyFlight> JourneyFlights { get; set; }
    }
}