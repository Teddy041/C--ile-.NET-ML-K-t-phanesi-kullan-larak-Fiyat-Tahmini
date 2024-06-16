namespace FuelPricePrediction.Forms
{
    partial class frmAnaSayfa
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
            this.btnPredict = new Guna.UI2.WinForms.Guna2Button();
            this.dtpDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblResult = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.comboBoxCities = new System.Windows.Forms.ComboBox();
            this.btnGetir = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPredict
            // 
            this.btnPredict.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPredict.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPredict.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPredict.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPredict.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPredict.ForeColor = System.Drawing.Color.Black;
            this.btnPredict.Location = new System.Drawing.Point(243, 14);
            this.btnPredict.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(205, 36);
            this.btnPredict.TabIndex = 0;
            this.btnPredict.Text = "Tahmin Yap";
            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = true;
            this.dtpDate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDate.Location = new System.Drawing.Point(15, 14);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 36);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.Value = new System.DateTime(2024, 5, 19, 22, 44, 5, 745);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
            this.lblResult.Location = new System.Drawing.Point(24, 91);
            this.lblResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(3, 2);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = null;
            // 
            // comboBoxCities
            // 
            this.comboBoxCities.FormattingEnabled = true;
            this.comboBoxCities.Items.AddRange(new object[] {
            "istanbul"});
            this.comboBoxCities.Location = new System.Drawing.Point(1059, 37);
            this.comboBoxCities.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCities.Name = "comboBoxCities";
            this.comboBoxCities.Size = new System.Drawing.Size(160, 24);
            this.comboBoxCities.TabIndex = 3;
            this.comboBoxCities.Text = "İl Seçiniz";
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(1227, 34);
            this.btnGetir.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(100, 28);
            this.btnGetir.TabIndex = 4;
            this.btnGetir.Text = "Getir";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1071, 91);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(760, 466);
            this.dataGridView1.TabIndex = 5;
            // 
            // frmAnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 631);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGetir);
            this.Controls.Add(this.comboBoxCities);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnPredict);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmAnaSayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAnaSayfa";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnPredict;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDate;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblResult;
        private System.Windows.Forms.ComboBox comboBoxCities;
        private System.Windows.Forms.Button btnGetir;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}