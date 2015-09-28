using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreSeating.Models;

namespace TheatreSeating.BusinessLogic
{
   public class ArrangeSeat
    {
       /// <summary>
       /// this function performs best fit in the array for sections for each group
       /// </summary>
       /// <param name="section"></param>
       /// <param name="customerList"></param>
       /// <returns></returns>
        public static IList<Section> BestFitArrangement(IList<Section> section, IList<Customer> customerList)
        {
            foreach (var cus in customerList)
            {
                var result = section.OrderBy(x => x.EmpltySeats).
                    FirstOrDefault(x => x.EmpltySeats >= cus.SeatRequest);
                if (result != null)
                {
                    result.OccupiedSeats += cus.SeatRequest;
                    result.GivenTo.Add(cus.Name);
                    cus.IsSeatAllocated = true;
                }
                else
                {
                    var splitResult = section.
                        GroupBy(x => x.Rownumber).Select
                        (y => y.Sum(x => x.EmpltySeats)).Any
                        (y => y > cus.SeatRequest);
                    if (splitResult)
                    {
                        cus.Error = "Call to split party";
                        cus.IsSeatAllocated = false;
                    }
                    else
                    {
                        cus.Error = "Sorry, we can't handle your party";
                        cus.IsSeatAllocated = false;
                    }
                }
            }
            return section;
        }

       /// <summary>
       /// this function performs compacting that is bringing in last sitting group to front as so on
       /// </summary>
       /// <param name="section"></param>
       /// <returns></returns>
        public static IList<Section> Compacting(IList<Section> section)
        {
            for (int i = 0; i < section.Count; i++)
            {
                for (int j = section.Count - 1; j >= i; j--)
                {
                    if (section[i].EmpltySeats >= section[j].OccupiedSeats && section[j].OccupiedSeats > 0)
                    {
                        section[i].OccupiedSeats += section[j].OccupiedSeats;
                        section[i].GivenTo.AddRange(section[j].GivenTo);
                        section[j].GivenTo.Clear();
                        section[j].OccupiedSeats = 0;
                    }
                }
            }
            return section;
        }

    }
}
