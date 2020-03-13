using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class termXash4 : Form
    {
        public termXash4(grnNew invoice, Double Amount, Double cashPaidH, String[] creditDetail, String[] chequeDetail, String[] cardDetail, bool creditH, bool chequeH, bool cardH)
        {
            InitializeComponent();
            sub = Amount;
            cashPaid.Text = cashPaidH + "";
            invoiceAmount.Text = Amount + "";
            balance.Text = "0";
            invoiceHome = invoice;
            invoice.Enabled = false;
            creditDetailH = creditDetail;
            chequeDetailH = chequeDetail;
            cardDetailH = cardDetail;
            this.ActiveControl = cashPaid;
            creditB = creditH;
            chequeB = chequeH;
            cardB = cardH;

            //   MessageBox.Show(Amount+"/"+cashPaidH+"/");
        }

        //My Variable Start+++
        private double amount, sub, credit, cheque, card, balanceF;

        private Boolean creditB, chequeB, cardB;
        private String type, customer, code;
        private grnNew invoiceHome;
        private DB db, db2, db3, db4, db5;
        private SqlDataReader reader, reader2, reader3;
        private SqlConnection conn, conn2, conn3, conn4, conn5;
        private ArrayList arrayList, stockList;
        private string[] idArray, creditDetailH, chequeDetailH, cardDetailH;
        private string brand, description, listBoxType, invoieNoTemp;
        private int count;
        private string tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;

        //My Variable End+++

        //My Method Start+++
        private void setTabFouces()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                creditAmount.Focus();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                chequeAmount.Focus();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                cardAmount.Focus();
            }
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
                save();
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
            if (!cashPaid.Text.Equals(""))
            {
                if (Double.Parse(cashPaid.Text) + (credit + cheque + card) > (sub))
                {
                    cashPaid.Focus();
                    cashPaid.Text = "0";
                    balanceF = 0;
                    cashPaid.SelectAll();
                }

                balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));

                balance.Text = balanceF + "";
            }
            else
            {
                balanceF = sub - ((credit + cheque + card));

                balance.Text = balanceF + "";
                cashPaid.Text = "0";
                cashPaid.SelectAll();
            }
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

                dataGridView2.AllowUserToAddRows = false;
                dataGridView1.AllowUserToAddRows = false;
                this.ActiveControl = cashPaid;

                //  MessageBox.Show(creditB+"/"+creditDetailH.Length);
                this.TopMost = true;

                if (creditB)
                {
                    try
                    {
                        if (creditDetailH != null)
                        {
                            creditAmount.Text = creditDetailH[0];
                            creditPeriod.Value = Int32.Parse(creditDetailH[1].ToString());
                            credit = Double.Parse(creditAmount.Text);
                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }

                if (chequeB)
                {
                    if (chequeDetailH != null)
                    {
                        count = 0;
                        for (int i = 0; i < (chequeDetailH.Length) / 5; i++)
                        {
                            //  MessageBox.Show((chequeDetailH.Length - 2) / 5+"");
                            cheque = cheque + Double.Parse(chequeDetailH[count] + "");
                            tempChequeAmoun = chequeDetailH[count];
                            count++;
                            tempChequeNo = chequeDetailH[count];
                            count++;
                            tempChequeCodeNo = chequeDetailH[count];
                            count++;
                            tempChequeDate = chequeDetailH[count];
                            count++;
                            tempChequeId = chequeDetailH[count];
                            count++;
                            dataGridView2.Rows.Add(tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId);
                            if (tempChequeId.ToUpper().Equals("TRUE"))
                            {
                                dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                            }
                        }
                    }
                }
                if (cardB)
                {
                    if (cardDetailH != null)
                    {
                        count = 0;
                        for (int i = 0; i < (cardDetailH.Length) / 4; i++)
                        {
                            card = card + Double.Parse(cardDetailH[count] + "");
                            //  MessageBox.Show((chequeDetailH.Length - 2) / 5+"");
                            tempChequeAmoun = cardDetailH[count];
                            count++;
                            tempChequeNo = cardDetailH[count];
                            count++;
                            tempChequeCodeNo = cardDetailH[count];
                            count++;
                            tempChequeDate = cardDetailH[count];
                            count++;
                            dataGridView1.Rows.Add(tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate);
                        }
                    }
                }
                //   MessageBox.Show(credit+"/"+cheque+"/"+card);
                balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                balance.Text = balanceF + "";
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
                if (Double.Parse(cashPaid.Text) + (credit + cheque + (Double.Parse(chequeAmount.Text)) + card) > (sub))
                {
                    MessageBox.Show("Sorry Cheque Value Exit more than Balance Payment");
                    chequeAmount.Focus();
                }
                else
                {
                    if (dataGridView2.Rows.Count == 0)
                    {
                        dataGridView2.Rows.Add(chequeAmount.Text, chequeNo.Text, chequeCodeNo.Text, dateTimePicker1.Value.ToString().Split(' ')[0], "1");

                        cheque = cheque + Double.Parse(chequeAmount.Text);
                        chequeAmount.Text = amount + "";
                        chequeNo.Text = "";
                        chequeCodeNo.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        chequeAmount.Focus();
                        balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                        balance.Text = balanceF + "";
                        //   MessageBox.Show(sub+"/"+balanceF);
                    }
                    else
                    {
                        var state = true;
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            if (dataGridView2.Rows[i].Cells[1].Value.ToString().Equals(chequeNo.Text))
                            {
                                state = false;
                            }
                            //    MessageBox.Show(dataGridView2.Rows[i].Cells[1].ToString() + "/" + chequeNo.Text);
                        }

                        if (state)
                        {
                            dataGridView2.Rows.Add(chequeAmount.Text, chequeNo.Text, chequeCodeNo.Text, dateTimePicker1.Value.ToString().Split(' ')[0], "1");

                            cheque = cheque + Double.Parse(chequeAmount.Text);
                            chequeAmount.Text = amount + "";
                            chequeNo.Text = "";
                            chequeCodeNo.Text = "";
                            dateTimePicker1.Value = DateTime.Now;
                            chequeAmount.Focus();
                            balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                            balance.Text = balanceF + "";
                        }
                        else
                        {
                            MessageBox.Show("Sorry Duplicate CHEQUE");
                            chequeNo.Focus();
                        }
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Empty Data to Remove from Table");
                // code.Focus();
            }
            else
            {
                try
                {
                    var y = dataGridView2.SelectedRows[0].Index;
                    balanceF = balanceF + (Double.Parse(dataGridView2.Rows[y].Cells[0].Value.ToString()));
                    cheque = cheque - Double.Parse(dataGridView2.Rows[y].Cells[0].Value.ToString());
                    dataGridView2.Rows.RemoveAt(y);

                    balance.Text = balanceF + "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Row to Delete");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                chequeDetailH = new string[dataGridView2.Rows.Count * 5];

                count = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    chequeDetailH[count] = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    count++;
                    chequeDetailH[count] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                    count++;
                    chequeDetailH[count] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                    count++;
                    chequeDetailH[count] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                    count++;
                    chequeDetailH[count] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                    count++;
                }
                this.Dispose();
                invoiceHome.Enabled = true;
                invoiceHome.setTermBack(true);
                invoiceHome.TopMost = true;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.StackTrace);
            }
        }

        private void cashPaidCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                cardNo.Focus();
            }
            else if (e.KeyValue == 40)
            {
                cardNo.Focus();
            }
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
            db.setTextBoxPath(cardAmount, ccv, ccv, e.KeyValue);
        }

        private void ccv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                nameonCard.Focus();
            }
            else if (e.KeyValue == 38)
            {
                cardNo.Focus();
            }
            else if (e.KeyValue == 40)
            {
                nameonCard.Focus();
            }
        }

        private void nameonCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button11_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                ccv.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button2.Focus();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void termXash_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            invoiceHome.Enabled = true;
            invoiceHome.setTermBack(true);
            invoiceHome.TopMost = true;
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
                creditPeriod.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button3.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (balance.Text.Equals("") || Double.Parse(balance.Text) != 0)
            {
                MessageBox.Show("Sorry,There have more Balance to Settele");
                setTabFouces();
            }
            else
            {
                try
                {
                    if (creditAmount.Text.Equals("") || Double.Parse(creditAmount.Text) == 0)
                    {
                        creditB = false;
                        creditDetailH = null;
                    }
                    else
                    {
                        creditB = true;
                        creditDetailH = new string[2];
                        creditDetailH[0] = creditAmount.Text;
                        creditDetailH[1] = creditPeriod.Value.ToString();
                    }

                    if (dataGridView2.Rows.Count == 0)
                    {
                        chequeB = false;
                        chequeDetailH = null;
                    }
                    else
                    {
                        chequeB = true;
                        chequeDetailH = new string[dataGridView2.Rows.Count * 5];

                        count = 0;
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            chequeDetailH[count] = dataGridView2.Rows[i].Cells[0].Value.ToString();
                            count++;
                            chequeDetailH[count] = dataGridView2.Rows[i].Cells[1].Value.ToString();
                            count++;
                            chequeDetailH[count] = dataGridView2.Rows[i].Cells[2].Value.ToString();
                            count++;
                            chequeDetailH[count] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                            count++;
                            chequeDetailH[count] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                            count++;
                        }
                    }
                    if (dataGridView1.Rows.Count == 0)
                    {
                        cardB = false;
                        cardDetailH = null;
                    }
                    else
                    {
                        cardB = true;

                        cardDetailH = new string[dataGridView1.Rows.Count * 4];

                        count = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            cardDetailH[count] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            count++;
                            cardDetailH[count] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            count++;
                            cardDetailH[count] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            count++;
                            cardDetailH[count] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                            count++;
                        }
                    }

                    invoiceHome.creditDetail = creditDetailH;
                    invoiceHome.chequeDetail = chequeDetailH;
                    invoiceHome.cardDetail = cardDetailH;

                    invoiceHome.creditDetailB = creditB;
                    invoiceHome.chequeDetailB = chequeB;
                    invoiceHome.cardDetailB = cardB;

                    invoiceHome.cashPaid = Double.Parse(cashPaid.Text);
                    this.Dispose();
                    invoiceHome.Enabled = true;
                    invoiceHome.setTermBack(true);
                    invoiceHome.TopMost = true;
                }
                catch (Exception)
                {
                }
            }
        }

        private void save()
        {
        }

        private void creditBalance_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void creditBalance_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!creditAmount.Text.Equals(""))
                {
                    credit = Double.Parse(creditAmount.Text);
                    if (Double.Parse(cashPaid.Text) + (credit + cheque + card) > (sub))
                    {
                        creditAmount.Text = "0";
                        credit = 0;
                        creditAmount.SelectAll();
                    }

                    balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                    balance.Text = balanceF + "";
                }
                else
                {
                    credit = 0;
                    balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                    balance.Text = balanceF + "";
                    creditAmount.Text = "0";
                    creditAmount.SelectAll();
                }
            }
            catch (Exception)
            {
            }
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
            if (!chequeAmount.Text.Equals(""))
            {
                if (Double.Parse(cashPaid.Text) + (credit + cheque + (Double.Parse(chequeAmount.Text)) + card) > (sub))
                {
                    //  MessageBox.Show("Sorry, Invalied Cheque Value");
                    chequeAmount.Focus();
                    chequeAmount.Text = "0";
                    chequeAmount.SelectAll();
                }
                else
                {
                    //   cheque = cheque + Double.Parse(chequeAmount.Text);
                    //    MessageBox.Show(balanceF+"");
                    //balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                    //balance.Text = balanceF + "";
                }
            }
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
            if (e.KeyValue == 40)
            {
                cardNo.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                cardNo.Focus();
            }
        }

        private void cardAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (!cardAmount.Text.Equals(""))
            {
                //  card = card+ Double.Parse(chequeAmount.Text);
                if (Double.Parse(cashPaid.Text) + (credit + cheque + (Double.Parse(cardAmount.Text)) + card) > (sub))
                {
                    // MessageBox.Show("Sorry, Invalied Card Value");
                    cardAmount.Focus();
                    cardAmount.Text = "0";
                    cardAmount.SelectAll();
                }
                else
                {
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cardAmount.Text.Equals("") || Double.Parse(cardAmount.Text) == 0)
            {
                MessageBox.Show("Sorry , Invalied Card Paid Value");
                cardAmount.Focus();
            }
            else if (cardNo.Text.Equals(""))
            {
                MessageBox.Show("Invalied Card No");
                cardNo.Focus();
            }
            else
            {
                if (Double.Parse(cashPaid.Text) + (credit + cheque + (Double.Parse(cardAmount.Text)) + card) > (sub))
                {
                    MessageBox.Show("Sorry Card Value Exit more than Balance Payment");
                    cardAmount.Focus();
                }
                else
                {
                    dataGridView1.Rows.Add(cardAmount.Text, cardNo.Text, ccv.Text, nameonCard.Text);

                    card = card + Double.Parse(cardAmount.Text);
                    cardAmount.Text = amount + "";
                    cardNo.Text = "";
                    ccv.Text = "";
                    nameonCard.Text = "";
                    cardAmount.Focus();

                    balanceF = sub - (Double.Parse(cashPaid.Text) + (credit + cheque + card));
                    balance.Text = balanceF + "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Empty Data to Remove from Table");
                // code.Focus();
            }
            else
            {
                try
                {
                    var y = dataGridView1.SelectedRows[0].Index;
                    balanceF = balanceF + (Double.Parse(dataGridView1.Rows[y].Cells[0].Value.ToString()));
                    card = card - Double.Parse(dataGridView1.Rows[y].Cells[0].Value.ToString());
                    dataGridView1.Rows.RemoveAt(y);
                    balance.Text = balanceF + "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Select Row to Delete");
                }
            }
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