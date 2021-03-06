﻿namespace EpsonPOSReport
{
    partial class EpsonReportRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EpsonReportRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpsonReportRibbon));
            this.TabAddIns = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.runReportButton = this.Factory.CreateRibbonButton();
            this.TabAddIns.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabAddIns
            // 
            this.TabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabAddIns.Groups.Add(this.group1);
            this.TabAddIns.Label = "TabAddIns";
            this.TabAddIns.Name = "TabAddIns";
            // 
            // group1
            // 
            this.group1.Items.Add(this.runReportButton);
            this.group1.Label = "Epson Reports";
            this.group1.Name = "group1";
            // 
            // runReportButton
            // 
            this.runReportButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.runReportButton.Image = ((System.Drawing.Image)(resources.GetObject("runReportButton.Image")));
            this.runReportButton.Label = "Run Epson Report";
            this.runReportButton.Name = "runReportButton";
            this.runReportButton.ShowImage = true;
            this.runReportButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.runReportButton_Click);
            // 
            // EpsonReportRibbon
            // 
            this.Name = "EpsonReportRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.TabAddIns);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.EpsonReportRibbon_Load);
            this.TabAddIns.ResumeLayout(false);
            this.TabAddIns.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton runReportButton;
    }

    partial class ThisRibbonCollection
    {
        internal EpsonReportRibbon EpsonReportRibbon
        {
            get { return this.GetRibbon<EpsonReportRibbon>(); }
        }
    }
}
