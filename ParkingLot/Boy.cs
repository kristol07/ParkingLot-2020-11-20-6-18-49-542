using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Boy
    {
        public Ticket Park(Car car, Lot lot)
        {
            return new Ticket(car.GetLicenseNumber(), lot.GetLocation());
        }
    }
}
