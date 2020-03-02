namespace pos
{
    partial class LoanAdvanced
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
            this.button22 = new System.Windows.Forms.Button();
            this.loanTable = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noOfInstallment = new System.Windows.Forms.TextBox();
            this.loanAmount = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.left = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboLoanType = new System.Windows.Forms.ComboBox();
            this.paid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loanTable)).BeginInit();
            this.SuspendLayout();
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(598, 149);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(260, 23);
            this.button22.TabIndex = 58;
            this.button22.Text = "SAVE LOAN DETAILS";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // loanTable
            // 
            this.loanTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loanTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.Column3});
            this.loanTable.Location = new System.Drawing.Point(264, 188);
            this.loanTable.Name = "loanTable";
            this.loanTable.Size = new System.Drawing.Size(594, 240);
            this.loanTable.TabIndex = 56;
            this.loanTable.TabStop = false;
            this.loanTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.loanTable_CellClick);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "id";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Type";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Month";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Loan Amount";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "No Of Install.";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Paid";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // noOfInstallment
            // 
            this.noOfInstallment.Location = new System.Drawing.Point(747, 97);
            this.noOfInstallment.Name = "noOfInstallment";
            this.noOfInstallment.Size = new System.Drawing.Size(111, 20);
            this.noOfInstallment.TabIndex = 55;
            this.noOfInstallment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.noOfInstallment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noOfInstallment_KeyDown);
            this.noOfInstallment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loanAmount_KeyPress_1);
            // 
            // loanAmount
            // 
            this.loanAmount.Location = new System.Drawing.Point(747, 71);
            this.loanAmount.Name = "loanAmount";
            this.loanAmount.Size = new System.Drawing.Size(111, 20);
            this.loanAmount.TabIndex = 53;
            this.loanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.loanAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loanAmount_KeyDown);
            this.loanAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loanAmount_KeyPress_1);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(261, 95);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(123, 18);
            this.label34.TabIndex = 54;
            this.label34.Text = "No of Installment ";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(261, 68);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(139, 18);
            this.label35.TabIndex = 52;
            this.label35.Text = "Loan Amount (RS .)";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.comboBox2.Location = new System.Drawing.Point(700, 14);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(158, 24);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.DropDown += new System.EventHandler(this.comboBox2_DropDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(598, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(96, 22);
            this.dateTimePicker1.TabIndex = 15;
            this.dateTimePicker1.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 62;
            this.label3.Text = "Loan Type";
            // 
            // left
            // 
            this.left.FormattingEnabled = true;
            this.left.Location = new System.Drawing.Point(12, 23);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(227, 407);
            this.left.TabIndex = 63;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(261, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 18);
            this.label4.TabIndex = 65;
            this.label4.Text = "Loan Apply From";
            // 
            // comboLoanType
            // 
            this.comboLoanType.FormattingEnabled = true;
            this.comboLoanType.Items.AddRange(new object[] {
            "WelFare",
            "Easy Pay1",
            "Easy Pay2",
            "Easy Pay3",
            "Other"});
            this.comboLoanType.Location = new System.Drawing.Point(700, 44);
            this.comboLoanType.Name = "comboLoanType";
            this.comboLoanType.Size = new System.Drawing.Size(158, 21);
            this.comboLoanType.TabIndex = 68;
            // 
            // paid
            // 
            this.paid.Location = new System.Drawing.Point(747, 123);
            this.paid.Name = "paid";
            this.paid.Size = new System.Drawing.Size(111, 20);
            this.paid.TabIndex = 70;
            this.paid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paid.Visible = false;
            this.paid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.paid_KeyDown);
            this.paid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.paid_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(261, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 18);
            this.label1.TabIndex = 69;
            this.label1.Text = "Paid ";
            this.label1.Visible = false;
            // 
            // LoanAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 437);
            this.Controls.Add(this.paid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboLoanType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.left);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button22);
            this.Controls.Add(this.loanTable);
            this.Controls.Add(this.noOfInstallment);
            this.Controls.Add(this.loanAmount);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBox2);
            this.Name = "LoanAdvanced";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loan Advanced";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoanAdvanced_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoanAdvanced_FormClosed);
            this.Load += new System.EventHandler(this.LoanAdvanced_Load);
            this.Click += new System.EventHandler(this.LoanAdvanced_Click);
            ((System.ComponentModel.ISupportInitialize)(this.loanTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.DataGridView loanTable;
        private System.Windows.Forms.TextBox noOfInstallment;
        private System.Windows.Forms.TextBox loanAmount;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox left;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboLoanType;
        private System.Windows.Forms.TextBox paid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}