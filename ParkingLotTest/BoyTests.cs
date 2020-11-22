using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot;
using Xunit;

namespace ParkingLotTest
{
    public class BoyTests
    {
        [Fact]
        public void Should_return_no_ticket_when_car_to_park_is_null()
        {
            var boy = new Boy();
            Car car = null;
            var lot = new Lot();
            boy.Lots = new Lot[] { lot };

            string message;
            var ticket = boy.Park(car, out message);

            Assert.Null(ticket);
        }

        [Fact]
        public void Should_return_no_ticket_when_car_to_park_is_already_parked()
        {
            var boy = new Boy();
            Car car = new Car("123");
            var lot = new Lot();
            boy.Lots = new Lot[] { lot };
            string message;
            var ticket = boy.Park(car, out message);

            var tryTicket = boy.Park(car, out message);

            Assert.Null(tryTicket);
        }

        [Fact]
        public void Should_return_no_car_when_ticket_is_wrong_with_boyId()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string message;
            var correctTicket = boy.Park(car, out message);
            var wrongTicket = new Ticket(car.GetLicenseNumber(), lot.GetLocation(), boy.Id + 1);

            var fetchedCarWithWrongTicket = boy.Fetch(wrongTicket, out message);
            var fetchedCarWithCorrectTicket = boy.Fetch(correctTicket, out message);

            Assert.Null(fetchedCarWithWrongTicket);
            Assert.Equal(car, fetchedCarWithCorrectTicket);
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_not_provided_for_fetching_car()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };

            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);
            var fetchedCar = boy.Fetch(null, out errorMessage);

            Assert.Equal("Please provide your parking ticket.", errorMessage);
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_wrong_with_boyId_for_fetching_car()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);

            var wrongTicket = new Ticket(car.GetLicenseNumber(), lot.GetLocation(), boy.Id + 1);
            var fetchedCar = boy.Fetch(wrongTicket, out errorMessage);

            Assert.Equal("Unrecognized parking ticket.", errorMessage);
        }

        [Fact]
        public void Should_return_error_message_when_ticket_is_already_used_for_fetching_car()
        {
            var boy = new Boy();
            var lot = new Lot();
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };

            string errorMessage;
            var ticket = boy.Park(car, out errorMessage);

            var fetchedCar = boy.Fetch(ticket, out errorMessage);
            var newFetched = boy.Fetch(ticket, out errorMessage);

            Assert.Equal("Unrecognized parking ticket.", errorMessage);
        }

        [Fact]
        public void Should_return_error_message_when_parking_lot_is_full_for_parking_car()
        {
            var boy = new Boy();
            var lot = new Lot(capacity: 1);
            var car = new Car("123");
            boy.Lots = new Lot[] { lot };
            string errorMessage;
            boy.Park(car, out errorMessage);

            var car2 = new Car("456");
            var ticket = boy.Park(car2, out errorMessage);

            Assert.Equal("Not enough position.", errorMessage);
        }

        [Fact]
        public void Should_only_park_car_to_new_lot_when_previous_lot_is_full()
        {
            var boy = new Boy();
            var lot1 = new Lot("loca1", 5);
            var lot2 = new Lot("loca2", 5);
            boy.Lots = new[] { lot1, lot2 };

            string errorMessage;
            var licenses = new List<string>();
            foreach (var car in TestData.GetCars(8))
            {
                var ticket = boy.Park(car, out errorMessage);
                licenses.Add(car.GetLicenseNumber());
                Assert.Empty(errorMessage);
            }

            Assert.True(
                licenses.Where(license => licenses.IndexOf(license) < lot1.Capacity)
                    .All(license => lot1.HaveCar(license)));

            Assert.True(
                licenses.Where(license => licenses.IndexOf(license) > lot1.Capacity)
                    .All(license => lot2.HaveCar(license)));
        }

        [Fact]
        public void Should_return_error_message_when_all_lots_managed_are_full_for_parking_new_car()
        {
            var boy = new Boy();
            var lot1 = new Lot("loca1", 1);
            var lot2 = new Lot("loca2", 2);
            boy.Lots = new[] { lot1, lot2 };

            string errorMessage;
            var messages = new List<string>();
            foreach (var car in TestData.GetCars(lot1.Capacity + lot2.Capacity + 1))
            {
                var ticket = boy.Park(car, out errorMessage);
                messages.Add(errorMessage);
            }

            Assert.True(messages.Where(message => messages.IndexOf(message) < (lot1.Capacity + lot2.Capacity))
                .All(message => string.IsNullOrEmpty(message)));

            Assert.Equal("Not enough position.", messages.Last());
        }
    }
}
