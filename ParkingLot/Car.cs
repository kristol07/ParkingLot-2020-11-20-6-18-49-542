using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Car
    {
        private readonly string licenseNumber;
        public Car(string licenseNumber)
        {
            this.licenseNumber = licenseNumber;
        }

        public string GetLicenseNumber()
        {
            return licenseNumber;
        }
    }
}
