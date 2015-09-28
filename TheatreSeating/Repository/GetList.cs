using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreSeating.Models;

namespace TheatreSeating.Repository
{
   public class GetList
    {
        /// <summary>
        /// send sample sparse array and row number and it will convert into model
        /// </summary>
        /// <param name="seats"></param>
        /// <returns></returns>
        public IList<Section> BuildTheatreSeatLayout(IDictionary<int, int[]> seats)
        {
            var seatsData = new List<Section>();

            foreach (var seat in seats)
            {
                for (int i = 0; i < seat.Value.Length; i++)
                {
                    seatsData.Add(new Section
                    {
                        Rownumber = seat.Key,
                        SectionId = i + 1,
                        TotalnumberofSeats = seat.Value[i],
                        GivenTo = new List<string>()
                    });
                }

            }
            return seatsData;
        }


        /// <summary>
        /// Send Sample data / name-int into it and it will convert to model for customerList
        /// </summary>
        /// <param name="cusList"></param>
        /// <returns></returns>
        public IList<Customer> GetCustomerLList(IDictionary<string, int> cusList)
        {
            var customerList = cusList.Select(x =>
                new Customer
                {
                    SeatRequest = x.Value,
                    Name = x.Key
                }).ToList();
            return customerList;
        }

    }
}
