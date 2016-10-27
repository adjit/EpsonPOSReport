using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace EpsonPOSReport
{
    public partial class EpsonReportRibbon
    {
        private void EpsonReportRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void runReportButton_Click(object sender, RibbonControlEventArgs e)
        {
            EpsonReportForm epf = new EpsonReportForm();
            epf.Show();
        }
    }
}
