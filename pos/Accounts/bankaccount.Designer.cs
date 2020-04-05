namespace pos
{
    partial class bankaccount
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
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.iTEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDDNEWITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAVECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETECURRENTITEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rEFRESHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label10 = new System.Windows.Forms.Label();
            this.bankName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.acountNo = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(555, 44);
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
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(10, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 16);
            this.label10.TabIndex = 64;
            this.label10.Text = "BANK NAME";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bankName
            // 
            this.bankName.AllowDrop = true;
            this.bankName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bankName.Location = new System.Drawing.Point(242, 74);
            this.bankName.Name = "bankName";
            this.bankName.Size = new System.Drawing.Size(307, 22);
            this.bankName.TabIndex = 65;
            this.bankName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.companyC_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(10, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 16);
            this.label12.TabIndex = 66;
            this.label12.Text = "ACCOUNT NUMBER";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // acountNo
            // 
            this.acountNo.AllowDrop = true;
            this.acountNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acountNo.Location = new System.Drawing.Point(412, 46);
            this.acountNo.Name = "acountNo";
            this.acountNo.Size = new System.Drawing.Size(137, 22);
            this.acountNo.TabIndex = 67;
            this.acountNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressC_KeyDown);
            // 
            // bankaccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 106);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bankName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.acountNo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "bankaccount";
            this.Text = "BANK ACCOUNT\'S";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.itemProfile_FormClosing);
            this.Load += new System.EventHandler(this.itemProfile_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iTEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDDNEWITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAVECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETECURRENTITEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rEFRESHToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bankName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox acountNo;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
    }
}