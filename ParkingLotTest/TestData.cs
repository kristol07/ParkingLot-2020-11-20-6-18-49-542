using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot;

namespace ParkingLotTest
{
    public static class TestData
    {
        public static IEnumerable<Car> GetCars(int number)
        {
            int count = 0;
            while (count++ < number)
            {
                var random = new Random();
                var letters = Enumerable.Range('A', 'Z' - 'A' + 1)
                    .Select(c => ((char)c).ToString());
                var numbers = Enumerable.Range(0, 10).Select(c => c.ToString());
                var randomLicenseNumber = letters.Concat(numbers)
                    .OrderBy(c => random.Next())
                    .Take(4)
                    .Aggregate((a, b) => $"{a}{b}");
                yield return new Car(randomLicenseNumber);
            }
        }
    }
}
