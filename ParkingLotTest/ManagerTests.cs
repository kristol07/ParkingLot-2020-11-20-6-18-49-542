using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot;
using Xunit;

namespace ParkingLotTest
{
    public class ManagerTests
    {
        [Fact]
        public void Should_able_to_assign_parking_boy_to_park_car()
        {
            var manager = new Manager();
            var lots = TestData.GetLots(3);
            var boys = TestData.GetBoys();
            manager.Boys = boys;
            boys.ForEach(boy => boy.Lots = lots.ToArray());
            var car = new Car("123");

            string message;
            Ticket ticket = manager.Park(car, out message);

            Assert.Equal(car.GetLicenseNumber(), ticket.GetLicenseNumber());
            Assert.Contains(boys, boy => boy.Id == ticket.GetBoyId());
            Assert.Empty(message);
        }

        [Fact]
        public void Should_able_to_assign_parking_boy_to_fetch_car()
        {
            var manager = new Manager();
            var lots = TestData.GetLots(3);
            var boys = TestData.GetBoys();
            manager.Boys = boys;
            boys.ForEach(boy => boy.Lots = lots.ToArray());
            var car = new Car("123");
            string message;
            Ticket ticket = manager.Park(car, out message);

            var fetchedCar = manager.Fetch(ticket, out message);

            Assert.Equal(car, fetchedCar);
        }
    }
}
