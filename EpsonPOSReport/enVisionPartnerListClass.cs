using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EpsonPOSReport
{
    /*  Will not be using this enum
     *  To be deleted at program completion.
     * */
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

    public enum PriceLevelIndex
    {
        SELECT      = 1,    //2
        PLUS        = 2,    //3
        PREMIER     = 3,    //3
        MSELECT     = 4,    //5
    }


    /*  Not going to use the PriceLevels class. This would have been
     *  used to correlate the customer price level to the price level
     *  as dictated by GP. However, now we are now instead using 
     * 
     * */
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

        public enVisionPartnerList()
        {
            customers = new List<enVisionCustomer>();
        }

        public void addCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, PriceLevelIndex priceLevel)
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

        /*  This method is called by the ThisAddIn class, and is passed
         *  progress and taskProgress to update the progress bar UI and
         *  show the user that something is actually being worked on.
         * */
        public void runPartnerListInitialization(IProgress<int> progress, IProgress<string> taskProgress)
        {
            taskProgress.Report("Price List Initialization has begun...");
            initializePartnerList(Properties.Settings.Default._filePath_priceList);
            taskProgress.Report("Price List Initialization finished.");
            progress.Report(33);
        }

        /*  This method is a standalone initialization method for the enVision
         *  partner list class which would take in a filename and
         * 
         * */
        public bool initializePartnerList(string filePath)
        {
            int PARTNER_LIST_SHEET = 1;
            int START_ROW = 3;
            int CUSTOMER_NAME = 5;
            int CUSTOMER_NUMBER = 6;
            int PRICE_LEVEL = 9;

            bool _partnersAdded = false;

            Excel.Workbook partnerListWorkbook;
            Excel.Worksheet pLS;

            string priceLevel = "";
            string customer = "";
            string enVisionNumber = "";
            PriceLevelIndex priceIndex = 0;

            enVisionPartnerList epl = new enVisionPartnerList();

            try
            {
                partnerListWorkbook = Globals.ThisAddIn.Application.Workbooks.Open(filePath, false, true);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error opening Partner List\n" + e, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }

            pLS = partnerListWorkbook.Worksheets[PARTNER_LIST_SHEET];

            for(int i = START_ROW; i < pLS.UsedRange.Rows.Count; i++)
            {
                customer = Convert.ToString(pLS.Cells[i, CUSTOMER_NAME]);
                enVisionNumber = Convert.ToString(pLS.Cells[i, CUSTOMER_NUMBER]);

                priceLevel = Convert.ToString(pLS.Cells[i, PRICE_LEVEL]);
                priceLevel = priceLevel.ToLower();

                if(priceLevel.Contains(PriceLevelIndex.SELECT.ToString().ToLower()))
                {
                    priceIndex = PriceLevelIndex.SELECT;
                }
                else if (priceLevel.Contains(PriceLevelIndex.PLUS.ToString().ToLower()))
                {
                    priceIndex = PriceLevelIndex.PLUS;
                }
                else if (priceLevel.Contains(PriceLevelIndex.PREMIER.ToString().ToLower()))
                {
                    priceIndex = PriceLevelIndex.PREMIER;
                }
                else if (priceLevel.Contains(PriceLevelIndex.MSELECT.ToString().ToLower()))
                {
                    priceIndex = PriceLevelIndex.MSELECT;
                }

                epl.addCustomer(priceLevel, customer, enVisionNumber, priceIndex);
                _partnersAdded = true;
            }

            return _partnersAdded;
        }
    }

    class enVisionCustomer
    {
        public string enVisionLevel { get; }
        public string customer { get; }
        public string enVisionNumber { get; }
        public PriceLevelIndex priceLevelIndex { get; }

        public enVisionCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, PriceLevelIndex priceLevelIndex)
        {
            this.enVisionLevel = enVisionLevel;
            this.customer = customer;
            this.enVisionNumber = enVisionNumber;
            this.priceLevelIndex= priceLevelIndex;
        }
    }
}
