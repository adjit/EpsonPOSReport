using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EpsonPOSReport
{
    public enum PriceLevelIndex
    {
        SELECT      = 1,    //2
        PLUS        = 2,    //3
        PREMIER     = 3,    //3
        MSELECT     = 4,    //5
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

        public void addCustomer(string enVisionLevel, string customer,
                                string enVisionNumber, PriceLevelIndex priceLevel, bool isColorworks)
        {
            customers.Add(new enVisionCustomer(enVisionLevel, customer,
                                                enVisionNumber, priceLevel, isColorworks));
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
            int PRICE_GROUP = 4;
            int CUSTOMER_NAME = 5;
            int CUSTOMER_NUMBER = 6;
            int PRICE_LEVEL = 9;

            bool _partnersAdded = false;
            bool _isColorworks = false;

            Excel.Workbook partnerListWorkbook;
            Excel.Worksheet pLS;

            string priceLevel = "";
            string customer = "";
            string enVisionNumber = "";
            string priceGroup = "";
            PriceLevelIndex priceIndex = 0;

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
                customer = pLS.Cells[i, CUSTOMER_NAME].Value2.ToString();
                enVisionNumber = pLS.Cells[i, CUSTOMER_NUMBER].Value2.ToString();

                priceLevel = pLS.Cells[i, PRICE_LEVEL].Value2.ToString();
                priceLevel = priceLevel.ToLower();

                priceGroup = pLS.Cells[i, PRICE_GROUP].Value2.ToString();
                priceGroup = priceGroup.ToLower();

                if (priceGroup == "colorworks") _isColorworks = true;

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

                if(_isColorworks) addCustomer(priceLevel, customer, enVisionNumber, priceIndex, _isColorworks);
                else addCustomer(priceLevel, customer, enVisionNumber, priceIndex);
                _partnersAdded = true;
            }

            return _partnersAdded;
        }
    }
}
