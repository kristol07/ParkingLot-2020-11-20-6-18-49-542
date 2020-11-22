using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class SmartBoy : Boy
    {
        public SmartBoy(int id = 0) : base(id)
        {
        }

        public override Lot FindLotWithStrategy()
        {
            return Lots.Where(lot => lot.HasPosition)
                .OrderBy(lot => lot.LeftPosition)
                .LastOrDefault();
        }
    }

    public class SuperSmartBoy : Boy
    {
        public SuperSmartBoy(int id = 0) : base(id)
        {
        }

        public override Lot FindLotWithStrategy()
        {
            return Lots.Where(lot => lot.HasPosition)
                .OrderBy(lot => lot.AvailablePositionRate)
                .LastOrDefault();
        }
    }
}
