using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IFlight Flights { get; }
        public IJourney Journeys { get; }
        public IJourneyFlight JourneyFlights { get; }
        public ITransport Transports { get; }
        Task<int> SaveAsync();
    }
}