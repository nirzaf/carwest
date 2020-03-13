using System;
using System.Collections;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace pos
{
    public partial class GRNSearchCreditNew : Form
    {
        public GRNSearchCreditNew(Form form, String user)
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
            //MessageBox.Show("sa");
            db = new DB();
            conn = db.createSqlConnection2();
            db2 = new DB();
            conn2 = db2.createSqlConnection2();
            db3 = new DB();
            conn3 = db3.createSqlConnection2();
            db4 = new DB();
            conn4 = db4.createSqlConnection2();

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

            // loadUser();
            setAutoComplete();
            checkBox1.Checked = true;
            checkBox1.Checked = false;

            //   this.TopMost = true;

            //  customer.CharacterCasing = CharacterCasing.Upper;
            cutomerUnSaved.CharacterCasing = CharacterCasing.Upper;
            //dataGridView1.Columns[5].Visible = false;
        }

        private void invoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
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

        private double cashB, cardB, totalB, paid;
        private string vehicleNO;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                cashB = 0;
                cardB = 0;
                totalB = 0;
                vehicleNO = "";
                conn.Open();
                if (checkBox1.Checked)
                {
                    reader = new SqlCommand("select a.subTotal,a.grnDate,a.customerId,A.ID,a.invoiceNo from GRN as a  where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'", conn).ExecuteReader();
                }
                else
                {
                    reader = new SqlCommand("select subTotal,grnDate,customerId,id,invoiceNo from GRN ", conn).ExecuteReader();
                }

                while (reader.Read())
                {
                    paid = 0;
                    conn2.Open();
                    reader2 = new SqlCommand("select * from GRNtERM where invoiceid='" + reader[3] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        // MessageBox.Show("1");
                        if (reader2.GetBoolean(2))
                        {
                            //MessageBox.Show("2");
                            // MessageBox.Show("1");
                            conn2.Close();
                            conn2.Open();
                            reader2 = new SqlCommand("select COMPANY from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                vehicleNO = reader2[0] + "";
                            }
                            conn2.Close();

                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[3] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    paid = reader2.GetDouble(0);
                                }
                                conn2.Close();
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }

                            //MessageBox.Show(reader[0]+"");
                            dataGridView1.Rows.Add(reader[3], vehicleNO, reader[4], reader.GetDateTime(1).ToShortDateString(), db.setAmountFormat(reader[0] + ""), db.setAmountFormat(paid + ""), db.setAmountFormat(reader.GetDouble(0) - paid + ""));

                            conn2.Close();
                            totalB = totalB + reader.GetDouble(0);
                            cashB = cashB + (paid);
                            cardB = cardB + (reader.GetDouble(0) - paid);
                        }
                    }
                    conn2.Close();
                }
                reader.Close();
                conn.Close();

                total.Text = db.setAmountFormat(totalB + "");
                debit.Text = db.setAmountFormat(paid + "");
                balance.Text = db.setAmountFormat(cardB + "");

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
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
            if (e.ColumnIndex == 7)
            {
                new invoiceNewPURCH(this, user, "CREDIT", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()).Visible = true;
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

                cashB = 0;
                cardB = 0;
                totalB = 0;
                //  MessageBox.Show(tempCustomer);
                conn3.Open();
                if (checkBox1.Checked)
                {
                    reader3 = new SqlCommand("select DISTINCT a.id from grn as a ,supplier as b where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' and a.customerid = '" + tempCustomer + "' ", conn3).ExecuteReader();
                }
                else
                {
                    reader3 = new SqlCommand("select DISTINCT a.id from grn as a ,supplier as b where  a.customerid = '" + tempCustomer + "' ", conn3).ExecuteReader();
                }
                db.setCursoerWait();
                dataGridView1.Rows.Clear();
                cashB = 0;
                cardB = 0;
                totalB = 0;
                vehicleNO = "";
                while (reader3.Read())
                {
                    try
                    {
                        conn.Open();

                        reader = new SqlCommand("select subTotal,grnDate,customerId,id,invoiceNo from GRN where id='" + reader3[0] + "'", conn).ExecuteReader();

                        if (reader.Read())
                        {
                            paid = 0;
                            conn2.Open();
                            reader2 = new SqlCommand("select * from GRNtERM where invoiceid='" + reader[3] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                // MessageBox.Show("1");
                                if (reader2.GetBoolean(2))
                                {
                                    //MessageBox.Show("2");
                                    // MessageBox.Show("1");
                                    conn2.Close();
                                    conn2.Open();
                                    reader2 = new SqlCommand("select COMPANY from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        vehicleNO = reader2[0] + "";
                                    }
                                    conn2.Close();

                                    try
                                    {
                                        conn2.Open();
                                        reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[3] + "'", conn2).ExecuteReader();
                                        if (reader2.Read())
                                        {
                                            paid = reader2.GetDouble(0);
                                        }
                                        conn2.Close();
                                    }
                                    catch (Exception)
                                    {
                                        conn2.Close();
                                    }

                                    //MessageBox.Show(reader[3]+"");
                                    dataGridView1.Rows.Add(reader[3], vehicleNO, reader[4], reader.GetDateTime(1).ToShortDateString(), db.setAmountFormat(reader[0] + ""), db.setAmountFormat(paid + ""), db.setAmountFormat(reader.GetDouble(0) - paid + ""));

                                    conn2.Close();
                                    totalB = totalB + reader.GetDouble(0);
                                    cashB = cashB + (paid);
                                    cardB = cardB + (reader.GetDouble(0) - paid);
                                }
                            }
                            conn2.Close();
                        }
                        reader.Close();
                        conn.Close();

                        total.Text = db.setAmountFormat(totalB + "");
                        debit.Text = db.setAmountFormat(paid + "");
                        balance.Text = db.setAmountFormat(cardB + "");

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
                reader3.Close();
                conn3.Close();

                total.Text = db.setAmountFormat(totalB + "");

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
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
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
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
                    reader = new SqlCommand("select id,description from supplier where description like '%" + cutomerUnSaved.Text + "%' ", conn).ExecuteReader();
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

        private void vehicleNO_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    db.setCursoerWait();

                    cashB = 0;
                    cardB = 0;
                    totalB = 0;
                    vehicleNO = "";
                    conn.Open();

                    reader = new SqlCommand("select subTotal,grnDate,customerId,id,invoiceNo from GRN where invoiceno='" + textBox1.Text + "'", conn).ExecuteReader();

                    while (reader.Read())
                    {
                        paid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select * from GRNtERM where invoiceid='" + reader[3] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            // MessageBox.Show("1");
                            if (reader2.GetBoolean(2))
                            {
                                //MessageBox.Show("2");
                                // MessageBox.Show("1");
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select COMPANY from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    vehicleNO = reader2[0] + "";
                                }
                                conn2.Close();

                                try
                                {
                                    conn2.Open();
                                    reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[3] + "'", conn2).ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        paid = reader2.GetDouble(0);
                                    }
                                    conn2.Close();
                                }
                                catch (Exception)
                                {
                                    conn2.Close();
                                }

                                //MessageBox.Show(reader[0]+"");
                                dataGridView1.Rows.Add(reader[3], vehicleNO, reader[4], reader.GetDateTime(1).ToShortDateString(), db.setAmountFormat(reader[0] + ""), db.setAmountFormat(paid + ""), db.setAmountFormat(reader.GetDouble(0) - paid + ""));

                                conn2.Close();
                                totalB = totalB + reader.GetDouble(0);
                                cashB = cashB + (paid);
                                cardB = cardB + (reader.GetDouble(0) - paid);
                            }
                        }
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();

                    total.Text = db.setAmountFormat(totalB + "");
                    debit.Text = db.setAmountFormat(paid + "");
                    balance.Text = db.setAmountFormat(cardB + "");

                    if (dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalied Date Range");
                    }
                    else
                    {
                        MessageBox.Show("Data Downloaded Succesfully ");
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
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    db.setCursoerWait();

                    cashB = 0;
                    cardB = 0;
                    totalB = 0;
                    vehicleNO = "";
                    conn.Open();

                    reader = new SqlCommand("select subTotal,grnDate,customerId,id,invoiceNo from GRN where id='" + textBox2.Text + "'", conn).ExecuteReader();

                    while (reader.Read())
                    {
                        paid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select * from GRNtERM where invoiceid='" + reader[3] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            // MessageBox.Show("1");
                            if (reader2.GetBoolean(2))
                            {
                                //MessageBox.Show("2");
                                // MessageBox.Show("1");
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select COMPANY from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    vehicleNO = reader2[0] + "";
                                }
                                conn2.Close();

                                try
                                {
                                    conn2.Open();
                                    reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[3] + "'", conn2).ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        paid = reader2.GetDouble(0);
                                    }
                                    conn2.Close();
                                }
                                catch (Exception)
                                {
                                    conn2.Close();
                                }

                                //MessageBox.Show(reader[0]+"");
                                dataGridView1.Rows.Add(reader[3], vehicleNO, reader[4], reader.GetDateTime(1).ToShortDateString(), db.setAmountFormat(reader[0] + ""), db.setAmountFormat(paid + ""), db.setAmountFormat(reader.GetDouble(0) - paid + ""));

                                conn2.Close();
                                totalB = totalB + reader.GetDouble(0);
                                cashB = cashB + (paid);
                                cardB = cardB + (reader.GetDouble(0) - paid);
                            }
                        }
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();

                    total.Text = db.setAmountFormat(totalB + "");
                    debit.Text = db.setAmountFormat(paid + "");
                    balance.Text = db.setAmountFormat(cardB + "");

                    if (dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalied Date Range");
                    }
                    else
                    {
                        MessageBox.Show("Data Downloaded Succesfully ");
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
        }
    }
}