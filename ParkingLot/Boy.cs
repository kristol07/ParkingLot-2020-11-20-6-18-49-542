using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public Lot[] Lots { get; set; }

        public virtual Ticket Park(Car car, out string responseMessage)
        {
            var lot = Lots.First(lot => lot.HasPosition);
            if (lot == null)
            {
                responseMessage = "Not enough position.";
                return null;
            }
            else
            {
                responseMessage = string.Empty;
                var ticket = lot.ParkCar(car, this);
                return ticket;
            }
        }

        public Ticket Park(Car car, Lot lot)
        {
            if (car == null || !lot.HasPosition || lot.HaveCar(car))
            {
                return null;
            }

            var ticket = lot.ParkCar(car, this);
            return ticket;
        }

        public Ticket Park(Car car, Lot lot, out string responseMessage)
        {
            if (car == null || lot.HaveCar(car))
            {
                responseMessage = "Please provide valid car.";
                return null;
            }

            if (!lot.HasPosition)
            {
                responseMessage = "Not enough position.";
                return null;
            }

            responseMessage = string.Empty;
            var ticket = lot.ParkCar(car, this);
            return ticket;
        }

        public Car Fetch(Ticket ticket, Lot lot, out string responseMessage)
        {
            if (ticket == null)
            {
                responseMessage = "Please provide your parking ticket.";
                return null;
            }

            if (ticket.GetBoyId() != id
                || ticket.IsUsed)
            {
                responseMessage = "Unrecognized parking ticket.";
                return null;
            }

            responseMessage = string.Empty;
            var car = lot.ReturnCar(ticket, this);
            return car;
        }

        public Car Fetch(Ticket ticket, Lot lot)
        {
            if (ticket == null
                || ticket.GetBoyId() != id
                || ticket.IsUsed)
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
