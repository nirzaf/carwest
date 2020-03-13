﻿using System;
using System.Windows.Forms;

namespace pos
{
    public partial class separate2 : Form
    {
        private invoiceEdit formH;
        private DB db;
        private string indexH;

        public separate2(invoiceEdit form, string unitPriceH, string codeH, string countH, string rateH)
        {
            InitializeComponent();
            formH = form;

            unitPrice.Text = unitPriceH;
            discount.Text = "0";
            code.Text = codeH;
            rate.Text = rateH;
            separateCount.Text = countH;
            unitPrice.Focus();
            try
            {
                separatePrice.Text = Math.Round((Double.Parse(unitPrice.Text) / Double.Parse(countH)), 2) + "";
            }
            catch (Exception)
            {
                separatePrice.Text = "0";
            }
        }

        private void save()
        {
            if (unitPrice.Text.Equals("") || Double.Parse(unitPrice.Text) == 0)
            {
                MessageBox.Show("Please Eneter Valied Unit Price");
                unitPrice.Focus();
            }
            else if (discount.Text.Equals(""))
            {
                MessageBox.Show("Please Eneter Valied Discount");
                discount.Focus();
            }
            else if (qty.Text.Equals("") || Double.Parse(qty.Text) == 0)
            {
                MessageBox.Show("Please Eneter Valied qty");
                qty.Focus();
            }
            else
            {
                //  MessageBox.Show(unitPrice.Text+"/"+discount.Text+"/"+qty.Text);

                formH.addToTableSep(Double.Parse(qty.Text), Double.Parse(separatePrice.Text), Double.Parse(discount.Text), Math.Round((Double.Parse(qty.Text) / Double.Parse(separateCount.Text)), 2));
                formH.Enabled = true;
                this.Dispose();
            }
        }

        private void itemTable_Load(object sender, EventArgs e)
        {
            formH.Enabled = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            db = new DB();
            separatePrice.Focus();
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(unitPrice, discount, discount, e.KeyValue);
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(separatePrice, qty, qty, e.KeyValue);
        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
            else if (e.KeyValue == 38)
            {
                discount.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void itemTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            formH.Enabled = true;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formH.Enabled = true;
            this.Dispose();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void separatePrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(separatePrice, discount, discount, e.KeyValue);
        }
    }
}