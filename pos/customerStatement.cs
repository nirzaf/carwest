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
    public partial class customerStatement : Form
    {
        public customerStatement(Form form, String user, bool AS)
        {
            InitializeComponent();
            home = form;
            userH = user;
            isSingle = AS;
        }


        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;

        Double openingBalance, amount, cre, debi;
        string userH, cusH;
        public bool states, loadBankBool = false, loadFixedAsset = false, loadEQUITY = false, loadLibilityBool = false,isSingle;

        Int32 yearB, monthB;
        DateTime dateSearchB;
        DataTable dt;
        DataSet ds;
        double totalOut, amount3, amount4, amount2;
        public void loadBank(DateTime dateSearch, string cus)
        {
            comboBox1.Text = db.getMOnthName(dateSearch.Month.ToString().ToUpper());
            cusH = cus;
            db.setCursoerWait();
            dt = new DataTable();
            ds = new DataSet();


            dt.Columns.Add("DATE", typeof(string));
            dt.Columns.Add("INVOICE", typeof(string));
            dt.Columns.Add("CHEQUE/CASH", typeof(string));
            dt.Columns.Add("CREDIT", typeof(double));

            dt.Columns.Add("DEBIT", typeof(double));
            dt.Columns.Add("BALANCE", typeof(double));

            dateSearchB = dateSearch;

            try
            {
                totalOut = 0;
                amount3 = 0;
                amount4 = 0;
                try
                {
                    conn2.Open();
                    amount = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + cus + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                    //    reader2 = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + "C-" + invoiceNO.Text + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                //        MessageBox.Show("1");
                    }
                    else {
                        reader2 = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where  a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                      //  MessageBox.Show("2");
                    }
                    if (reader2.Read())
                    {
                        amount = reader2.GetDouble(0);
                     //   MessageBox.Show("" + amount);
                    }
                    conn2.Close();
                }
                catch (Exception a)
                {
                    //MessageBox.Show(a.Message+"/"+a.StackTrace);
                    amount = 0;
                    conn2.Close();
                }
              //  MessageBox.Show(dateSearch.Year + "-" + dateSearch.Month + "-1" + "A");
                //try
                //{
                //    conn2.Open();
                //   // amount = 0;
                //    if (isSingle)
                //    {
                //        reader2 = new SqlCommand("select amount from cusBalance  where customer='" + cus + "' and date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" +  "' ", conn2).ExecuteReader();

                //    }
                //    else
                //    {
                //        reader2 = new SqlCommand("select amount from cusBalance  where customer='" + cus + "' ", conn2).ExecuteReader();

                //    }
                //    if (reader2.Read())
                //    {
                //        amount =amount+ reader2.GetDouble(0);
                //    }
                //    conn2.Close();
                //}
                //catch (Exception)
                //{
                //    amount = 0;
                //    conn2.Close();
                //}
              //  MessageBox.Show(amount + "B");
                try
                {
                    conn2.Open();
                    amount2 = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select sum(amount2) from receipt where customer='" + cus + "' and date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + "" + "' ", conn2).ExecuteReader();

                    }
                    else {
                        reader2 = new SqlCommand("select sum(amount2) from receipt where  date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + "" + "' ", conn2).ExecuteReader();

                    }
                    if (reader2.Read())
                    {
                        amount2 = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount2 = 0;
                    conn2.Close();
                }
              //  MessageBox.Show(amount2 + "C");
                totalOut = amount - amount2;

                dt.Rows.Add("B/F", "",  "", "0","0", totalOut );
                //MessageBox.Show(invoiceNO.Text);
                //    var a = ;
                for (int i = 1; i <= Int32.Parse(db.getLastDate(dateSearch.Month, dateSearch.Year)); i++)
                {
                    // MessageBox.Show(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i);
                    conn2.Open();
                    amount = 0;
                    //if (isSingle)
                    //{
                    //    reader2 = new SqlCommand("select * from cusBalance where customer='" + cus + "' and date between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' ", conn2).ExecuteReader();

                    //}
                    //else
                    //{
                    //    reader2 = new SqlCommand("select * from cusBalance where date  between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' ", conn2).ExecuteReader();

                    //}
                    //while (reader2.Read())
                    //{
                    //    amount3 = amount3 + reader2.GetDouble(2);
                    //    totalOut = totalOut + reader2.GetDouble(2);
                    //    dt.Rows.Add(reader2.GetDateTime(3).ToShortDateString(), "BALANCE", "CREDIT INVOICE", reader2[2], 0.0, totalOut);
                    //}
                    conn2.Close();
                    conn2.Open();
                    amount = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select a.* from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + cus + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 00:00:00" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 23:59:59" + "' ", conn2).ExecuteReader();
                   
                    }
                    else {
                        reader2 = new SqlCommand("select a.* from creditInvoiceRetail as a,invoiceTerm as b where  a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 00:00:00" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 23:59:59" + "' ", conn2).ExecuteReader();
                   
                    }
                    while (reader2.Read())
                    {
                        amount3 = amount3 + reader2.GetDouble(4);
                        totalOut = totalOut + reader2.GetDouble(4);
                        dt.Rows.Add(reader2.GetDateTime(6).ToShortDateString(), "R-" + reader2[0], "CREDIT INVOICE",reader2[4] , 0.0,totalOut );
                    }
                    conn2.Close();

                    conn2.Open();
                    amount2 = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select * from receipt where customer='" + cus + "' and date='" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                 
                    }
                    else {
                        reader2 = new SqlCommand("select * from receipt where  date='" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                 
                    }
                   while (reader2.Read())
                    {
                        amount4 = amount4 + reader2.GetDouble(5);
                        totalOut = totalOut - reader2.GetDouble(5);
                        dt.Rows.Add(reader2.GetDateTime(1).ToShortDateString(), reader2[0], reader2[8], 0.0, reader2[5] , totalOut);
                    }
                    conn2.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }


            ds.Tables.Add(dt);
            cusStatement pp = new cusStatement();
            pp.SetDataSource(ds);
            if (isSingle)
            {
                conn2.Open();
                reader2 = new SqlCommand("select company from customer where id='"+cus+"'",conn2).ExecuteReader();
                if (reader2.Read())
                {
                    pp.SetParameterValue("user", dateSearch.Year + "/" + dateSearch.Month+" [ "+reader2.GetString(0).ToUpper()+" ]");

                }
                conn2.Close();
               

            }
            else {
                pp.SetParameterValue("user",  dateSearch.Year + "/" + dateSearch.Month+" [ ALL CUSTOMERS ]");

            }
          

            crystalReportViewer1.ReportSource = pp;
            // MessageBox.Show("2");
            // pp.PrintToPrinter(1, false, 0, 0);
            //new cusStateView(pp).Visible = true;
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

            p = new Point();



            p = crystalReportViewer1.Location;
            //   MessageBox.Show((width - dataGridView1.Width) / 2+"");
            p.X = (width - crystalReportViewer1.Width) / 2;
            crystalReportViewer1.Location = p;

            db = new DB();
            conn = db.createSqlConnection();

            db2 = new DB();
            conn2 = db2.createSqlConnection();

            year.Format = DateTimePickerFormat.Custom;
            year.CustomFormat = "yyyy";
            //  load();

        }

        private void accountList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;

            home.TopMost = true;
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
            loadBank(DateTime.Parse(year.Value.Year + "/" + comboBox1.SelectedItem.ToString() + "/1"), cusH);

        }

        private void year_ValueChanged(object sender, EventArgs e)
        {
            //loadBank(DateTime.Parse(year.Value.Year + "/" + comboBox1.SelectedItem.ToString() + "/1"), cusH);

        }

        private void year_CloseUp(object sender, EventArgs e)
        {
            loadBank(DateTime.Parse(year.Value.Year + "/" + comboBox1.SelectedItem.ToString() + "/1"), cusH);

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
