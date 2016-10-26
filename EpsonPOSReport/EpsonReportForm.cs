using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpsonPOSReport
{
    public partial class EpsonReportForm : Form
    {
        public EpsonReportForm()
        {
            InitializeComponent();
        }

        private void runQueryOnlyBtn_Click(object sender, EventArgs e)
        {
            runningLabel.Text = "Running...";
            Globals.ThisAddIn.runQueryReport(monthPicker.Value);
            Close();
        }
    }
}
