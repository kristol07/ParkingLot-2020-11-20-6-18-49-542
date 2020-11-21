using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Boy
    {
        private readonly int id;

        public Boy(int id = 0)
        {
            this.id = id;
        }

        public Ticket Park(Car car, Lot lot)
        {
            if (car == null)
            {
                return null;
            }

            var ticket = lot.ParkCar(car, this);
            return ticket;
        }

        public Car Fetch(Ticket ticket, Lot lot)
        {
            if (ticket == null || ticket.IsUsed)
            {
                return null;
            }

            var car = lot.ReturnCar(ticket, this);
            return car;
        }

        public int GetId()
        {
            return id;
        }
    }
}
