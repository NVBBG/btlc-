namespace demobtl2
{
    partial class Thongke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Thongke));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGiaKetThuc = new System.Windows.Forms.TextBox();
            this.txtGiaBanDau = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdTonggiatri = new System.Windows.Forms.RadioButton();
            this.rdallgia = new System.Windows.Forms.RadioButton();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtGiaKetThuc);
            this.groupBox2.Controls.Add(this.txtGiaBanDau);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.rdTonggiatri);
            this.groupBox2.Controls.Add(this.rdallgia);
            this.groupBox2.Location = new System.Drawing.Point(42, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(606, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thống kê theo giá";
            // 
            // txtGiaKetThuc
            // 
            this.txtGiaKetThuc.Location = new System.Drawing.Point(428, 17);
            this.txtGiaKetThuc.Name = "txtGiaKetThuc";
            this.txtGiaKetThuc.Size = new System.Drawing.Size(80, 20);
            this.txtGiaKetThuc.TabIndex = 8;
            // 
            // txtGiaBanDau
            // 
            this.txtGiaBanDau.Location = new System.Drawing.Point(299, 17);
            this.txtGiaBanDau.Name = "txtGiaBanDau";
            this.txtGiaBanDau.Size = new System.Drawing.Size(79, 20);
            this.txtGiaBanDau.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Đến";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Từ";
            // 
            // rdTonggiatri
            // 
            this.rdTonggiatri.AutoSize = true;
            this.rdTonggiatri.Location = new System.Drawing.Point(106, 20);
            this.rdTonggiatri.Name = "rdTonggiatri";
            this.rdTonggiatri.Size = new System.Drawing.Size(97, 17);
            this.rdTonggiatri.TabIndex = 3;
            this.rdTonggiatri.Text = "Tổng giá trị HD";
            this.rdTonggiatri.UseVisualStyleBackColor = true;
            this.rdTonggiatri.CheckedChanged += new System.EventHandler(this.rdTonggiatri_CheckedChanged);
            // 
            // rdallgia
            // 
            this.rdallgia.AutoSize = true;
            this.rdallgia.Checked = true;
            this.rdallgia.Location = new System.Drawing.Point(27, 20);
            this.rdallgia.Name = "rdallgia";
            this.rdallgia.Size = new System.Drawing.Size(56, 17);
            this.rdallgia.TabIndex = 2;
            this.rdallgia.TabStop = true;
            this.rdallgia.Text = "Tất cả";
            this.rdallgia.UseVisualStyleBackColor = true;
            this.rdallgia.CheckedChanged += new System.EventHandler(this.rdallgia_CheckedChanged);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 67);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(676, 383);
            this.crystalReportViewer1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(514, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Thống kê";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Thongke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(676, 450);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Thongke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống Kê";
            this.Load += new System.EventHandler(this.thongketheothang_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rdTonggiatri;
        private System.Windows.Forms.RadioButton rdallgia;
        private System.Windows.Forms.TextBox txtGiaKetThuc;
        private System.Windows.Forms.TextBox txtGiaBanDau;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}