using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class itemTable4 : Form
    {
        grnNew formH;
        DB db;
        string indexH;
        public itemTable4(grnNew form,string unitPriceH,string discountH,string qtyH,string index,string codeH)
        {
            InitializeComponent();
            formH = form;
            indexH = index;

            qty.Text = qtyH;
            code.Text = codeH;
            qty.Focus();
        }

        void save() {
          
             if (qty.Text.Equals("") || Double.Parse(qty.Text) == 0)
            {
                MessageBox.Show("Please Eneter Valied qty");
                qty.Focus();
            }
            else {

                formH.updateTableItem("0", "0", qty.Text, Int32.Parse(indexH));
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
            if (e.KeyValue==12| e.KeyValue==13)
            {
                save();
            }
            else if (e.KeyValue==38)
            {
           //     discount.Focus();
            }
            else if (e.KeyValue==40)
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
