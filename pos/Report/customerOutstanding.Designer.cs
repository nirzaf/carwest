namespace pos
{
    partial class customerOutstanding
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sETTINGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oRDERBYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboOrderBY = new System.Windows.Forms.ToolStripComboBox();
            this.oRDERTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboOrderTO = new System.Windows.Forms.ToolStripComboBox();
            this.cREDITTERMTYPEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComboCreditTerm = new System.Windows.Forms.ToolStripComboBox();
            this.pRINTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qUICKPRINTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.to = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.radioAllDate = new System.Windows.Forms.RadioButton();
            this.radioDateCustom = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.customerID = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioAllItem = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioAdvancedSearch = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 602);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "LOAD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "search panel";
            // 
            // panel3
            // 
            this.panel3.AccessibleName = "sasas";
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.crystalReportViewer1);
            this.panel3.Location = new System.Drawing.Point(388, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(792, 572);
            this.panel3.TabIndex = 20;
            this.panel3.Tag = "";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.AutoSize = true;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(-2, -2);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowRefreshButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(787, 572);
            this.crystalReportViewer1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eXITToolStripMenuItem,
            this.sETTINGSToolStripMenuItem,
            this.pRINTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 24);
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
            this.oRDERTOToolStripMenuItem,
            this.cREDITTERMTYPEToolStripMenuItem});
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
            this.oRDERBYToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.oRDERBYToolStripMenuItem.Text = "ORDER BY";
            // 
            // comboOrderBY
            // 
            this.comboOrderBY.Items.AddRange(new object[] {
            "DATE",
            "INVOICE ID"});
            this.comboOrderBY.Name = "comboOrderBY";
            this.comboOrderBY.Size = new System.Drawing.Size(121, 23);
            // 
            // oRDERTOToolStripMenuItem
            // 
            this.oRDERTOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboOrderTO});
            this.oRDERTOToolStripMenuItem.Name = "oRDERTOToolStripMenuItem";
            this.oRDERTOToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
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
            // cREDITTERMTYPEToolStripMenuItem
            // 
            this.cREDITTERMTYPEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComboCreditTerm});
            this.cREDITTERMTYPEToolStripMenuItem.Name = "cREDITTERMTYPEToolStripMenuItem";
            this.cREDITTERMTYPEToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.cREDITTERMTYPEToolStripMenuItem.Text = "CREDIT-TERM TYPE";
            // 
            // ComboCreditTerm
            // 
            this.ComboCreditTerm.Items.AddRange(new object[] {
            "BALANCE PENDING INVOICE ONLY",
            "COMPLETE SETTLE INVOICE ONLY",
            "BOTH INVOICE"});
            this.ComboCreditTerm.Name = "ComboCreditTerm";
            this.ComboCreditTerm.Size = new System.Drawing.Size(250, 23);
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Location = new System.Drawing.Point(12, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(361, 119);
            this.panel4.TabIndex = 22;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.to);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.from);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.radioAllDate);
            this.groupBox2.Controls.Add(this.radioDateCustom);
            this.groupBox2.Location = new System.Drawing.Point(3, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 111);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(137, 84);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(200, 20);
            this.to.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 18);
            this.label7.TabIndex = 26;
            this.label7.Text = "TO";
            // 
            // from
            // 
            this.from.Location = new System.Drawing.Point(137, 55);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(200, 20);
            this.from.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "FROM";
            // 
            // radioAllDate
            // 
            this.radioAllDate.AutoSize = true;
            this.radioAllDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAllDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioAllDate.Location = new System.Drawing.Point(2, 7);
            this.radioAllDate.Name = "radioAllDate";
            this.radioAllDate.Size = new System.Drawing.Size(123, 22);
            this.radioAllDate.TabIndex = 24;
            this.radioAllDate.TabStop = true;
            this.radioAllDate.Text = "All Date Period";
            this.radioAllDate.UseVisualStyleBackColor = true;
            // 
            // radioDateCustom
            // 
            this.radioDateCustom.AutoSize = true;
            this.radioDateCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioDateCustom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioDateCustom.Location = new System.Drawing.Point(2, 32);
            this.radioDateCustom.Name = "radioDateCustom";
            this.radioDateCustom.Size = new System.Drawing.Size(161, 22);
            this.radioDateCustom.TabIndex = 23;
            this.radioDateCustom.TabStop = true;
            this.radioDateCustom.Text = "Custom Date Period";
            this.radioDateCustom.UseVisualStyleBackColor = true;
            this.radioDateCustom.CheckedChanged += new System.EventHandler(this.radioDateCustom_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AccessibleName = "sasas";
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.customerID);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.radioAllItem);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.radioAdvancedSearch);
            this.panel1.Location = new System.Drawing.Point(12, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 389);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ITEM CODE / NAME";
            // 
            // customerID
            // 
            this.customerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerID.Location = new System.Drawing.Point(25, 93);
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
            this.dataGridView2.Location = new System.Drawing.Point(23, 127);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(316, 241);
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
            // radioAllItem
            // 
            this.radioAllItem.AutoSize = true;
            this.radioAllItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAllItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.radioAllItem.Location = new System.Drawing.Point(6, 10);
            this.radioAllItem.Name = "radioAllItem";
            this.radioAllItem.Size = new System.Drawing.Size(81, 22);
            this.radioAllItem.TabIndex = 22;
            this.radioAllItem.TabStop = true;
            this.radioAllItem.Text = "All Items";
            this.radioAllItem.UseVisualStyleBackColor = true;
            this.radioAllItem.CheckedChanged += new System.EventHandler(this.searchALL_CheckedChanged);
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
            // customerOutstanding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 631);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "customerOutstanding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CUSTOMER OUTSTANDING (CREDIT)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.stockReport_FormClosing);
            this.Load += new System.EventHandler(this.stockReport_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sETTINGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oRDERBYToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderBY;
        private System.Windows.Forms.ToolStripMenuItem oRDERTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderTO;
        private System.Windows.Forms.ToolStripMenuItem pRINTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qUICKPRINTToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioAllDate;
        private System.Windows.Forms.RadioButton radioDateCustom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker to;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker from;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.ToolStripMenuItem cREDITTERMTYPEToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox ComboCreditTerm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox customerID;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.RadioButton radioAllItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioAdvancedSearch;
    }
}