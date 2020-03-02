namespace pos
{
    partial class stockReport6
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchALL = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkQty = new System.Windows.Forms.CheckBox();
            this.radioMinValue = new System.Windows.Forms.RadioButton();
            this.radioMaxValue = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.maxValue = new System.Windows.Forms.TextBox();
            this.minValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.descriptionName = new System.Windows.Forms.TextBox();
            this.checkDescription = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.categoryName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.brandName = new System.Windows.Forms.TextBox();
            this.checkCategory = new System.Windows.Forms.CheckBox();
            this.checkBrand = new System.Windows.Forms.CheckBox();
            this.radioAdvancedSearch = new System.Windows.Forms.RadioButton();
            this.radioSearchByDate = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.itemCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.TextBox();
            this.panelStockValue = new System.Windows.Forms.Panel();
            this.stockValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.totStockValue = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelStockValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleName = "sasas";
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.searchALL);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.maxValue);
            this.panel1.Controls.Add(this.minValue);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.descriptionName);
            this.panel1.Controls.Add(this.checkDescription);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.categoryName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.brandName);
            this.panel1.Controls.Add(this.checkCategory);
            this.panel1.Controls.Add(this.checkBrand);
            this.panel1.Controls.Add(this.radioAdvancedSearch);
            this.panel1.Controls.Add(this.radioSearchByDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.itemCode);
            this.panel1.Location = new System.Drawing.Point(12, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 10);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "";
            // 
            // searchALL
            // 
            this.searchALL.AutoSize = true;
            this.searchALL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.searchALL.Location = new System.Drawing.Point(6, 10);
            this.searchALL.Name = "searchALL";
            this.searchALL.Size = new System.Drawing.Size(73, 17);
            this.searchALL.TabIndex = 22;
            this.searchALL.TabStop = true;
            this.searchALL.Text = "Search All";
            this.searchALL.UseVisualStyleBackColor = true;
            this.searchALL.CheckedChanged += new System.EventHandler(this.searchALL_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkQty);
            this.groupBox2.Controls.Add(this.radioMinValue);
            this.groupBox2.Controls.Add(this.radioMaxValue);
            this.groupBox2.Location = new System.Drawing.Point(27, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 82);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // checkQty
            // 
            this.checkQty.AutoSize = true;
            this.checkQty.Location = new System.Drawing.Point(0, 4);
            this.checkQty.Name = "checkQty";
            this.checkQty.Size = new System.Drawing.Size(48, 17);
            this.checkQty.TabIndex = 13;
            this.checkQty.Text = "QTY";
            this.checkQty.UseVisualStyleBackColor = true;
            this.checkQty.CheckedChanged += new System.EventHandler(this.checkQty_CheckedChanged);
            this.checkQty.CheckStateChanged += new System.EventHandler(this.checkQty_CheckStateChanged);
            // 
            // radioMinValue
            // 
            this.radioMinValue.AutoSize = true;
            this.radioMinValue.Location = new System.Drawing.Point(19, 27);
            this.radioMinValue.Name = "radioMinValue";
            this.radioMinValue.Size = new System.Drawing.Size(83, 17);
            this.radioMinValue.TabIndex = 14;
            this.radioMinValue.TabStop = true;
            this.radioMinValue.Text = "MIN VALUE";
            this.radioMinValue.UseVisualStyleBackColor = true;
            this.radioMinValue.CheckedChanged += new System.EventHandler(this.radioMinValue_CheckedChanged);
            // 
            // radioMaxValue
            // 
            this.radioMaxValue.AutoSize = true;
            this.radioMaxValue.Location = new System.Drawing.Point(19, 55);
            this.radioMaxValue.Name = "radioMaxValue";
            this.radioMaxValue.Size = new System.Drawing.Size(86, 17);
            this.radioMaxValue.TabIndex = 15;
            this.radioMaxValue.TabStop = true;
            this.radioMaxValue.Text = "MAX VALUE";
            this.radioMaxValue.UseVisualStyleBackColor = true;
            this.radioMaxValue.CheckedChanged += new System.EventHandler(this.radioMaxValue_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(112, 607);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(1, 470);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 17);
            this.panel2.TabIndex = 18;
            // 
            // maxValue
            // 
            this.maxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxValue.Location = new System.Drawing.Point(265, 417);
            this.maxValue.Name = "maxValue";
            this.maxValue.Size = new System.Drawing.Size(79, 24);
            this.maxValue.TabIndex = 17;
            this.maxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brandName_KeyDown);
            // 
            // minValue
            // 
            this.minValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minValue.Location = new System.Drawing.Point(265, 387);
            this.minValue.Name = "minValue";
            this.minValue.Size = new System.Drawing.Size(79, 24);
            this.minValue.TabIndex = 16;
            this.minValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brandName_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "DESCRIPTION NAME";
            // 
            // descriptionName
            // 
            this.descriptionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionName.Location = new System.Drawing.Point(27, 324);
            this.descriptionName.Name = "descriptionName";
            this.descriptionName.Size = new System.Drawing.Size(317, 24);
            this.descriptionName.TabIndex = 11;
            this.descriptionName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brandName_KeyDown);
            // 
            // checkDescription
            // 
            this.checkDescription.AutoSize = true;
            this.checkDescription.Location = new System.Drawing.Point(27, 282);
            this.checkDescription.Name = "checkDescription";
            this.checkDescription.Size = new System.Drawing.Size(79, 17);
            this.checkDescription.TabIndex = 10;
            this.checkDescription.Text = "Description";
            this.checkDescription.UseVisualStyleBackColor = true;
            this.checkDescription.CheckedChanged += new System.EventHandler(this.checkDescription_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "CATEGORY NAME";
            // 
            // categoryName
            // 
            this.categoryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryName.Location = new System.Drawing.Point(27, 248);
            this.categoryName.Name = "categoryName";
            this.categoryName.Size = new System.Drawing.Size(317, 24);
            this.categoryName.TabIndex = 8;
            this.categoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brandName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "BRAND NAME";
            // 
            // brandName
            // 
            this.brandName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brandName.Location = new System.Drawing.Point(27, 172);
            this.brandName.Name = "brandName";
            this.brandName.Size = new System.Drawing.Size(317, 24);
            this.brandName.TabIndex = 6;
            this.brandName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.brandName_KeyDown);
            // 
            // checkCategory
            // 
            this.checkCategory.AutoSize = true;
            this.checkCategory.Location = new System.Drawing.Point(27, 206);
            this.checkCategory.Name = "checkCategory";
            this.checkCategory.Size = new System.Drawing.Size(68, 17);
            this.checkCategory.TabIndex = 5;
            this.checkCategory.Text = "Category";
            this.checkCategory.UseVisualStyleBackColor = true;
            this.checkCategory.CheckedChanged += new System.EventHandler(this.checkCategory_CheckedChanged);
            // 
            // checkBrand
            // 
            this.checkBrand.AutoSize = true;
            this.checkBrand.Location = new System.Drawing.Point(27, 128);
            this.checkBrand.Name = "checkBrand";
            this.checkBrand.Size = new System.Drawing.Size(54, 17);
            this.checkBrand.TabIndex = 4;
            this.checkBrand.Text = "Brand";
            this.checkBrand.UseVisualStyleBackColor = true;
            this.checkBrand.CheckedChanged += new System.EventHandler(this.checkBrand_CheckedChanged);
            // 
            // radioAdvancedSearch
            // 
            this.radioAdvancedSearch.AutoSize = true;
            this.radioAdvancedSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioAdvancedSearch.Location = new System.Drawing.Point(6, 94);
            this.radioAdvancedSearch.Name = "radioAdvancedSearch";
            this.radioAdvancedSearch.Size = new System.Drawing.Size(111, 17);
            this.radioAdvancedSearch.TabIndex = 3;
            this.radioAdvancedSearch.TabStop = true;
            this.radioAdvancedSearch.Text = "Advanced Search";
            this.radioAdvancedSearch.UseVisualStyleBackColor = true;
            this.radioAdvancedSearch.CheckedChanged += new System.EventHandler(this.radioAdvancedSearch_CheckedChanged);
            // 
            // radioSearchByDate
            // 
            this.radioSearchByDate.AutoSize = true;
            this.radioSearchByDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioSearchByDate.Location = new System.Drawing.Point(6, 36);
            this.radioSearchByDate.Name = "radioSearchByDate";
            this.radioSearchByDate.Size = new System.Drawing.Size(101, 17);
            this.radioSearchByDate.TabIndex = 2;
            this.radioSearchByDate.TabStop = true;
            this.radioSearchByDate.Text = "Search by Code";
            this.radioSearchByDate.UseVisualStyleBackColor = true;
            this.radioSearchByDate.CheckedChanged += new System.EventHandler(this.radioSearchByDate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "ITEM CODE";
            // 
            // itemCode
            // 
            this.itemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemCode.Location = new System.Drawing.Point(151, 61);
            this.itemCode.Name = "itemCode";
            this.itemCode.Size = new System.Drawing.Size(193, 24);
            this.itemCode.TabIndex = 0;
            this.itemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemCode_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 47);
            this.button1.TabIndex = 19;
            this.button1.Text = "LOAD ALL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.AccessibleName = "sasas";
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(12, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(632, 479);
            this.panel3.TabIndex = 20;
            this.panel3.Tag = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(13, 14);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(604, 398);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(665, 56);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(10, 24);
            this.name.TabIndex = 20;
            this.name.Visible = false;
            this.name.KeyUp += new System.Windows.Forms.KeyEventHandler(this.name_KeyUp);
            // 
            // panelStockValue
            // 
            this.panelStockValue.BackColor = System.Drawing.Color.Gainsboro;
            this.panelStockValue.Controls.Add(this.stockValue);
            this.panelStockValue.Controls.Add(this.label8);
            this.panelStockValue.Location = new System.Drawing.Point(12, 40);
            this.panelStockValue.Name = "panelStockValue";
            this.panelStockValue.Size = new System.Drawing.Size(10, 10);
            this.panelStockValue.TabIndex = 23;
            // 
            // stockValue
            // 
            this.stockValue.Enabled = false;
            this.stockValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockValue.ForeColor = System.Drawing.Color.Red;
            this.stockValue.Location = new System.Drawing.Point(6, 29);
            this.stockValue.Name = "stockValue";
            this.stockValue.Size = new System.Drawing.Size(236, 24);
            this.stockValue.TabIndex = 24;
            this.stockValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 18);
            this.label8.TabIndex = 24;
            this.label8.Text = "STOCK VALUE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 575);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "TOTAL STOCK VALUE";
            // 
            // totStockValue
            // 
            this.totStockValue.Enabled = false;
            this.totStockValue.Location = new System.Drawing.Point(739, 573);
            this.totStockValue.Margin = new System.Windows.Forms.Padding(2);
            this.totStockValue.Name = "totStockValue";
            this.totStockValue.Size = new System.Drawing.Size(176, 20);
            this.totStockValue.TabIndex = 25;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "INVOICE NO/ GRN NO";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SYSTEM DATE";
            this.Column6.Name = "Column6";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CHEQUE NO";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CHEQUE CODE NO";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "AMOUNT";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(444, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 26;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(156, 56);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 17);
            this.radioButton1.TabIndex = 27;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "CUSTOMER";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(248, 56);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(78, 17);
            this.radioButton2.TabIndex = 28;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "SUPPLIER";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(506, 420);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 20);
            this.textBox1.TabIndex = 1;
            // 
            // stockReport6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 539);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.totStockValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelStockValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "stockReport6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHEQUE LIST";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.stockReport_FormClosing);
            this.Load += new System.EventHandler(this.stockReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelStockValue.ResumeLayout(false);
            this.panelStockValue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox itemCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioSearchByDate;
        private System.Windows.Forms.RadioButton radioAdvancedSearch;
        private System.Windows.Forms.CheckBox checkCategory;
        private System.Windows.Forms.CheckBox checkBrand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox brandName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox categoryName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox descriptionName;
        private System.Windows.Forms.CheckBox checkDescription;
        private System.Windows.Forms.CheckBox checkQty;
        private System.Windows.Forms.RadioButton radioMinValue;
        private System.Windows.Forms.TextBox maxValue;
        private System.Windows.Forms.TextBox minValue;
        private System.Windows.Forms.RadioButton radioMaxValue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelStockValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox stockValue;
        private System.Windows.Forms.RadioButton searchALL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totStockValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox textBox1;
    }
}