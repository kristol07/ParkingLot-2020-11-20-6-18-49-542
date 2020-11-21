using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ParkingLot;

namespace ParkingLotTest
{
    public class LotTests
    {
        [Fact]
        public void Should_return_ticket_with_car_number_and_parkinglot_location_when_boy_park_car()
        {
            //given
            var boy = new Boy();
            var lot = new Lot("test location");
            var car = new Car("123");

            //when
            var ticket = boy.Park(car, lot);

            //then
            Assert.Equal("123", ticket.GetLicenseNumber());
            Assert.Equal("test location", ticket.GetLotLocation());
        }
    }
}
