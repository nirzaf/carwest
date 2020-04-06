using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class ChequeView : Form
    {
        public ChequeView()
        {
            InitializeComponent();
        }
        DataTable dt; DataSet ds;
        SqlConnection sqlconn, conn2;
        SqlDataReader reader, reader2;
        private void ChequeView_Load(object sender, EventArgs e)
        {

            this.TopMost = true;
            this.ActiveControl = bankName;
            db = new DB();
            sqlconn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn = db3.createSqlConnection();
            try
            {
                sqlconn.Open();
                reader = new SqlCommand("select * from bank", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[1] + "-" + reader[2]);
                }
                sqlconn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            loadAutoCompleteAll();
            radioSend.Checked = true;
        }
        string setAmountFormat(string amount)
        {
            string amountI = (int)Double.Parse(amount) + "";

            double amountD = Double.Parse(amount);
            if (amountI.Length == 1)
            {
                amount = String.Format("{0:0.00}", amountD);
            }
            else if (amountI.Length == 2)
            {
                amount = String.Format("{0:00.00}", amountD);
            }
            else if (amountI.Length == 3)
            {
                amount = String.Format("{0:000.00}", amountD);
            }
            else if (amountI.Length == 4)
            {
                amount = String.Format("{0:0,000.00}", amountD);
            }
            else if (amountI.Length == 5)
            {
                amount = String.Format("{0:00,000.00}", amountD);
                ///price = "hu";
            }
            else if (amountI.Length == 6)
            {
                amount = String.Format("{0:000,000.00}", amountD);
            }
            else if (amountI.Length == 7)
            {
                amount = String.Format("{0:0,000,000.00}", amountD);
            }
            else if (amountI.Length == 8)
            {
                amount = String.Format("{0:00,000,000.00}", amountD);
            }
            else if (amountI.Length == 9)
            {
                amount = String.Format("{0:000,000,000.00}", amountD);
            }
            else if (amountI.Length == 10)
            {
                amount = String.Format("{0:0,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 11)
            {
                amount = String.Format("{0:00,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 12)
            {
                amount = String.Format("{0:000,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 13)
            {
                amount = String.Format("{0:0,000,000,000,000.00}", amountD);
            }

            return amount;
        }

        ArrayList arrayList;

        DB db, db2, db3;
        SqlConnection conn;
        String[] idArray;
        void loadAutoCompleteAll()
        {
            try
            {
                sqlconn.Open();
                reader = new SqlCommand("select bank from chequeSummery ", sqlconn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }

                sqlconn.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(bankName, idArray);
                sqlconn.Open();
                reader = new SqlCommand("select branch from chequeSummery ", sqlconn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }

                sqlconn.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(branch, idArray);

                sqlconn.Open();
                reader = new SqlCommand("select acNO from chequeSummery ", sqlconn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }

                sqlconn.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(acNo, idArray);

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }


        string month1, month2, date1, date2, amount, pay, date;
        private void LOAD_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Invalied Date");
            }

            else
            {
                sqlconn.Open();

                {
                    sqlconn.Close();
                    Cursor.Current = Cursors.WaitCursor;


                    amount = amountByNumber.Text;


                    try
                    {
                        pay = payName.Text;
                    }
                    catch (Exception)
                    {
                        pay = payName.Text;
                    }


                    //     MessageBox.Show(pay);

                    date = dateTimePicker1.Value.ToString().Split(' ')[0];
                    string states = "";


                    bool depositC = false;

                    if (cutomerID.Equals(""))
                    {
                        cutomerID = payName.Text;
                    }

                    if (radioSend.Checked)
                    {
                        if (radioAcPay.Checked)
                        {

                            states = new printCheque().setprintChequeAcPay(date, amount, pay, radioButton1.Checked);

                        }
                        else
                        {
                            if (!crossed.Checked)
                            {
                                states = new printCheque().setprintCheque(date, amount, "CASH", radioButton1.Checked);

                            }
                            else
                            {
                                states = new printCheque().setprintChequeCross(date, amount, "CASH", radioButton1.Checked);

                            }


                        }
                        depositC = true;
                        // saveInvoice2();
                    }
                    if (radioDeposit.Checked)
                    {
                        depositC = true;

                    }
                    if (states.Equals(""))
                    {
                        sqlconn.Open();
                        //    new SqlCommand("insert into chequeSummery values('" + bankName.Text + "','" + branch.Text + "','" + acNo.Text + "','" + radioRecivd.Checked + "','" + radioDeposit.Checked + "','" + radioSend.Checked + "','" + amountByNumber.Text + "','" + comboBox1.SelectedItem.ToString().Split('-')[1] + "','" + chqueNumber.Text + "','" + dateTimePicker1.Value + "','" + false + "','" + depositC + "','" + cutomerID + "')", sqlconn).ExecuteNonQuery();
                        sqlconn.Close();
                        MessageBox.Show("Cheque Send to Printer to Print Succefully");
                        payName.Text = "";
                        amountByNumber.Text = "";
                        payName.Focus();

                        radioAcPay.Checked = true;
                        dateTimePicker1.Value = DateTime.Now;
                        bankName.Text = "";
                        branch.Text = "";
                        acNo.Text = "";
                        radioRecivd.Checked = true;
                        radioAcPay.Checked = true;

                        bankName.Focus();
                        panelSend.Enabled = true;
                    }
                    else
                    {

                        MessageBox.Show(" Something Issue with These Reasoen : " + states);
                    }
                }
                this.Dispose();

            }
            sqlconn.Close();
            Cursor.Current = Cursors.Default;
        }

        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        string userH, tempInvoiceNO;
        Double amountCost, amountPaid, amountTemp, amountTemp2;
        public string[] creditDetail, chequeDetail, cardDetail;
        Int32 tempcreditPaidID, dbID;
        public Double paidAmount, cashPaidDB;
        Int32 getID()
        {
            dbID = 0;

            try
            {
                conn.Open();
                reader = new SqlCommand("select max(id) from receipt2", conn).ExecuteReader();
                if (reader.Read())
                {
                    dbID = reader.GetInt32(0);
                    dbID++;
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                dbID = 1;
            }
            return dbID;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }
        void saveInvoice2()
        {
            try
            {
                recepitNo2.Text = getID() + "";

                if (recepitNo2.Text.Equals(""))
                {
                    MessageBox.Show("Please enter Recepit NO");
                    recepitNo2.Focus();
                }
                else
                {
                    if (cashCheque.Text.Equals(""))
                    {
                        cashCheque.Text = "0";
                    }
                    var cus = "";
                    db.setCursoerWait();

                    tempInvoiceNO = "";
                    var amountD = Double.Parse(cashCheque.Text);

                    amountD = amountD + Double.Parse(amountByNumber.Text);

                    amountTemp2 = amountD;
                    amountTemp = 0;
                    conn.Open();
                    //MessageBox.Show(amountD+"");
                    new SqlCommand("insert into receipt2 values('" + recepitNo2.Text + "','" + dateTimePicker1.Value + "','" + "" + "','" + cutomerID + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + "" + "','" + "CHEQUE" + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();


                    tempcreditPaidID = Int32.Parse(recepitNo2.Text);


                    states = false;

                    conn2.Open();
                    tempInvoiceNO = "";
                    reader2 = new SqlCommand("select invoiceid,balance,amount from creditGRN where customerid='" + cutomerID + "' order by requstdate", conn2).ExecuteReader();
                    while (reader2.Read())
                    {

                        if (amountTemp2 != 0)
                        {
                            amountTemp = 0;

                            conn.Open();
                            reader = new SqlCommand("select paid from GRNCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
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
                                    new SqlCommand("insert into GRNcreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                    //  MessageBox.Show("1" + tempInvoiceNO);
                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/" + reader2[0];
                                    }



                                    amountTemp2 = 0;
                                }
                                else
                                {
                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                    conn.Open();
                                    new SqlCommand("insert into GRNCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/" + reader2[0];
                                    }
                                    amountTemp2 = amountTemp2 - amountTemp;

                                }
                            }
                        }
                    }
                    conn2.Close();
                    if (amountTemp2 > 0)
                    {
                        conn.Open();
                        new SqlCommand("insert into overPayC2 values('" + cutomerID + "','" + amountTemp2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    savesub2(amountD, tempInvoiceNO);
                    //new payment().updateOverPay("S-" + invoiceNO.Text, userH);




                    conn.Close();
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No " + a.Message + "/" + a.StackTrace);

                conn.Close();
            }
        }
        public void savesub2(double amountD, string tempInvoiceNOS)
        {

            conn.Open();
            new SqlCommand("insert into chequeGRN2 values ('" + tempcreditPaidID + "','" + cutomerID + "','" + amountByNumber.Text + "','" + 0 + "','" + amountByNumber.Text + "','" + chqueNumber.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + "" + "','" + true + "')", conn).ExecuteNonQuery();
            conn.Close();




            try
            {
                conn.Open();
                new SqlCommand("update receipt2 set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                conn.Close();

                conn.Open();
                new SqlCommand("insert into supplierStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Invoice -Cheque" + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception)
            {
                conn.Close();
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void amountByNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void d2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void d1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void m2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void m1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void y2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void y1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            ////e.Handled = true;
        }

        private void ChequeView_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkAmount_CheckedChanged(object sender, EventArgs e)
        {

        }
        string tempCustomer = "", cutomerID = "";
        public Boolean loadCustomer(string id)
        {
            //MessageBox.Show(id);
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    payName.Text = reader[2] + "";

                    cutomerID = reader[0] + "";
                    tempCustomer = reader[0] + "";
                }
                else
                {
                    states = false;

                    cutomerID = id;
                    tempCustomer = "";
                }
                amountByNumber.Focus();
                reader.Close();
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "nnnnnnnnnnnnnnnnnnnnnnn " + a.StackTrace);
                conn.Close();
                reader.Close();
            }
            return states;
        }
        private void payName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (payName.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    payName.Focus();
                }
                else
                {
                    loadCustomer(payName.Text);
                }
            }

            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox1.Visible)
                    {
                        listBox1.Focus();
                        listBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        amountByNumber.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void amountByNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(acNo, chqueNumber, chqueNumber, e.KeyValue);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCash.Checked)
            {
                payName.Enabled = false;
                crossed.Enabled = true;
            }
            else
            {
                payName.Enabled = true;
                crossed.Enabled = false;
            }

            chqueNumber.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCash.Checked)
            {
                payName.Enabled = false;
                crossed.Enabled = true;
            }
            else
            {
                payName.Enabled = true;
                crossed.Enabled = false;
            }
            chqueNumber.Focus();
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

        private void chqueNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                LOAD_Click(null, null);
            }
        }

        private void radioRecivd_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSend.Checked)
            {
                panelSend.Enabled = true;
            }
            else
            {
                panelSend.Enabled = false;
            }
        }

        private void radioDeposit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSend.Checked)
            {
                panelSend.Enabled = true;
            }
            else
            {
                panelSend.Enabled = false;
            }
        }

        private void radioSend_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSend.Checked)
            {
                panelSend.Enabled = true;
            }
            else
            {
                panelSend.Enabled = false;
            }
        }

        private void bankName_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(bankName, branch, branch, e.KeyValue);
        }

        private void branch_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(bankName, acNo, acNo, e.KeyValue);
        }

        private void acNo_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(acNo, amountByNumber, amountByNumber, e.KeyValue);
        }

        private void payName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | payName.Text.Equals("")))
            {
                db.setList(listBox1, payName, payName.Width);
                listBox1.Visible = true;

                listBox1.Height = 55;
                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,company from supplier where company like '%" + payName.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();
                    states = true;
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[1].ToString().ToUpper());
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states)
                    {
                        listBox1.Visible = false;
                    }
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }

            }
            if (payName.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void panelSend_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                payName.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
            loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            payName.Text = listBox1.SelectedItem.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new bankStatement().Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new bankSummery().Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new chequeD().Visible = true;
        }
    }
}
