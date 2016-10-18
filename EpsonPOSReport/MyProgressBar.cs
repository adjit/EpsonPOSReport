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
    public partial class MyProgressBar : Form
    {
        public IProgress<int> progress { get; private set; }
        public IProgress<string> spaListProgress { get; private set; }
        public IProgress<string> priceListProgress { get; private set; }
        public IProgress<string> partnerListProgress { get; private set; }

        public MyProgressBar()
        {
            InitializeComponent();

            progress = new Progress<int>(i => progressBar1.Value += i);
            spaListProgress = new Progress<string>(s => SpaListLabel.Text = s);
            priceListProgress = new Progress<string>(s => PriceListLabel.Text = s);
            partnerListProgress = new Progress<string>(s => PartnerListLabel.Text = s);
        }
    }
}
