using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Persistence.Data;

namespace Application.Repositories
{
    public class TransportRepository : GenericRepository<Transport>, ITransport
    {
        private readonly ViajeContext _context;

        public TransportRepository(ViajeContext context) : base(context)
        {
            _context = context;
        }
    }
}