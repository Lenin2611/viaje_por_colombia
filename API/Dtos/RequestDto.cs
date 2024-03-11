using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RequestDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}