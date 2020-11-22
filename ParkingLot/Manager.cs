using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class Manager : Boy
    {
        public Manager(int id = 0) : base(id)
        {
        }

        public List<Boy> Boys { get; set; } = new List<Boy>();

        public Ticket TryPark(Car car, out string message)
        {
            if (car == null
                || Boys.SelectMany(boy => boy.Lots)
                    .Any(lot => lot.HaveCar(car))
                || Lots.Any(lot => lot.HaveCar(car)))
            {
                message = "Please provide valid car.";
                return null;
            }

            var boy = Boys.Concat(new List<Boy>() { this })
                .Where(boy => boy.FindLotWithStrategy() != null).FirstOrDefault();
            if (boy != null)
            {
                return boy.Park(car, out message);
            }
            else
            {
                message = "Not enough position.";
                return null;
            }
        }

        public Ticket AssignPark(Car car, Boy boy, out string message)
        {
            if (!Boys.Contains(boy))
            {
                message = "Boy can not be managed.";
                return null;
            }

            var ticket = boy.Park(car, out message);
            return ticket;
        }

        public Car TryFetch(Ticket ticket, out string message)
        {
            if (ticket == null)
            {
                message = "Please provide your parking ticket.";
                return null;
            }

            var boy = Boys.Concat(new List<Boy>() { this })
                .FirstOrDefault(boy => boy.Id == ticket.GetBoyId());
            if (boy != null)
            {
                return boy.Fetch(ticket, out message);
            }
            else
            {
                message = "Unrecognized parking ticket.";
                return null;
            }
        }

        public Car AssignFetch(Ticket ticket, Boy boy, out string message)
        {
            if (!Boys.Contains(boy))
            {
                message = "Boy can not be managed.";
                return null;
            }

            return boy.Fetch(ticket, out message);
        }
    }
}
