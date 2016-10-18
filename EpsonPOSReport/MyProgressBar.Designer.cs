namespace EpsonPOSReport
{
    partial class MyProgressBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyProgressBar));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SpaListLabel = new System.Windows.Forms.Label();
            this.PriceListLabel = new System.Windows.Forms.Label();
            this.PartnerListLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.SpaListLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PriceListLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PartnerListLabel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 88);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // SpaListLabel
            // 
            this.SpaListLabel.AutoSize = true;
            this.SpaListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpaListLabel.Location = new System.Drawing.Point(3, 0);
            this.SpaListLabel.Name = "SpaListLabel";
            this.SpaListLabel.Size = new System.Drawing.Size(278, 29);
            this.SpaListLabel.TabIndex = 0;
            this.SpaListLabel.Text = "Spa List Status";
            this.SpaListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PriceListLabel
            // 
            this.PriceListLabel.AutoSize = true;
            this.PriceListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PriceListLabel.Location = new System.Drawing.Point(3, 29);
            this.PriceListLabel.Name = "PriceListLabel";
            this.PriceListLabel.Size = new System.Drawing.Size(278, 29);
            this.PriceListLabel.TabIndex = 1;
            this.PriceListLabel.Text = "Price List Status";
            this.PriceListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PartnerListLabel
            // 
            this.PartnerListLabel.AutoSize = true;
            this.PartnerListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartnerListLabel.Location = new System.Drawing.Point(3, 58);
            this.PartnerListLabel.Name = "PartnerListLabel";
            this.PartnerListLabel.Size = new System.Drawing.Size(278, 30);
            this.PartnerListLabel.TabIndex = 2;
            this.PartnerListLabel.Text = "Partner List Status";
            this.PartnerListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MyProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 150);
            this.Name = "MyProgressBar";
            this.Text = "Initialization Progress";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label SpaListLabel;
        private System.Windows.Forms.Label PriceListLabel;
        private System.Windows.Forms.Label PartnerListLabel;
    }
}