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
            Excel.Workbook spaListWorkbook;
            try
            {
                spaListWorkbook = Globals.ThisAddIn.Application.Workbooks.Open(filePath, false, true);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error opening Spa List\n" + Exception, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return false;
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