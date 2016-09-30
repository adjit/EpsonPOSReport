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

        public xRow()
        {

        }
    }
}
