using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Ticket
    {
        private readonly string licenseNumber;
        private readonly string lotLocation;
        private readonly int boyId;
        public Ticket(string licenseNumber, string lotLocation, int boyId)
        {
            this.licenseNumber = licenseNumber;
            this.lotLocation = lotLocation;
            this.boyId = boyId;
            this.IsUsed = false;
        }

        public bool IsUsed
        {
            get; set;
        }

        public string GetLicenseNumber()
        {
            return licenseNumber;
        }

        public string GetLotLocation()
        {
            return lotLocation;
        }

        public int GetBoyId()
        {
            return boyId;
        }
    }
}
