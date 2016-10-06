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
    class spaList
    {
        private List<customerItemFulfillment> customers;

        public customerItemFulfillment addNewCustomer(string customerNumber, string customerName)
        {
            customers.Add(new customerItemFulfillment(customerNumber, customerName));
            return customers[customers.Count - 1];
        }

        public customerItemFulfillment getCustomerFulfillment(string customerNumber)
        {
            for(int i = 0; i < customers.Count; i++)
            {
                if (customers[i].customerNumber == customerNumber) return customers[i];
            }
            return null;
        }

        public bool initializeSpaListings(string filePath)
        {
            int SPA_LIST_SHEET = 1;
            int ENVISION_NUMBER = 1;
            int CUSTOMER_NUMBER = 2;
            int CUSTOMER_NAME = 3;
            int PART_NUMBER = 4;
            int REBATE = 5;
            int FULFILLMENT = 6;

            Excel.Workbook spaListWorkbook;
            Excel.Worksheet spaWorksheet;
            Excel.Range range;

            bool flag = false;

            int rows,
                columns,
                thisRow,
                thisColumn;

            try
            {
                spaListWorkbook = Globals.ThisAddIn.Application.Workbooks.Open(filePath, false, true);
                flag = true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error opening Spa List\n" + e, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            spaWorksheet = spaListWorkbook.Worksheets[SPA_LIST_SHEET];
            range = spaWorksheet.UsedRange;
            rows = range.Rows.Count;
            columns = range.Columns.Count;
            thisRow = thisColumn = 1;

            Excel.ListObject spaTable = null;
            Excel.Range spaDataRange = null;
            bool spaTableExists = false;

            for (int i = 0; i < spaWorksheet.ListObjects.Count; i++)
            {
                if (spaWorksheet.ListObjects[i].DisplayName == "EPSON_SPA_LIST")
                {
                    spaTable = spaWorksheet.ListObjects[i];
                    spaDataRange = spaTable.DataBodyRange;
                    spaTableExists = true;
                    break;
                }
            }

            if (!spaTableExists)
            {
                System.Windows.Forms.MessageBox.Show("Could not find EPSON_SPA_LIST table. Please check spreadsheet and table name.", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            int rowCount = spaDataRange.Rows.Count;
            int colCount = spaDataRange.Columns.Count;

            for(int i = rowCount; i >= 0; i--)
            {
                spaDataRange.Item[i] = 1;
            }

            return flag;
        }

    }

    public class customerItemFulfillment
    {
        public string customerNumber { get; }
        public string customerName { get; }
        public List<itemFulfillment> fulfillmentItems { get; }

        public void addItem(string itemNumber, double rebateAmount, double fulfillmentPercent)
        {
            fulfillmentItems.Add(new itemFulfillment(itemNumber, rebateAmount, fulfillmentPercent));
        }

        public itemFulfillment getItemFulfillment(string itemNumber)
        {
            for(int i = 0; i < fulfillmentItems.Count; i++)
            {
                if (fulfillmentItems[i].item == itemNumber) return fulfillmentItems[i];
            }
            return null;
        }

        public customerItemFulfillment(string customerNumber, string customerName)
        {
            this.customerNumber = customerNumber;
            this.customerName = customerName;
        }
    }

    public class itemFulfillment
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