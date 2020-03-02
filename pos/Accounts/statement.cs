using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace pos
{
    public partial class statement : Form
    {
        public statement()
        {
            InitializeComponent();

        }

       // accountList homeH;
        // My Variable Start

        DB db, db2, db3, db4;
        Form home;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        Double openingBalance, amount, cre, debi;
        string date, credit, debit, idB, userH;
        public bool states, loadBankBool = false, loadFixedAsset = false, loadEQUITY = false, loadLibilityBool = false;
        ArrayList arrayLst;
        string[] array;
        Int32 yearB, monthB;
        DateTime dateSearchB;
        public void loadRevenue(string id, DateTime dateFrom, DateTime dateTo, string acName)
        {
            ArrayList monthList = new ArrayList();

            for (DateTime dts = dateTo; dts <= dateFrom; dts = dts.AddMonths(1))
            {
                monthList.Add(new ListItem(dts.ToString("yyyy/MMMM"), dts.ToString("yyyy")));
            }
            if (dateTo.Month == dateFrom.Month && monthList.Count == 0)
            {
                monthList.Add(new ListItem(dateTo.ToString("yyyy/MMMM"), dateTo.ToString("yyyy")));
            }

            double electricityRate = 0, waterRate = 0;
            conn.Open();
            reader = new SqlCommand("select * from plRates ", conn).ExecuteReader();
            if (reader.Read())
            {
                electricityRate = reader.GetDouble(0);
                waterRate = reader.GetDouble(1);
            }
            conn.Close();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("DATE", typeof(string));
            dt.Columns.Add("REF-", typeof(string));


            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("CREDIT", typeof(double));
            dt.Columns.Add("DEBIT", typeof(double));
            if (id.Equals("01") || id.Equals(" 01.01") || id.Equals(" 01.02"))
            {
                conn.Open();
                reader = new SqlCommand("select id,date,PAYTYPE from invoiceRetail where date between '" + dateFrom.Date + "' and '" + dateTo.Date + "' order by date", conn).ExecuteReader();
                while (reader.Read())
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select totalPrice,itemcode from invoiceRetailDetail where invoiceid='" + reader[0] + "' ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        if (id.Equals("01"))
                        {

                            if (!reader.GetString(2).Equals("CASH -"))
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", reader2.GetDouble(0), 0);

                            }
                            else
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", 0, reader2.GetDouble(0));

                            }




                        }
                        else if (id.Equals(" 01.01"))
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select sparePart from item where code='" + reader2[1] + "' ", conn3).ExecuteReader();
                            if (reader3.Read())
                            {
                                if (!reader3.GetBoolean(0))
                                {
                                    if (!reader.GetString(2).Equals("CASH -"))
                                    {
                                        dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", reader2.GetDouble(0), 0);

                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", 0, reader2.GetDouble(0));

                                    }

                                }

                            }
                            else
                            {
                                if (!reader.GetString(2).Equals("CASH -"))
                                {
                                    dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", reader2.GetDouble(0), 0);

                                }
                                else
                                {
                                    dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", 0, reader2.GetDouble(0));

                                }

                            }
                            conn3.Close();
                        }
                        else
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select sparePart from item where code='" + reader2[1] + "' ", conn3).ExecuteReader();
                            if (reader3.Read())
                            {
                                if (reader3.GetBoolean(0))
                                {
                                    if (!reader.GetString(2).Equals("CASH -"))
                                    {
                                        dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", reader2.GetDouble(0), 0);

                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), "R-" + reader[0], reader[2] + " INVOICE", 0, reader2.GetDouble(0));

                                    }

                                }

                            }
                            conn3.Close();

                        }



                    }
                    conn2.Close();

                }
                conn.Close();
            }
            else
            {
                if (id.Equals(" 02.01"))
                {

                    double epf12 = 0, etf3 = 0;
                    for (int i = 0; i < monthList.Count; i++)
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.grossSalary,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' and a.empid=b.empid and b.isExecutive='"+false+"'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add("", reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                            conn.Close();
                        }
                    }


                }
                else if (id.Equals(" 02.02"))
                {
                    double epf12 = 0, etf3 = 0;
                    for (int i = 0; i < monthList.Count; i++)
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.epf12,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' and a.empid=b.empid", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add("", reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                            conn.Close();
                        }
                    }
                }
                else if (id.Equals(" 02.03"))
                {
                    double epf12 = 0, etf3 = 0;
                    for (int i = 0; i < monthList.Count; i++)
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select a.etf3,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' and a.empid=b.empid", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add("", reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                            conn.Close();
                        }
                    }
                }
                else if (id.Equals(" 02.04"))
                {
                    double credit = 0, debit = 0, purchasing = 0, bfStock = 0, lastStock = 0; ;



                    credit = 0;
                    debit = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select purchasingPrice,QTY,code,detail from ITEM where isItem='" + true + "' ", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                        credit = 0;
                        debit = 0; conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date between '" + dateFrom + "' and '" + dateTo + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();

                        if (debit != 0)
                        {
                            dt.Rows.Add("", "", reader4[3], 0, (debit));

                        }
                    }



                }
                else if (id.Equals(" 02.05") || id.Equals(" 03.01"))
                {
                    double expensesTotal = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + acName.Split('.')[0] + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        {
                            conn.Open();
                            reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3], 0, (reader[0]));

                            }
                            conn.Close();

                        }

                    }
                    conn4.Close();
                }
                else if (id.Equals(" 04.01"))
                {
                    double general = 0;
                    for (int i = 0; i < monthList.Count; i++)
                    {
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select empid,name from emp where isExecutive='" + true + "'", conn2).ExecuteReader();
                            while (reader2.Read())
                            {
                                conn.Open();
                                reader = new SqlCommand("select sum(grossSalary) from paySheetAudit where month = '" + monthList[i] + "' and empid='" + reader2[0] + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {

                                    dt.Rows.Add("", reader2[0], reader2[1], 0, (reader[0]));
                                }
                                conn.Close();

                            }
                            conn2.Close();

                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }



                    }
                }
                else if (id.Equals(" 04.02"))
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "102" + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        conn.Open();
                        reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3] + " " + electricityRate + "%", 0, (reader.GetDouble(0) / 100) * electricityRate);

                        }
                        conn.Close();

                    }
                    conn4.Close();

                }
                else if (id.Equals(" 04.03"))
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "101" + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        conn.Open();
                        reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3] + " " + waterRate + "%", 0, (reader.GetDouble(0) / 100) * waterRate);

                        }
                        conn.Close();

                    }
                    conn4.Close();

                }
                else if (id.Equals(" 04.04"))
                {
                    conn4.Open();
                  //  reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' ", conn4).ExecuteReader();
                  //  while (reader4.Read())
                    {
                        //if (reader4.GetInt32(0) == 101 || reader4.GetInt32(0) == 102 || reader4.GetInt32(0) == 103 || reader4.GetInt32(0) == 104 || reader4.GetInt32(0) == 105 || reader4.GetInt32(0) == 106 || reader4.GetInt32(0) == 121 || reader4.GetInt32(0) == 122 || reader4.GetInt32(0) == 123)
                        //{

                        //}
                        //else
                        {
                         //   MessageBox.Show(acName.Split('.')[0].ToString().Split(' ')[1].ToString());
                            conn.Open();
                            reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + acName.Split('.') [0].ToString().Split(' ')[1]+ "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3], 0, (reader.GetDouble(0)));

                            }
                            conn.Close();

                        }
                    }
                    conn4.Close();

                }
                else if (id.Equals(" 05.01"))
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "DEPRECIATION" + "' ", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                       
                        {
                            conn.Open();
                            reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3], 0, (reader.GetDouble(0)));

                            }
                            conn.Close();

                        }
                    }
                    conn4.Close();

                }
                else if (id.Equals(" 06.01"))
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "'  and id='" + "121" + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        {
                            conn.Open();
                            reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3], 0, (reader.GetDouble(0)));

                            }
                            conn.Close();

                        }
                    }
                    conn4.Close();

                }
                else if (id.Equals(" 07.01"))
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "'  and id='" + "122" + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        {
                            conn.Open();
                            reader = new SqlCommand("select amount2,date,ref,reason from receipt where date between '" + dateFrom + "' and '" + dateTo + "' and customer='" + reader4[0] + "' order by date", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dt.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3], 0, (reader.GetDouble(0)));

                            }
                            conn.Close();

                        }
                    }
                    conn4.Close();

                }
            }




            ds.Tables.Add(dt);

            //ds.WriteXmlSchema("statment2018.xml");
            // this.Dispose();
            // MessageBox.Show("ok");
            statementVIew pp4 = new statementVIew();
            pp4.SetDataSource(ds);
            pp4.SetParameterValue("head", id + " " + acName + " Summery");
            pp4.SetParameterValue("period", dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString());
            //pp4.SetParameterValue("BFSTOCK", db.setAmountFormat(bfStock + ""));
            //pp4.SetParameterValue("purchasing", db.setAmountFormat(costOfPurchasing + ""));
            //pp4.SetParameterValue("totalinvest", db.setAmountFormat((costOfPurchasing + bfStock) + ""));
            //pp4.SetParameterValue("stock", db.setAmountFormat(stockValue + ""));
            //pp4.SetParameterValue("costofgoodsale", db.setAmountFormat(((costOfPurchasing + bfStock) - stockValue + "")));
            //pp4.SetParameterValue("totalsale", db.setAmountFormat(sale + ""));
            //pp4.SetParameterValue("totalexpences", db.setAmountFormat(expenses + ""));
            //pp4.SetParameterValue("netprofit", db.setAmountFormat(sale - (expenses + ((costOfPurchasing + investEx + bfStock) - stockValue) + investEx) + ""));
            crystalReportViewer1.ReportSource = pp4;

        }

        public void loadCostOfSale(string id, DateTime dateFrom, DateTime dateTo, string acName)
        {
            ArrayList monthList = new ArrayList();

            for (DateTime dts = dateTo; dts <= dateFrom; dts = dts.AddMonths(1))
            {
                monthList.Add(new ListItem(dts.ToString("yyyy/MMMM"), dts.ToString("yyyy")));
            }
            if (dateTo.Month == dateFrom.Month && monthList.Count == 0)
            {
                monthList.Add(new ListItem(dateTo.ToString("yyyy/MMMM"), dateTo.ToString("yyyy")));
            }
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("DATE", typeof(DateTime));
            dt.Columns.Add("REF-", typeof(string));


            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("CREDIT", typeof(double));
            dt.Columns.Add("DEBIT", typeof(double));


            if (id.Equals("02.01"))
            {

                double epf12 = 0, etf3 = 0;
                for (int i = 0; i < monthList.Count; i++)
                {
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.grossSalary,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' a.empid=b.empid", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(monthList[i], reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                        conn.Close();
                    }
                }


            }
            else if (id.Equals("02.02"))
            {
                double epf12 = 0, etf3 = 0;
                for (int i = 0; i < monthList.Count; i++)
                {
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.epf12,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' a.empid=b.empid", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(monthList[i], reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                        conn.Close();
                    }
                }
            }
            else if (id.Equals("02.03"))
            {
                double epf12 = 0, etf3 = 0;
                for (int i = 0; i < monthList.Count; i++)
                {
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.etf3,b.name,a.empid from paySheetAudit as a,emp as b where a.month = '" + monthList[i] + "' a.empid=b.empid", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(monthList[i], reader[2], reader[1], 0, db.setAmountFormat(reader[0] + ""));
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                        conn.Close();
                    }
                }
            }

            ds.Tables.Add(dt);

            //ds.WriteXmlSchema("statment2018.xml");
            // this.Dispose();
            // MessageBox.Show("ok");
            statementVIew pp4 = new statementVIew();
            pp4.SetDataSource(ds);
            pp4.SetParameterValue("head", id + " " + acName + " Summery");
            pp4.SetParameterValue("period", dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString());
            //pp4.SetParameterValue("BFSTOCK", db.setAmountFormat(bfStock + ""));
            //pp4.SetParameterValue("purchasing", db.setAmountFormat(costOfPurchasing + ""));
            //pp4.SetParameterValue("totalinvest", db.setAmountFormat((costOfPurchasing + bfStock) + ""));
            //pp4.SetParameterValue("stock", db.setAmountFormat(stockValue + ""));
            //pp4.SetParameterValue("costofgoodsale", db.setAmountFormat(((costOfPurchasing + bfStock) - stockValue + "")));
            //pp4.SetParameterValue("totalsale", db.setAmountFormat(sale + ""));
            //pp4.SetParameterValue("totalexpences", db.setAmountFormat(expenses + ""));
            //pp4.SetParameterValue("netprofit", db.setAmountFormat(sale - (expenses + ((costOfPurchasing + investEx + bfStock) - stockValue) + investEx) + ""));
            crystalReportViewer1.ReportSource = pp4;

        }

        Point p;
        private void accountList_Load(object sender, EventArgs e)
        {
            //   this.Textstatement = "hy";
            //            dataGridView1.AllowUserToAddRows = false;

            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            ////dataGridView1.Width = width - 20;
            //dataGridView1.Height = height - 130;
            ////  dataGridView1.Columns[0].Width = dataGridView1.Width - 470;

            //p = new Point();

            //p.X = 0;
            //p.Y = dataGridView1.Height + 50;

            //panel1.Location = p;

            //p = dataGridView1.Location;
            ////   MessageBox.Show((width - dataGridView1.Width) / 2+"");
            //p.X = (width - dataGridView1.Width) / 2;
            //dataGridView1.Location = p;

            db = new DB();
            conn = db.createSqlConnection();

            db2 = new DB();
            conn2 = db2.createSqlConnection();

            db3 = new DB();
            conn3 = db3.createSqlConnection();
            db4 = new DB();
            conn4 = db4.createSqlConnection();
            //year.Format = DateTimePickerFormat.Custom;
            //year.CustomFormat = "yyyy";
            //  load();

        }

        private void accountList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            //homeH.Enabled = true;
            //homeH.load();
            //homeH.TopMost = true;
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

        }

        private void year_ValueChanged(object sender, EventArgs e)
        {

        }

        private void year_CloseUp(object sender, EventArgs e)
        {

        }

        private void year_KeyDown(object sender, KeyEventArgs e)
        {

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
    }
}
