namespace pos
{
    partial class fixedAsset
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
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iTEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDNEWITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEFRESHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.TextBox();
            this.checkOpeningBalance = new System.Windows.Forms.CheckBox();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(611, 383);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 52);
            this.button1.TabIndex = 12;
            this.button1.Text = "ADD NEW ACCOUNT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(610, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 52);
            this.button2.TabIndex = 13;
            this.button2.Text = "SAVE CURRENT ACCOUNT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iTEMToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // iTEMToolStripMenuItem
            // 
            this.iTEMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aDDNEWITEMToolStripMenuItem,
            this.sAVECURRENTITEMToolStripMenuItem,
            this.dELETECURRENTITEMToolStripMenuItem,
            this.rEFRESHToolStripMenuItem});
            this.iTEMToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTEMToolStripMenuItem.Name = "iTEMToolStripMenuItem";
            this.iTEMToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.iTEMToolStripMenuItem.Text = "MENU";
            // 
            // aDDNEWITEMToolStripMenuItem
            // 
            this.aDDNEWITEMToolStripMenuItem.Name = "aDDNEWITEMToolStripMenuItem";
            this.aDDNEWITEMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.aDDNEWITEMToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.aDDNEWITEMToolStripMenuItem.Text = "ADD NEW ITEM";
            this.aDDNEWITEMToolStripMenuItem.Click += new System.EventHandler(this.aDDNEWITEMToolStripMenuItem_Click);
            // 
            // sAVECURRENTITEMToolStripMenuItem
            // 
            this.sAVECURRENTITEMToolStripMenuItem.Name = "sAVECURRENTITEMToolStripMenuItem";
            this.sAVECURRENTITEMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.sAVECURRENTITEMToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.sAVECURRENTITEMToolStripMenuItem.Text = "SAVE CURRENT ITEM";
            this.sAVECURRENTITEMToolStripMenuItem.Click += new System.EventHandler(this.sAVECURRENTITEMToolStripMenuItem_Click);
            // 
            // dELETECURRENTITEMToolStripMenuItem
            // 
            this.dELETECURRENTITEMToolStripMenuItem.Name = "dELETECURRENTITEMToolStripMenuItem";
            this.dELETECURRENTITEMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.dELETECURRENTITEMToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.dELETECURRENTITEMToolStripMenuItem.Text = "DELETE CURRENT ITEM";
            this.dELETECURRENTITEMToolStripMenuItem.Click += new System.EventHandler(this.dELETECURRENTITEMToolStripMenuItem_Click);
            // 
            // rEFRESHToolStripMenuItem
            // 
            this.rEFRESHToolStripMenuItem.Name = "rEFRESHToolStripMenuItem";
            this.rEFRESHToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.rEFRESHToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.rEFRESHToolStripMenuItem.Text = "REFRESH";
            this.rEFRESHToolStripMenuItem.Click += new System.EventHandler(this.rEFRESHToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.listBox1.ForeColor = System.Drawing.Color.Blue;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(704, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(10, 4);
            this.listBox1.TabIndex = 17;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(13, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 62;
            this.label9.Text = "NAME";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // name
            // 
            this.name.AllowDrop = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(245, 72);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(307, 22);
            this.name.TabIndex = 63;
            this.name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameC_KeyDown);
            this.name.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nameC_KeyUp);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.SlateGray;
            this.label13.Location = new System.Drawing.Point(61, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 16);
            this.label13.TabIndex = 70;
            this.label13.Text = "DATE";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.SlateGray;
            this.label14.Location = new System.Drawing.Point(61, 134);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 16);
            this.label14.TabIndex = 68;
            this.label14.Text = "AMOUNT";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // amount
            // 
            this.amount.AllowDrop = true;
            this.amount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amount.Location = new System.Drawing.Point(415, 131);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(137, 22);
            this.amount.TabIndex = 69;
            this.amount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mobileNumberC_KeyDown);
            // 
            // checkOpeningBalance
            // 
            this.checkOpeningBalance.AutoSize = true;
            this.checkOpeningBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkOpeningBalance.ForeColor = System.Drawing.Color.Blue;
            this.checkOpeningBalance.Location = new System.Drawing.Point(16, 105);
            this.checkOpeningBalance.Name = "checkOpeningBalance";
            this.checkOpeningBalance.Size = new System.Drawing.Size(142, 17);
            this.checkOpeningBalance.TabIndex = 78;
            this.checkOpeningBalance.Text = "OPENING BALANCE";
            this.checkOpeningBalance.UseVisualStyleBackColor = true;
            this.checkOpeningBalance.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(415, 160);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(137, 20);
            this.date.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 90;
            this.label2.Text = "NUMBER";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // number
            // 
            this.number.AllowDrop = true;
            this.number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.number.Location = new System.Drawing.Point(415, 44);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(137, 22);
            this.number.TabIndex = 91;
            this.number.KeyDown += new System.Windows.Forms.KeyEventHandler(this.number_KeyDown);
            this.number.KeyUp += new System.Windows.Forms.KeyEventHandler(this.number_KeyUp);
            // 
            // fixedAsset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 506);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.number);
            this.Controls.Add(this.date);
            this.Controls.Add(this.checkOpeningBalance);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fixedAsset";
            this.Text = "FIXED ASSETS";
            this.Activated += new System.EventHandler(this.itemProfile_Activated);
            this.Deactivate += new System.EventHandler(this.itemProfile_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.itemProfile_FormClosing);
            this.Load += new System.EventHandler(this.itemProfile_Load);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.itemProfile_GiveFeedback);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.itemProfile_MouseClick);
            this.MouseHover += new System.EventHandler(this.itemProfile_MouseHover);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iTEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDDNEWITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEFRESHToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkOpeningBalance;
        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox number;
    }
}