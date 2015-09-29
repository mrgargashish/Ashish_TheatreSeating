using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating
{
    public class Customer
    {
        public int SeatRequest { get; set; }
        public string Name { get; set; }
        public bool IsSeatAllocated { get; set; }
        public string Error { get; set; }
        public int AllocatedRow { get; set; }
        public int AllocatedSection { get; set; }

        /// <summary>
        /// send customer data into it and it builds the customer model list for same.
        /// </summary>
        /// <param name="cusList"></param>
        /// <returns></returns>
        public static IList<Customer> GetCustomerLList(IDictionary<string, int> cusList)
        {
            var customerList = cusList.Select(x =>
                new Customer
                {
                    SeatRequest = x.Value,
                    Name = x.Key
                }).ToList();
            return customerList;
        }

        /// <summary>
        /// allocate customer section and row
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="sectionNumber"></param>
        public void AllocateSeat(int rowNum, int sectionNumber)
        {
            AllocatedRow = rowNum;
            AllocatedSection = sectionNumber;
            IsSeatAllocated = true;
        }

        /// <summary>
        /// deallocate customer section and row
        /// </summary>
        public void DeallocateSeat()
        {
            AllocatedRow = 0; AllocatedSection = 0;
            IsSeatAllocated = false;
        }

    }
}
