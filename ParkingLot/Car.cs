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

        public override bool Equals(object? obj)
        {
            return licenseNumber == ((Car)obj).GetLicenseNumber();
        }
    }
}
