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
            Properties.Settings s = Properties.Settings.Default;

            spaListFilePath.Text = s._filePath_spaList;
            partnerListFilePath.Text = s._filePath_partnerList;
            priceListFilePath.Text = s._filePath_priceList;

            NUD_itemNumber.Value = s._plColumn_itemNumber;
            NUD_unitCost.Value = s._plColumn_unitCost;
            NUD_selectFFP.Value = s._plColumn_selectFFP;
            NUD_selectRebate.Value = s._plColumn_selectRebate;
            NUD_plusFFP.Value = s._plColumn_plusFFP;
            NUD_plusRebate.Value = s._plColumn_plusRebate;
            NUD_premierFFP.Value = s._plColumn_premierFFP;
            NUD_premierRebate.Value = s._plColumn_premierRebate;
            NUD_mSelectFFP.Value = s._plColumn_mSelectFFP;
            NUD_mSelectRebate.Value = s._plColumn_mSelectRebate;

            NUD_selectPgrmCd.Value = s._pgrmCode_select;
            NUD_plusPgrmCd.Value = s._pgrmCode_plus;
            NUD_premierPgrmCd.Value = s._pgrmCode_premier;
            NUD_mSelectPgrmCd.Value = s._pgrmCode_mSelect;
            NUD_colorSelectPgrmCd.Value = s._pgrmCode_colorSelect;
            NUD_colorPlusPgrmCd.Value = s._pgrmCode_colorPlus;
            NUD_colorPremierPgrmCd.Value = s._pgrmCode_colorPremier;
        }
    }
}
