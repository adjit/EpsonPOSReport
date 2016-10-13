using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsonPOSReport
{
    public enum enVisionLevels
    {
        COLORWORKS_OEM,
        COLORWORKS_PLUS,
        COLORWORKS_PREMIER,
        DISCPRODUCER,
        M_SELECT_ISV,
        PLUS_OEM,
        PLUS_VAR,
        PREMIER_OEM,
        PREMIER_VAR,
        SELECT_VAR,
        SOFTWARE_SOLUTION,
        TECH
    }

    public enum priceLevelIndex
    {
        SELECT = 0,     //2
        PLUS = 1,       //3
        PREMIER = 2,    //3
        EFI = 3,        //4
        MSELECT = 4,    //5
        SPECIAL = 5     //6
    }

    class PriceLevels
    {
        public int[] priceLevels { get; }
        private readonly int[] defaultPriceLevels = { 2, 3, 3, 4, 5, 6 };

        /*public PriceLevels(int[] newPriceLevels = defaultPriceLevels)
        {
            priceLevels = newPriceLevels;
        }*/

        public PriceLevels()
        {
            priceLevels = defaultPriceLevels;
        }
        
        public PriceLevels(int[] newPriceLevels)
        {
            priceLevels = newPriceLevels;
        }
        /*Decided not to use the PriceLevel class for each customer
         * Instead - going to keep it simple, and in the main program
         * contain an integer array in which the priceLevel enum
         * will be responsible for the proper indecies
         * 
         * public override string ToString()
        {
            string myString = "No Level Set";

            if (priceLevelIndex == (int)priceLevel.SELECT) return "Partner-Select";
            if (priceLevelIndex == (int)priceLevel.PLUS) return "Plus/Premier";
            if (priceLevelIndex == (int)priceLevel.PREMIER) return "Plus/Premier";
            if (priceLevelIndex == (int)priceLevel.EFI) return "eFi Plus-SPA";
            if (priceLevelIndex == (int)priceLevel.MSELECT) return "mSelect";
            if (priceLevelIndex == (int)priceLevel.SPECIAL) return "Special";

            return myString;
        }*/
    }

    class enVisionPartnerList
    {
        public List<enVisionCustomer> customers { get; }

        public void addCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, int priceLevel)
        {
            customers.Add(new enVisionCustomer(enVisionLevel, customer,
                                                enVisionNumber, priceLevel));
        }

        public enVisionCustomer getCustomer(string identifier, bool isEnVisionNumber)
        {
            if(isEnVisionNumber)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].enVisionNumber == identifier) return customers[i];
                }
            }
            else
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].customer == identifier) return customers[i];
                }
            }

            return null;
        }
    }

    class enVisionCustomer
    {
        public string enVisionLevel { get; }
        public string customer { get; }
        public string enVisionNumber { get; }
        public int priceLevelIndex { get; }

        public enVisionCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, int priceLevelIndex)
        {
            this.enVisionLevel = enVisionLevel;
            this.customer = customer;
            this.enVisionNumber = enVisionNumber;
            this.priceLevelIndex= priceLevelIndex;
        }
    }
}
