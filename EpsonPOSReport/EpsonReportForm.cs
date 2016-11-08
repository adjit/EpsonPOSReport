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

        private void changeSettingsButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
        }

        private void runReportButton_Click(object sender, EventArgs e)
        {
            runningLabel.Text = "Running...";
            Globals.ThisAddIn.runReport(monthPicker.Value);
            Close();
        }
    }
}
