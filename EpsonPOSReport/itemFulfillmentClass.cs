using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsonPOSReport
{
    class itemFulfillment
    {
        public string item { get; }
        public double rebate { get; }
        public double fulfillment { get; }

        public itemFulfillment(string itemNumber, double rebateAmount, double fulfillmentPercent)
        {
            item = itemNumber;
            rebate = rebateAmount;
            fulfillment = fulfillmentPercent;
        }
    }
}
