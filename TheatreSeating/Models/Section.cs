using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating.Models
{
   public class Section
    {
        public int Rownumber { get; set; }
        public int SectionId { get; set; }
        public int TotalnumberofSeats { get; set; }
        public int OccupiedSeats { get; set; }
        public List<string> GivenTo { get; set; }

        public int EmpltySeats
        {
            get { return TotalnumberofSeats - OccupiedSeats; }
        }

    }
}
