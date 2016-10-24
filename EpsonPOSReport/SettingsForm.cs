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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            populateSettings();
        }

        private void populateSettings()
        {
            spaListFilePath.Text = Properties.Settings.Default._filePath_spaList;
            partnerListFilePath.Text = Properties.Settings.Default._filePath_partnerList;
            priceListFilePath.Text = Properties.Settings.Default._filePath_priceList;
        }
    }
}
