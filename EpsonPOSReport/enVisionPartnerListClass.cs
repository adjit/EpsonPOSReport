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

    public enum priceLevels
    {
        Select = 2,
        Plus = 3,
        Premier = 3,
        eFi = 4,
        mSelect
    }

    class enVisionPartnerList
    {
        public List<enVisionCustomer> customers { get; }

        public void addCustomer(int priceLevelIndex, string enVisionLevel, string customer,
                                string enVisionNumber, string priceLevel)
        {
            customers.Add(new enVisionCustomer(priceLevelIndex, enVisionLevel, customer,
                                                enVisionNumber, priceLevel));
        }

        public enVisionCustomer getCustomer(string identifier, bool isEnVisionNumber)
        {
            enVisionCustomer customer = null;

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

            return customer;
        }
    }

    class enVisionCustomer
    {
        public int priceLevelIndex { get; }
        public string enVisionLevel { get; }
        public string customer { get; }
        public string enVisionNumber { get; }
        public string priceLevel { get; }

        public enVisionCustomer(int priceLevelIndex, string enVisionLevel, string customer,
                                string enVisionNumber, string priceLevel)
        {
            this.priceLevelIndex = priceLevelIndex;
            this.enVisionLevel = enVisionLevel;
            this.customer = customer;
            this.enVisionNumber = enVisionNumber;
            this.priceLevel = priceLevel;
        }
    }
}
