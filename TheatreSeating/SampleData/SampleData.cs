using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatreSeating.SampleData
{
    public class SampleData
    {
        /// <summary>
        /// sample list can be removed from solution, only for console run test
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, int[]> TheatreLayout()
        {
            var seats = new Dictionary<int, int[]>();
            seats.Add(1, new[] { 6, 6 });
            seats.Add(2, new[] { 3, 5, 5, 3 });
            seats.Add(3, new[] { 4, 6, 6, 4 });
            seats.Add(4, new[] { 2, 8, 8, 2 });
            seats.Add(5, new[] { 6, 6 });
            return seats;
        }

        /// <summary>
        /// sample list can be removed from solution, only for console run test
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> CustomerRequest()
        {
            var cusList = new Dictionary<string, int>();
            cusList.Add("Smith", 2);
            cusList.Add("Jones", 5);
            cusList.Add("Davis", 6);
            cusList.Add("Wilson", 100);
            cusList.Add("Johnson", 3);
            cusList.Add("Willsons", 4);
            cusList.Add("Brown", 8);
            cusList.Add("Miller", 12);
            return cusList;
        }
    }
}
