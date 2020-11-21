using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Boy
    {
        public Ticket Park(Car car, Lot lot)
        {
            var ticket = lot.ParkCar(car);
            return ticket;
        }

        public Car Fetch(Ticket ticket, Lot lot)
        {
            var car = lot.ReturnCar(ticket);
            return car;
        }
    }
}
