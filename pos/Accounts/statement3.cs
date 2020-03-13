using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace pos
{
    public partial class statement3 : Form
    {
        public statement3()
        {
            InitializeComponent();
        }

        //   accountList homeH;
        // My Variable Start

        private DB db, db2, db3, db4;
        private Form home;
        private SqlConnection conn, conn2, conn3, conn4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private Double openingBalance, amount, cre, debi;
        private string date, credit, debit, idB, userH;
        public bool states, loadBankBool = false, loadFixedAsset = false, loadEQUITY = false, loadLibilityBool = false;
        private ArrayList arrayLst;
        private string[] array;
        private Int32 yearB, monthB;
        private DateTime dateSearchB;

        public void loadRevenue(DataGridView dgv, DateTime dateFrom, DateTime dateTo, string acName)
        {
            ArrayList monthList = new ArrayList();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("DATE", typeof(string));
            dt.Columns.Add("REF-", typeof(string));

            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("CREDIT", typeof(string));
            dt.Columns.Add("DEBIT", typeof(string));

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dt.Rows.Add(dgv.Rows[i].Cells[0].Value, dgv.Rows[i].Cells[1].Value, dgv.Rows[i].Cells[2].Value);
            }

            ds.Tables.Add(dt);

            //ds.WriteXmlSchema("statment2018.xml");
            // this.Dispose();
            // MessageBox.Show("ok");
            statementVIew3 pp4 = new statementVIew3();
            pp4.SetDataSource(ds);
            pp4.SetParameterValue("head", acName + " ");
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

        private Point p;

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