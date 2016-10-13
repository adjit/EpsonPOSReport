using System;
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

        public void runReport()
        {
        }

        public bool initializePriceLevels()
        {
            //check for settings file (need to come up with a settings structure)
            return false;
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
                invoiceDate = (DateTime)date;
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
