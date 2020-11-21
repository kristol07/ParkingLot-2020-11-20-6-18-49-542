﻿using System;
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
            Assert.Equal(car.GetLicenseNumber(), ticket.GetLicenseNumber());
            Assert.Equal(lot.GetLocation(), ticket.GetLotLocation());
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
            var initialLeftPosition = lot.LeftPosition;

            var ticket = boy.Park(car, lot);
            var newLeftPosition = lot.LeftPosition;

            Assert.Equal(1, initialLeftPosition - newLeftPosition);
        }

        [Fact]
        public void Should_have_changed_number_of_position_when_car_is_fetched()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            var ticket = boy.Park(car, lot);
            var initialLeftPosition = lot.LeftPosition;

            var fecthedCar = boy.Fetch(ticket, lot);
            var newLeftPosition = lot.LeftPosition;

            Assert.Equal(1, newLeftPosition - initialLeftPosition);
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

        [Fact]
        public void Should_return_no_car_when_ticket_is_wrong_with_boyId()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            var correctTicket = boy.Park(car, lot);
            var wrongTicket = new Ticket(car.GetLicenseNumber(), lot.GetLocation(), boy.GetId() + 1);

            var fetchedCarWithWrongTicket = boy.Fetch(wrongTicket, lot);
            var fetchedCarWithCorrectTicket = boy.Fetch(correctTicket, lot);

            Assert.Null(fetchedCarWithWrongTicket);
            Assert.Equal(car, fetchedCarWithCorrectTicket);
        }
    }
}
