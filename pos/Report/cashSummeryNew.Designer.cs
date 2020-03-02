namespace pos
{
    partial class cashSummeryNew
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
            this.label2 = new System.Windows.Forms.Label();
            this.itemCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.gROUPBYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboGroupBy = new System.Windows.Forms.ToolStripComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.to = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.radioAllDate = new System.Windows.Forms.RadioButton();
            this.radioDateCustom = new System.Windows.Forms.RadioButton();
            this.listItem = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.listItem.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleName = "sasas";
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.itemCode);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 385);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "USER CODE / NAME";
            // 
            // itemCode
            // 
            this.itemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemCode.Location = new System.Drawing.Point(4, 28);
            this.itemCode.Name = "itemCode";
            this.itemCode.Size = new System.Drawing.Size(336, 24);
            this.itemCode.TabIndex = 0;
            this.itemCode.TextChanged += new System.EventHandler(this.itemCode_TextChanged);
            this.itemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemCode_KeyDown);
            this.itemCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.customerID_KeyUp);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 355);
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
            this.crystalReportViewer1.Size = new System.Drawing.Size(792, 572);
            this.crystalReportViewer1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eXITToolStripMenuItem,
            this.sETTINGSToolStripMenuItem});
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
            this.gROUPBYToolStripMenuItem});
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
            // gROUPBYToolStripMenuItem
            // 
            this.gROUPBYToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboGroupBy});
            this.gROUPBYToolStripMenuItem.Name = "gROUPBYToolStripMenuItem";
            this.gROUPBYToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.gROUPBYToolStripMenuItem.Text = "GROUP BY";
            // 
            // comboGroupBy
            // 
            this.comboGroupBy.Items.AddRange(new object[] {
            "CUSTOMER",
            "ITEM",
            "BOTH -CUSTOMER TO ITEM",
            "BOTH -ITEM  TO CUSTOMER"});
            this.comboGroupBy.Name = "comboGroupBy";
            this.comboGroupBy.Size = new System.Drawing.Size(170, 23);
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
            // listItem
            // 
            this.listItem.Controls.Add(this.tabPage1);
            this.listItem.Location = new System.Drawing.Point(12, 181);
            this.listItem.Name = "listItem";
            this.listItem.SelectedIndex = 0;
            this.listItem.Size = new System.Drawing.Size(361, 415);
            this.listItem.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(353, 389);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "FILTER BY USER";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // cashSummeryNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 631);
            this.Controls.Add(this.listItem);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "cashSummeryNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CASH SUMMERY";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.stockReport_FormClosing);
            this.Load += new System.EventHandler(this.stockReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.listItem.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem sETTINGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oRDERBYToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderBY;
        private System.Windows.Forms.ToolStripMenuItem oRDERTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboOrderTO;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioAllDate;
        private System.Windows.Forms.RadioButton radioDateCustom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker to;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker from;
        private System.Windows.Forms.Label label2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TabControl listItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem gROUPBYToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comboGroupBy;
    }
}