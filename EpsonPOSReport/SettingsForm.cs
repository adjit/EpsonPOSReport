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
            spaListFolderPath.Text = s._folderPath_spaList;

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

            /*------------   Group Number Settings Load   ---------------*/
            var gMs = s._groupMembers;
            DataTable dt = new DataTable();
            dt.Columns.Add("RS Number", typeof(string));
            dt.Columns.Add("Group Name", typeof(string));

            for(int i = 0; i < gMs.Count; i++)
            {
                string[] arr = gMs[i].Split(';');
                if (arr.Length == 2) dt.Rows.Add(arr[0], arr[1]);
                else if (arr.Length == 1) dt.Rows.Add(arr[0]);
            }

            groupNumberGrid.DataSource = dt;
            groupNumberGrid.Columns[0].Width = 125;
            groupNumberGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for(int i = 0; i < optionalColumnsChecklistBox.Items.Count; i++)
            {
                switch (optionalColumnsChecklistBox.Items[i].ToString())
                {
                    case "Bill-to Address":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_btAddress);
                        break;
                    case "Bill-to City":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_btCity);
                        break;
                    case "Bill-to State":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_btState);
                        break;
                    case "Bill-to Zip":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_btZip);
                        break;
                    case "Ship-to Address":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_stAddress);
                        break;
                    case "Ship-to City":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_stCity);
                        break;
                    case "Ship-to State":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_stState);
                        break;
                    case "Ship-to Zip":
                        optionalColumnsChecklistBox.SetItemChecked(i, s._showColumn_stZip);
                        break;
                    default:break;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings s = Properties.Settings.Default;

            s._filePath_spaList = spaListFilePath.Text;
            s._filePath_partnerList = partnerListFilePath.Text;
            s._filePath_priceList = priceListFilePath.Text;
            s._folderPath_spaList = spaListFolderPath.Text;
            
            s._plColumn_itemNumber = (int)NUD_itemNumber.Value;
            s._plColumn_unitCost = (int)NUD_unitCost.Value;
            s._plColumn_selectFFP = (int)NUD_selectFFP.Value;
            s._plColumn_selectRebate = (int)NUD_selectRebate.Value;
            s._plColumn_plusFFP = (int)NUD_plusFFP.Value;
            s._plColumn_plusRebate = (int)NUD_plusRebate.Value;
            s._plColumn_premierFFP = (int)NUD_premierFFP.Value;
            s._plColumn_premierRebate = (int)NUD_premierRebate.Value;
            s._plColumn_mSelectFFP = (int)NUD_mSelectFFP.Value;
            s._plColumn_mSelectRebate = (int)NUD_mSelectRebate.Value;

            s._pgrmCode_select = (int)NUD_selectPgrmCd.Value;
            s._pgrmCode_plus = (int)NUD_plusPgrmCd.Value;
            s._pgrmCode_premier = (int)NUD_premierPgrmCd.Value;
            s._pgrmCode_mSelect = (int)NUD_mSelectPgrmCd.Value;
            s._pgrmCode_colorSelect = (int)NUD_colorSelectPgrmCd.Value;
            s._pgrmCode_colorPlus = (int)NUD_colorPlusPgrmCd.Value;
            s._pgrmCode_colorPremier = (int)NUD_colorPremierPgrmCd.Value;

            string newGroupMember = "";

            s._groupMembers.Clear();

            for(int i = 0; i < groupNumberGrid.Rows.Count; i++)
            {
                if (groupNumberGrid.Rows[i].Cells[0].Value == null) continue;
                else
                {
                    newGroupMember = groupNumberGrid.Rows[i].Cells[0].Value.ToString() + ";";
                    newGroupMember += groupNumberGrid.Rows[i].Cells[1].Value.ToString();
                    if (s._groupMembers.Contains(newGroupMember)) continue;
                    else s._groupMembers.Add(newGroupMember);
                }
            }

            for (int i = 0; i < optionalColumnsChecklistBox.Items.Count; i++)
            {
                switch (optionalColumnsChecklistBox.Items[i].ToString())
                {
                    case "Bill-to Address":
                        s._showColumn_btAddress = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Bill-to City":
                        s._showColumn_btCity = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Bill-to State":
                        s._showColumn_btState = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Bill-to Zip":
                        s._showColumn_btZip = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Ship-to Address":
                        s._showColumn_stAddress = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Ship-to City":
                        s._showColumn_stCity = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Ship-to State":
                        s._showColumn_stState = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    case "Ship-to Zip":
                        s._showColumn_stZip = optionalColumnsChecklistBox.GetItemChecked(i);
                        break;
                    default: break;
                }
            }

            s.Save();
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void spaBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xl;*.xls;*xlsx;*.xlsm";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK) spaListFilePath.Text = ofd.FileName;
        }

        private void priceListBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xl;*.xls;*xlsx;*.xlsm";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK) priceListFilePath.Text = ofd.FileName;
        }

        private void partnerListBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xl;*.xls;*xlsx;*.xlsm";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK) partnerListFilePath.Text = ofd.FileName;
        }

        private void spaListFolderBrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK) spaListFolderPath.Text = fbd.SelectedPath;
        }
    }
}
