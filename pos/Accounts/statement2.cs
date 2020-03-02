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
    public partial class statement2 : Form
    {
        public statement2()
        {
            InitializeComponent();

        }

      //  accountList homeH;
        // My Variable Start

        DB db, db2, db3, db4;
        Form home;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        Double openingBalance, amount, cre, debi;
        string date, idB, userH;
        public bool states, loadBankBool = false, loadFixedAsset = false, loadEQUITY = false, loadLibilityBool = false;
        ArrayList arrayLst;
        string[] array;
        Int32 yearB, monthB;
        DateTime dateSearchB;
        public void loadRevenue(string id, DateTime dateFrom, DateTime dateTo, string acName)
        {

            double electricityRate = 0, waterRate = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("DATE", typeof(string));
            dt.Columns.Add("REF-", typeof(string));


            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("CREDIT", typeof(double));
            dt.Columns.Add("DEBIT", typeof(double));
            dt.Columns.Add("BALANCE", typeof(double));
            double bf = 0;
            {
                if (id.Equals(" 01.01"))
                {
                    double expensesTotal = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "NON CURRENT ASSETS" + "' and id='" + acName.Split('.')[0] + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        {
                            bf = 0;
                            try
                            {
                                conn.Open();
                                reader = new SqlCommand("select sum(amount2) from receipt where date < '" + dateFrom + "'  customer='" + reader4[0] + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    bf = reader.GetDouble(0);

                                }
                                conn.Close();
                            }
                            catch (Exception)
                            {
                                conn.Close();
                            }
                            dt.Rows.Add("B/F", "", "", 0, bf);
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
                else if (id.Equals(" 02.01"))
                {
                    double expensesTotal = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select id,name from accounts where type='" + "INVESTMENTS" + "' and id='" + acName.Split('.')[0] + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {

                        {
                            bf = 0;
                            try
                            {
                                conn.Open();
                                reader = new SqlCommand("select sum(amount2) from receipt where date < '" + dateFrom + "'  customer='" + reader4[0] + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    bf = reader.GetDouble(0);

                                }
                                conn.Close();
                            }
                            catch (Exception)
                            {
                                conn.Close();
                            }
                            dt.Rows.Add("B/F", "", "", 0, bf);
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
                else if (id.Equals(" 03.01"))
                {
                    double CurrentAssetsSparePart = 0, credit = 0, debit = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select purchasingPrice,QTY,code,detail from ITEM where isItem='" + true + "' AND sparePart='" + true + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                        credit = 0;
                        debit = 0;
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date<'" + dateFrom + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date<'" + dateFrom + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();

                        CurrentAssetsSparePart = CurrentAssetsSparePart+(credit - debit);
                       
                       

                      
                    }
                    conn4.Close();
                    dt.Rows.Add("B/F", "", "", 0, 0, CurrentAssetsSparePart);
                    conn4.Open();
                    reader4 = new SqlCommand("select purchasingPrice,QTY,code,detail from ITEM where isItem='" + true + "' AND sparePart='" + true + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                        credit = 0;
                        debit = 0;
                      
                        credit = 0;
                        debit = 0;
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date between  '" + dateFrom.ToShortDateString() + "' and '" + dateTo.ToShortDateString() + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date between  '" + dateFrom.ToShortDateString() + "' and '" + dateTo.ToShortDateString() + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();

                        conn.Close();
                        if (credit == 0 && debit == 0)
                        {
                          
                        }
                        else {
                            dt.Rows.Add("", "", reader4[3], credit, debit, (credit - debit));
                        }
                      
                    }
                    conn4.Close();
                }
                else if (id.Equals(" 03.02"))
                {
                    double CurrentAssetsSparePart = 0, credit = 0, debit = 0;

                    conn4.Open();
                    reader4 = new SqlCommand("select purchasingPrice,QTY,code,detail from ITEM where isItem='" + true + "' AND sparePart='" + false + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                       
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date<'" + dateFrom + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date<'" + dateFrom + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();
                       
                        
                    }
                    conn4.Close();
                    dt.Rows.Add("B/F", "", "", 0, 0, (credit - debit));
                    conn4.Open();
                    reader4 = new SqlCommand("select purchasingPrice,QTY,code,detail from ITEM where isItem='" + true + "' AND sparePart='" + false + "'", conn4).ExecuteReader();
                    while (reader4.Read())
                    {
                       
                        credit = 0;
                        debit = 0;
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date between  '" + dateFrom + "' and '" + dateTo + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date between  '" + dateFrom + "' and '" + dateTo + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                        }
                        conn3.Close();

                        conn.Close();
                        if (credit == 0 && debit == 0)
                        {

                        }
                        else
                        {
                            dt.Rows.Add("", "", reader4[3], credit, debit, (credit - debit));
                        }
                      
                      
                    }
                    conn4.Close();
                }
                else if (id.Equals(" 03.03"))
                {
                    double credit = 0, TradeReceivables = 0;
                    double debit = 0;
                    
                   
                    conn2.Open();
                    reader2 = new SqlCommand("select id,company from customer order by id", conn2).ExecuteReader();
                    while (reader2.Read())
                    {

                       
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(balance) from creditInvoiceRetail  where customerid ='" +reader2[0] + "' and date<'" + dateFrom + "' ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                 credit =credit+ reader.GetDouble(0);
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

                            reader = new SqlCommand("select sum(paid) from invoiceCreditPaid as a,invoiceretail as b  where a.date   < '" + dateFrom + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
                            if (reader.Read())
                            {

                                debit =debit+ reader.GetDouble(0);


                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }
                      

                        
                           
                        
                    }
                    dt.Rows.Add("B/F", "", "", credit, debit, (credit - debit));

                    conn2.Close();
                    conn2.Open();
                    reader2 = new SqlCommand("select id,company from customer order by company", conn2).ExecuteReader();
                    while (reader2.Read())
                    {

                        credit = 0;
                        debit = 0;
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(balance) from creditInvoiceRetail  where customerid ='" + reader2[0] + "' and date between '" + dateFrom + "' and '" + dateTo + "' ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                credit = reader.GetDouble(0);
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

                            reader = new SqlCommand("select sum(paid) from invoiceCreditPaid as a,invoiceretail as b  where a.date   between '" + dateFrom + "' and '" + dateTo + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
                            if (reader.Read())
                            {

                                debit = reader.GetDouble(0);


                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }


                        if (credit == 0 && debit == 0)
                        {

                        }
                        else
                        {
                            dt.Rows.Add("", reader2[0], reader2[1], credit, debit, (credit - debit));
                        }
                    }


                    conn2.Close();

                }
                else if (id.Equals(" 04.01"))
                {
                    double credit = 0, TradeReceivables = 0;
                    double debit = 0;


                    conn2.Open();
                    reader2 = new SqlCommand("select id,company from supplier order by id", conn2).ExecuteReader();
                    while (reader2.Read())
                    {


                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(balance) from creditgrn  where customerid ='" + reader2[0] + "' and date<'" + dateFrom + "' ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                credit = credit + reader.GetDouble(0);
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

                            reader = new SqlCommand("select sum(paid) from grnCreditPaid as a,grn as b  where a.date   < '" + dateFrom + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
                            if (reader.Read())
                            {

                                debit = debit + reader.GetDouble(0);


                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }





                    }
                    dt.Rows.Add("B/F", "", "", credit, debit, (credit - debit));

                    conn2.Close();
                    conn2.Open();
                    reader2 = new SqlCommand("select id,company from supplier order by company", conn2).ExecuteReader();
                    while (reader2.Read())
                    {

                        credit = 0;
                        debit = 0;
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(balance) from creditgrn  where customerid ='" + reader2[0] + "' and date between '" + dateFrom + "' and '" + dateTo + "' ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                credit = reader.GetDouble(0);
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

                            reader = new SqlCommand("select sum(paid) from grnCreditPaid as a,grn as b  where a.date   between '" + dateFrom + "' and '" + dateTo + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
                            if (reader.Read())
                            {

                                debit = reader.GetDouble(0);


                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }


                        if (credit == 0 && debit == 0)
                        {

                        }
                        else
                        {
                            dt.Rows.Add("", reader2[0], reader2[1], credit, debit, (credit - debit));
                        }
                    }


                    conn2.Close();

                }
            }




            ds.Tables.Add(dt);

            ds.WriteXmlSchema("statment2018_.xml");
            // this.Dispose();
            // MessageBox.Show("ok");
            statementVIew2 pp4 = new statementVIew2();
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
            //   this.Text = "hy";
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
