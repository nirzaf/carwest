using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace pos
{
    class invoicePrint
    {
        string states = "", cusName, cusCompany, cusAddress, comName, comAddres, comcontact, cusMobile, cusFax, cusCode, desc, customer, user, subTotal, balance, paid, payType;
        invoiceReportPOS pp;
        invoiceFormatHalf pp4;
        char[] charArray;
        ArrayList array;
        string wCode, userH;
        SqlDataReader reader2;
        SqlConnection conn2;
        DB db2;
        Int32 warenty, count;
        invoiceReportunpiad pp2;
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
        DataTable dt; DataSet ds;
        public void setprintCheque(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("dec", typeof(string));
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("rate", typeof(string));
                dt.Columns.Add("sub", typeof(string));

                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";

                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    // MessageBox.Show();
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }
                    var rate = "";
                    if (dataGridView.Rows[i].Cells[5].Value.ToString().Equals("0"))
                    {
                        rate = setAmountFormat(dataGridView.Rows[i].Cells[4].Value.ToString()) + "  *  " + dataGridView.Rows[i].Cells[6].Value.ToString();
                    }
                    else
                    {
                        rate = "(" + setAmountFormat(dataGridView.Rows[i].Cells[4].Value.ToString()) + "  -  " + dataGridView.Rows[i].Cells[5].Value.ToString() + ")*" + dataGridView.Rows[i].Cells[6].Value.ToString();

                    }

                    dt.Rows.Add(dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + wCode, dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), rate, setAmountFormat(dataGridView.Rows[i].Cells[7].Value.ToString()));
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceThisara.xml");
                pp = new invoiceReportPOS();
                pp.SetDataSource(ds);
                pp.SetParameterValue("customer", cusName);
                pp.SetParameterValue("time", timeH);
                pp.SetParameterValue("date", dateH);
                pp.SetParameterValue("subTotal", setAmountFormat(subTotal + ""));
                pp.SetParameterValue("balance", setAmountFormat(balance + ""));
                pp.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp.SetParameterValue("invoiceID", invoiceNo);
                pp.SetParameterValue("term", payType);
                pp.SetParameterValue("item", dataGridView.Rows.Count);
                pp.SetParameterValue("user", user.ToString().ToUpper());

                pp.SetParameterValue("comName", comName.ToString().ToUpper());
                pp.SetParameterValue("comAddress", comAddres.ToString().ToUpper());
                pp.SetParameterValue("comContact", comcontact.ToString().ToUpper());

                // MessageBox.Show("2");
                pp.PrintToPrinter(1, false, 0, 0);
                // new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintChequeUnPay(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("dec", typeof(string));
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("rate", typeof(string));
                dt.Columns.Add("sub", typeof(string));

                //  MessageBox.Show(s);

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";

                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;

                    conn.Open();
                    reader = new SqlCommand("select name from login where id='" + user + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        userH = reader[0] + "".ToUpper();
                    }
                    //   MessageBox.Show(user);
                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    // MessageBox.Show();
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }
                    var rate = "";
                    if (dataGridView.Rows[i].Cells[5].Value.ToString().Equals("0"))
                    {
                        rate = setAmountFormat(dataGridView.Rows[i].Cells[4].Value.ToString()) + "  *  " + dataGridView.Rows[i].Cells[6].Value.ToString();
                    }
                    else
                    {
                        rate = "(" + setAmountFormat(dataGridView.Rows[i].Cells[4].Value.ToString()) + "  -  " + dataGridView.Rows[i].Cells[5].Value.ToString() + ")*" + dataGridView.Rows[i].Cells[6].Value.ToString();

                    }

                    dt.Rows.Add(dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + wCode, dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), rate, setAmountFormat(dataGridView.Rows[i].Cells[7].Value.ToString()));
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceThisara.xml");
                pp2 = new invoiceReportunpiad();
                pp2.SetDataSource(ds);
                pp2.SetParameterValue("customer", cusName);
                pp2.SetParameterValue("time", timeH);
                pp2.SetParameterValue("date", dateH);
                pp2.SetParameterValue("subTotal", setAmountFormat(subTotal + ""));
                pp2.SetParameterValue("balance", setAmountFormat(balance + ""));
                pp2.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp2.SetParameterValue("invoiceID", invoiceNo);
                pp2.SetParameterValue("term", payType);
                pp2.SetParameterValue("item", dataGridView.Rows.Count);
                pp2.SetParameterValue("user", userH.ToString().ToUpper());

                pp.SetParameterValue("comName", comName, ToString().ToUpper());
                pp.SetParameterValue("comAddress", comAddres, ToString().ToUpper());
                pp.SetParameterValue("comContact", comcontact, ToString().ToUpper());
                pp2.PrintToPrinter(1, false, 0, 0);
                //  new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.Message + "//" + userH);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setprintCheque2(string invoiceNo, SqlConnection conn, SqlDataReader reader)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("dec", typeof(string));
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("rate", typeof(string));
                dt.Columns.Add("sub", typeof(string));

                //  MessageBox.Show(s);

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select customerID,userid,subTotal,cash,balance,paytype from invoiceretail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        customer = reader[0] + "";
                        user = reader[1] + "";
                        subTotal = reader[2] + "";
                        paid = reader[3] + "";
                        balance = reader[4] + "";
                        payType = reader[5] + "";
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";

                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;

                    conn.Open();
                    reader = new SqlCommand("select name from login where id='" + user + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        userH = reader[0] + "".ToUpper();
                    }
                    //   MessageBox.Show(user);
                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                count = 0;
                conn.Open();
                reader = new SqlCommand("select * from invoiceretaildetail where invoiceid='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    count++;

                    // MessageBox.Show();
                    if (reader[1].ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + reader[9].ToString().ToUpper() + "]";
                    }
                    var rate = "";
                    if (reader[7].ToString().Equals("0"))
                    {
                        rate = setAmountFormat(reader[3].ToString()) + "  *  " + reader[2].ToString();
                    }
                    else
                    {
                        rate = "(" + setAmountFormat(reader[3].ToString()) + "  -  " + reader[7].ToString() + ")*" + reader[2].ToString();

                    }

                    dt.Rows.Add(reader[10].ToString().ToUpper() + wCode, reader[1].ToString().ToUpper(), rate, setAmountFormat(reader[4].ToString()));
                }
                conn.Close();
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceThisara.xml");
                pp = new invoiceReportPOS();
                pp.SetDataSource(ds);
                pp.SetParameterValue("customer", cusName);
                pp.SetParameterValue("time", timeH);
                pp.SetParameterValue("date", dateH);
                pp.SetParameterValue("subTotal", setAmountFormat(subTotal + ""));
                pp.SetParameterValue("balance", setAmountFormat(balance + ""));
                pp.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp.SetParameterValue("invoiceID", invoiceNo);
                pp.SetParameterValue("term", payType);
                pp.SetParameterValue("item", count);
                pp.SetParameterValue("user", userH.ToString().ToUpper());

                pp.PrintToPrinter(1, false, 0, 0);
                // new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
                MessageBox.Show("Send To Print Successfully");
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.Message + "//" + userH);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprint(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";
                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }



                    dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString(), dataGridView.Rows[i].Cells[7].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                ds.WriteXmlSchema("invoiceHalf.xml");
                pp = new invoiceReportPOS();
                pp.SetDataSource(ds);
                pp.SetParameterValue("customerName", cusName.ToUpper());
                pp.SetParameterValue("customerAddres", cusAddress.ToUpper());
                pp.SetParameterValue("customerNumber", cusMobile.ToUpper());

                pp.SetParameterValue("companyName", comName.ToUpper());
                pp.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp.SetParameterValue("time", timeH);
                pp.SetParameterValue("date", dateH);
                pp.SetParameterValue("cash", setAmountFormat(subTotal + ""));
                pp.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp.SetParameterValue("invoiceID", invoiceNo);


                // MessageBox.Show("2");
                // pp.PrintToPrinter(1, false, 0, 0);
                // new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintExpenses(SqlConnection conn, SqlDataReader reader, string dateTo, string dateFrom)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));

                try
                {

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date between '" + dateFrom + "' and  '" + dateTo + "' and remark='" + "EXPENCES-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetDateTime(3).ToShortDateString(), reader[0] + "", reader[2] + "", 0, 0, 0);
                    }
                    conn.Close();

                }
                catch (Exception)
                {

                }


                ds.Tables.Add(dt);

                // ds.WriteXmlSchema("invoiceHalf.xml");
                invoiceReportEx pp = new invoiceReportEx();
                pp.SetDataSource(ds);
                pp.SetParameterValue("comName", "From " + dateFrom + " To " + dateTo);



                // MessageBox.Show("2");
                // pp.PrintToPrinter(1, false, 0, 0);
                new expensesView(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintCasOut(SqlConnection conn, SqlDataReader reader, string dateTo, string dateFrom)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));

                try
                {

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date between '" + dateFrom + "' and  '" + dateTo + "' and remark='" + "CASH OUT" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetDateTime(3).ToShortDateString(), reader[0] + "", reader[2] + "", 0, 0, 0);
                    }
                    conn.Close();

                }
                catch (Exception)
                {

                }


                ds.Tables.Add(dt);

                // ds.WriteXmlSchema("invoiceHalf.xml");
                invoiceReportCashout pp = new invoiceReportCashout();
                pp.SetDataSource(ds);
                pp.SetParameterValue("comName", "From " + dateFrom + " To " + dateTo);



                // MessageBox.Show("2");
                // pp.PrintToPrinter(1, false, 0, 0);
                new cashOutView(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setprintHalfInvoiceService(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string vehcileNO, string vehcileDesc, string meterNow, string MeterNext)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "CONTACT : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {

                    conn.Open();
                    reader = new SqlCommand("select categorey,description from item where code='" + dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper() + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString(), dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString());

                    }
                    conn.Close();

                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "", serviceBy = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time,pono from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();
                    serviceBy = reader[2] + "";
                }
                conn.Close();

                ds.Tables.Add(dt);

                //if (payType.Equals("CASH MEMO"))
                //{
                //    invoiceFormatHalfService_ pp4 = new invoiceFormatHalfService_();
                //    pp4.SetDataSource(ds);
                //    pp4.SetParameterValue("customerName", cusName.ToUpper());
                //    pp4.SetParameterValue("serviceBy", serviceBy.ToUpper());
                //    pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                //    pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                //    //  MessageBox.Show(vehcileNO);
                //    pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                //    pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                //    pp4.SetParameterValue("meterNow", meterNow);

                //    pp4.SetParameterValue("meterNext", MeterNext);
                //    pp4.SetParameterValue("companyName", comName.ToUpper());
                //    pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                //    pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                //    pp4.SetParameterValue("time", timeH);
                //    pp4.SetParameterValue("date", dateH.Split('/')[1] + "/" + dateH.Split('/')[0] + "/" + dateH.Split('/')[2]);
                //    pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                //    pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                //    pp4.SetParameterValue("invoiceNo", invoiceNo);


                //    pp4.SetParameterValue("term", payType.ToUpper());

                //    //   MessageBox.Show(comName);
                //    pp4.PrintToPrinter(1, false, 0, 0);
                //    new test(pp4).Visible = true;
                //}
                //else
                {
                    invoiceFormatHalfService pp4 = new invoiceFormatHalfService();
                    pp4.SetDataSource(ds);
                    pp4.SetParameterValue("customerName", cusName.ToUpper());
                    pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                    pp4.SetParameterValue("serviceBy", serviceBy.ToUpper());
                    pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                    //  MessageBox.Show(vehcileNO);
                    pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                    pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                    pp4.SetParameterValue("meterNow", meterNow);

                    pp4.SetParameterValue("meterNext", MeterNext);
                    pp4.SetParameterValue("companyName", comName.ToUpper());
                    pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                    pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                    pp4.SetParameterValue("time", timeH);
                    pp4.SetParameterValue("date", dateH.Split('/')[1] + "/" + dateH.Split('/')[0] + "/" + dateH.Split('/')[2]);
                    pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                    pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                    pp4.SetParameterValue("invoiceNo", invoiceNo);


                    pp4.SetParameterValue("term", payType.ToUpper());
                    pp4.SetParameterValue("cashier", user);

                    //   MessageBox.Show(comName);
                    pp4.PrintToPrinter(1, false, 0, 0);
                }
                //    ds.WriteXmlSchema("invoiceHalf.xml");




                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintHalfInvoiceServiceSaveasPDF(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string vehcileNO, string vehcileDesc, string meterNow, string MeterNext)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {

                    conn.Open();
                    reader = new SqlCommand("select categorey,description from item where code='" + dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper() + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString(), dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString());

                    }
                    conn.Close();

                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                invoiceFormatHalfService pp4 = new invoiceFormatHalfService();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                //  MessageBox.Show(vehcileNO);
                pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                pp4.SetParameterValue("meterNow", meterNow);

                pp4.SetParameterValue("meterNext", MeterNext);
                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", timeH);
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", payType.ToUpper());
                pp4.SetParameterValue("serviceby", payType.ToUpper());

                // MessageBox.Show(invoiceNo);
                // pp4.PrintToPrinter(1, false, 0, 0);
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                CrDiskFileDestinationOptions.DiskFileName = "C:/" + "/Invoice/ " + invoiceNo.Split('/')[1] + ".pdf";
                CrExportOptions = pp4.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                pp4.Export();
                MessageBox.Show("Export Succesfully");
                // new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message + "/" + s.StackTrace);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setqut(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string vehcileNO, string vehcileDesc, string meterNow, string MeterNext)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {



                    dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[3].Value.ToString(), dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date from qua where id='" + invoiceNo + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                qutView pp4 = new qutView();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                //  MessageBox.Show(vehcileNO);
                pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                pp4.SetParameterValue("meterNow", meterNow);

                pp4.SetParameterValue("meterNext", MeterNext);
                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", "");
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", payType.ToUpper());
                pp4.SetParameterValue("total", setAmountFormat(subTotal + ""));
                //   MessageBox.Show(comName);
                pp4.PrintToPrinter(1, false, 0, 0);


                //new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message + "/" + s.StackTrace);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setqutSaveAsPdf(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string vehcileNO, string vehcileDesc, string meterNow, string MeterNext)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {



                    dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[3].Value.ToString(), dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date from qua where id='" + invoiceNo + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                qutView pp4 = new qutView();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                //  MessageBox.Show(vehcileNO);
                pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                pp4.SetParameterValue("meterNow", meterNow);

                pp4.SetParameterValue("meterNext", MeterNext);
                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", "");
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", payType.ToUpper());
                pp4.SetParameterValue("total", setAmountFormat(subTotal + ""));
                //   MessageBox.Show(comName);
                //  pp4.PrintToPrinter(1, false, 0, 0);

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                CrDiskFileDestinationOptions.DiskFileName = "C:/" + "/Quat/ " + invoiceNo + ".pdf";
                CrExportOptions = pp4.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                pp4.Export();
                //new test(pp4).Visible = true;
                MessageBox.Show("Export Succesfully");
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message + "/" + s.StackTrace);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setprintHalfInvoiceServiceRE(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string paid, string balance, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string vehcileNO, string vehcileDesc, string meterNow, string MeterNext)
        {

            try
            {
                // MessageBox.Show(customer);

                //  MessageBox.Show("145");
                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Mobi : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Gen : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    //  MessageBox.Show(customer+"");
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {

                    dt.Rows.Add("0", "0", "0", "0", "0", "0");

                    //dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[3].Value.ToString(), dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date from receipt where id='" + invoiceNo + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                invoiceFormatHalfServiceRes pp4 = new invoiceFormatHalfServiceRes();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());
                //  MessageBox.Show(vehcileNO);
                pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                pp4.SetParameterValue("meterNow", meterNow);

                pp4.SetParameterValue("meterNext", MeterNext);
                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", timeH);
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", payType.ToUpper());

                //   MessageBox.Show(comName);
                pp4.PrintToPrinter(1, false, 0, 0);


                //new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message + "/" + s.StackTrace);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setprintReceiprt(string id, SqlConnection conn, SqlDataReader reader, string user)
        {

            try
            {
                // MessageBox.Show(paid+"/"+balance);

                db2 = new DB();
                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("invoiceID", typeof(string));
                dt.Columns.Add("term", typeof(string));
                dt.Columns.Add("invoiceAmount", typeof(float));
                dt.Columns.Add("profit", typeof(float));
                dt.Columns.Add("date", typeof(string));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }

                var qty = 0.0;

                //  MessageBox.Show("1");
                dt.Rows.Add("0", "0", "0", "0", "0", "0", "0");
                dt.Rows.Add("0", "0", "0", "0", "0", "0", "0");
                dt.Rows.Add("0", "0", "0", "0", "0", "0", "0");
                //  MessageBox.Show("2");


                ds.Tables.Add(dt);

                //     ds.WriteXmlSchema("invoiceHalf.xml");
                receipt pp = new receipt();
                pp.SetDataSource(ds);
                conn.Open();
                reader = new SqlCommand("select * from receipt where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    // MessageBox.Show(id);
                    pp.SetParameterValue("date", reader.GetDateTime(1).ToShortDateString());
                    pp.SetParameterValue("comName", comName);
                    pp.SetParameterValue("comAddress", comAddres);
                    pp.SetParameterValue("comContact", comcontact);

                    pp.SetParameterValue("comCntact2", "");

                    pp.SetParameterValue("comReg", "");
                    pp.SetParameterValue("refNo", reader.GetString(2).ToUpper());
                    pp.SetParameterValue("no", id);

                    pp.SetParameterValue("amount", reader[4]);

                    pp.SetParameterValue("remark", reader[7]);

                    pp.SetParameterValue("amount2", setAmountFormat(reader[5] + ""));


                    pp.SetParameterValue("term", reader[8]);
                    pp.SetParameterValue("reason", reader[6]);
                    try
                    {
                        cusName = "";
                        cusCompany = "";
                        cusAddress = ""; cusMobile = "";
                        //94
                        var a = reader[3] + "";
                        conn2 = new DB().createSqlConnection();
                        //  MessageBox.Show(a+"");
                        conn2.Open();
                        reader2 = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + a + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader2[1].ToString().ToLower());
                            cusCode = "[" + a + "]";

                        }
                        else
                        {
                            cusName = a;
                            cusCode = "";
                        }
                        reader2.Close();
                        conn2.Close();
                        cusName = cusName + cusCode;
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + "/" + a.StackTrace);
                        //  throw;
                        cusName = customer;
                        conn.Close();
                    }
                    pp.SetParameterValue("customer", "RECEVIED WITH THANKS , " + cusName.ToUpper() + " " + reader[8].ToString().ToUpper() + " FOR THE SUM OF RUPEES" + reader[4].ToString().ToUpper() + " FOR " + reader[6]);

                }
                conn.Close();



                // MessageBox.Show("2");
                pp.PrintToPrinter(1, false, 0, 0);
                //new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintReceiprtREturn(string id, SqlConnection conn, SqlDataReader reader, string user)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();
                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("invoiceID", typeof(string));
                dt.Columns.Add("term", typeof(string));
                dt.Columns.Add("invoiceAmount", typeof(float));
                dt.Columns.Add("profit", typeof(float));
                dt.Columns.Add("date", typeof(string));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }

                var qty = 0.0;


                dt.Rows.Add("0", "0", "0", "0", "0", "0", "0");



                ds.Tables.Add(dt);

                //     ds.WriteXmlSchema("invoiceHalf.xml");
                receipt pp = new receipt();
                pp.SetDataSource(ds);
                conn.Open();
                reader = new SqlCommand("select * from receipt where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    // MessageBox.Show(id);
                    pp.SetParameterValue("date", reader.GetDateTime(1).ToShortDateString());
                    pp.SetParameterValue("comName", comName);
                    pp.SetParameterValue("comAddress", comAddres);
                    pp.SetParameterValue("comContact", comcontact);

                    pp.SetParameterValue("comCntact2", "");

                    pp.SetParameterValue("comReg", "");
                    pp.SetParameterValue("refNo", reader.GetString(2).ToUpper());
                    pp.SetParameterValue("no", id);

                    pp.SetParameterValue("amount", reader[4]);

                    pp.SetParameterValue("remark", reader[7]);

                    pp.SetParameterValue("amount2", setAmountFormat(reader[5] + ""));


                    pp.SetParameterValue("term", reader[8]);
                    pp.SetParameterValue("reason", reader[6]);
                    try
                    {
                        cusName = "";
                        cusCompany = "";
                        cusAddress = ""; cusMobile = "";
                        //94
                        var a = reader[3] + "";
                        conn2 = new DB().createSqlConnection();
                        //  MessageBox.Show(a+"");
                        conn2.Open();
                        reader2 = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + a + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader2[1].ToString().ToLower());
                            cusCode = "[" + a + "]";

                        }
                        else
                        {
                            cusName = a;
                            cusCode = "";
                        }
                        reader2.Close();
                        conn2.Close();
                        cusName = cusName + cusCode;
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + "/" + a.StackTrace);
                        //  throw;
                        cusName = customer;
                        conn.Close();
                    }
                    pp.SetParameterValue("customer", reader[8].ToString().ToUpper() + " RETURN FOR  " + cusName.ToUpper() + " FOR THE SUM OF RUPEES " + reader[4].ToString().ToUpper().ToUpper());
                    // pp.SetParameterValue("customer", "RECEVIED WITH THANKS FROM " + cusName.ToUpper() + " " + reader[8].ToString().ToUpper() + " FOR THE SUM OF RUPEES" + reader[4].ToString().ToUpper() + " IN SETTELMENT OF INVOICE " + reader[2]);

                }
                conn.Close();



                // MessageBox.Show("2");
                pp.PrintToPrinter(1, false, 0, 0);
                //   new test(pp).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }

        public void setprintHalfInvoiceDBService(string invoiceNo, SqlConnection conn, SqlDataReader reader)
        {

            try
            {
                string vehcileNO = "", vehcileDesc = "", meterNow = "", MeterNext = "";
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select customerID,cash,balance from invoiceRetail where id='" + invoiceNo + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        customer = reader[0] + "";
                        paid = reader[1] + "";
                        balance = reader[2] + "";

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
                    reader = new SqlCommand("select vehicleno,descrip,meterNOw,meterNext from vehicle where invoiceID='" + invoiceNo + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        vehcileNO = reader[0] + "";
                        vehcileDesc = reader[1] + "";
                        meterNow = reader[2] + "";
                        MeterNext = reader[2] + "";

                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";
                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }

                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from invoiceRetailDetail where invoiceID='" + invoiceNo + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader[9].ToString().Equals(""))
                        {
                            wCode = "";
                        }
                        else
                        {
                            wCode = "[" + reader[9].ToString().ToUpper() + "]";
                        }



                        dt.Rows.Add(reader[1].ToString().ToString().ToUpper(), reader[10].ToString().ToUpper() + " " + wCode, reader[3].ToString(), reader[7].ToString(), reader[2].ToString(), reader[4].ToString());

                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                pp4 = new invoiceFormatHalf();
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());

                pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                pp4.SetParameterValue("meterNow", meterNow);

                pp4.SetParameterValue("meterNext", MeterNext);
                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", timeH);
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                pp4.SetParameterValue("invoiceNo", invoiceNo);



                // MessageBox.Show("2");
                pp4.PrintToPrinter(1, false, 0, 0);
                //new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintqUATE(string invoiceNo, string customer, DataGridView dataGridView, DateTime date, SqlConnection conn, SqlDataReader reader, string user, DateTime expireDate)
        {

            try
            {
                //   MessageBox.Show(paid+"/"+balance);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "Tel : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "Fax : " + reader[3];
                        }

                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception)
                {
                    conn.Close();
                }
                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());
                        cusCode = "[" + customer + "]";
                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "CONTACT : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();
                    cusName = cusName + cusCode;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }



                    dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString(), dataGridView.Rows[i].Cells[7].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date from qua where id='" + invoiceNo + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();


                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                quation pp4 = new quation();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());

                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());

                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("invoiceNo", invoiceNo);
                pp4.SetParameterValue("vehicleDesc", expireDate.ToShortDateString());


                // MessageBox.Show("2");
                pp4.PrintToPrinter(1, false, 0, 0);
                //new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintHalfInvoice(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string discount, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string refre, string po, string nbt, string vat, string nbtPre, string vatPre, string payble)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(string));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "HUNTING : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "GENERAL : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "/ E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        //  MessageBox.Show("");

                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }



                    dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString() + " " + dataGridView.Rows[i].Cells[8].Value.ToString(), dataGridView.Rows[i].Cells[7].Value.ToString());
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                pp4 = new invoiceFormatHalf();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());

                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", timeH);
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", payType.ToUpper());
                pp4.SetParameterValue("ref", refre.ToUpper());

                pp4.SetParameterValue("po", po.ToUpper());

                pp4.SetParameterValue("subTotal", setAmountFormat(subTotal));

                pp4.SetParameterValue("disc", setAmountFormat(discount));

                pp4.SetParameterValue("netTotal", setAmountFormat(Math.Round(Double.Parse(subTotal) - Double.Parse(discount), 2) + ""));
                pp4.SetParameterValue("vat", setAmountFormat(vat + ""));
                pp4.SetParameterValue("nbt", setAmountFormat(nbt + ""));
                //  MessageBox.Show(vatPre);
                pp4.SetParameterValue("vatPre", vatPre);
                pp4.SetParameterValue("nbtPre", nbtPre);
                pp4.SetParameterValue("payble", setAmountFormat(payble + ""));
                //   MessageBox.Show(comName);
                pp4.PrintToPrinter(1, false, 0, 0);
                // new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintHalfInvoiceReturn(string invoiceNo, string customer, string payType, DataGridView dataGridView, string subTotal, string discount, DateTime date, SqlConnection conn, SqlDataReader reader, string user, string refre, string po, string nbt, string vat, string nbtPre, string vatPre, string payble)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("code", typeof(string));
                dt.Columns.Add("desc", typeof(string));
                dt.Columns.Add("uPrice", typeof(double));
                dt.Columns.Add("disc", typeof(double));
                dt.Columns.Add("qty", typeof(double));
                dt.Columns.Add("tPrice", typeof(double));
                //  MessageBox.Show(s);


                try
                {
                    comName = "";
                    comAddres = "";
                    comcontact = "";
                    conn.Open();
                    reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                    if (reader.Read())
                    {

                        comName = reader.GetString(0).ToUpper();
                        comAddres = reader.GetString(1).ToUpper();
                        if (!reader.GetString(2).Equals(""))
                        {
                            comcontact = "HUNTING : " + reader[2] + " / ";
                        }
                        if (!reader.GetString(3).Equals(""))
                        {
                            comcontact = comcontact + "GENERAL : " + reader[3];
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            comcontact = comcontact + "/ E-Mail : " + reader[4];
                        }
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                try
                {
                    cusName = "";
                    cusCompany = "";
                    cusAddress = ""; cusMobile = "";
                    //94
                    conn.Open();
                    reader = new SqlCommand("select name,company,address,mobileNo,LandNo,FaxNumber from customer where id='" + customer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cusName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower());

                        cusAddress = reader.GetString(2).ToUpper();
                        if (!reader.GetString(3).Equals(""))
                        {
                            cusMobile = "HOT LINE : " + reader.GetString(3);
                        }
                        if (!reader.GetString(4).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "GENERAL : " + reader.GetString(4);
                        }
                        if (!reader.GetString(5).Equals(""))
                        {
                            cusMobile = cusMobile + " " + "FAX : " + reader.GetString(5);
                        }
                    }
                    else
                    {
                        cusName = customer;
                        cusCode = "";
                    }
                    reader.Close();
                    conn.Close();

                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    //  throw;
                    cusName = customer;
                    conn.Close();
                }
                var qty = 0.0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells[2].Value.ToString().Equals(""))
                    {
                        wCode = "";
                    }
                    else
                    {
                        wCode = "[" + dataGridView.Rows[i].Cells[2].Value.ToString().ToUpper() + "]";
                    }


                    if (dataGridView.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        dt.Rows.Add(dataGridView.Rows[i].Cells[1].Value.ToString().ToUpper(), dataGridView.Rows[i].Cells[3].Value.ToString().ToUpper() + " " + wCode, dataGridView.Rows[i].Cells[4].Value.ToString(), dataGridView.Rows[i].Cells[5].Value.ToString(), dataGridView.Rows[i].Cells[6].Value.ToString(), dataGridView.Rows[i].Cells[7].Value.ToString());

                    }
                }
                // dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                string dateH = "", timeH = "";
                conn.Open();
                //MessageBox.Show("1");
                reader = new SqlCommand("select date,time from invoiceRetail where id='" + invoiceNo.Split('-')[1] + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dateH = reader.GetDateTime(0).ToShortDateString();
                    timeH = reader.GetTimeSpan(1).ToString();

                }
                conn.Close();

                ds.Tables.Add(dt);

                //    ds.WriteXmlSchema("invoiceHalf.xml");
                pp4 = new invoiceFormatHalf();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("customerName", cusName.ToUpper());
                pp4.SetParameterValue("customerAddress", cusAddress.ToUpper());
                pp4.SetParameterValue("customerNumber", cusMobile.ToUpper());

                pp4.SetParameterValue("companyName", comName.ToUpper());
                pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                pp4.SetParameterValue("time", timeH);
                pp4.SetParameterValue("date", dateH);
                pp4.SetParameterValue("invoiceNo", invoiceNo);

                pp4.SetParameterValue("term", "RETURN-NOTE");
                pp4.SetParameterValue("ref", refre.ToUpper());

                pp4.SetParameterValue("po", po.ToUpper());

                pp4.SetParameterValue("subTotal", setAmountFormat(subTotal));

                pp4.SetParameterValue("disc", setAmountFormat(discount));

                pp4.SetParameterValue("netTotal", setAmountFormat(Math.Round(Double.Parse(subTotal) - Double.Parse(discount), 2) + ""));
                pp4.SetParameterValue("vat", setAmountFormat(vat + ""));
                pp4.SetParameterValue("nbt", setAmountFormat(nbt + ""));
                //  MessageBox.Show(vatPre);
                pp4.SetParameterValue("vatPre", vatPre);
                pp4.SetParameterValue("nbtPre", nbtPre);
                pp4.SetParameterValue("payble", setAmountFormat(payble + ""));
                //   MessageBox.Show(comName);
                pp4.PrintToPrinter(1, false, 0, 0);
                // new test(pp4).Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintSalary(string MOnth, string Year, SqlConnection conn, SqlDataReader reader)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("EMP ID", typeof(string));//reader[1]
                dt.Columns.Add("NAME", typeof(string));//reader.GetString(0)
                dt.Columns.Add("WORKING DAYS", typeof(int));//reader[4]
                dt.Columns.Add("ABSENT DAYS", typeof(int));//reader[5]
                dt.Columns.Add("BASIC", typeof(double));//reader[6]
                dt.Columns.Add("ALLOWANCE", typeof(double));//reader[8]
                dt.Columns.Add("INCENTIVE", typeof(double));//reader[10]
                dt.Columns.Add("TOTAL EARNING", typeof(double));//reader[14]
                dt.Columns.Add("GROSS SALARY", typeof(double));//reader[11]
                dt.Columns.Add("ADAVNCE", typeof(double));//reader[18]
                dt.Columns.Add("EPF 8%", typeof(double));//reader[15]
                dt.Columns.Add("NO PAY", typeof(double));//reader[16]
                dt.Columns.Add("TOTAL DEDUCTION", typeof(double));//reader[19]
                dt.Columns.Add("NET PAY", typeof(double));//reader[20]
                dt.Columns.Add("01", typeof(string));
                dt.Columns.Add("02", typeof(string));
                dt.Columns.Add("03", typeof(double));
                dt.Columns.Add("04Y", typeof(double));
                //  MessageBox.Show(s);



                conn.Open();
                reader = new SqlCommand("select b.name,a.* from paysheet as a,emp as b where a.month='" + Year + "/" + MOnth + "' and a.empid=b.empid ORDER BY B.EMPID", conn).ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader[2], reader.GetString(0), reader[4], reader[5], reader[6], reader[8], reader[10], reader[14], reader[11], reader[18], reader[15], reader[16], reader[19], reader[20], "", "", reader[17], 0);
                }

                conn.Close();

                ds.Tables.Add(dt);

                //  ds.WriteXmlSchema("salary.xml");
                // MessageBox.Show("ok");
                salarySummery pp4 = new salarySummery();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("month", "SALARY SUMMRY " + Year + "/" + MOnth.ToUpper());
                //  MessageBox.Show(vehcileNO);
                //pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                //pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                //pp4.SetParameterValue("meterNow", meterNow);

                //pp4.SetParameterValue("meterNext", MeterNext);
                //pp4.SetParameterValue("companyName", comName.ToUpper());
                //pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                //pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                //pp4.SetParameterValue("time", timeH);
                //pp4.SetParameterValue("date", dateH);
                //pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                //pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                //pp4.SetParameterValue("invoiceNo", invoiceNo);

                //pp4.SetParameterValue("term", payType.ToUpper());

                //   MessageBox.Show(comName);
                //  pp4.PrintToPrinter(1, false, 0, 0);


                new test2(pp4).Visible = true;
                //  crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintSalaryAudit(string MOnth, string Year, SqlConnection conn, SqlDataReader reader)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("EMP ID", typeof(int));//reader[1]
                dt.Columns.Add("NAME", typeof(string));//reader.GetString(0)
                dt.Columns.Add("EPF NO", typeof(string));//reader.GetString(0)
                dt.Columns.Add("BASIC", typeof(double));//reader[4]
                dt.Columns.Add("BUD. ALLOWANCE", typeof(double));//reader[5]
                dt.Columns.Add("LIVING ALLOWANCE", typeof(double));//reader[6]
                dt.Columns.Add("TRAVELLING ALLOWANCE", typeof(double));//reader[8]
                dt.Columns.Add("ATTENDANCE ALLOWANCE", typeof(double));//reader[10]
                dt.Columns.Add("MEAL ALLOWANCE", typeof(double));//reader[14]
                dt.Columns.Add("NO PAY", typeof(double));//reader[11]

                dt.Columns.Add("GROSS SALARY", typeof(double));//reader[18]
                dt.Columns.Add("TOTAL EPF", typeof(double));//reader[15]
                dt.Columns.Add("EPF 12%", typeof(double));//reader[16]
                dt.Columns.Add("ETF 3%", typeof(double));//reader[19]
                dt.Columns.Add("COMPANY COMMITMENT", typeof(double));//reader[20]
                dt.Columns.Add("SALARY ADVANCE", typeof(double));
                dt.Columns.Add("LOAN", typeof(double));
                dt.Columns.Add("EPF 8%", typeof(double));
                dt.Columns.Add("PAYEE", typeof(double));
                dt.Columns.Add("TOTAL DEDUCTION", typeof(double));
                dt.Columns.Add("NET PAY", typeof(double));
                //  MessageBox.Show(s);



                conn.Open();
                reader = new SqlCommand("select b.name,a.* from paysheetaUDIT as a,emp as b where a.month='" + Year + "/" + MOnth + "' and a.empid=b.empid and b.isepf='" + true + "' and b.isExecutive='" + true + "' ORDER BY B.EMPID", conn).ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader[1], reader.GetString(0), reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9], reader[10], reader[11], reader[12], reader[13], reader[14], reader[15], reader[16], reader[17], reader[18], reader[19], reader[20], reader[21]);
                }

                conn.Close();

                ds.Tables.Add(dt);

                ds.WriteXmlSchema("salaryaUDIT_.xml");
                // MessageBox.Show("ok");
                salarySummeryAudit pp4 = new salarySummeryAudit();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("month", "SALARY SUMMRY " + Year + "/" + MOnth.ToUpper());
                pp4.SetParameterValue("line", "EXECUTIVE PAYROLL");



                new test2_(pp4).Visible = true;
                //  crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                states = s.StackTrace;
                // throw;
            }
            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("EMP ID", typeof(int));//reader[1]
                dt.Columns.Add("NAME", typeof(string));//reader.GetString(0)
                dt.Columns.Add("EPF NO", typeof(string));//reader.GetString(0)
                dt.Columns.Add("BASIC", typeof(double));//reader[4]
                dt.Columns.Add("BUD. ALLOWANCE", typeof(double));//reader[5]
                dt.Columns.Add("LIVING ALLOWANCE", typeof(double));//reader[6]
                dt.Columns.Add("TRAVELLING ALLOWANCE", typeof(double));//reader[8]
                dt.Columns.Add("ATTENDANCE ALLOWANCE", typeof(double));//reader[10]
                dt.Columns.Add("MEAL ALLOWANCE", typeof(double));//reader[14]
                dt.Columns.Add("NO PAY", typeof(double));//reader[11]

                dt.Columns.Add("GROSS SALARY", typeof(double));//reader[18]
                dt.Columns.Add("TOTAL EPF", typeof(double));//reader[15]
                dt.Columns.Add("EPF 12%", typeof(double));//reader[16]
                dt.Columns.Add("ETF 3%", typeof(double));//reader[19]
                dt.Columns.Add("COMPANY COMMITMENT", typeof(double));//reader[20]
                dt.Columns.Add("SALARY ADVANCE", typeof(double));
                dt.Columns.Add("LOAN", typeof(double));
                dt.Columns.Add("EPF 8%", typeof(double));
                dt.Columns.Add("PAYEE", typeof(double));
                dt.Columns.Add("TOTAL DEDUCTION", typeof(double));
                dt.Columns.Add("NET PAY", typeof(double));
                //  MessageBox.Show(s);



                conn.Open();
                reader = new SqlCommand("select b.name,a.* from paysheetaUDIT as a,emp as b where a.month='" + Year + "/" + MOnth + "' and a.empid=b.empid and b.isepf='" + true + "' and b.isExecutive='" + false + "' ORDER BY B.EMPID", conn).ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader[1], reader.GetString(0), reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], reader[9], reader[10], reader[11], reader[12], reader[13], reader[14], reader[15], reader[16], reader[17], reader[18], reader[19], reader[20], reader[21]);
                }

                conn.Close();

                ds.Tables.Add(dt);

                ds.WriteXmlSchema("salaryaUDIT_.xml");
                // MessageBox.Show("ok");
                salarySummeryAudit pp4 = new salarySummeryAudit();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("month", "SALARY SUMMRY " + Year + "/" + MOnth.ToUpper());
                pp4.SetParameterValue("line", "NON-EXECUTIVE PAYROLL");



                new test2_(pp4).Visible = true;
                //  crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                states = s.StackTrace;
                // throw;
            }

        }
        public void setprintSalarySlip(string MOnth, string Year, SqlConnection conn, SqlDataReader reader)
        {

            try
            {
                // MessageBox.Show(customer);

                db2 = new DB();

                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("EMP ID", typeof(string));//reader[1]
                dt.Columns.Add("NAME", typeof(string));//reader.GetString(0)
                dt.Columns.Add("WORKING DAYS", typeof(int));//reader[4]
                dt.Columns.Add("ABSENT DAYS", typeof(int));//reader[5]
                dt.Columns.Add("BASIC", typeof(double));//reader[6]
                dt.Columns.Add("ALLOWANCE", typeof(double));//reader[8]
                dt.Columns.Add("INCENTIVE", typeof(double));//reader[10]
                dt.Columns.Add("TOTAL EARNING", typeof(double));//reader[14]
                dt.Columns.Add("GROSS SALARY", typeof(double));//reader[11]
                dt.Columns.Add("ADAVNCE", typeof(double));//reader[18]
                dt.Columns.Add("EPF 8%", typeof(double));//reader[15]
                dt.Columns.Add("NO PAY", typeof(double));//reader[16]
                dt.Columns.Add("TOTAL DEDUCTION", typeof(double));//reader[19]
                dt.Columns.Add("NET PAY", typeof(double));//reader[20]
                dt.Columns.Add("01", typeof(string));
                dt.Columns.Add("02", typeof(string));
                dt.Columns.Add("03", typeof(double));
                dt.Columns.Add("04Y", typeof(double));
                //  MessageBox.Show(s);



                conn.Open();
                reader = new SqlCommand("select b.name,a.* from paysheet as a,emp as b where a.month='" + Year + "/" + MOnth + "' and a.empid=b.empid ORDER BY B.EMPID", conn).ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(reader[2], reader.GetString(0), reader[4], reader[5], reader[6], reader[8], reader[10], reader[14], reader[11], reader[18], reader[15], reader[16], reader[19], reader[20], "", "", 0, 0);
                }

                conn.Close();

                ds.Tables.Add(dt);

                //  ds.WriteXmlSchema("salary.xml");
                // MessageBox.Show("ok");
                salarySummerySlip pp4 = new salarySummerySlip();
                pp4.SetDataSource(ds);
                pp4.SetParameterValue("month", "SALARY SUMMRY " + Year + "/" + MOnth.ToUpper());
                //  MessageBox.Show(vehcileNO);
                //pp4.SetParameterValue("vehicleNumber", vehcileNO.ToUpper());

                //pp4.SetParameterValue("vehicleDesc", vehcileDesc.ToUpper());

                //pp4.SetParameterValue("meterNow", meterNow);

                //pp4.SetParameterValue("meterNext", MeterNext);
                //pp4.SetParameterValue("companyName", comName.ToUpper());
                //pp4.SetParameterValue("companyAddress", comAddres.ToUpper());
                //pp4.SetParameterValue("companyNumber", comcontact.ToUpper());
                //pp4.SetParameterValue("time", timeH);
                //pp4.SetParameterValue("date", dateH);
                //pp4.SetParameterValue("cash", setAmountFormat(paid + ""));
                //pp4.SetParameterValue("balance", setAmountFormat(balance + ""));

                //pp4.SetParameterValue("invoiceNo", invoiceNo);

                //pp4.SetParameterValue("term", payType.ToUpper());

                //   MessageBox.Show(comName);
                //  pp4.PrintToPrinter(1, false, 0, 0);


                new test2_2(pp4).Visible = true;
                //  crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
                states = s.StackTrace;
                // throw;
            }

        }

    }


}
