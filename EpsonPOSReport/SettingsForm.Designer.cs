namespace EpsonPOSReport
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.partnerListBrowseButton = new System.Windows.Forms.Button();
            this.priceListBrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.spaListFilePath = new System.Windows.Forms.TextBox();
            this.priceListFilePath = new System.Windows.Forms.TextBox();
            this.partnerListFilePath = new System.Windows.Forms.TextBox();
            this.spaBrowseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.partnerListBrowseButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.priceListBrowseButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.spaListFilePath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.priceListFilePath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.partnerListFilePath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.spaBrowseButton, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(559, 161);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // partnerListBrowseButton
            // 
            this.partnerListBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.partnerListBrowseButton.Location = new System.Drawing.Point(462, 88);
            this.partnerListBrowseButton.Name = "partnerListBrowseButton";
            this.partnerListBrowseButton.Size = new System.Drawing.Size(94, 23);
            this.partnerListBrowseButton.TabIndex = 6;
            this.partnerListBrowseButton.Text = "Browse";
            this.partnerListBrowseButton.UseVisualStyleBackColor = true;
            // 
            // priceListBrowseButton
            // 
            this.priceListBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.priceListBrowseButton.Location = new System.Drawing.Point(462, 48);
            this.priceListBrowseButton.Name = "priceListBrowseButton";
            this.priceListBrowseButton.Size = new System.Drawing.Size(94, 23);
            this.priceListBrowseButton.TabIndex = 4;
            this.priceListBrowseButton.Text = "Browse";
            this.priceListBrowseButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Spa List File Location";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Price List File Location";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "Partner List File Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spaListFilePath
            // 
            this.spaListFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.spaListFilePath.Location = new System.Drawing.Point(132, 10);
            this.spaListFilePath.Name = "spaListFilePath";
            this.spaListFilePath.Size = new System.Drawing.Size(324, 20);
            this.spaListFilePath.TabIndex = 1;
            this.spaListFilePath.Text = "File Path...";
            // 
            // priceListFilePath
            // 
            this.priceListFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.priceListFilePath.Location = new System.Drawing.Point(132, 50);
            this.priceListFilePath.Name = "priceListFilePath";
            this.priceListFilePath.Size = new System.Drawing.Size(324, 20);
            this.priceListFilePath.TabIndex = 3;
            this.priceListFilePath.Text = "File Path...";
            // 
            // partnerListFilePath
            // 
            this.partnerListFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.partnerListFilePath.Location = new System.Drawing.Point(132, 90);
            this.partnerListFilePath.Name = "partnerListFilePath";
            this.partnerListFilePath.Size = new System.Drawing.Size(324, 20);
            this.partnerListFilePath.TabIndex = 5;
            this.partnerListFilePath.Text = "File Path...";
            // 
            // spaBrowseButton
            // 
            this.spaBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.spaBrowseButton.Location = new System.Drawing.Point(462, 8);
            this.spaBrowseButton.Name = "spaBrowseButton";
            this.spaBrowseButton.Size = new System.Drawing.Size(94, 23);
            this.spaBrowseButton.TabIndex = 2;
            this.spaBrowseButton.Text = "Browse";
            this.spaBrowseButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 161);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(575, 200);
            this.Name = "SettingsForm";
            this.Text = "Report Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button partnerListBrowseButton;
        private System.Windows.Forms.Button priceListBrowseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox spaListFilePath;
        private System.Windows.Forms.TextBox priceListFilePath;
        private System.Windows.Forms.TextBox partnerListFilePath;
        private System.Windows.Forms.Button spaBrowseButton;
    }
}