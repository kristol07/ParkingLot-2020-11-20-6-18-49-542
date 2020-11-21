using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Lot
    {
        private readonly string location;
        private int capacity;

        public Lot(string location, int capacity = 10)
        {
            this.location = location;
            this.capacity = capacity;
        }

        public bool HasPosition => capacity > 0;

        public string GetLocation()
        {
            return location;
        }
    }
}
