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
    public partial class processAllDateSelect : Form
    {
        public processAllDateSelect()
        {
            InitializeComponent();
          //  MessageBox.Show("1");
        }

        private void processAllDateSelect_Load(object sender, EventArgs e)
        {
            db3= new DB();
            sqlconn3 = db3.createSqlConnection();
            db2 = new DB();
            sqlconn2 = db2.createSqlConnection();
            db = new DB();
            sqlconn = db.createSqlConnection();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            this.TopMost = true;
        }
        SqlConnection sqlconn, sqlconn2,sqlconn3;
        DB db3,db2,db;
        SqlDataReader reader, reader2,reader3;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Sorry You Havent Select Process Period Correctly");
            }
            else
            {
                try
                {
                    sqlconn2.Open();
                    reader2 = new SqlCommand("select * from paysheet where month='" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem.ToString() + "'", sqlconn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        if (password.Text.Equals("ra1471sika"))
                        {
                            sqlconn2.Close();
                            sqlconn3.Open();
                            Cursor.Current = Cursors.WaitCursor;
                            reader3 = new SqlCommand("select empid from emp", sqlconn3).ExecuteReader();
                            //reader.Close();
                            while (reader3.Read())
                            {
                                process(reader3[0].ToString(), comboBox1.SelectedItem.ToString(), dateTimePicker1.Value.ToString("d").Split('/')[2], true);

                            }
                            sqlconn3.Close();
                            // loadTimeSheet();
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Process Completed Succesfully ");

                            this.Dispose();

                        }
                        else
                        {
                            MessageBox.Show("Sorry, Payroll Already Proccess for Relevent Month.Please In Password for Re-Procces");
                            password.Focus();
                        }
                    }
                    else
                    {
                        sqlconn2.Close();
                        sqlconn3 = new DB().createSqlConnection();
                        Cursor.Current = Cursors.WaitCursor;
                        reader3 = new SqlCommand("select id from emp", sqlconn3).ExecuteReader();
                        //reader.Close();
                        while (reader3.Read())
                        {
                            process(reader3[0].ToString(), comboBox1.SelectedItem.ToString(), dateTimePicker1.Value.ToString("d").Split('/')[2], true);

                        }
                        reader3.Close();
                        // loadTimeSheet();
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Process Completed Succesfully ");

                        this.Dispose();

                    }
                    sqlconn2.Close();
               
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message+"/"+a.StackTrace);
                }
            }
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
        Boolean otEpfPay;
        string imagePath = "", imageFullPath = "", attandanceType;
        string[] readfromAddress;
        double basicEpf, basicSalary2, offDayDeductValue2, absentDaysValue2, workingDaysValue2;
        string getMOnth(string y)
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
            } if (y.Equals("October"))
            {
                month = "10";
            } if (y.Equals("November"))
            {
                month = "11";
            } if (y.Equals("December"))
            {
                month = "12";
            }

            return month;


        }
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
        double basicSalary, bankingAmount, epf, etf, epf_, otRate, otRate2, lateRate, lateRate2, offDayRate, offDayRate2, earning2;
        string workingDaysSeconds, workingDaysSeconds2, otSeconds, otSeconds2, lateSeconds, lateSeconds2;
        int workingDaysDB, workingDaysDB2;
        bool otpay, otpay2, latededu, latededu2, lateBalance, lateBalance2;
        
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

        string lastDate;
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
        private void process(string id, string month, string year, bool states)
        {
            try
            {

                Cursor.Current = Cursors.WaitCursor;
                sqlconn.Open();
                reader = new SqlCommand("select type,empID,epfBasic,bankingAmount,epf,epf12,etf from emp where empid='" + id + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    attandanceType = reader.GetString(0);
                    basicEpf = reader.GetDouble(2);
                    otEpfPay = false;
                    maxOTHours = 0;
                    bankingAmount = reader.GetDouble(3);
                    epf = reader.GetDouble(4);
                    epf_ = reader.GetDouble(5);
                    etf = reader.GetDouble(6);
                }
                if (states)
                {

                    try
                    {
                        //  MessageBox.Show("dada2");
                        new companySalary().setTimeSheet(id, getMOnth(month), year, attandanceType, reader.GetInt32(1).ToString(), getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)));
                        //  MessageBox.Show("dada2");
                        //    new companySalary().setTimeSheet("7", "02", "2014", "timeBased", "1", "28");
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + "/abc/" + a.StackTrace);
                    }                 //   MessageBox.Show("hu2");
                }
                reader.Close();


                //******************************************************************************************************************************

                if (attandanceType.Equals("timeBased"))
                {
                    reader = new SqlCommand("select basic,workingDays,otRate,otpay,lateDeduction,lateRate,offDayrate,lateBalance from timeBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        basicSalary = reader.GetDouble(0);

                        workingDaysDB = 26;
                        otRate = reader.GetDouble(2);
                        otpay = reader.GetBoolean(3);
                        latededu = true;
                        lateRate = Math.Round((basicSalary / (26 * 8)), 2);
                        offDayRate = Math.Round((basicSalary / (26)), 2);
                        lateBalance = reader.GetBoolean(7);
                    }

                    reader.Close();
                }
                else if (attandanceType.Equals("dayBased"))
                {
                    reader = new SqlCommand("select daysalary,otRate,otpay,lateDeduction,lateRate,lateBalance from dayBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        basicSalary = reader.GetDouble(0);

                        // workingDaysDB = reader.GetInt32(1);
                        otRate = reader.GetDouble(1);
                        otpay = reader.GetBoolean(2);
                        latededu = true;
                        lateRate = Math.Round((basicSalary / (8)), 2);
                        offDayRate = basicSalary;
                        lateBalance = reader.GetBoolean(5);

                    }

                    reader.Close();
                }

                meal = 0.0;

                reader = new SqlCommand("select amount from meal  where  empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    meal = reader.GetDouble(0);
                }
                reader.Close();
                loan = 0.0;
                reader = new SqlCommand("select amount,installment,month,date from loan  where  empid='" + id + "'  ", sqlconn).ExecuteReader();
                while (reader.Read())
                {

                    try
                    {
                        if (((new DateTime(Int32.Parse(year), Int32.Parse(getMOnth(month)), 20) - reader.GetDateTime(3)).TotalDays) >= 0)
                        {
                            if (diffMonths(new DateTime(Int32.Parse(year), Int32.Parse(getMOnth(month)), 20), reader.GetDateTime(3)) < reader.GetInt32(1))
                            {
                                loan = loan + (reader.GetDouble(0) / reader.GetInt32(1));
                                //  MessageBox.Show(+"");

                            }
                        }

                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }

                }
                reader.Close();

                try
                {
                    reader = new SqlCommand("select a.date from timesheet as a , nopay as b where a.date=b.date and b.empid='" + id + "'", sqlconn).ExecuteReader();
                    while (reader.Read())
                    {
                        // MessageBox.Show("ss");
                        using (sqlconn2 = new DB().createSqlConnection())
                        {
                            new SqlCommand("update timesheet set workmin='" + "00:00" + "',lateMin='" + "00:00" + "',otMin='" + "00:00" + "' where empid='" + id + "' and date='" + reader[0] + "'", sqlconn2).ExecuteNonQuery();

                        }

                    }
                    reader.Close();
                }
                catch (Exception a)
                {

                    //  MessageBox.Show("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+a.Message);
                }
                reader = new SqlCommand("select count(a.empid) from dayoff as a,timesheet as b where a.empid='" + id + "' and a.date between '" + year + "-" + getMOnth(month) + "-1" + "' and '" + year + "-" + getMOnth(month) + "-" + getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)) + "' and b.empid='" + id + "' and b.workMin !='" + "00:00" + "' and a.date=b.date", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    dayOffCount = reader.GetInt32(0);
                    dayOffAmount = reader.GetInt32(0) * (offDayRate / 2);
                }
                reader.Close();

                {

                    if (attandanceType.Equals("dayBased") | attandanceType.Equals("dayBasedShift") | attandanceType.Equals("dayBasedMultiShift"))
                    {
                        //   MessageBox.Show("huuuuuuu");
                        absentDaysL = "0";
                        absentDaysValue = 0;
                        offDayL = "0";
                        reader = new SqlCommand("select count(empid) from timesheet where workMin!='" + "00:00:00" + "' and empid='" + id + "' and date between '" + year + "-" + getMOnth(month) + "-1" + "' and '" + year + "-" + getMOnth(month) + "-" + getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)) + "'", sqlconn).ExecuteReader();
                        if (reader.Read())
                        {
                            workingDaysL = reader[0].ToString();
                            // basicSalary = basicSalary * reader.GetInt32(0);
                            extraWorkDaysL = "0";
                            extraWorkingDayspayL = "0.00";
                            //      MessageBox.Show("huuuuuuu " + workingDaysL);
                        }
                        reader.Close();
                        //++++++++++++
                        reader = new SqlCommand("select sum( DATEPART(SECOND, [workMin]) + 60 *  DATEPART(MINUTE, [workMin]) + 3600 * DATEPART(HOUR, [workMin] ) ) ,sum( DATEPART(SECOND, [otMin]) + 60 *  DATEPART(MINUTE, [otMin]) + 3600 * DATEPART(HOUR, [otMin] ) ) ,sum( DATEPART(SECOND, [lateMin]) + 60 *  DATEPART(MINUTE, [lateMin]) + 3600 * DATEPART(HOUR, [lateMin] ) ) from timesheet where empid='" + id + "' and date between '" + year + "-" + getMOnth(month) + "-1" + "' and '" + year + "-" + getMOnth(month) + "-" + getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)) + "'  and workMin!='" + "00:00:00" + "'", sqlconn).ExecuteReader();

                        if (reader.Read())
                        {
                            workingDaysSeconds = reader[0].ToString();
                            otSeconds = reader[1].ToString();
                            lateSeconds = reader[2].ToString();
                            //  MessageBox.Show(lateSeconds);
                            if (!latededu | lateSeconds.Equals(""))
                            {
                                lateL = "00.00";
                            }
                            else
                            {
                                if (lateBalance & TimeSpan.Parse(otSeconds).TotalHours != 0)
                                {
                                    if (Int32.Parse(otSeconds) >= Int32.Parse(lateSeconds))
                                    {
                                        lateL = "0.00";
                                        otSeconds = (Int32.Parse(otSeconds) - Int32.Parse(lateSeconds)).ToString();
                                    }
                                    else
                                    {

                                        lateL = setAmountFormat(lateRate * (Double.Parse((Int32.Parse(lateSeconds) - Int32.Parse(otSeconds)).ToString()) / (60 * 60)) + "");
                                        otSeconds = "0";
                                    }
                                }
                                else
                                {
                                    //    MessageBox.Show((Double.Parse(lateSeconds) / (60 * 60)) + "");
                                    lateL = setAmountFormat(lateRate * (Double.Parse(lateSeconds) / (60 * 60)) + "");

                                }

                            }

                            if (!otpay | otSeconds.Equals(""))
                            {
                                otL = "0.00";
                            }
                            else
                            {
                                //  ot.Text = setAmountFormat(Math.Round(((Double.Parse(otSeconds) / (60 * 60)) * otRate), 2) + "");
                                otL = setAmountFormat(((Double.Parse(otSeconds) / (60 * 60)) * otRate) + "");

                            }
                        }
                        reader.Close();

                    }
                    else
                    {

                        reader = new SqlCommand("select count(empid) from timesheet where workMin!='" + "00:00:00" + "' and empid='" + id + "' and date between '" + year + "-" + getMOnth(month) + "-1" + "' and '" + year + "-" + getMOnth(month) + "-" + getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)) + "'", sqlconn).ExecuteReader();
                        if (reader.Read())
                        {



                            workingDaysL = reader[0].ToString();
                            if (reader.GetInt32(0) > workingDaysDB)
                            {
                                extraWorkDaysL = reader.GetInt32(0) - workingDaysDB + "";
                                extraWorkingDayspayL = setAmountFormat((reader.GetInt32(0) - workingDaysDB) * offDayRate + "");
                            }
                            else
                            {
                                extraWorkDaysL = "0";
                                extraWorkingDayspayL = "0.00";
                            }

                            if ((workingDaysDB - reader.GetInt32(0)) < 0)
                            {
                                offDayL = "0.0";
                                absentDaysL = "0";
                            }
                            else
                            {

                                offDayL = setAmountFormat((offDayRate * (workingDaysDB - reader.GetInt32(0))) + "");
                                absentDaysL = (workingDaysDB - reader.GetInt32(0)) + "";
                            }



                        }
                        reader.Close();
                        //++++++++++++
                        reader = new SqlCommand("select sum( DATEPART(SECOND, [workMin]) + 60 *  DATEPART(MINUTE, [workMin]) + 3600 * DATEPART(HOUR, [workMin] ) ) ,sum( DATEPART(SECOND, [otMin]) + 60 *  DATEPART(MINUTE, [otMin]) + 3600 * DATEPART(HOUR, [otMin] ) ) ,sum( DATEPART(SECOND, [lateMin]) + 60 *  DATEPART(MINUTE, [lateMin]) + 3600 * DATEPART(HOUR, [lateMin] ) ) from timesheet where empid='" + id + "' and date between '" + year + "-" + getMOnth(month) + "-1" + "' and '" + year + "-" + getMOnth(month) + "-" + getLastDate(Int32.Parse(getMOnth(month)), Int32.Parse(year)) + "'  and workMin!='" + "00:00:00" + "'", sqlconn).ExecuteReader();

                        if (reader.Read())
                        {
                            workingDaysSeconds = reader[0].ToString();
                            otSeconds = reader[1].ToString();
                            lateSeconds = reader[2].ToString();
                            //     MessageBox.Show(lateSeconds);
                            if (!latededu | lateSeconds.Equals(""))
                            {
                                lateL = "00.00";
                            }
                            else
                            {
                                if (lateBalance & TimeSpan.Parse(otSeconds).TotalHours != 0)
                                {
                                    if (Int32.Parse(otSeconds) >= Int32.Parse(lateSeconds))
                                    {
                                        lateL = "0.00";
                                        otSeconds = (Int32.Parse(otSeconds) - Int32.Parse(lateSeconds)).ToString();
                                    }
                                    else
                                    {

                                        lateL = setAmountFormat(lateRate * (Double.Parse((Int32.Parse(lateSeconds) - Int32.Parse(otSeconds)).ToString()) / (60 * 60)) + "");
                                        otSeconds = "0";
                                    }
                                }
                                else
                                {
                                    //    MessageBox.Show((Double.Parse(lateSeconds) / (60 * 60)) + "");
                                    lateL = setAmountFormat(lateRate * (Double.Parse(lateSeconds) / (60 * 60)) + "");

                                }

                            }
                            //   MessageBox.Show("ot ");
                            if (!otpay | otSeconds.Equals(""))
                            {
                                otL = "0.00";
                            }
                            else
                            {
                                //  ot.Text = setAmountFormat(Math.Round(((Double.Parse(otSeconds) / (60 * 60)) * otRate), 2) + "");
                                otL = setAmountFormat(((Double.Parse(otSeconds) / (60 * 60)) * otRate) + "");

                            }
                        }
                        reader.Close();

                    }

                    //+++++++++++++++++++

                    //++++++++++++
                }



                reader = new SqlCommand("select sum(amount) from deduct  where  id='" + id + "' and month = '" + year + "/" + month + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    if (reader[0].ToString().Equals(""))
                    {
                        advancedL = setAmountFormat("0.00");

                    }
                    else
                    {
                        advancedL = setAmountFormat(reader[0].ToString());

                    }

                }

                reader.Close();

                reader = new SqlCommand("select sum(amount) from earning  where  id='" + id + "' and month = '" + year + "/" + month + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {

                    //if ()
                    //{

                    //}
                    try
                    {
                        earning = reader.GetDouble(0);
                    }
                    catch (Exception)
                    {

                        earning = 0.0;
                    }



                }

                reader.Close();



                reader.Close();

                reader = new SqlCommand("select sum(amount) from fixedDeduction where empid='" + id + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    if (!reader[0].ToString().Equals(""))
                    {
                        fixedDeductionL = setAmountFormat(reader[0].ToString());
                    }
                    else
                    {
                        fixedDeductionL = "0.00";
                    }

                }
                reader.Close();
                reader = new SqlCommand("select sum(amount) from fixedAllownces where empid='" + id + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    if (!reader[0].ToString().Equals(""))
                    {
                        fixedAllowncesL = setAmountFormat(reader[0].ToString());
                    }
                    else
                    {
                        fixedAllowncesL = "0.00";
                    }

                }


                reader.Close();
                attendanceAllownce = 0;
                workingDaysDB = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(getMOnth(month)));
                workingDaysDB = workingDaysDB - 5;
                if (Int32.Parse(workingDaysL) >= workingDaysDB)
                {
                    attendanceAllownce = 2500;
                }
                new SqlCommand("delete from paysheet where month='" + year + "/" + month + "' and empid='" + id + "'", sqlconn).ExecuteNonQuery();
                //   MessageBox.Show(extraWorkingDayspayL+" "+extraWorkDaysL);

                new SqlCommand("delete from banking where id='" + id + "' and date='" + year + "-" + getMOnth(month) + "-1" + "'", sqlconn).ExecuteNonQuery();

                new SqlCommand("insert into banking values ('" + id + "','" + year + "-" + getMOnth(month) + "-1" + "','" + year + "-" + getMOnth(month) + "-1 BANKING AMOUNT" + "','" + Int32.Parse(workingDaysL) * bankingAmount + "','" + 0 + "')", sqlconn).ExecuteNonQuery();
                new SqlCommand("insert into banking values ('" + id + "','" + year + "-" + getMOnth(month) + "-1" + "','" + year + "-" + getMOnth(month) + "-1 EPF" + "','" + 0 + "','" + epf + "')", sqlconn).ExecuteNonQuery();

                new SqlCommand("insert into paysheet values('" + id + "','" + year + "/" + month + "','" + (basicSalary) + "','" + (workingDaysL) + "','" + Int32.Parse(absentDaysL) + "','" + Double.Parse(fixedAllowncesL) + "','" + (Double.Parse(otL)) + "','" + bankingAmount + "','" + Double.Parse(advancedL) + "','" + (Double.Parse(lateL)) + "','" + (Double.Parse(offDayL) + dayOffAmount) + "','" + DateTime.Now + "','" + Double.Parse(extraWorkingDayspayL) + "','" + Int32.Parse(extraWorkDaysL) + "','" + earning + "','" + meal + "','" + loan + "','" + epf + "','" + epf_ + "','" + etf + "','" + attendanceAllownce + "')", sqlconn).ExecuteNonQuery();



                //  sqlconn.Close();
                //******************************************************************************************************************************


                //  listBox2_Click(new object, new EventArgs);
                Cursor.Current = Cursors.Default;
                sqlconn.Close();


            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + " / " + a.StackTrace);
            }

        }


        string extraWorkDaysL, extraWorkingDayspayL;
        Int32 workingdaysGover, count, maxOTHours,dayOffCount;
        TimeSpan workHours, lateMin, OtMIn, otEpfPayTime;
        bool sndayOT;
        bool states, states2;
        companySalary companySalary;
        double attendanceAllownce, offDayDeductValue = 0.0, absentDaysValue = 0.0, workingDaysValue = 0, earning, dayOffAmount, loan, advanced1, advanced2;
        TimeSpan workingDaysSecondsValue, otSecondsValue, lateSecondsValue;
        String workingDaysL, absentDaysL, fixedAllowncesL, otL, fixedDeductionL, advancedL, lateL, offDayL;
        string late1, late2, ot1, ot2, offDay1, offDay2, extraDay1, extraDay2, workDay1, workDay2, extraDayPay1, extraDayPay2, offDayPay1, offDayPay2;
        double  basicSalary_, basicSalary2_,meal,balance;

        Double otepfPayAmount, grossSalary, netSalaryLast, netSalaryAmOunt, Traveling, Meal, Bounes;
    }
}
