using System;
using System.Collections;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace pos
{
    public partial class grnSearchCash : Form
    {
        public grnSearchCash(Form form, String user)
        {
            InitializeComponent();
            this.user = user;
            home = form;
        }

        //+++++++My Variable Start
        private DB db, db2, db3, db4;

        private Form home;
        private SqlConnection conn, conn2, conn3, conn4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private ArrayList arrayList, stockList;
        private Boolean check, checkListBox, states, item, checkStock, isCompany;
        private string user, type, cutomerID = "", description, invoieNoTemp, query;
        private String[] idArray;
        private DataGridViewButtonColumn btn;
        private Int32 invoiceMaxNo, rowCount;
        private Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal;
        //+++++++++My Variable End

        //++My Method Start

        private void setAutoComplete()
        {
            try
            {
                //conn.Open();
                //reader = new SqlCommand("select customerid from " + query + " ", conn).ExecuteReader();
                //arrayList = new ArrayList();
                //while (reader.Read())
                //{
                //    // MessageBox.Show("m");
                //    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                //}
                //reader.Close();
                //idArray = arrayList.ToArray(typeof(string)) as string[];
                //db.setAutoComplete(cutomerUnSaved, idArray);
                //conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Columns[6].Visible = reader.GetBoolean(14);
                    dataGridView1.Columns[7].Visible = reader.GetBoolean(20);
                    dataGridView1.Columns[0].Visible = reader.GetBoolean(2);
                    isCompany = reader.GetBoolean(2);
                }
                reader.Close();
                conn.Close();
                if (isCompany)
                {
                    query = "invoiceRetail";
                }
                else
                {
                    query = "invoiceDump";
                }
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //+++++++My Method End
        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void invoiceSearch_Load(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("sa");
                db = new DB();
                conn = db.createSqlConnection();
                db2 = new DB();
                conn2 = db2.createSqlConnection();
                db3 = new DB();
                conn3 = db3.createSqlConnection();
                db4 = new DB();
                conn4 = db4.createSqlConnection();

                dataGridView1.AllowUserToAddRows = false;
                btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.Width = 50;
                btn.Text = "VIEW";

                btn.UseColumnTextForButtonValue = true;
                //btn = new DataGridViewButtonColumn();
                //dataGridView1.Columns.Add(btn);
                //btn.Width = 50;
                //btn.Text = "VIEW";
                //btn.UseColumnTextForButtonValue = true;

                //btn = new DataGridViewButtonColumn();
                //dataGridView1.Columns.Add(btn);
                //btn.Width = 60;
                //btn.Text = "CANSEL";
                //btn.UseColumnTextForButtonValue = true;

                //btn = new DataGridViewButtonColumn();
                //dataGridView1.Columns.Add(btn);
                //btn.Width = 60;
                //btn.Text = "RETURN";
                //btn.UseColumnTextForButtonValue = true;

                loadUser();
                setAutoComplete();
                checkBox1.Checked = true;
                checkBox1.Checked = false;

                this.TopMost = true;

                //  customer.CharacterCasing = CharacterCasing.Upper;
                cutomerUnSaved.CharacterCasing = CharacterCasing.Upper;
                //dataGridView1.Columns[5].Visible = false;
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void invoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                from.Enabled = true;
                toDate.Enabled = true;
            }
            else
            {
                from.Enabled = false;
                toDate.Enabled = false;
            }
        }

        private void invoiceSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private double cashB, cardB, totalB;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                cashB = 0;
                cardB = 0;
                totalB = 0;
                conn.Open();
                if (checkBox1.Checked)
                {
                    reader = new SqlCommand("select a.id,a.CASH,a.subTotal,a.date,a.payType,a.customerId,a.pono from invoiceRetail as a  where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'", conn).ExecuteReader();
                }
                else
                {
                    reader = new SqlCommand("select id,CASH,subTotal,date,payType,customerId,pono from invoiceRetail ", conn).ExecuteReader();
                }

                while (reader.Read())
                {
                    string vehicleNO = "";
                    conn2.Open();
                    reader2 = new SqlCommand("select vehicleno from vehicle where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        vehicleNO = reader2[0] + "";
                    }
                    conn2.Close();

                    conn2.Open();
                    reader2 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        if (radioCash.Checked && reader2.GetBoolean(1))
                        {
                            // MessageBox.Show("1");
                            dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), vehicleNO + "", "R-" + reader[0], db.setAmountFormat(reader[1] + ""), 0, db.setAmountFormat(reader[2] + ""));
                            cashB = cashB + reader.GetDouble(1);
                            totalB = totalB + reader.GetDouble(2);
                        }
                        else if (radioCard.Checked && reader2.GetBoolean(4))
                        {
                            //MessageBox.Show("2");
                            conn2.Close();
                            conn2.Open();
                            reader2 = new SqlCommand("select paid from cardInvoiceRetail where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                cashB = cashB + reader.GetDouble(1);
                                cardB = cardB + reader2.GetDouble(0);
                                totalB = totalB + reader.GetDouble(2);
                                dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), vehicleNO + "", "R-" + reader[0], db.setAmountFormat(reader[1] + ""), db.setAmountFormat(reader2[0] + ""), db.setAmountFormat(reader[2] + ""));
                            }
                            conn2.Close();
                        }
                        else if ((reader2.GetBoolean(1) || reader2.GetBoolean(4)) && radioCash.Checked)
                        {
                            // MessageBox.Show("3");
                            conn2.Close();
                            conn2.Open();
                            reader2 = new SqlCommand("select paid from cardInvoiceRetail where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                cashB = cashB + reader.GetDouble(1);
                                cardB = cardB + reader2.GetDouble(0);
                                totalB = totalB + reader.GetDouble(2);
                                dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), vehicleNO + "", "R-" + reader[0], db.setAmountFormat(reader[1] + ""), db.setAmountFormat(reader2[0] + ""), db.setAmountFormat(reader[2] + ""));
                            }
                            else
                            {
                                cashB = cashB + reader.GetDouble(1);

                                totalB = totalB + reader.GetDouble(2);
                                dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), vehicleNO + "", "R-" + reader[0], db.setAmountFormat(reader[1] + ""), 0, db.setAmountFormat(reader[2] + ""));
                            }
                            conn2.Close();
                        }
                    }
                    conn2.Close();
                }
                reader.Close();
                conn.Close();

                CASH.Text = db.setAmountFormat(cashB + "");
                credit.Text = db.setAmountFormat(cardB + "");
                total.Text = db.setAmountFormat(totalB + "");
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    //MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.StackTrace + "/" + a.Message);
                // throw;
                conn.Close();
                conn2.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGridView1.Rows.Clear();
            //    db.setCursoerWait();
            //    // MessageBox.Show(from.Value.ToShortDateString());

            //    if (checkBox1.Checked)
            //    {
            //        conn.Open();
            //        reader = new SqlCommand("select DISTINCT a.id from " + query + " as a ,customer as b where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' and a.customerid = '" + customer.Text + "' ", conn).ExecuteReader();
            //        while (reader.Read())
            //        {
            //            // MessageBox.Show(reader[0] + " " + reader4[0]);
            //            conn2.Open();
            //            reader2 = new SqlCommand("select a.id,a.retail,a.subTotal,a.date,a.payType,a.customerId,a.pono from " + query + " as a  where a.id='" + reader[0] + "'", conn2).ExecuteReader();
            //            if (reader2.Read())
            //            {
            //                conn3.Open();
            //                reader3 = new SqlCommand("select name,company from customer where id='" + reader2[5] + "'", conn3).ExecuteReader();
            //                if (reader3.Read())
            //                {
            //                    dataGridView1.Rows.Add("R-" + reader2[0], reader2[6], reader2[4] + "(" + reader3[0].ToString().ToUpper() + " " + reader3[1].ToString().ToUpper() + ")", db.setAmountFormat(reader2[2] + ""), reader2[3].ToString().Split(' ')[0]);
            //                }
            //                else
            //                {
            //                    dataGridView1.Rows.Add("R-" + reader2[0], reader2[6], reader2[4] + "(" + reader2[5].ToString().ToUpper() + ")", db.setAmountFormat(reader2[2] + ""), reader2[3].ToString().Split(' ')[0]);
            //                }
            //                reader3.Close();
            //                conn3.Close();
            //            }

            //            reader2.Close();
            //            conn2.Close();
            //        }
            //        reader.Close();
            //        conn.Close();
            //    }
            //    else
            //    {
            //        conn.Open();
            //        reader = new SqlCommand("select DISTINCT a.id from " + query + " as a ,customer as b where  a.customerid  ='" + customer.Text + "' ", conn).ExecuteReader();
            //        while (reader.Read())
            //        {
            //            // MessageBox.Show(reader[0] + " " + reader4[0]);
            //            conn2.Open();
            //            reader2 = new SqlCommand("select a.id,a.retail,a.subTotal,a.date,a.payType,a.customerId,a.pono from " + query + " as a  where a.id='" + reader[0] + "'", conn2).ExecuteReader();
            //            if (reader2.Read())
            //            {
            //                conn3.Open();
            //                reader3 = new SqlCommand("select name,company from customer where id='" + reader2[5] + "'", conn3).ExecuteReader();
            //                if (reader3.Read())
            //                {
            //                    dataGridView1.Rows.Add("R-" + reader2[0], reader2[6], reader2[4] + "(" + reader3[0].ToString().ToUpper() + " " + reader3[1].ToString().ToUpper() + ")", db.setAmountFormat(reader2[2] + ""), reader2[3].ToString().Split(' ')[0]);
            //                }
            //                else
            //                {
            //                    dataGridView1.Rows.Add("R-" + reader2[0], reader2[6], reader2[4] + "(" + reader2[5].ToString().ToUpper() + ")", db.setAmountFormat(reader2[2] + ""), reader2[3].ToString().Split(' ')[0]);
            //                }
            //                reader3.Close();
            //                conn3.Close();
            //            }

            //            reader2.Close();
            //            conn2.Close();
            //        }
            //        reader.Close();
            //        conn.Close();

            //    }

            //    if (dataGridView1.Rows.Count == 0)
            //    {
            //        MessageBox.Show("Invalied Date Range");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Data Downloaded Succesfully ");
            //    }
            //    reader.Close();
            //    conn.Close();
            //    db.setCursoerDefault();
            //}
            //catch (Exception a)
            //{
            //    MessageBox.Show(a.Message);
            //    // throw;
            //    conn.Close(); conn2.Close(); conn3.Close(); conn4.Close();
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                new invoiceNewView(this, user, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Split('-')[1]).Visible = true;
                //  this.Enabled = true;
            }
            else if (e.ColumnIndex == 5)
            {
                conn.Open();
                db.setCursoerWait();
                //  new invoicePrint().setprintHalfInvoiceDB(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Split('-')[1], conn, reader);
                db.setCursoerDefault();
                conn.Close();
            }
            else if (e.ColumnIndex == 7)
            {
                if ((MessageBox.Show("Are you Sure, Cansel ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.setCursoerWait();
                    try
                    {
                        invoieNoTemp = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Split('-')[1];

                        conn.Open();
                        reader = new SqlCommand("select pc from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + true + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("This Invoice has Been Marked as a Return INVOICE and Cant be Edit");
                        }
                        else
                        {
                            conn.Close();
                            conn.Open();
                            reader = new SqlCommand("select itemCode,qty,purchasingPrice from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select qty from purchasingPriceList where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    reader2.Close();
                                    conn2.Close();
                                    conn2.Open();
                                    new SqlCommand("update purchasingPriceList set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteNonQuery();
                                    conn2.Close();
                                }

                                conn2.Close();
                                conn2.Open();
                                new SqlCommand("update item set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                                conn2.Close();

                                conn2.Open();
                                new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + reader[0] + "','" + false + "','" + reader[1] + "','" + DateTime.Now + "','" + "INVOICE-CANSEL" + "','" + user + "')", conn2).ExecuteNonQuery();

                                conn2.Close();
                            }
                            reader.Close();
                            conn.Close();
                            var cashPaid = 0.0;

                            conn.Open();
                            reader = new SqlCommand("select cash from invoiceRetail where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                cashPaid = reader.GetDouble(0);
                            }
                            conn.Close();
                            if (cashPaid != 0)
                            {
                            }

                            conn.Open();
                            new SqlCommand("delete from fullservice where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from cardInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from creditInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from chequeInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from cashInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into deletedInvoice values('" + "R-" + invoieNoTemp + "','" + user + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("delete from companyInvoice where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from vehicle where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from invoiceTerm where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from invoiceRetail where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from invoiceRetaildetail where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from sale where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from incomeAccountStatement where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("update   cashSummery set reason='" + "Cansel Invoice" + "' where reason='" + "New Invoice" + "' and remark='" + "Invoice No-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            db.setCursoerDefault();
                            MessageBox.Show("Selected Invoice CANSSEL Succesfully");
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        conn.Close();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + " " + a.StackTrace);
                        conn.Close();
                    }
                }
            }
            else if (e.ColumnIndex == 8)
            {
                invoieNoTemp = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Split('-')[1];

                conn.Open();
                reader = new SqlCommand("select id from canselInvoice where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("This Invoice has Been Marked as a CANSEL INVOICE and Cant be Edit");
                }
                else
                {
                    // MessageBox.Show(invoieNoTemp);
                    conn.Close();
                    new returnInvoice(this, user, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()).Visible = true;
                    //     this.Enabled = true;
                }
                conn.Close();
            }
        }

        private void customer_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();
                // MessageBox.Show(from.Value.ToShortDateString());
                conn3.Open();
                if (checkBox1.Checked)
                {
                    reader3 = new SqlCommand("select DISTINCT a.id from invoiceRetail as a ,customer as b where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' and a.customerid = '" + tempCustomer + "' ", conn3).ExecuteReader();
                }
                else
                {
                    reader3 = new SqlCommand("select DISTINCT a.id from invoiceRetail as a ,customer as b where  a.customerid = '" + tempCustomer + "' ", conn3).ExecuteReader();
                }

                while (reader3.Read())
                {
                    conn.Open();
                    reader = new SqlCommand("select a.id,a.cash,a.subTotal,a.date,a.payType,a.customerId,a.pono from invoiceRetail as a  where a.id='" + reader3[0] + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select * from invoiceTerm where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            if (radioCash.Checked && reader2.GetBoolean(1))
                            {
                                dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), "R-" + reader[0], db.setAmountFormat(reader[1] + ""), 0, db.setAmountFormat(reader[2] + ""));
                                cashB = cashB + reader.GetDouble(1);
                                totalB = totalB + reader.GetDouble(2);
                            }
                            else if (radioCard.Checked && reader2.GetBoolean(4))
                            {
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select paid from cardInvoiceRetail where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    cashB = cashB + reader.GetDouble(1);
                                    cardB = cardB + reader2.GetDouble(0);
                                    totalB = totalB + reader.GetDouble(2);
                                    dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), "R-" + reader[0], db.setAmountFormat(reader[1] + ""), db.setAmountFormat(reader2[0] + ""), db.setAmountFormat(reader[2] + ""));
                                }
                                conn2.Close();
                            }
                            else if (reader2.GetBoolean(1) || reader2.GetBoolean(4))
                            {
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select paid from cardInvoiceRetail where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    cashB = cashB + reader.GetDouble(1);
                                    cardB = cardB + reader2.GetDouble(0);
                                    totalB = totalB + reader.GetDouble(2);
                                    dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), "R-" + reader[0], db.setAmountFormat(reader[1] + ""), db.setAmountFormat(reader2[0] + ""), db.setAmountFormat(reader[2] + ""));
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader.GetDateTime(3).ToShortDateString(), "R-" + reader[0], db.setAmountFormat(reader[1] + ""), 0, db.setAmountFormat(reader[2] + ""));
                                    cashB = cashB + reader.GetDouble(1);
                                    totalB = totalB + reader.GetDouble(2);
                                }
                                conn2.Close();
                            }
                        }
                        conn2.Close();
                    }
                    conn.Close();
                }
                reader3.Close();
                conn3.Close();

                CASH.Text = db.setAmountFormat(cashB + "");
                credit.Text = db.setAmountFormat(cardB + "");
                total.Text = db.setAmountFormat(totalB + "");

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    //MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                // throw;
                conn.Close(); conn2.Close(); conn3.Close(); conn4.Close();
            }
        }

        private string tempCustomer;

        public Boolean loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    //  codeC = id;
                    cutomerUnSaved.Text = reader.GetString(2);
                    //  nameC.Text = reader.GetString(1);
                    //  a//ddressC.Text = reader.GetString(3);
                    // mobileNumberC.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    //addressC.SelectionLength = addressC.TextLength;
                    //  db.ToTitleCase(new TextBox[] { companyC, addressC, mobileNumberC });
                }
                else
                {
                    states = false;
                    tempCustomer = "";
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                //  addressC.Focus();
            }
            catch (Exception)
            {
                conn.Close();
            }
            return states;
        }

        private void cutomerUnSaved_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (cutomerUnSaved.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    cutomerUnSaved.Focus();
                }
                else
                {
                    loadCustomer(cutomerUnSaved.Text);
                    button9_Click(sender, e);
                    //addressC.Focus();
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox1.Visible)
                    {
                        listBox1.Focus();
                        listBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        // addressC.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void cutomerUnSaved_KeyUp(object sender, KeyEventArgs e)
        {
            tempCustomer = "";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | cutomerUnSaved.Text.Equals("")))
            {
                db.setList(listBox1, cutomerUnSaved, cutomerUnSaved.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,description from customer where description like '%" + cutomerUnSaved.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox1.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
            if (cutomerUnSaved.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cutomerUnSaved_TextChanged(object sender, EventArgs e)
        {
        }

        private void customer_TextChanged(object sender, EventArgs e)
        {
        }

        private void customer_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            //{
            //    db.setList(listBox1, customer, customer.Width);

            //    try
            //    {
            //        listBox1.Items.Clear();
            //        conn.Open();
            //        reader = new SqlCommand("select description from customer where description like '%" + customer.Text + "%' ", conn).ExecuteReader();
            //        arrayList = new ArrayList();

            //        while (reader.Read())
            //        {
            //            //   MessageBox.Show("1");
            //            listBox1.Items.Add(reader[0].ToString().ToUpper());
            //            listBox1.Visible = true;
            //            listBox1.Height = 50;
            //        }
            //        reader.Close();
            //        conn.Close();
            //    }
            //    catch (Exception a)
            //    {//
            //        MessageBox.Show(a.StackTrace + "/" + a.Message);
            //        conn.Close();
            //    }
            //}
            //if (customer.Text.Equals(""))
            //{
            //    //   MessageBox.Show("2");
            //    listBox1.Visible = false;
            //}
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                cutomerUnSaved.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
                button9_Click(sender, e);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
            button9_Click(sender, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cutomerUnSaved.Text = listBox1.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void vehicleNO_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}