using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreSeating;
using TheatreSeating.SampleData;

namespace TheatreSeatingTest
{
    /// <summary>
    /// Tests for the Chat Service Class
    /// Documentation: 
    /// </summary>
    [TestFixture, Description("Tests for Theatre System")]
    public class TheatreSeatingTests
    {
        private TheatreSeating.ArrangeSeat _theatre;

        private IList<TheatreSeating.Customer> _modelCustomer;
     

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            var SampleDataForTheatreLayout = SampleData.TheatreLayout();
            var SampleDataForCustomerRequest = SampleData.CustomerRequest();
            _theatre = TheatreSeating.ArrangeSeat.BuildTheatreSeatLayout(SampleDataForTheatreLayout);
            _modelCustomer = TheatreSeating.Customer.GetCustomerLList(SampleDataForCustomerRequest);

            //sample data
            //seats.Add(1, new[] { 6, 6 });
            //seats.Add(2, new[] { 3, 5, 5, 3 });
            //seats.Add(3, new[] { 4, 6, 6, 4 });
            //seats.Add(4, new[] { 2, 8, 8, 2 });
            //seats.Add(5, new[] { 6, 6 });

            //cusList.Add("Smith", 2);
            //cusList.Add("Jones", 5);
            //cusList.Add("Davis", 6);
            //cusList.Add("Wilson", 100);
            //cusList.Add("Johnson", 3);
            //cusList.Add("Willsons", 4);
            //cusList.Add("Brown", 8);
            //cusList.Add("Miller", 12);
        }

        [Test]
        public void AllocateThisSectionTest()
        {
            var model = new Section
            {
                TotalnumberofSeats = 6,
                OccupiedSeats = 1,
                GivenTo = new List<Customer>()
            };

            var cus = new Customer
            {
                Name = "Ashish",
                SeatRequest = 5
            };
            model.AllocateThisSection(new Customer[] {cus}, 5);
            Assert.AreEqual(0, model.EmpltySeats);
            Assert.AreEqual(6, model.OccupiedSeats);
        }


        [Test]
        public void DeallocateThisSectionTest()
        {
            var model = new Section
            {
                TotalnumberofSeats = 6,
                OccupiedSeats = 1,
                 GivenTo = new List<Customer>()
            };

            var cus = new Customer
            {
                Name = "Ashish",
                SeatRequest = 5
            };
            model.AllocateThisSection(new Customer[] { cus }, 5);

            model.DeallocateThisAllocation();

            Assert.AreEqual(6, model.EmpltySeats);
            Assert.AreEqual(0, model.GivenTo.Count);
            Assert.AreEqual(0, model.OccupiedSeats);
        }

        [Test]
        public void AllocateSeatTest()
        {  var cus = new Customer
            {
                Name = "Ashish",
                SeatRequest = 5
            };

        cus.AllocateSeat(3, 1);
        Assert.AreEqual(3, cus.AllocatedRow);
        Assert.AreEqual(1, cus.AllocatedSection);
        }

        [Test]
        public void DeallocateSeat()
        {
            var cus = new Customer
            {
                Name = "Ashish",
                SeatRequest = 5
            };

            cus.AllocateSeat(3, 1);
            cus.DeallocateSeat();
            Assert.AreEqual(0, cus.AllocatedRow);
            Assert.AreEqual(0, cus.AllocatedSection);
        }

       


        [Test]
        public void BestFitArrangementTest()
        {

            var result = _theatre.BestFitArrangement(_modelCustomer);

            foreach (var item in _modelCustomer)
            {
                if (item.Name.Equals("Smith"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                }
                if (item.Name.Equals("Jones"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                }
                if (item.Name.Equals("Davis"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);;
                }
                if (item.Name.Equals("Johnson"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                }
                if (item.Name.Equals("Willsons"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                }
                if (item.Name.Equals("Wilson"))
                {
                    Assert.AreEqual(false, item.IsSeatAllocated);
                }
                if (item.Name.Equals("Miller"))
                {
                    Assert.AreEqual(false, item.IsSeatAllocated);
                }
            }

        }



        [Test]
        public void CompactingTest()
        {
           var initalList= _theatre.BestFitArrangement(_modelCustomer);
           Assert.AreNotEqual(0, initalList.Where(x => x.Rownumber == 1).Sum(x => x.EmpltySeats));
           var resultAfter = _theatre.Compacting(_modelCustomer);
           Assert.AreEqual(0, initalList.Where(x => x.Rownumber == 1).Sum(x => x.EmpltySeats));
        }



        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
              _theatre = null;
            _modelCustomer=null;

        }
    }
}
