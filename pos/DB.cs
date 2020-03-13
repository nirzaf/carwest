using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace pos
{
    internal class DB
    {
        public home home;
        public itemProfile itemProfile;
        private SqlConnection conn, conn2, conn3, conn4, conn5;
        private SqlDataReader reader, reader2, reader3, reader4, reader5;
        private DB db, db2, db3, db4, db5;
        private string[] readfromAddress;

        public SqlConnection createSqlConnection2()
        {
            try
            {
                conn = new SqlConnection();
                readfromAddress = File.ReadAllLines("D:\\configSettings.txt");
                conn.ConnectionString = "User ID='" + readfromAddress[4].Split('/')[0] + "';Password='" + readfromAddress[4].Split('/')[1] + "';data source='" + readfromAddress[4].Split('/')[2] + "';" + "persist security info=true;initial catalog='" + readfromAddress[4].Split('/')[3] + "';Connect Timeout=30";
                // conn.ConnectionString = "User ID='" + readfromAddress[0].Split('/')[0] + "';Password='" + readfromAddress[0].Split('/')[1] + "';data source='" + readfromAddress[0].Split('/')[2]+ "';" + "persist security info=true;initial catalog='" + readfromAddress[0].Split('/')[3] + "';Connect Timeout=0";

                // conn.ConnectionString = "User ID='" + readfromAddress[0].Split('/')[0] + "';Password='" + readfromAddress[0].Split('/')[1] + "';data source='" + readfromAddress[0].Split('/')[2] + "';" + "persist security info=true;initial catalog='" + readfromAddress[0].Split('/')[3] + "';Connect Timeout=0";

                //    conn.ConnectionString = "integrated security=SSPI;User ID=Mahe;Password=123;data source=Mahe;" + "persist security info=true;initial catalog=RasaBojun";
                //   conn.ConnectionString = "User ID='" + "Mahe" + "';Password='" + "456" + "';data source='" + "456" + "';" + "persist security info=true;initial catalog='" + "RasaBojun" + "';Connect Timeout=30";

                // conn.Open();

                return conn;
            }
            catch (Exception abc)
            {
                MessageBox.Show(abc.Message + "/" + abc.StackTrace + "/" + readfromAddress[4]);
                return conn;
            }
        }

        private Double cashOut_, cashBf, invest, amount, total, tempTOtalSale, tempCredistSale, tempChequeSale, tempCardSale, tempCashSale, tempExpen, tempCashRecevied, tempCashGiven, tempCashPaidReturn;

        public string setItemDescriptionCusSupp(TextBox[] textBox)
        {
            itemDescriptionName = "";
            foreach (var item in textBox)
            {
                if (!item.Text.Equals(""))
                {
                    if (itemDescriptionName.Equals(""))
                    {
                        itemDescriptionName = item.Text;
                    }
                    else
                    {
                        itemDescriptionName = itemDescriptionName + " " + item.Text;
                    }
                }
            }

            return itemDescriptionName;
        }

        public void setCashBalance(DateTime date)
        {
            try
            {
                db = new DB();
                conn = db.createSqlConnection();

                db2 = new DB();
                conn2 = db2.createSqlConnection();

                db3 = new DB();
                conn3 = db3.createSqlConnection();

                db4 = new DB();
                conn4 = db4.createSqlConnection();

                db5 = new DB();
                conn5 = db5.createSqlConnection();
                db.setCursoerWait();
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
                cashOut_ = 0;

                try
                {
                    amount = 0;
                    db.setCursoerWait();
                    conn.Open();
                    reader = new SqlCommand("select * from cashBF where date='" + date + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        cashBf = reader.GetDouble(1);
                        amount = amount + reader.GetDouble(1);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' and remark='" + "INVEST-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount + reader.GetDouble(2);
                        invest = invest + reader.GetDouble(2);
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' order by remark", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn3.Open();
                        //reader3 = new SqlCommand("select * from invoiceRetail where id='" + reader[1].ToString().Split('-')[1] + "' and retail='" + true + "'", conn3).ExecuteReader();
                        //if (reader3.Read())
                        {
                            conn3.Close();
                            if (reader.GetString(0).Equals("New Invoice"))
                            {
                                amount = amount + reader.GetDouble(2);
                                try
                                {
                                    conn3.Open();
                                    //          MessageBox.Show("A"+reader[1].ToString().Split('-')[1]+"A");
                                    reader3 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader[1].ToString().Split('-')[1] + "'  ", conn3).ExecuteReader();
                                    if (reader3.Read())
                                    {
                                        if (reader3.GetBoolean(3))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select cheque,amount,cutomerId from chequeInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                //  MessageBox.Show(reader[1]+"/"+reader2[2]);

                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempChequeSale = tempChequeSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(4))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select paid,amount,cutomerID from cardInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCardSale = tempCardSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else if (reader3.GetBoolean(2))
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select balance,amount,customerId from creditInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader2.GetDouble(1);
                                                    tempCredistSale = tempCredistSale + reader2.GetDouble(0);
                                                    tempCashSale = tempCashSale + (reader2.GetDouble(1) - reader2.GetDouble(0));
                                                    //  MessageBox.Show((reader2.GetDouble(1) - reader2.GetDouble(0))+"");
                                                }
                                                conn4.Close();
                                            }

                                            conn2.Close();
                                        }
                                        else
                                        {
                                            conn2.Open();
                                            reader2 = new SqlCommand("select amount,amount,cutomerID from cashInvoiceRetail where invoiceid='" + reader[1].ToString().Split('-')[1] + "'", conn2).ExecuteReader();
                                            if (reader2.Read())
                                            {
                                                conn4.Open();
                                                reader4 = new SqlCommand("select company from customer where id='" + reader2[2] + "'", conn4).ExecuteReader();
                                                if (reader4.Read())
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                                }
                                                else
                                                {
                                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                                }
                                                conn4.Close();
                                            }
                                            conn2.Close();
                                        }
                                        conn3.Close();
                                    }
                                }
                                catch (Exception a)
                                {
                                    tempTOtalSale = tempTOtalSale + reader.GetDouble(2);
                                    tempCashSale = tempCashSale + reader.GetDouble(2);
                                    //  MessageBox.Show(a.Message+"/"+a.StackTrace);
                                    conn2.Close();
                                }
                            }
                        }
                        conn3.Close();
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid"))
                            {
                                amount = amount + reader.GetDouble(2);
                                tempCashRecevied = tempCashRecevied + reader.GetDouble(2);
                            }
                            else if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid Card"))
                            {
                                amount = amount + reader.GetDouble(2);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("Invoice Credit Paid Cheque"))
                            {
                                amount = amount + reader.GetDouble(2);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.GetString(0).Split('-')[0].ToString().Equals("GRN Credit Paid"))
                            {
                                amount = amount - reader.GetDouble(2);
                                tempCashGiven = tempCashGiven + reader.GetDouble(2);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' and remark='" + "EXPENCES-MANUAL" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        tempExpen = tempExpen + reader.GetDouble(2);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' and remark='" + "CASH OUT" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        cashOut_ = cashOut_ + reader.GetDouble(2);
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from cashSummery where date='" + date + "' and reason='" + "CASH PAID" + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amount = amount - reader.GetDouble(2);
                        tempCashPaidReturn = tempCashPaidReturn + reader.GetDouble(2);
                    }
                    conn.Close();
                    //  conn.Open();

                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }

                //  MessageBox.Show("3");

                {
                    conn.Open();
                    new SqlCommand("delete from cashBF where date='" + date + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into cashBF values ('" + date + "','" + ((tempCashSale + tempCashRecevied + invest + cashBf) - (tempCashGiven + tempCashPaidReturn + tempExpen + cashOut_)) + "')", conn).ExecuteNonQuery();
                    conn.Close();
                }
                conn.Open();

                conn.Close();

                db.setCursoerDefault();
            }
            catch (Exception s)
            {
                MessageBox.Show("aaaaaaaaaaaaaaaaaa " + s.StackTrace + "//" + s.Message);
                // throw;
            }
        }

        public void loadLikeTextCustomer(SqlConnection connection, SqlDataReader reader, ListBox listBox, TextBox textBox)
        {
            try
            {
                listBox.Items.Clear();
                if (!textBox.Text.Equals(""))
                {
                    reader = new SqlCommand("select description from customer  where description like '%" + textBox.Text + "%' ", connection).ExecuteReader();
                    while (reader.Read())
                    {
                        listBox.Items.Add(CultureInfo.CurrentCulture.TextInfo.ToUpper(reader[0].ToString().ToLower()));
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void loadLikeTextSupplier(SqlConnection connection, SqlDataReader reader, ListBox listBox, TextBox textBox)
        {
            try
            {
                listBox.Items.Clear();
                if (!textBox.Text.Equals(""))
                {
                    reader = new SqlCommand("select description from supplier  where description like '%" + textBox.Text + "%' ", connection).ExecuteReader();
                    while (reader.Read())
                    {
                        listBox.Items.Add(CultureInfo.CurrentCulture.TextInfo.ToUpper(reader[0].ToString().ToLower()));
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public void ToTitleCase(TextBox[] textBox)
        {
            foreach (var item in textBox)
            {
                item.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Text.ToLower());
                //   item.Select(item.Text.Length, 0);
            }
        }

        public void ToUpperCase(TextBox[] textBox)
        {
            foreach (var item in textBox)
            {
                item.CharacterCasing = CharacterCasing.Upper;
                //   item.Select(item.Text.Length, 0);
            }
        }

        public SqlConnection createSqlConnection()
        {
            try
            {
                conn = new SqlConnection();
                string[] readfromAddress = File.ReadAllLines("D:\\configSettings.txt");
                conn.ConnectionString = "User ID='" + readfromAddress[0].Split('/')[0] + "';Password='" + readfromAddress[0].Split('/')[1] + "';data source='" + readfromAddress[0].Split('/')[2] + "';" + "persist security info=true;initial catalog='" + readfromAddress[0].Split('/')[3] + "';Connect Timeout=0";
                // conn.ConnectionString = "User ID='" + readfromAddress[0].Split('/')[0] + "';Password='" + readfromAddress[0].Split('/')[1] + "';data source='" + readfromAddress[0].Split('/')[2]  + "';" + "persist security info=true;initial catalog='" + readfromAddress[0].Split('/')[3] + "';Connect Timeout=0";

                //    conn.ConnectionString = "integrated secuDity=SSPI;User ID=Mahe;Password=123;data source=Mahe;" + "persist security info=true;initial catalog=RasaBojun";
                //   conn.ConnectionString = "User ID='" + "Mahe" + "';Password='" + "456" + "';data source='" + "456" + "';" + "persist security info=true;initial catalog='" + "RasaBojun" + "';Connect Timeout=30";

                //conn.Open();

                return conn;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);

                //throw;
                return conn;
            }
        }

        private Point p;
        private String itemDescriptionName;

        public string setItemDescriptionNameByName(String[] textBox)
        {
            itemDescriptionName = "";
            foreach (var item in textBox)
            {
                if (!item.Equals(""))
                {
                    if (itemDescriptionName.Equals(""))
                    {
                        itemDescriptionName = item;
                    }
                    else
                    {
                        itemDescriptionName = itemDescriptionName + " " + item;
                    }
                }
            }
            return itemDescriptionName;
        }

        public string setItemDescriptionName(TextBox[] textBox)
        {
            itemDescriptionName = "";
            foreach (var item in textBox)
            {
                if (!item.Text.Equals(""))
                {
                    if (itemDescriptionName.Equals(""))
                    {
                        itemDescriptionName = item.Text;
                    }
                    else
                    {
                        itemDescriptionName = itemDescriptionName + " " + item.Text;
                    }
                }
            }
            return itemDescriptionName;
        }

        public void setOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        public string setItemDescriptionNamebyString(ArrayList arraylist)
        {
            itemDescriptionName = "";
            foreach (var item in arraylist)
            {
                if (!item.Equals(""))
                {
                    if (itemDescriptionName.Equals(""))
                    {
                        itemDescriptionName = item + "";
                    }
                    else
                    {
                        itemDescriptionName = itemDescriptionName + " " + item;
                    }
                }
            }

            return itemDescriptionName;
        }

        public void setList(ListBox listBox1, TextBox pcCode, Int32 width)
        {
            listBox1.Visible = false;
            listBox1.Width = width;
            listBox1.Height = 150;
            p = pcCode.Location;
            p.Y += pcCode.Height;
            listBox1.Location = p;
            listBox1.BringToFront();
        }

        public void setTable(DataGridView listBox1, TextBox pcCode, Int32 width)
        {
            listBox1.Visible = false;
            listBox1.Width = width;
            listBox1.Height = 150;
            p = pcCode.Location;
            p.Y += pcCode.Height;
            listBox1.Location = p;
            listBox1.BringToFront();
        }

        public void setAutoComplete(TextBox textBox, string[] source)
        {
            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.AllowDrop = true;
            textBox.AutoCompleteCustomSource.Clear();
            textBox.AutoCompleteCustomSource.AddRange(source);
            textBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        public void setAutoComplete2(ToolStripTextBox textBox, string[] source)
        {
            textBox.CharacterCasing = CharacterCasing.Upper;
            textBox.AllowDrop = true;
            textBox.AutoCompleteCustomSource.Clear();
            textBox.AutoCompleteCustomSource.AddRange(source);
            textBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        public void setCursoerWait()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void setCursoerDefault()
        {
            Cursor.Current = Cursors.Default;
        }

        public void setTextBoxPath(TextBox keyUp, TextBox keyDown, TextBox enter, Int32 value)
        {
            if (value == 12 | value == 13)
            {
                enter.Focus();
                enter.SelectionLength = enter.TextLength;
            }
            else if (value == 38)
            {
                keyUp.Focus();
                keyUp.SelectionLength = keyUp.TextLength;
            }
            else if (value == 40)
            {
                keyDown.Focus();
                keyDown.SelectionLength = keyDown.TextLength;
            }
        }

        public void setTextBoxDefault(TextBox[] textBox)
        {
            foreach (var item in textBox)
            {
                item.Text = "";
            }
        }

        public string getMOnth(string y)
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

        public string getMOnthName(string y)
        {
            string month = "";
            y = Int32.Parse(y) + "";
            if (y.Equals("1"))
            {
                month = "January";
            }
            else if (y.Equals("2"))
            {
                month = "February";
            }
            else if (y.Equals("3"))
            {
                month = "March";
            }
            else if (y.Equals("4"))
            {
                month = "April";
            }
            else if (y.Equals("5"))
            {
                month = "May";
            }
            else if (y.Equals("6"))
            {
                month = "June";
            }
            else if (y.Equals("7"))
            {
                month = "July";
            }
            else if (y.Equals("8"))
            {
                month = "August";
            }
            else if (y.Equals("9"))
            {
                month = "September";
            }
            if (y.Equals("10"))
            {
                month = "October";
            }
            if (y.Equals("11"))
            {
                month = "November";
            }
            if (y.Equals("12"))
            {
                month = "December";
            }

            return month;
        }

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

        private String amountI, lastDate;
        private Double amountD;

        public string setAmountFormat(string amount)
        {
            if (!amount.Equals(""))
            {
                amountI = (int)Double.Parse(amount) + "";

                amountD = Double.Parse(amount);
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
            }
            else
            {
                amount = "";
            }

            return amount;
        }
    }
}