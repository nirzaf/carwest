using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class saleSummery : Form
    {
        public saleSummery(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private cusOuts pp;
        private DB db, db2, db3;
        private string userH, queary, tableName, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "", term, customer, item;
        private SqlConnection conn, conn2, conn3;
        private string[] idArray;
        private SqlDataReader reader, reader2, reader3;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private double amountCost, amountPaid, balance, temp030, temp3060, temp6090, temp90up, a;
        private Boolean isCompany, creditDetailB, chequeDetailB, cardDetailB;
        private DataGridViewButtonColumn btn;

        //
        //++++++ My Method Start+++
        private void getCustomer(string id)
        {
            try
            {
                customer = "";
                conn2.Open();
                reader2 = new SqlCommand("select id,name,company from customer where id='" + id + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    customer = reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper();
                }
                else
                {
                    customer = id;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                conn2.Close();
            }
        }

        private void getItem(string id)
        {
            try
            {
                item = "";
                conn2.Open();
                reader2 = new SqlCommand("select detail from item where code='" + id + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    item = reader2[0].ToString().ToUpper();
                }
                else
                {
                    item = id;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                conn2.Close();
            }
        }

        private void getInvoicePayType(string id)
        {
            try
            {
                conn2.Open();

                reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    creditDetailB = true;
                }
                conn2.Close();
                conn2.Open();
                reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    chequeDetailB = true;
                }
                conn2.Close();
                conn2.Open();
                reader2 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    cardDetailB = true;
                }
                conn2.Close();

                term = "";
                if (!creditDetailB & !chequeDetailB & !cardDetailB)
                {
                    term = "CASH";
                }
                else
                {
                    if (creditDetailB)
                    {
                        term = "CREDIT";
                    }
                    if (chequeDetailB)
                    {
                        term = term + "/ CHEQUE";
                    }
                    if (cardDetailB)
                    {
                        term = term + "/ CARD";
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void setTempDates(Double aH)
        {
            a = aH;
            temp030 = 0;
            temp3060 = 0;
            temp6090 = 0;
            temp90up = 0;
            if (aH < 30)
            {
                temp030 = balance;
            }
            else if (aH < 60 & a > 30)
            {
                temp3060 = balance;
            }
            else if (aH < 60 & a > 90)
            {
                temp6090 = balance;
            }
            else
            {
                temp90up = balance;
            }
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    userName = reader.GetString(0).ToUpper();
                    isCompany = reader.GetBoolean(2);
                }
                reader.Close();
                conn.Close();
                if (isCompany)
                {
                    tableName = "invoiceRetail";
                }
                else
                {
                    tableName = "invoiceDump";
                }
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
                    if (!reader.GetString(4).Equals(""))
                    {
                        comContact2 = "E-Mail : " + reader[4].ToString().ToUpper() + " / ";
                    }
                    if (!reader.GetString(5).Equals(""))
                    {
                        comContact2 = comContact2 + "Web : " + reader[5].ToString().ToUpper();
                    }
                    comReg = reader[6] + "";
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //
        private void stockReport_Load(object sender, EventArgs e)
        {
            tableItem.AllowUserToAddRows = false;
            tableCustomer.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn3 = db3.createSqlConnection();
            radioCustomItem.Checked = true;
            radioAllItem.Checked = true;
            radioCustomCustomer.Checked = true;
            radioAllCustomer.Checked = true;
            radioDateCustom.Checked = true;
            radioAllDate.Checked = true;

            this.TopMost = true;

            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;
            comboGroupBy.SelectedIndex = 0;

            btn = new DataGridViewButtonColumn();
            tableItem.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            tableCustomer.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;

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
                comReg = "";
                comContact2 = "";
            }
            reader.Close();
            conn.Close();
            conn.Open();
            reader = new SqlCommand("select DISTINCT(BRAND) from ITEM order by BRAND", conn).ExecuteReader();
            while (reader.Read())
            {
                CATEGORY.Items.Add(reader[0] + "");
            }
            reader.Close();
            conn.Close();
            conn.Open();
            reader = new SqlCommand("select DISTINCT(categorey) from ITEM order by categorey ", conn).ExecuteReader();
            while (reader.Read())
            {
                CATEGORY2.Items.Add(reader[0] + "");
            }
            reader.Close();
            conn.Close();
            CATEGORY.SelectedIndex = 0;
            CATEGORY2.SelectedIndex = 0;
        }

        private void radioSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            tableItem.Enabled = radioCustomItem.Checked;
            itemCode.Enabled = radioCustomItem.Checked;
            itemCode.Focus();
        }

        private void checkBrand_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkCategory_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkDescription_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkQty_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMinValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMaxValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void stockReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void checkQty_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amountCost = 0;
                db.setCursoerWait();

                queary = "";
                if (radioDateCustom.Checked)
                {
                    queary = " and b.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                }
                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by b.Date";
                }
                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.invoiceID";
                }

                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }

                if (radioAllItem.Checked && radioAllCustomer.Checked)
                {
                    if (comboGroupBy.SelectedIndex == 0)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("cusID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("date", typeof(string));
                        conn.Open();
                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = "where  date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "  " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            getInvoicePayType(reader[0] + "");
                            getCustomer(reader[4] + "");
                            if (isCompany)
                            {
                                dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDateTime(3).ToShortDateString());
                            }
                            else
                            {
                                dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDateTime(3).ToShortDateString());
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale1.xml");
                        //   MessageBox.Show("a");
                        saleSummery1 pp = new saleSummery1();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);

                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 1)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));
                        conn.Open();
                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            getInvoicePayType(reader[0] + "");
                            getCustomer(reader[4] + "");
                            getItem(reader[5] + "");

                            if (isCompany)
                            {
                                dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                            }
                            else
                            {
                                dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale2.xml");
                        //   MessageBox.Show("a");
                        saleSummery2 pp = new saleSummery2();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 2)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));
                        conn.Open();
                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            getInvoicePayType(reader[0] + "");
                            getCustomer(reader[4] + "");
                            getItem(reader[5] + "");

                            if (isCompany)
                            {
                                dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                            }
                            else
                            {
                                dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery3 pp = new saleSummery3();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 3)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));
                        conn.Open();
                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            getInvoicePayType(reader[0] + "");
                            getCustomer(reader[4] + "");
                            getItem(reader[5] + "");

                            if (isCompany)
                            {
                                dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                            }
                            else
                            {
                                dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery4 pp = new saleSummery4();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                }
                else if (radioCustomItem.Checked && radioCustomCustomer.Checked)
                {
                    if (comboGroupBy.SelectedIndex == 0)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("cusID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = "and date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            if (SALE.Checked)
                            {
                                reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "  where customerid='" + tableCustomer.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                            }
                            else
                            {
                                tableName = "GRN";
                                reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "  where customerid='" + tableCustomer.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                            }
                            //  reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "  where customerid='" + tableCustomer.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                for (int y = 0; y < tableItem.Rows.Count; y++)
                                {
                                    conn3.Open();
                                    if (SALE.Checked)
                                    {
                                        reader3 = new SqlCommand("select a.id from " + tableName + "  as a,grndetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                    }
                                    else
                                    {
                                        reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                    }
                                    while (reader3.Read())
                                    {
                                        getInvoicePayType(reader[0] + "");
                                        getCustomer(reader[4] + "");
                                        if (isCompany)
                                        {
                                            dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDateTime(3).ToShortDateString());
                                        }
                                        else
                                        {
                                            dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDateTime(3).ToShortDateString());
                                        }
                                    }

                                    conn3.Close();
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale1.xml");
                        //   MessageBox.Show("a");
                        if (SALE.Checked)
                        {
                            saleSummery1_ pp = new saleSummery1_();
                            pp.SetDataSource(ds);
                            pp.SetParameterValue("USER", userName);
                            if (radioAllDate.Checked)
                            {
                                pp.SetParameterValue("period", "ALL");
                            }
                            else
                            {
                                pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                            }
                            pp.SetParameterValue("comName", comName);
                            pp.SetParameterValue("comAddress", comAddres);
                            pp.SetParameterValue("comContact", comcontact);

                            pp.SetParameterValue("comCntact2", comContact2);

                            pp.SetParameterValue("comReg", comReg);
                            crystalReportViewer1.ReportSource = pp;
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                            }
                            else
                            {
                                //   MessageBox.Show("Succefully Downloaded");
                            }
                        }
                        else
                        {
                            saleSummery1 pp = new saleSummery1();
                            pp.SetDataSource(ds);
                            pp.SetParameterValue("USER", userName);
                            if (radioAllDate.Checked)
                            {
                                pp.SetParameterValue("period", "ALL");
                            }
                            else
                            {
                                pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                            }
                            pp.SetParameterValue("comName", comName);
                            pp.SetParameterValue("comAddress", comAddres);
                            pp.SetParameterValue("comContact", comcontact);

                            pp.SetParameterValue("comCntact2", comContact2);

                            pp.SetParameterValue("comReg", comReg);
                            crystalReportViewer1.ReportSource = pp;
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                            }
                            else
                            {
                                //   MessageBox.Show("Succefully Downloaded");
                            }
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 1)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                for (int y = 0; y < tableItem.Rows.Count; y++)
                                {
                                    conn3.Open();
                                    reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                    while (reader3.Read())
                                    {
                                        getInvoicePayType(reader[0] + "");
                                        getCustomer(reader[4] + "");
                                        getItem(reader[5] + "");

                                        if (isCompany)
                                        {
                                            dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                        }
                                        else
                                        {
                                            dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                        }
                                    }

                                    conn3.Close();
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale2.xml");
                        //   MessageBox.Show("a");
                        saleSummery2 pp = new saleSummery2();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 2)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                for (int y = 0; y < tableItem.Rows.Count; y++)
                                {
                                    conn3.Open();
                                    reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                    while (reader3.Read())
                                    {
                                        getInvoicePayType(reader[0] + "");
                                        getCustomer(reader[4] + "");
                                        getItem(reader[5] + "");

                                        if (isCompany)
                                        {
                                            dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                        }
                                        else
                                        {
                                            dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                        }
                                    }
                                    conn3.Close();
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery3 pp = new saleSummery3();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 3)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                for (int y = 0; y < tableItem.Rows.Count; y++)
                                {
                                    conn3.Open();
                                    reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                    while (reader3.Read())
                                    {
                                        getInvoicePayType(reader[0] + "");
                                        getCustomer(reader[4] + "");
                                        getItem(reader[5] + "");

                                        if (isCompany)
                                        {
                                            dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                        }
                                        else
                                        {
                                            dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                        }
                                    }
                                    conn3.Close();
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery4 pp = new saleSummery4();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                }
                else if (radioAllItem.Checked && radioCustomCustomer.Checked)
                {
                    if (comboGroupBy.SelectedIndex == 0)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("cusID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "  where customerid='" + tableCustomer.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                getInvoicePayType(reader[0] + "");
                                getCustomer(reader[4] + "");
                                if (isCompany)
                                {
                                    dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDateTime(3).ToShortDateString());
                                }
                                else
                                {
                                    dt.Rows.Add(reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDateTime(3).ToShortDateString());
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale1.xml");
                        //   MessageBox.Show("a");
                        saleSummery1 pp = new saleSummery1();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 1)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                getInvoicePayType(reader[0] + "");
                                getCustomer(reader[4] + "");
                                getItem(reader[5] + "");

                                if (isCompany)
                                {
                                    dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                }
                                else
                                {
                                    dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale2.xml");
                        //   MessageBox.Show("a");
                        saleSummery2 pp = new saleSummery2();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 2)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                getInvoicePayType(reader[0] + "");
                                getCustomer(reader[4] + "");
                                getItem(reader[5] + "");

                                if (isCompany)
                                {
                                    dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                }
                                else
                                {
                                    dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery3 pp = new saleSummery3();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 3)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        for (int i = 0; i < tableCustomer.Rows.Count; i++)
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid and a.customerid='" + tableCustomer.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                            while (reader.Read())
                            {
                                getInvoicePayType(reader[0] + "");
                                getCustomer(reader[4] + "");
                                getItem(reader[5] + "");

                                if (isCompany)
                                {
                                    dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                }
                                else
                                {
                                    dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                }
                            }
                            conn.Close();
                        }

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery4 pp = new saleSummery4();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                }
                else if (radioCustomItem.Checked && radioAllCustomer.Checked)
                {
                    if (comboGroupBy.SelectedIndex == 0)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("cusID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = "where  date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        //conn.Open();
                        //reader = new SqlCommand("select id,subTotal,profit,date,customerid from " + tableName + "   " + queary, conn).ExecuteReader();
                        //while (reader.Read())
                        //{
                        for (int y = 0; y < tableItem.Rows.Count; y++)
                        {
                            conn3.Open();
                            if (!SALE.Checked)
                            {
                                tableName = "GRN";
                                reader3 = new SqlCommand("select a.id,b.totalPrice,c.detail,b.qty from " + tableName + "  as a,GRNDetail as b,item as c where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and b.itemcode=c.code", conn3).ExecuteReader();
                            }
                            else
                            {
                                reader3 = new SqlCommand("select a.id,b.totalPrice,c.detail,b.qty from " + tableName + "  as a,invoiceRetailDetail as b,item as c where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and b.itemcode=c.code", conn3).ExecuteReader();
                            }
                            while (reader3.Read())
                            {
                                //getInvoicePayType(reader[0] + "");
                                //  getCustomer(reader[4] + "");
                                //if (isCompany)
                                //{
                                dt.Rows.Add("", reader3[2], "", "", reader3.GetDouble(1), reader3.GetDouble(3), DateTime.Now);

                                //}
                                //else
                                //{
                                //    dt.Rows.Add(reader[4], reader3[2], "R-" + reader[0], term.ToUpper(), reader3.GetDouble(1), reader3.GetDouble(3), reader.GetDateTime(3).ToShortDateString());

                                //}
                            }

                            conn3.Close();
                        }
                        //}
                        //conn.Close();

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale1.xml");
                        //   MessageBox.Show("a");
                        if (SALE.Checked)
                        {
                            saleSummery1 pp = new saleSummery1();
                            pp.SetDataSource(ds);
                            pp.SetParameterValue("USER", userName);
                            if (radioAllDate.Checked)
                            {
                                pp.SetParameterValue("period", "ALL");
                            }
                            else
                            {
                                pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                            }
                            pp.SetParameterValue("comName", comName);
                            pp.SetParameterValue("comAddress", comAddres);
                            pp.SetParameterValue("comContact", comcontact);

                            pp.SetParameterValue("comCntact2", comContact2);

                            pp.SetParameterValue("comReg", comReg);
                            crystalReportViewer1.ReportSource = pp;
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                            }
                            else
                            {
                                //   MessageBox.Show("Succefully Downloaded");
                            }
                        }
                        else
                        {
                            saleSummery1_ pp = new saleSummery1_();
                            pp.SetDataSource(ds);
                            pp.SetParameterValue("USER", userName);
                            if (radioAllDate.Checked)
                            {
                                pp.SetParameterValue("period", "ALL");
                            }
                            else
                            {
                                pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                            }
                            pp.SetParameterValue("comName", comName);
                            pp.SetParameterValue("comAddress", comAddres);
                            pp.SetParameterValue("comContact", comcontact);

                            pp.SetParameterValue("comCntact2", comContact2);

                            pp.SetParameterValue("comReg", comReg);
                            crystalReportViewer1.ReportSource = pp;
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                            }
                            else
                            {
                                //   MessageBox.Show("Succefully Downloaded");
                            }
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 1)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customer", typeof(string));
                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }

                        conn.Open();
                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid" + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            for (int y = 0; y < tableItem.Rows.Count; y++)
                            {
                                conn3.Open();
                                reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                while (reader3.Read())
                                {
                                    getInvoicePayType(reader[0] + "");
                                    getCustomer(reader[4] + "");
                                    getItem(reader[5] + "");

                                    if (isCompany)
                                    {
                                        dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader[5], item, customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                    }
                                }

                                conn3.Close();
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        // ds.WriteXmlSchema("sale2.xml");
                        //   MessageBox.Show("a");
                        saleSummery2 pp = new saleSummery2();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 2)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        conn.Open();
                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            for (int y = 0; y < tableItem.Rows.Count; y++)
                            {
                                conn3.Open();
                                reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                while (reader3.Read())
                                {
                                    getInvoicePayType(reader[0] + "");
                                    getCustomer(reader[4] + "");
                                    getItem(reader[5] + "");

                                    if (isCompany)
                                    {
                                        dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                    }
                                }
                                conn3.Close();
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery3 pp = new saleSummery3();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                    else if (comboGroupBy.SelectedIndex == 3)
                    {
                        dt = new DataTable();
                        ds = new DataSet();

                        dt.Columns.Add("itemID", typeof(string));
                        dt.Columns.Add("item", typeof(string));
                        dt.Columns.Add("customerID", typeof(string));
                        dt.Columns.Add("customer", typeof(string));

                        dt.Columns.Add("invoiceID", typeof(string));
                        dt.Columns.Add("term", typeof(string));
                        dt.Columns.Add("invoiceAmount", typeof(float));
                        dt.Columns.Add("profit", typeof(float));
                        dt.Columns.Add("itemQty", typeof(float));
                        dt.Columns.Add("itemAmount", typeof(float));
                        dt.Columns.Add("itemProfit", typeof(float));
                        dt.Columns.Add("date", typeof(string));

                        queary = "";
                        if (radioDateCustom.Checked)
                        {
                            queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                        }
                        if (comboOrderBY.SelectedIndex == 0)
                        {
                            queary = queary + " order by a.Date";
                        }
                        else if (comboOrderBY.SelectedIndex == 1)
                        {
                            queary = queary + " order by a.id";
                        }

                        if (comboOrderTO.SelectedIndex == 0)
                        {
                            queary = queary + " asc ";
                        }
                        else
                        {
                            queary = queary + " desc ";
                        }
                        if (!queary.Equals(""))
                        {
                            queary = " " + queary;
                        }
                        conn.Open();
                        reader = new SqlCommand("select a.id,a.subTotal,a.profit,a.date,a.customerid,b.itemcode,b.qty,b.totalPrice,b.profit from " + tableName + "  as a ,invoiceRetailDetail as b where a.id=b.invoiceid " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            for (int y = 0; y < tableItem.Rows.Count; y++)
                            {
                                conn3.Open();
                                reader3 = new SqlCommand("select a.id from " + tableName + "  as a,invoiceRetailDetail as b where b.itemcode='" + tableItem.Rows[y].Cells[0].Value + "' and a.id=b.invoiceID and a.id='" + reader[0] + "'", conn3).ExecuteReader();
                                while (reader3.Read())
                                {
                                    getInvoicePayType(reader[0] + "");
                                    getCustomer(reader[4] + "");
                                    getItem(reader[5] + "");

                                    if (isCompany)
                                    {
                                        dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), reader.GetDouble(2), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDateTime(3).ToShortDateString());
                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader[5], item, reader[4], customer, "R-" + reader[0], term.ToUpper(), reader.GetDouble(1), 0.0, reader.GetDouble(6), reader.GetDouble(7), 0.0, reader.GetDateTime(3).ToShortDateString());
                                    }
                                }
                                conn3.Close();
                            }
                        }
                        conn.Close();

                        ds.Tables.Add(dt);
                        //
                        //   ds.WriteXmlSchema("sale3.xml");
                        //   MessageBox.Show("a");
                        saleSummery4 pp = new saleSummery4();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("USER", userName);
                        if (radioAllDate.Checked)
                        {
                            pp.SetParameterValue("period", "ALL");
                        }
                        else
                        {
                            pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                        }
                        pp.SetParameterValue("comName", comName);
                        pp.SetParameterValue("comAddress", comAddres);
                        pp.SetParameterValue("comContact", comcontact);

                        pp.SetParameterValue("comCntact2", comContact2);

                        pp.SetParameterValue("comReg", comReg);
                        crystalReportViewer1.ReportSource = pp;
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                        }
                        else
                        {
                            //   MessageBox.Show("Succefully Downloaded");
                        }
                    }
                }

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void searchALL_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sETTINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
        }

        private void itemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (itemCode.Text.Equals(""))
                {
                    itemCode.Focus();
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select brand,categorey,description from item where code='" + itemCode.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        name = reader[0] + " " + reader[1] + " " + reader[2];
                    }
                    conn.Close();
                    if (tableItem.Rows.Count == 0)
                    {
                        tableItem.Rows.Add(itemCode.Text.ToUpper(), name);
                    }
                    else
                    {
                        states = false;
                        for (int i = 0; i < tableItem.Rows.Count; i++)
                        {
                            if (tableItem.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(itemCode.Text.ToUpper()))
                            {
                                states = true;
                            }
                        }
                        if (!states)
                        {
                            tableItem.Rows.Add(itemCode.Text.ToUpper(), name);
                        }
                    }
                    itemCode.Text = "";
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
                        itemCode.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void brandName_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void name_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data to Print");
                }
                else
                {
                    pp.PrintToPrinter(1, false, 0, 0);
                    MessageBox.Show("Send to Print Succesfully");
                }
            }
            catch (Exception)
            {
            }
        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void radioDateCustom_CheckedChanged(object sender, EventArgs e)
        {
            from.Enabled = radioDateCustom.Checked;
            to.Enabled = radioDateCustom.Checked;
        }

        private void itemCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMin_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMax_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCustomerID_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioName_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCompany_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void customerID_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | itemCode.Text.Equals("")))
            {
                db.setList(listBox1, itemCode, itemCode.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select detail from item where detail like '%" + itemCode.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper());
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
            if (itemCode.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void Name_KeyUp_1(object sender, KeyEventArgs e)
        {
        }

        private void company_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private string name;
        private Boolean states;
        private Point p;

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                itemCode.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                name = "";
                for (int i = 0; i < listBox1.SelectedItem.ToString().Split(' ').Length; i++)
                {
                    if (i != 0)
                    {
                        name = name + " " + listBox1.SelectedItem.ToString().Split(' ')[i];
                    }
                }

                itemCode.Focus();
                itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                if (tableItem.Rows.Count == 0)
                {
                    tableItem.Rows.Add(itemCode.Text, name);
                }
                else
                {
                    states = false;
                    for (int i = 0; i < tableItem.Rows.Count; i++)
                    {
                        if (tableItem.Rows[i].Cells[0].Value.ToString().Equals(itemCode.Text))
                        {
                            states = true;
                        }
                    }
                    if (!states)
                    {
                        tableItem.Rows.Add(itemCode.Text, name);
                    }
                }
                itemCode.Text = "";
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                tableItem.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            conn.Open();
            reader = new SqlCommand("select brand,categorey,description from item where code='" + itemCode.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                name = reader[0] + " " + reader[1] + " " + reader[2];
            }
            conn.Close();
            if (tableItem.Rows.Count == 0)
            {
                tableItem.Rows.Add(itemCode.Text.ToUpper(), name);
            }
            else
            {
                states = false;
                for (int i = 0; i < tableItem.Rows.Count; i++)
                {
                    if (tableItem.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(itemCode.Text.ToUpper()))
                    {
                        states = true;
                    }
                }
                if (!states)
                {
                    tableItem.Rows.Add(itemCode.Text.ToUpper(), name);
                }
            }
            itemCode.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void radioCustomCustomer_CheckedChanged(object sender, EventArgs e)
        {
            tableCustomer.Enabled = radioCustomCustomer.Checked;
            customerID.Enabled = radioCustomCustomer.Checked;
            customerID.Focus();
        }

        private void customerID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 12 | e.KeyValue == 13)
                {
                    listCustomer.Visible = false;
                    if (customerID.Text.Equals(""))
                    {
                        customerID.Focus();
                    }
                    else
                    {
                        conn.Open();
                        reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            name = reader[0] + " " + reader[1];
                        }
                        conn.Close();
                        if (tableCustomer.Rows.Count == 0)
                        {
                            tableCustomer.Rows.Add(customerID.Text.ToUpper(), name);
                        }
                        else
                        {
                            states = false;
                            for (int i = 0; i < tableCustomer.Rows.Count; i++)
                            {
                                if (tableCustomer.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                                {
                                    states = true;
                                }
                            }
                            if (!states)
                            {
                                tableCustomer.Rows.Add(customerID.Text.ToUpper(), name);
                            }
                        }
                        customerID.Text = "";
                    }
                }
                else if (e.KeyValue == 40)
                {
                    try
                    {
                        if (listCustomer.Visible)
                        {
                            listCustomer.Focus();
                            listCustomer.SelectedIndex = 0;
                        }
                        else
                        {
                            customerID.Focus();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/ " + a.StackTrace);
            }
        }

        private void customerID_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customerID.Text.Equals("")))
            {
                db.setList(listCustomer, customerID, customerID.Width);

                try
                {
                    listCustomer.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select description,id from customer where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listCustomer.Items.Add(reader[1] + " " + reader[0].ToString().ToUpper());
                        listCustomer.Visible = true;
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
            if (customerID.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listCustomer.Visible = false;
            }
        }

        private void tableCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                tableCustomer.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void listCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (listCustomer.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customerID.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listCustomer.Visible = false;
                name = "";
                for (int i = 0; i < listCustomer.SelectedItem.ToString().Split(' ').Length; i++)
                {
                    if (i != 0)
                    {
                        name = name + " " + listCustomer.SelectedItem.ToString().Split(' ')[i];
                    }
                }

                customerID.Focus();
                customerID.Text = listCustomer.SelectedItem.ToString().Split(' ')[0];
                if (tableCustomer.Rows.Count == 0)
                {
                    tableCustomer.Rows.Add(customerID.Text, name);
                }
                else
                {
                    states = false;
                    for (int i = 0; i < tableCustomer.Rows.Count; i++)
                    {
                        if (tableCustomer.Rows[i].Cells[0].Value.ToString().Equals(customerID.Text))
                        {
                            states = true;
                        }
                    }
                    if (!states)
                    {
                        tableCustomer.Rows.Add(customerID.Text, name);
                    }
                }
                customerID.Text = "";
            }
        }

        private void listCustomer_MouseClick(object sender, MouseEventArgs e)
        {
            listCustomer.Visible = false;

            customerID.Text = listCustomer.SelectedItem.ToString().Split(' ')[0];
            conn.Open();
            reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                name = reader[0] + " " + reader[1];
            }
            conn.Close();
            if (tableCustomer.Rows.Count == 0)
            {
                tableCustomer.Rows.Add(customerID.Text.ToUpper(), name);
            }
            else
            {
                states = false;
                for (int i = 0; i < tableCustomer.Rows.Count; i++)
                {
                    if (tableCustomer.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                    {
                        states = true;
                    }
                }
                if (!states)
                {
                    tableCustomer.Rows.Add(customerID.Text.ToUpper(), name);
                }
            }
            customerID.Text = "";
        }

        private void listCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listCustomer.SelectedItem.ToString().Split(' ')[0];
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                tableItem.Rows.Clear();
                conn.Open();
                if (CATEGORY.SelectedIndex != 0 && CATEGORY2.SelectedIndex != 0)
                {
                    reader = new SqlCommand("SELECT * FROM ITEM WHERE BRAND='" + CATEGORY.SelectedItem + "' AND categorey='" + CATEGORY2.SelectedItem + "'", conn).ExecuteReader();
                }
                else if (CATEGORY.SelectedIndex != 0)
                {
                    reader = new SqlCommand("SELECT * FROM ITEM WHERE BRAND='" + CATEGORY.SelectedItem + "'", conn).ExecuteReader();
                }
                else if (CATEGORY2.SelectedIndex != 0)
                {
                    reader = new SqlCommand("SELECT * FROM ITEM WHERE categorey='" + CATEGORY2.SelectedItem + "'", conn).ExecuteReader();
                }
                while (reader.Read())
                {
                    //conn2.Open();
                    //reader2 = new SqlCommand("select brand,categorey,description from item where code='" + reader[0] + "'", conn2).ExecuteReader();
                    //if (reader2.Read())
                    //{
                    //    name = reader2[0] + " " + reader2[1] + " " + reader2[2];
                    tableItem.Rows.Add(reader[0], "");
                    //}
                    //conn2.Close();
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }
    }
}