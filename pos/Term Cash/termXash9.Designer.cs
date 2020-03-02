namespace pos
{
    partial class termXash9
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.chequeCodeNo = new System.Windows.Forms.TextBox();
            this.chequeNo = new System.Windows.Forms.TextBox();
            this.chequeAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.comboChequePayment = new System.Windows.Forms.ToolStripComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 185);
            this.tabControl1.TabIndex = 61;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.chequeCodeNo);
            this.tabPage2.Controls.Add(this.chequeNo);
            this.tabPage2.Controls.Add(this.chequeAmount);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.dateTimePicker1);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(484, 159);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "             CHEQUE                 ";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(403, 112);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 71;
            this.button3.Text = "SAVE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chequeCodeNo
            // 
            this.chequeCodeNo.AllowDrop = true;
            this.chequeCodeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequeCodeNo.ForeColor = System.Drawing.Color.Red;
            this.chequeCodeNo.Location = new System.Drawing.Point(374, 35);
            this.chequeCodeNo.Name = "chequeCodeNo";
            this.chequeCodeNo.Size = new System.Drawing.Size(104, 20);
            this.chequeCodeNo.TabIndex = 86;
            this.chequeCodeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chequeCodeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chequeCodeNo_KeyDown);
            // 
            // chequeNo
            // 
            this.chequeNo.AllowDrop = true;
            this.chequeNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequeNo.ForeColor = System.Drawing.Color.Red;
            this.chequeNo.Location = new System.Drawing.Point(142, 30);
            this.chequeNo.Name = "chequeNo";
            this.chequeNo.Size = new System.Drawing.Size(104, 20);
            this.chequeNo.TabIndex = 84;
            this.chequeNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chequeNo_KeyDown);
            // 
            // chequeAmount
            // 
            this.chequeAmount.AllowDrop = true;
            this.chequeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequeAmount.ForeColor = System.Drawing.Color.Red;
            this.chequeAmount.Location = new System.Drawing.Point(142, 6);
            this.chequeAmount.Name = "chequeAmount";
            this.chequeAmount.Size = new System.Drawing.Size(104, 20);
            this.chequeAmount.TabIndex = 78;
            this.chequeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chequeAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chequeBalance_KeyDown);
            this.chequeAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chequeBalance_KeyPress);
            this.chequeAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.chequeBalance_KeyUp);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Purple;
            this.label19.Location = new System.Drawing.Point(252, 35);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(116, 15);
            this.label19.TabIndex = 85;
            this.label19.Text = "CHEQUE CODE NO";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Purple;
            this.label16.Location = new System.Drawing.Point(6, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 15);
            this.label16.TabIndex = 83;
            this.label16.Text = "CHEQUE NO";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(142, 54);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(191, 20);
            this.dateTimePicker1.TabIndex = 82;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Purple;
            this.label13.Location = new System.Drawing.Point(6, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 15);
            this.label13.TabIndex = 81;
            this.label13.Text = "CHEQUE DATE";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Purple;
            this.label12.Location = new System.Drawing.Point(6, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 15);
            this.label12.TabIndex = 77;
            this.label12.Text = "CHEQUE AMOUNT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboChequePayment});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 27);
            this.menuStrip1.TabIndex = 72;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // comboChequePayment
            // 
            this.comboChequePayment.Name = "comboChequePayment";
            this.comboChequePayment.Size = new System.Drawing.Size(350, 23);
            // 
            // termXash9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 244);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "termXash9";
            this.Text = "Payment Historey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.termXash_FormClosing);
            this.Load += new System.EventHandler(this.termXash_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox chequeCodeNo;
        private System.Windows.Forms.TextBox chequeNo;
        private System.Windows.Forms.TextBox chequeAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox comboChequePayment;
    }
}