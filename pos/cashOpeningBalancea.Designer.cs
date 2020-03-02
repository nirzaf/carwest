namespace pos
{
    partial class cashOpeningBalancea
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
            this.label1 = new System.Windows.Forms.Label();
            this.openingBalance = new System.Windows.Forms.TextBox();
            this.currentBalance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "OPENING BALANCE";
            // 
            // openingBalance
            // 
            this.openingBalance.Location = new System.Drawing.Point(234, 22);
            this.openingBalance.Name = "openingBalance";
            this.openingBalance.Size = new System.Drawing.Size(156, 20);
            this.openingBalance.TabIndex = 1;
            this.openingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.openingBalance.TextChanged += new System.EventHandler(this.openingBalance_TextChanged);
            this.openingBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.openingBalance_KeyPress);
            // 
            // currentBalance
            // 
            this.currentBalance.Enabled = false;
            this.currentBalance.Location = new System.Drawing.Point(234, 63);
            this.currentBalance.Name = "currentBalance";
            this.currentBalance.Size = new System.Drawing.Size(156, 20);
            this.currentBalance.TabIndex = 3;
            this.currentBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "CURRENT BALANCE";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "SAVE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cashOpeningBalancea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 171);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.currentBalance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.openingBalance);
            this.Controls.Add(this.label1);
            this.Name = "cashOpeningBalancea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cashOpeningBalance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cashOpeningBalance_FormClosing);
            this.Load += new System.EventHandler(this.cashOpeningBalance_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox openingBalance;
        private System.Windows.Forms.TextBox currentBalance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}