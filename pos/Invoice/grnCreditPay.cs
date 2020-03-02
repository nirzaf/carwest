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
    public partial class grnCreditPay : Form
    {
        Form homeH;
        public grnCreditPay(Form home, String user)
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
        Int32 tempcreditPaidID, dbID;
        public Double paidAmount, cashPaidDB;
        // my Variable End
        //
        //Method



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


        void loadInvoice(string id)
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
        double tempCheque = 0;
        public void savesub2(double amountD, string tempInvoiceNOS)
        {
            tempCheque = 0;

            tempCheque = tempCheque + Double.Parse(chequeAmount.Text);
            conn.Open();
            new SqlCommand("insert into chequegrn2 values ('" + tempcreditPaidID + "','" + "S-" + invoiceNO.Text + "','" + chequeAmount.Text + "','" + 0 + "','" + chequeAmount.Text + "','" + chequeNo.Text + "','" + dateTimePicker3.Value + "','" + dateTimePicker1.Value + "','" + "" + "','" + true + "','" + "" + "')", conn).ExecuteNonQuery();
            conn.Close();

            conn.Open();
       //     new SqlCommand("insert into chequeSummery values('" + tempcreditPaidID + "','" + "" + "','" + "" + "','" + false + "','" + false + "','" + true + "','" + chequeAmount.Text + "','" + comboBank.SelectedItem.ToString().Split('-')[1] + "','" + chequeNo.Text + "','" + dateTimePicker3.Value + "','" + false + "','" + true + "','" + "S-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
            new SqlCommand("insert into chequeSummery values('" + tempcreditPaidID + "','" + "" + "','" + "" + "','" + true + "','" + false + "','" + false + "','" + chequeAmount.Text + "','" + comboBank.SelectedItem.ToString().Split('-')[0] + "','" + chequeNo.Text + "','" + dateTimePicker3.Value + "','" + false + "','" + true + "','" + "S-" + invoiceNO.Text + "','" + "S-" + invoiceNO.Text + "','" + "" + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
          
            conn.Close();




            try
            {
                conn.Open();
                new SqlCommand("update receipt2 set ref='" + tempInvoiceNOS + "',reason='" + "SETTELMENT OF " + tempInvoiceNOS + "' where id='" + tempcreditPaidID + "'", conn).ExecuteNonQuery();
                conn.Close();

                conn.Open();
                new SqlCommand("insert into supplierStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Purchasing -Cheque" + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + "S-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception)
            {
                conn.Close();
            }
            
            var a = invoiceNO.Text;
            clear();
            loadInvoice(a);
        }
        double amount2;
        void saveInvoice()
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
                    if (checkBox1.Checked)
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
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
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
        bool checkOD()
        {
            states = false;
            if (chequeAmount.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Amount");
                chequeAmount.Focus();
            }
            else
            {
                odTotal = 0;
                odLimit = 0;
                try
                {

                    conn.Open();
                    reader = new SqlCommand("select sum(amount) from chequeSummery where send='" + "true" + "' and sendBank='" + comboBank.SelectedItem.ToString().Split('-')[0] + "' and date='" + dateTimePicker3.Value + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        odTotal = reader.GetDouble(0);
                    }
                    conn.Close();
                }
                catch (Exception a)
                {
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select dayLimit from bank where bankCode='" + comboBank.SelectedItem.ToString().Split('-')[0] + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        odLimit = reader.GetDouble(0);
                    }
                    conn.Close();


                }
                catch (Exception)
                {
                    conn.Close();

                }
             //   MessageBox.Show(odLimit + "/" + odTotal);
                odBalance = odLimit - odTotal;
                if (odBalance < Double.Parse(chequeAmount.Text))
                {
                  //  MessageBox.Show("Sorry You Have Only " + db.setAmountFormat(odBalance + "") + " Rupees  on Selected Day for the Cheque Issue.");
                }
                else
                {
                    states = true;
                }
                states = true;
            }

            return states;

        }

        double odTotal, odLimit, odBalance;
        void saveInvoice2()
        {
            try
            {
                recepitNo.Text = getID() + "";
                if (!checkOD())
                {
                    
                }else
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
                    if (checkBox1.Checked)
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
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount2 + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();
                                                    // MessageBox.Show(tempInvoiceNO);
                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    amount2 = 0;
                                                }
                                                else
                                                {
                                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                                    conn.Open();
                                                    new SqlCommand("insert into grnCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount + "','" + 0 + "','" + userH + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                                    conn.Close();

                                                    if (tempInvoiceNO.Equals(""))
                                                    {
                                                        tempInvoiceNO = "" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                                    }
                                                    else
                                                    {
                                                        tempInvoiceNO = tempInvoiceNO + "/" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
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
                   // new invoicePrint().setprintHalfInvoiceService("", "C-" + invoiceNO.Text, "CASH", dataGridView1, 0 + "", cash.Text, 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");
                    
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Sorry Duplcate Receipt No"+a.Message+"/"+a.StackTrace);
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
                    new SqlCommand("insert into cashSummery values('" + "Purchasing Credit Paid-" + tempcreditPaidID + "','" + "SUPPLIER -" + cusLabel.Text + "','" + amountD + "','" + dateTimePicker1.Value + "','" + userH + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into supplierStatement values('" + "SETTELE-" + tempcreditPaidID + "','" + "Settelemnt for Purchasing " + tempInvoiceNOS + "','" + 0 + "','" + amountD + "','" + true + "','" + dateTimePicker1.Value + "','" + "S-" + invoiceNO.Text + "')", conn).ExecuteNonQuery();
                    conn.Close();
                   
                    var a = invoiceNO.Text;
                    MessageBox.Show("Saved");
                    clear();
                    loadInvoice(a);
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message+"/"+a.StackTrace);
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
            chequeNo.Text = "";
            dateTimePicker1.Value = dateTimePicker1.Value;
            cutomerID2 = "";
            totalOutstanding.Text = "";
            checkBox1.Checked = true;
            checkBox1.Checked = false;
            chequeAmount.Text = "";
            checkBox1.Checked = true;
            idTemp2 = 0;
            //button2.Enabled = false;
            invoiceNO.Text = "";
            recepitNo.Text = "";
            dataGridView1.Rows.Clear();
            invoiceNoT.Text = "";
            checkBox1.Checked = true;
            cash.Text = "0";
            totalOutstanding.Text = "";
            recepitNo.Focus();
            cusLabel.Text = "";
            setBalance();
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
        double amount3, amount4;
        void load()
        {
            try
            {
                totalOut = 0;
                amount3 = 0;
                amount4 = 0;
                checkBox1.Checked = false;
                dataGridView1.Rows.Clear();
                //try
                //{
                //    conn2.Open();
                //    amount = 0;
                //    // MessageBox.Show(customer);
                //    arrayList = new ArrayList();
                //    reader2 = new SqlCommand("select sum(a.balance) from creditGRn as a,grnTerm as b where a.customerid='" + "S-" + invoiceNO.Text + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                //    if (reader2.Read())
                //    {
                //        amount = reader2.GetDouble(0);
                //    }
                //    conn2.Close();
                //}
                //catch (Exception)
                //{
                //    amount = 0;
                //    conn2.Close();
                //}
                //try
                //{
                //    conn2.Open();
                //    amount2 = 0;
                //    // MessageBox.Show(customer);
                //    arrayList = new ArrayList();
                //    reader2 = new SqlCommand("select sum(amount2) from receipt2 where customer='" + "S-" + invoiceNO.Text + "' and date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + "" + "' ", conn2).ExecuteReader();
                //    if (reader2.Read())
                //    {
                //        amount2 = reader2.GetDouble(0);
                //    }
                //    conn2.Close();
                //}
                //catch (Exception)
                //{
                //    amount2 = 0;
                //    conn2.Close();
                //}
                //try
                //{
                //    conn2.Open();
                //    // MessageBox.Show(customer);
                //    arrayList = new ArrayList();
                //    reader2 = new SqlCommand("select sum(value) from returnGoods where customer='" + "S-" + invoiceNO.Text + "' and date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + "" + "' and credit='"+true+"' ", conn2).ExecuteReader();
                //    if (reader2.Read())
                //    {
                //        amount2 =amount2+ reader2.GetDouble(0);
                //    }
                //    conn2.Close();
                //}
                //catch (Exception)
                //{
                //    conn2.Close();
                //}
                //totalOut = amount - amount2;

                dataGridView4.Rows.Clear();
              //  dataGridView4.Rows.Add("B/F", "", "", "", "", "", db.setAmountFormat(totalOut + ""), "");
                //MessageBox.Show(invoiceNO.Text);
                //    var a = ;
                for (int i = 1; i <= Int32.Parse(db.getLastDate(DateTime.Now.Month, DateTime.Now.Year)); i++)
                {
                    // MessageBox.Show(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i);
                    conn2.Open();
                    amount = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select a.* from creditGRN as a,GRNTerm as b where a.customerid='" + "S-" + invoiceNO.Text + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "'  ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        amount3 = amount3 + reader2.GetDouble(4);
                        conn3.Open();
                        reader3=new SqlCommand("select sum",conn3).ExecuteReader();
                        conn3.Close();
                        totalOut = totalOut + reader2.GetDouble(4);
                        dataGridView4.Rows.Add(reader2.GetDateTime(6).ToShortDateString(), "" + reader2[0], db.setAmountFormat(reader2[2] + ""), "CREDIT PURCHASING", db.setAmountFormat(reader2[4] + ""), 0.0, db.setAmountFormat(totalOut + ""), "OPEN");
                    }
                    conn2.Close();

                    conn2.Open();
                    amount2 = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select * from receipt2 where customer='" + "S-" + invoiceNO.Text + "' ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        amount4 = amount4 + reader2.GetDouble(5);
                        totalOut = totalOut - reader2.GetDouble(5);
                        dataGridView4.Rows.Add(reader2.GetDateTime(1).ToShortDateString(), reader2[0], db.setAmountFormat(reader2[5] + ""), reader2[8], 0.0, db.setAmountFormat(reader2[5] + ""), db.setAmountFormat(totalOut + ""), "PRINT");
                    }
                    conn2.Close();
                    conn2.Open();
                    amount2 = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    //reader2 = new SqlCommand("select value,itemCode,date from returnGoods where customer='" + "S-" + invoiceNO.Text + "' and date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' and credit='" + true + "'", conn2).ExecuteReader();
                    //while (reader2.Read())
                    //{
                    //    amount4 = amount4 + reader2.GetDouble(0);
                    //    totalOut = totalOut - reader2.GetDouble(0);
                    //    dataGridView4.Rows.Add(reader2.GetDateTime(2).ToShortDateString(),"RETURN-GOOD", db.setAmountFormat(reader2[0] + ""), "", 0.0, db.setAmountFormat(reader2[0] + ""), db.setAmountFormat(totalOut + ""), "PRINT");
                    //}
                    conn2.Close();
                }
                checkBox1.Checked = true;
                totalOutstanding.Text = db.setAmountFormat(totalOut + "");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        string[] idArray;
        //,
        double totalOut;
        void setBalance()
        {
            try
            {
                if (cash.Text.Equals(""))
                {
                    cash.Text = "0";
                }
                var amount = 0.0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                }

                balance.Text = Math.Round(Double.Parse(cash.Text) - amount, 2) + "";
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

                var amount = 0.0;

                amount = amount + Double.Parse(chequeAmount.Text);

                amount = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
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
            dataGridView1.AllowUserToAddRows = false;
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
            if (!userH.Equals("rasika"))
            {
                sUMMERYToolStripMenuItem.Enabled = false;
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
                    reader = new SqlCommand("select ID,company from supplier where description like '%" + invoiceNO.Text + "%' ", conn).ExecuteReader();
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

        }

        private void sUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invoiceCreditPaidedit(this, userH).Visible = true;
        }

        private void cUSTOMERSTATEMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new customerOutstanding(this, userH).Visible = true;
        }

        private void cUSTOMERSTATEMENTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new customerOutstanding2_(this, userH).Visible = true;
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
                supplierStatement statmentH = new supplierStatement(this, userH, true);
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
            supplierStatement aa = new supplierStatement(this, "S-" + invoiceNO.Text, false);
            aa.Visible = true;
            aa.loadBank(DateTime.Now, "S-" + invoiceNO.Text);
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                try
                {
                    if (dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-').Length > 1)
                    {
                        new invoiceNewPURCH(this, userH, "CREDIT", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;

                    }
                    else {
                        conn.Close();
                       // new invoicePrint().setprintHalfInvoiceServiceRE(dataGridView4.Rows[e.RowIndex].Cells[1].Value.ToString(), "C-" + invoiceNO.Text, "CASH", dataGridView1, 0 + "", cash.Text, 0 + "", DateTime.Now, conn, reader, userH, "", "", "", "");
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
            supplierStatement aa = new supplierStatement(this, "S-" + invoiceNO.Text, true);
            aa.Visible = true;
            aa.loadBank(DateTime.Now, "S-" + invoiceNO.Text);
        }

        private void sUMMERYToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new grnCreditPaidedit_(this, userH).Visible = true;
        }

        private void sUPPLIEROUTSTANDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new supplierOutstanding(this, userH).Visible = true;
        }

        private void sUPPLIERSTATEMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new supplierOutstanding2(this, userH).Visible = true;
        }

        private void panelChequ_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
