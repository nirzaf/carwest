using System;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    internal class companySalary
    {
        private double ot;
        private DB db, db2, db3, db4, db5, db6;
        private SqlDataReader reader, reader2, reader5, reader4, reader6, reader3;
        private SqlConnection sqlconn, sqlconn2, sqlconn3, sqlconn5, sqlconn4, sqlconn6;

        private DateTime dateTime1;

        private bool stateus;
        private ArrayList dates = new ArrayList();

        public string setTimeSheet(string id, string month, string year, string AttandanceType, string empID, string lastDate)
        {
            try
            {
                db5 = new DB();
                sqlconn5 = db5.createSqlConnection();
                db = new DB();
                sqlconn = db.createSqlConnection2();
                db4 = new DB();
                sqlconn4 = db4.createSqlConnection2();
                db6 = new DB();
                sqlconn6 = db6.createSqlConnection2();
                sqlconn4.Open();
                reader4 = new SqlCommand("select * from deleteattandance ", sqlconn4).ExecuteReader();
                while (reader4.Read())
                {
                    sqlconn6.Open();
                    new SqlCommand("delete from checkinout where userid='" + reader4[1] + "' and checktime='" + reader4.GetDateTime(0) + "'", sqlconn6).ExecuteNonQuery();
                    sqlconn6.Close();
                }
                sqlconn4.Close();

                db = new DB();
                sqlconn = db.createSqlConnection();
                db4 = new DB();
                sqlconn4 = db4.createSqlConnection();
                db2 = new DB();
                sqlconn2 = db2.createSqlConnection();
                db3 = new DB();
                sqlconn3 = db3.createSqlConnection();
                sqlconn4.Open();
                if (AttandanceType.Equals("dayBased"))
                {
                    reader = new SqlCommand("select lunch from dayBasedAttandance where empid='" + id + "'", sqlconn4).ExecuteReader();
                }
                else
                {
                    reader = new SqlCommand("select lunch from TimeBasedAttandance where empid='" + id + "'", sqlconn4).ExecuteReader();
                }

                if (reader.Read())
                {
                    if (reader.GetBoolean(0))
                    {
                        reader.Close();
                        sqlconn4.Close();
                        for (int i = 1; i <= Int32.Parse(lastDate); i++)
                        {
                            dates = new ArrayList();

                            sqlconn2.Open();
                            new SqlCommand("delete from timesheet where empid='" + id + "' and date = '" + year + "-" + month + "-" + i + "' ", sqlconn2).ExecuteNonQuery();

                            sqlconn2.Close();

                            sqlconn6.Open();
                            reader6 = new SqlCommand("select checkTime from CheckInout  where userid= (select userid from userinfo where badgenumber='" + empID + "')  and checkTime between '" + year + "-" + month + "-" + i + " 00:00:00.000" + "' and '" + year + "-" + month + "-" + i + " 23:59:00.000" + "' order by checktime", sqlconn6).ExecuteReader();

                            bool loop = true, loop2 = true;

                            for (int ia = 0; ia < 6; ia++)
                            {
                                if (reader6.Read())
                                {
                                    dates.Add(reader6[0].ToString().Split(' ')[1]);
                                }
                            }

                            sqlconn6.Close();

                            sqlconn2.Open();
                            if (dates.Count == 0)
                            {
                                new SqlCommand("insert into timesheet values ('" + id + "','" + "00:00" + "','" + "00:00" + "','" + year + "-" + month + "-" + i + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                            }
                            else if (dates.Count == 1)
                            {
                                new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + "00:00" + "','" + year + "-" + month + "-" + i + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                            }
                            else if (dates.Count == 2)
                            {
                                new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[1].ToString() + "','" + year + "-" + month + "-" + i + "','" + getLate(dates[0].ToString(), dates[1].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + getOt(dates[0].ToString(), dates[1].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + workHours(dates[0].ToString(), dates[1].ToString()) + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                            }
                            else if (dates.Count == 3)
                            {
                                new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[1].ToString() + "','" + year + "-" + month + "-" + i + "','" + getLate(dates[0].ToString(), dates[1].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + getOt(dates[0].ToString(), dates[1].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + workHours(dates[0].ToString(), dates[1].ToString()) + "','" + dates[2] + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                            }
                            else if (dates.Count == 4)
                            {
                                reader2 = new SqlCommand("select period from dayoff where empid='" + id + "' and  date ='" + year + "-" + month + "-" + i + "'", sqlconn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    reader2.Close();

                                    new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[3].ToString() + "','" + year + "-" + month + "-" + i + "','" + getLate(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + getOt(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + workHours(dates[0].ToString(), dates[3].ToString()) + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                                }
                                else
                                {
                                    reader2.Close();
                                    TimeSpan t1, t2, t3, t4;
                                    t1 = getLate(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i);

                                    t2 = workHours(dates[1].ToString(), dates[2].ToString());
                                    if (t2 > TimeSpan.Parse("03:00"))
                                    {
                                        t1 = t1 + (t2 - TimeSpan.Parse("03:00"));
                                    }

                                    t3 = getOt(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i);
                                    t4 = workHours(dates[1].ToString(), dates[2].ToString());
                                    if (t4 < TimeSpan.Parse("03:00"))
                                    {
                                        t3 = t3 + (-t4 + TimeSpan.Parse("03:00"));
                                    }

                                    t2 = workHours(dates[0].ToString(), dates[3].ToString());
                                    t4 = workHours(dates[1].ToString(), dates[2].ToString());
                                    t2 -= t4;

                                    new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[1].ToString() + "','" + year + "-" + month + "-" + i + "','" + t1 + "','" + t3 + "','" + t2 + "','" + dates[2] + "','" + dates[3] + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                reader2 = new SqlCommand("select period from dayoff where empid='" + id + "' and  date ='" + year + "-" + month + "-" + i + "'", sqlconn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    reader2.Close();

                                    new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[3].ToString() + "','" + year + "-" + month + "-" + i + "','" + getLate(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + getOt(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i) + "','" + workHours(dates[0].ToString(), dates[3].ToString()) + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                                }
                                else
                                {
                                    reader2.Close();
                                    TimeSpan t1, t2, t3, t4;
                                    t1 = getLate(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i);

                                    t2 = workHours(dates[1].ToString(), dates[2].ToString());
                                    if (t2 > TimeSpan.Parse("03:00"))
                                    {
                                        t1 = t1 + (t2 - TimeSpan.Parse("03:00"));
                                    }

                                    t3 = getOt(dates[0].ToString(), dates[3].ToString(), AttandanceType, id, year + "-" + month + "-" + i);
                                    t4 = workHours(dates[1].ToString(), dates[2].ToString());
                                    if (t4 < TimeSpan.Parse("03:00"))
                                    {
                                        t3 = t3 + (-t4 + TimeSpan.Parse("03:00"));
                                    }

                                    t2 = workHours(dates[0].ToString(), dates[3].ToString());
                                    t4 = workHours(dates[1].ToString(), dates[2].ToString());
                                    t2 -= t4;

                                    new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString() + "','" + dates[1].ToString() + "','" + year + "-" + month + "-" + i + "','" + t1 + "','" + t3 + "','" + t2 + "','" + dates[2] + "','" + dates[3] + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                                }
                            }
                            sqlconn2.Close();
                        }
                    }
                    else
                    {
                        reader.Close();
                        sqlconn4.Close();
                        reader = new SqlCommand("select checkTime from CheckInout  where userid= (select userid from userinfo where badgenumber='" + empID + "')  and checkTime between '" + year + "-" + month + "-1 00:00:00.000" + "' and '" + year + "-" + month + "-" + lastDate + " 23:59:00.000" + "' order by checktime", sqlconn).ExecuteReader();

                        bool loop = true, loop2 = true;
                        sqlconn2.Open();
                        new SqlCommand("delete from timesheet where empid='" + id + "' and date like '" + year + "-" + month + "%' ", sqlconn2).ExecuteNonQuery();

                        sqlconn2.Close();

                        if (reader.HasRows)
                        {
                            while (loop)
                            {
                                loop = reader.Read();
                                if (!loop)
                                {
                                    sqlconn2.Open();
                                    if (dates.Count == 1)
                                    {
                                        new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString().Split(' ')[1] + "','" + "00:00" + "','" + dateTime1 + "','" + getLate("0", "0", AttandanceType, id, dateTime1 + "") + "','" + getOt("0", "0", AttandanceType, id, dateTime1 + "") + "','" + workHours("0", "0") + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "')", sqlconn2).ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString().Split(' ')[1] + "','" + dates[dates.Count - 1].ToString().Split(' ')[1] + "','" + dateTime1 + "','" + getLate(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1], AttandanceType, id, dateTime1 + "") + "','" + getOt(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1], AttandanceType, id, dateTime1 + "") + "','" + workHours(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1]) + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "')", sqlconn2).ExecuteNonQuery();
                                    }

                                    sqlconn2.Close();
                                }
                                else
                                {
                                    if (loop2)
                                    {
                                        dateTime1 = Convert.ToDateTime(reader.GetDateTime(0).ToString("d"));
                                        dates.Add(reader.GetDateTime(0));

                                        loop2 = false;
                                    }
                                    else
                                    {
                                        if (dateTime1.Equals(Convert.ToDateTime(reader.GetDateTime(0).ToString("d"))))
                                        {
                                            dates.Add(reader.GetDateTime(0));
                                        }
                                        else
                                        {
                                            sqlconn2.Open();
                                            if (dates.Count == 1)
                                            {
                                                new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString().Split(' ')[1] + "','" + "00:00" + "','" + dateTime1 + "','" + getLate("0", "0", AttandanceType, id, dateTime1 + "") + "','" + getOt("0", "0", AttandanceType, id, dateTime1 + "") + "','" + workHours("0", "0") + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "')", sqlconn2).ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                new SqlCommand("insert into timesheet values ('" + id + "','" + dates[0].ToString().Split(' ')[1] + "','" + dates[dates.Count - 1].ToString().Split(' ')[1] + "','" + dateTime1 + "','" + getLate(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1], AttandanceType, id, dateTime1 + "") + "','" + getOt(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1], AttandanceType, id, dateTime1 + "") + "','" + workHours(dates[0].ToString().Split(' ')[1], dates[dates.Count - 1].ToString().Split(' ')[1]) + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "','" + "00:00:00" + "')", sqlconn2).ExecuteNonQuery();
                                            }
                                            sqlconn2.Close();

                                            dateTime1 = Convert.ToDateTime(reader.GetDateTime(0).ToString("d"));
                                            dates = new ArrayList();
                                            dates.Add(reader.GetDateTime(0));
                                        }
                                    }
                                }
                            }
                        }
                        reader.Close();

                        sqlconn2.Open();
                        for (int i = 1; i <= Int32.Parse(lastDate); i++)
                        {
                            reader = new SqlCommand("select id from timesheet where date='" + year + "-" + month + "-" + i + "' and empid='" + id + "'", sqlconn2).ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                new SqlCommand("insert into timesheet values ('" + id + "','" + "00:00" + "','" + "00:00" + "','" + year + "-" + month + "-" + i + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "','" + "00:00" + "')", sqlconn2).ExecuteNonQuery();
                            }
                            reader.Close();
                        }
                        sqlconn2.Close();
                    }

                    sqlconn.Close();

                    sqlconn5.Open();
                    try
                    {
                        new SqlCommand("insert into processDate values ('" + id + "','" + DateTime.Now + "')", sqlconn5).ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        new SqlCommand("update processDate set date='" + DateTime.Now + "' where id='" + id + "'", sqlconn5).ExecuteNonQuery();
                    }

                    sqlconn5.Close();
                }
                sqlconn4.Close();
                sqlconn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + a.StackTrace);
                stateus = false;
                sqlconn.Close();
            }
            return lastDate;
        }

        private TimeSpan sapm; private Exception b;
        private Int32 period = 0;

        public TimeSpan getLate(string inTime, String outTime, string type, string id, string date)
        {
            sapm = TimeSpan.Parse("00:00");
            if (inTime.Equals("0") | outTime.Equals("0") | TimeSpan.Parse(inTime).TotalMinutes == 0 | TimeSpan.Parse(outTime).TotalMinutes == 0)
            {
                sapm = TimeSpan.Parse("00:00");
            }
            else
            {
                sqlconn5.Open();

                reader5 = new SqlCommand("select period from dayoff where empid='" + id + "' and date='" + date + "' ", sqlconn5).ExecuteReader();

                if (reader5.Read())
                {
                    period = reader5.GetInt32(0);

                    if (type.Equals("timeBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from TimeBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (period == 1)
                            {
                                if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17)))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else
                                {
                                    sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17));
                                }

                                if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(16))
                                {
                                    sapm = sapm + (reader2.GetTimeSpan(16) - TimeSpan.Parse(outTime));
                                }

                                if (sapm > (reader2.GetTimeSpan(16) - (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17))))
                                {
                                    sapm = (reader2.GetTimeSpan(16) - (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17)));
                                }
                            }
                            else if (period == 2)
                            {
                                if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21)))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else
                                {
                                    sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21));
                                }

                                if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(20))
                                {
                                    sapm = sapm + (reader2.GetTimeSpan(20) - TimeSpan.Parse(outTime));
                                }

                                if (sapm > (reader2.GetTimeSpan(20) - (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21))))
                                {
                                    sapm = (reader2.GetTimeSpan(20) - (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21)));
                                }
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (period == 1)
                            {
                                if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(14) + reader2.GetTimeSpan(16)))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else
                                {
                                    sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(14) + reader2.GetTimeSpan(16));
                                }

                                if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(15))
                                {
                                    sapm = sapm + (reader2.GetTimeSpan(15) - TimeSpan.Parse(outTime));
                                }

                                if (sapm > (reader2.GetTimeSpan(15) - (reader2.GetTimeSpan(14) + reader2.GetTimeSpan(16))))
                                {
                                    sapm = (reader2.GetTimeSpan(15) - (reader2.GetTimeSpan(14) + reader2.GetTimeSpan(16)));
                                }
                            }
                            else
                            {
                                if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(18) + reader2.GetTimeSpan(20)))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }
                                else
                                {
                                    sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(18) + reader2.GetTimeSpan(20));
                                }

                                if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(19))
                                {
                                    sapm = sapm + (reader2.GetTimeSpan(19) - TimeSpan.Parse(outTime));
                                }

                                if (sapm > (reader2.GetTimeSpan(19) - (reader2.GetTimeSpan(18) + reader2.GetTimeSpan(20))))
                                {
                                    sapm = (reader2.GetTimeSpan(19) - (reader2.GetTimeSpan(18) + reader2.GetTimeSpan(20)));
                                }
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("shiftBased"))
                    {
                        sqlconn.Open();

                        reader2 = new SqlCommand("select * from shiftBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(11) < sapm)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = (reader2.GetTimeSpan(11) - sapm);
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBasedShift"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedShiftAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(10) < sapm)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = (reader2.GetTimeSpan(10) - sapm);
                            }
                        }
                        sqlconn.Close();
                    }
                }
                else
                {
                    if (type.Equals("timeBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from TimeBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(3)))
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(3));
                            }

                            if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(2))
                            {
                                sapm = sapm + (reader2.GetTimeSpan(2) - TimeSpan.Parse(outTime));
                            }

                            if (sapm > (reader2.GetTimeSpan(2) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(3))))
                            {
                                sapm = (reader2.GetTimeSpan(2) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(3)));
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if ((TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime)).TotalHours < 0)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else if (TimeSpan.Parse(inTime) <= (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(4)))
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = TimeSpan.Parse(inTime) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(4));
                            }

                            if (TimeSpan.Parse(outTime) <= reader2.GetTimeSpan(2))
                            {
                                sapm = sapm + (reader2.GetTimeSpan(2) - TimeSpan.Parse(outTime));
                            }

                            if (sapm > (reader2.GetTimeSpan(2) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(4))))
                            {
                                sapm = (reader2.GetTimeSpan(2) - (reader2.GetTimeSpan(1) + reader2.GetTimeSpan(4)));
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("shiftBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from shiftBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(1) < sapm)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = (reader2.GetTimeSpan(1) - sapm);
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBasedShift"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedShiftAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(1) < sapm)
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                            else
                            {
                                sapm = (reader2.GetTimeSpan(1) - sapm);
                            }
                        }
                        sqlconn.Close();
                    }
                }
                sqlconn5.Close();
            }
            return sapm;
        }

        public TimeSpan getOt(string inTime, String outTime, string type, string id, string date)
        {
            sapm = TimeSpan.Parse("00:00");
            if (inTime.Equals("0") | outTime.Equals("0") | TimeSpan.Parse(inTime).TotalMinutes == 0 | TimeSpan.Parse(outTime).TotalMinutes == 0)
            {
                sapm = TimeSpan.Parse("00:00");
            }
            else
            {
                sqlconn5.Open();
                reader5 = new SqlCommand("select period from dayoff where empid='" + id + "' and date='" + date + "' ", sqlconn5).ExecuteReader();

                if (reader5.Read())
                {
                    period = reader5.GetInt32(0);
                    if (type.Equals("timeBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from TimeBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (period == 1)
                            {
                                sapm = (reader2.GetTimeSpan(15)) - TimeSpan.Parse(inTime);
                                if (sapm < TimeSpan.Parse("00:00"))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }

                                if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(16) + reader2.GetTimeSpan(18))) > TimeSpan.Parse("00:00"))
                                {
                                    sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(16) + reader2.GetTimeSpan(18))));
                                }
                            }
                            else if (period == 2)
                            {
                                sapm = (reader2.GetTimeSpan(15)) - TimeSpan.Parse(inTime);
                                if (sapm < TimeSpan.Parse("00:00"))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }

                                if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(16) + reader2.GetTimeSpan(18))) > TimeSpan.Parse("00:00"))
                                {
                                    sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(16) + reader2.GetTimeSpan(18))));
                                }
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (period == 1)
                            {
                                sapm = (reader2.GetTimeSpan(14)) - TimeSpan.Parse(inTime);
                                if (sapm < TimeSpan.Parse("00:00"))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }

                                if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17))) > TimeSpan.Parse("00:00"))
                                {
                                    sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(15) + reader2.GetTimeSpan(17))));
                                }
                            }
                            else
                            {
                                sapm = (reader2.GetTimeSpan(18)) - TimeSpan.Parse(inTime);
                                if (sapm < TimeSpan.Parse("00:00"))
                                {
                                    sapm = TimeSpan.Parse("00:00");
                                }

                                if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21))) > TimeSpan.Parse("00:00"))
                                {
                                    sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(19) + reader2.GetTimeSpan(21))));
                                }
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("shiftBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from shiftBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(11) < sapm)
                            {
                                sapm = (sapm - reader2.GetTimeSpan(11));
                            }
                            else
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBasedShift"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedShiftAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(10) < sapm)
                            {
                                sapm = (sapm - reader2.GetTimeSpan(10));
                            }
                            else
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                        }
                        sqlconn.Close();
                    }
                }
                else
                {
                    if (type.Equals("timeBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from TimeBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = (reader2.GetTimeSpan(1)) - TimeSpan.Parse(inTime);
                            if (sapm < TimeSpan.Parse("00:00"))
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }

                            if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(2) + reader2.GetTimeSpan(4))) > TimeSpan.Parse("00:00"))
                            {
                                sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(2) + reader2.GetTimeSpan(4))));
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedAttandance where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = (reader2.GetTimeSpan(1)) - TimeSpan.Parse(inTime);
                            if (sapm < TimeSpan.Parse("00:00"))
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }

                            if ((TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(2) + reader2.GetTimeSpan(3))) > TimeSpan.Parse("00:00"))
                            {
                                sapm = (sapm + (TimeSpan.Parse(outTime) - (reader2.GetTimeSpan(2) + reader2.GetTimeSpan(3))));
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("shiftBased"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from shiftBasedAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(1) < sapm)
                            {
                                sapm = (sapm - reader2.GetTimeSpan(1));
                            }
                            else
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                        }
                        sqlconn.Close();
                    }
                    else if (type.Equals("dayBasedShift"))
                    {
                        sqlconn.Open();
                        reader2 = new SqlCommand("select * from dayBasedShiftAttandance  where empid='" + id + "'", sqlconn).ExecuteReader();
                        if (reader2.Read())
                        {
                            sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
                            if (reader2.GetTimeSpan(1) < sapm)
                            {
                                sapm = (sapm - reader2.GetTimeSpan(1));
                            }
                            else
                            {
                                sapm = TimeSpan.Parse("00:00");
                            }
                        }
                        sqlconn.Close();
                    }
                }
                sqlconn5.Close();
            }
            return sapm;
        }

        public TimeSpan workHours(string inTime, String outTime)
        {
            if (inTime.Equals("0") | outTime.Equals("0") | TimeSpan.Parse(inTime).TotalMinutes == 0 | TimeSpan.Parse(outTime).TotalMinutes == 0)
            {
                sapm = TimeSpan.Parse("00:00");
            }
            else
            {
                sapm = TimeSpan.Parse(outTime) - TimeSpan.Parse(inTime);
            }
            return sapm;
        }
    }
}