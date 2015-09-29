using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var theatreSeating = TheatreSeating.ArrangeSeat.BuildTheatreSeatLayout(SampleDataForTheatreLayout);
            var CustomerDemandModel = Customer.GetCustomerLList(SampleDataForCustomerRequest);

            //perform business logic first bestfit than compacting
            theatreSeating.BestFitArrangement(CustomerDemandModel);
            theatreSeating.Compacting(CustomerDemandModel);

            //display result in console
            foreach (var cus in CustomerDemandModel)
            {
                if (cus.IsSeatAllocated == true)
                    Console.WriteLine(String.Format("{0} Row {1} Section {2} {3}",
                        cus.Name, cus.AllocatedRow, cus.AllocatedSection, cus.Error));
                else
                    Console.WriteLine(String.Format("{0} {1}", cus.Name, cus.Error));
            }
            //wait
            Console.ReadLine();
        }
    }
}
