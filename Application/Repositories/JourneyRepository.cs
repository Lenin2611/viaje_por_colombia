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
    public class JourneyRepository : GenericRepository<Journey>, IJourney
    {
        private readonly ViajeContext _context;

        public JourneyRepository(ViajeContext context) : base(context)
        {
            _context = context;
        }

        
    }
}