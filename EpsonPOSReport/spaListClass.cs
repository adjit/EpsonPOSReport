using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EpsonPOSReport
{
    /*
     * 
     * */
    class SpaList
    {
        /*  Private element of the SpaList class which contains a list
         *  of spaItemFulfillment items. Each item is a spa which 
         *  contains a list of customers who the spa is applicable to
         *  and a list of items that are applicable to this spa
         * */
        private List<spaItemFulfillment> spaItems = new List<spaItemFulfillment>();
        private bool isInitialized = false;

        /*  Private function to add a spa item to
         *  the spaItems list.
         * */
        private void addNewSpaListItem(List<string> customers, List<itemFulfillment> items)
        {
            spaItems.Add(new spaItemFulfillment(customers, items));
        }

        /*  Public function that will be used to get spa items for a customer 
         * */
        public List<itemFulfillment> getCustomerSpaItems(string customerNumber)
        {
            if(!isInitialized)
            {
                System.Windows.Forms.MessageBox.Show("Please initialize SpaList class before trying to use", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            List<int> spaIndecies = new List<int>();
            List<itemFulfillment> customerSpaItems = new List<itemFulfillment>();
            bool hasSpa = false;

            for (int i = 0; i < spaItems.Count; i++)
            {
                if (spaItems[i].hasCustomer(customerNumber))
                {
                    hasSpa = true;
                    customerSpaItems.AddRange(spaItems[i].getItems());
                }
            }

            if (hasSpa) return customerSpaItems;
            else return null;
        }

        public void runSpaInitialization(IProgress<int> progress, IProgress<string> taskProgress)
        {
            taskProgress.Report("Spa List Initialization has begun...");
            initializeSpaList(Properties.Settings.Default._SpaListFilePath);
            taskProgress.Report("Spa List Initilaztion finished.");
            progress.Report(33);
        }

        /*  public function that will be used to initialize
         *  spa list workbook which can be found at the 
         *  location of the filepath
         * */
        public bool initializeSpaList(string filePath)
        {
            int SPA_CUSTOMER_SHEET = 1;
            int SPA_ITEMS_SHEET = 2;
            int SPA_TABLE_PREFIX = 0;
            int SPA_TABLE_NAME = 1;

            bool _spaItemsAdded = false;

            Excel.Workbook spaListWorkbook;
            Excel.Worksheet spaCustomerSheet;
            Excel.Worksheet spaItemsSheet;

            try
            {
                spaListWorkbook = Globals.ThisAddIn.Application.Workbooks.Open(filePath, false, true);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error opening Spa List\n" + e, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }

            spaCustomerSheet = spaListWorkbook.Worksheets[SPA_CUSTOMER_SHEET];
            spaItemsSheet = spaListWorkbook.Worksheets[SPA_ITEMS_SHEET];

            /*  For loop looping through the ListObjects of the customer sheet
             *  which contain the corresponding table names and customers who
             *  are applicable for those spa part numbers
             * */
            for (int i = 0; i < spaCustomerSheet.ListObjects.Count; i++)
            {
                string[] tableName = spaCustomerSheet.ListObjects[i].Name.Split('_');

                if (tableName[SPA_TABLE_PREFIX] == "spaCustomers")
                {
                    for (int j = 0; j < spaItemsSheet.ListObjects.Count; i++)
                    {
                        if (spaItemsSheet.ListObjects[j].Name == tableName[SPA_TABLE_NAME])
                        {
                            Excel.ListRows customerRows = spaCustomerSheet.ListObjects[i].ListRows;
                            Excel.ListRows itemsRows = spaItemsSheet.ListObjects[j].ListRows;

                            List<string> customers = new List<string>();
                            List<itemFulfillment> items = new List<itemFulfillment>();

                            /*  For loop to iterate through the customerRows ListRows.
                             *  Each row contains a single customer number and the
                             *  rows should only be 1 column wide
                             * */
                            for (int k = 0; k < customerRows.Count; k++)
                            {
                                /*  This works because we know that the customer number table
                                 *  will only be one column wide. Therefore the range of 1 row
                                 *  is only 1 cell, which we get the value from - the customer
                                 *  number
                                 * */
                                customers.Add((string)customerRows.Item[k].Range.Value2());
                            }

                            /*  For loop iterating through the items Rows which should be
                             *  3 columns wide. If we need to increase the information input
                             *  it should be easy to scale. Inside this for loop, iterating
                             *  through each row, and then iterating through all of the 
                             *  cells (3) in each row, and getting the proper values.  
                             * */
                            for (int k = 0; k < itemsRows.Count; k++)
                            {
                                Excel.Range cols = itemsRows.Item[k].Range.Columns;
                                string itemNumber = "";
                                double rebateAmount = 0.00;
                                double fulfillmentPercent = 0.00;
                                int counter = 0;
                                bool allValuesSet = false;

                                foreach (Excel.Range cell in cols.Cells)
                                {
                                    switch (counter)
                                    {
                                        case 0:
                                            {
                                                itemNumber = (string)cell.Value2;
                                                counter++;
                                                break;
                                            }
                                        case 1:
                                            {
                                                rebateAmount = (double)cell.Value2;
                                                counter++;
                                                break;
                                            }
                                        case 2:
                                            {
                                                fulfillmentPercent = (double)cell.Value2;
                                                allValuesSet = true;
                                                counter++;
                                                break;
                                            }
                                        default: break;
                                    }

                                    //Used to break out of the foreach loop.
                                    //If more variables are needed later
                                    //we can easily add cases.
                                    if (allValuesSet) break;
                                }

                                if (allValuesSet)
                                {
                                    items.Add(new itemFulfillment(itemNumber, rebateAmount, fulfillmentPercent));
                                }
                                else continue;
                            }  //End for loop looping through item table rows

                            addNewSpaListItem(customers, items);
                            _spaItemsAdded = true;

                            releaseObject(customerRows);
                            releaseObject(itemsRows);
                        } //End if table names match
                        else
                        {
                            System.Windows.Forms.MessageBox.Show(
                                "Could not find corresponding spa items table with the name: " + tableName[SPA_TABLE_NAME] + "\n\n Skipping...",
                                "Warning",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Warning
                            );
                            continue;
                        }
                    } //End for loop looping through spaItemSheet ListObjects
                } //End if SPA_TABLE_PREFIX = "spaCustomers"
            } //End for loop looping through customerSheet ListObjects containing tables of customer numbers

            spaListWorkbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            releaseObject(spaListWorkbook);
            releaseObject(spaCustomerSheet);
            releaseObject(spaItemsSheet);

            isInitialized = _spaItemsAdded;
            return _spaItemsAdded;
        }

        /*  Private function that is used to release
         *  excel com objects - added for additional
         *  garbage collection
         * */
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                System.Windows.Forms.MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /*  A private class that contains a single spa.
         *  It has a list of customers by customer number
         *  and a list of spa items belonging to each of
         *  all of those customers.
         * */
         private class spaItemFulfillment
        {
            private List<string> spaCustomers;
            private List<itemFulfillment> spaItems;

            public spaItemFulfillment(List<string> customers, List<itemFulfillment> items)
            {
                spaCustomers = customers;
                spaItems = items;
            }

            public bool hasCustomer(string customerNumber)
            {
                for (int i = 0; i < spaCustomers.Count; i++)
                {
                    if (spaCustomers[i] == customerNumber) return true;
                }
                return false;
            }

            public List<itemFulfillment> getItems()
            {
                return spaItems;
            }
        }
    }
}