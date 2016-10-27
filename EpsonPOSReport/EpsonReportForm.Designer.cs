namespace EpsonPOSReport
{
    partial class EpsonReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpsonReportForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.runQueryOnlyBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.selectMonthLabel = new System.Windows.Forms.Label();
            this.monthPicker = new System.Windows.Forms.DateTimePicker();
            this.changeSettingsButton = new System.Windows.Forms.LinkLabel();
            this.runReportButton = new System.Windows.Forms.Button();
            this.runningLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run Epson Report";
            // 
            // runQueryOnlyBtn
            // 
            this.runQueryOnlyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runQueryOnlyBtn.Location = new System.Drawing.Point(154, 64);
            this.runQueryOnlyBtn.Name = "runQueryOnlyBtn";
            this.runQueryOnlyBtn.Size = new System.Drawing.Size(141, 25);
            this.runQueryOnlyBtn.TabIndex = 1;
            this.runQueryOnlyBtn.Text = "Run Query Only";
            this.runQueryOnlyBtn.UseVisualStyleBackColor = true;
            this.runQueryOnlyBtn.Click += new System.EventHandler(this.runQueryOnlyBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.selectMonthLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.monthPicker, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.changeSettingsButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.runReportButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.runQueryOnlyBtn, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(300, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(303, 97);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // selectMonthLabel
            // 
            this.selectMonthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.selectMonthLabel.AutoSize = true;
            this.selectMonthLabel.Location = new System.Drawing.Point(8, 12);
            this.selectMonthLabel.MinimumSize = new System.Drawing.Size(120, 0);
            this.selectMonthLabel.Name = "selectMonthLabel";
            this.selectMonthLabel.Size = new System.Drawing.Size(140, 13);
            this.selectMonthLabel.TabIndex = 0;
            this.selectMonthLabel.Text = "Select First of Month";
            // 
            // monthPicker
            // 
            this.monthPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monthPicker.CustomFormat = "MMMM-  yyyy";
            this.monthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.monthPicker.Location = new System.Drawing.Point(154, 8);
            this.monthPicker.Name = "monthPicker";
            this.monthPicker.Size = new System.Drawing.Size(141, 20);
            this.monthPicker.TabIndex = 1;
            // 
            // changeSettingsButton
            // 
            this.changeSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changeSettingsButton.AutoSize = true;
            this.changeSettingsButton.Enabled = false;
            this.changeSettingsButton.Location = new System.Drawing.Point(8, 33);
            this.changeSettingsButton.Name = "changeSettingsButton";
            this.changeSettingsButton.Size = new System.Drawing.Size(140, 28);
            this.changeSettingsButton.TabIndex = 2;
            this.changeSettingsButton.TabStop = true;
            this.changeSettingsButton.Text = "Change Report Settings...";
            this.changeSettingsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.changeSettingsButton.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // runReportButton
            // 
            this.runReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runReportButton.AutoSize = true;
            this.runReportButton.Enabled = false;
            this.runReportButton.Location = new System.Drawing.Point(154, 36);
            this.runReportButton.Name = "runReportButton";
            this.runReportButton.Size = new System.Drawing.Size(141, 22);
            this.runReportButton.TabIndex = 3;
            this.runReportButton.Text = "Run Report";
            this.runReportButton.UseVisualStyleBackColor = true;
            // 
            // runningLabel
            // 
            this.runningLabel.AutoSize = true;
            this.runningLabel.Location = new System.Drawing.Point(8, 121);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(0, 13);
            this.runningLabel.TabIndex = 1;
            // 
            // EpsonReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(319, 136);
            this.Controls.Add(this.runningLabel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(500, 250);
            this.MinimumSize = new System.Drawing.Size(335, 150);
            this.Name = "EpsonReportForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Epson Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label selectMonthLabel;
        private System.Windows.Forms.DateTimePicker monthPicker;
        private System.Windows.Forms.LinkLabel changeSettingsButton;
        private System.Windows.Forms.Button runReportButton;
        private System.Windows.Forms.Button runQueryOnlyBtn;
        private System.Windows.Forms.Label runningLabel;
    }
}