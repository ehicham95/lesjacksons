using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dtos
{
    public class PlatformCreateDto
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public decimal Cost { get; set; }
    }
}