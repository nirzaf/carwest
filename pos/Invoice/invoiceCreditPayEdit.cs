using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class invoiceCreditPayEdit : Form
    {
        invoiceCreditPaidedit homeH;
        public invoiceCreditPayEdit(invoiceCreditPaidedit home, String user, string id, String redordID,string rowID)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
            inID = id;
            recordIDH = redordID;
            rowIDH = rowID;
        }
        // My Variable Start
        DB db;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        string userH,rowIDH;
        public string[] creditDetail, chequeDetail, cardDetail;
        string inID, recordIDH;
        public Double paidAmount, cashPaidDB, amountH, amount;
        // my Variable End
        //
        //Method





        void loadInvoice()
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                paidAmount = 0;
                cash.Text = "0";
                balance.Text = "0";
                reader = new SqlCommand("select subTotal,cash,balance from invoiceRetail where id='" + invoiceNO.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    subTotal.Text = reader[0] + "";
                    cash.Focus();

                    conn.Close();

                    conn2.Open();

                    reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {

                        creditDetailB = true;
                        //   MessageBox.Show("" + reader2.GetDouble(4));
                        paidAmount = paidAmount + reader2.GetDouble(4);
                    }
                    else
                    {

                        //  MessageBox.Show(invoiceNO.Text);
                    }
                    reader2.Close();
                    conn2.Close();

                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select sum(amount) from invoiceCreditPaid where invoiceid='" + invoiceNO.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            paidAmount = paidAmount - reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                    }
                    conn.Open();
                    reader = new SqlCommand("select amount,paid,balance from invoiceCreditPaid where id='" + recordIDH + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        amountH = reader.GetDouble(0);
                        cash.Text = reader[1] + "";
                        balance.Text = reader[2] + "";
                    }
                    conn.Close();

                    creditAmount.Text = (paidAmount + amountH) + "";

                    cash.Focus();
                    cash.SelectAll();
                }
                else
                {
                    MessageBox.Show("Invalied Invoice No");
                    invoiceNO.Focus();
                }
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                conn.Close();
            }

        }
        void saveInvoice()
        {
            try
            {
                if (creditAmount.Text.Equals("") || Double.Parse(creditAmount.Text) <= 0)
                {
                    MessageBox.Show("Sorry , Invalied Credit Amount");
                }

                else
                {
                    db.setCursoerWait();

                    conn.Open();
                    reader = new SqlCommand("select id from invoiceRetail where id='" + invoiceNO.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        conn.Open();
                        var amountD = Double.Parse(cash.Text) - Double.Parse(balance.Text);
                        homeH.dataGridView1.Rows[Int32.Parse(rowIDH)].Cells[2].Value = db.setAmountFormat(amountD+"");

                        homeH.dataGridView1.Rows[Int32.Parse(rowIDH)].Cells[3].Value = db.setAmountFormat(cash.Text + "");

                        homeH.dataGridView1.Rows[Int32.Parse(rowIDH)].Cells[4].Value = db.setAmountFormat(balance.Text + "");

                        new SqlCommand("update invoiceCreditPaid set amount='" + amountD + "',paid='" + cash.Text + "',balance='" + balance.Text + "',userid='" + userH + "' where id='"+recordIDH+"' ", conn).ExecuteNonQuery();
                        conn.Close();

                      
                        amountD = amountD - amountH;

                        if (amountD != 0)
                        {
                            if (dateNow)
                            {
                                conn.Open();
                                new SqlCommand("insert into cashSummery values('" + "Invoice Credit Paid-Settle" + "','" + "Update Record and Balance above Invoice No -" + invoiceNO.Text + "','" + amountD + "','" + DateTime.Now + "','" + userH + "')", conn).ExecuteNonQuery();
                     
                                conn.Close();
                            }
                            else
                            {
                                if ((MessageBox.Show("There Have Cash Balance above " + db.setAmountFormat(amountD + "") + " , and this is a Past Day Genarated One.Do you need settle this Cash Balance from To-Day Cash Flow", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                {
                                    conn.Open();
                                    new SqlCommand("insert into cashSummery values('" + "Invoice Credit Paid-Settle" + "','" + "Update Record and Balance above Invoice No -" + invoiceNO.Text + "','" + amountD + "','" + DateTime.Now + "','" + userH + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                }
                            }

                        }
                        this.Dispose();
                        homeH.Enabled = true;
                        homeH.TopMost = true;
                    }
                    else
                    {

                        MessageBox.Show("Invalied Invoice NO");
                        invoiceNO.Focus();

                    }
                    conn.Close();
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }
        void clear()
        {

            invoiceNO.Text = "";
            subTotal.Text = "0.0";
            cash.Text = "0";
            balance.Text = "0.0";
            creditAmount.Text = "0.0";
            invoiceNO.Focus();
        }
        Boolean checkTerm()
        {
            states = true;
            try
            {
                if (cash.Text.Equals(""))
                {
                    cash.Text = "0";
                }
                if (Double.Parse(cash.Text) == 0 & !creditDetailB & !chequeDetailB & !cardDetailB)
                {
                    if (saveInvoiceWithoutPay)
                    {
                        states = true;
                    }
                    else
                    {
                        states = false;
                    }
                    creditDetailB = false;
                    chequeDetailB = false;
                    cardDetailB = false;
                }
                else if (Double.Parse(cash.Text) == 0)
                {
                    if (creditDetailB | chequeDetailB | cardDetailB)
                    {
                        if (saveInvoiceWithoutPay)
                        {
                            states = true;
                        }
                        else
                        {
                            states = false;
                        }
                    }

                }
                else if (Double.Parse(cash.Text) < Double.Parse(subTotal.Text))
                {
                    if ((Double.Parse(subTotal.Text) - Double.Parse(cash.Text)) != paidAmount)
                    {
                        states = false;
                    }
                }
                else
                {

                    creditDetailB = false;
                    chequeDetailB = false;
                    cardDetailB = false;
                }






            }
            catch (Exception)
            {

            }

            return states;
        }

        //
        private void invoicePay_Load(object sender, EventArgs e)
        {
            homeH.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            invoiceNO.Focus();
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();
            clear();
            this.TopMost = true;
            invoiceNO.Text = inID;
            loadInvoice();
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
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void invoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                loadInvoice();
            }

        }

        private void cash_KeyUp(object sender, KeyEventArgs e)
        {
            if (!cash.Text.Equals(""))
            {

                amount = (Double.Parse(cash.Text)) - (Double.Parse(creditAmount.Text));

                if (amount <= 0)
                {
                    balance.Text = "0";
                }
                else
                {
                    balance.Text = amount + "";
                }
            }
        }

        private void cash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                saveInvoice();
            }
            else if (e.KeyValue == 38)
            {
                invoiceNO.Focus();
            }
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
