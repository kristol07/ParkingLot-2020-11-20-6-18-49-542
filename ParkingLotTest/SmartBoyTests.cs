using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot;
using Xunit;

namespace ParkingLotTest
{
    public class SmartBoyTests
    {
        [Fact]
        public void Should_park_new_car_only_to_lot_which_has_more_positions()
        {
            var boy = new SmartBoy();
            var lot1 = new Lot("loca1", 2);
            var lot2 = new Lot("loca2", 3);
            boy.Lots = new[] { lot1, lot2 };

            string message;
            var locations = new List<string>();
            foreach (var car in TestData.GetCars(4))
            {
                var ticket = boy.Park(car, out message);
                locations.Add(ticket.GetLotLocation());
                Assert.Empty(message);
            }

            Assert.Equal(lot2.GetLocation(), locations[0]);
            Assert.Equal(lot2.GetLocation(), locations[1]);
            Assert.Equal(lot1.GetLocation(), locations[2]);
            Assert.Equal(lot2.GetLocation(), locations[3]);
        }

        [Fact]
        public void Should_return_error_message_when_all_lots_managed_are_full_for_parking_new_car()
        {
            var boy = new SmartBoy();
            var lot1 = new Lot("loca1", 2);
            var lot2 = new Lot("loca2", 3);
            boy.Lots = new[] { lot1, lot2 };

            string message;
            var messages = new List<string>();
            foreach (var car in TestData.GetCars(lot1.Capacity + lot2.Capacity + 1))
            {
                var ticket = boy.Park(car, out message);
                messages.Add(message);
            }

            Assert.True(messages.Where(message => messages.IndexOf(message) < (lot1.Capacity + lot2.Capacity))
                .All(message => string.IsNullOrEmpty(message)));

            Assert.Equal("Not enough position.", messages.Last());
        }
    }

    public class SuperSmartBoyTests
    {
        [Fact]
        public void Should_park_new_car_only_to_lot_which_has_larger_rate()
        {
            var boy = new SuperSmartBoy();
            var lot1 = new Lot("loca1", 2);
            var lot2 = new Lot("loca2", 3);
            boy.Lots = new[] { lot1, lot2 };

            string message;
            var locations = new List<string>();
            foreach (var car in TestData.GetCars(4))
            {
                var ticket = boy.Park(car, out message);
                locations.Add(ticket.GetLotLocation());
                Assert.Empty(message);
            }

            Assert.Equal(lot2.GetLocation(), locations[0]);  // lot1: 1 || lot2: 1
            Assert.Equal(lot1.GetLocation(), locations[1]);  // lot1: 1 || lot2: 2/3
            Assert.Equal(lot2.GetLocation(), locations[2]);  // lot1: 1/2 || lot2: 2/3
            Assert.Equal(lot1.GetLocation(), locations[3]);  // lot1: 1/2 || lot2: 1/3
        }

        [Fact]
        public void Should_return_error_message_when_all_lots_managed_are_full_for_parking_new_car()
        {
            var boy = new SuperSmartBoy();
            var lot1 = new Lot("loca1", 2);
            var lot2 = new Lot("loca2", 3);
            boy.Lots = new[] { lot1, lot2 };

            string message;
            var messages = new List<string>();
            foreach (var car in TestData.GetCars(lot1.Capacity + lot2.Capacity + 1))
            {
                var ticket = boy.Park(car, out message);
                messages.Add(message);
            }

            Assert.True(messages.Where(message => messages.IndexOf(message) < (lot1.Capacity + lot2.Capacity))
                .All(message => string.IsNullOrEmpty(message)));

            Assert.Equal("Not enough position.", messages.Last());
        }
    }
}
