using System.Collections.Generic;
using System.Linq;

namespace TheatreSeating
{
   public class ArrangeSeat
    {
       private readonly IList<Section> _theatreList;

       private ArrangeSeat(IList<Section> sectionList)
       {
           _theatreList = sectionList;
       }

        /// <summary>
        /// send sample sparse array and row number and it will convert into model
        /// </summary>
        /// <param name="seats"></param>
        /// <returns></returns>
       public static ArrangeSeat BuildTheatreSeatLayout(IDictionary<int, int[]> seats)
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
                        GivenTo = new List<Customer>()
                    });
                }

            }
            return new ArrangeSeat(seatsData);
        }

       /// <summary>
       /// this function performs best fit in the array for sections for each group
       /// </summary>
       /// <param name="_theatreList"></param>
       /// <param name="customerList"></param>
       /// <returns></returns>
       public IList<Section> BestFitArrangement(IList<Customer> customerList)
        {
            foreach (var cus in customerList)
            {
                var result = _theatreList.OrderBy(x => x.EmpltySeats).
                    FirstOrDefault(x => x.EmpltySeats >= cus.SeatRequest);
                if (result != null)
                {
                    result.AllocateThisSection(new Customer[] { cus }, cus.SeatRequest);
                    cus.AllocateSeat(result.Rownumber, result.SectionId);
                }
                else
                {
                    var splitResult = _theatreList.
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
            return _theatreList;
        }

       /// <summary>
       /// this function performs compacting that is bringing in last sitting group to front as so on
       /// </summary>
       /// <param name="_theatreList"></param>
       /// <returns></returns>
       public IList<Section> Compacting(IList<Customer> customerList)
        {
            for (int i = 0; i < _theatreList.Count; i++)
            {
                for (int j = _theatreList.Count - 1; j >= i; j--)
                {
                    if (_theatreList[i].EmpltySeats >= _theatreList[j].
                        OccupiedSeats && _theatreList[j].OccupiedSeats > 0)
                    {
                        _theatreList[i].AllocateThisSection(_theatreList[j].
                            GivenTo.ToArray(), _theatreList[j].OccupiedSeats);

                        _theatreList[j].DeallocateThisAllocation();
                       
                        var customers = customerList.Intersect(_theatreList[i].GivenTo);
                        customers.ToList().ForEach(x => x.AllocateSeat(_theatreList[i].Rownumber, 
                            _theatreList[i].SectionId));
                    }
                }
            }
            return _theatreList;
        }

    }
}
