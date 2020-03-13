using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class supplierStatement : Form
    {
        public supplierStatement(Form form, String user, bool AS)
        {
            InitializeComponent();
            home = form;
            userH = user;
            isSingle = AS;
        }

        private DB db, db2;
        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;

        private Double openingBalance, amount, cre, debi;
        private string userH, cusH;
        public bool states, loadBankBool = false, loadFixedAsset = false, loadEQUITY = false, loadLibilityBool = false, isSingle;

        private Int32 yearB, monthB;
        private DateTime dateSearchB;
        private DataTable dt;
        private DataSet ds;
        private double totalOut, amount3, amount4, amount2;

        public void loadBank(DateTime dateSearch, string cus)
        {
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
                        reader2 = new SqlCommand("select sum(a.balance) from creditGRN as a,grnTerm as b where a.customerid='" + cus + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                    }
                    else
                    {
                        reader2 = new SqlCommand("select sum(a.balance) from creditGRN as a,grnTerm as b where  a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                    }
                    if (reader2.Read())
                    {
                        amount = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount = 0;
                    conn2.Close();
                }
                try
                {
                    conn2.Open();
                    amount2 = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select sum(amount2) from receipt2 where customer='" + cus + "' and date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + "" + "' ", conn2).ExecuteReader();
                    }
                    else
                    {
                        reader2 = new SqlCommand("select sum(amount2) from receipt2 where date<'" + dateSearch.Year + "-" + dateSearch.Month + "-1" + "" + "' ", conn2).ExecuteReader();
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
                totalOut = amount - amount2;

                dt.Rows.Add("B/F", "", "", "0", "0", totalOut);
                //MessageBox.Show(invoiceNO.Text);
                //    var a = ;
                for (int i = 1; i <= Int32.Parse(db.getLastDate(dateSearch.Month, dateSearch.Year)); i++)
                {
                    // MessageBox.Show(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i);
                    conn2.Open();
                    amount = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select a.* from creditGRN as a,grnTerm as b where a.customerid='" + cus + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 00:00:00" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 23:59:59" + "' ", conn2).ExecuteReader();
                    }
                    else
                    {
                        reader2 = new SqlCommand("select a.* from creditGRN as a,grnTerm as b where  a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date between '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 00:00:00" + "' and '" + dateSearch.Year + "-" + dateSearch.Month + "-" + i + " 23:59:59" + "' ", conn2).ExecuteReader();
                    }
                    while (reader2.Read())
                    {
                        amount3 = amount3 + reader2.GetDouble(4);
                        totalOut = totalOut + reader2.GetDouble(4);
                        dt.Rows.Add(reader2.GetDateTime(6).ToShortDateString(), "" + reader2[0], "CREDIT PURCHASING", reader2[4], 0.0, totalOut);
                    }
                    conn2.Close();

                    conn2.Open();
                    amount2 = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select * from receipt2 where customer='" + cus + "' and date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                    }
                    else
                    {
                        reader2 = new SqlCommand("select * from receipt2 where  date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                    }
                    while (reader2.Read())
                    {
                        amount4 = amount4 + reader2.GetDouble(5);
                        totalOut = totalOut - reader2.GetDouble(5);
                        dt.Rows.Add(reader2.GetDateTime(1).ToShortDateString(), reader2[0], reader2[8], 0.0, reader2[5], totalOut);
                    }
                    conn2.Close();

                    conn2.Open();
                    amount2 = 0;
                    if (isSingle)
                    {
                        reader2 = new SqlCommand("select * from returnGoods where customer='" + cus + "' and date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' AND credit='" + true + "'", conn2).ExecuteReader();
                    }
                    else
                    {
                        reader2 = new SqlCommand("select * from returnGoods where  date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' AND credit='" + true + "'", conn2).ExecuteReader();
                    }
                    while (reader2.Read())
                    {
                        amount4 = amount4 + reader2.GetDouble(5);
                        totalOut = totalOut - reader2.GetDouble(5);
                        dt.Rows.Add(reader2.GetDateTime(6).ToShortDateString(), "RETURN-GOODS", "", 0.0, reader2[5], totalOut);
                    }
                    conn2.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }

            ds.Tables.Add(dt);
            SUPStatement pp = new SUPStatement();
            pp.SetDataSource(ds);
            if (isSingle)
            {
                conn2.Open();
                reader2 = new SqlCommand("select company from supplier where id='" + cus + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    pp.SetParameterValue("user", dateSearch.Year + "/" + dateSearch.Month + " [ " + reader2.GetString(0).ToUpper() + " ]");
                }
                conn2.Close();
            }
            else
            {
                pp.SetParameterValue("user", dateSearch.Year + "/" + dateSearch.Month + " [ ALL SUPPLIERS ]");
            }

            crystalReportViewer1.ReportSource = pp;
            // MessageBox.Show("2");
            // pp.PrintToPrinter(1, false, 0, 0);
            //new cusStateView(pp).Visible = true;
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