using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class termXash9 : Form
    {
        public termXash9(invoiceCreditPay invoice, Double Amount, string idH, string customer)
        {
            InitializeComponent();
            sub = Amount;
            id = idH;
            chequeAmount.Text = Amount + "";
            invoiceHome = invoice;
            customerH = customer;
            invoice.Enabled = false;
        }

        //My Variable Start+++
        private double sub;

        private String id, customerH;
        private invoiceCreditPay invoiceHome;
        private DB db, db2, db3, db4, db5;
        private SqlDataReader reader, reader2, reader3;
        private SqlConnection conn, conn2, conn3, conn4, conn5;
        private ArrayList arrayList, stockList;
        private string[] idArray;

        //My Variable End+++

        //My Method Start+++
        private void setTabFouces()
        {
        }

        private void loadAutoCompleteAll()
        {
            try
            {
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(chequeCodeNo, idArray);
                conn.Open();
                reader = new SqlCommand("select checkcodeno from chequeInvoiceRetail ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(chequeCodeNo, idArray);
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //My Method End+++
        private void creditPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                //  save();
            }
            else if (e.KeyValue == 38)
            {
                //  creditPeriod.Focus();
            }
            else if (e.KeyValue == 40)
            {
                setTabFouces();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void creditPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void creditPaid_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void termXash_Load(object sender, EventArgs e)
        {
            try
            {
                this.TopMost = true;
                db = new DB();
                conn = db.createSqlConnection();
                db2 = new DB();
                conn2 = db2.createSqlConnection();
                db3 = new DB();
                conn3 = db3.createSqlConnection();
                db4 = new DB();
                conn4 = db4.createSqlConnection();
                db5 = new DB();
                conn5 = db5.createSqlConnection();
                loadAutoCompleteAll();
                this.ActiveControl = chequeAmount;
                try
                {
                    comboChequePayment.Items.Clear();
                    comboChequePayment.Items.Add("");
                    conn.Open();
                    reader = new SqlCommand("select a.isDefa,b.name,b.bankName,a.accountID from accountChequePayment as a,bankAccounts as b where a.accountid=b.id", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        comboChequePayment.Items.Add(reader.GetString(1).ToUpper() + "-" + reader.GetString(2).ToUpper() + "(" + reader[3] + ")");
                        if (reader.GetBoolean(0))
                        {
                            comboChequePayment.SelectedIndex = comboChequePayment.Items.Count - 1;
                        }
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }

                //  MessageBox.Show(creditB+"/"+creditDetailH.Length);
                this.TopMost = true;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void chequePaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                chequeAmount.Focus();
            }
            else if (e.KeyValue == 38)
            {
                //      qty.Focus();
            }
            else if (e.KeyValue == 40)
            {
                chequeAmount.Focus();
            }
        }

        private void chequePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void chequePaid_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void chequeBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                chequeNo.Focus();
            }
            else if (e.KeyValue == 38)
            {
                //   chequePaid.Focus();
            }
            else if (e.KeyValue == 40)
            {
                chequeNo.Focus();
            }
        }

        private void chequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                chequeCodeNo.Focus();
            }
            else if (e.KeyValue == 38)
            {
                chequeAmount.Focus();
            }
            else if (e.KeyValue == 40)
            {
                chequeCodeNo.Focus();
            }
        }

        private void chequeCodeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                dateTimePicker1.Focus();
            }
            else if (e.KeyValue == 38)
            {
                chequeNo.Focus();
            }
            else if (e.KeyValue == 40)
            {
                dateTimePicker1.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (chequeAmount.Text.Equals("") || Double.Parse(chequeAmount.Text) == 0)
            {
                MessageBox.Show("Sorry , Invalied Cheque Paid Value");
                chequeAmount.Focus();
            }
            else if (chequeNo.Text.Equals(""))
            {
                MessageBox.Show("Invalied Cheque No");
                chequeNo.Focus();
            }
            else
            {
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void cashPaidCard_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cashPaidCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void cashPaidCard_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cardNo_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ccv_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nameonCard_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void termXash_FormClosing(object sender, FormClosingEventArgs e)
        {
            invoiceHome.Enabled = true;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void creditBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void creditBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                //creditPeriod.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button3.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                //  save();
            }
        }

        private Int32 tempcreditPaidID = 0;
        private double amountTemp2, amountTemp;
        private bool states;
        private string tempInvoiceNO;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var amountD = Double.Parse(chequeAmount.Text);
                amountTemp2 = amountD;
                amountTemp = 0;
                conn.Open();
                new SqlCommand("insert into receipt values('" + DateTime.Now + "','" + "" + "','" + customerH + "','" + new amountByName().setAmountName(Double.Parse(chequeAmount.Text) + "") + "','" + chequeAmount.Text + "','" + "" + "','" + "CHEQUE NO-" + chequeNo.Text + " / CHEQUE DATE-" + dateTimePicker1.Value.ToShortDateString() + "','" + "CHEQUE" + "','" + "" + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                conn.Close();

                conn.Open();
                reader = new SqlCommand("select max(id) from receipt ", conn).ExecuteReader();
                if (reader.Read())
                {
                    tempcreditPaidID = reader.GetInt32(0);
                }
                conn.Close();
                states = false;
                System.Windows.Forms.DialogResult result;
                result = MessageBox.Show("Press YES to Settele Invoice with Randoem or Press NO to Settele Invoice with Manual Selection", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    conn2.Open();
                    tempInvoiceNO = "";
                    reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + customerH + "' order by requstdate", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        if (amountTemp2 != 0)
                        {
                            amountTemp = 0;

                            conn.Open();
                            reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                states = true;
                                amountTemp = amountTemp + reader.GetDouble(0);
                            }

                            conn.Close();
                            if (!states)
                            {
                                amountTemp = amountTemp + reader2.GetDouble(1);
                            }
                            else
                            {
                                amountTemp = reader2.GetDouble(1) - amountTemp;
                            }

                            if (amountTemp != 0)
                            {
                                if (amountTemp2 <= amountTemp)
                                {
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + "" + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                    //    MessageBox.Show("1" + tempInvoiceNO);
                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                    }

                                    amountTemp2 = 0;
                                }
                                else
                                {
                                    //    MessageBox.Show("2" + tempInvoiceNO);
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + "" + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                    }
                                    amountTemp2 = amountTemp2 - amountTemp;
                                }
                            }
                        }
                    }
                    conn2.Close();
                    savesub(amountD, tempInvoiceNO);
                }
                else if (result == System.Windows.Forms.DialogResult.No)
                {
                    invoiceSelection2 a = new invoiceSelection2(customerH, amountD + "", "", tempcreditPaidID + "", this);
                    a.Visible = true;
                    a.TopMost = true;
                }
                else
                {
                    savesub(amountD, tempInvoiceNO);
                }

                //+++++++++++++++++++++++++++++++
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                conn.Close();
            }
        }

        public void savesub(double amountD, string tempInvoiceNOS)
        {
            try
            {
                conn.Open();
                new SqlCommand("insert into chequeInvoiceRetail2 values ('" + tempcreditPaidID + "','" + customerH + "','" + amountD + "','" + 0 + "','" + amountD + "','" + chequeNo.Text + "','" + dateTimePicker1.Value + "','" + DateTime.Now + "','" + chequeCodeNo.Text + "','" + true + "')", conn).ExecuteNonQuery();
                conn.Close();

                if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                {
                    conn.Open();

                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + tempInvoiceNOS + "','" + "Invoice-Settelemnt" + "','" + customerH + "','" + "Cheque Payment :Cheque No-" + chequeCodeNo.Text + ",Cheque Date-" + dateTimePicker1.Value + "','" + true + "','" + false + "','" + dateTimePicker1.Value + "','" + amountD + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    new SqlCommand("update receipt set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("insert into customerStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Invoice -Cheque" + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + DateTime.Now + "','" + customerH + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    if ((MessageBox.Show("Do You want to Print Recepit", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select max(id) from receipt ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                var a = reader[0] + "";
                                conn.Close();
                                new invoicePrint().setprintReceiprt(a, conn, reader, "");
                                MessageBox.Show("Send To Print Successfully.......");
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }
                    }
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
            this.Dispose();
            invoiceHome.Enabled = true;
            invoiceHome.clear();
        }

        private void creditBalance_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void creditBalance_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void chequeBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void chequeBalance_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void cardAmount_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cardAmount_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTabFouces();
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }
    }
}