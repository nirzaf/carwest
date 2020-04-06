﻿using System;
using System.Windows.Forms;

namespace pos
{
    public partial class itemTable3 : Form
    {
        private returnInvoice formH;
        private DB db;
        private string indexH, qtyN;

        public itemTable3(returnInvoice form, string qtyH, string index)
        {
            InitializeComponent();
            formH = form;
            indexH = index;

            qty.Text = qtyH;
            qtyN = qtyH;
        }

        private void save()
        {
            if (qty.Text.Equals("") || double.Parse(qty.Text) == 0)
            {
                MessageBox.Show("Please Eneter Valid qty");
                qty.Focus();
            }
            else if (double.Parse(qtyN) < double.Parse(qty.Text))
            {
                MessageBox.Show("Sorry, Invalid Qty For Return");
            }
            else
            {
                formH.updateTableItem(qty.Text, int.Parse(indexH));
                formH.Enabled = true;
                Dispose();
            }
        }

        private void itemTable_Load(object sender, EventArgs e)
        {
            formH.Enabled = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            db = new DB();
            qty.Focus();
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
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
            else if (e.KeyValue == 38)
            {
                //discount.Focus();
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
    }
}