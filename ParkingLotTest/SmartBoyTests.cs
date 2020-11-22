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
    }
}
