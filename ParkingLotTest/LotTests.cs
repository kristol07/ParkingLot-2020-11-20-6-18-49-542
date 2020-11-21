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
            var lot = new Lot();
            var car = new Car("123");

            //when
            var ticket = boy.Park(car, lot);

            //then
            Assert.Equal("123", ticket.GetLicenseNumber());
            Assert.Equal("test location", ticket.GetLotLocation());
        }

        [Fact]
        public void ParkingLot_should_have_the_car_when_car_is_parked_in()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");

            var ticket = boy.Park(car, lot);
            var carsParked = lot.GetAllCars();

            Assert.True(carsParked.Contains(car));
        }
    }
}
