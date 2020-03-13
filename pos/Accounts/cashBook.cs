using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class cashBook : Form
    {
        public cashBook(Form form, String user)
        {
            InitializeComponent();
            homeH = form;
            userH = user;
        }

        private Form homeH;
        // My Variable Start

        private DB db, db2, db3, db4, db5;
        private SqlConnection conn, conn2, conn3, conn4, conn5;
        private SqlDataReader reader, reader2, reader3, reader4, reader5;
        private Double cashBf, invest, amount, total, tempTOtalSale, tempCredistSale, tempChequeSale, tempCardSale, tempCashSale, tempExpen, tempCashRecevied, tempCashGiven, tempCashPaidReturn;
        private string idB, userH, nameB;
        public bool statese, isLoadSaleAll, isLoadSale;
        private Int32 yearB, monthB, index;
        private DateTime dateSearchB;

        private string cus = "";
        private string[] array;

        private DataSet ds;
        private DataTable dt;

        public void loadIncome(string id, string name)
        {
            try
            {
                crystalReportViewer1.ReportSource = null;
                //this.Close();
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
                cashOut = 0;
                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("account", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("INVOICE NUMBER", typeof(string));
                dt.Columns.Add("CUSTOMER", typeof(string));
                dt.Columns.Add("VEHICLE NUMBER", typeof(string));
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
                    reader = new SqlCommand("select * from cashBF where date='" + dateFrom.Value.AddDays(-1) + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cashBf = reader.GetDouble(1);
                        amount = amount + reader.GetDouble(1);
                        dt.Rows.Add(" ", "", "B/F", "", "", 0, 0, 0, 0, cashBf);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and remark='" + "INVEST-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount + reader.GetDouble(2);
                        invest = invest + reader.GetDouble(2);
                        dt.Rows.Add("INVEST", "", reader[0].ToString().ToUpper() + "", "", "", 0, 0, reader[2] + "", 0, amount);
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
                                    //          MessageBox.Show("A"+reader[1].ToString().Split('-')[1]+"A");
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
                                                    conn5.Open();
                                                    reader5 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[1].ToString().Split('-')[1] + "'", conn5).ExecuteReader();

                                                    if (reader5.Read())
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CHEQUE INVOICE (" + reader4.GetString(0).ToUpper() + "/ VEHICLE NO :" + reader5[0] + " )" + " ,AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], reader5[0], 0, reader2[0], 0, reader[2], amount);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CHEQUE INVOICE (" + reader4.GetString(0).ToUpper() + " )" + " ,AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], "", 0, reader2[0], 0, reader[2], amount);
                                                    }
                                                    conn5.Close();
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
                                                    conn5.Open();
                                                    reader5 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[1].ToString().Split('-')[1] + "'", conn5).ExecuteReader();

                                                    if (reader5.Read())
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CARD INVOICE (" + reader4.GetString(0).ToUpper() + "/ VEHICLE NO :" + reader5[0] + " )" + ", AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], reader5[0], reader2[0], 0, 0, reader[2], amount);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CARD INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], "", reader2[0], 0, 0, reader[2], amount);
                                                    }
                                                    conn5.Close();
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
                                                    conn5.Open();
                                                    reader5 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[1].ToString().Split('-')[1] + "'", conn5).ExecuteReader();

                                                    if (reader5.Read())
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CREDIT INVOICE (" + reader4.GetString(0).ToUpper() + "/ VEHICLE NO :" + reader5[0] + " )" + ", AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], reader5[0], 0, 0, reader2[0], reader[2], amount);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CREDIT INVOICE (" + reader4.GetString(0).ToUpper() + " )" + ", AMOUNT-" + reader2[1] + "", reader[1].ToString().Split('-')[1], reader4[0], "", 0, 0, reader2[0], reader[2], amount);
                                                    }
                                                    conn5.Close();
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select amount,amount,cutomerID from cashInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                                    conn5.Open();
                                                    reader5 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[1].ToString().Split('-')[1] + "'", conn5).ExecuteReader();

                                                    if (reader5.Read())
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE " + "/ VEHICLE NO :" + reader5[0], reader[1].ToString().Split('-')[1], reader4[0], reader5[0], 0, 0, 0, reader[2], amount);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", reader[1].ToString().Split('-')[1], "", reader4[0], "0", 0, 0, reader[2], amount);
                                                    }
                                                    conn5.Close();
                                                }
                                                else
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                                    conn5.Open();
                                                    reader5 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[1].ToString().Split('-')[1] + "'", conn5).ExecuteReader();

                                                    if (reader5.Read())
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE " + "/ VEHICLE NO :" + reader5[0], reader[1].ToString().Split('-')[1], "", reader5[0], 0, 0, 0, reader[2], amount);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", reader[1].ToString().Split('-')[1], "", "", 0, 0, 0, reader[2], amount);
                                                    }
                                                    conn5.Close();
                                                }
                                                conn4.Close();
                                            }
                                            conn2.Close();
                                        }
                                        conn3.Close();
                                    }
                                }
                                catch (Exception a)
                                {
                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                    //  MessageBox.Show(a.Message+"/"+a.StackTrace);
                                    conn2.Close();
                                    dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE", reader[1].ToString().Split('-')[1], "", "", 0, 0, 0, reader[2], amount);
                                }
                            }
                            else if (reader.GetString(0).Equals("Cansel Invoice"))
                            {
                                dt.Rows.Add("MAIN-SALE", reader[1].ToString().ToUpper() + " CASH INVOICE-CANSEL", reader[1].ToString().Split('-')[1], "", "", 0, 0, 0, 0, 0);
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
                                dt.Rows.Add("CREDIT SETTELEMNT (CASH)", "", reader[1].ToString().ToUpper() + "", "", "", 0, 0, 0, reader[2], amount);
                            }
                            else if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid Card"))
                            {
                                amount = amount + reader.GetDouble(2);

                                dt.Rows.Add("CREDIT SETTELEMNT (CARD)", "", reader[1].ToString().ToUpper() + "", "", "", 0, 0, 0, reader[2], amount);
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
                                dt.Rows.Add("CREDIT SETTELEMNT CHEQUE", "", reader[1].ToString().ToUpper() + "", "", "", 0, reader[2], 0, 0, amount);
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
                                dt.Rows.Add("CREDIT SETTELEMNT", "", reader[1].ToString().ToUpper() + "", "", "", 0, 0, reader[2], 0, amount);
                            }
                        }
                        catch (Exception A)
                        {
                            //  MessageBox.Show(A.Message);
                        }
                    }
                    conn.Close();
                    var exACCOUNT = "";
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and remark='" + "EXPENCES-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select B.name from receipt as a,ExpensesAccounts as b where a.id='" + reader.GetString(0).Split('/')[1] + "' and a.customer=b.id", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                exACCOUNT = reader2[0] + "";
                            }
                            conn2.Close();
                        }
                        catch (Exception A)
                        {
                            //   MessageBox.Show(A.Message);
                            conn2.Close();
                        }

                        amount = amount - reader.GetDouble(2);
                        tempExpen = tempExpen + reader.GetDouble(2);
                        dt.Rows.Add("EXPENSES " + exACCOUNT.ToUpper(), "", reader[0].ToString().ToUpper() + "", "", "", 0, 0, reader[2] + "", 0, amount);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and remark='" + "CASH OUT" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        cashOut = cashOut + reader.GetDouble(2);
                        dt.Rows.Add("CASH OUT", "", reader[0].ToString().ToUpper() + "", "", "", 0, 0, reader[2] + "", 0, amount);
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + dateFrom.Value.ToShortDateString() + "' and reason='" + "CASH PAID" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        tempCashPaidReturn = tempCashPaidReturn + reader.GetDouble(2);
                        dt.Rows.Add("CASH PAID-RETURN INVOICE", "", reader[1].ToString().ToUpper() + "", "", "", 0, 0, reader[2] + "", 0, amount);
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

                ds.WriteXmlSchema("expenAMOS20181021.xml");
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
                pp4.SetParameterValue("balance", ((tempCashSale + tempCashRecevied + invest + cashBf) - (tempCashGiven + tempCashPaidReturn + tempExpen + cashOut)));
                pp4.SetParameterValue("cashTotal", (tempCashSale + tempChequeSale + tempCardSale + tempCredistSale) - tempExpen - cashOut);
                pp4.SetParameterValue("totalExpenses", (tempCashGiven + tempCashPaidReturn + tempExpen));
                pp4.SetParameterValue("cashOut", (cashOut));
                // pp4.SetParameterValue("time", timeH);

                //   if (DateTime.Now.Date == dateFrom.Value.Date)
                {
                    conn.Open();
                    new SqlCommand("delete from cashBF where date='" + dateFrom.Value + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into cashBF values ('" + dateFrom.Value + "','" + ((tempCashSale + tempCashRecevied + invest + cashBf) - (tempCashGiven + tempCashPaidReturn + tempExpen + cashOut)) + "')", conn).ExecuteNonQuery();
                    conn.Close();
                }
                conn.Open();

                conn.Close();

                crystalReportViewer1.ReportSource = pp4;

                db.setCursoerDefault();
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                // throw;
            }
        }

        private double cashOut = 0.0;

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

        private Point p;

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
            db5 = new DB();
            conn5 = db5.createSqlConnection();
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
            loadIncome(idB, "");

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
    }
}