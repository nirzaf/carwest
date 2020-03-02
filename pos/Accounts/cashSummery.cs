﻿using System;
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
    public partial class cashSummery : Form
    {
        public cashSummery(Form form, String user)
        {
            InitializeComponent();
            homeH = form;
            userH = user;

        }

        Form homeH;
        // My Variable Start

        DB db, db2, db3, db4;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        Double invest, amount, total, tempTOtalSale, tempCredistSale, tempChequeSale, tempCardSale, tempCashSale, tempExpen, tempCashRecevied, tempCashGiven, tempCashPaidReturn;
        string idB, userH, nameB;
        public bool statese, isLoadSaleAll, isLoadSale;
        Int32 yearB, monthB, index;
        DateTime dateSearchB;

        string cus = "";
        string[] array;

        DataSet ds;
        DataTable dt;
        public void loadIncome(string id, string name)
        {
            try
            {
                db.setCursoerWait();
                //  MessageBox.Show("1");
                tempTOtalSale = 0;
                tempCredistSale = 0;
                tempChequeSale = 0;
                tempCardSale = 0;
                tempCashSale = 0;
                tempExpen = 0;
                invest = 0;
                tempCashRecevied = 0;
                tempCashGiven = 0;
                tempCashPaidReturn = 0;
                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("account", typeof(string));
                dt.Columns.Add("desc", typeof(string));


                dt.Columns.Add("CARD", typeof(double));
                dt.Columns.Add("CHEQUE", typeof(double));
                dt.Columns.Add("CREDIT", typeof(double));
                dt.Columns.Add("DEBIT", typeof(double));
                dt.Columns.Add("balance", typeof(double));

                //      MessageBox.Show("2");

                try
                {
                    amount = 0;
                    db.setCursoerWait();
                    conn.Open();
                    reader = new SqlCommand("select * from cashOpeningBalance where date='" + dateFrom.Value.ToShortDateString() + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        amount = amount + reader.GetDouble(2);
                        dt.Rows.Add(" ", "B/F", 0, 0, 0, 0, amount);

                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and remark='" + "INVEST-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount + reader.GetDouble(2);
                        invest = invest + reader.GetDouble(2);
                        dt.Rows.Add("INVEST", reader[0].ToString().ToUpper() + "", 0, 0, reader[2] + "", 0, amount);

                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' order by remark", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn3.Open();
                        //reader3 = new SqlCommand("select * from invoiceRetail where id='" + reader[1].ToString().Split('-')[1] + "' and retail='" + true + "'", conn3).ExecuteReader();
                        //if (reader3.Read())
                        {
                            conn3.Close();
                            if (reader.GetString(0).Equals("New Invoice"))
                            {
                                amount = amount + reader.GetDouble(2);
                                try
                                {
                                    conn3.Open();
                                    // MessageBox.Show("A"+reader[1].ToString().Split('-')[1]+"A");
                                    reader3 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader[1].ToString().Split('-')[1] + "'  ", conn3).ExecuteReader();
                                    if (reader3.Read())
                                    {
                                        if (reader3.GetBoolean(3))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select cheque,amount,cutomerId from chequeInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                //  MessageBox.Show(reader[1]+"/"+reader2[2]);

                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempChequeSale = tempChequeSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));

                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CHEQUE INVOICE (" + reader4.GetString(0).ToUpper() + " )" + " ,AMOUNT-" + reader2[1] + "", 0, reader2[0], 0, reader[2], amount);

                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(4))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select paid,amount,cutomerID from cardInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {

                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCardSale = tempCardSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CARD INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", reader2[0], 0, 0, reader[2], amount);

                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(2))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select balance,amount,customerId from creditInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {

                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCredistSale = tempCredistSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CREDIT INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", 0, 0, reader2[0], reader[2], amount);
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }

                                        else
                                        {
                                            tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                            tempCashSale = tempCashSale + reader.GetDouble(2);
                                            dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", 0, 0, 0, reader[2], amount);

                                        }
                                    }
                                    conn3.Close();


                                }
                                catch (Exception a)
                                {
                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                    //  MessageBox.Show(a.Message+"/"+a.StackTrace);
                                    conn2.Close();
                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", 0, 0, 0, reader[2], amount);
                                }


                            }
                            else if (reader.GetString(0).Equals("Cansel Invoice"))
                            {
                                dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE-CANSEL", 0, 0, 0, 0, 0);

                            }


                        }
                        conn3.Close();


                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid"))
                            {
                                amount = amount + reader.GetDouble(2);
                                tempCashRecevied = tempCashRecevied + reader.GetDouble(2);
                                dt.Rows.Add("CREDIT SETTELEMNT", reader[1].ToString().ToUpper() + "", 0, 0, 0, reader[2], amount);


                            }
                        }
                        catch (Exception)
                        {


                        }
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid Cheque"))
                            {
                                amount = amount + reader.GetDouble(2);
                                // tempCashRecevied = tempCashRecevied + reader.GetDouble(2);
                                dt.Rows.Add("CREDIT SETTELEMNT CHEQUE", reader[1].ToString().ToUpper() + "", 0, reader[2], 0, 0, amount);


                            }
                        }
                        catch (Exception)
                        {


                        }
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("GRN Credit Paid"))
                            {
                                amount = amount - reader.GetDouble(2);
                                tempCashGiven = tempCashGiven + reader.GetDouble(2);
                                dt.Rows.Add("CREDIT SETTELEMNT", reader[1].ToString().ToUpper() + "", 0, 0, reader[2], 0, amount);


                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and remark='" + "EXPENCES-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        tempExpen = tempExpen + reader.GetDouble(2);
                        dt.Rows.Add("EXPENSES", reader[0].ToString().ToUpper() + "", 0, 0, reader[2] + "", 0, amount);

                    }
                    conn.Close();
                    //conn.Open();
                    //reader = new SqlCommand("select a.value,b.detail from returnGoods as a,item as b where a.date='" + dateFrom.Value.ToShortDateString() + "' and a.credit='" + false + "' and a.itemcode=b.code ", conn).ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    amount = amount - reader.GetDouble(0);
                    //    tempExpen = tempExpen + reader.GetDouble(0);
                    //    dt.Rows.Add("EXPENSES-RETURN GOOD", reader[1].ToString().ToUpper() + "", 0, 0, reader[0] + "", 0, amount);

                    //}
                    //conn.Close();
                    //conn.Open();
                    //reader = new SqlCommand("select a.reason,a.amount2,b.name from commis as a, emp as b where a.date='" + dateFrom.Value.ToShortDateString() + "' and a.empid=b.empid", conn).ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    amount = amount - reader.GetDouble(1);
                    //    tempExpen = tempExpen + reader.GetDouble(1);
                    //    dt.Rows.Add("EXPENSES-COMMISSION", reader[0].ToString().ToUpper() + " - " + reader[2].ToString().ToUpper(), 0, 0, reader[1] + "", 0, amount);

                    //}
                    //conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and reason='" + "CASH PAID" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        tempCashPaidReturn = tempCashPaidReturn + reader.GetDouble(2);
                        dt.Rows.Add("CASH PAID-RETURN INVOICE", reader[1].ToString().ToUpper() + "", 0, 0, reader[2] + "", 0, amount);

                    }
                    conn.Close();
                    //  conn.Open();

                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                //  MessageBox.Show("3");


                ds.Tables.Add(dt);

                ///  ds.WriteXmlSchema("expenAMOS2016.xml");
                // this.Dispose();
                //     MessageBox.Show(;
                cashBookView pp4 = new cashBookView();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("date", dateFrom.Value.ToShortDateString());
                pp4.SetParameterValue("cashSale", tempCashSale);
                pp4.SetParameterValue("creditSale", tempCredistSale);
                pp4.SetParameterValue("cardSae", tempCardSale);
                pp4.SetParameterValue("chequeSae", db.setAmountFormat(tempChequeSale + ""));
                pp4.SetParameterValue("totalSale", (tempCashSale + tempChequeSale + tempCardSale + tempCredistSale));
                pp4.SetParameterValue("creditSetteleInvoice", tempCashRecevied);
                pp4.SetParameterValue("cashPaid", tempCashPaidReturn);
                pp4.SetParameterValue("purchasing", tempCashGiven);
                pp4.SetParameterValue("expenses", tempExpen);
                pp4.SetParameterValue("balance", ((tempCashSale + tempCashRecevied + invest) - (tempCashGiven + tempCashPaidReturn + tempExpen)));
                pp4.SetParameterValue("cashTotal", (tempCashSale + tempChequeSale + tempCardSale + tempCredistSale) - tempExpen);
                pp4.SetParameterValue("totalExpenses", (tempCashGiven + tempCashPaidReturn + tempExpen));
                // pp4.SetParameterValue("time", timeH);

                //   MessageBox.Show(comName);
                //  pp4.PrintToPrinter(1, false, 0, 0);
                // new test(pp4).Visible = true;
                crystalReportViewer1.ReportSource = pp4;
                //try
                //{
                //    db.setCursoerWait();
                //    conn4.Open();
                //    reader4 = new SqlCommand("select * from item", conn4).ExecuteReader();
                //    while (reader3.Read())
                //    {
                //        try
                //        {
                //            conn2.Open();
                //            reader2 = new SqlCommand("select * from item where code='" + reader4[0] + "'", conn2).ExecuteReader();
                //            if (!reader2.Read())
                //            {
                //                conn2.Close();
                //                conn2.Open();
                //                new SqlCommand("insert into item values('" + reader4[0] + "','" + reader4[1] + "','" + reader4[2] + "','" + reader4[3] + "','" + reader4[4] + "','" + reader4[5] + "','" + reader4[6] + "','" + reader4[7] + "','" + reader4[8] + "','" + reader4[9] + "','" + reader4[10] + "','" + reader4[11] + "')", conn2).ExecuteNonQuery();
                //                conn2.Close();
                //            }
                //            conn2.Close();
                //        }
                //        catch (Exception a)
                //        {
                //            MessageBox.Show(a.Message);
                //        }

                //    }
                //    conn4.Close();
                //    db.setCursoerDefault();
                //    MessageBox.Show("ok");
                //}
                //catch (Exception a)
                //{
                //    MessageBox.Show(a.Message);
                //}
                db.setCursoerDefault();
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                // throw;
            }


        }
        public void loadIncome2(string id, string name)
        {
            try
            {
                db.setCursoerWait();
                //  MessageBox.Show("1");
                tempTOtalSale = 0;
                tempCredistSale = 0;
                tempChequeSale = 0;
                tempCardSale = 0;
                tempCashSale = 0;
                tempExpen = 0;
                invest = 0;
                tempCashRecevied = 0;
                tempCashGiven = 0;
                tempCashPaidReturn = 0;
                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("account", typeof(string));
                dt.Columns.Add("desc", typeof(string));


                dt.Columns.Add("CARD", typeof(double));
                dt.Columns.Add("CHEQUE", typeof(double));
                dt.Columns.Add("CREDIT", typeof(double));
                dt.Columns.Add("DEBIT", typeof(double));
                dt.Columns.Add("balance", typeof(double));

                //      MessageBox.Show("2");

                try
                {
                    amount = 0;
                    db.setCursoerWait();


                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' order by remark", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn3.Open();
                        reader3 = new SqlCommand("select * from invoiceRetail where id='" + reader[1].ToString().Split('-')[1] + "' and retail='" + true + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            conn3.Close();
                            if (reader.GetString(0).Equals("New Invoice"))
                            {
                                amount = amount + reader.GetDouble(2);
                                try
                                {
                                    conn3.Open();
                                    // MessageBox.Show("A"+reader[1].ToString().Split('-')[1]+"A");
                                    reader3 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader[1].ToString().Split('-')[1] + "'  ", conn3).ExecuteReader();
                                    if (reader3.Read())
                                    {
                                        if (reader3.GetBoolean(3))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select cheque,amount,cutomerId from chequeInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                //  MessageBox.Show(reader[1]+"/"+reader2[2]);

                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempChequeSale = tempChequeSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));

                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CHEQUE INVOICE (" + reader4.GetString(0).ToUpper() + " )" + " ,AMOUNT-" + reader2[1] + "", 0, reader2[0], 0, reader[2], amount);

                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(4))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select paid,amount,cutomerID from cardInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {

                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCardSale = tempCardSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CARD INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", reader2[0], 0, 0, reader[2], amount);

                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(2))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select balance,amount,customerId from creditInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {

                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCredistSale = tempCredistSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CREDIT INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", 0, 0, reader2[0], reader[2], amount);
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }

                                        else
                                        {
                                            tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                            tempCashSale = tempCashSale + reader.GetDouble(2);
                                            dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", 0, 0, 0, reader[2], amount);

                                        }
                                    }
                                    conn3.Close();


                                }
                                catch (Exception a)
                                {
                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                    //  MessageBox.Show(a.Message+"/"+a.StackTrace);
                                    conn2.Close();
                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", 0, 0, 0, reader[2], amount);
                                }


                            }
                            else if (reader.GetString(0).Equals("Cansel Invoice"))
                            {
                                dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE-CANSEL", 0, 0, 0, 0, 0);

                            }


                        }
                        conn3.Close();


                    }
                    conn.Close();




                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                //  MessageBox.Show("3");


                ds.Tables.Add(dt);

                ///  ds.WriteXmlSchema("expenAMOS2016.xml");
                // this.Dispose();
                //     MessageBox.Show(;
                cashBookView_ pp4 = new cashBookView_();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("date", dateFrom.Value.ToShortDateString());
                pp4.SetParameterValue("cashSale", tempCashSale);
                pp4.SetParameterValue("creditSale", tempCredistSale);
                pp4.SetParameterValue("cardSae", tempCardSale);
                pp4.SetParameterValue("chequeSae", db.setAmountFormat(tempChequeSale + ""));
                pp4.SetParameterValue("totalSale", (tempCashSale + tempChequeSale + tempCardSale + tempCredistSale));
                pp4.SetParameterValue("creditSetteleInvoice", tempCashRecevied);
                pp4.SetParameterValue("cashPaid", tempCashPaidReturn);
                pp4.SetParameterValue("purchasing", tempCashGiven);
                pp4.SetParameterValue("expenses", tempExpen);
                pp4.SetParameterValue("balance", ((tempCashSale + tempCashRecevied + invest) - (tempCashGiven + tempCashPaidReturn + tempExpen)));
                pp4.SetParameterValue("cashTotal", (tempCashSale + tempChequeSale + tempCardSale + tempCredistSale) - tempExpen);
                pp4.SetParameterValue("totalExpenses", (tempCashGiven + tempCashPaidReturn + tempExpen));
                // pp4.SetParameterValue("time", timeH);

                //   MessageBox.Show(comName);
                //  pp4.PrintToPrinter(1, false, 0, 0);
                // new test(pp4).Visible = true;
                crystalReportViewer1.ReportSource = pp4;

                db.setCursoerDefault();
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                // throw;
            }


        }

        Point p;
        private void accountList_Load(object sender, EventArgs e)
        {

            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            //dataGridView1.Width = width - 20;
            crystalReportViewer1.Height = height - 130;
            //  dataGridView1.Columns[0].Width = dataGridView1.Width - 470;



            p = crystalReportViewer1.Location;
            //   MessageBox.Show((width - dataGridView1.Width) / 2+"");
            p.X = (width - crystalReportViewer1.Width) / 2;
            crystalReportViewer1.Location = p;

            db = new DB();
            conn = db.createSqlConnection2();

            db2 = new DB();
            conn2 = db2.createSqlConnection2();


            db3 = new DB();
            conn3 = db3.createSqlConnection2();
            db4 = new DB();
            conn4 = db4.createSqlConnection();
            //  load();
            yearB = dateSearchB.Year;
            monthB = dateSearchB.Month;


        }

        private void accountList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            homeH.Enabled = true;
            // homeH.load();
            homeH.TopMost = true;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //   MessageBox.Show("a");

        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            // loadSale(idB, dateFrom.Value);

        }

        private void year_ValueChanged(object sender, EventArgs e)
        {

        }

        private void year_CloseUp(object sender, EventArgs e)
        {
            loadIncome(idB, "");


        }

        private void year_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                loadIncome(idB, "");

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //dataGridView1.Columns[8].ReadOnly = checkBox1.Checked;
        }

        private void dateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dateTo_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            //   MessageBox.Show("1");

        }

        private void dateTo_CloseUp(object sender, EventArgs e)
        {
            //if (checkBox1.Checked)
            //{
            //    loadIncome(idB, "");

            //}
            //else
            //{
            //    loadIncome2(idB, "");
            //}


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;

            // new addIncome(this, idB, true, false).Visible = true;

        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.Enabled = true;
            //new addIncomeEdit(this, idB, dateSearchB + "", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()).Visible = true;

        }

        private void PRINT_Click(object sender, EventArgs e)
        {

        }

        private void dateFrom_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        double balance = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            db.setCursoerWait();
            dt = new DataTable();
            ds = new DataSet();
            dt.Columns.Add("account", typeof(string));
            dt.Columns.Add("date", typeof(DateTime));


            dt.Columns.Add("CREDIT", typeof(double));
            dt.Columns.Add("DEBIT", typeof(double));
            dt.Columns.Add("BALANCE", typeof(double));


            try
            {

                balance = 0;
                conn.Open();
                reader = new SqlCommand("select * from cashSummery where date between '" + dateFrom.Value.ToShortDateString() + "' and '" + dateTo.Value.ToShortDateString() + "' order by date", conn).ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0).Equals("New Invoice"))
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader.GetString(1).Split('-')[1] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (reader2.GetBoolean(2))
                            {
                                conn4.Open();
                                reader4 = new SqlCommand("select balance,amount,customerId from creditInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                if (reader4.Read())
                                {
                                    if ((reader4.GetDouble(1) - reader4.GetDouble(0)) != 0)
                                    {
                                        balance = balance + (reader4.GetDouble(1) - reader4.GetDouble(0));
                                        dt.Rows.Add("Credit " + reader[1], reader.GetDateTime(3), reader4.GetDouble(1) - reader4.GetDouble(0), 0, balance);

                                    }
                                }

                                conn4.Close();

                            }
                            else if (reader2.GetBoolean(3))
                            {
                                conn4.Open();
                                reader4 = new SqlCommand("select paid,amount,cutomerId from chequeInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                if (reader4.Read())
                                {
                                    if ((reader4.GetDouble(1) - reader4.GetDouble(0)) != 0)
                                    {
                                        balance = balance + (reader4.GetDouble(1) - reader4.GetDouble(0));

                                        dt.Rows.Add("Cheque " + reader[1], reader.GetDateTime(3), reader4.GetDouble(1) - reader4.GetDouble(0), 0, balance);

                                    }


                                }

                                conn4.Close();

                                conn4.Open();

                                reader4 = new SqlCommand("select * from chequeInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                if (reader4.Read())
                                {
                                    balance = balance + (reader4.GetDouble(4));

                                    dt.Rows.Add("Cheque Payment Recive ( Chq no : " + reader4[5] + " ,cheq Date : " + reader4.GetDateTime(6).ToShortDateString() + " )", reader.GetDateTime(3), reader4.GetDouble(4), 0, balance);

                                }
                                conn4.Close();

                            }
                            else if (reader2.GetBoolean(1))
                            {
                                balance = balance + (reader.GetDouble(2));

                                dt.Rows.Add("Cash " + reader[1], reader.GetDateTime(3), reader.GetDouble(2), 0, balance);

                            }
                        }
                        conn2.Close();
                    }
                    else if (reader.GetString(1).Equals("EXPENCES-MANUAL"))
                    {
                        balance = balance - (reader.GetDouble(2));

                        dt.Rows.Add("Expenses " + reader.GetString(0).Split('/')[0], reader.GetDateTime(3), 0, reader.GetDouble(2), balance);
                    }
                    else if (reader.GetString(1).Equals("BANK"))
                    {
                        balance = balance - (reader.GetDouble(2));
                        conn2.Open();
                        reader2 = new SqlCommand("select customer,reason from receipt where id='" + reader.GetString(0).Split('/')[reader.GetString(0).Split('/').Length - 1] + "' and customer='" + "COMMERCIAL BANK-1147028599" + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            dt.Rows.Add(reader2[1] + " " + reader2.GetString(0).Split('/')[0], reader.GetDateTime(3), 0, reader.GetDouble(2), balance);

                        }
                        conn2.Close();
                    }
                    else
                    {

                        if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid"))
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select term,amount2 from receipt where id='" + reader.GetString(0).Split('-')[1] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                if (reader2.GetString(0).Equals("CASH"))
                                {
                                    balance = balance + (reader2.GetDouble(1));

                                    dt.Rows.Add("Cash Payment Recive Customer", reader.GetDateTime(3), reader2.GetDouble(1), 0, balance);

                                }
                                else if (reader2.GetString(0).Equals("CHEQUE"))
                                {
                                    conn4.Open();

                                    reader4 = new SqlCommand("select * from chequeInvoiceRetail2 where invoiceid='" + reader[0].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                    if (reader4.Read())
                                    {
                                        balance = balance + (reader4.GetDouble(4));

                                        dt.Rows.Add("Cheque Payment Recive Customer ( Chq no : " + reader4[5] + " ,cheq Date : " + reader4.GetDateTime(6).ToShortDateString() + " )", reader.GetDateTime(3), reader4.GetDouble(4), 0, balance);

                                    }
                                    conn4.Close();

                                }
                            }
                            conn2.Close();
                        }
                        else if (reader.GetString(0).Split('-')[0].ToString().Equals("GRN Credit Paid Cheque"))
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select term,amount2 from receipt2 where id='" + reader.GetString(0).Split('-')[1] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                if (reader2.GetString(0).Equals("CASH"))
                                {
                                    balance = balance - (reader2.GetDouble(1));

                                    dt.Rows.Add("Cash Payment Paid to Supplier ", reader.GetDateTime(3), 0, reader2.GetDouble(1), balance);

                                }
                                else if (reader2.GetString(0).Equals("CHEQUE"))
                                {
                                    conn4.Open();

                                    reader4 = new SqlCommand("select * from chequeGRN2 where invoiceid='" + reader[0].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                    if (reader4.Read())
                                    {
                                        balance = balance - (reader4.GetDouble(4));

                                        dt.Rows.Add("Cheque Payment Paid to Supplier ( Chq no : " + reader4[5] + " ,cheq Date : " + reader4.GetDateTime(6).ToShortDateString() + " )", reader.GetDateTime(3), 0, reader4.GetDouble(4), balance);

                                    }
                                    conn4.Close();

                                }
                            }
                            conn2.Close();
                        }
                        else if (reader.GetString(0).Split('-')[0].ToString().Equals("GRN Credit Paid"))
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select term,amount2 from receipt2 where id='" + reader.GetString(0).Split('-')[1] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                if (reader2.GetString(0).Equals("CASH"))
                                {
                                    balance = balance - (reader2.GetDouble(1));

                                    dt.Rows.Add("Cash Payment Paid to Supplier ", reader.GetDateTime(3), 0, reader2.GetDouble(1), balance);

                                }
                                else if (reader2.GetString(0).Equals("CHEQUE"))
                                {
                                    conn4.Open();

                                    reader4 = new SqlCommand("select * from chequeGRN2 where invoiceid='" + reader[0].ToString().Split('-')[1] + "'", conn4).ExecuteReader();
                                    if (reader4.Read())
                                    {
                                        balance = balance - (reader4.GetDouble(4));

                                        dt.Rows.Add("Cheque Payment Paid to Supplier ( Chq no : " + reader4[5] + " ,cheq Date : " + reader4.GetDateTime(6).ToShortDateString() + " )", reader.GetDateTime(3), 0, reader4.GetDouble(4), balance);

                                    }
                                    conn4.Close();

                                }
                            }
                            conn2.Close();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message + "/" + z.StackTrace);
                conn.Close();
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("cashSummery2.xml");
            cashBookView2 pp4 = new cashBookView2();
            pp4.SetDataSource(ds);
            pp4.SetParameterValue("date", dateFrom.Value.ToShortDateString());

            crystalReportViewer1.ReportSource = pp4;
            db.setCursoerDefault();
        }
    }
}
