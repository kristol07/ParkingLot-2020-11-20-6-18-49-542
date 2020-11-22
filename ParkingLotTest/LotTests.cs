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
            boy.Lots = new Lot[] { lot };

            //when
            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);

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
            boy.Lots = new Lot[] { lot };

            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);

            Assert.True(lot.HaveCar(car));
        }

        [Fact]
        public void Should_have_changed_number_of_position_when_new_car_is_parked_in()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var initialLeftPosition = lot.LeftPosition;

            var ticket = boy.Park(car, out errorMessage);
            var newLeftPosition = lot.LeftPosition;

            Assert.Equal(1, initialLeftPosition - newLeftPosition);
        }

        [Fact]
        public void Should_have_changed_number_of_position_when_car_is_fetched()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);
            var initialLeftPosition = lot.LeftPosition;

            var fetchedCar = boy.Fetch(ticket, out errorMessage);
            var newLeftPosition = lot.LeftPosition;

            Assert.Equal(1, newLeftPosition - initialLeftPosition);
        }

        [Fact]
        public void Should_return_correct_car_when_ticket_is_provided()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);

            Car fetchedCar = boy.Fetch(ticket, out errorMessage);

            Assert.Equal(car.GetLicenseNumber(), fetchedCar.GetLicenseNumber());
        }

        [Fact]
        public void Should_return_correct_car_when_multiple_tickets_are_provided()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car1 = new Car("123");
            var car2 = new Car("456");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var ticket1 = boy.Park(car1, out errorMessage);
            var ticket2 = boy.Park(car2, out errorMessage);

            Car fetchedCar1 = boy.Fetch(ticket1, out errorMessage);
            Car fetchedCar2 = boy.Fetch(ticket2, out errorMessage);

            Assert.Equal(car1.GetLicenseNumber(), fetchedCar1.GetLicenseNumber());
            Assert.Equal(car2.GetLicenseNumber(), fetchedCar2.GetLicenseNumber());
        }

        [Fact]
        public void Should_return_no_car_when_ticket_is_not_provided()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var correctTicket = boy.Park(car, out errorMessage);
            Ticket wrongTicket = null;

            var fetchedCarWithWrongTicket = boy.Fetch(wrongTicket, out errorMessage);
            var fetchedCarWithCorrectTicket = boy.Fetch(correctTicket, out errorMessage);

            Assert.Null(fetchedCarWithWrongTicket);
            Assert.Equal(car, fetchedCarWithCorrectTicket);
        }

        [Fact]
        public void Should_return_no_car_when_ticket_is_used()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);
            Car fetchedCar = boy.Fetch(ticket, out errorMessage);

            Car tryFetchedCar = boy.Fetch(ticket, out errorMessage);

            Assert.Null(tryFetchedCar);
        }

        [Fact]
        public void Should_not_park_car_and_return_no_ticket_when_positions_are_all_occupied()
        {
            var boy = new Boy();
            var lot = new Lot(capacity: 1);
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var car1 = new Car("123");
            var ticket = boy.Park(car1, out errorMessage);
            var car2 = new Car("456");

            var tryTicket = boy.Park(car2, out errorMessage);
            Assert.Null(tryTicket);
            Assert.False(lot.HaveCar(car2));

            Car tryFetchedCar = boy.Fetch(ticket, out errorMessage);
            var newTryTicket = boy.Park(car2, out errorMessage);
            Assert.NotNull(newTryTicket);
            Assert.Equal(newTryTicket.GetLicenseNumber(), car2.GetLicenseNumber());
            Assert.True(lot.HaveCar(car2));
        }
    }
}
