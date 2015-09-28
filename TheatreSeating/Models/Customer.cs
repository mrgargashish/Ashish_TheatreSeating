using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating.Models
{
   public class Customer
    {
        public int SeatRequest { get; set; }
        public string Name { get; set; }
        public bool IsSeatAllocated { get; set; }
        public string Error { get; set; }
    }
}
