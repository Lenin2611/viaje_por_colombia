using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class FlightDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public TransportDto Transport { get; set; }
    }
}