using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    internal class process
    {
        private bool BonusH, epfH;
        private double loanAmount, totalEaringH, payAdvanedH, workingDaysH, absentH, commisH, bringFrontD, grossSalaryD, epf8D, punishD, bonusD, advancedD, LoanD, totalDeductionH, NetSalaryH, PayH, BalanceH, salaryD, bankingAmountD, AllowanceD, mealAllowanceD, incentiveD, epf12, etf3;
        private string epfNo = "";

        // public void saveSummery(string id, string month, string year, SqlConnection sqlconn, SqlDataReader reader, string paySalary, string payAdvanced, DB db)
        public void saveSummery(string id, string month, string year, SqlConnection sqlconn, SqlDataReader reader, string paySalary, string payAdvanced, DB db, DB db2, SqlConnection sqlconn2, SqlDataReader reader2, DB db3, SqlConnection sqlconn3, SqlDataReader reader3)
        {
            //
            try
            {
                if (!paySalary.Equals(""))
                {
                    //  PayH = Double.Parse(paySalary);
                }
                loanAmount = 0;
                sqlconn.Open();
                // reader = new SqlCommand("select * from loan ", sqlconn).ExecuteReader();
                reader = new SqlCommand("select * from loan where amount>paid and empid='" + id + "'", sqlconn).ExecuteReader();

                while (reader.Read())
                {
                    sqlconn2.Open();
                    new SqlCommand("delete from loanHistory where loanid='" + reader[7] + "' and month='" + year + "/" + month + "'", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    if ((reader.GetDouble(1) - reader.GetDouble(6)) > 1)
                    {
                        sqlconn2.Open();
                        new SqlCommand("insert into loanHistory values ('" + id + "','" + year + "/" + month + "','" + ((reader.GetDouble(1) / reader.GetInt32(2))) + "','" + reader[7] + "')", sqlconn2).ExecuteNonQuery();
                        sqlconn2.Close();
                        loanAmount = loanAmount + ((reader.GetDouble(1) / reader.GetInt32(2)));
                    }

                    sqlconn2.Open();
                    new SqlCommand("update loan set paid='" + 0 + "' where id='" + reader[7] + "'", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    sqlconn2.Open();
                    reader2 = new SqlCommand("select * from loanHistory where   loanid='" + reader[7] + "' ", sqlconn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        sqlconn3.Open();
                        new SqlCommand("update loan set paid=paid+'" + ((reader.GetDouble(1) / reader.GetInt32(2))) + "' where id='" + reader[7] + "'", sqlconn3).ExecuteNonQuery();
                        sqlconn3.Close();
                    }
                    sqlconn2.Close();
                }
                sqlconn.Close();
                try
                {
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from loanHistory where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        loanAmount = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }

                try
                {
                    payAdvanedH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from advancedEmp where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        payAdvanedH = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                try
                {
                    PayH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from salaryPay where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        PayH = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                sqlconn.Open();
                new SqlCommand("delete paysheet where empID='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                sqlconn.Close();
                sqlconn.Open();
                new SqlCommand("delete paysheetAudit where empID='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                sqlconn.Close();

                try
                {
                    workingDaysH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select count(empid) from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and present='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        workingDaysH = reader.GetInt32(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    sqlconn.Close();
                }
                try
                {
                    absentH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select count(empid) from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and punish='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        absentH = reader.GetInt32(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                try
                {
                    BonusH = false;
                    sqlconn.Open();
                    reader = new SqlCommand("select empid from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and punish!='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        BonusH = true;
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                //try
                //{
                //    commisH = 0;
                //    sqlconn.Open();
                //    reader = new SqlCommand("select sum(amount2) from COMMIS   where date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and empid='" + id + "'", sqlconn).ExecuteReader();
                //    if (reader.Read())
                //    {
                //        commisH = reader.GetInt32(0);
                //    }
                //    sqlconn.Close();
                //}
                //catch (Exception)
                //{
                //    sqlconn.Close();
                //}

                try
                {
                    bringFrontD = 0;
                    sqlconn.Open();

                    var a = Convert.ToDateTime(db.getMOnth(month) + "/1" + "/" + year).AddMonths(-1);
                    //MessageBox.Show(a.Year + "/" + a.Month);
                    reader = new SqlCommand("select balance from paysheet where month = '" + a.Year + "/" + db.getMOnthName(a.Month + "") + "' and empid='" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        bringFrontD = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    sqlconn.Close();
                }

                try
                {
                    epf8D = 0;
                    grossSalaryD = 0;
                    punishD = 0;
                    advancedD = 0;
                    LoanD = 0;
                    bankingAmountD = 0;
                    salaryD = 0;
                    AllowanceD = 0;
                    mealAllowanceD = 0;
                    epf12 = 0;
                    etf3 = 0;
                    incentiveD = 0;
                    epfNo = "";
                    epfH = false;
                    sqlconn.Open();

                    var a = Convert.ToDateTime("1/" + db.getMOnth(month) + "/" + year).AddMonths(-1);
                    reader = new SqlCommand("select epf,gross,punish,advanced,loan,bankingAmount,epfBasic,allowances,meal,incentive,epfno,isEpf from emp where empid = '" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        epf8D = reader.GetDouble(0);
                        grossSalaryD = reader.GetDouble(1);
                        punishD = reader.GetDouble(2);
                        advancedD = reader.GetDouble(3);
                        LoanD = reader.GetDouble(4);
                        bankingAmountD = reader.GetDouble(5);
                        salaryD = reader.GetDouble(6);
                        AllowanceD = reader.GetDouble(7);
                        mealAllowanceD = reader.GetDouble(8);
                        incentiveD = reader.GetDouble(9);
                        epfH = reader.GetBoolean(11);
                        epfNo = reader[10] + "";
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                if (workingDaysH > 15)
                {
                    if (id.Equals("3"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }

                    if (id.Equals("2"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("1"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("17"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("12"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("13"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("0"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("24"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("5"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }
                    if (id.Equals("21"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }
                }
                if (!BonusH)
                {
                    bonusD = 0;
                }
                advancedD = advancedD + payAdvanedH;
                // MessageBox.Show(workingDaysH+"");
                bankingAmountD = bankingAmountD;
                //MessageBox.Show(bankingAmountD + "");
                //  salaryD = salaryD * workingDaysH;
                punishD = 0;

                if (workingDaysH > 26)
                {
                    {
                        incentiveD = 3000;
                    }
                    if (epfH)
                    {
                        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                    }
                }
                else if (workingDaysH < 26)
                {
                    if (id.Equals("6") || id.Equals("2") || id.Equals("19") || id.Equals("21"))
                    {
                    }
                    else
                    {
                        punishD = ((salaryD + AllowanceD) / 25) * (25 - workingDaysH);
                        absentH = 25 - workingDaysH;
                    }
                    if (epfH)
                    {
                        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                    }
                }

                //else
                //{
                //    if (epfH)
                //    {
                //        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                //        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                //        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                //    }
                //}
                //  punishD = punishD * absentH;

                grossSalaryD = bankingAmountD + salaryD + AllowanceD + mealAllowanceD + incentiveD;
                totalEaringH = grossSalaryD + bonusD + bringFrontD + commisH;

                // punishD=
                totalDeductionH = epf8D + (punishD) + advancedD + loanAmount;

                NetSalaryH = totalEaringH - totalDeductionH;

                BalanceH = NetSalaryH - PayH;
                if (!(workingDaysH == 0 && NetSalaryH == 0))
                {
                    sqlconn.Open();
                    new SqlCommand("insert into paysheet values ('" + id + "','" + year + "/" + month + "','" + workingDaysH + "','" + absentH + "','" + salaryD + "','" + bankingAmountD + "','" + AllowanceD + "','" + mealAllowanceD + "','" + incentiveD + "','" + grossSalaryD + "','" + bonusD + "','" + bringFrontD + "','" + totalEaringH + "','" + epf8D + "','" + punishD + "','" + loanAmount + "','" + advancedD + "','" + totalDeductionH + "','" + NetSalaryH + "','" + PayH + "','" + BalanceH + "','" + commisH + "','" + epf12 + "','" + etf3 + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();
                    sqlconn.Open();
                    new SqlCommand("insert into paysheetAudit values ('" + id + "','" + year + "/" + month + "','" + epfNo + "','" + ((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + "','" + (3500 - (((3500) / 25) * absentH)) + "','" + (AllowanceD / 100) * 45 + "','" + (AllowanceD / 100) * 55 + "','" + incentiveD + "','" + mealAllowanceD + "','" + punishD + "','" + grossSalaryD + "','" + (((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) + "','" + epf12 + "','" + etf3 + "','" + (totalEaringH + epf12 + etf3) + "','" + advancedD + "','" + loanAmount + "','" + epf8D + "','" + 0 + "','" + totalDeductionH + "','" + NetSalaryH + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();
                }

                //getSalaru(id, month, year);
                MessageBox.Show("Process Completed");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }

        public void saveSummery2(string id, string month, string year, SqlConnection sqlconn, SqlDataReader reader, string paySalary, string payAdvanced, DB db, DB db2, SqlConnection sqlconn2, SqlDataReader reader2, DB db3, SqlConnection sqlconn3, SqlDataReader reader3)
        {
            try
            {
                if (!paySalary.Equals(""))
                {
                    //  PayH = Double.Parse(paySalary);
                }
                //if (!payAdvanced.Equals(""))
                //{
                //    sqlconn.Open();
                //    new SqlCommand("insert into advancedEmp values ('" + id + "','" + payAdvanced + "','" + DateTime.Now + "','" + year + "/" + month + "')", sqlconn).ExecuteNonQuery();
                //    sqlconn.Close();

                //}
                loanAmount = 0;
                sqlconn.Open();
                // reader = new SqlCommand("select * from loan ", sqlconn).ExecuteReader();
                reader = new SqlCommand("select * from loan where amount>paid and empid='" + id + "'", sqlconn).ExecuteReader();

                while (reader.Read())
                {
                    sqlconn2.Open();
                    new SqlCommand("delete from loanHistory where loanid='" + reader[7] + "' and month='" + year + "/" + month + "'", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    if ((reader.GetDouble(1) - reader.GetDouble(6)) > 1)
                    {
                        sqlconn2.Open();
                        new SqlCommand("insert into loanHistory values ('" + id + "','" + year + "/" + month + "','" + ((reader.GetDouble(1) / reader.GetInt32(2))) + "','" + reader[7] + "')", sqlconn2).ExecuteNonQuery();
                        sqlconn2.Close();
                        loanAmount = loanAmount + ((reader.GetDouble(1) / reader.GetInt32(2)));
                    }

                    sqlconn2.Open();
                    new SqlCommand("update loan set paid='" + 0 + "' where id='" + reader[7] + "'", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    sqlconn2.Open();
                    reader2 = new SqlCommand("select * from loanHistory where   loanid='" + reader[7] + "' ", sqlconn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        sqlconn3.Open();
                        new SqlCommand("update loan set paid=paid+'" + ((reader.GetDouble(1) / reader.GetInt32(2))) + "' where id='" + reader[7] + "'", sqlconn3).ExecuteNonQuery();
                        sqlconn3.Close();
                    }
                    sqlconn2.Close();
                }
                sqlconn.Close();
                try
                {
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from loanHistory where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        loanAmount = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }

                try
                {
                    payAdvanedH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from advancedEmp where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        payAdvanedH = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                try
                {
                    PayH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(amount) from salaryPay where empid='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        PayH = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                sqlconn.Open();
                new SqlCommand("delete paysheet where empID='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                sqlconn.Close();
                sqlconn.Open();
                new SqlCommand("delete paysheetAudit where empID='" + id + "' and month='" + year + "/" + month + "'", sqlconn).ExecuteReader();
                sqlconn.Close();

                try
                {
                    workingDaysH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select count(empid) from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and present='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        workingDaysH = reader.GetInt32(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    sqlconn.Close();
                }
                try
                {
                    absentH = 0;
                    sqlconn.Open();
                    reader = new SqlCommand("select count(empid) from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and punish='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        absentH = reader.GetInt32(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                try
                {
                    BonusH = false;
                    sqlconn.Open();
                    reader = new SqlCommand("select empid from attendance where empid='" + id + "' and date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and punish!='" + true + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        BonusH = true;
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                //try
                //{
                //    commisH = 0;
                //    sqlconn.Open();
                //    reader = new SqlCommand("select sum(amount2) from COMMIS   where date between '" + year + "-" + month + "-1" + "' and '" + year + "-" + month + "-" + db.getLastDate(Int32.Parse(db.getMOnth(month)), Int32.Parse(year)) + "' and empid='" + id + "'", sqlconn).ExecuteReader();
                //    if (reader.Read())
                //    {
                //        commisH = reader.GetInt32(0);
                //    }
                //    sqlconn.Close();
                //}
                //catch (Exception)
                //{
                //    sqlconn.Close();
                //}

                try
                {
                    bringFrontD = 0;
                    sqlconn.Open();

                    var a = Convert.ToDateTime(db.getMOnth(month) + "/1" + "/" + year).AddMonths(-1);
                    //MessageBox.Show(a.Year + "/" + a.Month);
                    reader = new SqlCommand("select balance from paysheet where month = '" + a.Year + "/" + db.getMOnthName(a.Month + "") + "' and empid='" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        bringFrontD = reader.GetDouble(0);
                    }
                    sqlconn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                    sqlconn.Close();
                }

                try
                {
                    epf8D = 0;
                    grossSalaryD = 0;
                    punishD = 0;
                    advancedD = 0;
                    LoanD = 0;
                    bankingAmountD = 0;
                    salaryD = 0;
                    AllowanceD = 0;
                    mealAllowanceD = 0;
                    epf12 = 0;
                    etf3 = 0;
                    incentiveD = 0;
                    epfNo = "";
                    epfH = false;
                    sqlconn.Open();

                    var a = Convert.ToDateTime("1/" + db.getMOnth(month) + "/" + year).AddMonths(-1);
                    reader = new SqlCommand("select epf,gross,punish,advanced,loan,bankingAmount,epfBasic,allowances,meal,incentive,epfno,isEpf from emp where empid = '" + id + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        epf8D = reader.GetDouble(0);
                        grossSalaryD = reader.GetDouble(1);
                        punishD = reader.GetDouble(2);
                        advancedD = reader.GetDouble(3);
                        LoanD = reader.GetDouble(4);
                        bankingAmountD = reader.GetDouble(5);
                        salaryD = reader.GetDouble(6);
                        AllowanceD = reader.GetDouble(7);
                        mealAllowanceD = reader.GetDouble(8);
                        incentiveD = reader.GetDouble(9);
                        epfH = reader.GetBoolean(11);
                        epfNo = reader[10] + "";
                    }
                    sqlconn.Close();
                }
                catch (Exception)
                {
                    sqlconn.Close();
                }
                if (workingDaysH > 15)
                {
                    if (id.Equals("3"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }

                    if (id.Equals("2"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("1"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("17"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("12"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("13"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("0"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("24"))
                    {
                        mealAllowanceD = mealAllowanceD + 5000;
                    }
                    if (id.Equals("5"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }
                    if (id.Equals("21"))
                    {
                        mealAllowanceD = mealAllowanceD + 3000;
                    }
                }
                if (!BonusH)
                {
                    bonusD = 0;
                }
                advancedD = advancedD + payAdvanedH;
                // MessageBox.Show(workingDaysH+"");
                bankingAmountD = bankingAmountD;
                //MessageBox.Show(bankingAmountD + "");
                //  salaryD = salaryD * workingDaysH;
                punishD = 0;

                if (workingDaysH > 25)
                {
                    if (id.Equals("6") || id.Equals("2") || id.Equals("19") || id.Equals("21"))
                    {
                    }
                    else
                    {
                        incentiveD = ((salaryD + AllowanceD) / 25) * (workingDaysH - 25);
                    }
                    if (epfH)
                    {
                        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                    }
                }
                else if (workingDaysH < 25)
                {
                    if (id.Equals("6") || id.Equals("2") || id.Equals("19") || id.Equals("21"))
                    {
                    }
                    else
                    {
                        punishD = ((salaryD + AllowanceD) / 25) * (25 - workingDaysH);
                        absentH = 25 - workingDaysH;
                    }

                    if (epfH)
                    {
                        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                    }
                }
                else
                {
                    if (epfH)
                    {
                        epf8D = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 8;
                        epf12 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 12;
                        etf3 = ((((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) / 100) * 3;
                    }
                }
                //  punishD = punishD * absentH;

                grossSalaryD = bankingAmountD + salaryD + AllowanceD + mealAllowanceD + incentiveD;
                totalEaringH = grossSalaryD + bonusD + bringFrontD + commisH;

                // punishD=
                totalDeductionH = epf8D + (punishD) + advancedD + loanAmount;

                NetSalaryH = totalEaringH - totalDeductionH;

                BalanceH = NetSalaryH - PayH;
                if (!(workingDaysH == 0 && NetSalaryH == 0))
                {
                    sqlconn.Open();
                    new SqlCommand("insert into paysheet values ('" + id + "','" + year + "/" + month + "','" + workingDaysH + "','" + absentH + "','" + salaryD + "','" + bankingAmountD + "','" + AllowanceD + "','" + mealAllowanceD + "','" + incentiveD + "','" + grossSalaryD + "','" + bonusD + "','" + bringFrontD + "','" + totalEaringH + "','" + epf8D + "','" + punishD + "','" + loanAmount + "','" + advancedD + "','" + totalDeductionH + "','" + NetSalaryH + "','" + PayH + "','" + BalanceH + "','" + commisH + "','" + epf12 + "','" + etf3 + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();
                    sqlconn.Open();
                    new SqlCommand("insert into paysheetAudit values ('" + id + "','" + year + "/" + month + "','" + epfNo + "','" + ((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + "','" + (3500 - (((3500) / 25) * absentH)) + "','" + (AllowanceD / 100) * 45 + "','" + (AllowanceD / 100) * 55 + "','" + incentiveD + "','" + mealAllowanceD + "','" + punishD + "','" + grossSalaryD + "','" + (((salaryD - 3500) - ((salaryD - 3500) / 25) * absentH) + ((3500 - (((3500) / 25) * absentH)))) + "','" + epf12 + "','" + etf3 + "','" + (totalEaringH + epf12 + etf3) + "','" + advancedD + "','" + loanAmount + "','" + epf8D + "','" + 0 + "','" + totalDeductionH + "','" + NetSalaryH + "')", sqlconn).ExecuteNonQuery();
                    sqlconn.Close();
                }

                //getSalaru(id, month, year);
                // MessageBox.Show("Process Completed");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }
    }
}