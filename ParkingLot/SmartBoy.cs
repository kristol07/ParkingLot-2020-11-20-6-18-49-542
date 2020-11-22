using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class SmartBoy : Boy
    {
        public override Ticket Park(Car car, out string responseMessage)
        {
            var lot = Lots.Where(lot => lot.HasPosition)
                .OrderBy(lot => lot.LeftPosition)
                .LastOrDefault();
            if (lot != null)
            {
                responseMessage = string.Empty;
                var ticket = lot.ParkCar(car, this);
                return ticket;
            }
            else
            {
                responseMessage = "Not enough position.";
                return null;
            }
        }
    }

    public class SuperSmartBoy : Boy
    {
        public override Ticket Park(Car car, out string responseMessage)
        {
            var lot = Lots.Where(lot => lot.HasPosition)
                .OrderBy(lot => lot.AvailablePositionRate)
                .LastOrDefault();
            if (lot != null)
            {
                responseMessage = string.Empty;
                var ticket = lot.ParkCar(car, this);
                return ticket;
            }
            else
            {
                responseMessage = "Not enough position.";
                return null;
            }
        }
    }
}
