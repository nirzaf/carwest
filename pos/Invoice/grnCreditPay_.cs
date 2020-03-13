using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class grnCreditPay_ : Form
    {
        private Form homeH;

        public grnCreditPay_(Form home, String user)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2, conn3;
        private SqlDataReader reader, reader2, reader3;
        private ArrayList arrayList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        private string userH, tempInvoiceNO;
        private Double amount, amountCost, amountPaid, amountTemp, amountTemp2;
        public string[] creditDetail, chequeDetail, cardDetail;
        private Int32 tempcreditPaidID, dbID;
        public Double paidAmount, cashPaidDB;
        // my Variable End
        //
        //Method

        private Int32 getID()
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

        private void loadInvoice(string id)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select company,auto from supplier where id='" + "S-" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    cusLabel.Text = reader[0] + "";
                }
                conn.Close();
                //MessageBox.Show(id + "");
                invoiceNO.Text = id;
                amountCost = 0;
                amountPaid = 0;

                conn.Close();
                load();
                cash.Focus();
                cash.SelectionLength = cash.Text.Length;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private double tSettle1;

        private bool checkCells()
        {
            check = true;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                if (i != 0 && dataGridView4.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    if (check)
                    {
                        try
                        {
                            tSettle1 = Double.Parse(dataGridView4.Rows[i].Cells[4].Value.ToString());

                            if (tSettle1 > Double.Parse(dataGridView4.Rows[i].Cells[8].Value.ToString()))
                            {
                                MessageBox.Show("Sorry You Have Settle Exceed Value more Than Invoice Balance");
                                check = false;
                                dataGridView4.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                            }
                            else
                            {
                                dataGridView4.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Input Value not in Correct Foramt");
                            check = false;
                            dataGridView4.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        }
                    }
                }
            }
            return check;
        }

        private double tempCheque = 0;

        public void savesub2(double amountD, string tempInvoiceNOS)
        {
            tempCheque = 0;

            tempCheque = tempCheque + Double.Parse(chequeAmount.Text);
            conn.Open();
            new SqlCommand("insert into chequegrn2 values ('" + tempcreditPaidID + "','" + "S-" + invoiceNO.Text + "','" + chequeAmount.Text + "','" + 0 + "','" + chequeAmount.Text + "','" + chequeNo.Text + "','" + dateTimePicker3.Value + "','" + dateTimePicker1.Value + "','" + "" + "','" + true + "')", conn).ExecuteNonQuery();
            conn.Close();

            conn.Open();
            new SqlCommand("insert into chequeSummery values('" + tempcreditPaidID + "','" + "" + "','" + "" + "','" + true + "','" + false + "','" + false + "','" + chequeAmount.Text + "','" + comboBank.SelectedItem.ToString().Split('-')[0] + "','" + chequeNo.Text + "','" + dateTimePicker3.Value + "','" + false + "','" + true + "','" + "S-" + invoiceNO.Text + "','" + "S-" + invoiceNO.Text + "','" + "" + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
            // new SqlCommand("insert into chequeSummery values('" + invoieNoTemp + "','" + "" + "','" + "" + "','" + true + "','" + false + "','" + false + "','" + chequeAmount.Text + "','" + comboBank.SelectedItem.ToString().Split('-')[0] + "','" + cheQueNumber.Text + "','" + chequeDate.Value + "','" + false + "','" + true + "','" + cutomerID + "','" + cutomerID + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "')", conn).ExecuteNonQuery();

            conn.Close();
            conn.Open();
            new SqlCommand("insert into cashSummery values('" + "GRN Credit Paid Cheque-" + tempcreditPaidID + "','" + "SUPPLIER -" + cusLabel.Text + "','" + chequeAmount.Text + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
            conn.Close();

            try
            {
                conn.Open();
                new SqlCommand("update receipt2 set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                conn.Close();

                conn.Open();
                new SqlCommand("insert into supplierStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Invoice -Cheque" + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + "S-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            //            if ((MessageBox.Show("Print Reciept ?", "Confirmation",
            //MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
            //MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            //            {
            //                conn.Close();
            //                new invoicePrint().setprintHalfInvoiceServiceRE(recepitNo.Text, "C-" + invoiceNO.Text, "CHEQUE", dataGridView4, 0 + "", tempCheque + "", 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");
            //                conn.Close();

            //            }
            var a = invoiceNO.Text;
            //MessageBox.Show("1");
            clear();
            //  MessageBox.Show("2");
            loadInvoice(a);
            // MessageBox.Show("2ss");
        }

        private double amount2;

        private void saveInvoice()
        {
            try
            {
                recepitNo.Text = getID() + "";

                if (recepitNo.Text.Equals(""))
                {
                    MessageBox.Show("Please enter Recepit NO");
                    recepitNo.Focus();
                }
                else if (radioCash.Checked && (Double.Parse(totalOutstanding.Text) < Double.Parse(cash.Text)))
                {
                    MessageBox.Show("Payble Amount Exceed Outstanding Amount");
                    cash.Focus();
                }
                else if (radioCard.Checked && (Double.Parse(totalOutstanding.Text) < Double.Parse(cash.Text)))
                {
                    MessageBox.Show("Payble Amount Exceed Outstanding Amount");
                    cash.Focus();
                }
                else if (radioCheque.Checked && (Double.Parse(totalOutstanding.Text) < Double.Parse(chequeAmount.Text)))
                {
                    MessageBox.Show("Payble Amount Exceed Outstanding Amount");
                    chequeAmount.Focus();
                }
                else
                {
                    var cus = "";
                    db.setCursoerWait();

                    // MessageBox.Show(recepitNo.Text + "");
                    var amountD = Double.Parse(cash.Text);
                    amountTemp2 = amountD;
                    amountTemp = 0;
                    conn.Open();
                    //MessageBox.Show(amountD+"");
                    if (radioCash.Checked)
                    {
                        new SqlCommand("insert into receipt2 values('" + recepitNo.Text + "','" + dateTimePicker1.Value + "','" + "" + "','" + "S-" + invoiceNO.Text + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + cutomerID2 + "','" + "CASH" + "','" + userH + "')", conn).ExecuteNonQuery();
                    }
                    else
                    {
                        new SqlCommand("insert into receipt2 values('" + recepitNo.Text + "','" + dateTimePicker1.Value + "','" + "" + "','" + "S-" + invoiceNO.Text + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + cutomerID2 + "','" + "CARD" + "','" + userH + "')", conn).ExecuteNonQuery();
                    }
                    conn.Close();

                    tempcreditPaidID = Int32.Parse(recepitNo.Text);

                    tempInvoiceNO = "";
                    states = false;
                    if (checkBox2.Checked)
                    {
                        conn2.Open();

                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditgRN where customerid='" + "S-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            if (amountTemp2 != 0)
                            {
                                amountTemp = 0;

                                conn.Open();
                                reader = new SqlCommand("select paid from grnCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
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
                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                            new SqlCommand("insert into overPayS values('" + "S-" + invoiceNO.Text + "','" + amountTemp2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        saveSub(amountD, tempInvoiceNO);
                        //new payment().updateOverPay("C-" + invoiceNO.Text, userH);
                    }
                    else
                    {
                        try
                        {
                            amount2 = amountTemp2;
                            if (checkCells())
                            {
                                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                                {
                                    if (dataGridView4.Rows[i].Cells[7].Value.ToString().ToUpper().Equals("TRUE"))
                                    {
                                        if (amount2 != 0)
                                        {
                                            amount = 0;

                                            amount = Double.Parse(dataGridView4.Rows[i].Cells[8].Value.ToString());

                                            if (amount != 0)
                                            {
                                                if (amount2 <= amount)
                                                {
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView4.Rows[i].Cells[1].Value + "','" + dataGridView4.Rows[i].Cells[4].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    //     new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();

                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView4.Rows[i].Cells[1].Value + "','" + dataGridView4.Rows[i].Cells[4].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView4.Rows[i].Cells[1];
                                                    }
                                                    amount2 = amount2 - amount;
                                                }
                                            }
                                        }
                                    }
                                }
                                //   MessageBox.Show(amount2+"");
                                if (amount2 > 0)
                                {
                                    if ((MessageBox.Show("There have more Balance to Settle (" + amount2 + ") . Do you need not settle Invoice with AUTO ??", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                    {
                                        conn2.Open();
                                        states = false; ;
                                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditGrN where customerid='" + "S-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                                        while (reader2.Read())
                                        {
                                            if (amount2 != 0)
                                            {
                                                amount = 0;

                                                conn.Open();
                                                reader = new SqlCommand("select paid from grnCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    states = true;
                                                    amount = amount + reader.GetDouble(0);
                                                }

                                                conn.Close();
                                                if (!states)
                                                {
                                                    amount = amount + reader2.GetDouble(1);
                                                }
                                                else
                                                {
                                                    amount = reader2.GetDouble(1) - amount;
                                                }

                                                if (amount != 0)
                                                {
                                                    if (amount2 <= amount)
                                                    {
                                                        conn.Open();
                                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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

                                                        amount2 = 0;
                                                    }
                                                    else
                                                    {
                                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                                        conn.Open();
                                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();

                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/" + reader2[0];
                                                        }
                                                        amount2 = amount2 - amount;
                                                    }
                                                }
                                            }
                                        }
                                        conn2.Close();
                                    }
                                }

                                if (amount2 > 0)
                                {
                                    conn.Open();
                                    new SqlCommand("insert into overPayS values('" + "S-" + invoiceNO.Text + "','" + amount2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                saveSub(amountTemp2, tempInvoiceNO);
                                // new payment().updateOverPay("C-" + customer, user);
                                ///  this.Dispose();
                            }
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message + " " + a.StackTrace);
                        }
                    }
                    //   MessageBox.Show(tempcreditPaidID+"/www");
                    // MessageBox.Show(recepitNo.Text);
                    db.setCashBalance(DateTime.Now);
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No 0" + a.Message + "/" + a.StackTrace);
                recepitNo.Focus();
                conn.Close();
            }
        }

        private void saveInvoice2()
        {
            try
            {
                recepitNo.Text = getID() + "";

                if (recepitNo.Text.Equals(""))
                {
                    MessageBox.Show("Please enter Recepit NO");
                    recepitNo.Focus();
                }
                else
                {
                    var cus = "";
                    db.setCursoerWait();

                    tempInvoiceNO = "";
                    var amountD = 0.0;

                    amountD = amountD + Double.Parse(chequeAmount.Text);

                    amountTemp2 = amountD;
                    amountTemp = 0;
                    conn.Open();
                    //MessageBox.Show(amountD+"");
                    new SqlCommand("insert into receipt2 values('" + recepitNo.Text + "','" + dateTimePicker1.Value + "','" + "" + "','" + "S-" + invoiceNO.Text + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + cutomerID2 + "','" + "CHEQUE" + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    tempcreditPaidID = Int32.Parse(recepitNo.Text);

                    states = false;
                    if (checkBox2.Checked)
                    {
                        conn2.Open();
                        tempInvoiceNO = "";
                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditGRN where customerid='" + "S-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            if (amountTemp2 != 0)
                            {
                                amountTemp = 0;

                                conn.Open();
                                reader = new SqlCommand("select paid from grnCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
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
                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                            new SqlCommand("insert into overPayS values('" + "S-" + invoiceNO.Text + "','" + amountTemp2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        //  MessageBox.Show("A");
                        savesub2(amountD, tempInvoiceNO);
                        //new payment().updateOverPay("C-" + invoiceNO.Text, userH);
                        //  MessageBox.Show("A1");
                    }
                    else
                    {
                        try
                        {
                            amount2 = amountTemp2;
                            if (checkCells())
                            {
                                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                                {
                                    if (dataGridView4.Rows[i].Cells[7].Value.ToString().ToUpper().Equals("TRUE"))
                                    {
                                        if (amount2 != 0)
                                        {
                                            amount = 0;

                                            amount = Double.Parse(dataGridView4.Rows[i].Cells[8].Value.ToString());

                                            if (amount != 0)
                                            {
                                                if (amount2 <= amount)
                                                {
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView4.Rows[i].Cells[1].Value + "','" + dataGridView4.Rows[i].Cells[4].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView4.Rows[i].Cells[1].Value + "','" + dataGridView4.Rows[i].Cells[4].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView4.Rows[i].Cells[1].Value;
                                                    }
                                                    amount2 = amount2 - amount;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (amount2 > 0)
                                {
                                    if ((MessageBox.Show("There have more Balance to Settle (" + amount2 + ") .. Do you need not settle Invoice with AUTO ??", "Confirmation",
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                    {
                                        conn2.Open();
                                        states = false; ;
                                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditGRN where customerid='" + "S-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                                        while (reader2.Read())
                                        {
                                            if (amount2 != 0)
                                            {
                                                amount = 0;

                                                conn.Open();
                                                reader = new SqlCommand("select paid from grnCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    states = true;
                                                    amount = amount + reader.GetDouble(0);
                                                }

                                                conn.Close();
                                                if (!states)
                                                {
                                                    amount = amount + reader2.GetDouble(1);
                                                }
                                                else
                                                {
                                                    amount = reader2.GetDouble(1) - amount;
                                                }

                                                if (amount != 0)
                                                {
                                                    if (amount2 <= amount)
                                                    {
                                                        conn.Open();
                                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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

                                                        amount2 = 0;
                                                    }
                                                    else
                                                    {
                                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                                        conn.Open();
                                                        new SqlCommand("insert into grnCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();

                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/" + reader2[0];
                                                        }
                                                        amount2 = amount2 - amount;
                                                    }
                                                }
                                            }
                                        }
                                        conn2.Close();
                                    }
                                }

                                if (amount2 > 0)
                                {
                                    conn.Open();
                                    new SqlCommand("insert into overPayS values('" + "S-" + invoiceNO.Text + "','" + amount2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                savesub2(amountTemp2, tempInvoiceNO);
                                // new payment().updateOverPay("C-" + customer, user);
                                ///  this.Dispose();
                            }
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message + " " + a.StackTrace);
                        }
                    }

                    conn.Close();
                    // new invoicePrint().setprintHalfInvoiceServiceRE(tempcreditPaidID + "", conn, reader, "C-" + invoiceNO.Text);
                    // new invoicePrint().setprintHalfInvoiceService("", "C-" + invoiceNO.Text, "CASH", dataGridView4, 0 + "", cash.Text, 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");

                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No" + "/" + a.Message + "/" + a.StackTrace);
                recepitNo.Focus();
                conn.Close();
            }
        }

        public void saveSub(double amountD, string tempInvoiceNOS)
        {
            try
            {
                try
                {
                    conn.Open();
                    new SqlCommand("update receipt2 set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into cashSummery values('" + "GRN Credit Paid-" + tempcreditPaidID + "','" + "SUPPLIER -" + cusLabel.Text + "','" + amountD + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into supplierStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for GRN " + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + "S-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    //                    if ((MessageBox.Show("Print Reciept ?", "Confirmation",
                    //MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    //MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    //                    {
                    //                        conn.Close();
                    //                        new invoicePrint().setprintHalfInvoiceServiceRE(recepitNo.Text, "C-" + invoiceNO.Text, "CASH", dataGridView4, 0 + "", cash.Text, 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");
                    //                        conn.Close();

                    //                    }
                    var a = invoiceNO.Text;
                    clear();
                    loadInvoice(a);
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void clear()
        {
            // MessageBox.Show("21");
            dateTimePicker1.Value = dateTimePicker1.Value;
            cutomerID2 = "";
            chequeAmount.Text = "";
            chequeNo.Text = "";
            totalOutstanding.Text = "";
            checkBox2.Checked = true;
            checkBox2.Checked = false;
            checkBox2.Checked = true;
            idTemp2 = 0;
            //button2.Enabled = false;
            invoiceNO.Text = "";
            recepitNo.Text = "";
            dataGridView4.Rows.Clear();
            invoiceNoT.Text = "";
            checkBox2.Checked = true;
            cash.Text = "0";
            totalOutstanding.Text = "";
            recepitNo.Focus();
            cusLabel.Text = "";
            setBalance();
        }

        private Boolean checkTerm()
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

        private double amount3, amount4, retrunAmount;

        private void load()
        {
            try
            {
                //  MessageBox.Show("3");
                totalOut = 0;
                amount3 = 0;
                amount4 = 0;
                checkBox2.Checked = false;
                dataGridView4.Rows.Clear();
                try
                {
                    conn2.Open();
                    amount = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();

                    reader2 = new SqlCommand("select sum(a.balance) from creditGRN as a,invoiceTerm as b where a.customerid='" + "S-" + invoiceNO.Text + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        //      amount = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount = 0;
                    conn2.Close();
                }
                //  MessageBox.Show(amount + "A");
                //try
                //{
                //    conn2.Open();
                //    // MessageBox.Show(customer);
                //    arrayList = new ArrayList();
                //    reader2 = new SqlCommand("select amount from cusBalance where customer='" + "C-" + invoiceNO.Text + "' and date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + "" + "' ", conn2).ExecuteReader();
                //    if (reader2.Read())
                //    {
                //        amount = amount + reader2.GetDouble(0);
                //    }
                //    conn2.Close();
                //}
                //catch (Exception)
                //{
                //    amount = 0;
                //    conn2.Close();
                //}
                //   MessageBox.Show(amount + "B");
                try
                {
                    conn2.Open();
                    amount2 = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select sum(amount2) from receipt2 where customer='" + "S-" + invoiceNO.Text + "' and date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + "" + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        //       amount2 = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount2 = 0;
                    conn2.Close();
                }
                //   MessageBox.Show(amount + "C");
                // totalOut = amount - amount2;

                dataGridView4.Rows.Clear();
                //  dataGridView4.Rows.Add("B/F", "", "", "", "", "", db.setAmountFormat(totalOut + ""), false, "0", "");
                //MessageBox.Show(invoiceNO.Text);
                //    var a = ;
                //  for (int i = 1; i <= Int32.Parse(db.getLastDate(DateTime.Now.Month, DateTime.Now.Year)); i++)
                {
                    // MessageBox.Show(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i);
                    //try
                    //{
                    //    conn2.Open();
                    //    amount = 0;
                    //    // MessageBox.Show(customer);
                    //    arrayList = new ArrayList();
                    //    reader2 = new SqlCommand("select * from cusBalance where  customer='" + "C-" + invoiceNO.Text + "' and date between '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' and '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                    //    if (reader2.Read())
                    //    {
                    //        amount3 = amount3 + reader2.GetDouble(2);
                    //        totalOut = totalOut + reader2.GetDouble(2);
                    //        dataGridView4.Rows.Add(reader2.GetDateTime(3).ToShortDateString(), "BALANCE", db.setAmountFormat(reader2[2] + ""), "CREDIT INVOICE", db.setAmountFormat(reader2[2] + ""), 0.0, db.setAmountFormat(totalOut + ""), "OPEN");
                    //    }
                    //    conn2.Close();
                    //}
                    //catch (Exception a)
                    //{
                    //    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //    // throw;
                    //}

                    conn2.Open();
                    amount = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select a.* from creditGRN as a,GRNTerm as b where a.customerid='" + "S-" + invoiceNO.Text + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "'  order by a.date", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        retrunAmount = 0;
                        try
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select amount from returnGoods where invoiceID='" + reader2[0] + "'", conn3).ExecuteReader();
                            while (reader3.Read())
                            {
                                retrunAmount = retrunAmount + (reader3.GetDouble(0));
                            }
                            conn3.Close();
                        }
                        catch (Exception)
                        {
                            conn3.Close();
                        }
                        amount3 = amount3 + reader2.GetDouble(4);
                        amount3 = amount3 - retrunAmount;
                        totalOut = totalOut + reader2.GetDouble(4);
                        totalOut = totalOut - retrunAmount;

                        amountPaid = 0;
                        try
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select paid,balance from grnCreditPaid where invoiceID='" + reader2[0] + "'", conn3).ExecuteReader();
                            while (reader3.Read())
                            {
                                amountPaid = amountPaid + (reader3.GetDouble(0) - reader3.GetDouble(1));
                            }
                            conn3.Close();
                        }
                        catch (Exception)
                        {
                            conn3.Close();
                        }
                        totalOut = totalOut - amountPaid;
                        dataGridView4.Rows.Add(reader2.GetDateTime(6).ToShortDateString(), "" + reader2[0], db.setAmountFormat(reader2[2] + ""), "CREDIT GRN", (reader2.GetDouble(4) - retrunAmount + ""), amountPaid, db.setAmountFormat(totalOut + ""), false, "0", "OPEN");
                    }
                    conn2.Close();

                    //conn2.Open();
                    //amount2 = 0;

                    ////  MessageBox.Show("5");
                    //// MessageBox.Show(customer);
                    //arrayList = new ArrayList();
                    //reader2 = new SqlCommand("select * from receipt2 where customer='" + "S-" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    //while (reader2.Read())
                    //{
                    //    amount4 = amount4 + reader2.GetDouble(5);
                    //    totalOut = totalOut - reader2.GetDouble(5);
                    //    dataGridView4.Rows.Add(reader2.GetDateTime(1).ToShortDateString(), reader2[0], db.setAmountFormat(reader2[5] + ""), reader2[8], 0.0, db.setAmountFormat(reader2[5] + ""), db.setAmountFormat(totalOut + ""), false, "0", "PRINT");
                    //}
                    //conn2.Close();
                }
                checkBox2.Checked = true;
                totalOutstanding.Text = db.setAmountFormat(totalOut + "");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
            //  MessageBox.Show("5");
        }

        private string[] idArray;

        //,
        private double totalOut;

        private void setBalance()
        {
            try
            {
                if (cash.Text.Equals(""))
                {
                    cash.Text = "0";
                }
                var amount = 0.0;
                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    if (dataGridView4.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView4.Rows[i].Cells[5].Value.ToString());
                    }
                }

                balance.Text = Math.Round(Double.Parse(cash.Text) - amount, 2) + "";
            }
            catch (Exception a)
            {
                // MessageBox.Show(a.Message);
            }
        }

        private void setBalance2()
        {
            try
            {
                var amount = 0.0;

                amount = amount + Double.Parse(chequeAmount.Text);

                amount = 0;
                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    if (dataGridView4.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView4.Rows[i].Cells[5].Value.ToString());
                    }
                }

                balance.Text = Math.Round(Double.Parse(chequeAmount.Text) - amount, 2) + "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void invoicePay_Load(object sender, EventArgs e)
        {
            radioCheque.Checked = true;
            radioCash.Checked = true;
            dataGridView4.AllowUserToAddRows = false;
            //      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            invoiceNO.Focus();
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();
            db = new DB();
            conn3 = db.createSqlConnection();

            this.TopMost = true;
            dataGridView4.AllowUserToAddRows = false;
            this.ActiveControl = recepitNo;

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from bank ", conn).ExecuteReader();
                if (reader.Read())
                {
                    comboBank.Items.Add(reader[2] + "-" + reader[1]);
                }
                comboBank.SelectedIndex = 0;
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            clear();

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView4.Columns.Add(btn);
            btn.Width = 120;
            btn.Text = "OPEN / PRINT";

            btn.UseColumnTextForButtonValue = true;
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
            //if ((e.KeyChar == '.')) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            //e.Handled = true;
        }

        private void invoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (invoiceNO.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    invoiceNO.Focus();
                }
                else
                {
                    loadInvoice(invoiceNO.Text);
                    cash.Focus();
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
                        cash.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void cash_KeyUp(object sender, KeyEventArgs e)
        {
            setBalance();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (recepitNo.Text.Equals(""))
            {
                MessageBox.Show("Please enter Recepit NO");
                recepitNo.Focus();
            }
            else
            {//MessageBox.Show(customer);
                //    new termXash9(this, Double.Parse(creditAmount.Text), invoiceNO.Text, "C-" + invoiceNO.Text, cutomerID2, dateTimePicker1.Value,recepitNo.Text).Visible = true;
            }
        }

        private void invoiceNO_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | invoiceNO.Text.Equals("")))
            {
                db.setList(listBox1, invoiceNO, invoiceNO.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select ID,company from SUPPLIER where description like '%" + invoiceNO.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().Split('-')[1] + " " + reader[1].ToString().ToUpper());
                        listBox1.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
            if (invoiceNO.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                invoiceNO.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                //  idTemp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
                // companyC.SelectionLength code= code.MaxLength;
                loadInvoice(listBox1.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
            loadInvoice(listBox1.SelectedItem.ToString().Split(' ')[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!cash.Equals("") && Double.Parse(cash.Text) != 0)
            {
                conn.Open();
                new SqlCommand("insert into supplierStatement values('" + "CASH RETURN" + "','" + "Payble Amount of Return-GRN" + "','" + 0 + "','" + -(Double.Parse(cash.Text)) + "','" + true + "','" + dateTimePicker1.Value + "','" + "C-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                new SqlCommand("insert into cashSummery values ('" + "CASH RETURN" + "','" + "Return Cash above the Recived Credit Settelmnt-Supplier " + cusLabel.Text + "','" + cash.Text + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                //MessageBox.Show(amountD+"");
                new SqlCommand("insert into receipt2 values('" + dateTimePicker1.Value + "','" + "" + "','" + "S-" + invoiceNO.Text + "','" + new amountByName().setAmountName(cash.Text + "") + "','" + cash.Text + "','" + "Cash return on CRedit Settele ISsue" + "','" + "" + "','" + "CASH" + "','" + userH + "')", conn).ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Invalied Amount for a Return");
                cash.Focus();
            }
        }

        private void saleRef_TextChanged(object sender, EventArgs e)
        {
        }

        private void label29_Click(object sender, EventArgs e)
        {
        }

        private string codeC2, cutomerID2;
        private Int32 idTemp2;

        public Boolean loadCustomer2(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                //  MessageBox.Show(id);
                reader = new SqlCommand("select * from salesRef where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    codeC2 = id;
                    cash.Text = reader.GetString(1);
                }
                else
                {
                    states = false;
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                cash.Focus();
                cutomerID2 = id;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
            return states;
        }

        private void saleRef_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void saleRef_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                cash.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                idTemp2 = Int32.Parse(listBox2.SelectedItem.ToString().Split(' ')[0]);
                // companyC.SelectionLength code= code.MaxLength;
                loadCustomer2(idTemp2 + "");
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;

            idTemp2 = Int32.Parse(listBox2.SelectedItem.ToString().Split(' ')[0]);
            loadCustomer2(idTemp2 + "");
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            idTemp2 = Int32.Parse(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void saleRef_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void recepitNo_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(invoiceNO, invoiceNO, invoiceNO, e.KeyValue);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = !checkBox2.Checked;
            invoiceNoT.Focus();
        }

        private void invoiceNoT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    for (int i = 0; i < dataGridView4.Rows.Count; i++)
                    {
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString().Equals(invoiceNoT.Text))
                        {
                            dataGridView4.Rows[i].Selected = true;
                            dataGridView4.Rows[i].Cells[4].Value = true;
                            break;
                        }
                    }
                    setBalance();
                }
                catch (Exception)
                {
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saveInvoice();
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            checkCells();
        }

        private void invoiceNO2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void invoiceNO2_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void saleRef2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void saleRef2_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void listBox4_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox4_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chequeCodeNo_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void chequeAmount_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void invoiceNoT2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            setBalance2();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            saveInvoice2();
        }

        private void recepitNo2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                recepitNo.Focus();
            }
            else
            {
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void cashCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar == '.')) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void dataGridView4_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView4_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            setBalance();
        }

        private void dataGridView4_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView4_Leave(object sender, EventArgs e)
        {
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView4_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            setBalance();
        }

        private void dataGridView4_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            setBalance();
        }

        private void cashCheque_KeyUp(object sender, KeyEventArgs e)
        {
            setBalance2();
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            setBalance2();
        }

        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            setBalance2();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void sUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new grnCreditPaidedit_(this, userH).Visible = true;
        }

        private void cUSTOMERSTATEMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new supplierOutstanding(this, userH).Visible = true;
        }

        private void cUSTOMERSTATEMENTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new supplierOutstanding2(this, userH).Visible = true;
        }

        private void invoiceNO_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                customerStatement statmentH = new customerStatement(this, userH, true);
                statmentH.Visible = true;
                statmentH.loadBank(DateTime.Now, "S-" + invoiceNO.Text);
                statmentH.Text = "STATEMENT OF " + "S-" + invoiceNO.Text + " ABOVE " + DateTime.Now.Year + "/" + db.getMOnthName(DateTime.Now.Month.ToString()).ToUpper();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            customerStatement aa = new customerStatement(this, "S-" + invoiceNO.Text, false);
            aa.Visible = true;
            aa.loadBank(DateTime.Now, "S-" + invoiceNO.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                try
                {
                    if (dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-').Length > 1)
                    {
                        new invoiceNewView(this, userH, dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;
                    }
                    else
                    {
                        conn.Close();
                        new invoicePrint().setprintHalfInvoiceServiceRE(dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString(), "S-" + invoiceNO.Text, "CASH", dataGridView4, 0 + "", cash.Text, 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");
                        conn.Close();
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            else if (e.ColumnIndex == 8)
            {
            }
        }

        private void radioCash_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCash.Checked || radioCard.Checked)
            {
                cash.Focus();
                cash.Enabled = true;
                panelChequ.Enabled = false;
            }
            else
            {
                cash.Enabled = false;
                panelChequ.Enabled = true;
                chequeAmount.Focus();
            }
        }

        private void radioCard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCash.Checked || radioCard.Checked)
            {
                cash.Focus();
                cash.Enabled = true;
                panelChequ.Enabled = false;
            }
            else
            {
                cash.Enabled = false;
                panelChequ.Enabled = true;
                chequeAmount.Focus();
            }
        }

        private void radioCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCash.Checked || radioCard.Checked)
            {
                cash.Focus();
                cash.Enabled = true;
                panelChequ.Enabled = false;
            }
            else
            {
                cash.Enabled = false;
                panelChequ.Enabled = true;
                chequeAmount.Focus();
            }
        }

        private void chequeAmount_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(invoiceNO, chequeNo, chequeNo, e.KeyValue);
        }

        private void chequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                dateTimePicker3.Focus();
            }
            else if (e.KeyValue == 38)
            {
                chequeAmount.Focus();
            }
        }

        private void dateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                saveInvoice2();
            }
            else if (e.KeyValue == 38)
            {
                chequeNo.Focus();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //customerStatement aa = new customerStatement(this, "S-" + invoiceNO.Text, true);
            //aa.Visible = true;
            //aa.loadBank(DateTime.Now, "S-" + invoiceNO.Text);
            saveInvoice2();
        }

        private void sALESUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new saleSummery(this, userH).Visible = true;
        }

        private void dataGridView4_KeyUp(object sender, KeyEventArgs e)
        {
            double a = 0.0;
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                try
                {
                    a = a + Double.Parse(dataGridView4.Rows[i].Cells[8].Value.ToString());
                }
                catch (Exception)
                {
                }
            }

            cash.Text = a + "";
        }
    }
}