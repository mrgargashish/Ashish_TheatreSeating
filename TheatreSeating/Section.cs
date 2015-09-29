using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating
{
    public class Section
    {
        public int Rownumber { get; set; }
        public int SectionId { get; set; }
        public int TotalnumberofSeats { get; set; }
        public int OccupiedSeats { get; set; }
        public List<Customer> GivenTo { get; set; }

        public int EmpltySeats
        {
            get { return TotalnumberofSeats - OccupiedSeats; }
        }


        public void AllocateThisSection(Customer[] name, int requiredSeats)
        {
            if (EmpltySeats >= requiredSeats)
            {
                OccupiedSeats += requiredSeats;
                GivenTo.AddRange(name);
                this.GivenTo.All(x => x.IsSeatAllocated = true);
            }
            else
            {
                throw new Exception("Section cannot be allocated, not enough seats");
            }
        }

        public void DeallocateThisAllocation()
        {
            OccupiedSeats = 0;
            GivenTo.Clear();
        }
    }
}
