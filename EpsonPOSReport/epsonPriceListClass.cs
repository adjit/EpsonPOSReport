using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EpsonPOSReport
{
    class epsonPriceList
    {
        public List<Item> items { get; }

        public epsonPriceList()
        {
            items = new List<Item>();
        }

        public void addItem(Item newItem)
        {
            items.Add(newItem);
        }

        public Item getItem(string cCode)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].cCode == cCode) return items[i];
            }
            return null;
        }

        public void runEpsonPriceListInitialization(IProgress<int> progress, IProgress<string> taskProgress)
        {
            taskProgress.Report("Price List Initialization has begun...");
            initializeEpsonPriceList(Properties.Settings.Default._filePath_priceList);
            taskProgress.Report("Price List Initialization finished.");
            progress.Report(33);
        }

        public bool initializeEpsonPriceList(string filePath)
        {
            bool _hasItems = false;

            int PRICE_LIST_SHEET = 1;
            int START_ROW = 3;

            Properties.Settings us = new Properties.Settings();

            Excel.Workbook priceListWorkbook;
            Excel.Worksheet pLWS;

            try
            {
                priceListWorkbook = Globals.ThisAddIn.Application.Workbooks.Open(filePath, false, true);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error opening Price List\n" + e, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return false;
            }

            pLWS = priceListWorkbook.Worksheets[PRICE_LIST_SHEET];

            for(int i = START_ROW; i < pLWS.UsedRange.Rows.Count; i++)
            {
                Excel.Range itemNumberCell = pLWS.Cells[i, us._plColumn_itemNumber];

                if(itemNumberCell.Font.Underline != (int)Excel.XlUnderlineStyle.xlUnderlineStyleNone ||
                    //itemNumberCell.Font.Strikethrough ||
                    itemNumberCell.Value2 == null /*||
                    string.IsNullOrEmpty((string)itemNumberCell.Value2)*/)
                {
                    continue;
                }

                Item item = new Item();

                item.cCode = /*string.IsNullOrEmpty(itemNumberCell.Value2) ? "" :*/ Convert.ToString(itemNumberCell.Value2);
                item.Cost = (pLWS.Cells[i, us._plColumn_unitCost].Value2 == null ? 0 : getNumber(pLWS.Cells[i, us._plColumn_unitCost].Value2));
                item.Select.fulfillment = (pLWS.Cells[i, us._plColumn_selectFFP].Value2 == null ? 0 : getNumber(pLWS.Cells[i, us._plColumn_selectFFP].Value2));
                item.Select.rebate = (pLWS.Cells[i, us._plColumn_selectRebate].Value2 == null ? 0 : -getNumber(pLWS.Cells[i, us._plColumn_selectRebate].Value2));
                item.Plus.fulfillment = (pLWS.Cells[i, us._plColumn_plusFFP].Value2 == null ? 0 : getNumber(pLWS.Cells[i, us._plColumn_plusFFP].Value2));
                item.Plus.rebate = (pLWS.Cells[i, us._plColumn_plusRebate].Value2 == null ? 0 : -getNumber(pLWS.Cells[i, us._plColumn_plusRebate].Value2));
                item.Premier.fulfillment = (pLWS.Cells[i, us._plColumn_premierFFP].Value2 == null ? 0 : getNumber(pLWS.Cells[i, us._plColumn_premierFFP].Value2));
                item.Premier.rebate = (pLWS.Cells[i, us._plColumn_premierRebate].Value2 == null ? 0 : -getNumber(pLWS.Cells[i, us._plColumn_premierRebate].Value2));
                item.mSelect.fulfillment = (pLWS.Cells[i, us._plColumn_mSelectFFP].Value2 == null ? 0 : getNumber(pLWS.Cells[i, us._plColumn_mSelectFFP].Value2));
                item.mSelect.rebate = (pLWS.Cells[i, us._plColumn_mSelectRebate].Value2 == null ? 0 : -getNumber(pLWS.Cells[i, us._plColumn_mSelectRebate].Value2));

                /*item.Cost = (double)pLWS.Cells[i, us._plColumn_unitCost].Value2;
                item.Select.fulfillment = (double)pLWS.Cells[i, us._plColumn_selectFFP].Value2;
                item.Select.rebate = (double)pLWS.Cells[i, us._plColumn_selectRebate].Value2;
                item.Plus.fulfillment = (double)pLWS.Cells[i, us._plColumn_plusFFP].Value2;
                item.Plus.rebate = (double)pLWS.Cells[i, us._plColumn_plusRebate].Value2;
                item.Premier.fulfillment = (double)pLWS.Cells[i, us._plColumn_premierFFP].Value2;
                item.Premier.rebate = (double)pLWS.Cells[i, us._plColumn_premierRebate].Value2;
                item.mSelect.fulfillment = (double)pLWS.Cells[i, us._plColumn_mSelectFFP].Value2;
                item.mSelect.rebate = (double)pLWS.Cells[i, us._plColumn_mSelectRebate].Value2;*/

                items.Add(item);
                _hasItems = true;
            }

            priceListWorkbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            releaseObject(priceListWorkbook);
            releaseObject(pLWS);
            
            return _hasItems;
        }

        private double getNumber(object value)
        {
            double i = 0.00;
            string v = Convert.ToString(value);
            bool result = double.TryParse(v, out i);
            return i;                  
        }

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
    }
}
