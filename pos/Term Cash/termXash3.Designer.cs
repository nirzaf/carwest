﻿namespace pos
{
    partial class termXash3
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.creditPeriod = new System.Windows.Forms.NumericUpDown();
            this.creditAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button10 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button9 = new System.Windows.Forms.Button();
            this.chequeCodeNo = new System.Windows.Forms.TextBox();
            this.chequeNo = new System.Windows.Forms.TextBox();
            this.chequeAmount = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameonCard = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.ccv = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cardNo = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cardAmount = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cashPaid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.invoiceAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.balance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sAVEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.creditPeriod)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(11, 116);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 273);
            this.tabControl1.TabIndex = 61;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.creditPeriod);
            this.tabPage1.Controls.Add(this.creditAmount);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(484, 247);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "              CREDIT                   ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // creditPeriod
            // 
            this.creditPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditPeriod.Location = new System.Drawing.Point(433, 8);
            this.creditPeriod.Name = "creditPeriod";
            this.creditPeriod.Size = new System.Drawing.Size(45, 21);
            this.creditPeriod.TabIndex = 64;
            this.creditPeriod.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // creditAmount
            // 
            this.creditAmount.AllowDrop = true;
            this.creditAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditAmount.ForeColor = System.Drawing.Color.Red;
            this.creditAmount.Location = new System.Drawing.Point(374, 42);
            this.creditAmount.Name = "creditAmount";
            this.creditAmount.Size = new System.Drawing.Size(104, 22);
            this.creditAmount.TabIndex = 62;
            this.creditAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.creditAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.creditBalance_KeyDown);
            this.creditAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.creditBalance_KeyPress);
            this.creditAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.creditBalance_KeyUp);
            this.creditAmount.Layout += new System.Windows.Forms.LayoutEventHandler(this.creditBalance_Layout);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Purple;
            this.label9.Location = new System.Drawing.Point(6, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 16);
            this.label9.TabIndex = 63;
            this.label9.Text = "CREDIT PERIOD (DAYS)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Purple;
            this.label8.Location = new System.Drawing.Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 16);
            this.label8.TabIndex = 61;
            this.label8.Text = "CREDIT AMOUNT (RS.)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button10);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.button9);
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
            this.tabPage2.Size = new System.Drawing.Size(484, 247);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "             CHEQUE                 ";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 224);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(133, 23);
            this.button10.TabIndex = 92;
            this.button10.Text = "REMOVE CHEQUE";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column13,
            this.Column15});
            this.dataGridView2.Location = new System.Drawing.Point(12, 85);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(466, 135);
            this.dataGridView2.TabIndex = 91;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "CHEQUE AMOUNT";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "CHEQUE NO";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "CHEQUE CODE NO";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "CHEQUE DATE";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 120;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(403, 60);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 90;
            this.button9.Text = "ADD";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.nameonCard);
            this.tabPage3.Controls.Add(this.label29);
            this.tabPage3.Controls.Add(this.ccv);
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.cardNo);
            this.tabPage3.Controls.Add(this.label27);
            this.tabPage3.Controls.Add(this.cardAmount);
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(484, 247);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "CREDIT CARD";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(398, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 94;
            this.button2.Text = "ADD";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 93;
            this.button1.Text = "REMOVE CARD DETAIL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(9, 97);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(465, 116);
            this.dataGridView1.TabIndex = 78;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "AMOUNT";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CARD NO";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CCV";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 30;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "NAME ON CARD";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 200;
            // 
            // nameonCard
            // 
            this.nameonCard.AllowDrop = true;
            this.nameonCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameonCard.ForeColor = System.Drawing.Color.Red;
            this.nameonCard.Location = new System.Drawing.Point(137, 65);
            this.nameonCard.Name = "nameonCard";
            this.nameonCard.Size = new System.Drawing.Size(240, 22);
            this.nameonCard.TabIndex = 77;
            this.nameonCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nameonCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameonCard_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Purple;
            this.label29.Location = new System.Drawing.Point(8, 65);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(123, 16);
            this.label29.TabIndex = 76;
            this.label29.Text = "NAME ON CARD";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ccv
            // 
            this.ccv.AllowDrop = true;
            this.ccv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ccv.ForeColor = System.Drawing.Color.Red;
            this.ccv.Location = new System.Drawing.Point(420, 39);
            this.ccv.Name = "ccv";
            this.ccv.Size = new System.Drawing.Size(53, 22);
            this.ccv.TabIndex = 75;
            this.ccv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ccv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ccv_KeyDown);
            this.ccv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cardNo_KeyPress);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Purple;
            this.label28.Location = new System.Drawing.Point(380, 39);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 16);
            this.label28.TabIndex = 74;
            this.label28.Text = "CCV";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardNo
            // 
            this.cardNo.AllowDrop = true;
            this.cardNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardNo.ForeColor = System.Drawing.Color.Red;
            this.cardNo.Location = new System.Drawing.Point(137, 39);
            this.cardNo.Name = "cardNo";
            this.cardNo.Size = new System.Drawing.Size(240, 22);
            this.cardNo.TabIndex = 73;
            this.cardNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cardNo_KeyDown);
            this.cardNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cardNo_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Purple;
            this.label27.Location = new System.Drawing.Point(8, 45);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 16);
            this.label27.TabIndex = 72;
            this.label27.Text = "CARD NO";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardAmount
            // 
            this.cardAmount.AllowDrop = true;
            this.cardAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cardAmount.ForeColor = System.Drawing.Color.Red;
            this.cardAmount.Location = new System.Drawing.Point(273, 12);
            this.cardAmount.Name = "cardAmount";
            this.cardAmount.Size = new System.Drawing.Size(104, 22);
            this.cardAmount.TabIndex = 68;
            this.cardAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cardAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cardAmount_KeyDown);
            this.cardAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cardNo_KeyPress);
            this.cardAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cardAmount_KeyUp);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Purple;
            this.label26.Location = new System.Drawing.Point(8, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(158, 16);
            this.label26.TabIndex = 67;
            this.label26.Text = "CARD AMOUNT (RS.)";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cashPaid
            // 
            this.cashPaid.AllowDrop = true;
            this.cashPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashPaid.ForeColor = System.Drawing.Color.Red;
            this.cashPaid.Location = new System.Drawing.Point(396, 55);
            this.cashPaid.Name = "cashPaid";
            this.cashPaid.Size = new System.Drawing.Size(104, 22);
            this.cashPaid.TabIndex = 66;
            this.cashPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cashPaid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.creditPaid_KeyDown);
            this.cashPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.creditPaid_KeyPress);
            this.cashPaid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.creditPaid_KeyUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Purple;
            this.label10.Location = new System.Drawing.Point(8, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 16);
            this.label10.TabIndex = 65;
            this.label10.Text = "CASH PAID (RS.)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // invoiceAmount
            // 
            this.invoiceAmount.AllowDrop = true;
            this.invoiceAmount.Enabled = false;
            this.invoiceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceAmount.ForeColor = System.Drawing.Color.Red;
            this.invoiceAmount.Location = new System.Drawing.Point(396, 24);
            this.invoiceAmount.Name = "invoiceAmount";
            this.invoiceAmount.Size = new System.Drawing.Size(104, 22);
            this.invoiceAmount.TabIndex = 68;
            this.invoiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Purple;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "INVOICE AMOUNT (RS.)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // balance
            // 
            this.balance.AllowDrop = true;
            this.balance.Enabled = false;
            this.balance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.balance.ForeColor = System.Drawing.Color.Red;
            this.balance.Location = new System.Drawing.Point(396, 87);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(104, 22);
            this.balance.TabIndex = 70;
            this.balance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Purple;
            this.label2.Location = new System.Drawing.Point(8, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 16);
            this.label2.TabIndex = 69;
            this.label2.Text = "BALANCE (RS.)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(428, 392);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 71;
            this.button3.Text = "SAVE";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sAVEToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 72;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sAVEToolStripMenuItem
            // 
            this.sAVEToolStripMenuItem.Name = "sAVEToolStripMenuItem";
            this.sAVEToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.sAVEToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.sAVEToolStripMenuItem.Text = "SAVE";
            this.sAVEToolStripMenuItem.Click += new System.EventHandler(this.sAVEToolStripMenuItem_Click);
            // 
            // termXash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 427);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.balance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.invoiceAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cashPaid);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "termXash";
            this.Text = "Payment Historey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.termXash_FormClosing);
            this.Load += new System.EventHandler(this.termXash_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.creditPeriod)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown creditPeriod;
        private System.Windows.Forms.TextBox cashPaid;
        private System.Windows.Forms.TextBox creditAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox chequeCodeNo;
        private System.Windows.Forms.TextBox chequeNo;
        private System.Windows.Forms.TextBox chequeAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox nameonCard;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox ccv;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox cardNo;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox cardAmount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox invoiceAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sAVEToolStripMenuItem;
    }
}