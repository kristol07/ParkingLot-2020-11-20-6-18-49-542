using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Lot
    {
        private readonly string location;
        private int capacity;
        private List<Car> cars;

        public Lot(string location = "test location", int capacity = 10)
        {
            this.location = location;
            this.capacity = capacity;
            this.cars = new List<Car>();
        }

        public int LeftPosition
        {
            get
            {
                return capacity - cars.Count;
            }
        }

        public bool HasPosition => LeftPosition > 0;

        public string GetLocation()
        {
            return location;
        }

        public Ticket ParkCar(Car car)
        {
            cars.Add(car);
            return new Ticket(car.GetLicenseNumber(), location);
        }

        public Car ReturnCar(Ticket ticket)
        {
            var car = cars.Find(car => car.GetLicenseNumber() == ticket.GetLicenseNumber());
            cars.Remove(car);
            return car;
        }

        public bool HaveCar(Car car)
        {
            return cars.Contains(car);
        }
    }
}
