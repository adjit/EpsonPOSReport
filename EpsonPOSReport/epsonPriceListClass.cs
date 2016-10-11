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
        public enVisionPriceLevel Select { get; set; }
        public enVisionPriceLevel Plus { get; set; }
        public enVisionPriceLevel Premier { get; set; }
        public enVisionPriceLevel mSelect { get; set; }
        public enVisionPriceLevel eFi { get; set; }
        public enVisionPriceLevel Colorworks { get; set; }
    }

    public class enVisionPriceLevel
    {
        public double rebate { get; set; }
        public double fulfillment { get; set; }
    }
}
