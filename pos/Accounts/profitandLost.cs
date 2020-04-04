using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace pos
{
    public partial class profitandLost : Form
    {
        public profitandLost(Form form, String user)
        {
            InitializeComponent();
            homeH = form;
            userH = user;
        }

        private Form homeH;
        // My Variable Start

        private DB db, db2, db3, db4;
        private SqlConnection conn, conn2, conn3, conn4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private Double invest, amount, total, tempTOtalSale, tempCredistSale, tempChequeSale, tempCardSale, tempCashSale, tempExpen, tempCashRecevied, tempCashGiven, tempCashPaidReturn;
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
        }

        private Point p;

        private void accountList_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;

            //dataGridView1.Width = width - 20;
            //  crystalReportViewer1.Height = height - 130;
            //  dataGridView1.Columns[0].Width = dataGridView1.Width - 470;

            //p = crystalReportViewer1.Location;
            //   MessageBox.Show((width - dataGridView1.Width) / 2+"");
            //p.X = (width - crystalReportViewer1.Width) / 2;
            //crystalReportViewer1.Location = p;

            db = new DB();
            conn = db.createSqlConnection();

            db2 = new DB();
            conn2 = db2.createSqlConnection();

            db3 = new DB();
            conn3 = db3.createSqlConnection();
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
            loadIncome(idB, "");
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

        private double amountCost, amountPaid, balance, temp030, temp3060, temp6090, temp90up, a;

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

        private double stockValue, costOfPurchasing, sale, expenses, SalaryEx, investEx, bfStock, electricityRate, waterRate, gross;

        private void PRINT_Click(object sender, EventArgs e)
        {
            db.setCursoerWait();
            conn.Open();
            reader = new SqlCommand("select itemCode,id from itemStatement", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select purchasingPrice from item where code='" + reader[0] + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    conn3.Open();
                    new SqlCommand("update itemStatement set purchsingPrice='" + reader2[0] + "' where id='" + reader[1] + "'", conn3).ExecuteNonQuery();
                    conn3.Close();
                }
                conn2.Close();
            }
            conn.Close();
            conn.Open();
            reader = new SqlCommand("select reason,id from receipt", conn).ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(0).Split('-')[0].ToString().Equals("ADVANCE"))
                {
                    conn3.Open();
                    new SqlCommand("delete from receipt where id='" + reader[1] + "'", conn3).ExecuteNonQuery();
                    conn3.Close();
                }
            }
            conn.Close();

            conn.Open();
            reader = new SqlCommand("select id,customerID from invoiceretail", conn).ExecuteReader();
            while (reader.Read())
            {
                conn3.Open();
                new SqlCommand("update creditInvoiceRetail set customerId='" + reader[1] + "'  where invoiceID='" + reader[0] + "'", conn3).ExecuteNonQuery();
                conn3.Close();
            }
            conn.Close();
            db.setCursoerDefault();
            // MessageBox.Show("ok");
            db.setCursoerWait();
            if (radioButton1.Checked)
            {
                dataGridView1.Rows.Clear();
                ArrayList monthList = new ArrayList();

                for (DateTime dts = toDate.Value; dts <= fromDate.Value; dts = dts.AddMonths(1))
                {
                    monthList.Add(new ListItem(dts.ToString("yyyy/MMMM"), dts.ToString("yyyy")));
                }
                if (toDate.Value.Month == fromDate.Value.Month && monthList.Count == 0)
                {
                    monthList.Add(new ListItem(toDate.Value.ToString("yyyy/MMMM"), toDate.Value.ToString("yyyy")));
                }

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

                amount = 0;
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from plRates ", conn).ExecuteReader();
                if (reader.Read())
                {
                    electricityRate = reader.GetDouble(0);
                    waterRate = reader.GetDouble(1);
                }
                conn.Close();
                gross = 0;
                expenses = 0;
                double ssRevenue = 0.0, spRevenue = 0.0;

                conn.Open();
                reader = new SqlCommand("select* from invoiceRetail where date between '" + fromDate.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' ", conn).ExecuteReader();
                while (reader.Read())
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select totalPrice,itemcode from invoiceRetailDetail where invoiceid='" + reader[0] + "' ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        conn3.Open();
                        reader3 = new SqlCommand("select sparePart from item where code='" + reader2[1] + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            if (!reader3.GetBoolean(0))
                            {
                                ssRevenue = ssRevenue + reader2.GetDouble(0);
                            }
                            else
                            {
                                spRevenue = spRevenue + reader2.GetDouble(0);
                            }
                            gross = gross + reader2.GetDouble(0);
                        }
                        else
                        {
                            ssRevenue = ssRevenue + reader2.GetDouble(0);
                        }
                        conn3.Close();
                    }
                    conn2.Close();
                }
                conn.Close();
                dataGridView1.Rows.Add("01", "Revenue", db.setAmountFormat(ssRevenue + spRevenue + ""), "log");
                dataGridView1.Rows.Add(" 01.01", "   Service Station Revenue", db.setAmountFormat(ssRevenue + ""), "log");
                dataGridView1.Rows.Add(" 01.02", "   Spare Parts Revenue", db.setAmountFormat(spRevenue + ""), "log");
                try
                {
                    investEx = 0;
                    conn4.Open();
                    reader4 = new SqlCommand("select sum(amount) from cashsummery where remark='" + "INVEST-MANUAL" + "' and date between '" + fromDate.Value + "' and '" + toDate.Value + "'", conn4).ExecuteReader();
                    if (reader4.Read())
                    {
                        dataGridView1.Rows.Add(" 01.03", "   Invest", db.setAmountFormat(reader4.GetDouble(0) + ""), "log");
                        investEx = reader4.GetDouble(0);
                    }
                    conn4.Close();
                }
                catch (Exception)
                {
                    conn4.Close();
                }
                dataGridView1.Rows.Add("", "", "", "");
                gross = 0;
                double epf12 = 0, etf3 = 0;
                for (int i = 0; i < monthList.Count; i++)
                {
                    try
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select empid from emp where isExecutive='" + false + "'", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(grossSalary) from paySheetAudit where month = '" + monthList[i] + "' and empid='" + reader2[0] + "'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                // dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", reader.GetDouble(0), reader.GetDouble(0));

                                gross = gross + reader.GetDouble(0);
                            }
                            conn.Close();
                        }
                        conn2.Close();
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                        //dt.Rows.Add("Cost of Sales", "Direct Labor Expenses", 0, 0);

                        conn.Close();
                    }
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select sum(EPF12) from paySheetAudit where month = '" + monthList[i] + "' ", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            // dt.Rows.Add("Cost of Sales", "EPF", reader.GetDouble(0), reader.GetDouble(0), 2);
                            epf12 = epf12 + reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                        // dt.Rows.Add("Cost of Sales", "EPF", 0, 0, 2);

                        conn.Close();
                    }
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select sum(ETF3) from paySheetAudit where month = '" + monthList[i] + "' ", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Cost of Sales", "ETF", reader.GetDouble(0), reader.GetDouble(0), 2);
                            etf3 = etf3 + reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                        //  dt.Rows.Add("Cost of Sales", "ETF", 0, 0, 2);
                        conn.Close();
                    }
                }

                double credit = 0, debit = 0, purchasing = 0, bfStock = 0, lastStock = 0; ;

                credit = 0;
                debit = 0;

                conn4.Open();
                reader4 = new SqlCommand("select purchasingPrice,QTY,code from ITEM where isItem='" + true + "' ", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    conn3.Open();
                    reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date between '" + fromDate.Value + "' and '" + toDate.Value + "'", conn3).ExecuteReader();
                    while (reader3.Read())
                    {
                        debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                    }
                    conn3.Close();
                }
                conn4.Close();

                dataGridView1.Rows.Add("02", "Cost of Sales", db.setAmountFormat(gross + epf12 + etf3 + (debit) + ""), "log");
                dataGridView1.Rows.Add(" 02.01", "  Direct Labor Expenses", db.setAmountFormat(gross + ""), "log");
                dataGridView1.Rows.Add(" 02.02", "  EPF 12%", db.setAmountFormat(epf12 + ""), "log");
                dataGridView1.Rows.Add(" 02.03", "  ETF 3%", db.setAmountFormat(etf3 + ""), "log");
                dataGridView1.Rows.Add(" 02.04", "  Direct Materials", db.setAmountFormat((debit) + ""), "log");

                double expensesTotal = 0;

                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    expenses = 0;
                    if (reader4.GetInt32(0) == 101)
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Cost of Sales", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);

                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        expensesTotal = expensesTotal + (expenses / 100) * (100 - waterRate);
                        dataGridView1.Rows.Add(" 02.05", "  " + reader4[0] + "." + reader4[1] + " " + (100 - waterRate) + "%", db.setAmountFormat(((expenses / 100) * (100 - waterRate)) + ""), "log");
                    }
                    else if (reader4.GetInt32(0) == 102)
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Cost of Sales", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);

                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        expensesTotal = expensesTotal + (expenses / 100) * (100 - electricityRate);
                        dataGridView1.Rows.Add(" 02.05", "  " + reader4[0] + "." + reader4[1] + " " + (100 - electricityRate) + "%", db.setAmountFormat(((expenses / 100) * (100 - electricityRate)) + ""), "log");
                    }
                    else if (reader4.GetInt32(0) == 103 || reader4.GetInt32(0) == 104)
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Cost of Sales", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);

                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        expensesTotal = expensesTotal + expenses;
                        dataGridView1.Rows.Add(" 02.05", "  " + reader4[0] + "." + reader4[1], db.setAmountFormat((expenses) + ""), "log");
                    }
                }
                conn4.Close();

                dataGridView1.Rows[4].Cells[2].Value = db.setAmountFormat((expensesTotal + gross + epf12 + etf3 + (debit)) + "");

                dataGridView1.Rows.Add("", "Gross Profit", db.setAmountFormat((ssRevenue + spRevenue + investEx) - ((expensesTotal + gross + epf12 + etf3 + (debit))) + ""), "");
                dataGridView1.Rows.Add("", "", "", "");

                dataGridView1.Rows.Add("03", "Distribution Costs", 0, "");

                int rowcount = dataGridView1.Rows.Count;
                double distribiuitExpenses = 0, totaldistribiuitExpenses = 0;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    if (reader4.GetInt32(0) == 105 || reader4.GetInt32(0) == 106)
                    {
                        distribiuitExpenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Distribution Costs", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);
                            distribiuitExpenses = distribiuitExpenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        totaldistribiuitExpenses = totaldistribiuitExpenses + distribiuitExpenses;
                        dataGridView1.Rows.Add(" 03.01", "  " + reader4[0] + "." + reader4[1], db.setAmountFormat(distribiuitExpenses + ""), "log");
                    }
                }
                conn4.Close();
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(totaldistribiuitExpenses + "");
                double adminExpenses = 0, general = 0;
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("04", "Administrative Expenses", 0, "");

                rowcount = dataGridView1.Rows.Count;
                for (int i = 0; i < monthList.Count; i++)
                {
                    try
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select empid from emp where isExecutive='" + true + "'", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(grossSalary) from paySheetAudit where month = '" + monthList[i] + "' and empid='" + reader2[0] + "'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                general = general + reader.GetDouble(0);
                            }
                            conn.Close();
                        }
                        conn2.Close();
                        dataGridView1.Rows.Add(" 04.01", "  General Salary", db.setAmountFormat(general + ""), "log");
                        adminExpenses = adminExpenses + general;
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                        conn.Close();
                    }
                }
                double GeneralElectricity = 0;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "102" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            GeneralElectricity = GeneralElectricity + reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                }
                conn4.Close();

                adminExpenses = adminExpenses + (GeneralElectricity / 100) * electricityRate;
                if (GeneralElectricity != 0)
                {
                    dataGridView1.Rows.Add(" 04.02", "  General Electricity " + electricityRate + "%", db.setAmountFormat("" + (GeneralElectricity / 100) * electricityRate), "log");
                }
                else
                {
                    dataGridView1.Rows.Add(" 04.02", "  General Electricity " + electricityRate + "%", db.setAmountFormat("0"), "log");
                }
                double GeneralWater = 0;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "101" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            GeneralWater = GeneralWater + reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                }
                conn4.Close();
                adminExpenses = adminExpenses + (GeneralWater / 100) * waterRate;
                if (GeneralElectricity != 0)
                {
                    dataGridView1.Rows.Add(" 04.03", "  General Water " + waterRate + "%", db.setAmountFormat("" + (GeneralWater / 100) * waterRate), "log");
                }
                else
                {
                    dataGridView1.Rows.Add(" 04.03", "  General Water " + waterRate + "%", db.setAmountFormat("0"), "log");
                }
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    if (reader4.GetInt32(0) == 101 || reader4.GetInt32(0) == 102 || reader4.GetInt32(0) == 103 || reader4.GetInt32(0) == 104 || reader4.GetInt32(0) == 105 || reader4.GetInt32(0) == 106 || reader4.GetInt32(0) == 121 || reader4.GetInt32(0) == 122 || reader4.GetInt32(0) == 123)
                    {
                    }
                    else
                    {
                        expenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        adminExpenses = adminExpenses + expenses;
                        dataGridView1.Rows.Add(" 04.04", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(expenses + ""), "log");
                    }
                }
                conn4.Close();

                try
                {
                    conn4.Open();
                    reader4 = new SqlCommand("select sum(amount2) from receipt where ref='" + "CASH OUT" + "' and date between '" + fromDate.Value + "' and '" + toDate.Value + "'", conn4).ExecuteReader();
                    if (reader4.Read())
                    {
                        dataGridView1.Rows.Add(" 04.05", " Cash Out - Expenses", db.setAmountFormat(reader4.GetDouble(0) + ""), "log");
                        adminExpenses = adminExpenses + reader4.GetDouble(0);
                    }
                    conn4.Close();
                }
                catch (Exception)
                {
                    conn4.Close();
                }

                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(adminExpenses + "");
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("05", "Depreciation", 0, "");
                double DEPRECIATION = 0;
                rowcount = dataGridView1.Rows.Count;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "DEPRECIATION" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        expenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //  dt.Rows.Add("Depreciation", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);

                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        adminExpenses = adminExpenses + expenses;
                        DEPRECIATION = DEPRECIATION + expenses;
                        dataGridView1.Rows.Add(" 05.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(expenses + ""), "log");
                    }
                }
                conn4.Close();

                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(DEPRECIATION + "");
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("06", "Other Expenses", 0, "");
                double OtherExpenses = 0;
                rowcount = dataGridView1.Rows.Count;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "121" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        expenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            //dt.Rows.Add("Other Expenses", reader4[1], reader.GetDouble(0), reader.GetDouble(0), 3);

                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        adminExpenses = adminExpenses + expenses;
                        OtherExpenses = OtherExpenses + expenses;
                        dataGridView1.Rows.Add(" 06.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(expenses + ""), "log");
                    }
                }
                conn4.Close();
                double expDeleted = 0.0;
                conn4.Open();
                reader4 = new SqlCommand("select reason,amount from cashsummery where remark='" + "EXPENCES-MANUAL" + "' and date between '" + fromDate.Value + "' and '" + toDate.Value + "' ", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    conn3.Open();
                    reader3 = new SqlCommand("select * from receipt where id='" + reader4.GetString(0).Split('/')[reader4.GetString(0).Split('/').Length - 1] + "'", conn3).ExecuteReader();
                    if (!reader3.Read())
                    {
                        expDeleted = expDeleted + reader4.GetDouble(1);
                    }
                    conn3.Close();
                }
                conn4.Close();
                OtherExpenses = OtherExpenses + expDeleted;
                dataGridView1.Rows.Add(" 06.02", " Other Exp(non Allocate)", db.setAmountFormat(expDeleted + ""), "log");
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(OtherExpenses + "");

                dataGridView1.Rows.Add("", "Operating Profit", db.setAmountFormat(((ssRevenue + spRevenue + investEx) - ((expensesTotal + gross + epf12 + etf3 + (debit))) - adminExpenses - OtherExpenses) + ""), "");

                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("07", "Finance Expenses", 0, "");
                double FinanceExpenses = 0;
                rowcount = dataGridView1.Rows.Count;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "122" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        expenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        adminExpenses = adminExpenses + expenses;
                        FinanceExpenses = FinanceExpenses + expenses;
                        dataGridView1.Rows.Add(" 07.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(expenses + ""), "log");
                    }
                }
                conn4.Close();
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(FinanceExpenses + "");
                dataGridView1.Rows.Add("", "Profit Before Income Tax", db.setAmountFormat(((ssRevenue + spRevenue + investEx) - ((expensesTotal + OtherExpenses + gross + epf12 + etf3 + (debit))) - adminExpenses) + ""), "");
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("08", "Income tax Expenses", 0, "");
                double IncometaxExpenses = 0;
                rowcount = dataGridView1.Rows.Count;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "EXPENSES" + "' and id='" + "123" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        expenses = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date between '" + fromDate.Value + "' and '" + toDate.Value + "' and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            expenses = expenses + reader.GetDouble(0);
                        }
                        conn.Close();
                        adminExpenses = adminExpenses + expenses;
                        IncometaxExpenses = IncometaxExpenses + expenses;
                        dataGridView1.Rows.Add(" 08.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(expenses + ""), "log");
                    }
                }
                conn4.Close();
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(FinanceExpenses + "");
                dataGridView1.Rows.Add("", "Net Profit", db.setAmountFormat(((ssRevenue + spRevenue + investEx) - ((expensesTotal + OtherExpenses + gross + epf12 + etf3 + (debit))) - adminExpenses) + ""), "");
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add("01", "Non Current Assets", 0, "");
                double NonCurrentAssets = 0;
                int rowcount = dataGridView1.Rows.Count;
                investEx = 0;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "NON CURRENT ASSETS" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date <= '" + toDate.Value + "'  and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            investEx = investEx + reader.GetDouble(0);
                        }
                        conn.Close();
                    }
                    NonCurrentAssets = NonCurrentAssets + investEx;

                    conn.Close();
                    dataGridView1.Rows.Add(" 01.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(investEx + ""), "");
                }
                conn4.Close();
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(NonCurrentAssets + "");
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("02", "Invesments", 0, "");
                double Invesments = 0;
                rowcount = dataGridView1.Rows.Count;
                conn4.Open();
                reader4 = new SqlCommand("select id,name from accounts where type='" + "INVESTMENTS" + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    {
                        investEx = 0;
                        conn.Open();
                        reader = new SqlCommand("select amount2 from receipt where date <= '" + toDate.Value + "'  and customer='" + reader4[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            investEx = investEx + reader.GetDouble(0);
                        }
                        conn.Close();

                        Invesments = Invesments + investEx;

                        conn.Close();
                        dataGridView1.Rows.Add(" 02.01", " " + reader4[0] + "." + reader4[1], db.setAmountFormat(investEx + ""), "");
                    }
                }
                conn4.Close();
                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(Invesments + "");
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("03", "Current Assets", 0, "");
                double CurrentAssetsSparePart = 0, CurrentAssetsOil = 0;
                rowcount = dataGridView1.Rows.Count;
                double credit = 0, debit = 0;
                conn4.Open();
                reader4 = new SqlCommand("select purchasingPrice,QTY,code from ITEM where isItem='" + true + "' AND sparePart='" + true + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    credit = 0;
                    debit = 0;

                    conn3.Open();
                    reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date<='" + toDate.Value + "'", conn3).ExecuteReader();
                    while (reader3.Read())
                    {
                        credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                    }
                    conn3.Close();
                    conn3.Open();
                    reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date<='" + toDate.Value + "'", conn3).ExecuteReader();
                    while (reader3.Read())
                    {
                        debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                    }
                    conn3.Close();

                    CurrentAssetsSparePart = CurrentAssetsSparePart + (credit - debit);

                    conn.Close();
                }
                conn4.Close();
                dataGridView1.Rows.Add(" 03.01", " Inventories - Spare Parts", db.setAmountFormat((CurrentAssetsSparePart) + ""), "");

                conn4.Open();
                reader4 = new SqlCommand("select purchasingPrice,QTY,code from ITEM where isItem='" + true + "' AND sparePart='" + false + "'", conn4).ExecuteReader();
                while (reader4.Read())
                {
                    credit = 0;
                    debit = 0;

                    conn3.Open();
                    reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + false + "' and itemcode='" + reader4[2] + "' and date<='" + toDate.Value + "'", conn3).ExecuteReader();
                    while (reader3.Read())
                    {
                        credit = credit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                    }
                    conn3.Close();
                    conn3.Open();
                    reader3 = new SqlCommand("select purchsingPrice,qty from itemStatement where credit='" + true + "' and itemcode='" + reader4[2] + "' and date<='" + toDate.Value + "'", conn3).ExecuteReader();
                    while (reader3.Read())
                    {
                        debit = debit + (reader3.GetDouble(0) * reader3.GetDouble(1));
                    }
                    conn3.Close();
                    CurrentAssetsOil = CurrentAssetsOil + (credit - debit);
                }
                conn4.Close();
                dataGridView1.Rows.Add(" 03.02", " Inventories - Oil & Accesorries", db.setAmountFormat((CurrentAssetsOil) + ""), "");

                double balance = 0;
                credit = 0;
                debit = 0;
                double TradeReceivables = 0;
                conn2.Open();
                reader2 = new SqlCommand("select id,company from customer order by id", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    credit = 0;
                    debit = 0;
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select sum(balance) from creditInvoiceRetail  where customerid ='" + reader2[0] + "' and date<='" + toDate.Value + "' ", conn).ExecuteReader();
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

                        reader = new SqlCommand("select sum(paid) from invoiceCreditPaid as a,invoiceretail as b  where a.date   <= '" + toDate.Value + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
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

                    TradeReceivables = TradeReceivables + (credit - debit);
                }

                conn2.Close();
                dataGridView1.Rows.Add(" 03.03", " Trade Receivables", db.setAmountFormat((TradeReceivables) + ""), "");

                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(CurrentAssetsOil + CurrentAssetsSparePart + TradeReceivables + "");

                conn2.Close();
                TradeReceivables = 0;
                dataGridView1.Rows.Add("", "", "", "");
                dataGridView1.Rows.Add("04", "Current Liabilites", 0, "");
                double TradePayble = 0;
                rowcount = dataGridView1.Rows.Count;

                conn2.Open();
                reader2 = new SqlCommand("select id,company from supplier order by id", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    credit = 0;
                    debit = 0;
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select sum(balance) from creditgrn  where customerid ='" + reader2[0] + "' and date<='" + toDate.Value + "' ", conn).ExecuteReader();
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

                        reader = new SqlCommand("select sum(paid) from grnCreditPaid as a,grn as b  where a.date   <= '" + toDate.Value + "' and b.customerid='" + reader2[0] + "' and a.invoiceID=b.id", conn).ExecuteReader();
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

                    TradePayble = TradePayble + (credit - debit);
                }

                conn2.Close();
                dataGridView1.Rows.Add(" 04.01", " Trade Payble", db.setAmountFormat((TradePayble) + ""), "");

                dataGridView1.Rows[rowcount - 1].Cells[2].Value = db.setAmountFormat(TradePayble + "");
            }
            try
            {
                //ds.Tables.Add(dt);

                //  ds.WriteXmlSchema("expenAMOS2016.xml");
                // this.Dispose();
                //     MessageBox.Show(;
                //pl pp4 = new pl();
                //pp4.SetDataSource(ds);
                // pp4.SetParameterValue("date", dateFrom.Value.ToShortDateString());
                //pp4.SetParameterValue("invest",fromDate.Value.ToShortDateString()+" - "+toDate.Value.ToShortDateString());
                //pp4.SetParameterValue("BFSTOCK", db.setAmountFormat(bfStock + ""));
                //pp4.SetParameterValue("purchasing", db.setAmountFormat(costOfPurchasing + ""));
                //pp4.SetParameterValue("totalinvest", db.setAmountFormat((costOfPurchasing + bfStock) + ""));
                //pp4.SetParameterValue("stock", db.setAmountFormat(stockValue + ""));
                //pp4.SetParameterValue("costofgoodsale", db.setAmountFormat(((costOfPurchasing + bfStock) - stockValue + "")));
                //pp4.SetParameterValue("totalsale", db.setAmountFormat(sale + ""));
                //pp4.SetParameterValue("totalexpences", db.setAmountFormat(expenses + ""));
                //pp4.SetParameterValue("netprofit", db.setAmountFormat(sale - (expenses + ((costOfPurchasing + investEx + bfStock) - stockValue) + investEx) + ""));
                //crystalReportViewer1.ReportSource = pp4;

                db.setCursoerDefault();
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                // throw;
            }
        }

        private void dateFrom_ContextMenuStripChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ArrayList monthList = new ArrayList();
            for (DateTime dts = toDate.Value; dts <= fromDate.Value; dts = dts.AddMonths(1))
            {
                monthList.Add(new ListItem(dts.ToString("yyyy/MMMM"), dts.ToString("yyyy")));
            }

            for (int i = 0; i < monthList.Count; i++)
            {
                MessageBox.Show(monthList[i] + "");
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (radioButton1.Checked)
                {
                    statement a = new statement();
                    a.Visible = true;
                    a.loadRevenue(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), fromDate.Value, toDate.Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
                else
                {
                    statement2 a = new statement2();
                    a.Visible = true;
                    a.loadRevenue(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), fromDate.Value, toDate.Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                statement3 a = new statement3();
                a.Visible = true;
                a.loadRevenue(dataGridView1, fromDate.Value, toDate.Value, "P/L");
            }
            else
            {
                statement3 a = new statement3();
                a.Visible = true;
                a.loadRevenue(dataGridView1, fromDate.Value, toDate.Value, "BALANCE SHEET");
            }
        }
    }
}