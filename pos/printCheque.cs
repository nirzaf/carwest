using System;
using System.Data;
using System.Globalization;

namespace pos
{
    internal class printCheque
    {
        private string states = "";

        public string setprintCheque(string date, string amount, string payName, bool pabc)
        {
            try
            {
                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("month1", typeof(string));
                dt.Columns.Add("month2", typeof(string));
                dt.Columns.Add("date1", typeof(string));
                dt.Columns.Add("date2", typeof(string));
                dt.Columns.Add("year1", typeof(string));
                dt.Columns.Add("year2", typeof(string));
                dt.Columns.Add("amountNumeric", typeof(string));
                dt.Columns.Add("amountVarchar", typeof(string));
                string amountNew, amountBYNameNEw;
                try
                {
                    amountNew = setAmountFormat(amount);
                    amountBYNameNEw = new amountByName().setAmountName(amount);
                }
                catch (Exception)
                {
                    amountNew = "";
                    amountBYNameNEw = "";
                }

                //  MessageBox.Show(s);

                if (date.Equals(""))
                {
                    dt.Rows.Add("", "", "", "", "", "", amountNew, amountBYNameNEw);
                }
                else if (amount.Equals("") | payName.Equals(""))
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), "", "");
                }
                else
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), amountNew, amountBYNameNEw);
                }

                ds.Tables.Add(dt);

                //     ds.WriteXmlSchema("cheque.xml");
                if (pabc)
                {
                    chequeReportNew pp = new chequeReportNew();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    //  pp.SetParameterValue("suppllier", chequen);
                    pp.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    chequeReportNewHNB pp = new chequeReportNewHNB();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    //  pp.SetParameterValue("suppllier", chequen);
                    pp.PrintToPrinter(1, false, 0, 0);
                }

                //test3 a = new test3();
                //a.pp = pp;
                //a.Visible = true;
                //crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                throw;
                states = s.Message;
            }
            return states;
        }

        public string setprintChequeAcPay(string date, string amount, string payName, bool pabc)
        {
            try
            {
                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("month1", typeof(string));
                dt.Columns.Add("month2", typeof(string));
                dt.Columns.Add("date1", typeof(string));
                dt.Columns.Add("date2", typeof(string));
                dt.Columns.Add("year1", typeof(string));
                dt.Columns.Add("year2", typeof(string));
                dt.Columns.Add("amountNumeric", typeof(string));
                dt.Columns.Add("amountVarchar", typeof(string));

                //  MessageBox.Show(s);

                if (date.Equals(""))
                {
                    dt.Rows.Add("", "", "", "", "", "", setAmountFormat(amount), new amountByName().setAmountName(amount));
                }
                else if (amount.Equals("") | payName.Equals(""))
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), "", "");
                }
                else
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                }

                ds.Tables.Add(dt);

                if (pabc)
                {
                    chequeReportNewACPay pp = new chequeReportNewACPay();

                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    pp.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    chequeReportNewACPayHNB pp = new chequeReportNewACPayHNB();

                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    pp.PrintToPrinter(1, false, 0, 0);
                }

                //   ds.WriteXmlSchema("cheque.xml");

                //   crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                states = s.Message;
            }
            return states;
        }

        public string setprintChequeCross(string date, string amount, string payName, bool pabc)
        {
            try
            {
                dt = new DataTable();
                ds = new DataSet();
                dt.Columns.Add("month1", typeof(string));
                dt.Columns.Add("month2", typeof(string));
                dt.Columns.Add("date1", typeof(string));
                dt.Columns.Add("date2", typeof(string));
                dt.Columns.Add("year1", typeof(string));
                dt.Columns.Add("year2", typeof(string));
                dt.Columns.Add("amountNumeric", typeof(string));
                dt.Columns.Add("amountVarchar", typeof(string));

                //  MessageBox.Show(s);

                if (date.Equals(""))
                {
                    dt.Rows.Add("", "", "", "", "", "", setAmountFormat(amount), new amountByName().setAmountName(amount));
                }
                else if (amount.Equals("") | payName.Equals(""))
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), "", "");
                }
                else
                {
                    if (date.Split('/')[0].ToString().ToCharArray().Length == 1)
                    {
                        month1 = "0";
                        month2 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        month1 = date.Split('/')[0].ToString().ToCharArray()[0].ToString();
                        month2 = date.Split('/')[0].ToString().ToCharArray()[1].ToString();
                    }
                    if (date.Split('/')[1].ToString().ToCharArray().Length == 1)
                    {
                        date1 = "0";
                        date2 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                    }
                    else
                    {
                        date1 = date.Split('/')[1].ToString().ToCharArray()[0].ToString();
                        date2 = date.Split('/')[1].ToString().ToCharArray()[1].ToString();
                    }
                    dt.Rows.Add(month1, month2, date1, date2, date.Split('/')[2].ToString().ToCharArray()[2].ToString(), date.Split('/')[2].ToString().ToCharArray()[3].ToString(), setAmountFormat(amount), new amountByName().setAmountName(amount));
                }

                ds.Tables.Add(dt);

                ///   ds.WriteXmlSchema("cheque.xml");
                ///   if
                ///
                if (pabc)
                {
                    chequeReportNewCross pp = new chequeReportNewCross();

                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    pp.PrintToPrinter(1, false, 0, 0);
                }
                else
                {
                    chequeReportNewCrossHNB pp = new chequeReportNewCrossHNB();

                    pp.SetDataSource(ds);
                    pp.SetParameterValue("name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(payName.ToLower()));
                    pp.PrintToPrinter(1, false, 0, 0);
                }
                //   crystalReportViewer1.ReportSource = pp;
            }
            catch (Exception s)
            {
                states = s.Message;
            }
            return states;
        }

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

        private string month1, month2, date1, date2; private DataTable dt; private DataSet ds;
    }
}