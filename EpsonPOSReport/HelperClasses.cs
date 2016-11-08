using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsonPOSReport
{
    /*  ITEM FULFILLMENT CLASS
     *  ----------------------
     *  This class will be used for individual items in a customers spa list
     */
    class itemFulfillment
    {
        public string item { get; }
        public double rebate { get; }
        public double fulfillment { get; }
        public double unitCost { get; }

        public itemFulfillment(string itemNumber, double rebateAmount, double fulfillmentPercent, double cost)
        {
            item = itemNumber;
            rebate = rebateAmount;
            fulfillment = fulfillmentPercent;
            unitCost = cost;
        }
    }

    /*  ITEM CLASS
     *  ----------------------
     *  This class will be used for individual items in the Price List
     */
    class Item
    {
        public string cCode { get; set; }
        public double Cost { get; set; }
        public PriceLevel Select { get; set; } = new PriceLevel();
        public PriceLevel Plus { get; set; } = new PriceLevel();
        public PriceLevel Premier { get; set; } = new PriceLevel();
        public PriceLevel mSelect { get; set; } = new PriceLevel();
    }

    /*  PRICE LEVEL CLASS
     *  ----------------------
     *  This class will be used for the Price Levels of a specific item
     */
    class PriceLevel
    {
        public double rebate { get; set; }
        public double fulfillment { get; set; }
    }

    /*  ENVISION CUSTOMER CLASS
     *  ------------------------
     *  This class will be used for enVision Customers on the Partner List
     */
    class enVisionCustomer
    {
        public string enVisionLevel { get; }
        public string customer { get; }
        public string enVisionNumber { get; }
        public PriceLevelIndex priceLevelIndex { get; } = new PriceLevelIndex();
        public bool isColorworks { get; } = false;

        public enVisionCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, PriceLevelIndex priceLevelIndex)
        {
            this.enVisionLevel = enVisionLevel;
            this.customer = customer;
            this.enVisionNumber = enVisionNumber;
            this.priceLevelIndex = priceLevelIndex;
        }

        public enVisionCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, PriceLevelIndex priceLevelIndex, bool isColorworks)
        {
            this.enVisionLevel = enVisionLevel;
            this.customer = customer;
            this.enVisionNumber = enVisionNumber;
            this.priceLevelIndex = priceLevelIndex;
            this.isColorworks = isColorworks;
        }
    }
}
