using System;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class invoicePay : Form
    {
        Form homeH;
        public invoicePay(Form home, String user)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
        }
        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        ArrayList arrayList, stockList, detaiArrayList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        string userH, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId, invoiceino;
        int count;
        string type = "";
        Boolean loadItemCheck = false;
        public Double paidAmount, cashPaidDB;
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
                reader = new SqlCommand("select subTotal,cash,balance from invoiceRetail where id='" + invoiceNO.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    subTotal.Text = reader[0] + "";
                    cash.Text = reader[1] + "";
                    balance.Text = reader[2] + "";
                    cash.Focus();

                    cashPaidDB = reader.GetDouble(1);
                    conn.Close();
                    conn2.Open();

                    reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        creditDetail = new string[2];
                        creditDetail[0] = reader2[4] + "";
                        creditDetail[1] = reader2[5] + "";
                        creditDetailB = true;
                        paidAmount = paidAmount + reader2.GetDouble(4);
                    }
                    reader2.Close();
                    conn2.Close();



                    conn2.Open();
                    reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    detaiArrayList = new ArrayList();

                    while (reader2.Read())
                    {
                        chequeDetailB = true;
                        detaiArrayList.Add(reader2[4].ToString());
                        detaiArrayList.Add(reader2[5].ToString());
                        detaiArrayList.Add(reader2[8].ToString());
                        detaiArrayList.Add(reader2.GetDateTime(6).ToShortDateString().Split(' ')[0]);
                        detaiArrayList.Add(reader2[9].ToString());
                        paidAmount = paidAmount + reader2.GetDouble(4);
                    }
                    chequeDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                    reader2.Close();
                    conn2.Close();

                    conn2.Open();
                    reader2 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    detaiArrayList = new ArrayList();

                    while (reader2.Read())
                    {
                        cardDetailB = true;
                        detaiArrayList.Add(reader2[5].ToString());
                        detaiArrayList.Add(reader2[6].ToString());
                        detaiArrayList.Add(reader2[7].ToString());
                        detaiArrayList.Add(reader2[8].ToString());
                        paidAmount = paidAmount + reader2.GetDouble(5);
                    }
                    cardDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                    reader2.Close();
                    conn2.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from invoiceRetail where date='" + DateTime.Now.ToShortDateString() + "' and id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        dateNow = true;
                    }
                    else
                    {

                        dateNow = false;
                    }
                    reader.Close();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Invalied Invoice No");
                    invoiceNO.Focus();
                }
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception)
            {
                conn.Close();
            }

        }
        void saveInvoice()
        {
            try
            {
                if (!dateNow & !changeInvoiceDifDate)
                {
                    MessageBox.Show("Sorry This is and Past Genarate Invoice and You Havent Permission to EDIT/ DELETE OR RETURN");
                }
                else if (!checkTerm())
                {

                    MessageBox.Show("Please Eneter Pay Detail on Term Section Before Genarate Invoice");
                }
                else
                {
                    db.setCursoerWait();
                    
                    conn.Open();
                    reader = new SqlCommand("select id from invoiceRetail where id='"+invoiceNO.Text+"'",conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update invoiceRetail set cash='"+cash.Text+"',balance='"+balance.Text+"' where id='"+invoiceNO.Text+"'",conn).ExecuteNonQuery();
                        conn.Close();

                        var amountD = Double.Parse(cash.Text) - Double.Parse(balance.Text);
                        amountD = cashPaidDB - amountD;

                        if (amountD != 0)
                        {
                            if (dateNow)
                            {
                                conn.Open();
                                new SqlCommand("insert into cashSummery values('" + "Edit Invoice" + "','" + "Balance Cash Amount / Invoice No -" + invoieNoTemp + "','" + amountD + "','" + DateTime.Now + "','" + userH + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                if ((MessageBox.Show("There Have Cash Balance above " + db.setAmountFormat(amountD + "") + " , and this is a Past Day Genarated Invoice.Do you need settle this Cash Balance from To-Day Cash Flow", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                {
                                    conn.Open();
                                    new SqlCommand("insert into cashSummery values('" + "Edit Invoice" + "','" + "Balance Cash Amount / Invoice No -" + invoieNoTemp + "','" + amountD + "','" + DateTime.Now + "','" + userH + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                }
                            }

                        }

                        var cashDetailB = true;
                        if (!creditDetailB & !chequeDetailB & !cardDetailB)
                        {
                            cashDetailB = true;
                        }
                        else
                        {
                            cashDetailB = false;

                        }
                        conn.Open();
                        new SqlCommand("delete from invoiceTerm where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cashInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cardInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from creditInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from chequeInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        if (!(Double.Parse(cash.Text) == 0 & !creditDetailB & !chequeDetailB & !cardDetailB))
                        {
                            conn.Open();
                            new SqlCommand("insert into invoiceTerm values('" + invoieNoTemp + "','" + cashDetailB + "','" + creditDetailB + "','" + chequeDetailB + "','" + cardDetailB + "','" + userH + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {

                            conn.Open();
                            new SqlCommand("insert into creditInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + 0 + "','" + subTotal.Text + "','" + 30 + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }



                        if (cashDetailB)
                        {
                            conn.Open();
                            new SqlCommand("insert into cashInvoiceRetail values('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                            conn.Close();


                        }
                        if (creditDetailB)
                        {
                            conn.Open();
                            new SqlCommand("insert into creditInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + 0 + "','" + creditDetail[0] + "','" + creditDetail[1] + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                            conn.Close();

                        }
                        if (chequeDetailB)
                        {
                            count = 0;
                            for (int i = 0; i < (chequeDetail.Length) / 5; i++)
                            {
                                tempChequeAmoun = chequeDetail[count];
                                count++;
                                tempChequeNo = chequeDetail[count];
                                count++;
                                tempChequeCodeNo = chequeDetail[count];
                                count++;
                                tempChequeDate = chequeDetail[count];
                                count++;
                                tempChequeId = chequeDetail[count];
                                count++;
                                conn.Open();
                                new SqlCommand("insert into chequeInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeDate + "','" + DateTime.Now + "','" + tempChequeCodeNo + "','" + tempChequeId + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                        if (cardDetailB)
                        {
                            count = 0;
                            for (int i = 0; i < (cardDetail.Length) / 4; i++)
                            {
                                tempChequeAmoun = cardDetail[count];
                                count++;
                                tempChequeNo = cardDetail[count];
                                count++;
                                tempChequeCodeNo = cardDetail[count];
                                count++;
                                tempChequeDate = cardDetail[count];
                                count++;
                                conn.Open();
                                new SqlCommand("insert into cardInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + DateTime.Now + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeCodeNo + "','" + tempChequeDate + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                        conn.Open();
                        reader = new SqlCommand("select id from invoicedump where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            conn.Close();


                            conn.Open();
                            new SqlCommand("update invoiceDump set customerID='" + cutomerID + "',subTotal='" + balance.Text + "',profit='" + profitTotal + "',cash='" + cash.Text + "',balance='" + balance.Text + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();


                        }
                        reader.Close();

                        conn.Close();
                        conn.Open();
                        new invoicePrint().setprintCheque2("R-" + invoiceNO.Text, conn, reader);
                        conn.Close();
                        MessageBox.Show("Send To Print Successfully.......");

                        clear();
                    }
                    else {

                        MessageBox.Show("Invalied Invoice NO");
                        invoiceNO.Focus();

                    }
                    conn.Close();
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.StackTrace);
                conn.Close();
            }
        }
        void clear() {

            invoiceNO.Text = "";
            subTotal.Text = "0.0";
            cash.Text = "0";
            balance.Text = "0.0";
            invoiceNO.Focus();

            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            invoiceNO.Focus();
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();
            clear();
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

                amount = (Double.Parse(cash.Text)) - (Double.Parse(subTotal.Text));

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
            new termXash7(this, Double.Parse(subTotal.Text), Double.Parse(cash.Text), creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;

        }
    }
}
