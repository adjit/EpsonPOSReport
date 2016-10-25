using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace EpsonPOSReport
{
    public partial class ThisAddIn
    {
        private PriceLevels pls;

        private bool reportsInitialized = false;

        private SpaList spaList = new SpaList();
        private epsonPriceList priceList = new epsonPriceList();
        private enVisionPartnerList partnerList = new enVisionPartnerList();

        public async void runReportInitialization()
        {
            MyProgressBar ui = new MyProgressBar();
            ui.Show();
            await Task.Factory.StartNew(() => initializeReports(ui));
            ui.Close();
        }

        private void initializeReports(MyProgressBar ui)
        {
            Application.ScreenUpdating = false;

            Task[] taskArray =
            {
                Task.Factory.StartNew(() => spaList.runSpaInitialization(ui.progress, ui.spaListProgress)),
                Task.Factory.StartNew(() => priceList.runEpsonPriceListInitialization(ui.progress, ui.priceListProgress)),
                Task.Factory.StartNew(() => partnerList.runPartnerListInitialization(ui.progress, ui.partnerListProgress))
            };
            Task.WhenAll(taskArray).Wait();
            reportsInitialized = true;
        }

        public void runReport()
        {
            
        }

        public bool initializePriceLevels()
        {
            //check for settings file (need to come up with a settings structure)
            return false;
        }

        private class qRow
        {
            public string cogs { get; }
            public string customerNumber { get; }
            public string enVisionNumber { get; }
            public string customerName { get; }
            public string endUserName { get; }
            public DateTime invoiceDate { get; }
            public string invoiceNumber { get; }
            public string cCode { get; }
            public string itemNumber { get; }
            public string[] serialNumbers { get; }
            public int quantity { get; }
            public string salesRepID { get; }
            public Address billTo { get; set; } = new Address();
            public Address shipTo { get; set; } = new Address();
            public double unitCost { get; }
            public double unitRebate { get; }
            public double fulfillmentPcnt { get; } 

            private bool formattingSet = false;

            public qRow (   object Cogs, object CustNum, object EnvNum, object CustName, object EndName, object Date, object InvNum,
                            object CCode, object ItemNum, object delimittedSerials, object QTY, object SalesRepID, object CustAddress,
                            object CustCity, object CustState, object CustZip, object StAddress, object StCity, object StState, object StZip
                        )
            {
                cogs = Cogs.ToString().Trim();
                customerNumber = CustNum.ToString().Trim();
                enVisionNumber = EnvNum.ToString().Trim();
                customerName = CustName.ToString().Trim();
                endUserName = EndName.ToString().Trim();
                invoiceDate = Convert.ToDateTime(Date);
                invoiceNumber = InvNum.ToString().Trim();
                cCode = CCode.ToString().Trim();
                itemNumber = ItemNum.ToString().Trim();
                serialNumbers = delimittedSerials.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                quantity = Convert.ToInt32(QTY);
                salesRepID = SalesRepID.ToString().Trim();
                billTo.address = CustAddress.ToString().Trim();
                billTo.city = CustCity.ToString().Trim();
                billTo.state = CustState.ToString().Trim();
                billTo.zip = CustZip.ToString().Trim();
                shipTo.address = StAddress.ToString().Trim();
                shipTo.city = StCity.ToString().Trim();
                shipTo.state = StState.ToString().Trim();
                shipTo.zip = StZip.ToString().Trim();

                //getFulfillment();
                //This method will be added later to do my comparisons
            }

            public void parseRow(Excel.Worksheet ws)
            {
                int rn = ws.UsedRange.Row + ws.UsedRange.Rows.Count;

                string currentSerial = "";

                //If there is a mismatch between the number of serials and quantity warn user
                if(serialNumbers.Length > 0 && serialNumbers.Length != quantity)
                {
                    System.Windows.Forms.MessageBox.Show(   "Please note to check row " + rn +"\n Serial Number Count does not equal Quantity",
                                                            "Quantity Warning",
                                                            System.Windows.Forms.MessageBoxButtons.OK,
                                                            System.Windows.Forms.MessageBoxIcon.Warning);
                }

                for(int i = 0; i <= serialNumbers.Length; i++)
                {
                    if (serialNumbers.Length == 0) currentSerial = "";
                    else if (i == serialNumbers.Length) break;
                    else currentSerial = serialNumbers[i];

                    ws.Cells[rn, (int)qCols.ResellerNo].Value2 = enVisionNumber;
                    ws.Cells[rn, (int)qCols.ResellerName].Value2 = customerName;
                    ws.Cells[rn, (int)qCols.EndUserName].Value2 = endUserName;
                    ws.Cells[rn, (int)qCols.InvDt].Value2 = invoiceDate;
                    ws.Cells[rn, (int)qCols.InvNo].Value2 = invoiceNumber;
                    ws.Cells[rn, (int)qCols.CCode].Value2 = cCode;
                    ws.Cells[rn, (int)qCols.ItemNo].Value2 = itemNumber;
                    ws.Cells[rn, (int)qCols.SerialNo].Value2 = currentSerial;
                    ws.Cells[rn, (int)qCols.BtAddress].Value2 = billTo.address;
                    ws.Cells[rn, (int)qCols.BtCity].Value2 = billTo.city;
                    ws.Cells[rn, (int)qCols.BtState].Value2 = billTo.state;
                    ws.Cells[rn, (int)qCols.BtZip].Value2 = billTo.zip;
                    ws.Cells[rn, (int)qCols.StCompany].Value2 = endUserName;
                    ws.Cells[rn, (int)qCols.StAddress].Value2 = shipTo.address;
                    ws.Cells[rn, (int)qCols.StCity].Value2 = shipTo.city;
                    ws.Cells[rn, (int)qCols.StState].Value2 = shipTo.state;
                    ws.Cells[rn, (int)qCols.StZip].Value2 = shipTo.zip;
                    ws.Cells[rn, (int)qCols.SalesRepID].Value2 = salesRepID;
                }
            }

            private void getFulfillment()
            {

            }
        }

        private enum qCols
        {
            ZEROINDEX,
            ResellerNo,
            ResellerName,
            EndUserName,
            InvDt,
            InvNo,
            CCode,
            ItemNo,
            SerialNo,
            BtAddress,
            BtCity,
            BtState,
            BtZip,
            StCompany,
            StAddress,
            StCity,
            StState,
            StZip,
            SalesRepID
        }

        private class xRow
        {
            private enum xCols
            {
                ZEROINDEX,
                ResellerNo,
                ResellerName,
                EndUserName,
                InvDt,
                InvNo,
                Part,
                ItemNumber,
                SerialNo,
                PgrmCd,
                Units,
                UnitCost,
                UnitRebate,
                AdjUnitCost,
                FFUnitPcnt,
                FFUnitRebate,
                TotalUnitRebate,
                TotalExtRebate,
                EpsonenVision
            }

            private enum enVisionLevels
            {
                ZEROINDEX,
                NO,
                PartnerSelect,
                PlusPremier,
                eFi,
                Member,
                Special,
                Colorworks
            }

            public string resellerNo { get; }
            public string customerNumber { get; }
            public string resellerName { get; }
            public string endUserName { get; }
            public DateTime invoiceDate { get; }
            public string invoiceNo { get; }
            public string part { get; }
            public string itemNumber { get; }
            public string[] serialNumbers { get; }
            public int pgrmCd { get; }
            public int quantity { get; }
            public int unitCost { get; }
            public int unitRebate { get; }
            public int adjUnitCost { get; }
            public int ffUnitPcnt { get; }
            public int ffUnitRebate { get; }
            public int totalUnitRebate { get; }
            public int totalExtRebate { get; }
            public string enVisionStatus { get; }

            private bool formattingSet = false;

            public xRow(
                object rsNumber, object custNum, object rsName, object esName, object date, object invoice, object partNumber, object mfgNumber,
                object delimittedSerials, object enVisionCode, object qty, object cost, object rebate, object fulfillmentPcnt)
            {
                resellerNo = rsNumber.ToString().Trim();
                customerNumber = custNum.ToString().Trim();
                resellerName = rsName.ToString().Trim();
                endUserName = esName.ToString().Trim();
                invoiceDate = Convert.ToDateTime(date);
                part = partNumber.ToString().Trim();
                itemNumber = mfgNumber.ToString().Trim();
                serialNumbers = delimittedSerials.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                pgrmCd = Convert.ToInt32(enVisionCode);
                quantity = Convert.ToInt32(qty);
                unitCost = Convert.ToInt32(cost);
                unitRebate = Convert.ToInt32(rebate);

                switch (pgrmCd)
                {
                    case (int)enVisionLevels.PartnerSelect:
                        ffUnitPcnt = 0; enVisionStatus = "Partner-Select"; break;
                    case (int)enVisionLevels.PlusPremier:
                        ffUnitPcnt = Convert.ToInt32(fulfillmentPcnt); break;
                    case (int)enVisionLevels.eFi:

                    default: ffUnitPcnt = 0; enVisionStatus = "Plus/Premier"; break;
                }
            }
        }


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }

}
