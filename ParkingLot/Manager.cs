﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class Manager
    {
        public List<Boy> Boys { get; set; } = new List<Boy>();

        public Ticket Park(Car car, out string message)
        {
            var random = new Random();
            var selectedBoy = Boys.OrderBy(boy => random.Next())
                .FirstOrDefault();

            var ticket = selectedBoy.Park(car, out message);
            return ticket;
        }

        public Car Fetch(Ticket ticket, out string message)
        {
            if (ticket == null)
            {
                message = "Please provide your parking ticket.";
                return null;
            }

            var boy = Boys.Find(boy => boy.Id == ticket.GetBoyId());
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
    }
}
