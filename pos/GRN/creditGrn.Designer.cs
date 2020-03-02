namespace pos.GRN
{
    partial class creditGrn
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
            this.radioNotPaid = new System.Windows.Forms.RadioButton();
            this.radioPaid = new System.Windows.Forms.RadioButton();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.radioAll);
            this.panel1.Controls.Add(this.radioPaid);
            this.panel1.Controls.Add(this.radioNotPaid);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 45);
            this.panel1.TabIndex = 0;
            // 
            // radioNotPaid
            // 
            this.radioNotPaid.AutoSize = true;
            this.radioNotPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioNotPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.radioNotPaid.Location = new System.Drawing.Point(11, 12);
            this.radioNotPaid.Name = "radioNotPaid";
            this.radioNotPaid.Size = new System.Drawing.Size(109, 24);
            this.radioNotPaid.TabIndex = 0;
            this.radioNotPaid.TabStop = true;
            this.radioNotPaid.Text = "NOT PAID";
            this.radioNotPaid.UseVisualStyleBackColor = true;
            // 
            // radioPaid
            // 
            this.radioPaid.AutoSize = true;
            this.radioPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.radioPaid.Location = new System.Drawing.Point(138, 12);
            this.radioPaid.Name = "radioPaid";
            this.radioPaid.Size = new System.Drawing.Size(69, 24);
            this.radioPaid.TabIndex = 1;
            this.radioPaid.TabStop = true;
            this.radioPaid.Text = "PAID";
            this.radioPaid.UseVisualStyleBackColor = true;
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.radioAll.Location = new System.Drawing.Point(224, 12);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(59, 24);
            this.radioAll.TabIndex = 2;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "ALL";
            this.radioAll.UseVisualStyleBackColor = true;
            // 
            // creditGrn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 518);
            this.Controls.Add(this.panel1);
            this.Name = "creditGrn";
            this.Text = "PURCHASING ";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioNotPaid;
        private System.Windows.Forms.RadioButton radioPaid;
        private System.Windows.Forms.RadioButton radioAll;
    }
}