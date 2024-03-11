using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json.Linq;
using Persistence.Data;

namespace Application.Repositories
{
    public class JourneyFlightRepository : GenericRepository<JourneyFlight>, IJourneyFlight
    {
        private readonly ViajeContext _context;

        public JourneyFlightRepository(ViajeContext context) : base(context)
        {
            _context = context;
        }

        
    }
}