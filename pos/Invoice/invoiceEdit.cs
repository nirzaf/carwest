﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class invoiceEdit : Form
    {
        public invoiceEdit(Form form, String user,string id)
        {
            InitializeComponent();
            home = form;
            this.user = user;
            invoieNoTemp = id;
        }
        // My Variable Start
        DB db, db2,db3,db4;
        Form home;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        ArrayList arrayList, stockList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, cashFLowAuto;
        string user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        Double amount, amount2, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        int count;
        string type = "";
        Boolean loadItemCheck = false, discPrestage, isNBT, isTax;
        public double paidAmount = 0, taxpre, nbtpre, purchashingPrice;
       
        // my Variable End
        //my Method Start++++++
        DateTime invoiceDate;
        bool isReturn;
        ArrayList detaiArrayList;
        bool dateNow;
        double cashPaidDB;
        public void loadInvoice(string id)
        {
            try
            {
                isReturn = false;
                invoieNoTemp = id.ToString().Split('-')[1].ToString();
                //    MessageBox.Show("1");
                db.setCursoerWait();
                if (id.Split('-')[0].ToString().ToUpper().Equals("R"))
                {
                    conn3.Open();
                    reader3 = new SqlCommand("select * from invoiceRetail where id='" + invoieNoTemp + "'", conn3).ExecuteReader();
                    if (reader3.Read())
                    {


                        customer.Text = reader3[1] + "";

                        total.Text = reader3[9] + "";
                        this.Text = id + "  (" + reader3.GetDateTime(4).ToShortDateString() + " " + reader3.GetTimeSpan(7) + ")";
                        var ter = reader3[6] + "";

                        cashPaid.Text = reader3[12] + "";
                        balance.Text = reader3[13] + "";
                        cashPaidDB = reader3.GetDouble(12);
                        invoiceDate = reader3.GetDateTime(4);
                        reader3.Close();
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select * from fullservice where invoiceid='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            checkDF.Checked = reader3.GetBoolean(1);
                            checkOF.Checked = reader3.GetBoolean(2);
                            checkEO.Checked = reader3.GetBoolean(3);
                            checkGO.Checked = reader3.GetBoolean(4);
                            checkAF.Checked = reader3.GetBoolean(5);
                            checkGreesen.Checked = reader3.GetBoolean(6);
                            
                        }
                        conn3.Close();
                        loadCustomer(customer.Text);
                        conn4.Open();
                        reader4 = new SqlCommand("select a.vatAmount,a.nbtAmount,b.isTax,b.taxPre,b.isNbt,b.nbtPre from companyInvoice as a,company as b  where a.id='" + invoieNoTemp + "' and  a.companyID=b.id", conn4).ExecuteReader();
                        if (reader4.Read())
                        {
                            isTax = reader4.GetBoolean(2);
                            taxpre = reader4.GetDouble(3);
                            isNBT = reader4.GetBoolean(4);
                            nbtpre = reader4.GetDouble(5);

                            // taxLabel.Text = taxpre+"";
                            //nbtLabel.Text = nbtpre + "";
                        }
                        conn4.Close();

                        conn4.Open();

                        reader4 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();
                        if (reader4.Read())
                        {
                            creditDetail = new string[2];
                            creditDetail[0] = reader4[4] + "";
                            creditDetail[1] = reader4[5] + "";
                            creditDetailB = true;
                            paidAmount = paidAmount + reader4.GetDouble(4);
                        }
                        reader4.Close();
                        conn4.Close();

                       

                        conn4.Open();
                        reader4 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();
                        detaiArrayList = new ArrayList();

                        while (reader4.Read())
                        {
                            chequeDetailB = true;
                            detaiArrayList.Add(reader4[4].ToString());
                            detaiArrayList.Add(reader4[5].ToString());
                            detaiArrayList.Add(reader4[8].ToString());
                            detaiArrayList.Add(reader4.GetDateTime(6).ToShortDateString().Split(' ')[0]);
                            detaiArrayList.Add(reader4[9].ToString());
                            paidAmount = paidAmount + reader4.GetDouble(4);
                        }
                        chequeDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                        reader4.Close();
                        conn4.Close();

                        conn4.Open();
                        reader4 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();
                        detaiArrayList = new ArrayList();

                        while (reader4.Read())
                        {
                            cardDetailB = true;
                            detaiArrayList.Add(reader4[5].ToString());
                            detaiArrayList.Add(reader4[6].ToString());
                            detaiArrayList.Add(reader4[7].ToString());
                            detaiArrayList.Add(reader4[8].ToString());
                            paidAmount = paidAmount + reader4.GetDouble(5);
                        }
                        cardDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                        reader4.Close();
                        conn4.Close();


                        conn3.Open();
                        reader3= new SqlCommand("select * from invoiceRetailDetail where invoiceId='" + invoieNoTemp + "' and pc='" + false + "'", conn3).ExecuteReader();
                        Int32 count = 0;
                        rowCount = 0;
                        while (reader3.Read())
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount, reader3[1], reader3[10], reader3[3], reader3[7], reader3[2], reader3[4], "FALSE", reader3[11], reader3[12], reader3[13]);

                        }
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select * from invoiceRetailDetail where invoiceId='" + invoieNoTemp + "' and pc='" + true + "' ", conn3).ExecuteReader();

                        while (reader3.Read())
                        {
                            isReturn = true;
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount, reader3[1], reader3[10], reader3[3], reader3[7], reader3[2], reader3[4], "TRUE", reader3[11], reader3[12], reader3[13]);
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;

                        }
                        conn3.Close();
                        setTermBack(true);

                        conn3.Open();
                        reader3 = new SqlCommand("select * from invoiceRetail where date='" + DateTime.Now.ToShortDateString() + "' and id='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            dateNow = true;
                        }
                        else
                        {

                            dateNow = false;
                        }
                        reader3.Close();
                        conn3.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from vehicle where invoiceId='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {

                            vehicleNumber.Text = reader3[1] + "";
                            vehicleDescrition.Text = reader3[2] + "";
                            metreNow.Text = reader3[3] + "";
                            metreNext.Text = reader3[4] + "";
                            this.Text = vehicleNumber.Text;
                        }
                        conn3.Close();

                    }
                    else
                    {

                        MessageBox.Show("Invoice not Loading Correctlly");

                        this.Dispose();
                        home.Enabled = true;
                        home.TopMost = true;


                    }
                    code.Focus();
                    conn3.Close();
                }



                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied Invoice ID " + a.Message + " //" + a.StackTrace);
                conn3.Close();
            }
        }
        void loadCompany()
        {
            try
            {
                conn.Open();
                isNBT = false;
                isTax = false;
                taxpre = 0;
                nbtpre = 0;
                reader = new SqlCommand("select name,istax,taxpre,isNBT,nbtPre,defa,id from company ", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboCompany.Items.Add(reader[6] + "-" + reader.GetString(0).ToUpper());

                    if (reader.GetBoolean(5))
                    {
                        //   MessageBox.Show("2");
                        isNBT = reader.GetBoolean(3);
                        isTax = reader.GetBoolean(1);
                        taxpre = reader.GetDouble(2);
                        nbtpre = reader.GetDouble(4);
                        comboCompany.SelectedIndex = comboCompany.Items.Count - 1;
                    }
                }
                conn.Close();
                if (comboCompany.Items.Count != 0)
                {
                    comboCompany.SelectedIndex = 0;
                }

            }
            catch (Exception a)
            {
                MessageBox.Show(a.StackTrace);
                conn.Close();
            }
        }
        Boolean checkUser()
        {
            try
            {
                states = false;
                if (!creditDetailB & !cardDetailB & !chequeDetailB)
                {
                    states = true;
                    //    MessageBox.Show("1");
                }
                else
                {
                    // MessageBox.Show("2");
                    if (!customer.Text.Equals("[CASH CUSTOMER]"))
                    {
                        states = true;
                    }


                }

            }
            catch (Exception)
            {
                states = false;
                conn.Close();
            }
            return states;
        }
        void loadAccountList()
        {

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
            try
            {

                comboCardPayment.Items.Clear();
                comboCardPayment.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select a.isDefa,b.name,b.bankName,a.accountID from accountCardPayment as a,bankAccounts as b where a.accountid=b.id", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboCardPayment.Items.Add(reader.GetString(1).ToUpper() + "-" + reader.GetString(2).ToUpper() + "(" + reader[3] + ")");
                    if (reader.GetBoolean(0))
                    {
                        comboCardPayment.SelectedIndex = comboChequePayment.Items.Count - 1;
                    }
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {

                comboSaleAccount.Items.Clear();
                comboSaleAccount.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select id,name from salesRef", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboSaleAccount.Items.Add("SA." + reader[0] + "." + reader[1].ToString().ToUpper());

                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {


                conn.Open();
                reader = new SqlCommand("select id,name from incomeAccounts", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboSaleAccount.Items.Add("IN." + reader[0] + "." + reader[1].ToString().ToUpper());

                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }


        }
        public Int32 checkDumpInvoice(double amount, string date, string id)
        {
            no = 0;
            countDB = 0;
            maxAmount = 0;

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from dumpInvoiceSetting", conn).ExecuteReader();
                if (reader.Read())
                {
                    countDB = reader.GetInt32(0);
                    maxAmount = reader.GetDouble(1);
                }
                reader.Close();
                conn.Close();
                if (countDB != 0 & maxAmount != 0)
                {
                    if (maxAmount >= amount)
                    {
                        conn.Open();
                        reader = new SqlCommand("select count from dumpinvoiceCount where date='" + date + "' ", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            no = reader.GetInt32(0);
                            reader.Close();
                        }
                        conn.Close();
                        reader.Close();
                        no++;
                        if (no < countDB)
                        {
                            //MessageBox.Show("1");
                            conn.Open();
                            reader = new SqlCommand("select * from dumpInvoiceCount where id='" + id + "'", conn).ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                conn.Close();
                                conn.Open();
                                //  MessageBox.Show("2");
                                reader = new SqlCommand("select * from dumpInvoiceCount where date='" + date + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    reader.Close();
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update dumpInvoiceCount set count=count+'" + 1 + "',id='" + id + "' where date='" + date + "' ", conn).ExecuteNonQuery();
                                    conn.Close();
                                    no = 0;
                                }
                                else
                                {

                                    reader.Close();
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("insert into dumpInvoiceCount values('" + date + "','" + 1 + "','" + id + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }

                            }
                            else
                            {

                                no = 0;

                            }

                            reader.Close();
                            conn.Close();
                            if (no == 1)
                            {
                                try
                                {
                                    conn.Open();
                                    reader = new SqlCommand("select max(dumpNO) from invoiceDump", conn).ExecuteReader();
                                    if (reader.Read())
                                    {
                                        no = reader.GetInt32(0);
                                    }
                                    else
                                    {
                                        no = 0;
                                    }
                                    reader.Close();
                                    conn.Close();
                                }
                                catch (Exception)
                                {

                                    reader.Close();
                                    conn.Close();
                                    no = 0;
                                }
                                no++;
                            }
                            else
                            {
                                no = 0;
                            }
                        }

                        else
                        {
                            no = 0;
                            conn.Open();
                            new SqlCommand("delete from dumpinvoiceCount", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                }



            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/ " + a.StackTrace + " /sas " + date + " " + amount);
            }
            conn.Close();
            return no;

        }
        string tempCustomer = "";
        public Boolean loadCustomer(string id)
        {
            //MessageBox.Show(id);
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    customer.Text = reader[2] + "";
                    address.Text = reader[3] + "";
                    mobileNumber.Text = reader[4] + "";
                    cutomerID = reader[0]+"";
                    tempCustomer = reader[0] + "";
                }
                else
                {
                    //  customer.Text = "[cash supplier]";
                    states = false;
                    cutomerID = "";
                    tempCustomer = "";
                }
                address.Focus();
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
        public void setTermBack(Boolean check)
        {
            term.Text = "";
            if (!creditDetailB & !chequeDetailB & !cardDetailB)
            {
                term.Text = "CASH";
            }
            else
            {

                if (creditDetailB)
                {
                    term.Text = "CREDIT";
                }
                if (chequeDetailB)
                {
                    term.Text = term.Text + "/ CHEQUE";
                }
                if (cardDetailB)
                {
                    term.Text = term.Text + "/ CARD";
                }
            }
        }
        void loadInvoiceNoRetail()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from invoiceRetail", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceNo = "R-" + invoiceMaxNo + "";
                reader.Close();
                conn.Close();

            }
            catch (Exception)
            {
                // throw;
                invoiceNo = "R-1";
                reader.Close();
                conn.Close();
            }
        }
        Double amountR;
        Boolean checkTerm()
        {
            //  MessageBox.Show(creditDetailB+"");
            amountR = (Double.Parse(cashPaid.Text) - Double.Parse(balance.Text));
            if (creditDetailB)
            {
                amountR = amountR + Double.Parse(creditDetail[0].ToString());

            }
            if (chequeDetailB)
            {
                count = 0;
                for (int i = 0; i < (chequeDetail.Length) / 5; i++)
                {
                    amountR = amountR + Double.Parse(chequeDetail[count].ToString());
                    count++;
                    count++;
                    count++;
                    count++;
                    count++;
                }
            }
            if (cardDetailB)
            {
                count = 0;
                for (int i = 0; i < (cardDetail.Length) / 4; i++)
                {
                    amountR = amountR + Double.Parse(cardDetail[count].ToString());
                    count++;
                    count++;
                    count++;
                    count++;
                }
            }
            states = true;
            if (amountR != Double.Parse(total.Text))
            {
                states = false;
            }

            return states;
        }
        void clear()
        {
            checkDF.Checked = false;
            checkOF.Checked = false;
            checkEO.Checked = false;
            checkGO.Checked = false;
            checkAF.Checked = false;
            checkGreesen.Checked = false;
            balance.Text = "0.0";
            total.Text = "0.0";
            vehicleDescrition.Text = "";
            vehicleNumber.Text = "";
            metreNext.Text = "";
            metreNow.Text = "";
            cashPaid.Text = "0";
            if (discPrestage)
            {
                comboDiscount.SelectedIndex = 0;

            }
            else
            {
                comboDiscount.SelectedIndex = 1;
            }
            balance.Text = "0.0";
            customer.Text = "[CASH CUSTOMER]";
            mobileNumber.Text = "";
            address.Text = "";
            term.Text = "CASH";
            cutomerID = "";
            dataGridView1.Rows.Clear();
            // loadInvoiceNoRetail();
            clearSub();
            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;

            comboSaleAccount.SelectedIndex = -1;

        }
        void clearSub()
        {
            code.Text = "";
            unitPrice.Text = "0.0";
            tempDesc.Text = "";
            qty.Text = "";
            discount.Text = "0";
            code.Focus();

        }
        void loadItem(string codeValue)
        {
            try
            {
                uom = "";
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,rate from item where code='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;
                    code.Text = codeValue + "";
                    tempDesc.Text = reader[1] + "";
                    uom = reader[4] + "";

                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {

                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }
                    unitPrice.Text = reader.GetDouble(2) + "";

                    discount.Focus();
                    discount.SelectionLength = discount.TextLength;
                    conn.Close();

                }
                else
                {
                    // var code=itemc
                    //  MessageBox.Show("Invalied Item Codea");
                    item = false;
                    clearSub();
                    code.Text = codeValue;
                    unitPrice.Focus();
                }
                reader.Close();
                conn.Close();



            }
            catch (Exception a)
            {
                //    throw;
                reader.Close();
                conn.Close();
                code.Focus();
                //MessageBox.Show(a.Message);
            }
        }
        Int16 itemCount = 0;
        string uom;
        void addToTable()
        {
            if (true)
            {
                try
                {
                    // MessageBox.Show(item+"");

                    if (item)
                    {
                        if (qty.Text.Equals("") || Double.Parse(qty.Text) <= 0)
                        {
                            MessageBox.Show("Sorry Stock not Available on this Item to Invoice ");
                            qty.Focus();
                        }

                        else if (unitPrice.Text.Equals(""))
                        {
                            MessageBox.Show("Sorry Unit Price Cannot be Emprty Or Zero");
                            unitPrice.Focus();
                        }

                        else
                        {


                            if (dataGridView1.Rows.Count == 0)
                            {
                                amount2 = (Double.Parse(unitPrice.Text) * Double.Parse(qty.Text));

                                rowCount++;
                                if (discPrestage)
                                {
                                    amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                                }
                                else
                                {
                                    amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));

                                }
                                amount = Math.Round(amount, 2);
                                dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);


                            }
                            else
                            {
                                states = true;

                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    //   MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString()+"b"+code.Text+"c");
                                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                    {
                                        states = false;
                                    }
                                }
                                // MessageBox.Show(states+"");
                                if (!states)
                                {
                                    if ((MessageBox.Show("This Item Already ADD to Invoice, Do you Need to Update Current Record", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                    {
                                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                        {
                                            if (!states)
                                            {
                                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                                {
                                                    states = true;
                                                    qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) + Double.Parse(qty.Text);


                                                    if (discPrestage)
                                                    {
                                                        amountTemp = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * qtyTemp);

                                                    }
                                                    else
                                                    {
                                                        amountTemp = (Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * qtyTemp;

                                                    }
                                                    amountTemp = Math.Round(amountTemp, 2);
                                                    //   amount = Double.Parse(subTotal.Text) + amountTemp;
                                                    //dataGridView1.Rows.RemoveAt(i);
                                                    //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);

                                                    dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                    dataGridView1.Rows[i].Cells[2].Value = description;
                                                    dataGridView1.Rows[i].Cells[3].Value = unitPrice.Text;
                                                    dataGridView1.Rows[i].Cells[4].Value = discount.Text;
                                                    dataGridView1.Rows[i].Cells[5].Value = qtyTemp;

                                                    dataGridView1.Rows[i].Cells[6].Value = amountTemp;

                                                    dataGridView1.Rows[i].Cells[8].Value = qtyTemp;


                                                }
                                            }


                                        }


                                    }
                                }
                                else
                                {
                                    rowCount++;
                                    if (discPrestage)
                                    {
                                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                                    }
                                    else
                                    {
                                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));


                                    }
                                    amount = Math.Round(amount, 2);
                                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);

                                    amount = amount + (Double.Parse(total.Text));
                                    var y = dataGridView1.RowCount;
                                    y--;
                                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.Azure;


                                }
                            }



                        }
                        amount = 0;
                        amount2 = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                        }

                        total.Text = amount + "";

                      
                    }
                    else
                    {

                        if (discPrestage)
                        {
                            amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                        }
                        else
                        {
                            amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                        }
                        amount = Math.Round(amount, 2);
                        var a = MessageBox.Show("You Have Enterd New Item and Do You Need to Save it to System", "Confirmation",
       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1);
                        if (a == System.Windows.Forms.DialogResult.Yes)
                        {
                            newItemInvoice2 ab = new newItemInvoice2(this);
                            ab.code.Text = code.Text;
                            ab.sellingPrice.Text = unitPrice.Text;
                            ab.Visible = true;
                        }
                        else if (a == System.Windows.Forms.DialogResult.No)
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount + "", "#", code.Text, unitPrice.Text, discount.Text, qty.Text, amount, "", qty.Text, 1);


                            var y = dataGridView1.RowCount;
                            y--;
                            dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;
                            amount = 0;
                            amount2 = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                            }

                            total.Text = amount + "";

                            
                        }



                        
                    }

                  //  updateInvoice();
                    clearSub();

                    cashPaid.Text = "0";
                    balance.Text = "0.0";
                    code.Focus();
                }
                catch (Exception s)
                {
                    MessageBox.Show("Please Enter QTY ");

                }
            }
            else
            {
                MessageBox.Show("Sorry , Maximum Item's Count Per Invoice Hav Exceed.please Genarate New Invoice");
            }



        }

        public void addItemNew(string codeL)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    rowCount++;
                }
                //   MessageBox.Show(codeL);
                uom = "";
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,rate from item where code='" + codeL + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;

                    tempDesc.Text = reader[1] + "";
                    uom = reader[4] + "";

                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {

                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }

                    conn.Close();

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));

                    }
                    amount = Math.Round(amount, 2);
                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);

                }
                else
                {
                    item = false;

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                    }
                    dataGridView1.Rows.Add(rowCount + "", "#", code.Text, unitPrice.Text, discount.Text, qty.Text, amount, "", qty.Text, 1);
                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                reader.Close();
                conn.Close();
                amount = 0;
                amount2 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                }

                total.Text = amount + "";

                clearSub();

                cashPaid.Text = "0";
                balance.Text = "0.0";
                code.Focus();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        public void addToTableSep(double qtyH, double unitPriceH, double discountH, double qtyFull)
        {
            if (true)
            {
                try
                {

                    if (qtyH <= 0)
                    {
                        MessageBox.Show("Sorry Stock not Available on this Item to Invoice ");
                        qty.Focus();
                    }


                    else
                    {


                        if (dataGridView1.Rows.Count == 0)
                        {
                            amount2 = (unitPriceH * qtyH);

                            rowCount++;
                            if (discPrestage)
                            {
                                amount = ((unitPriceH - ((unitPriceH / 100) * discountH)) * (qtyH));
                            }
                            else
                            {
                                amount = ((unitPriceH - discountH) * (qtyH));

                            }
                            amount = Math.Round(amount, 2);
                            dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPriceH, discountH, qtyH, amount, uom, qtyFull, 0);

                            conn.Open();
                            reader = new SqlCommand("select value from distance where code='" + code.Text + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                if (Double.Parse(reader.GetString(0)) != 0)
                                {
                                    if (metreNow.Text.Equals(""))
                                    {
                                        metreNext.Text = reader[0] + "";
                                    }
                                    else
                                    {
                                        metreNext.Text = Int64.Parse(metreNow.Text) + Double.Parse(reader.GetString(0)) + "";
                                    }
                                }
                            }
                            conn.Close();
                        }
                        else
                        {
                            states = true;

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                //   MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString()+"b"+code.Text+"c");
                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                {
                                    states = false;
                                }
                            }
                            // MessageBox.Show(states+"");
                            if (!states)
                            {
                                if ((MessageBox.Show("This Item Already ADD to Invoice, Do you Need to Update Current Record", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                {
                                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                    {
                                        if (!states)
                                        {
                                            if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                            {
                                                states = true;
                                                qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + qtyH;


                                                if (discPrestage)
                                                {
                                                    amountTemp = ((unitPriceH - ((unitPriceH / 100) * discountH)) * qtyTemp);

                                                }
                                                else
                                                {
                                                    amountTemp = (Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * qtyTemp;

                                                }
                                                amountTemp = Math.Round(amountTemp, 2);
                                                //   amount = Double.Parse(subTotal.Text) + amountTemp;
                                                //dataGridView1.Rows.RemoveAt(i);
                                                //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);

                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[3].Value = description;
                                                dataGridView1.Rows[i].Cells[4].Value = unitPriceH;
                                                dataGridView1.Rows[i].Cells[5].Value = discountH;
                                                dataGridView1.Rows[i].Cells[6].Value = qtyTemp;

                                                dataGridView1.Rows[i].Cells[7].Value = amountTemp;
                                                dataGridView1.Rows[i].Cells[9].Value = qtyFull;
                                                dataGridView1.Rows[i].Cells[8].Value = uom;
                                                dataGridView1.Rows[i].Cells[10].Value = 0;
                                            }
                                        }


                                    }


                                }
                            }
                            else
                            {
                                rowCount++;
                                if (discPrestage)
                                {
                                    amount = ((unitPriceH - ((unitPriceH / 100) * discountH)) * (qtyH));

                                }
                                else
                                {
                                    amount = ((unitPriceH - discountH) * (qtyH));


                                }
                                amount = Math.Round(amount, 2);
                                dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPriceH, discountH, qtyH, amount, uom, qtyFull, 0);

                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Azure;
                                conn.Open();
                                reader = new SqlCommand("select value from distance where code='" + code.Text + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    if (Double.Parse(reader.GetString(0)) != 0)
                                    {
                                        if (metreNow.Text.Equals(""))
                                        {
                                            metreNext.Text = reader[0] + "";
                                        }
                                        else
                                        {
                                            metreNext.Text = Int64.Parse(metreNow.Text) + Int64.Parse(metreNext.Text) + "";
                                        }
                                    }
                                }
                                conn.Close();

                            }
                        }

                        amount = 0;
                        amount2 = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            if (Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) != 0)
                            {

                                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

                            }
                        }


                        total.Text = amount + amount2 + "";


                        clearSub();
                        code.Focus();

                    }


                }
                catch (Exception s)
                {
                    MessageBox.Show("Please Enter Value " + s.Message + "/" + s.StackTrace);

                }
            }
            else
            {
                MessageBox.Show("Sorry , Maximum Item's Count Per Invoice Hav Exceed.please Genarate New Invoice");
            }



        }


        public void updateTableItem(string unitPrice, string discount, string qty, Int32 index)
        {
            if (discPrestage)
            {
                amountTemp = ((Double.Parse(unitPrice) - ((Double.Parse(unitPrice) / 100) * Double.Parse(discount))) * Double.Parse(qty));

            }
            else
            {
                amountTemp = (Double.Parse(unitPrice) - Double.Parse(discount)) * Double.Parse(qty);

            }
            amountTemp = Math.Round(amountTemp, 2);
            //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);


            dataGridView1.Rows[index].Cells[4].Value = unitPrice;
            dataGridView1.Rows[index].Cells[5].Value = discount;
            dataGridView1.Rows[index].Cells[6].Value = qty;

            dataGridView1.Rows[index].Cells[7].Value = (amountTemp + "");
            dataGridView1.Rows[index].Cells[9].Value = qty;
            // MessageBox.Show(amountTemp + "");
            amount = 0;
            amount2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

            }

            total.Text = amount + amount2 + "";

        }
        public void updateTableItemSep(string unitPrice, string discount, string qty, Int32 index, double qtyAll)
        {
            if (discPrestage)
            {
                amountTemp = ((Double.Parse(unitPrice) - ((Double.Parse(unitPrice) / 100) * Double.Parse(discount))) * Double.Parse(qty));

            }
            else
            {
                amountTemp = (Double.Parse(unitPrice) - Double.Parse(discount)) * Double.Parse(qty);

            }
            amountTemp = Math.Round(amountTemp, 2);
            //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);


            dataGridView1.Rows[index].Cells[4].Value = unitPrice;
            dataGridView1.Rows[index].Cells[5].Value = discount;
            dataGridView1.Rows[index].Cells[6].Value = qty;

            dataGridView1.Rows[index].Cells[7].Value = (amountTemp + "");
            dataGridView1.Rows[index].Cells[9].Value = qtyAll;
            // MessageBox.Show(amountTemp + "");
            amount = 0;
            amount2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

            }

            total.Text = amount + amount2 + "";

        }

        void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    nEWCUSTOMERToolStripMenuItem.Enabled = reader.GetBoolean(6);

                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }

        }

        //   my Method End+++++++++
       
        private void invoiceNew_Load(object sender, EventArgs e)
        {
            invoiceDate = DateTime.Now;
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            //  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            quickPanel.Width = width;
            dataGridView1.Width = width - 370;
            dataGridView1.Height = height - (335);

            dataGridView1.Columns[2].Width = dataGridView1.Width - 615;
            Point p = new Point();


            p.X = width - panel4.Width - 15;
            p.Y = height - panel4.Height - 15;
            panel4.Location = p;
            p = panelTax.Location;
            p.X = width - panelTax.Width - 10;
            panelTax.Location = p;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 40;
            btn.Text = "EDIT";


            btn.UseColumnTextForButtonValue = true;

            db = new DB();
            conn = db.createSqlConnection2();
            db2 = new DB();
            conn2 = db2.createSqlConnection2();
            db3 = new DB();
            conn3 = db3.createSqlConnection2();
            db4 = new DB();
            conn4 = db4.createSqlConnection2();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;

            vehicleNumber.CharacterCasing = CharacterCasing.Upper;

            vehicleDescrition.CharacterCasing = CharacterCasing.Upper;

            address.CharacterCasing = CharacterCasing.Upper;
            comboPrinter.SelectedIndex = 0;
            loadUser();
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {

                    saveInvoiceWithoutPay = reader.GetBoolean(0);
                    discPrestage = reader.GetBoolean(4);
                    panel7.Visible = reader.GetBoolean(5);

                    vEHICLENUMBERToolStripMenuItem.Visible = reader.GetBoolean(5);

                    loadItemCheck = reader.GetBoolean(9);
                }
                else
                {
                    saveInvoiceWithoutPay = false;
                }
                conn.Close();

                if (discPrestage)
                {
                    comboDiscount.SelectedIndex = 0;

                }
                else
                {
                    comboDiscount.SelectedIndex = 1;
                }
            }
            catch (Exception)
            {
                conn.Close();
            }
            loadCompany();
            loadAccountList();
            //
            conn.Open();
            reader = new SqlCommand("select vehicleNO from vehicle ", conn).ExecuteReader();
            arrayList = new ArrayList();
            while (reader.Read())
            {
                // MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");

            }
            reader.Close();
            idArray = arrayList.ToArray(typeof(string)) as string[];
            db.setAutoComplete(vehicleNumber, idArray);
            conn.Close();
            loadInvoice(invoieNoTemp);
        }

        private void invoiceNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void quickPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new cusomerQuick2(this).Visible = true;

        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            customer.SelectAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                amount = 0.0;
                amount2 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    dataGridView1.Rows[i].Cells[0].Value = ++i;
                    i--;

                }

                total.Text = amount + amount2 + "";

                cashPaid.Text = "0";
                balance.Text = "0.0";
                rowCount--;
            }
            else if (e.ColumnIndex == 12)
            {

                new itemTable2(this, dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), e.RowIndex + "", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;


            }
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bILLTOAREAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            new termXash2(this, Double.Parse(total.Text), Double.Parse(cashPaid.Text), creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
            //     MessageBox.Show(creditDetailB+"");
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aDDITEMTOINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Focus();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void code_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    code.Focus();
                }
                else
                {
                    loadItem(code.Text);
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
                        unitPrice.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                if (loadItemCheck)
                {
                    db.setList(listBox1, code, code.Width * 2);

                    try
                    {
                        listBox1.Items.Clear();
                        listBox1.Visible = true;
                        listBox1.Height = panel2.Height - 30;
                        conn.Open();
                        reader = new SqlCommand("select code,detail from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
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
            }
            if (code.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, discount, discount, e.KeyValue);

        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(unitPrice, qty, qty, e.KeyValue);

        }

        private void unitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
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

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {

            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
            }
            else if (e.KeyValue == 38)
            {
                discount.Focus();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void availebleQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                code.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                code.SelectionLength = code.MaxLength;
                loadItem(code.Text);
            }

        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            code.SelectionLength = code.MaxLength;
            loadItem(code.Text);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];

        }

        private void warrentyCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                dataGridView1.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
            }
            else if (e.KeyValue == 38)
            {
                qty.Focus();
            }
        }

        private void cashPaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (!cashPaid.Text.Equals(""))
            {

                amount = (Double.Parse(cashPaid.Text)) - (Double.Parse(total.Text));

                if (amount <= 0)
                {
                    balance.Text = "0";
                }
                else
                {
                    balance.Text = amount + "";
                }
                if (Double.Parse(cashPaid.Text) >= Double.Parse(total.Text))
                {
                    //termButton.Enabled = false;
                    term.Text = "CASH";
                }
                else
                {

                    // termButton.Enabled = true;
                    setTermBack(true);
                }
            }
            else
            {
                cashPaid.Text = "0";
                cashPaid.SelectAll();
            }
        }

        void updateInvoice()
        {
            try
            {
                invoieNoTemp = invoiceNo.ToString().Split('-')[1].ToString();

                if (discPrestage)
                {
                    amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                }
                else
                {
                    amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                }
                amount = Math.Round(amount, 2);
                qtyTemp = Double.Parse(qty.Text);

                if (!item)
                {
                    conn2.Open();
                    new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + code.Text + "','" + qty.Text + "','" + unitPrice.Text + "','" + amount + "','" + 0 + "','" + 0 + "','" +discount.Text + "','" + 0 + "','" + "" + "','" + description + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();

                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select qty from item where code='" + code.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.GetDouble(0) < qtyTemp)
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set qty='" + 0 + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        }
                    }


                    reader.Close();
                    reader.Close();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + code.Text + "','" + true + "','" + qtyTemp + "','" + DateTime.Now + "','" + "INVOICE" + "','" + user + "')", conn).ExecuteNonQuery();

                    conn.Close();
                    conn2.Open();
                    new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + code.Text + "','" + qty.Text + "','" + unitPrice.Text + "','" + amount + "','" + 0 + "','" + 0 + "','" + discount.Text + "','" + 0 + "','" + "" + "','" + description + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                    conn.Open();
                    states = true;
                    reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + code.Text + "' order by date", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        states = false;
                        if (qtyTemp == 0)
                        {

                        }
                        else if (qtyTemp <= reader.GetDouble(1))
                        {
                             var price = reader.GetDouble(0);
                            // reader.Close();


                            conn2.Open();

                            new SqlCommand("update purchasingPriceList set qty=qty-'" + qtyTemp + "' where code='" + code.Text + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                            conn2.Close();

                            qtyTemp = 0;
                        }
                        else
                        {
                             var price = reader.GetDouble(0);

                            qtyTemp = qtyTemp - reader.GetDouble(1);


                            conn2.Open();
                            new SqlCommand("update purchasingPriceList set qty=qty-'" + reader.GetDouble(1) + "' where code='" + code.Text + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                            conn2.Close();


                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                if (cutomerID.Equals(""))
                {
                    cutomerID = customer.Text;
                }
                conn.Open();
                new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CASH - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message+"/"+a.StackTrace);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Emprt Data for Generate Invoice");
                code.Focus();
            }
            else if (!checkTerm())
            {

                MessageBox.Show("Please Enter Pay Detail on Term Section Before Genarate Invoice");
            }
            else if (!checkUser())
            {
                MessageBox.Show("Please Enter a Registerd Customer for a Credit Invoice");

            }

            else if (chequeDetailB & comboChequePayment.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Bank Account for a Cheque Deposit");
            }
            else if (cardDetailB & comboCardPayment.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Bank Account for a Card Payment's ");
            }
            else if ((MessageBox.Show("Generate Invoice ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                  //  loadInvoiceNoRetail();
                 //   invoieNoTemp = invoiceNo.ToString().Split('-')[1].ToString();

          
                    //+++++Intial OLD INVOice++++
                    conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceid= '" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();

                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from fullService where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into fullservice values('" + invoieNoTemp + "','" + checkDF.Checked + "','" + checkOF.Checked + "','" + checkEO.Checked + "','" + checkGO.Checked + "','" + checkAF.Checked + "','" + checkGreesen.Checked + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + false + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select qty from purchasingPriceList where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            reader2.Close();
                            conn2.Close();
                            conn2.Open();
                            new SqlCommand("update purchasingPriceList set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }

                        conn2.Close();
                        conn2.Open();
                        new SqlCommand("update item set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + false + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from cashSummery where reason='" + "New Invoice" + "' and remark='" + "Invoice No-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from warrenty where invoiceid='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    ///+++++++++++++++++++++++++   
                    amount = 0;
                    profit = 0;
                    profitTotal = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    }



                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                        {
                            conn2.Open();
                            new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                            conn2.Close();

                        }
                        else
                        {
                            conn.Open();
                            reader = new SqlCommand("select qty from item where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                if (reader.GetDouble(0) < qtyTemp)
                                {
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update item set qty='" + 0 + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                }
                                else
                                {
                                    reader.Close();
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                }
                            }


                            reader.Close();
                            reader.Close();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + true + "','" + qtyTemp + "','" + DateTime.Now + "','" + "INVOICE" + "','" + user + "')", conn).ExecuteNonQuery();

                            conn.Close();
                            conn2.Open();
                            new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                            conn2.Close();
                            conn.Open();
                            states = true;
                            reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                states = false;
                                if (qtyTemp == 0)
                                {

                                }
                                else if (qtyTemp <= reader.GetDouble(1))
                                {
                                    profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;
                                    profitTotal = profitTotal + profit;

                                    var price = reader.GetDouble(0);
                                    // reader.Close();


                                    conn2.Open();

                                    new SqlCommand("update purchasingPriceList set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                    conn2.Close();

                                    qtyTemp = 0;
                                }
                                else
                                {
                                    profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;

                                    profitTotal = profitTotal + profit;
                                    var price = reader.GetDouble(0);

                                    qtyTemp = qtyTemp - reader.GetDouble(1);


                                    conn2.Open();
                                    new SqlCommand("update purchasingPriceList set qty=qty-'" + reader.GetDouble(1) + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                    conn2.Close();


                                }
                            }
                            reader.Close();
                            conn.Close();

                        }

                    }
                    conn.Open();
                    reader = new SqlCommand("select * from customer where id='" + tempCustomer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update customer set address='" + address.Text + "',mobileNo='" + mobileNumber.Text + "', company='" + customer.Text + "' where id='"+tempCustomer+"'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        getID();
                        conn.Open();
                        new SqlCommand("insert into customer values ('" + "C-" + idTemp + "','" + "" + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "','" + "" + "','" + customer.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        tempCustomer = "C-" + idTemp;
                        cutomerID = "C-" + idTemp;
                    }
                    String[] a;
                    String inv = "R-" + invoieNoTemp;


                    if (cutomerID.Equals(""))
                    {
                        cutomerID = customer.Text;
                    }

                    var cashDetailB = true;



                    //conn.Open();
                    //new SqlCommand("delete from invoiceretail where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    //conn.Close();
                    if (!creditDetailB & !chequeDetailB & !cardDetailB)
                    {
                        cashDetailB = true;
                        var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);
                        //conn.Open();
                        //new SqlCommand("insert into invoiceRetail values('" + invoieNoTemp + "','" + "" + "','" + true + "','" + 0 + "','" + DateTime.Now + "','" + true + "','" + "" + "','" + DateTime.Now + "','" + 0 + "','" + 0 + "','" + 0 + "','" + user + "','" + 0 + "','" + 0 + "','" + "" + "')", conn).ExecuteNonQuery();
                        //conn.Close();
                        conn.Open();
                        new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CASH - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into cashSummery values('" + "New Invoice" + "','" + "Invoice No-" + invoieNoTemp + "','" + total.Text + "','" + invoiceDate + "','" + user + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into cashBalance values('" + user + "','" + "Invoice R-" + invoieNoTemp + "','" + false + "','" + total.Text + "','" + invoiceDate + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        cashDetailB = false;
                        var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);
                        //conn.Open();
                        //new SqlCommand("insert into invoiceRetail values('" + invoieNoTemp + "','" + "" + "','" + true + "','" + 0 + "','" + DateTime.Now + "','" + true + "','" + "" + "','" + DateTime.Now + "','" + 0 + "','" + 0 + "','" + 0 + "','" + user + "','" + 0 + "','" + 0 + "','" + "" + "')", conn).ExecuteNonQuery();
                        //conn.Close();
                        conn.Open();
                        new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CREDIT - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("insert into cashSummery values('" + "New Invoice" + "','" + "Invoice No-" + invoieNoTemp + "','" + amountD + "','" + invoiceDate + "','" + user + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into cashBalance values('" + user + "','" + "Invoice R-" + invoieNoTemp + "','" + false + "','" + amountD + "','" + invoiceDate + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }

                    conn.Open();
                    new SqlCommand("delete from companyInvoice where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from sale where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from incomeAccountStatement where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("delete from bankAccountStatment where number='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from vehicle where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into vehicle values('" + invoieNoTemp + "','" + vehicleNumber.Text + "','" + vehicleDescrition.Text + "','" + metreNow.Text + "','" + metreNext.Text + "','" + cutomerID + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                    conn.Close();
                   
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

                //    MessageBox.Show("");
                    conn.Open();
                    new SqlCommand("insert into invoiceTerm values('" + invoieNoTemp + "','" + cashDetailB + "','" + creditDetailB + "','" + chequeDetailB + "','" + cardDetailB + "','" + user + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Invoice Amount" + "','" + total.Text + "','" + 0 + "','" + true + "','" + DateTime.Now+ "','" + cutomerID + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    if (Double.Parse(cashPaid.Text) != 0)
                    {
                        if (Double.Parse(cashPaid.Text) > Double.Parse(total.Text))
                        {
                            conn.Open();
                            new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cash Payment of Invoice" + "','" + 0 + "','" + total.Text + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {

                            conn.Open();
                            new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cash Payment of Invoice" + "','" + 0 + "','" + cashPaid.Text + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if (cashDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into cashInvoiceRetail values('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();


                    }
                    if (creditDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into creditInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + 0 + "','" + creditDetail[0] + "','" + creditDetail[1] + "','" + DateTime.Now + "','" + DateTime.Now.AddDays(Int32.Parse(creditDetail[1])) + "')", conn).ExecuteNonQuery();
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
                            new SqlCommand("insert into chequeInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeDate + "','" + DateTime.Now + "','" + tempChequeCodeNo + "','" + tempChequeId + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                            {
                                try
                                {
                                    conn.Open();
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + comboSaleAccount.Text.ToString().Split('.')[1].ToString() + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                catch (Exception)
                                {
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                           

                            conn.Open();
                            new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cheque for Balance Amount of Invoice" + "','" + 0 + "','" + tempChequeAmoun + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    if ((MessageBox.Show("Invoice Succefully Generated , Do You want to Print it", "Confirmation",
          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {

                        var aa = "";
                        if (cashDetailB)
                        {
                            aa = "CASH MEMO";
                        }
                        else
                        {
                            aa = "CREDIT MEMO";
                        }
                        if (balance.Text.Equals("0") || Double.Parse(balance.Text) == 0)
                        {
                            new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, Double.Parse(total.Text)-Double.Parse(cashPaid.Text)+"", DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);

                        }
                        else {
                            new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);

                        }


                        // conn.Close();
                        //  a.Visible = true;
                    }
                    //++++++++++++++++++++Tax Invoice Start

                    conn.Open();
                    reader = new SqlCommand("select id from invoicedump where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();


                        conn.Open();
                        new SqlCommand("update invoiceDump set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();


                    }
                    reader.Close();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();


                    //++++++++++++++++++++Tax Inoice End
                    //  clear();
                    conn.Open();
                    new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    this.Dispose();

                }
                catch (Exception a)
                {
                    MessageBox.Show("Sorry, You Have Make Mistake,Try Again " + a.StackTrace + "/" + a.Message);
                    conn.Close();
                    conn2.Close();
                }
                db.setCursoerDefault();

            }
        }
        Int32 idTemp = 0;
        void getID()
        {

            try
            {
                conn2.Open();
                reader2 = new SqlCommand("select max(auto) from customer", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    idTemp = reader2.GetInt32(0);
                    idTemp++;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }
        private void cREATEINVOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void cashPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button3_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                term.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button3.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void unitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void sELECTCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void eDITTERMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);

        }

        private void eNABLEDISABLEAUTOLOADINGITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadItemCheck)
            {
                loadItemCheck = false;
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Visible = true;
                loadItemCheck = true;
            }
        }

        private void nEWCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerProfile(this, user).Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cASHPAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cashPaid.Focus();
        }

        private void comboCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //  MessageBox.Show(comboCompany.SelectedItem.ToString().Split('-')[0].ToString());
                reader = new SqlCommand("select name,istax,taxpre,isNBT,nbtPre,defa from company where  id='" + comboCompany.SelectedItem.ToString().Split('-')[0].ToString() + "'", conn).ExecuteReader();
                if (reader.Read())
                {


                    isNBT = reader.GetBoolean(3);
                    isTax = reader.GetBoolean(1);
                    taxpre = reader.GetDouble(2);
                    nbtpre = reader.GetDouble(4);

                }
                conn.Close();

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void saleRef_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void saleRef_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void poNumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void saleRef_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void saleRef_Leave(object sender, EventArgs e)
        {

        }

        private void saleRef_MouseLeave(object sender, EventArgs e)
        {

        }

        private void saleRef_KeyDown_1(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboCompany_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboChequePayment_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboCardPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            //       e.SuppressKeyPress = true;
        }

        private void comboSaleAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //      e.SuppressKeyPress = true;
        }

        private void comboCardPayment_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboSaleAccount_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void disc_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, cashPaid, cashPaid, e.KeyValue);
        }

        private void disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void disc_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void subTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void vEHICLENUMBERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vehicleNumber.Focus();
        }

        private void vehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vehicleNumber, vehicleDescrition, vehicleDescrition, e.KeyValue);
        }

        private void vehicleDescrition_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vehicleNumber, metreNow, metreNow, e.KeyValue);
        }

        private void metreNow_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vehicleDescrition, metreNext, metreNext, e.KeyValue);
        }
        bool meterCheck = false;
        private void metreNext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    if (meterCheck)
                    {
                        metreNext.Text = Double.Parse(metreNow.Text) + Double.Parse(metreNext.Text) + "";
                        meterCheck = false;
                    }
                }
                catch (Exception)
                {

                }
                code.Focus();
            }
            else if (e.KeyValue == 38)
            {
                metreNow.Focus();
            }
        }

        private void metreNow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;

        }

        private void vehicleNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void meterNextOn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void meterNextOn_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void metreNext_TextChanged(object sender, EventArgs e)
        {

        }

        private void qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void iTEMPROFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new itemProfile(this, user).Visible = true;
        }

        private void comboDiscount_DropDownClosed(object sender, EventArgs e)
        {
            if (comboDiscount.SelectedIndex == 0)
            {
                discPrestage = true;
            }
            else
            {
                discPrestage = false;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from itemSub where code='" + code.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    uom = reader[4] + "";
                    new separate2(this, unitPrice.Text, code.Text, reader[1] + "", reader[4] + "").Visible = true;

                }
                else
                {
                    MessageBox.Show("Sorry This Item not a Separated One or Invalied Item Code");
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }

        }

        private void customer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                if (customer.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    customer.Focus();
                }
                else
                {
                    loadCustomer(customer.Text);
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
                        customer.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        private void customer_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            {
                db.setList(listBox2, customer, customer.Width);
                listBox2.Visible = true;

                listBox2.Height = 55;
                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,company from customer where company like '%" + customer.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();
                    states = true;
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states)
                    {
                        listBox2.Visible = false;
                    }
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }

            }
            if (customer.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox2.Visible = false;
            }
        }

        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, mobileNumber, mobileNumber, e.KeyValue);
        }

        private void mobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(address, code, code, e.KeyValue);
        }

        private void listBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customer.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox2_MouseClick_1(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;
            loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            customer.Text = listBox2.SelectedItem.ToString();
        }

        private void metreNow_KeyUp(object sender, KeyEventArgs e)
        {
            meterCheck = true;

        }

        private void metreNext_KeyUp(object sender, KeyEventArgs e)
        {
            meterCheck = true;
        }

        private void vehicleNumber_KeyUp(object sender, KeyEventArgs e)
        {
            this.Text = vehicleNumber.Text;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var cashDetailB = true;


            if (!creditDetailB & !chequeDetailB & !cardDetailB)
            {
                cashDetailB = true;
                var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);


            }
            else
            {
                cashDetailB = false;
                var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);


            }
            if ((MessageBox.Show("Invoice Succefully Generated , Do You want to Print it", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {

                var aa = "";
                if (cashDetailB)
                {
                    aa = "CASH MEMO";
                }
                else
                {
                    aa = "CREDIT MEMO";
                }
                if (balance.Text.Equals("0") || Double.Parse(balance.Text) == 0)
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, Double.Parse(total.Text) - Double.Parse(cashPaid.Text) + "", DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);

                }
                else
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);

                }


                // conn.Close();
                //  a.Visible = true;
            }
        }
    }
}
