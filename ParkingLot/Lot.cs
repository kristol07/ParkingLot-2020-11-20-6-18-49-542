using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class Lot
    {
        private readonly string location;
        private int capacity;
        private Dictionary<int, List<Car>> inventory;

        public Lot(string location = "default location", int capacity = 10)
        {
            this.location = location;
            this.capacity = capacity;
            this.inventory = new Dictionary<int, List<Car>>();
        }

        public int LeftPosition
        {
            get
            {
                return capacity - Cars.Count;
            }
        }

        public List<Car> Cars
        {
            get
            {
                return inventory.SelectMany(boyCars => boyCars.Value).ToList();
            }
        }

        public bool HasPosition => LeftPosition > 0;

        public string GetLocation()
        {
            return location;
        }

        public Ticket ParkCar(Car car, Boy boy)
        {
            List<Car> cars;
            if (!inventory.TryGetValue(boy.GetId(), out cars))
            {
                cars = new List<Car>();
                inventory.Add(boy.GetId(), cars);
            }

            cars.Add(car);

            return new Ticket(car.GetLicenseNumber(), location, boy.GetId());
        }

        public Car ReturnCar(Ticket ticket, Boy boy)
        {
            if (ticket.GetBoyId() != boy.GetId())
            {
                return null;
            }

            List<Car> cars;
            if (!inventory.TryGetValue(boy.GetId(), out cars))
            {
                inventory.Add(boy.GetId(), new List<Car>());
            }

            var car = cars.Find(car => car.GetLicenseNumber() == ticket.GetLicenseNumber());
            cars.Remove(car);
            return car;
        }

        public bool HaveCar(Car car)
        {
            return Cars.Contains(car);
        }
    }
}
