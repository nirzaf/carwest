namespace pos
{
    partial class staff
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
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iTEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDNEWITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEFRESHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.codeC = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nameC = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.addressC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.mobileNumberC = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.salary = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bankingAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.epf = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.etf = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(603, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 52);
            this.button1.TabIndex = 12;
            this.button1.Text = "ADD NEW STAFF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 52);
            this.button2.TabIndex = 13;
            this.button2.Text = "SAVE CURRENT STAFF MEMBER";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(604, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 52);
            this.button3.TabIndex = 14;
            this.button3.Text = "DELETE STAFF MEMBER";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iTEMToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(719, 24);
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
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(477, 48);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 74;
            this.button8.Text = "LOAD";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(13, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 60;
            this.label8.Text = "CODE";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // codeC
            // 
            this.codeC.AllowDrop = true;
            this.codeC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeC.Location = new System.Drawing.Point(245, 55);
            this.codeC.Name = "codeC";
            this.codeC.Size = new System.Drawing.Size(92, 22);
            this.codeC.TabIndex = 61;
            this.codeC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codeC_KeyDown);
            this.codeC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeC_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(13, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 62;
            this.label9.Text = "NAME";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nameC
            // 
            this.nameC.AllowDrop = true;
            this.nameC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameC.Location = new System.Drawing.Point(245, 86);
            this.nameC.Name = "nameC";
            this.nameC.Size = new System.Drawing.Size(307, 22);
            this.nameC.TabIndex = 63;
            this.nameC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nameC_KeyDown);
            this.nameC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nameC_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(13, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 16);
            this.label12.TabIndex = 66;
            this.label12.Text = "ADDRESS";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addressC
            // 
            this.addressC.AllowDrop = true;
            this.addressC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressC.Location = new System.Drawing.Point(101, 125);
            this.addressC.Name = "addressC";
            this.addressC.Size = new System.Drawing.Size(451, 22);
            this.addressC.TabIndex = 67;
            this.addressC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressC_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(13, 161);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 16);
            this.label14.TabIndex = 68;
            this.label14.Text = "MOBILE NUMBER";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mobileNumberC
            // 
            this.mobileNumberC.AllowDrop = true;
            this.mobileNumberC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mobileNumberC.Location = new System.Drawing.Point(415, 158);
            this.mobileNumberC.Name = "mobileNumberC";
            this.mobileNumberC.Size = new System.Drawing.Size(137, 22);
            this.mobileNumberC.TabIndex = 69;
            this.mobileNumberC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mobileNumberC_KeyDown);
            this.mobileNumberC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mobileNumberC_KeyPress);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(705, 33);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(10, 4);
            this.listBox2.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 78;
            this.label1.Text = "SALARY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // salary
            // 
            this.salary.AllowDrop = true;
            this.salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salary.Location = new System.Drawing.Point(415, 188);
            this.salary.Name = "salary";
            this.salary.Size = new System.Drawing.Size(137, 22);
            this.salary.TabIndex = 79;
            this.salary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.salary_KeyDown);
            this.salary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mobileNumberC_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(13, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 16);
            this.label2.TabIndex = 80;
            this.label2.Text = "BANKING AMOUNT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bankingAmount
            // 
            this.bankingAmount.AllowDrop = true;
            this.bankingAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bankingAmount.Location = new System.Drawing.Point(415, 219);
            this.bankingAmount.Name = "bankingAmount";
            this.bankingAmount.Size = new System.Drawing.Size(137, 22);
            this.bankingAmount.TabIndex = 81;
            this.bankingAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bankingAmount_KeyDown);
            this.bankingAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mobileNumberC_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(13, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 82;
            this.label3.Text = "EPF";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // epf
            // 
            this.epf.AllowDrop = true;
            this.epf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.epf.Location = new System.Drawing.Point(101, 250);
            this.epf.Name = "epf";
            this.epf.Size = new System.Drawing.Size(137, 22);
            this.epf.TabIndex = 83;
            this.epf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.epf_KeyDown);
            this.epf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.epf_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(355, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 84;
            this.label4.Text = "ETF";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // etf
            // 
            this.etf.AllowDrop = true;
            this.etf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.etf.Location = new System.Drawing.Point(415, 250);
            this.etf.Name = "etf";
            this.etf.Size = new System.Drawing.Size(137, 22);
            this.etf.TabIndex = 85;
            this.etf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.etf_KeyDown);
            this.etf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.epf_KeyPress);
            // 
            // staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 281);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.etf);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.epf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bankingAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.salary);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.codeC);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nameC);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.addressC);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.mobileNumberC);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "staff";
            this.Text = "STAFF PROFILE";
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iTEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDDNEWITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEFRESHToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox codeC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nameC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox addressC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox mobileNumberC;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox salary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bankingAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox epf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox etf;
    }
}