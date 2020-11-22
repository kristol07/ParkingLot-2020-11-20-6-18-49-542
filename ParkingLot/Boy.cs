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
            if (car == null || Lots.Any(lot => lot.HaveCar(car)))
            {
                responseMessage = "Please provide valid car.";
                return null;
            }

            var lot = Lots.FirstOrDefault(lot => lot.HasPosition);
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

        public virtual Car Fetch(Ticket ticket, out string responseMessage)
        {
            if (ticket == null)
            {
                responseMessage = "Please provide your parking ticket.";
                return null;
            }

            var lot = Lots.FirstOrDefault(lot => lot.GetLocation() == ticket.GetLotLocation());
            if (ticket.GetBoyId() != id || ticket.IsUsed || lot == null)
            {
                responseMessage = "Unrecognized parking ticket.";
                return null;
            }
            else
            {
                responseMessage = string.Empty;
                var car = lot.ReturnCar(ticket, this);
                return car;
            }
        }

        public int GetId()
        {
            return id;
        }
    }
}
