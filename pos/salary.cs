using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class salary : Form
    {
        private DB db, db2, db3, db4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private SqlConnection sqlconn, sqlconn2, sqlconn3, sqlconn4;
        private DataSet dataSet;
        private double absentDaysValue;
        private string imagePath = "", imageFullPath = "", attandanceType, userH;
        private string[] readfromAddress;
        private SqlDataAdapter dataAdapter;

        public salary(string user)
        {
            InitializeComponent();

            deduction_advancedtable.Refresh();
            userH = user;
        }

        private void salary_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            this.TopMost = true;
            // comboBoxEmployeeList.edi\\//
            nm.Text = "0.0";
            newAmountMeal.Text = "0.0";
            this.deduction_advancedtable.AllowUserToAddRows = false;
            this.timesheetTable.AllowUserToAddRows = false;

            ComboBoxYear.Format = DateTimePickerFormat.Custom;
            ComboBoxYear.CustomFormat = "yyyy";
            try
            {
                db = new DB();
                sqlconn = db.createSqlConnection();
                db2 = new DB();
                sqlconn2 = db2.createSqlConnection();
                db3 = new DB();
                sqlconn3 = db3.createSqlConnection();
                db4 = new DB();
                sqlconn4 = db4.createSqlConnection();
            }
            catch (Exception abc)
            {
                //sqlconn.Close();
                MessageBox.Show("Database Connectivity Error g" + abc.Message);
            }
            try
            {
                sqlconn.Open();

                reader = new SqlCommand("select empID,Name from emp order by empid", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    //   MessageBox.Show(reader.GetInt32(0)+"");
                    comboBoxEmployeeList.Items.Add(reader.GetInt32(0) + "-" + reader.GetString(1).ToUpper());
                }
                sqlconn.Close();
            }
            catch (Exception abc)
            {
                //sqlconn.Close();
                MessageBox.Show("Database Connectivity Error when Employee Loading to ComboBox" + abc.StackTrace);
            }
            //this.Width = 1383;

            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void searchEmployee_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (reson.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Reason cant be Empty value");
            }
            else if (amounde.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Amount cant be Empty value");
            }
            else if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Sorry, Before Updating Deduct Value You must Select Relevant YEAR / MONTH ");
            }
            else if (attandanceType.Equals("timeBasedMultiCompany"))
            {
                MessageBox.Show("Sorry, This User Multi Company Type and cant be Make Advanced from this Area");
            }
            else if (radioButtonAdvanced.Checked)
            {
                deduction_advancedtable.Rows.Add("Advanced", reson.Text, amounde.Text, Convert.ToDateTime(dateTimePicker1.Value).ToString("dd-MM-yyyy"));
                radioButtonDeduction.Checked = true;
                reson.Text = "";
                amounde.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
            else if (radioButtonDeduction.Checked)
            {
                deduction_advancedtable.Rows.Add("Deduction", reson.Text, amounde.Text, Convert.ToDateTime(dateTimePicker1.Value).ToString("dd-MM-yyyy"));
                radioButtonDeduction.Checked = true;
                reson.Text = "";
                amounde.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Please Select type as Deduction or Advanced");
            }
        }

        private void amounde_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void amounde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void deduction_advancedtable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void deduction_advancedtable_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!(deduction_advancedtable.RowCount == 0))
            //    {
            //        var index=deduction_advancedtable.SelectedRows[0].Index;
            //        if (deduction_advancedtable.Rows[index].Cells[0].Value.ToString().Equals("Advanced"))
            //        {
            //            radioButtonAdvanced.Checked = true;
            //        }
            //        else
            //        {
            //            radioButtonDeduction.Checked = true;
            //        }

            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Sorry , You havnet selected any Row to Update");
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(deduction_advancedtable.RowCount == 0))
                {
                    var index = deduction_advancedtable.SelectedRows[0].Index;

                    deduction_advancedtable.Rows.RemoveAt(index);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry , You havnet selected any Row to Delete");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxEmployeeList.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Employee");
                }
                else if ((MessageBox.Show("Are you Sure Save these Deduct Values for Select User", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    sqlconn.Open();
                    new SqlCommand("delete from deduct where month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "' and id='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "'", sqlconn).ExecuteNonQuery();
                    for (int i = 0; i < deduction_advancedtable.RowCount; i++)
                    {
                        new SqlCommand("insert into deduct values('" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "','" + deduction_advancedtable.Rows[i].Cells[0].Value + "','" + deduction_advancedtable.Rows[i].Cells[1].Value + "','" + deduction_advancedtable.Rows[i].Cells[2].Value + "','" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "','" + DateTime.Now + "','" + deduction_advancedtable.Rows[i].Cells[3].Value.ToString().Split('-')[1] + "-" + deduction_advancedtable.Rows[i].Cells[3].Value.ToString().Split('-')[0] + "-" + deduction_advancedtable.Rows[i].Cells[3].Value.ToString().Split('-')[2] + "')", sqlconn).ExecuteNonQuery();
                    }

                    sqlconn.Close();
                    MessageBox.Show("Succefully Upated Deduct values");
                    listBox2_Click(sender, e);
                }
            }
            catch (Exception abc)
            {
                sqlconn.Close();
                MessageBox.Show("Internal Error from Saving Deduction / Advanced" + "/" + abc.Message + "/" + abc.StackTrace);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem);
        }

        private Int32 annual = 0, casual = 0, leave = 0;

        private void listBox2_Click(object sender, EventArgs e)
        {
            leave = 0;

            try
            {
                db = new DB();
                Cursor.Current = Cursors.WaitCursor;

                if (comboBoxEmployeeList.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Employee First and try agian");
                    listBox2.SelectedIndex = -1;
                }
                else
                {
                    if (listBox2.SelectedIndex != -1)
                    {
                        loadTimeSheet();
                        MessageBox.Show("User History Downloaded");
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception abc)
            {
                listBox2.SelectedIndex = -1;
                MessageBox.Show("Error. Please Dont contunie, Conatct Administartor" + abc.StackTrace);
            }
        }

        private double workingDaysValue = 0;

        private double epfV, epfV_, etfV;

        private void getDateList(string empId, string month, string year, bool oneToEnd, bool pastToPresent)
        {
            try
            {
                dateList = new ArrayList();

                if (oneToEnd)
                {
                    //  MessageBox.Show("sa1sasa");
                    for (int i = 1; i <= Int32.Parse(db.getLastDate(Int32.Parse(month), Int32.Parse(year))); i++)
                    {
                        dateList.Add(year + "-" + month + "-" + i);
                    }
                }
                else if (pastToPresent)
                {
                    // MessageBox.Show("sa2sasa");
                    tempMonth = new DateTime(Int32.Parse(year), Int32.Parse(month), 1).AddMonths(-1).Month;

                    for (int i = 21; i <= Int32.Parse(db.getLastDate(tempMonth, Int32.Parse(year))); i++)
                    {
                        dateList.Add(year + "-" + tempMonth + "-" + i);
                    }
                    for (int i = 1; i <= 20; i++)
                    {
                        dateList.Add(year + "-" + month + "-" + i);
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }

        private bool pastToPresent, oneToEnd;
        private int tempMonth;
        private string empid;
        private ArrayList dateList;

        private void loadTimeSheet()
        {
            sqlconn.Open();
            reader = new SqlCommand("select * from deduct where month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "' and id='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteReader();
            deduction_advancedtable.Rows.Clear();
            while (reader.Read())
            {
                deduction_advancedtable.Rows.Add(reader[1], reader[2], reader[3], Convert.ToDateTime(reader.GetDateTime(6)).ToString("dd-MM-yyyy"));
            }
            sqlconn.Close();

            sqlconn.Open();
            reader = new SqlCommand("select * from earning where month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "' and id='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteReader();
            earningTable.Rows.Clear();
            while (reader.Read())
            {
                earningTable.Rows.Add(reader[1], reader[2], Convert.ToDateTime(reader.GetDateTime(4)).ToString("dd-MM-yyyy"));
            }
            sqlconn.Close();

            sqlconn.Open();
            reader = new SqlCommand("select epfBasic,type from emp where  empid='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteReader();

            if (reader.Read())
            {
                basicEpf = reader.GetDouble(0);
                attandanceType = reader[1].ToString();
            }
            sqlconn.Close();
            sqlconn.Open();
            reader = new SqlCommand("select amount from meal where  empid='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "' and month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "'", sqlconn).ExecuteReader();

            if (reader.Read())
            {
                //    MessageBox.Show("sa " + reader.GetDouble(0));
                nm.Text = setAmountFormat(reader.GetDouble(0) + "");
            }

            sqlconn.Close();

            getDateList("", listBox2.SelectedIndex + 1 + "", ComboBoxYear.Value.Year + "", true, false);
            empid = comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0];
            timesheetTable.Rows.Clear();
            for (int i = 0; i < dateList.Count; i++)
            {
                sqlconn.Open();
                reader = new SqlCommand("select * from attendance where empid='" + empid + "' and date='" + dateList[i] + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    timesheetTable.Rows.Add(dateList[i] + " " + DateTime.Parse(dateList[i] + "").DayOfWeek, reader.GetBoolean(2));
                }
                else
                {
                    timesheetTable.Rows.Add(dateList[i] + " " + DateTime.Parse(dateList[i] + "").DayOfWeek, false);
                }
                sqlconn.Close();
            }

            basic.Text = "";
            workingDays.Text = "";

            // absentDays.Text = "";
            fixedAllownces.Text = "";
            ot.Text = "";
            fixedDeduct.Text = "";
            advanced.Text = "";

            //   offDayDeduct.Text = "";
            totalAllownces.Text = "";
            totalDeduction.Text = "";
            netSalary.Text = "";
            epf_8.Text = "";
            epf_12.Text = "";
            etf_3.Text = "";
            //extraWorkingDays.Text = "";
            extraWorkingDayPay.Text = "";
            allowances.Text = "";
            mealValue.Text = "";

            epfV = 0;
            epfV_ = 0;
            etfV = 0;
            //  nm.Text = "0.0";
            newAmountMeal.Text = "0.0";
            sqlconn.Open();
            reader = new SqlCommand("select * from paysheet where empid='" + empid + "'  and month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "'", sqlconn).ExecuteReader();
            if (reader.Read())
            {
                workingDays.Text = reader.GetInt32(4) + "";
                basic.Text = reader.GetDouble(3) + "";
                fixedDeduct.Text = reader.GetDouble(22) + "";
                advanced.Text = reader.GetDouble(9) + "";
                mealValue.Text = reader.GetDouble(16) + "";
                totalAllownces.Text = reader.GetDouble(23) + "";
                totalDeduction.Text = reader.GetDouble(24) + "";
                netSalary.Text = reader.GetDouble(25) + "";
                epf_8.Text = reader.GetDouble(18) + "";
                epf_12.Text = reader.GetDouble(19) + "";

                etf_3.Text = reader.GetDouble(20) + "";
            }
            sqlconn.Close();

            dataGridView1.Rows.Clear();
            var year = ComboBoxYear.Value.ToString("d").ToString().Split('/')[2];
            var month = listBox2.SelectedItem.ToString();

            sqlconn.Open();
            reader = new SqlCommand("select balance from empPay where empid='" + empid + "' and resaon='" + "B/F Balance" + "' and date between '" + dateList[0] + "' and '" + dateList[dateList.Count - 1] + "'", sqlconn).ExecuteReader();
            if (reader.Read())
            {
                balancePay = reader.GetDouble(0);
                dataGridView1.Rows.Add("B/F Balance", "", "", setAmountFormat(balancePay + ""));
            }
            sqlconn.Close();

            sqlconn.Open();
            reader = new SqlCommand("select * from empPay where empid='" + empid + "' and resaon!='" + "B/F Balance" + "' and date between '" + dateList[0] + "' and '" + dateList[dateList.Count - 1] + "'", sqlconn).ExecuteReader();
            while (reader.Read())
            {
                balancePay = balancePay + (reader.GetDouble(3)) - reader.GetDouble(4);
                dataGridView1.Rows.Add(reader[2], setAmountFormat(reader[3] + ""), setAmountFormat(reader[4] + ""), setAmountFormat(balancePay + ""));
            }
            sqlconn.Close();
        }

        private double balancePay;

        private string setAmountFormat(string amount)
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

        private double basicSalary, otRate, otRate2, lateRate, lateRate2, offDayRate, offDayRate2;
        private string workingDaysSeconds, workingDaysSeconds2, otSeconds, otSeconds2, lateSeconds, lateSeconds2;
        private int workingDaysDB, workingDaysDB2;
        private bool otpay, otpay2, latededu, latededu2, lateBalance, lateBalance2;

        private void clearValue()
        {
            mealValue.Text = "";
            nm.Text = "0.0";
            newAmountMeal.Text = "0.0";
            earningTable.Rows.Clear();
            earningResoen.Text = "";
            earningResoen.Text = "";
            earningAmount.Text = "";
            earningDate.Value = DateTime.Now;

            basic.Text = "";

            //  absentDays.Text = "";
            fixedAllownces.Text = "";
            ot.Text = "";
            fixedDeduct.Text = "";
            advanced.Text = "";

            //  offDayDeduct.Text = "";
            totalAllownces.Text = "";
            totalDeduction.Text = "";
            netSalary.Text = "";
            epf_8.Text = "";
            epf_12.Text = "";
            etf_3.Text = "";
            // extraWorkingDays.Text = "";
            extraWorkingDayPay.Text = "";

            //  processDate2.Text = "";
            deduction_advancedtable.Rows.Clear();
            listBox2.SelectedIndex = -1;
            reson.Text = "";
            amounde.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            timesheetTable.Rows.Clear();
        }

        public List<DateTime> AllDatesInAMonth(int month, int year)
        {
            var firstOftargetMonth = new DateTime(year, month, 1);
            var firstOfNextMonth = firstOftargetMonth.AddMonths(1);

            var allDates = new List<DateTime>();

            for (DateTime date = firstOftargetMonth; date < firstOfNextMonth; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void comboBoxEmployeeList_DropDown(object sender, EventArgs e)
        {
            clearValue();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_TabStopChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
        }

        private void tabControl1_ChangeUICues(object sender, UICuesEventArgs e)
        {
        }

        private void tabControl1_DockChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (var item in AllDatesInAMonth(1, 2013))
            {
                MessageBox.Show(item + "");
            }
        }

        private void comboBoxEmployeeList_KeyPress(object sender, KeyPressEventArgs e)
        {
            clearValue();
        }

        private void ComboBoxYear_DropDown(object sender, EventArgs e)
        {
            clearValue();
        }

        private void ComboBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            clearValue();
        }

        private void timesheetTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private ArrayList dates = new ArrayList();

        private void button4_Click_1(object sender, EventArgs e)
        {
        }

        private string a = "b";

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private bool states, states2;
        private companySalary companySalary;

        private bool checkTimesheetValue(string time)
        {
            db = new DB();
            sqlconn2 = db.createSqlConnection();
            states = true;
            try
            {
                using (sqlconn2)
                {
                    new SqlCommand("insert into checkTime values('" + time + "')", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                }
            }
            catch (Exception q)
            {
                sqlconn2.Close();
                states = false;
                states2 = false;
                // MessageBox.Show(q.Message);
            }
            return states;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //        try
            //        {
            //            if (comboBoxEmployeeList.SelectedIndex == -1)
            //            {
            //                MessageBox.Show("Please Select Employee");
            //            }
            //            else if (listBox2.SelectedIndex == -1)
            //            {
            //                MessageBox.Show("Please Select Process Period");
            //            }
            //            else if ((MessageBox.Show("Manual Process will be Delete all Manual Time sheet Ajesment ,Are you sure Contunie ?", "Confirmation",
            //MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            //            {
            //                sqlconn.Open();
            //                reader = new SqlCommand("select * from paysheet where month='" + ComboBoxYear.Value.ToString("d").Split('/')[2] + "/" + listBox2.SelectedItem.ToString() + "'", sqlconn).ExecuteReader();
            //                if (reader.Read())
            //                {
            //                    sqlconn.Close();
            //                    process(comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0].ToString(), listBox2.SelectedItem.ToString(), ComboBoxYear.Value.ToString("d").Split('/')[2], true);
            //                    loadTimeSheet();

            //                    MessageBox.Show("Process Completed Succesfully " + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + " " + (listBox2.SelectedIndex + 1).ToString() + " " + ComboBoxYear.Value.ToString("d").Split('/')[2] + " ");

            //                }
            //                else
            //                {
            //                    sqlconn.Close();
            //                    process(comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0].ToString(), listBox2.SelectedItem.ToString(), ComboBoxYear.Value.ToString("d").Split('/')[2], true);
            //                    loadTimeSheet();

            //                    MessageBox.Show("Process Completed Succesfully " + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + " " + (listBox2.SelectedIndex + 1).ToString() + " " + ComboBoxYear.Value.ToString("d").Split('/')[2] + " ");

            //                }
            //                reader.Close();
            //                sqlconn.Close();
            //                //   MessageBox.Show("hu2");
            //            }

            //        }
            //        catch (Exception ass)
            //        {
            //            MessageBox.Show(ass.StackTrace + "12 " + ass.Message);
            //        }
        }

        private string getMOnth(string y)
        {
            string month = "";
            if (y.Equals("January"))
            {
                month = "01";
            }
            else if (y.Equals("February"))
            {
                month = "02";
            }
            else if (y.Equals("March"))
            {
                month = "03";
            }
            else if (y.Equals("April"))
            {
                month = "04";
            }
            else if (y.Equals("May"))
            {
                month = "05";
            }
            else if (y.Equals("June"))
            {
                month = "06";
            }
            else if (y.Equals("July"))
            {
                month = "07";
            }
            else if (y.Equals("August"))
            {
                month = "08";
            }
            else if (y.Equals("September"))
            {
                month = "09";
            }
            if (y.Equals("October"))
            {
                month = "10";
            }
            if (y.Equals("November"))
            {
                month = "11";
            }
            if (y.Equals("December"))
            {
                month = "12";
            }

            return month;
        }

        private string lastDate;

        public string getLastDate(int month, int year)
        {
            var firstOftargetMonth = new DateTime(year, month, 1);
            var firstOfNextMonth = firstOftargetMonth.AddMonths(1);

            var allDates = new List<DateTime>();

            for (DateTime date = firstOftargetMonth; date < firstOfNextMonth; date = date.AddDays(1))
            {
                allDates.Add(date);
            }
            lastDate = allDates[allDates.Count - 1].ToString().Split(' ')[0].ToString().Split('/')[1];
            return lastDate;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //        try
            //        {
            //            if (comboBoxEmployeeList.SelectedIndex == -1)
            //            {
            //                MessageBox.Show("Please Select Employee");
            //            }
            //            else if (listBox2.SelectedIndex == -1)
            //            {
            //                MessageBox.Show("Please Select Process Period");
            //            }
            //            else if ((MessageBox.Show("Manual Process will be Delete all Manual Time sheet Ajesment ,Are you sure Contunie ?", "Confirmation",
            //MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            //MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            //            {
            //                process(comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0].ToString(), listBox2.SelectedItem.ToString(), ComboBoxYear.Value.ToString("d").Split('/')[2], true);
            //                loadTimeSheet();

            //                MessageBox.Show("Process Completed Succesfully " + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + " " + (listBox2.SelectedIndex + 1).ToString() + " " + ComboBoxYear.Value.ToString("d").Split('/')[2] + " ");
            //            }

            //        }
            //        catch (Exception ass)
            //        {
            //            MessageBox.Show(ass.StackTrace + "12");
            //        }
        }

        private String workingDaysL, absentDaysL, fixedAllowncesL, otL, fixedDeductionL, advancedL, lateL, offDayL;

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        public static int diffMonths(DateTime start, DateTime end)
        {
            if (start > end)
            {
                var swapper = start;
                start = end;
                end = swapper;
            }

            switch (end.Year - start.Year)
            {
                case 0: // Same year
                    return end.Month - start.Month;

                case 1: // last year
                    return (12 - start.Month) + end.Month;

                default:
                    return 12 * (3 - (end.Year - start.Year)) + (12 - start.Month) + end.Month;
            }
        }

        private Int32 dayOffCount;
        private double basicEpf, bankingAmount, epf, etf, epf_, basicSalary2, advanced1, advanced2, basicSalary_, basicSalary2_, loan, offDayDeductValue2, absentDaysValue2, workingDaysValue2, earning, earning2, meal, dayOff, dayOffAmount;
        private Boolean otEpfPay;
        private string late1, late2, ot1, ot2, offDay1, offDay2, extraDay1, extraDay2, workDay1, workDay2, extraDayPay1, extraDayPay2, offDayPay1, offDayPay2;

        private Int32 workingdaysGover, count, maxOTHours;
        private TimeSpan workHours, lateMin, OtMIn, otEpfPayTime;
        private bool sndayOT;
        private Double otepfPayAmount, grossSalary, totalDedution, netSalaryAmOunt, salaryForepf, Meal, Bounes, balance, attendanceAllownce;

        private void label19_Click_1(object sender, EventArgs e)
        {
        }

        private string extraWorkDaysL, extraWorkingDayspayL;

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // MessageBox.Show( + "");
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Manual Process will be Delete all Manual Time sheet Ajesment ,Are you sure Contunie ?", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    new processAllDateSelect().Show();
                }
            }
            catch (Exception ass)
            {
                MessageBox.Show(ass.Message + "12");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Manual Process will be Delete all Manual Time sheet Ajesment ,Are you sure Contunie ?", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    new processAllDateSelect().Show();
                }
            }
            catch (Exception ass)
            {
                MessageBox.Show("Please Select MOnths " + ass.Message + "/" + ass.StackTrace);
            }
        }

        private string[] id;

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBoxEmployeeList.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Employee");
            }
            else if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Process Period");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;

                Cursor.Current = Cursors.Default;
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            new UserSelect().Show();
        }

        private string[] IDarray;

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (comboBoxEmployeeList.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Employee");
            }
            else if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Process Period");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                PaySheetOffice paySheetOffice = new PaySheetOffice();
                paySheetOffice.setValue(comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0], "single", ComboBoxYear.Value.ToString("d").Split('/')[2] + "/" + listBox2.SelectedItem.ToString(), new string[0]);
                paySheetOffice.Show();
                Cursor.Current = Cursors.Default;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new PaySheetOffice().Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (earningResoen.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Reason cant be Empty value");
            }
            else if (earningAmount.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Amount cant be Empty value");
            }
            else if (attandanceType.Equals("timeBasedMultiCompany"))
            {
                MessageBox.Show("Sorry, This User Multi Company Type and cant be Make Earing from this Area");
            }
            else if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Sorry, Before Updating Deduct Value You must Select Relevant YEAR / MONTH ");
            }
            else
            {
                earningTable.Rows.Add(earningResoen.Text, earningAmount.Text, Convert.ToDateTime(earningDate.Value).ToString("dd-MM-yyyy"));
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(earningTable.RowCount == 0))
                {
                    var index = earningTable.SelectedRows[0].Index;

                    earningTable.Rows.RemoveAt(index);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry , You havnet selected any Row to Delete");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxEmployeeList.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Employee");
                }
                else if ((MessageBox.Show("Are you Sure Save these Deduct Values for Select User", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    using (sqlconn = db.createSqlConnection())
                    {
                        new SqlCommand("delete from earning where month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "' and id='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteNonQuery();
                        for (int i = 0; i < earningTable.RowCount; i++)
                        {
                            new SqlCommand("insert into earning values('" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "','" + earningTable.Rows[i].Cells[0].Value + "','" + Double.Parse(earningTable.Rows[i].Cells[1].Value.ToString()) + "','" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "','" + earningTable.Rows[i].Cells[2].Value.ToString().Split('-')[1] + "-" + earningTable.Rows[i].Cells[2].Value.ToString().Split('-')[0] + "-" + earningTable.Rows[i].Cells[2].Value.ToString().Split('-')[2] + "')", sqlconn).ExecuteNonQuery();
                        }

                        sqlconn.Close();
                        MessageBox.Show("Succefully Upated Allowances");
                        listBox2_Click(sender, e);
                    }
                }
            }
            catch (Exception abc)
            {
                sqlconn.Close();
                MessageBox.Show("Internal Error from Saving Earning " + abc.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (newAmountMeal.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Amount cant be Empty value");
            }
            else if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Sorry, Before Updating Deduct Value You must Select Relevant YEAR / MONTH ");
            }
            else
            {
                using (sqlconn = db.createSqlConnection())
                {
                    new SqlCommand("delete from meal where month='" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "' and empid='" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "'", sqlconn).ExecuteNonQuery();

                    new SqlCommand("insert into meal values('" + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + "','" + newAmountMeal.Text + "','" + ComboBoxYear.Value.ToString("d").ToString().Split('/')[2] + "/" + listBox2.SelectedItem + "')", sqlconn).ExecuteNonQuery();

                    sqlconn.Close();
                    MessageBox.Show("Succefully Upated Meal Value");
                    listBox2_Click(sender, e);
                }
            }
        }

        private void newAmountMeal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private Int32 PERIOD;

        private void button20_Click(object sender, EventArgs e)
        {
        }

        private void button19_Click(object sender, EventArgs e)
        {
        }

        private void loanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void noOfInstallment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
        }

        private void button21_Click(object sender, EventArgs e)
        {
        }

        private void button22_Click(object sender, EventArgs e)
        {
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
        }

        private void button23_Click_1(object sender, EventArgs e)
        {
        }

        private void amountMultiCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
        }

        private void reson_TextChanged(object sender, EventArgs e)
        {
        }

        private void salary_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button24_Click(object sender, EventArgs e)
        {
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
        }

        private void earningTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button27_Click(object sender, EventArgs e)
        {
        }

        private void button26_Click(object sender, EventArgs e)
        {
        }

        private void button25_Click(object sender, EventArgs e)
        {
        }

        private void button28_Click(object sender, EventArgs e)
        {
        }

        private void button28_Click_1(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    count = 0;
                    db = new DB();
                    sqlconn = db.createSqlConnection();
                    db2 = new DB();
                    sqlconn2 = db2.createSqlConnection();
                    db.setCursoerWait();
                    sqlconn.Close();
                    sqlconn.Open();
                    sqlconn2.Close();
                    sqlconn2.Open();
                    reader = new SqlCommand("select *from timeSheetGovermentBak ", sqlconn).ExecuteReader();
                    new SqlCommand("delete from timeSheetGoverment", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    sqlconn2.Open();
                    while (reader.Read())
                    {
                        count++;
                        new SqlCommand("insert into timeSheetGoverment values ('" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "','" + reader[10] + "','" + reader[11] + "','" + reader[12] + "')", sqlconn2).ExecuteNonQuery();
                    }
                    reader.Close();
                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    MessageBox.Show(count + "");
                }
            }
            catch (Exception)
            {
            }
        }

        private void listBox2_ChangeUICues(object sender, UICuesEventArgs e)
        {
        }

        private void label41_Click(object sender, EventArgs e)
        {
        }

        private void pAYSHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PaySheetOffice().Show();
        }

        private void pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            //try
            //{
            //    sqlconn.Open();
            //    new SqlCommand("insert into empPay values('" + empid + "','" + dateTimePicker2.Value + "','" + dateTimePicker2.Value + "Cash Paid" + "','" + 0 + "','" + pay.Text + "','" + 0 + "')", sqlconn).ExecuteNonQuery();
            //    sqlconn.Close();
            //    process(empid, listBox2.SelectedItem.ToString(), ComboBoxYear.Value.ToString("d").Split('/')[2], true);

            //    loadTimeSheet();
            //    dateTimePicker2.Value = DateTime.Now;
            //    pay.Text = "";
            //    MessageBox.Show("PAID");
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message + "/" + a.StackTrace);
            //    sqlconn.Close();
            //}
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void pROCESSALLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                int month = comboBox1.SelectedIndex;
                month++;
                DateTime date2 = DateTime.Now;
                DateTime date1 = Convert.ToDateTime(month + "/01" + "/" + ComboBoxYear.Value.Year);
                int disf = ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
                if (disf <= -3)
                {
                    MessageBox.Show("Sorry, You Dont Have Permisssion");
                }
                else
                {
                    sqlconn.Open();
                    reader = new SqlCommand("select * from salarylock where month='" + ComboBoxYear.Value.Year + "/" + comboBox1.SelectedItem.ToString() + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Sorry, Selected Period Locked");
                    }
                    else
                    {
                        sqlconn.Close();
                        sqlconn2.Open();
                        reader2 = new SqlCommand("select empid from emp where resgin='" + false + "'", sqlconn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            new process().saveSummery2(reader2[0] + "", comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader, paySalary.Text, payAdvanced.Text, db, db3, sqlconn3, reader3, db4, sqlconn4, reader4);
                            //  getSalaru(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString());
                        }
                        sqlconn2.Close();
                        MessageBox.Show("Save Succefully");
                    }
                }
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            //try
            //{
            //    sqlconn.Open();
            //    new SqlCommand("delete from attendance where empid='" + empid + "' and date between '" + dateList[0] + "' and '" + dateList[dateList.Count - 1] + "'", sqlconn).ExecuteNonQuery();
            //    sqlconn.Close();

            //    for (int i = 0; i < timesheetTable.Rows.Count; i++)
            //    {
            //        sqlconn.Open();
            //        new SqlCommand("insert into attendance values('" + empid + "','" + timesheetTable.Rows[i].Cells[0].Value.ToString().Split(' ')[0] + "','" + timesheetTable.Rows[i].Cells[1].Value + "')", sqlconn).ExecuteNonQuery();
            //        sqlconn.Close();
            //    }
            //    process(empid, listBox2.SelectedItem.ToString(), ComboBoxYear.Value.ToString("d").Split('/')[2], true);
            //    loadTimeSheet();
            //    MessageBox.Show("Process Completed Succesfully " + comboBoxEmployeeList.SelectedItem.ToString().Split('-')[0] + " " + (listBox2.SelectedIndex + 1).ToString() + " " + ComboBoxYear.Value.ToString("d").Split('/')[2] + " ");

            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message + "/" + a.StackTrace);
            //    sqlconn.Close();
            //}
        }

        private string tempCustomer;

        public Boolean loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                sqlconn.Open();
                reader = new SqlCommand("select name from emp where empid='" + id + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("12");
                    states = true;
                    //  codeC = id;
                    companyC.Text = reader.GetString(0);

                    tempCustomer = id + "";
                    sqlconn.Close();
                    getSalaru(id, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString());
                }
                else
                {
                    //MessageBox.Show("1222"+id);
                    states = false;
                    tempCustomer = "";
                }
                reader.Close();
                sqlconn.Close();
                db.setCursoerDefault();
            }
            catch (Exception)
            {
                sqlconn.Close();
            }
            return states;
        }

        private void companyC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                if (companyC.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    companyC.Focus();
                }
                else
                {
                    loadCustomer(companyC.Text);
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox3.Visible)
                    {
                        listBox3.Focus();
                        listBox3.SelectedIndex = 0;
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void companyC_KeyUp(object sender, KeyEventArgs e)
        {
            tempCustomer = "";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyC.Text.Equals("")))
            {
                db.setList(listBox3, companyC, companyC.Width);

                try
                {
                    listBox3.Items.Clear();
                    sqlconn.Open();
                    reader = new SqlCommand("select empid,name from emp where name like '%" + companyC.Text + "%' and resgin='" + false + "'", sqlconn).ExecuteReader();

                    while (reader.Read())
                    {
                        listBox3.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox3.Visible = true;
                    }
                    reader.Close();
                    sqlconn.Close();
                }
                catch (Exception a)
                {//
                    MessageBox.Show(a.Message);
                    sqlconn.Close();
                }
            }
            if (companyC.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox3.Visible = false;
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox3.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyC.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                loadCustomer(listBox3.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            listBox3.Visible = false;

            loadCustomer(listBox3.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyC.Text = listBox3.SelectedItem.ToString().Split(' ')[1];
        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            sqlconn.Open();
            reader = new SqlCommand("select * from salarylock where month='" + ComboBoxYear.Value.Year + "/" + comboBox1.SelectedItem.ToString() + "'", sqlconn).ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Sorry, Selected Period Locked");
            }
            else
            {
                sqlconn.Close();
                sqlconn.Close();
                if (tempCustomer.Equals(""))
                {
                    MessageBox.Show("Please Select Staff Member");
                    companyC.Focus();
                }
                else
                {
                    int month = comboBox1.SelectedIndex;
                    month++;
                    DateTime date2 = DateTime.Now;
                    DateTime date1 = Convert.ToDateTime(month + "/01" + "/" + ComboBoxYear.Value.Year);
                    int disf = ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
                    if (disf <= -3)
                    {
                        MessageBox.Show("Sorry, You Dont Have Permisssion");
                    }
                    else
                    {
                        new process().saveSummery(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader, paySalary.Text, payAdvanced.Text, db, db2, sqlconn2, reader2, db3, sqlconn3, reader3);
                        getSalaru(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString());
                    }
                }
            }
        }

        private void getSalaru(string id, string month, string year)
        {
            try
            {
                balance = 0;
                dataGridView3.Rows.Clear();
                dataGridView5.Rows.Clear();
                sqlconn.Open();
                reader = new SqlCommand("select * from paysheet where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    balance = reader.GetDouble(12);
                    dataGridView3.Rows.Add("B/F", "", "", db.setAmountFormat(reader[12] + ""), db.setAmountFormat(balance + ""));
                    dataGridView3.Rows.Add("", "", "", "", "");

                    dataGridView3.Rows.Add("BASIC SALARY", "", "", db.setAmountFormat(reader[5] + ""), "");
                    dataGridView3.Rows.Add("BANKING AMOUNT", "", "", db.setAmountFormat(reader[6] + ""), "");
                    dataGridView3.Rows.Add("ALLOWANCES", "", "", db.setAmountFormat(reader[7] + ""), "");
                    dataGridView3.Rows.Add("MEAL", "", "", db.setAmountFormat(reader[8] + ""), "");
                    dataGridView3.Rows.Add("INCENTIVE", "", "", db.setAmountFormat(reader[9] + ""), "");
                    balance = balance + reader.GetDouble(10);
                    dataGridView3.Rows.Add("GROSS SALARY", "", "", db.setAmountFormat(reader[10] + ""), db.setAmountFormat(balance + ""));
                    balance = balance + reader.GetDouble(11);
                    dataGridView3.Rows.Add("BONUS", "", "", db.setAmountFormat(reader[11] + ""), db.setAmountFormat(balance + ""));
                    balance = balance + reader.GetDouble(22);
                    dataGridView3.Rows.Add("COMMIS", "", "", db.setAmountFormat(reader[22] + ""), db.setAmountFormat(balance + ""));

                    dataGridView3.Rows.Add("EPF 8%", "", db.setAmountFormat(reader[14] + ""), "", "");
                    dataGridView3.Rows.Add("PUNISH", "", db.setAmountFormat(reader[15] + ""), "", "");
                    dataGridView3.Rows.Add("LOAN", "", db.setAmountFormat(reader[16] + ""), "", "");
                    dataGridView3.Rows.Add("ADVANCED", "", db.setAmountFormat(reader[17] + ""), "", "");
                    balance = balance - reader.GetDouble(18);
                    dataGridView3.Rows.Add("TOTAL DEDUCTION", "", db.setAmountFormat(reader[18] + ""), "", db.setAmountFormat(balance + ""));

                    dataGridView3.Rows.Add("NET SALARY", "", "", "", db.setAmountFormat(reader[19] + ""));
                    dataGridView3.Rows.Add("PAY", "", db.setAmountFormat(reader[20] + ""), "", db.setAmountFormat(reader[21] + ""));
                    dataGridView3.Rows.Add("BALANCE TO NEXT MONTH", "", "", "", db.setAmountFormat(reader[21] + ""));
                }
                else
                {
                    MessageBox.Show("Sorry Selected Employee Has been not Process Salary.");
                }

                sqlconn.Close();

                dataGridView4.Rows.Clear();
                sqlconn.Open();
                reader = new SqlCommand("select * from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' order by date", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView4.Rows.Add(reader.GetDateTime(1).ToShortDateString(), reader[2], reader[3]);
                }
                sqlconn.Close();
                sqlconn.Open();
                reader = new SqlCommand("select * from advancedEmp where empid='" + id + "' and MONTH = '" + year + "/" + month + "' order by date", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView5.Rows.Add(reader.GetDateTime(2).ToShortDateString(), db.setAmountFormat(reader[1] + ""));
                }
                sqlconn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (tempCustomer.Equals(""))
            {
                MessageBox.Show("Please Select Staff Member");
                companyC.Focus();
            }
            else
            {
                try
                {
                    var tempID = 0;

                    try
                    {
                        sqlconn.Open();
                        reader = new SqlCommand("select max(id) from receipt ", sqlconn).ExecuteReader();
                        if (reader.Read())
                        {
                            tempID = reader.GetInt32(0);
                            tempID++;
                        }
                        sqlconn.Close();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        tempID = 1;
                    }
                    //   MessageBox.Show(tempID);
                    sqlconn.Open();
                    new SqlCommand("insert into receipt values('" + tempID + "','" + DateTime.Now + "','" + "EXPENCES" + "','" + "" + "','" + new amountByName().setAmountName(paySalary.Text) + "','" + paySalary.Text + "','" + "ADVANCE-" + companyC.Text + "','" + "" + "','" + "" + "','" + userH + "','" + DateTime.Now + "')", sqlconn).ExecuteReader();
                    sqlconn.Close();

                    sqlconn.Open();
                    new SqlCommand("insert into cashSummery values('" + companyC.Text + "/" + tempID + "','" + "EXPENCES-MANUAL" + "','" + Double.Parse(paySalary.Text) + "','" + DateTime.Now + "','" + userH + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();
                    // new invoicePrint().setprintReceiprt(tempID + "", conn2, reader, userH);

                    sqlconn.Open();
                    new SqlCommand("insert into salaryPay values ('" + tempCustomer + "','" + paySalary.Text + "','" + DateTime.Now + "','" + ComboBoxYear.Value.Year.ToString() + "/" + comboBox1.SelectedItem.ToString() + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();

                    //  MessageBox.Show("Saved");
                }
                catch (Exception a)
                {
                    sqlconn.Close();
                    MessageBox.Show("try again");
                }

                new process().saveSummery(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader, paySalary.Text, paySalary.Text, db, db2, sqlconn2, reader2, db3, sqlconn3, reader3);
                getSalaru(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString());
                db.setCashBalance(DateTime.Now);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (tempCustomer.Equals(""))
            {
                MessageBox.Show("Please Select Staff Member");
                companyC.Focus();
            }
            else
            {
                try
                {
                    var tempID = 0;

                    try
                    {
                        sqlconn.Open();
                        reader = new SqlCommand("select max(id) from receipt ", sqlconn).ExecuteReader();
                        if (reader.Read())
                        {
                            tempID = reader.GetInt32(0);
                            tempID++;
                        }
                        sqlconn.Close();
                    }
                    catch (Exception a)
                    {
                        //  MessageBox.Show(a.Message);
                        tempID = 1;
                        sqlconn.Close();
                    }
                    //   MessageBox.Show(tempID);
                    sqlconn.Open();
                    new SqlCommand("insert into receipt values('" + tempID + "','" + DateTime.Now + "','" + "EXPENCES" + "','" + "" + "','" + new amountByName().setAmountName(payAdvanced.Text) + "','" + payAdvanced.Text + "','" + "ADVANCE-" + companyC.Text + "','" + "" + "','" + "" + "','" + userH + "','" + DateTime.Now + "')", sqlconn).ExecuteReader();
                    sqlconn.Close();

                    sqlconn2.Open();
                    new SqlCommand("insert into cashSummery values('" + companyC.Text + "/" + tempID + "','" + "EXPENCES-MANUAL" + "','" + Double.Parse(payAdvanced.Text) + "','" + DateTime.Now + "','" + userH + "')", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    // new invoicePrint().setprintReceiprt(tempID + "", conn2, reader, userH);

                    //  MessageBox.Show("Saved");
                }
                catch (Exception a)
                {
                    sqlconn.Close();
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }

                sqlconn.Open();
                new SqlCommand("insert into advancedEmp values ('" + tempCustomer + "','" + payAdvanced.Text + "','" + DateTime.Now + "','" + ComboBoxYear.Value.Year.ToString() + "/" + comboBox1.SelectedItem.ToString() + "')", sqlconn).ExecuteNonQuery();
                sqlconn.Close();
                new process().saveSummery(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader, paySalary.Text, payAdvanced.Text, db, db2, sqlconn2, reader2, db3, sqlconn3, reader3);
                getSalaru(tempCustomer, comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString());
                db.setCashBalance(DateTime.Now);
                MessageBox.Show("Saved");
            }
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            new advancedSerach(this, "").Visible = true;
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            try
            {
                new invoicePrint().setprintSalary(comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader);
            }
            catch (Exception)
            {
            }
        }

        private void button10_Click_2(object sender, EventArgs e)
        {
            try
            {
                new invoicePrint().setprintSalaryAudit(comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader);
            }
            catch (Exception)
            {
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            try
            {
                new invoicePrint().setprintSalarySlip(comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader);
            }
            catch (Exception)
            {
            }
        }

        private void pROCESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click_3(object sender, EventArgs e)
        {
            try
            {
                new invoicePrint().setprintSalarySlip(comboBox1.SelectedItem.ToString(), ComboBoxYear.Value.Year.ToString(), sqlconn, reader);
            }
            catch (Exception)
            {
            }
        }
    }
}