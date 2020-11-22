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
