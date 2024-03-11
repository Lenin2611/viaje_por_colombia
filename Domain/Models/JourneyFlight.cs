using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class JourneyFlight : BaseModel
    {
        public int IdJourneyFk { get; set; }
        public int IdFlightFk { get; set; }
        public Journey Journeys { get; set; }
        public Flight Flights { get; set; }
    }
}