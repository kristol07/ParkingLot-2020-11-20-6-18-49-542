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
            var selectedBoy = boys.First();
            var car = new Car("123");

            string message;
            Ticket ticket = manager.AssignPark(car, selectedBoy, out message);

            Assert.Equal(car.GetLicenseNumber(), ticket.GetLicenseNumber());
            Assert.Equal(selectedBoy.Id, ticket.GetBoyId());
            Assert.Empty(message);
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_used()
        {
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_not_matched_for_boy()
        {
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_not_provided_for_fetching_car()
        {
        }

        [Fact]
        public void Should_able_to_assign_parking_boy_to_fetch_car()
        {
            var manager = new Manager();
            var lots = TestData.GetLots(3);
            var boys = TestData.GetBoys();
            manager.Boys = boys;
            boys.ForEach(boy => boy.Lots = lots.ToArray());
            var selectedBoy = boys.First();
            var car = new Car("123");
            string message;
            Ticket ticket = manager.AssignPark(car, selectedBoy, out message);

            var fetchedCar = manager.AssignFetch(ticket, selectedBoy, out message);

            Assert.Equal(car, fetchedCar);
            Assert.Empty(message);
        }

        [Fact]
        public void Should_able_to_park_car_by_self()
        {
            var manager = new Manager();
            var lots = TestData.GetLots(3);
            var boys = TestData.GetBoys();
            manager.Boys = boys;
            manager.Lots = lots.ToArray();
            var car = new Car("123");

            string message;
            Ticket ticket = manager.Park(car, out message);

            Assert.Equal(car.GetLicenseNumber(), ticket.GetLicenseNumber());
            Assert.Contains(lots, lot => lot.GetLocation() == ticket.GetLotLocation());
            Assert.True(manager.Id == ticket.GetBoyId());
            Assert.Empty(message);
        }

        [Fact]
        public void Should_able_to_fetch_car_by_self()
        {
            var manager = new Manager();
            var lots = TestData.GetLots(3);
            var boys = TestData.GetBoys();
            manager.Boys = boys;
            manager.Lots = lots.ToArray();
            boys.ForEach(boy => boy.Lots = lots.ToArray());
            var car = new Car("123");
            string message;
            Ticket ticket = manager.Park(car, out message);

            var fetchedCar = manager.Fetch(ticket, out message);

            Assert.Equal(car, fetchedCar);
        }
    }
}
