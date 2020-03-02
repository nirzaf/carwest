namespace pos
{
    partial class bankSummery
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioRecevied = new System.Windows.Forms.RadioButton();
            this.radioSend = new System.Windows.Forms.RadioButton();
            this.radioDeposit = new System.Windows.Forms.RadioButton();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.recivdTotal = new System.Windows.Forms.TextBox();
            this.sendTotal = new System.Windows.Forms.TextBox();
            this.depositTotal = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chequNumber = new System.Windows.Forms.TextBox();
            this.amount = new System.Windows.Forms.TextBox();
            this.chequeDate = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.customer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mobileNumber = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(59, 74);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 75;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(59, 100);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 76;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 77;
            this.label2.Text = "FROM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 18);
            this.label3.TabIndex = 78;
            this.label3.Text = "TO";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(276, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 26);
            this.button1.TabIndex = 79;
            this.button1.Text = "LOAD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(1, 210);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1234, 323);
            this.dataGridView1.TabIndex = 80;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "DATE";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "NAME";
            this.Column7.Name = "Column7";
            this.Column7.Width = 200;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "ADDRESS";
            this.Column8.Name = "Column8";
            this.Column8.Width = 150;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "MOBILE NO";
            this.Column9.Name = "Column9";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "BANK NAME";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CHEQUE NO";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "RECEVIED AMOUNT";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "SEND AMOUNT";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 130;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "DEPOSIT AMOUNT";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 130;
            // 
            // radioRecevied
            // 
            this.radioRecevied.AutoSize = true;
            this.radioRecevied.Location = new System.Drawing.Point(6, 12);
            this.radioRecevied.Name = "radioRecevied";
            this.radioRecevied.Size = new System.Drawing.Size(79, 17);
            this.radioRecevied.TabIndex = 81;
            this.radioRecevied.Text = "RECEVIED";
            this.radioRecevied.UseVisualStyleBackColor = true;
            // 
            // radioSend
            // 
            this.radioSend.AutoSize = true;
            this.radioSend.Location = new System.Drawing.Point(91, 12);
            this.radioSend.Name = "radioSend";
            this.radioSend.Size = new System.Drawing.Size(55, 17);
            this.radioSend.TabIndex = 82;
            this.radioSend.Text = "SEND";
            this.radioSend.UseVisualStyleBackColor = true;
            // 
            // radioDeposit
            // 
            this.radioDeposit.AutoSize = true;
            this.radioDeposit.Location = new System.Drawing.Point(152, 12);
            this.radioDeposit.Name = "radioDeposit";
            this.radioDeposit.Size = new System.Drawing.Size(72, 17);
            this.radioDeposit.TabIndex = 83;
            this.radioDeposit.Text = "DEPOSIT";
            this.radioDeposit.UseVisualStyleBackColor = true;
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Checked = true;
            this.radioAll.Location = new System.Drawing.Point(230, 12);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(44, 17);
            this.radioAll.TabIndex = 84;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "ALL";
            this.radioAll.UseVisualStyleBackColor = true;
            // 
            // recivdTotal
            // 
            this.recivdTotal.Enabled = false;
            this.recivdTotal.ForeColor = System.Drawing.Color.Red;
            this.recivdTotal.Location = new System.Drawing.Point(802, 539);
            this.recivdTotal.Name = "recivdTotal";
            this.recivdTotal.Size = new System.Drawing.Size(136, 20);
            this.recivdTotal.TabIndex = 85;
            // 
            // sendTotal
            // 
            this.sendTotal.Enabled = false;
            this.sendTotal.ForeColor = System.Drawing.Color.Red;
            this.sendTotal.Location = new System.Drawing.Point(944, 539);
            this.sendTotal.Name = "sendTotal";
            this.sendTotal.Size = new System.Drawing.Size(136, 20);
            this.sendTotal.TabIndex = 86;
            // 
            // depositTotal
            // 
            this.depositTotal.Enabled = false;
            this.depositTotal.ForeColor = System.Drawing.Color.Red;
            this.depositTotal.Location = new System.Drawing.Point(1086, 539);
            this.depositTotal.Name = "depositTotal";
            this.depositTotal.Size = new System.Drawing.Size(129, 20);
            this.depositTotal.TabIndex = 87;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(254, 21);
            this.comboBox1.TabIndex = 88;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(409, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "CHEQUE NUMBER";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(409, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "AMOUNT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(409, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 91;
            this.label5.Text = "CHEQUE DATE";
            // 
            // chequNumber
            // 
            this.chequNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequNumber.Location = new System.Drawing.Point(596, 95);
            this.chequNumber.Name = "chequNumber";
            this.chequNumber.Size = new System.Drawing.Size(147, 20);
            this.chequNumber.TabIndex = 92;
            this.chequNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chequNumber_KeyDown);
            // 
            // amount
            // 
            this.amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amount.Location = new System.Drawing.Point(596, 121);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(147, 20);
            this.amount.TabIndex = 93;
            // 
            // chequeDate
            // 
            this.chequeDate.AccessibleDescription = "e";
            this.chequeDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chequeDate.Location = new System.Drawing.Point(543, 147);
            this.chequeDate.Name = "chequeDate";
            this.chequeDate.Size = new System.Drawing.Size(200, 20);
            this.chequeDate.TabIndex = 94;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(631, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 26);
            this.button2.TabIndex = 95;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(598, 33);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(10, 4);
            this.listBox2.TabIndex = 102;
            this.listBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox2_MouseClick);
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.listBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox2_KeyDown);
            // 
            // customer
            // 
            this.customer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customer.Location = new System.Drawing.Point(524, 7);
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(219, 20);
            this.customer.TabIndex = 97;
            this.customer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.customer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customer_KeyDown);
            this.customer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(409, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 96;
            this.label8.Text = "NAME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(410, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 100;
            this.label6.Text = "ADDRESS";
            // 
            // address
            // 
            this.address.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address.Location = new System.Drawing.Point(616, 33);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(127, 20);
            this.address.TabIndex = 98;
            this.address.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.address_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(409, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 13);
            this.label15.TabIndex = 101;
            this.label15.Text = "MOBILE NUMBER";
            // 
            // mobileNumber
            // 
            this.mobileNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mobileNumber.Location = new System.Drawing.Point(616, 56);
            this.mobileNumber.Name = "mobileNumber";
            this.mobileNumber.Size = new System.Drawing.Size(127, 20);
            this.mobileNumber.TabIndex = 99;
            this.mobileNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mobileNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mobileNumber_KeyDown);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1125, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 23);
            this.button3.TabIndex = 103;
            this.button3.Text = "CHEQUE DEPOSIT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // bankSummery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 565);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.customer);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.address);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.mobileNumber);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chequeDate);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.chequNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.depositTotal);
            this.Controls.Add(this.sendTotal);
            this.Controls.Add(this.recivdTotal);
            this.Controls.Add(this.radioAll);
            this.Controls.Add(this.radioDeposit);
            this.Controls.Add(this.radioSend);
            this.Controls.Add(this.radioRecevied);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "bankSummery";
            this.Text = "BANK SUMMERY";
            this.Load += new System.EventHandler(this.bankStatement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton radioRecevied;
        private System.Windows.Forms.RadioButton radioSend;
        private System.Windows.Forms.RadioButton radioDeposit;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.TextBox recivdTotal;
        private System.Windows.Forms.TextBox sendTotal;
        private System.Windows.Forms.TextBox depositTotal;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox chequNumber;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.DateTimePicker chequeDate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox customer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox mobileNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button button3;
    }
}