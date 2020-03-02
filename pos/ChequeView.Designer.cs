namespace pos
{
    partial class ChequeView
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
            this.label1 = new System.Windows.Forms.Label();
            this.amountByNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LOAD = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.payName = new System.Windows.Forms.TextBox();
            this.radioAcPay = new System.Windows.Forms.RadioButton();
            this.radioCash = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chqueNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bankName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.branch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.acNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelSend = new System.Windows.Forms.Panel();
            this.crossed = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.radioDeposit = new System.Windows.Forms.RadioButton();
            this.radioRecivd = new System.Windows.Forms.RadioButton();
            this.radioSend = new System.Windows.Forms.RadioButton();
            this.recepitNo2 = new System.Windows.Forms.TextBox();
            this.cashCheque = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panelSend.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "PAY NAME";
            // 
            // amountByNumber
            // 
            this.amountByNumber.Location = new System.Drawing.Point(131, 95);
            this.amountByNumber.Name = "amountByNumber";
            this.amountByNumber.Size = new System.Drawing.Size(131, 20);
            this.amountByNumber.TabIndex = 42;
            this.amountByNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.amountByNumber_KeyDown);
            this.amountByNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amountByNumber_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "AMOUNT";
            // 
            // LOAD
            // 
            this.LOAD.Location = new System.Drawing.Point(488, 169);
            this.LOAD.Name = "LOAD";
            this.LOAD.Size = new System.Drawing.Size(75, 23);
            this.LOAD.TabIndex = 46;
            this.LOAD.Text = "PRINT";
            this.LOAD.UseVisualStyleBackColor = true;
            this.LOAD.Click += new System.EventHandler(this.LOAD_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1194, -186);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(26, 20);
            this.textBox2.TabIndex = 48;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(131, 121);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(179, 20);
            this.dateTimePicker1.TabIndex = 59;
            // 
            // payName
            // 
            this.payName.Location = new System.Drawing.Point(128, 49);
            this.payName.Name = "payName";
            this.payName.Size = new System.Drawing.Size(413, 20);
            this.payName.TabIndex = 64;
            this.payName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.payName_KeyDown);
            this.payName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.payName_KeyUp);
            // 
            // radioAcPay
            // 
            this.radioAcPay.AutoSize = true;
            this.radioAcPay.Checked = true;
            this.radioAcPay.Location = new System.Drawing.Point(10, 3);
            this.radioAcPay.Name = "radioAcPay";
            this.radioAcPay.Size = new System.Drawing.Size(66, 17);
            this.radioAcPay.TabIndex = 66;
            this.radioAcPay.TabStop = true;
            this.radioAcPay.Text = "AC -PAY";
            this.radioAcPay.UseVisualStyleBackColor = true;
            this.radioAcPay.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioCash
            // 
            this.radioCash.AutoSize = true;
            this.radioCash.Location = new System.Drawing.Point(82, 3);
            this.radioCash.Name = "radioCash";
            this.radioCash.Size = new System.Drawing.Size(54, 17);
            this.radioCash.TabIndex = 67;
            this.radioCash.Text = "CASH";
            this.radioCash.UseVisualStyleBackColor = true;
            this.radioCash.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "CHEQUE NUMBER";
            this.label3.Visible = false;
            // 
            // chqueNumber
            // 
            this.chqueNumber.Location = new System.Drawing.Point(140, 407);
            this.chqueNumber.Name = "chqueNumber";
            this.chqueNumber.Size = new System.Drawing.Size(131, 20);
            this.chqueNumber.TabIndex = 69;
            this.chqueNumber.Visible = false;
            this.chqueNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chqueNumber_KeyDown);
            this.chqueNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "SEND BANK NAME";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "DATE";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(140, 380);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(414, 21);
            this.comboBox1.TabIndex = 72;
            this.comboBox1.Visible = false;
            // 
            // bankName
            // 
            this.bankName.Location = new System.Drawing.Point(138, 12);
            this.bankName.Name = "bankName";
            this.bankName.Size = new System.Drawing.Size(414, 20);
            this.bankName.TabIndex = 73;
            this.bankName.Visible = false;
            this.bankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bankName_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "BANK NAME";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "BRANCH";
            this.label7.Visible = false;
            // 
            // branch
            // 
            this.branch.Location = new System.Drawing.Point(137, 38);
            this.branch.Name = "branch";
            this.branch.Size = new System.Drawing.Size(131, 20);
            this.branch.TabIndex = 76;
            this.branch.Visible = false;
            this.branch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.branch_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 77;
            this.label8.Text = "A/C NO";
            this.label8.Visible = false;
            // 
            // acNo
            // 
            this.acNo.Location = new System.Drawing.Point(137, 64);
            this.acNo.Name = "acNo";
            this.acNo.Size = new System.Drawing.Size(131, 20);
            this.acNo.TabIndex = 78;
            this.acNo.Visible = false;
            this.acNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.acNo_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelSend);
            this.panel1.Controls.Add(this.amountByNumber);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 151);
            this.panel1.TabIndex = 79;
            // 
            // panelSend
            // 
            this.panelSend.Controls.Add(this.crossed);
            this.panelSend.Controls.Add(this.listBox1);
            this.panelSend.Controls.Add(this.radioAcPay);
            this.panelSend.Controls.Add(this.radioCash);
            this.panelSend.Controls.Add(this.label1);
            this.panelSend.Controls.Add(this.payName);
            this.panelSend.Enabled = false;
            this.panelSend.Location = new System.Drawing.Point(3, 3);
            this.panelSend.Name = "panelSend";
            this.panelSend.Size = new System.Drawing.Size(557, 86);
            this.panelSend.TabIndex = 83;
            this.panelSend.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSend_Paint);
            // 
            // crossed
            // 
            this.crossed.AutoSize = true;
            this.crossed.Enabled = false;
            this.crossed.Location = new System.Drawing.Point(82, 26);
            this.crossed.Name = "crossed";
            this.crossed.Size = new System.Drawing.Size(63, 17);
            this.crossed.TabIndex = 69;
            this.crossed.Text = "CROSS";
            this.crossed.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(536, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(10, 4);
            this.listBox1.TabIndex = 68;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // radioDeposit
            // 
            this.radioDeposit.AutoSize = true;
            this.radioDeposit.Location = new System.Drawing.Point(178, 41);
            this.radioDeposit.Name = "radioDeposit";
            this.radioDeposit.Size = new System.Drawing.Size(72, 17);
            this.radioDeposit.TabIndex = 82;
            this.radioDeposit.Text = "DEPOSIT";
            this.radioDeposit.UseVisualStyleBackColor = true;
            this.radioDeposit.Visible = false;
            this.radioDeposit.CheckedChanged += new System.EventHandler(this.radioDeposit_CheckedChanged);
            // 
            // radioRecivd
            // 
            this.radioRecivd.AutoSize = true;
            this.radioRecivd.Checked = true;
            this.radioRecivd.Location = new System.Drawing.Point(131, 33);
            this.radioRecivd.Name = "radioRecivd";
            this.radioRecivd.Size = new System.Drawing.Size(72, 17);
            this.radioRecivd.TabIndex = 81;
            this.radioRecivd.TabStop = true;
            this.radioRecivd.Text = "RECIVED";
            this.radioRecivd.UseVisualStyleBackColor = true;
            this.radioRecivd.Visible = false;
            this.radioRecivd.CheckedChanged += new System.EventHandler(this.radioRecivd_CheckedChanged);
            // 
            // radioSend
            // 
            this.radioSend.AutoSize = true;
            this.radioSend.Location = new System.Drawing.Point(108, 37);
            this.radioSend.Name = "radioSend";
            this.radioSend.Size = new System.Drawing.Size(55, 17);
            this.radioSend.TabIndex = 80;
            this.radioSend.Text = "SEND";
            this.radioSend.UseVisualStyleBackColor = true;
            this.radioSend.Visible = false;
            this.radioSend.CheckedChanged += new System.EventHandler(this.radioSend_CheckedChanged);
            // 
            // recepitNo2
            // 
            this.recepitNo2.Location = new System.Drawing.Point(542, 38);
            this.recepitNo2.Name = "recepitNo2";
            this.recepitNo2.Size = new System.Drawing.Size(10, 20);
            this.recepitNo2.TabIndex = 80;
            this.recepitNo2.Visible = false;
            // 
            // cashCheque
            // 
            this.cashCheque.Location = new System.Drawing.Point(525, 38);
            this.cashCheque.Name = "cashCheque";
            this.cashCheque.Size = new System.Drawing.Size(11, 20);
            this.cashCheque.TabIndex = 81;
            this.cashCheque.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 17);
            this.radioButton1.TabIndex = 83;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "PABC";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(62, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(48, 17);
            this.radioButton2.TabIndex = 84;
            this.radioButton2.Text = "HNB";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Location = new System.Drawing.Point(3, 168);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 24);
            this.panel2.TabIndex = 85;
            // 
            // ChequeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 194);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.radioDeposit);
            this.Controls.Add(this.radioSend);
            this.Controls.Add(this.radioRecivd);
            this.Controls.Add(this.cashCheque);
            this.Controls.Add(this.recepitNo2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.acNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.branch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bankName);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.chqueNumber);
            this.Controls.Add(this.LOAD);
            this.Name = "ChequeView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheque Print";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChequeView_FormClosing);
            this.Load += new System.EventHandler(this.ChequeView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelSend.ResumeLayout(false);
            this.panelSend.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox amountByNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LOAD;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox payName;
        private System.Windows.Forms.RadioButton radioAcPay;
        private System.Windows.Forms.RadioButton radioCash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox chqueNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox bankName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox branch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox acNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioSend;
        private System.Windows.Forms.RadioButton radioRecivd;
        private System.Windows.Forms.RadioButton radioDeposit;
        private System.Windows.Forms.Panel panelSend;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox recepitNo2;
        private System.Windows.Forms.TextBox cashCheque;
        private System.Windows.Forms.CheckBox crossed;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel2;
    }
}