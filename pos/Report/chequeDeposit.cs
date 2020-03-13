using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class chequeDeposit : Form
    {
        private Form homeH;

        public chequeDeposit(Form home, String user)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
        }

        // My Variable Start
        private DB db;

        private SqlConnection conn, conn2;
        private SqlDataReader reader;
        private ArrayList arrayList;
        public Boolean deposit, check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        private string userH;
        private String[] idArray;
        public string[] creditDetail, chequeDetail, cardDetail;
        public Double paidAmount, cashPaidDB;
        // my Variable End
        //
        //Method

        //
        private void invoicePay_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            invoiceNO.Focus();
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();

            invoiceNO.CharacterCasing = CharacterCasing.Upper;
            this.TopMost = true;
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {
                    saveInvoiceWithoutPay = reader.GetBoolean(0);
                    changeInvoiceDifDate = reader.GetBoolean(1);
                }
                else
                {
                    saveInvoiceWithoutPay = false;
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            conn.Open();
            reader = new SqlCommand("select chequenumber from chequeInvoiceRetail ", conn).ExecuteReader();
            arrayList = new ArrayList();
            while (reader.Read())
            {
                //  MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
            }
            reader.Close();

            conn.Close();
            conn.Open();
            reader = new SqlCommand("select chequenumber from chequeGRN ", conn).ExecuteReader();

            while (reader.Read())
            {
                // MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
            }
            reader.Close();
            idArray = arrayList.ToArray(typeof(string)) as string[];
            db.setAutoComplete(invoiceNO, idArray);
            conn.Close();
            invoiceNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            homeH.Enabled = true;
            homeH.TopMost = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsDigit(e.KeyChar)) return;
            //if (Char.IsControl(e.KeyChar)) return;
            ////if ((e.KeyChar == '.')  ) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            //e.Handled = true;
        }

        private void invoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    states = false;
                    deposit = false;
                    conn.Open();
                    reader = new SqlCommand("select deposit from chequeInvoiceRetail where chequenumber='" + invoiceNO.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        states = true;
                        deposit = reader.GetBoolean(0);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select chequenumber from chequeGrn where chequenumber='" + invoiceNO.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        states = true;

                        deposit = reader.GetBoolean(0);
                    }
                    conn.Close();
                    if (deposit)
                    {
                        MessageBox.Show("Sorry, This Cheque Already Deposit");
                    }
                    else if (!states)
                    {
                        MessageBox.Show("Sorry, Invalied Cheque Number");
                    }
                    else
                    {
                        conn.Open();
                        new SqlCommand("update chequeInvoiceRetail set deposit='" + true + "' where chequenumber='" + invoiceNO.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update chequegrn set deposit='" + true + "' where chequenumber='" + invoiceNO.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Successfully Deposit Cheque");
                        invoiceNO.Text = "";
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }
            }
        }

        private void cash_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cash_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            homeH.Enabled = true;
            homeH.TopMost = true;
        }

        private void pAYDETAILToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}