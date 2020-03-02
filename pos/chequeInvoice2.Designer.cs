namespace pos
{
    partial class chequeInvoice2
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
            this.panelCustomer = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.customerID = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchALL = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioAdvancedSearch = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sETTINGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oRDERBYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboOrderBY = new System.Windows.Forms.ToolStripComboBox();
            this.oRDERTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboOrderTO = new System.Windows.Forms.ToolStripComboBox();
            this.pRINTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qUICKPRINTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qUICKSEARCHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cHEQUENUMBERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelCheque = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateChequeTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateChequeFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.checkCustomer = new System.Windows.Forms.CheckBox();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panelCheque.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCustomer
            // 
            this.panelCustomer.AccessibleName = "sasas";
            this.panelCustomer.BackColor = System.Drawing.Color.Gainsboro;
            this.panelCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCustomer.Controls.Add(this.label2);
            this.panelCustomer.Controls.Add(this.customerID);
            this.panelCustomer.Controls.Add(this.listBox1);
            this.panelCustomer.Controls.Add(this.dataGridView2);
            this.panelCustomer.Controls.Add(this.searchALL);
            this.panelCustomer.Controls.Add(this.groupBox1);
            this.panelCustomer.Controls.Add(this.radioAdvancedSearch);
            this.panelCustomer.Location = new System.Drawing.Point(13, 178);
            this.panelCustomer.Name = "panelCustomer";
            this.panelCustomer.Size = new System.Drawing.Size(361, 313);
            this.panelCustomer.TabIndex = 0;
            this.panelCustomer.Tag = "";
            this.panelCustomer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "CUSTOMER ID /NAME COMPANY";
            // 
            // customerID
            // 
            this.customerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerID.Location = new System.Drawing.Point(29, 93);
            this.customerID.Name = "customerID";
            this.customerID.Size = new System.Drawing.Size(314, 24);
            this.customerID.TabIndex = 0;
            this.customerID.TextChanged += new System.EventHandler(this.itemCode_TextChanged);
            this.customerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemCode_KeyDown);
            this.customerID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customerID_KeyUp);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(330, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(10, 4);
            this.listBox1.TabIndex = 27;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10});
            this.dataGridView2.Location = new System.Drawing.Point(27, 127);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(316, 177);
            this.dataGridView2.TabIndex = 24;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // Column9
            // 
            this.Column9.HeaderText = "ID";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 40;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "NAME/COMPANY";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 250;
            // 
            // searchALL
            // 
            this.searchALL.AutoSize = true;
            this.searchALL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchALL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.searchALL.Location = new System.Drawing.Point(6, 10);
            this.searchALL.Name = "searchALL";
            this.searchALL.Size = new System.Drawing.Size(119, 22);
            this.searchALL.TabIndex = 22;
            this.searchALL.TabStop = true;
            this.searchALL.Text = "All Customers";
            this.searchALL.UseVisualStyleBackColor = true;
            this.searchALL.CheckedChanged += new System.EventHandler(this.searchALL_CheckedChanged);
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
            // radioAdvancedSearch
            // 
            this.radioAdvancedSearch.AutoSize = true;
            this.radioAdvancedSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAdvancedSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioAdvancedSearch.Location = new System.Drawing.Point(5, 39);
            this.radioAdvancedSearch.Name = "radioAdvancedSearch";
            this.radioAdvancedSearch.Size = new System.Drawing.Size(79, 22);
            this.radioAdvancedSearch.TabIndex = 3;
            this.radioAdvancedSearch.TabStop = true;
            this.radioAdvancedSearch.Text = "Custom";
            this.radioAdvancedSearch.UseVisualStyleBackColor = true;
            this.radioAdvancedSearch.CheckedChanged += new System.EventHandler(this.radioAdvancedSearch_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 595);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "LOAD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 579);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 10);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.AccessibleName = "sasas";
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Location = new System.Drawing.Point(388, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(792, 565);
            this.panel3.TabIndex = 20;
            this.panel3.Tag = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column11});
            this.dataGridView1.Location = new System.Drawing.Point(-2, -2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(791, 567);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eXITToolStripMenuItem,
            this.sETTINGSToolStripMenuItem,
            this.pRINTToolStripMenuItem,
            this.qUICKSEARCHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1191, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);
            // 
            // sETTINGSToolStripMenuItem
            // 
            this.sETTINGSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oRDERBYToolStripMenuItem,
            this.oRDERTOToolStripMenuItem});
            this.sETTINGSToolStripMenuItem.Name = "sETTINGSToolStripMenuItem";
            this.sETTINGSToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.sETTINGSToolStripMenuItem.Text = "SEARCH SETTINGS";
            this.sETTINGSToolStripMenuItem.Click += new System.EventHandler(this.sETTINGSToolStripMenuItem_Click);
            // 
            // oRDERBYToolStripMenuItem
            // 
            this.oRDERBYToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboOrderBY});
            this.oRDERBYToolStripMenuItem.Name = "oRDERBYToolStripMenuItem";
            this.oRDERBYToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.oRDERBYToolStripMenuItem.Text = "ORDER BY";
            // 
            // comboOrderBY
            // 
            this.comboOrderBY.Items.AddRange(new object[] {
            "INVOICE DATE",
            "INVOICE ID",
            "CHEQUE DATE"});
            this.comboOrderBY.Name = "comboOrderBY";
            this.comboOrderBY.Size = new System.Drawing.Size(121, 23);
            // 
            // oRDERTOToolStripMenuItem
            // 
            this.oRDERTOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboOrderTO});
            this.oRDERTOToolStripMenuItem.Name = "oRDERTOToolStripMenuItem";
            this.oRDERTOToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.oRDERTOToolStripMenuItem.Text = "ORDER TO";
            // 
            // comboOrderTO
            // 
            this.comboOrderTO.Items.AddRange(new object[] {
            "ASCENDING",
            "DESCENDING"});
            this.comboOrderTO.Name = "comboOrderTO";
            this.comboOrderTO.Size = new System.Drawing.Size(121, 23);
            // 
            // pRINTToolStripMenuItem
            // 
            this.pRINTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qUICKPRINTToolStripMenuItem});
            this.pRINTToolStripMenuItem.Name = "pRINTToolStripMenuItem";
            this.pRINTToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.pRINTToolStripMenuItem.Text = "PRINT";
            this.pRINTToolStripMenuItem.Click += new System.EventHandler(this.pRINTToolStripMenuItem_Click);
            // 
            // qUICKPRINTToolStripMenuItem
            // 
            this.qUICKPRINTToolStripMenuItem.Name = "qUICKPRINTToolStripMenuItem";
            this.qUICKPRINTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.qUICKPRINTToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.qUICKPRINTToolStripMenuItem.Text = "QUICK PRINT";
            this.qUICKPRINTToolStripMenuItem.Click += new System.EventHandler(this.qUICKPRINTToolStripMenuItem_Click);
            // 
            // qUICKSEARCHToolStripMenuItem
            // 
            this.qUICKSEARCHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cHEQUENUMBERToolStripMenuItem});
            this.qUICKSEARCHToolStripMenuItem.Name = "qUICKSEARCHToolStripMenuItem";
            this.qUICKSEARCHToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.qUICKSEARCHToolStripMenuItem.Text = "QUICK SEARCH";
            // 
            // cHEQUENUMBERToolStripMenuItem
            // 
            this.cHEQUENUMBERToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.cHEQUENUMBERToolStripMenuItem.Name = "cHEQUENUMBERToolStripMenuItem";
            this.cHEQUENUMBERToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.cHEQUENUMBERToolStripMenuItem.Text = "CHEQUE NUMBER";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 23);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Red;
            this.checkBox1.Location = new System.Drawing.Point(9, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(174, 20);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "FILTER CHEQUE-DATE";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // panelCheque
            // 
            this.panelCheque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelCheque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCheque.Controls.Add(this.groupBox3);
            this.panelCheque.Location = new System.Drawing.Point(13, 80);
            this.panelCheque.Name = "panelCheque";
            this.panelCheque.Size = new System.Drawing.Size(361, 62);
            this.panelCheque.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateChequeTo);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dateChequeFrom);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(6, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 58);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // dateChequeTo
            // 
            this.dateChequeTo.Location = new System.Drawing.Point(137, 31);
            this.dateChequeTo.Name = "dateChequeTo";
            this.dateChequeTo.Size = new System.Drawing.Size(200, 20);
            this.dateChequeTo.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "TO";
            // 
            // dateChequeFrom
            // 
            this.dateChequeFrom.Location = new System.Drawing.Point(137, 2);
            this.dateChequeFrom.Name = "dateChequeFrom";
            this.dateChequeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateChequeFrom.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "FROM";
            // 
            // checkCustomer
            // 
            this.checkCustomer.AutoSize = true;
            this.checkCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCustomer.ForeColor = System.Drawing.Color.Red;
            this.checkCustomer.Location = new System.Drawing.Point(12, 152);
            this.checkCustomer.Name = "checkCustomer";
            this.checkCustomer.Size = new System.Drawing.Size(153, 20);
            this.checkCustomer.TabIndex = 26;
            this.checkCustomer.Text = "FILTER CUSTOMER";
            this.checkCustomer.UseVisualStyleBackColor = true;
            this.checkCustomer.CheckedChanged += new System.EventHandler(this.checkCustomer_CheckedChanged);
            // 
            // Column8
            // 
            this.Column8.HeaderText = "CUSTOMER";
            this.Column8.Name = "Column8";
            this.Column8.Width = 300;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CHEQUE DATE";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "AMOUNT";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "CHEQUE NUMBER";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "CHEQUE CODE NO";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ISSUE DATE";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "RETURN";
            this.Column6.Name = "Column6";
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "codeNo";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "invoiceNo";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // chequeInvoice2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 632);
            this.Controls.Add(this.checkCustomer);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panelCheque);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelCustomer);
            this.Controls.Add(this.menuStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "chequeInvoice2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHEQUE PAYMENT (INVOICE)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.stockReport_FormClosing);
            this.Load += new System.EventHandler(this.stockReport_Load);
            this.panelCustomer.ResumeLayout(false);
            this.panelCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelCheque.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCustomer;
        private System.Windows.Forms.TextBox customerID;
        private System.Windows.Forms.RadioButton radioAdvancedSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton searchALL;
        private System.Windows.Forms.ToolStripMenuItem sETTINGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oRDERBYToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderBY;
        private System.Windows.Forms.ToolStripMenuItem oRDERTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderTO;
        private System.Windows.Forms.ToolStripMenuItem pRINTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qUICKPRINTToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panelCheque;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateChequeTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateChequeFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkCustomer;
        private System.Windows.Forms.ToolStripMenuItem qUICKSEARCHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cHEQUENUMBERToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}