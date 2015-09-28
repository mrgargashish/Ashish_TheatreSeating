using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreSeating.Models;

namespace TheatreSeating
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //build sample data for console app
            var SampleDataForTheatreLayout = SampleData.SampleData.TheatreLayout();
            var SampleDataForCustomerRequest = SampleData.SampleData.CustomerRequest();

            //build model from sample data
            var repo = new Repository.GetList();
            var TheatreLayoutSection = repo.BuildTheatreSeatLayout(SampleDataForTheatreLayout);
            var CustomerRequestList = repo.GetCustomerLList(SampleDataForCustomerRequest);

            //perform business logic first bestfit than compacting
            BusinessLogic.ArrangeSeat.BestFitArrangement(TheatreLayoutSection, CustomerRequestList);
            BusinessLogic.ArrangeSeat.Compacting(TheatreLayoutSection);

            //display result in console
            foreach (var cus in CustomerRequestList)
            {
                var assignment = TheatreLayoutSection.FirstOrDefault(x => x.GivenTo.Contains(cus.Name));
                if (assignment == null)
                {
                    Console.WriteLine(String.Format("{0} {1}",
                   cus.Name, cus.Error));
                }
                else
                {
                    Console.WriteLine(String.Format("{0} Row {1} Section {2} {3}",
                        cus.Name, assignment.Rownumber, assignment.SectionId, cus.Error));
                }
            }
            //wait
            Console.ReadLine();
        }
    }
}
