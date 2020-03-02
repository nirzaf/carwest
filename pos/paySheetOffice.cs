using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class PaySheetOffice : Form
    {
        public PaySheetOffice()
        {
            InitializeComponent();
        }

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
        String lastDate;
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

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
        string typeL, idL, dateL, workingDaysSeconds, otSeconds, lateSeconds, covering, coinsBF, payle, pay;
        DB db, db2;
        SqlDataReader reader, reader2;
        SqlConnection sqlconn, sqlconn2;
        string[] idArray;
        public void setValue(string id, string type, string date, string[] idA)
        {
            typeL = type;
            idL = id;
            dateL = date;
            idArray = idA;
        }
        private void PaySheetOffice_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            db = new DB();
            sqlconn = db.createSqlConnection();
            db2 = new DB();
            sqlconn2 = db2.createSqlConnection();
            //DataSet ds = new DataSet();

            //DataTable dt = new DataTable();


           
            //dt.Columns.Add("name", typeof(string));
          
            //dt.Columns.Add("basic", typeof(string));
            //dt.Columns.Add("br", typeof(string));
            //dt.Columns.Add("salaryforepf", typeof(string));
            //dt.Columns.Add("ot", typeof(string));
            //dt.Columns.Add("salaryArears", typeof(string));
            //dt.Columns.Add("specialAllownces", typeof(string));
            //dt.Columns.Add("fuel", typeof(string));
            //dt.Columns.Add("telephone", typeof(string));
            //dt.Columns.Add("accomadation", typeof(string));
            //dt.Columns.Add("attdanceAllownces", typeof(string));
            //dt.Columns.Add("fixedAllownces", typeof(string));
            //dt.Columns.Add("coinsBF", typeof(string));
            //dt.Columns.Add("otherAllownces", typeof(string));
            //dt.Columns.Add("totalEarning", typeof(string));
            //dt.Columns.Add("grossSalary", typeof(string));
            //dt.Columns.Add("late", typeof(string));
            //dt.Columns.Add("nopay", typeof(string));
            //dt.Columns.Add("salaryAdvance", typeof(string));
            //dt.Columns.Add("loan", typeof(string));
            //dt.Columns.Add("meal", typeof(string));
            //dt.Columns.Add("epf8", typeof(string));
            //dt.Columns.Add("payeTax", typeof(string));
            //dt.Columns.Add("deathDeduction", typeof(string));
            //dt.Columns.Add("wefere", typeof(string));
            //dt.Columns.Add("otherDeduction", typeof(string));
            //dt.Columns.Add("netsalary", typeof(string));
            //dt.Columns.Add("epf12", typeof(string));
            //dt.Columns.Add("etf3", typeof(string));
            //dt.Columns.Add("coinsCD", typeof(string));


            //String attandanceType = "", leave = "0"; ;
            //if (typeL.Equals("single"))
            //{
            //    sqlconn.Open();
            //    reader = new SqlCommand("select type from emp where empid='" + idL + "'", sqlconn).ExecuteReader();
            //    if (reader.Read())
            //    {
            //        attandanceType = reader.GetString(0);
            //    }
            //    sqlconn.Close();


            //    sqlconn.Open();
            //    reader = new SqlCommand("select a.month,b.empid,b.name,b.epfBasic,a.ot,a.ot,a.fixedAllownces,a.allwonces,a.late,a.offday,a.advanced,a.loan,a.meal,a.meal,a.workingDays,a.absentDays from paysheet as a,emp as b,company as c where a.empID='" + idL + "' and b.empid='" + idL + "' and month='" + dateL + "' ", sqlconn).ExecuteReader();
            //    if (reader.Read())
            //    {
            //        ///     dt.Rows.Add(reader[0], reader[1], reader[2], setAmountFormat(reader.GetDouble(7) + ""), reader[3], dateL.Split('/')[1] + " of " + dateL.Split('/')[0], reader[4], reader[5], idL, reader[6], setAmountFormat(reader.GetDouble(7) - 1000 + ""), "1000.00", reader[12], setAmountFormat(reader[16] + ""), setAmountFormat(reader[17] + ""), setAmountFormat(reader[19] + ""), setAmountFormat(reader[25] + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 8) + ""), setAmountFormat(((reader.GetDouble(17) + reader.GetDouble(18) + reader.GetDouble(19) + (((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 8))) + ""), setAmountFormat(reader[14] + ""), setAmountFormat(reader[15] + ""), setAmountFormat(reader[23] + ""), setAmountFormat(reader[24] + ""), setAmountFormat(reader.GetDouble(7) + reader.GetDouble(15) + reader.GetDouble(23) + reader.GetDouble(24) + ""), setAmountFormat(reader[26] + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 12) + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 3) + ""));
            //        double basic, totalEarning, totalDeduction, basiEPF;

            //        basic = reader.GetDouble(3);
            //        basiEPF = basic - (reader.GetDouble(8) + reader.GetDouble(9));
            //        totalEarning = reader.GetDouble(4) + reader.GetDouble(5) + reader.GetDouble(6) + reader.GetDouble(7);
            //        totalDeduction = ((basiEPF / 100) * 8) + reader.GetDouble(8) + reader.GetDouble(9) + reader.GetDouble(10) + reader.GetDouble(11) + reader.GetDouble(12) + reader.GetDouble(13);
            //        sqlconn2.Open();
            //        try
            //        {
            //            reader2 = new SqlCommand("select sum( DATEPART(SECOND, [otMin]) + 60 *  DATEPART(MINUTE, [otMin]) + 3600 * DATEPART(HOUR, [otMin] ) ) ,sum( DATEPART(SECOND, [lateMin]) + 60 *  DATEPART(MINUTE, [lateMin]) + 3600 * DATEPART(HOUR, [lateMin] ) ) from timesheet where empid='" + idL + "' and date between '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-1" + "' and '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-" + getLastDate(Int32.Parse(getMOnth(dateL.Split('/')[1].ToString())), Int32.Parse(dateL.Split('/')[0].ToString())) + "'  and workMin!='" + "00:00:00" + "'", sqlconn2).ExecuteReader();

            //            if (reader2.Read())
            //            {
            //                otSeconds = reader2.GetInt32(0) + "";
            //                lateSeconds = reader2.GetInt32(1) + "";
            //            }
            //            else
            //            {
            //                otSeconds = "";
            //                lateSeconds = "";
            //            }
            //            reader2.Close();
            //        }
            //        catch (Exception)
            //        {
            //            reader2.Close();
            //            otSeconds = "";
            //            lateSeconds = "";
            //        }
                   
            //        leave = "0";
            //        try
            //        {
            //            reader2 = new SqlCommand("select count(empid) from leave where date between '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-1" + "' and '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-" + getLastDate(Int32.Parse(getMOnth(dateL.Split('/')[1].ToString())), Int32.Parse(dateL.Split('/')[0].ToString())) + "' and empid='" + idL + "'", sqlconn2).ExecuteReader();
            //            if (reader2.Read())
            //            {
            //                leave = reader2.GetInt32(0) + "";
            //            }
            //            reader2.Close();
            //        }
            //        catch (Exception)
            //        {
            //            leave = "0";
            //            reader2.Close();
            //        }

            //        if (!otSeconds.Equals("") & reader.GetDouble(9) != 0)
            //        {

            //            otSeconds = "(" + Math.Round(Double.Parse(TimeSpan.FromSeconds(Int32.Parse(otSeconds)).TotalHours + ""), 2) + ")     " + setAmountFormat(reader[9] + "");

            //        }
            //        else
            //        {
            //            otSeconds = "0.0";
            //        }
            //        if (!lateSeconds.Equals("") & reader.GetDouble(13) != 0)
            //        {

            //            lateSeconds = "(" + Math.Round(Double.Parse(TimeSpan.FromSeconds(Int32.Parse(lateSeconds)).TotalHours + ""), 2) + ")     " + setAmountFormat(reader[13] + "");
            //        }
            //        else
            //        {
            //            lateSeconds = "0.0";
            //        }

            //        coinsBF = "0.0";
            //        payle = "0.0";
            //        pay = "0.0";
            //        sqlconn2.Close();

            //        //dt.Rows.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[2].ToString().ToLower()), "Pay Slip for the " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader.GetString(3).Split('/')[0].ToString().ToLower()) + " of " + reader.GetString(3).Split('/')[1], reader[4], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[2].ToString().ToLower()), setAmountFormat(basic - 1000 + ""), "1000.00", setAmountFormat(basiEPF + ""), otSeconds, "0.00", "0.00", reader[15], leave, "0.00", setAmountFormat(reader[5] + ""), setAmountFormat(reader[6] + ""), reader[14], setAmountFormat(reader[7] + ""), setAmountFormat(totalEarning + ""), setAmountFormat(basic + totalEarning + ""), lateSeconds, setAmountFormat(reader[9] + ""), setAmountFormat(reader[10] + ""), setAmountFormat(reader[11] + ""), setAmountFormat(reader[12] + ""), setAmountFormat((basiEPF / 100) * 8 + ""), setAmountFormat(coinsBF), setAmountFormat(payle), setAmountFormat(pay), setAmountFormat(reader[13] + ""), setAmountFormat((basic + totalEarning) - totalDeduction + ""), setAmountFormat((basiEPF / 100) * 12 + ""), setAmountFormat((basiEPF / 100) * 3 + ""), setAmountFormat(totalDeduction + ""));
            //        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", reader[15], leave, "0.00", setAmountFormat(reader[5] + ""), setAmountFormat(reader[6] + ""), reader[14], setAmountFormat(reader[7] + ""), setAmountFormat(totalEarning + ""), setAmountFormat(basic + totalEarning + ""), lateSeconds, setAmountFormat(reader[9] + ""), setAmountFormat(reader[10] + ""), setAmountFormat(reader[11] + ""), setAmountFormat(reader[12] + ""), setAmountFormat((basiEPF / 100) * 8 + ""), setAmountFormat(coinsBF), setAmountFormat(payle), setAmountFormat(pay), setAmountFormat(reader[13] + ""), setAmountFormat((basic + totalEarning) - totalDeduction + ""), setAmountFormat((basiEPF / 100) * 12 + ""), setAmountFormat((basiEPF / 100) * 3 + ""), setAmountFormat(totalDeduction + ""));
             
            //    }
            //    else
            //    {
            //        MessageBox.Show("no");
            //    }
            //    sqlconn.Close();




            //}
            //else
            //{


            //    try
            //    {
            //        sqlconn.Open();
            //        for (int i = 0; i < idArray.Length; i++)
            //        {
            //            // MessageBox.Show("2");
            //            reader = new SqlCommand("select type from emp where id='" + idArray[i] + "'", sqlconn).ExecuteReader();
            //            if (reader.Read())
            //            {
            //                attandanceType = reader.GetString(0);
            //            }
            //            reader.Close();

            //            {
            //                reader = new SqlCommand("select a.month,b.empid,b.name,b.epfBasic,a.ot,a.ot,a.fixedAllownces,a.allwonces,a.late,a.offday,a.advanced,a.loan,a.meal,a.meal,a.workingDays,a.absentDays from paysheet as a,emp as b,company as c where a.empID='" + idArray[i] + "' and b.id='" + idArray[i] + "' and month='" + dateL + "' ", sqlconn).ExecuteReader();
            //                if (reader.Read())
            //                {
            //                    // MessageBox.Show("3");
            //                    ///     dt.Rows.Add(reader[0], reader[1], reader[2], setAmountFormat(reader.GetDouble(7) + ""), reader[3], dateL.Split('/')[1] + " of " + dateL.Split('/')[0], reader[4], reader[5], idL, reader[6], setAmountFormat(reader.GetDouble(7) - 1000 + ""), "1000.00", reader[12], setAmountFormat(reader[16] + ""), setAmountFormat(reader[17] + ""), setAmountFormat(reader[19] + ""), setAmountFormat(reader[25] + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 8) + ""), setAmountFormat(((reader.GetDouble(17) + reader.GetDouble(18) + reader.GetDouble(19) + (((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 8))) + ""), setAmountFormat(reader[14] + ""), setAmountFormat(reader[15] + ""), setAmountFormat(reader[23] + ""), setAmountFormat(reader[24] + ""), setAmountFormat(reader.GetDouble(7) + reader.GetDouble(15) + reader.GetDouble(23) + reader.GetDouble(24) + ""), setAmountFormat(reader[26] + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 12) + ""), setAmountFormat((((reader.GetDouble(11) + reader.GetDouble(27)) / 100) * 3) + ""));
            //                    double basic, totalEarning, totalDeduction, basiEPF;

            //                    basic = reader.GetDouble(3);
            //                    basiEPF = basic - (reader.GetDouble(8) + reader.GetDouble(9));
            //                    totalEarning = reader.GetDouble(4) + reader.GetDouble(5) + reader.GetDouble(6) + reader.GetDouble(7);
            //                    totalDeduction = ((basiEPF / 100) * 8) + reader.GetDouble(8) + reader.GetDouble(9) + reader.GetDouble(10) + reader.GetDouble(11) + reader.GetDouble(12) + reader.GetDouble(13);

            //                    sqlconn2.Open();
            //                    try
            //                    {
            //                        reader2 = new SqlCommand("select sum( DATEPART(SECOND, [otMin]) + 60 *  DATEPART(MINUTE, [otMin]) + 3600 * DATEPART(HOUR, [otMin] ) ) ,sum( DATEPART(SECOND, [lateMin]) + 60 *  DATEPART(MINUTE, [lateMin]) + 3600 * DATEPART(HOUR, [lateMin] ) ) from timesheet where empid='" + idArray[i] + "' and date between '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-1" + "' and '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-" + getLastDate(Int32.Parse(getMOnth(dateL.Split('/')[1].ToString())), Int32.Parse(dateL.Split('/')[0].ToString())) + "'  and workMin!='" + "00:00:00" + "'", sqlconn2).ExecuteReader();

            //                        if (reader2.Read())
            //                        {
            //                            otSeconds = reader2.GetInt32(0) + "";
            //                            lateSeconds = reader2.GetInt32(1) + "";
            //                        }
            //                        else
            //                        {
            //                            otSeconds = "";
            //                            lateSeconds = "";
            //                        }
            //                        reader2.Close();
            //                    }
            //                    catch (Exception a)
            //                    {

            //                        reader2.Close();
            //                        otSeconds = "";
            //                        lateSeconds = "";
            //                    }
                               
            //                    leave = "0";
            //                    try
            //                    {
            //                        reader2 = new SqlCommand("select count(empid) from leave where date between '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-1" + "' and '" + dateL.Split('/')[0] + "-" + getMOnth(dateL.Split('/')[1].ToString()) + "-" + getLastDate(Int32.Parse(getMOnth(dateL.Split('/')[1].ToString())), Int32.Parse(dateL.Split('/')[0].ToString())) + "' and empid='" + idArray[i] + "'", sqlconn2).ExecuteReader();
            //                        if (reader2.Read())
            //                        {
            //                            leave = reader2.GetInt32(0) + "";
            //                        }
            //                        reader2.Close();
            //                    }
            //                    catch (Exception)
            //                    {
            //                        leave = "0";
            //                        reader2.Close();
            //                    }

            //                    if (!otSeconds.Equals("") & reader.GetDouble(9) != 0)
            //                    {

            //                        otSeconds = "(" + Math.Round(Double.Parse(TimeSpan.FromSeconds(Int32.Parse(otSeconds)).TotalHours + ""), 2) + ")     " + setAmountFormat(reader[9] + "");

            //                    }
            //                    else
            //                    {
            //                        otSeconds = "0.0";
            //                    }
            //                    if (!lateSeconds.Equals("") & reader.GetDouble(13) != 0)
            //                    {

            //                        lateSeconds = "(" + Math.Round(Double.Parse(TimeSpan.FromSeconds(Int32.Parse(lateSeconds)).TotalHours + ""), 2) + ")     " + setAmountFormat(reader[13] + "");

            //                    }
            //                    else
            //                    {
            //                        lateSeconds = "0.0";
            //                    }
            //                    coinsBF = "0.0";
            //                    payle = "0.0";
            //                    pay = "0.0";
                               
            //                    sqlconn2.Close();

            //                    dt.Rows.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[1].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[2].ToString().ToLower()), "Pay Slip for the " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader.GetString(3).Split('/')[0].ToString().ToLower()) + " of " + reader.GetString(3).Split('/')[1], reader[4], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[5].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[6].ToString().ToLower()), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[7].ToString().ToLower()), setAmountFormat(basic - 1000 + ""), "1000.00", setAmountFormat(basiEPF + ""), otSeconds, "0.00", "0.00", reader[20], leave, "0.00", setAmountFormat(reader[10] + ""), setAmountFormat(reader[11] + ""), reader[19], setAmountFormat(reader[12] + ""), setAmountFormat(totalEarning + ""), setAmountFormat(basic + totalEarning + ""), lateSeconds, setAmountFormat(reader[14] + ""), setAmountFormat(reader[15] + ""), setAmountFormat(reader[16] + ""), setAmountFormat(reader[17] + ""), setAmountFormat((basiEPF / 100) * 8 + ""), setAmountFormat(coinsBF), setAmountFormat(payle), setAmountFormat(pay), setAmountFormat(reader[18] + ""), setAmountFormat((basic + totalEarning) - totalDeduction + ""), setAmountFormat((basiEPF / 100) * 12 + ""), setAmountFormat((basiEPF / 100) * 3 + ""), setAmountFormat(totalDeduction + ""));
            //                }
            //                else
            //                {
            //                    MessageBox.Show("no");
            //                }
            //                reader.Close();
            //            }

            //        }
            //        sqlconn.Close();
            //    }
            //    catch (Exception a)
            //    {
            //        MessageBox.Show(a.StackTrace + "/" + a.Message);
            //    }

            //}







            //ds.Tables.Add(dt);

            //// ds.WriteXmlSchema("Sample13bbbb.xml");
            //payslip2 paySheet = new payslip2();
            //paySheet.SetDataSource(ds);
            ////paySheet.SetParameterValue("name","mahesh");
            ////paySheet.SetParameterValue("name", "mahesh");
            //crystalReportViewer1.ReportSource = paySheet;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                DataTable dt = new DataTable();


                dt.Columns.Add("id", typeof(string));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("days", typeof(double));
                dt.Columns.Add("basic", typeof(double));
                dt.Columns.Add("fixedAllownces", typeof(double));
                dt.Columns.Add("otherAllownces", typeof(double));
                dt.Columns.Add("totalEarning", typeof(double));
                dt.Columns.Add("grossSalary", typeof(double));
                dt.Columns.Add("late", typeof(double));
                dt.Columns.Add("nopay", typeof(double));
                dt.Columns.Add("salaryAdvance", typeof(double));
                dt.Columns.Add("epf8", typeof(double));
                dt.Columns.Add("otherDeduction", typeof(double));
                dt.Columns.Add("totalDeduction", typeof(double));
                dt.Columns.Add("netsalary", typeof(double));
                dt.Columns.Add("epf12", typeof(double));
                dt.Columns.Add("etf3", typeof(double));
                sqlconn.Open();
                double epf8, epf12, etf3,tot,ear,dedu;
                reader = new SqlCommand("select a.*,b.name,b.type from paysheet as a,emp as b where a.month='" + dateTimePicker1.Value.ToString("d").ToString().Split('/')[2] + "/" + comboBox1.SelectedItem + "' and a.empid=b.empid", sqlconn).ExecuteReader();
                while (reader.Read())
                {

                    if (reader.GetString(21).Equals("timeBased"))
                    {
                        epf8 = reader.GetDouble(18);
                        epf12 = reader.GetDouble(19);
                        etf3 = reader.GetDouble(20);
                        ear = (reader.GetDouble(6) + reader.GetDouble(15));
                        dedu = (reader.GetDouble(10) + reader.GetDouble(11) + reader.GetDouble(9) + epf8 + reader.GetDouble(8));
                   
                         tot = (reader.GetDouble(3) + ear) - dedu;
                    }
                    else {
                        epf8 = reader.GetDouble(18) * reader.GetInt32(4) ;
                        epf12 = reader.GetDouble(19) * reader.GetInt32(4) ;
                        etf3 = reader.GetDouble(20) * reader.GetInt32(4);
                        ear = (reader.GetDouble(6) + reader.GetDouble(15));
                        dedu = (reader.GetDouble(10) + reader.GetDouble(11) + reader.GetDouble(9) + epf8 + reader.GetDouble(8));

                        tot = ((reader.GetDouble(3) * reader.GetInt32(4)) + ear) - dedu;
                    }
                     
                    dt.Rows.Add(reader[1], reader[21], reader[4], reader[3], reader[6], reader[15],ear, 0, reader[10], reader[11], reader[9], epf8, reader[8], dedu,tot,epf12,etf3);
                }
                ds.Tables.Add(dt);
                paySheet paySheet = new paySheet();
                paySheet.SetDataSource(ds);
                paySheet.SetParameterValue("month", "Pay Sheet on " + dateTimePicker1.Value.ToString("d").ToString().Split('/')[2] + "/" + comboBox1.SelectedItem);
                crystalReportViewer1.ReportSource = paySheet;
              //  ds.WriteXmlSchema("paySlipg.xml");
               // MessageBox.Show("ok");
                sqlconn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message+"/"+a.StackTrace);
                sqlconn.Close();
            }
        }
    }
}
