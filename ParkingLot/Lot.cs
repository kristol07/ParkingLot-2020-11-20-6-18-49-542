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

        public bool HasPosition => capacity > 0;

        public string GetLocation()
        {
            return location;
        }

        public Ticket ParkCar(Car car)
        {
            cars.Add(car);
            capacity -= 1;
            return new Ticket(car.GetLicenseNumber(), location);
        }

        public bool HaveCar(Car car)
        {
            return cars.Contains(car);
        }
    }
}
