using System;
using System.IO;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace EpsonPOSReport
{
    public partial class ThisAddIn
    {
        private PriceLevels pls;

        private bool reportsInitialized = false;

        public List<string> checkRows = new List<string>();

        private SpaList spaList = new SpaList();
        private epsonPriceList priceList = new epsonPriceList();
        private enVisionPartnerList partnerList = new enVisionPartnerList();

        private enVisionCustomer lastCustomer = null;
        private Item lastItem = null;

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

            foreach (var task in taskArray) task.Dispose();
        }

        public void runReport()
        {
            
        }

        public void runQueryReport(DateTime date)
        {
            int month = date.Month;
            int year = date.Year;

            qRow thisRow;

            //string query = File.ReadAllText(@"EpsonQuery.sql");
            string query = Properties.Resources.EpsonQuery;
            query = string.Format(query, month, year);

            List<string> checkRows = new List<string>();

            Excel.Workbook thisWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook;
            Excel.Worksheet thisWorksheet = thisWorkbook.ActiveSheet;

            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default._DatabaseConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = dbConnection;

            dbConnection.Open();

            reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                setQueryColumnFormatting(thisWorksheet);

                while(reader.Read())
                {
                    thisRow = GetQueryRow(reader);
                    checkRows.AddRange(thisRow.parseQueryRow(thisWorksheet));
                }
            }
            else System.Windows.Forms.MessageBox.Show("No Rows Found");

            if (checkRows.Count > 0)
            {
                WarnMismatchedRows(checkRows);
            }
            
            dbConnection.Close();

            releaseObject(thisWorkbook);
            releaseObject(thisWorksheet);
        }

        private static void WarnMismatchedRows(List<string> checkRows)
        {
            string errorRows = string.Join(", ", checkRows);

            System.Windows.Forms.MessageBox.Show("Please check the following rows: " + errorRows + "\nSerial Number Count does not equal Quantity",
                                                    "Quantity Warning",
                                                    System.Windows.Forms.MessageBoxButtons.OK,
                                                    System.Windows.Forms.MessageBoxIcon.Warning);
        }

        private static qRow GetQueryRow(SqlDataReader reader)
        {
            return new qRow(    reader.GetValue((int)qCols.Cogs),
                                reader.GetValue((int)qCols.CustNo),
                                reader.GetValue((int)qCols.ResellerNo),
                                reader.GetValue((int)qCols.ResellerName),
                                reader.GetValue((int)qCols.EndUserName),
                                reader.GetValue((int)qCols.InvDt),
                                reader.GetValue((int)qCols.InvNo),
                                reader.GetValue((int)qCols.CCode),
                                reader.GetValue((int)qCols.ItemNo),
                                reader.GetValue((int)qCols.SerialNo),
                                reader.GetValue((int)qCols.QTY),
                                reader.GetValue((int)qCols.SalesRepID),
                                reader.GetValue((int)qCols.BtAddress),
                                reader.GetValue((int)qCols.BtCity),
                                reader.GetValue((int)qCols.BtState),
                                reader.GetValue((int)qCols.BtZip),
                                reader.GetValue((int)qCols.StAddress),
                                reader.GetValue((int)qCols.StCity),
                                reader.GetValue((int)qCols.StState),
                                reader.GetValue((int)qCols.StZip)
                            );
        }

        private List<string> ParseRow(qRow row, Excel.Worksheet ws)
        {
            List<string> checkRows = new List<string>();

            int rn = ws.UsedRange.Row + ws.UsedRange.Rows.Count;
            int thisQuantity;
            int pgrmCd = 0;

            double rebateAmount = 0.00;
            double ffpcnt = 0.00;
            double unitCost = 0.00;

            string currentSerial = "";

            bool _hasSpa = false;

            enVisionCustomer thisCustomer = null;
            Item thisItem = null;

            List<itemFulfillment> spaItems;

            if(row.serialNumbers.Length > 0 && row.serialNumbers.Length != row.quantity)
            {
                checkRows.Add(rn.ToString());
            }

            if(rn == 2)
            {
                AddRunHeader(ws);
                setRunColumnFormatting(ws);
            }

            if (lastCustomer == null) thisCustomer = lastCustomer = partnerList.getCustomer(row.enVisionNumber, true);
            else if (lastCustomer.enVisionNumber == row.enVisionNumber) thisCustomer = lastCustomer;
            else thisCustomer = partnerList.getCustomer(row.enVisionNumber, true);

            /*   UNCOMMENT WHEN SPA LIST CLASS IS COMPLETE
             * 
            
            spaItems = spaList.getCustomerSpaItems(thisCustomer.customer);

            if(spaItems != null)
            {
                for(int i = 0; i < spaItems.Count; i++)
                {
                    if(spaItems[i].item == row.cCode)
                    {
                        rebateAmount = spaItems[i].rebate;
                        ffpcnt = spaItems[i].fulfillment;
                        unitCost = spaItems[i].unitCost;
                        _hasSpa = true;
                    }
                }
            }*/

            if (!_hasSpa)
            {
                if (lastItem == null) thisItem = lastItem = priceList.getItem(row.cCode);
                else if (lastItem.cCode == row.cCode && thisCustomer == lastCustomer) thisItem = lastItem;
                else thisItem = priceList.getItem(row.cCode);

                switch (thisCustomer.priceLevelIndex)
                {
                    case PriceLevelIndex.SELECT:
                        rebateAmount = thisItem.Select.rebate;
                        ffpcnt = thisItem.Select.fulfillment;
                        pgrmCd = Properties.Settings.Default._pgrmCode_select;
                        break;
                    case PriceLevelIndex.PLUS:
                        rebateAmount = thisItem.Plus.rebate;
                        ffpcnt = thisItem.Plus.fulfillment;
                        pgrmCd = Properties.Settings.Default._pgrmCode_plus;
                        break;
                    case PriceLevelIndex.PREMIER:
                        rebateAmount = thisItem.Premier.rebate;
                        ffpcnt = thisItem.Premier.fulfillment;
                        pgrmCd = Properties.Settings.Default._pgrmCode_premier;
                        break;
                    case PriceLevelIndex.MSELECT:
                        rebateAmount = thisItem.mSelect.rebate;
                        ffpcnt = thisItem.mSelect.fulfillment;
                        pgrmCd = Properties.Settings.Default._pgrmCode_mSelect;
                        break;
                    default:
                        break;
                }
            }

            int serialLength = row.serialNumbers.Length;

            for (int i = 0; i <= serialLength; i++)
            {
                if (serialLength == 0)
                {
                    currentSerial = "";
                    thisQuantity = row.quantity;
                }
                else if (i == serialLength) break;
                else
                {
                    currentSerial = row.serialNumbers[i];
                    thisQuantity = 1;
                }

                double adjUnitCost = unitCost - rebateAmount;
                double ffUnitRebate = adjUnitCost * ffpcnt;
                double totalUnitRebate = rebateAmount + ffUnitRebate;
                double totalExtRebate = totalUnitRebate * thisQuantity;

                ws.Cells[rn, (int)xrCols.ResellerNo].Value2 = row.enVisionNumber;
                ws.Cells[rn, (int)xrCols.ResellerName].Value2 = row.customerName;
                ws.Cells[rn, (int)xrCols.EndUserName].Value2 = row.endUserName;
                ws.Cells[rn, (int)xrCols.InvDt].Value2 = row.invoiceDate;
                ws.Cells[rn, (int)xrCols.InvNo].Value2 = row.invoiceNumber;
                ws.Cells[rn, (int)xrCols.CCode].Value2 = row.cCode;
                ws.Cells[rn, (int)xrCols.ItemNo].Value2 = row.itemNumber;
                ws.Cells[rn, (int)xrCols.SerialNo].Value2 = currentSerial;
                ws.Cells[rn, (int)xrCols.PgrmCd].Value2 = pgrmCd;
                ws.Cells[rn, (int)xrCols.QTY].Value2 = thisQuantity;
                ws.Cells[rn, (int)xrCols.UnitCost].Value2 = unitCost;
                ws.Cells[rn, (int)xrCols.UnitRebate].Value2 = rebateAmount;
                ws.Cells[rn, (int)xrCols.AdjUnitCost].Value2 = adjUnitCost;
                ws.Cells[rn, (int)xrCols.FFUnitPcnt].Value2 = ffpcnt;
                ws.Cells[rn, (int)xrCols.FFUnitRebate].Value2 = ffUnitRebate;
                ws.Cells[rn, (int)xrCols.TotalUnitRebate].Value2 = totalUnitRebate;
                ws.Cells[rn, (int)xrCols.TotalExtRebate].Value2 = totalExtRebate;
                ws.Cells[rn, (int)xrCols.EnvisionLevel].Value2 = thisCustomer.enVisionLevel;
                ws.Cells[rn, (int)xrCols.BtAddress].Value2 = row.billTo.address;
                ws.Cells[rn, (int)xrCols.BtCity].Value2 = row.billTo.city;
                ws.Cells[rn, (int)xrCols.BtState].Value2 = row.billTo.state;
                ws.Cells[rn, (int)xrCols.BtZip].Value2 = row.billTo.zip;
                ws.Cells[rn, (int)xrCols.StAddress].Value2 = row.shipTo.address;
                ws.Cells[rn, (int)xrCols.StCity].Value2 = row.shipTo.city;
                ws.Cells[rn, (int)xrCols.StState].Value2 = row.shipTo.state;
                ws.Cells[rn, (int)xrCols.StZip].Value2 = row.shipTo.zip;
                ws.Cells[rn, (int)xrCols.SalesRepID].Value2 = row.salesRepID;

                rn++;
            }

            lastCustomer = thisCustomer;
            lastItem = thisItem;
            RemoveOptionalColumns(ws);
            return checkRows;
        }

        private static void RemoveOptionalColumns(Excel.Worksheet ws)
        {
            Properties.Settings sets = Properties.Settings.Default;

            if (!sets._showColumn_btAddress) ws.Cells[1, (int)xrCols.BtAddress].EntireColumn.Delete();
            if (!sets._showColumn_btCity) ws.Cells[1, (int)xrCols.BtCity].EntireColumn.Delete();
            if (!sets._showColumn_btState) ws.Cells[1, (int)xrCols.BtState].EntireColumn.Delete();
            if (!sets._showColumn_btZip) ws.Cells[1, (int)xrCols.BtZip].EntireColumn.Delete();
            if (!sets._showColumn_stAddress) ws.Cells[1, (int)xrCols.StAddress].EntireColumn.Delete();
            if (!sets._showColumn_stCity) ws.Cells[1, (int)xrCols.StCity].EntireColumn.Delete();
            if (!sets._showColumn_stState) ws.Cells[1, (int)xrCols.StState].EntireColumn.Delete();
            if (!sets._showColumn_stZip) ws.Cells[1, (int)xrCols.StZip].EntireColumn.Delete();

        }

        private static void AddRunHeader(Excel.Worksheet ws)
        {
            ws.Cells[1, (int)xrCols.ResellerNo].Value2 = "Reseller No.";
            ws.Cells[1, (int)xrCols.ResellerName].Value2 = "Reseller Name";
            ws.Cells[1, (int)xrCols.EndUserName].Value2 = "End User Name";
            ws.Cells[1, (int)xrCols.InvDt].Value2 = "Invoice Date";
            ws.Cells[1, (int)xrCols.InvNo].Value2 = "Invoice No.";
            ws.Cells[1, (int)xrCols.CCode].Value2 = "Part";
            ws.Cells[1, (int)xrCols.ItemNo].Value2 = "Item Number";
            ws.Cells[1, (int)xrCols.SerialNo].Value2 = "Serial No.";
            ws.Cells[1, (int)xrCols.PgrmCd].Value2 = "PgrmCd";
            ws.Cells[1, (int)xrCols.QTY].Value2 = "Quantity";
            ws.Cells[1, (int)xrCols.UnitCost].Value2 = "Unit Cost";
            ws.Cells[1, (int)xrCols.UnitRebate].Value2 = "Unit Rebate";
            ws.Cells[1, (int)xrCols.AdjUnitCost].Value2 = "Adjusted Unit Cost";
            ws.Cells[1, (int)xrCols.FFUnitPcnt].Value2 = "FF Unit Pcnt";
            ws.Cells[1, (int)xrCols.FFUnitRebate].Value2 = "FF Unit Rebate";
            ws.Cells[1, (int)xrCols.TotalUnitRebate].Value2 = "Total Unit Rebate";
            ws.Cells[1, (int)xrCols.TotalExtRebate].Value2 = "Total Ext Rebate";
            ws.Cells[1, (int)xrCols.EnvisionLevel].Value2 = "Epson enVision";
            ws.Cells[1, (int)xrCols.BtAddress].Value2 = "Customer Address";
            ws.Cells[1, (int)xrCols.BtCity].Value2 = "Customer City";
            ws.Cells[1, (int)xrCols.BtState].Value2 = "Customer State";
            ws.Cells[1, (int)xrCols.BtZip].Value2 = "Customer Zip";
            ws.Cells[1, (int)xrCols.StAddress].Value2 = "Ship To Address";
            ws.Cells[1, (int)xrCols.StCity].Value2 = "Ship To City";
            ws.Cells[1, (int)xrCols.StState].Value2 = "Ship To State";
            ws.Cells[1, (int)xrCols.StZip].Value2 = "Ship To Zip";
            ws.Cells[1, (int)xrCols.SalesRepID].Value2 = "Salserep ID";
        }

        private void setQueryColumnFormatting(Excel.Worksheet ws)
        {
            ws.Cells[1, (int)xqCols.InvDt].EntireColumn.NumberFormat = "MM/DD/YYYY";
            ws.Cells[1, (int)xqCols.SerialNo].EntireColumn.NumberFormat = "@";
            ws.Cells[1, (int)xqCols.BtZip].EntireColumn.NumberFormat = "00000";
            ws.Cells[1, (int)xqCols.StZip].EntireColumn.NumberFormat = "00000";
            ws.Cells[1, (int)xqCols.SalesRepID].EntireColumn.NumberFormat = "000";
        }

        private void setRunColumnFormatting(Excel.Worksheet ws)
        {
            ws.Cells[1, (int)xrCols.InvDt].EntireColumn.NumberFormat = "MM/DD/YYYY";
            ws.Cells[1, (int)xrCols.SerialNo].EntireColumn.NumberFormat = "@";
            ws.Cells[1, (int)xrCols.BtZip].EntireColumn.NumberFormat = "00000";
            ws.Cells[1, (int)xrCols.StZip].EntireColumn.NumberFormat = "00000";
            ws.Cells[1, (int)xrCols.UnitCost].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.UnitRebate].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.AdjUnitCost].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.FFUnitPcnt].EntireColumn.NumberFormat = "0.00%";
            ws.Cells[1, (int)xrCols.FFUnitRebate].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.TotalUnitRebate].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.TotalExtRebate].EntireColumn.NumberFormat = "$0.00";
            ws.Cells[1, (int)xrCols.SalesRepID].EntireColumn.NumberFormat = "000";
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
                serialNumbers = delimittedSerials.ToString().Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
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

            public List<string> parseQueryRow(Excel.Worksheet ws)
            {
                int rn = ws.UsedRange.Row + ws.UsedRange.Rows.Count;
                int thisQuantity;

                string currentSerial = "";

                List<string> checkRows = new List<string>();

                //If there is a mismatch between the number of serials and quantity warn user
                if(serialNumbers.Length > 0 && serialNumbers.Length != quantity)
                {
                    checkRows.Add(rn.ToString());
                }
                
                //If the row is 2 (aka the very first row) add header
                if(rn == 2)
                {
                    AddQueryHeaderTo(ws);
                }

                for (int i = 0; i <= serialNumbers.Length; i++)
                {
                    if (serialNumbers.Length == 0)
                    {
                        currentSerial = "";
                        thisQuantity = quantity;
                    }
                    else if (i == serialNumbers.Length) break;
                    else
                    {
                        currentSerial = serialNumbers[i];
                        thisQuantity = 1;
                    }

                    ws.Cells[rn, (int)xqCols.ResellerNo].Value2 = enVisionNumber;
                    ws.Cells[rn, (int)xqCols.ResellerName].Value2 = customerName;
                    ws.Cells[rn, (int)xqCols.EndUserName].Value2 = endUserName;
                    ws.Cells[rn, (int)xqCols.InvDt].Value2 = invoiceDate;
                    ws.Cells[rn, (int)xqCols.InvNo].Value2 = invoiceNumber;
                    ws.Cells[rn, (int)xqCols.CCode].Value2 = cCode;
                    ws.Cells[rn, (int)xqCols.ItemNo].Value2 = itemNumber;
                    ws.Cells[rn, (int)xqCols.SerialNo].Value2 = currentSerial;
                    ws.Cells[rn, (int)xqCols.QTY].Value2 = thisQuantity;
                    ws.Cells[rn, (int)xqCols.BtAddress].Value2 = billTo.address;
                    ws.Cells[rn, (int)xqCols.BtCity].Value2 = billTo.city;
                    ws.Cells[rn, (int)xqCols.BtState].Value2 = billTo.state;
                    ws.Cells[rn, (int)xqCols.BtZip].Value2 = billTo.zip;
                    ws.Cells[rn, (int)xqCols.StCompany].Value2 = endUserName;
                    ws.Cells[rn, (int)xqCols.StAddress].Value2 = shipTo.address;
                    ws.Cells[rn, (int)xqCols.StCity].Value2 = shipTo.city;
                    ws.Cells[rn, (int)xqCols.StState].Value2 = shipTo.state;
                    ws.Cells[rn, (int)xqCols.StZip].Value2 = shipTo.zip;
                    ws.Cells[rn, (int)xqCols.SalesRepID].Value2 = salesRepID;
                    
                    rn++;
                }

                return checkRows;
            }

            private static void AddQueryHeaderTo(Excel.Worksheet ws)
            {
                ws.Cells[1, (int)xqCols.ResellerNo].Value2 = "Reseller No.";
                ws.Cells[1, (int)xqCols.ResellerName].Value2 = "Reseller Name";
                ws.Cells[1, (int)xqCols.EndUserName].Value2 = "End User Name";
                ws.Cells[1, (int)xqCols.InvDt].Value2 = "Invoice Date";
                ws.Cells[1, (int)xqCols.InvNo].Value2 = "Invoice No.";
                ws.Cells[1, (int)xqCols.CCode].Value2 = "Part";
                ws.Cells[1, (int)xqCols.ItemNo].Value2 = "Item Number";
                ws.Cells[1, (int)xqCols.SerialNo].Value2 = "Serial No.";
                ws.Cells[1, (int)xqCols.QTY].Value2 = "Quantity";
                ws.Cells[1, (int)xqCols.BtAddress].Value2 = "Customer Address";
                ws.Cells[1, (int)xqCols.BtCity].Value2 = "Customer City";
                ws.Cells[1, (int)xqCols.BtState].Value2 = "Customer State";
                ws.Cells[1, (int)xqCols.BtZip].Value2 = "Customer Zip";
                ws.Cells[1, (int)xqCols.StCompany].Value2 = "Ship To Customer";
                ws.Cells[1, (int)xqCols.StAddress].Value2 = "Ship To Address";
                ws.Cells[1, (int)xqCols.StCity].Value2 = "Ship To City";
                ws.Cells[1, (int)xqCols.StState].Value2 = "Ship To State";
                ws.Cells[1, (int)xqCols.StZip].Value2 = "Ship To Zip";
                ws.Cells[1, (int)xqCols.SalesRepID].Value2 = "Salserep ID";
            }

            private void getFulfillment()
            {

            }
        }

        //This enum refers to the excel columns to print the query only
        private enum xqCols
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
            QTY,
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

        private enum xrCols
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
            PgrmCd,
            QTY,
            UnitCost,
            UnitRebate,
            AdjUnitCost,
            FFUnitPcnt,
            FFUnitRebate,
            TotalUnitRebate,
            TotalExtRebate,
            EnvisionLevel,
            BtAddress,
            BtCity,
            BtState,
            BtZip,
            StAddress,
            StCity,
            StState,
            StZip,
            SalesRepID
        }

        //This enum refers to the columns produced by the query
        private enum qCols
        {
            Cogs,
            CustNo,
            ResellerNo,
            ResellerName,
            EndUserName,
            InvDt,
            InvNo,
            CCode,
            ItemNo,
            SerialNo,
            QTY,
            SalesRepID,
            BtAddress,
            BtCity,
            BtState,
            BtZip,
            StAddress,
            StCity,
            StState,
            StZip
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
