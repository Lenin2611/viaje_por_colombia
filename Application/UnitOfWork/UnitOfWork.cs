using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ViajeContext _context;
        private IFlight _Flights;
        private IJourney _Journeys;
        private IJourneyFlight _JourneyFlights;
        private ITransport _Transports;

        public UnitOfWork(ViajeContext context)
        {
            _context = context;
        }

        public IFlight Flights
        {
            get
            {
                if (_Flights == null)
                {
                    _Flights = new FlightRepository(_context);
                }
                return _Flights;
            }
        }
        public IJourney Journeys
        {
            get
            {
                if (_Journeys == null)
                {
                    _Journeys = new JourneyRepository(_context);
                }
                return _Journeys;
            }
        }
        public IJourneyFlight JourneyFlights
        {
            get
            {
                if (_JourneyFlights == null)
                {
                    _JourneyFlights = new JourneyFlightRepository(_context);
                }
                return _JourneyFlights;
            }
        }
        public ITransport Transports
        {
            get
            {
                if (_Transports == null)
                {
                    _Transports = new TransportRepository(_context);
                }
                return _Transports;
            }
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}