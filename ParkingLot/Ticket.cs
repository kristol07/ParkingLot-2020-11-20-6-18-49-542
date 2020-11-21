using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Ticket
    {
        private readonly string licenseNumber;
        private readonly string lotLocation;
        public Ticket(string licenseNumber, string lotLocation)
        {
            this.licenseNumber = licenseNumber;
            this.lotLocation = lotLocation;
        }

        public string GetLicenseNumber()
        {
            return licenseNumber;
        }

        public string GetLotLocation()
        {
            return lotLocation;
        }
    }
}
