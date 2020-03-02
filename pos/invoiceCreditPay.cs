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
    public partial class invoiceCreditPay : Form
    {
        Form homeH;
        public invoiceCreditPay(Form home, String user)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
        }
        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2, conn3;
        SqlDataReader reader, reader2, reader3;
        ArrayList arrayList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, dateNow, changeInvoiceDifDate;
        string userH, tempInvoiceNO;
        Double amount, amountCost, amountPaid, amountTemp, amountTemp2;
        public string[] creditDetail, chequeDetail, cardDetail;
        Int32 tempcreditPaidID;
        public Double paidAmount, cashPaidDB;
        // my Variable End
        //
        //Method





        void loadInvoice2(string id)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select company,auto from customer where auto='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    cusLabel2.Text = reader[0] + "";
                    id = reader[1] + "";
                    //  button2.Enabled = true;
                }
                conn.Close();
                invoiceNO2.Text = id;
                amountCost = 0;
                amountPaid = 0;



                conn.Close();
                load2();
                saleRef2.Focus();
                chequeAmount.SelectionLength = chequeAmount.Text.Length;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        void loadInvoice(string id)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select company,auto from customer where auto='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    cusLabel.Text = reader[0] + "";
                    id = reader[1] + "";
                    //  button2.Enabled = true;
                }
                conn.Close();
                invoiceNO.Text = id;
                amountCost = 0;
                amountPaid = 0;



                conn.Close();
                load();
                saleRef.Focus();
                cash.SelectionLength = cash.Text.Length;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        double tSettle1;
        bool checkCells()
        {
            check = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (check)
                {
                    try
                    {
                        tSettle1 = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());


                        if (tSettle1 > Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()))
                        {
                            MessageBox.Show("Sorry You Have Settle Exceed Value more Than Invoice Balance");
                            check = false;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        }
                        else
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Input Value not in Correct Foramt");
                        check = false;
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                    }


                }
            }
            return check;
        }
        public void savesub2(double amountD, string tempInvoiceNOS)
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                conn.Open();
                new SqlCommand("insert into chequeInvoiceRetail2 values ('" + tempcreditPaidID + "','" + "C-" + invoiceNO2.Text + "','" + dataGridView3.Rows[i].Cells[0].Value + "','" + 0 + "','" + dataGridView3.Rows[i].Cells[0].Value + "','" + dataGridView3.Rows[i].Cells[1].Value + "','" + dataGridView3.Rows[i].Cells[2].Value + "','" + dateTimePicker2.Value + "','" + "" + "','" + true + "')", conn).ExecuteNonQuery();
                conn.Close();
            }



            try
            {
                conn.Open();
                new SqlCommand("update receipt set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                conn.Close();

                conn.Open();
                new SqlCommand("insert into customerStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Invoice -Cheque" + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker2.Value + "','" + "C-" + invoiceNO2.Text + "')", conn).ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception)
            {
                conn.Close();
            }

            clear2();
        }
        bool checkCells2()
        {
            check = true;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (check)
                {
                    try
                    {
                        tSettle1 = Double.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());


                        if (tSettle1 > Double.Parse(dataGridView2.Rows[i].Cells[7].Value.ToString()))
                        {
                            MessageBox.Show("Sorry You Have Settle Exceed Value more Than Invoice Balance");
                            check = false;
                            dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        }
                        else
                        {
                            dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Input Value not in Correct Foramt");
                        check = false;
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                    }


                }
            }
            return check;
        }
        double amount2;
        void saveInvoice()
        {
            try
            {
                if (recepitNo.Text.Equals(""))
                {
                    MessageBox.Show("Please enter Recepit NO");
                    recepitNo.Focus();
                }
                else
                {
                    var cus = "";
                    db.setCursoerWait();


                    var amountD = Double.Parse(cash.Text);
                    amountTemp2 = amountD;
                    amountTemp = 0;
                    conn.Open();
                    //MessageBox.Show(amountD+"");
                    new SqlCommand("insert into receipt values('" + recepitNo.Text + "','" + dateTimePicker1.Value + "','" + "" + "','" + "C-" + invoiceNO.Text + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + cutomerID2 + "','" + "CASH" + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();


                    tempcreditPaidID = Int32.Parse(recepitNo.Text);

                    tempInvoiceNO = "";
                    states = false;
                    if (checkBox1.Checked)
                    {
                        conn2.Open();

                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
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
                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                        conn.Close();
                                        //  MessageBox.Show("1" + tempInvoiceNO);
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
                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                        conn.Open();
                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                        if (amountTemp2 > 0)
                        {
                            conn.Open();
                            new SqlCommand("insert into overPayC values('" + "C-" + invoiceNO.Text + "','" + amountTemp2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
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
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                                    {

                                        if (amount2 != 0)
                                        {
                                            amount = 0;

                                            amount = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                                            if (amount != 0)
                                            {
                                                if (amount2 <= amount)
                                                {
                                                    conn.Open();
                                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
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
                                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                                        while (reader2.Read())
                                        {

                                            if (amount2 != 0)
                                            {
                                                amount = 0;

                                                conn.Open();
                                                reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
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
                                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();
                                                        //  MessageBox.Show("1" + tempInvoiceNO);
                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "R-" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                                        }



                                                        amount2 = 0;
                                                    }
                                                    else
                                                    {
                                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                                        conn.Open();
                                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();

                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "R-" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
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
                                    new SqlCommand("insert into overPayC values('" + "C-" + invoiceNO.Text + "','" + amount2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
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

                    conn.Close();
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No");
                recepitNo.Focus();
                conn.Close();
            }
        }
        void saveInvoice2()
        {
            try
            {
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
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        amountD = amountD + Double.Parse(dataGridView3.Rows[i].Cells[0].Value.ToString());
                    }
                    amountTemp2 = amountD;
                    amountTemp = 0;
                    conn.Open();
                    //MessageBox.Show(amountD+"");
                    new SqlCommand("insert into receipt values('" + recepitNo2.Text + "','" + dateTimePicker2.Value + "','" + "" + "','" + "C-" + invoiceNO2.Text + "','" + new amountByName().setAmountName(amountD + "") + "','" + amountD + "','" + "" + "','" + cutomerID2 + "','" + "CHEQUE" + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();


                    tempcreditPaidID = Int32.Parse(recepitNo2.Text);


                    states = false;
                    if (checkBox2.Checked)
                    {
                        conn2.Open();
                        tempInvoiceNO = "";
                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO2.Text + "' order by requstdate", conn2).ExecuteReader();
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
                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp2 + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                        conn.Close();
                                        //  MessageBox.Show("1" + tempInvoiceNO);
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
                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                        conn.Open();
                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amountTemp + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
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
                        if (amountTemp2 > 0)
                        {
                            conn.Open();
                            new SqlCommand("insert into overPayC values('" + "C-" + invoiceNO2.Text + "','" + amountTemp2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        savesub2(amountD, tempInvoiceNO);
                        //new payment().updateOverPay("C-" + invoiceNO.Text, userH);

                    }
                    else
                    {
                        try
                        {
                            amount2 = amountTemp2;
                            if (checkCells())
                            {
                                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                                {
                                    if (dataGridView2.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                                    {

                                        if (amount2 != 0)
                                        {
                                            amount = 0;

                                            amount = Double.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());

                                            if (amount != 0)
                                            {
                                                if (amount2 <= amount)
                                                {
                                                    conn.Open();
                                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView2.Rows[i].Cells[6].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "R-" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView2.Rows[i].Cells[6].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "R-" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1];
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
                                        reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO2.Text + "' order by requstdate", conn2).ExecuteReader();
                                        while (reader2.Read())
                                        {

                                            if (amount2 != 0)
                                            {
                                                amount = 0;

                                                conn.Open();
                                                reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
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
                                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();
                                                        //  MessageBox.Show("1" + tempInvoiceNO);
                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "R-" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                                        }



                                                        amount2 = 0;
                                                    }
                                                    else
                                                    {
                                                        //  MessageBox.Show("2" + tempInvoiceNO);
                                                        conn.Open();
                                                        new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                        conn.Close();

                                                        if (tempInvoiceNO.Equals(""))
                                                        {
                                                            tempInvoiceNO = "R-" + reader2[0];
                                                        }
                                                        else
                                                        {
                                                            tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
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
                                    new SqlCommand("insert into overPayC values('" + "C-" + invoiceNO2.Text + "','" + amount2 + "','" + tempcreditPaidID + "','" + "" + "')", conn).ExecuteNonQuery();
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
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No");
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
                    new SqlCommand("update receipt set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into cashSummery values('" + "Invoice Credit Paid" + "','" + "CUSTOMER -" + cusLabel.Text + "','" + amountD + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into customerStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Invoice " + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + "C-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                    conn.Close();



                    clear();
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
            saleRef.Text = "";
            checkBox1.Checked = true;
            checkBox1.Checked = false;
            checkBox1.Checked = true;
            idTemp2 = 0;
            //button2.Enabled = false;
            invoiceNO.Text = "";
            recepitNo.Text = "";
            dataGridView1.Rows.Clear();
            invoiceNoT.Text = "";
            checkBox1.Checked = true;
            cash.Text = "0";

            recepitNo.Focus();
            cusLabel.Text = "";
            setBalance();
        }
        public void clear2()
        {
            // MessageBox.Show("21");
            dateTimePicker2.Value = dateTimePicker1.Value;
            cutomerID2 = "";
            saleRef2.Text = "";
            checkBox2.Checked = true;
            checkBox2.Checked = false;
            checkBox2.Checked = true;
            cashCheque.Text = "0";
            idTemp2 = 0;
            //button2.Enabled = false;
            invoiceNO2.Text = "";
            recepitNo2.Text = "";
            dataGridView2.Rows.Clear();
            invoiceNoT2.Text = "";
            checkBox2.Checked = true;
            chequeAmount.Text = "0";

            recepitNo2.Focus();
            cusLabel2.Text = "";
            dateTimePicker3.Value = DateTime.Now;
            chequeNo.Text = "";
            dataGridView3.Rows.Clear();
            setBalance2();
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
        void load2()
        {
            try
            {
                checkBox2.Checked = false;
                dataGridView2.Rows.Clear();
                conn2.Open();
                // MessageBox.Show(customer);
                arrayList = new ArrayList();
                reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO2.Text + "' order by requstdate", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    amountTemp = 0;
                    conn.Open();
                    reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amountTemp = amountTemp + reader.GetDouble(0);

                    }


                    if (amountTemp < reader2.GetDouble(1))
                    {
                        //  MessageBox.Show(reader2[0]+"");
                        dataGridView2.Rows.Add("R-" + reader2[0], db.setAmountFormat(reader2[1] + ""), db.setAmountFormat(amountTemp + ""), db.setAmountFormat((reader2.GetDouble(1) - amountTemp) + ""), false, (reader2.GetDouble(1) - amountTemp), reader2[1], (reader2.GetDouble(1) - amountTemp));
                        arrayList.Add(reader2[0] + "");
                    }
                    conn.Close();
                    idArray = arrayList.ToArray(typeof(string)) as string[];
                    db.setAutoComplete(invoiceNoT2, idArray);
                }
                conn2.Close();
                checkBox2.Checked = true;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        void load()
        {
            try
            {
                checkBox1.Checked = false;
                dataGridView1.Rows.Clear();
                conn2.Open();
                // MessageBox.Show(customer);
                arrayList = new ArrayList();
                reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + "C-" + invoiceNO.Text + "' order by requstdate", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    amountTemp = 0;
                    conn.Open();
                    reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amountTemp = amountTemp + reader.GetDouble(0);

                    }


                    if (amountTemp < reader2.GetDouble(1))
                    {
                        //  MessageBox.Show(reader2[0]+"");
                        dataGridView1.Rows.Add("R-" + reader2[0], db.setAmountFormat(reader2[1] + ""), db.setAmountFormat(amountTemp + ""), db.setAmountFormat((reader2.GetDouble(1) - amountTemp) + ""), false, (reader2.GetDouble(1) - amountTemp), reader2[1], (reader2.GetDouble(1) - amountTemp));
                        arrayList.Add(reader2[0] + "");
                    }
                    conn.Close();
                    idArray = arrayList.ToArray(typeof(string)) as string[];
                    db.setAutoComplete(invoiceNoT, idArray);
                }
                conn2.Close();
                checkBox1.Checked = true;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        string[] idArray;
        //

        void setBalance()
        {
            try
            {
                if (cash.Text.Equals(""))
                {
                    cash.Text = "0";
                }
                var amount=0.0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                }

                balance.Text = Math.Round(Double.Parse(cash.Text) - amount,2) + "";
            }
            catch (Exception a)
            {
               // MessageBox.Show(a.Message);
            }
        }
        void setBalance2()
        {
            try
            {
                if (cashCheque.Text.Equals(""))
                {
                    cashCheque.Text = "0";
                }
                var amount = 0.0;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                   // MessageBox.Show("sa");
                    amount = amount + Double.Parse(dataGridView3.Rows[i].Cells[0].Value.ToString());
                }
                tempTotal.Text = Math.Round(Double.Parse(cashCheque.Text) + amount,2) + "";
                amount = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString());
                    }
                }

                balance2.Text = Math.Round(Double.Parse(tempTotal.Text) - amount,2) + "";
            }
            catch (Exception a)
            {
               MessageBox.Show(a.Message+"/"+a.StackTrace);
            }
        }
        private void invoicePay_Load(object sender, EventArgs e)
        {
            //      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            invoiceNO.Focus();
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();
            db = new DB();
            conn3 = db.createSqlConnection();
           
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;

            dataGridView3.AllowUserToAddRows = false;
            this.ActiveControl = recepitNo;
            clear();
            clear2();
            


            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView3.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

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
                    saleRef.Focus();
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
                        saleRef.Focus();
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
                    reader = new SqlCommand("select auto,company from customer where description like '%" + invoiceNO.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());
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
                new SqlCommand("insert into customerStatement values('" + "CASH RETURN" + "','" + "Payble Amount of Return-Invoice" + "','" + 0 + "','" + -(Double.Parse(cash.Text)) + "','" + true + "','" + dateTimePicker1.Value + "','" + "C-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                new SqlCommand("insert into cashSummery values ('" + "CASH RETURN" + "','" + "Return Cash above the Recived Credit Settelmnt-Customer " + cusLabel.Text + "','" + cash.Text + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                //MessageBox.Show(amountD+"");
                new SqlCommand("insert into receipt values('" + dateTimePicker1.Value + "','" + "" + "','" + "C-" + invoiceNO.Text + "','" + new amountByName().setAmountName(cash.Text + "") + "','" + cash.Text + "','" + "Cash return on CRedit Settele ISsue" + "','" + "" + "','" + "CASH" + "','" + userH + "')", conn).ExecuteNonQuery();
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
        string codeC2, cutomerID2;
        Int32 idTemp2;
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
                    saleRef.Text = reader.GetString(1);

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
        public Boolean loadCustomer4(string id)
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
                    saleRef2.Text = reader.GetString(1);

                }
                else
                {
                    states = false;
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                chequeAmount.Focus();
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
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                if (saleRef.Text.Equals(""))
                {
                    // MessageBox.Show("Sorry, Invalied Ref");
                    cash.Focus();
                }
                else
                {
                    loadCustomer2(idTemp2 + "");
                    cash.Focus();
                }
            }

            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox2.Visible)
                    {
                        listBox2.Focus();
                        listBox2.SelectedIndex = 0;
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
            else if (e.KeyValue == 38)
            {
                invoiceNO.Focus();
            }
        }

        private void saleRef_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | saleRef.Text.Equals("")))
            {

                db.setList(listBox2, saleRef, saleRef.Width);

                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,name from salesRef where description like '%" + saleRef.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());
                        listBox2.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
        }
        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                saleRef.Focus();
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
            panel3.Enabled = !checkBox1.Checked;
            invoiceNoT.Focus();
        }

        private void invoiceNoT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1].ToString().Equals(invoiceNoT.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            dataGridView1.Rows[i].Cells[4].Value = true;
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            checkCells();
        }

        private void invoiceNO2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                if (invoiceNO2.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    invoiceNO2.Focus();
                }
                else
                {
                    loadInvoice2(invoiceNO2.Text);
                    saleRef2.Focus();
                }
            }

            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox3.Visible)
                    {
                        listBox3.Focus();
                        listBox3.SelectedIndex = 0;
                    }
                    else
                    {
                        saleRef2.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void invoiceNO2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | invoiceNO2.Text.Equals("")))
            {

                db.setList(listBox3, invoiceNO2, invoiceNO2.Width);

                try
                {
                    listBox3.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select auto,company from customer where description like '%" + invoiceNO2.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox3.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());
                        listBox3.Visible = true;
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
            if (invoiceNO2.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox3.Visible = false;
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox3.SelectedIndex == 0 && e.KeyValue == 38)
            {
                invoiceNO2.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                //  idTemp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
                // companyC.SelectionLength code= code.MaxLength;
                loadInvoice2(listBox3.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            listBox3.Visible = false;
            loadInvoice2(listBox3.SelectedItem.ToString().Split(' ')[0]);
        }

        private void saleRef2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox4.Visible = false;
                if (saleRef2.Text.Equals(""))
                {
                    // MessageBox.Show("Sorry, Invalied Ref");
                    chequeAmount.Focus();
                }
                else
                {
                    loadCustomer4(idTemp2 + "");
                    chequeAmount.Focus();
                }
            }

            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox4.Visible)
                    {
                        listBox4.Focus();
                        listBox4.SelectedIndex = 0;
                    }
                    else
                    {
                        chequeAmount.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
            else if (e.KeyValue == 38)
            {
                invoiceNO2.Focus();
            }
        }

        private void saleRef2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | saleRef2.Text.Equals("")))
            {

                db.setList(listBox4, saleRef2, saleRef2.Width);

                try
                {
                    listBox4.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,name from salesRef where description like '%" + saleRef2.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox4.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());
                        listBox4.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
        }

        private void listBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox4.SelectedIndex == 0 && e.KeyValue == 38)
            {
                saleRef2.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox4.Visible = false;
                idTemp2 = Int32.Parse(listBox4.SelectedItem.ToString().Split(' ')[0]);
                // companyC.SelectionLength code= code.MaxLength;
                loadCustomer4(idTemp2 + "");
            }
        }

        private void listBox4_MouseClick(object sender, MouseEventArgs e)
        {
            listBox4.Visible = false;

            idTemp2 = Int32.Parse(listBox4.SelectedItem.ToString().Split(' ')[0]);
            loadCustomer4(idTemp2 + "");
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            idTemp2 = Int32.Parse(listBox4.SelectedItem.ToString().Split(' ')[0]);
        }

        private void chequeCodeNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chequeAmount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(saleRef2, chequeNo, chequeNo, e.KeyValue);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Enabled = !checkBox2.Checked;
            invoiceNoT2.Focus();
        }

        private void invoiceNoT2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString().Split('-')[1].ToString().Equals(invoiceNoT2.Text))
                        {
                            dataGridView2.Rows[i].Selected = true;
                            dataGridView2.Rows[i].Cells[4].Value = true;
                            break;
                        }

                    }
                    setBalance2();
                }
                catch (Exception)
                {

                }
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            checkCells2();
            setBalance2();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            saveInvoice2();
        }

        private void recepitNo2_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(invoiceNO2, invoiceNO2, invoiceNO2, e.KeyValue);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                recepitNo.Focus();
            }
            else
            {
                recepitNo2.Focus();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (chequeAmount.Text.Equals(""))
            {
                MessageBox.Show("Enter Cheque Amount");
                chequeAmount.Focus();
            }
            else
            {
                dataGridView3.Rows.Add(chequeAmount.Text, chequeNo.Text, dateTimePicker3.Value.ToShortDateString());
                chequeAmount.Text = "";
                chequeNo.Text = "";
                setBalance2();
            }
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

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {

            setBalance();
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

            setBalance();
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
            if (e.ColumnIndex==3)
            {
                dataGridView3.Rows.RemoveAt(e.RowIndex);
                setBalance2();
            }
        }
    }
}
