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
        public void Should_return_ticket_with_car_number_and_lot_location_when_boy_park_car()
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
        public void Should_have_the_car_when_car_is_parked_in()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");

            var ticket = boy.Park(car, lot);

            Assert.True(lot.HaveCar(car));
        }

        [Fact]
        public void Should_have_changed_number_of_position_when_new_car_is_parked_in()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            var initialCapacity = lot.GetCapacity();

            var ticket = boy.Park(car, lot);
            var newCapacity = lot.GetCapacity();

            Assert.Equal(1, initialCapacity - newCapacity);
        }

        [Fact]
        public void Should_have_changed_number_of_position_when_car_is_fetched()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            var ticket = boy.Park(car, lot);
            var initialCapacity = lot.GetCapacity();

            boy.Fetch(ticket, lot);
            var newCapacity = lot.GetCapacity();

            Assert.Equal(1, newCapacity - initialCapacity);
        }

        [Fact]
        public void Should_return_correct_car_when_ticket_is_provided()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            var ticket = boy.Park(car, lot);

            Car fetchedCar = boy.Fetch(ticket, lot);

            Assert.Equal(car.GetLicenseNumber(), fetchedCar.GetLicenseNumber());
        }
    }
}
