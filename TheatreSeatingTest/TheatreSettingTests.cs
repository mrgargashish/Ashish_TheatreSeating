using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatreSeating;

namespace TheatreSeatingTest
{
    /// <summary>
    /// Tests for the Chat Service Class
    /// Documentation: 
    /// </summary>
    [TestFixture, Description("Tests for Theatre System")]
    public class TheatreSettingTests
    {
        //class variable
        private TheatreSeating.Repository.GetList _repo;
        //class variable
        private IList<TheatreSeating.Models.Customer> _modelCustomer;
        //class variable
        private IList<TheatreSeating.Models.Section> _modelSection;
        //class variable
        private TheatreSeating.BusinessLogic.ArrangeSeat _blayer;

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of Chat Service
            _repo = new TheatreSeating.Repository.GetList();
            _modelSection = _repo.BuildTheatreSeatLayout(TheatreSeating.SampleData.SampleData.TheatreLayout());
            _modelCustomer = _repo.GetCustomerLList(TheatreSeating.SampleData.SampleData.CustomerRequest());

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
        public void BestFitArrangementTest()
        {

            var result = TheatreSeating.BusinessLogic.ArrangeSeat.BestFitArrangement(_modelSection, _modelCustomer);

            foreach (var item in _modelCustomer)
            {
                if (item.Name.Equals("Smith"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                    Assert.AreEqual(2, _modelSection.First(x => x.GivenTo.Contains("Smith")).OccupiedSeats);
                }
                if (item.Name.Equals("Jones"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                    Assert.AreEqual(5, _modelSection.First(x => x.GivenTo.Contains("Jones")).OccupiedSeats);
                }
                if (item.Name.Equals("Davis"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                    Assert.AreEqual(6, _modelSection.First(x => x.GivenTo.Contains("Davis")).OccupiedSeats);
                }
                if (item.Name.Equals("Johnson"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                    Assert.AreEqual(3, _modelSection.First(x => x.GivenTo.Contains("Johnson")).OccupiedSeats);
                }
                if (item.Name.Equals("Willsons"))
                {
                    Assert.AreEqual(true, item.IsSeatAllocated);
                    Assert.AreEqual(4, _modelSection.First(x => x.GivenTo.Contains("Willsons")).OccupiedSeats);
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
            TheatreSeating.BusinessLogic.ArrangeSeat.BestFitArrangement(_modelSection, _modelCustomer);
            var resultAfter = TheatreSeating.BusinessLogic.ArrangeSeat.Compacting(_modelSection);
            Assert.AreEqual(0, resultAfter.First().EmpltySeats);
        }



        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            _repo = null;
            _modelSection.Clear();
            _modelCustomer.Clear();

        }
    }
}
