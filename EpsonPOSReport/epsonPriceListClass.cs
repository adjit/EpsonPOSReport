using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsonPOSReport
{
    class epsonPriceList
    {
        public List<item> items { get; }

        public void addItem(item newItem)
        {
            items.Add(newItem);
        }

        public item getItem(string cCode)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].cCode == cCode) return items[i];
            }
            return null;
        }
    }

    public class item
    {
        public string cCode { get; set; }
        public double Cost { get; set; }
        public priceLevel Select { get; set; }
        public priceLevel Plus { get; set; }
        public priceLevel Premier { get; set; }
        public priceLevel mSelect { get; set; }
        public priceLevel eFi { get; set; }
        public priceLevel Colorworks { get; set; }
    }

    public class priceLevel
    {
        public double rebate { get; set; }
        public double fulfillment { get; set; }
    }
}
