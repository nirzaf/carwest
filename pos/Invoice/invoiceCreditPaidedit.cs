using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class invoiceCreditPaidedit : Form
    {
        public invoiceCreditPaidedit(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private creditPaidHistory pp;
        private DB db, db2;
        private string userH, queary, userName;
        private SqlConnection conn, conn2;
        private string[] idArray;
        private SqlDataReader reader, reader2;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private DataGridViewButtonColumn btn;
        private Boolean isCompany;
        //
        //++++++ My Method Start+++

        private void loadUser()
        {
            try
            {
                // MessageBox.Show(userH);
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    //       MessageBox.Show("222222222222");

                    userName = reader.GetString(0).ToUpper();
                    //  dataGridView1.Columns[8].Visible = reader.GetBoolean(14);

                    // dataGridView1.Columns[7].Visible = reader.GetBoolean(13);
                    isCompany = reader.GetBoolean(2);
                }
                reader.Close();
                conn.Close();
                if (!(userName.Equals("rasika") || userName.Equals("mahesh")))
                {
                    // dataGridView1.Columns[5].Visible = false;
                    // dataGridView3.Columns[7].Visible = false;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        //
        private void stockReport_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();

            radioAdvancedSearch.Checked = true;
            searchALL.Checked = true;
            radioDateCustom.Checked = true;
            radioAllDate.Checked = true;

            this.TopMost = true;

            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "VIEW";

            btn.UseColumnTextForButtonValue = true;
            loadUser();
        }

        private void radioSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Enabled = radioAdvancedSearch.Checked;
            customerID.Enabled = radioAdvancedSearch.Checked;
            customerID.Focus();
        }

        private void checkBrand_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkCategory_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkDescription_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkQty_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMinValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMaxValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void stockReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void checkQty_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private double tempPaid;

        private void loadInvoice()
        {
            try
            {
                queary = "";
                dataGridView3.Rows.Clear();
                if (searchALL.Checked)
                {
                    if (radioDateCustom.Checked)
                    {
                        queary = "where a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and a.customerid=b.id";
                    }
                    else
                    {
                        queary = "where a.customerid=b.id";
                    }

                    conn.Open();
                    reader = new SqlCommand("select a.invoiceid,b.id,b.company,a.balance,a.date from creditinvoiceRetail as a,customer as b " + queary, conn).ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            tempPaid = 0;
                            conn2.Open();
                            reader2 = new SqlCommand("select sum(paid) from invoiceCreditPaid where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                tempPaid = reader2.GetDouble(0);
                            }
                            conn2.Close();

                            if (radioSettele.Checked)
                            {
                                if (tempPaid >= reader.GetDouble(3))
                                {
                                    dataGridView3.Rows.Add(reader[1] + " " + reader[2], "R-" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), reader.GetDateTime(4).ToShortDateString(), db.setAmountFormat((reader.GetDouble(3) - tempPaid) + ""), true);
                                }
                            }
                            else if (radioPending.Checked)
                            {
                                if (tempPaid < reader.GetDouble(3))
                                {
                                    dataGridView3.Rows.Add(reader[1] + " " + reader[2], "R-" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), reader.GetDateTime(4).ToShortDateString(), db.setAmountFormat((reader.GetDouble(3) - tempPaid) + ""), false);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            conn2.Close();
                        }
                    }
                    conn.Close();
                }
                else if (radioAdvancedSearch.Checked)
                {
                    if (radioDateCustom.Checked)
                    {
                        queary = "and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and a.customerid=b.id";
                    }
                    else
                    {
                        queary = "and a.customerid=b.id";
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.invoiceid,b.id,b.company,a.balance,a.date from creditinvoiceRetail as a,customer as b where a.customerid='" + dataGridView2.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                tempPaid = 0;
                                conn2.Open();
                                reader2 = new SqlCommand("select sum(paid) from invoiceCreditPaid where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    tempPaid = reader2.GetDouble(0);
                                }
                                conn2.Close();

                                if (radioSettele.Checked)
                                {
                                    if (tempPaid >= reader.GetDouble(3))
                                    {
                                        dataGridView3.Rows.Add(reader[1] + " " + reader[2], "R-" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), reader.GetDateTime(4).ToShortDateString(), db.setAmountFormat((reader.GetDouble(3) - tempPaid) + ""), true);
                                    }
                                }
                                else if (radioPending.Checked)
                                {
                                    if (tempPaid < reader.GetDouble(3))
                                    {
                                        //  MessageBox.Show((reader.GetDouble(3) - tempPaid)+"");
                                        dataGridView3.Rows.Add(reader[1] + " " + reader[2], "R-" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), reader.GetDateTime(4).ToShortDateString(), db.setAmountFormat((reader.GetDouble(3) - tempPaid) + ""), false);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                dataGridView1.Rows.Clear();
                queary = "";

                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by a.Date";
                }

                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }
                if (searchALL.Checked)
                {
                    if (radioDateCustom.Checked)
                    {
                        queary = "where a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and a.reason!='" + "CASH RETURN" + "'";
                    }
                    else
                    {
                        queary = "where  a.reason!='" + "CASH RETURN" + "'";
                    }
                    //    MessageBox.Show(isCompany+"");
                    conn.Open();
                    reader = new SqlCommand("select a.* from receipt as a " + queary, conn).ExecuteReader();
                    //  MessageBox.Show("sasa");

                    while (reader.Read())
                    {
                        //  MessageBox.Show("sa");
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[3] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), db.setAmountFormat(reader[5] + ""), reader.GetDateTime(1).ToShortDateString(), reader[0], reader[8]);
                            }
                            else
                            {
                                dataGridView1.Rows.Add(reader[3], db.setAmountFormat(reader[5] + ""), reader.GetDateTime(1).ToShortDateString(), reader[0], reader[8]);
                            }
                            conn2.Close();
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message + "/" + a.StackTrace);
                            conn2.Close();
                        }
                    }
                    conn.Close();
                }
                else if (radioAdvancedSearch.Checked)
                {
                    if (radioDateCustom.Checked)
                    {
                        queary = "and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "' and a.reason!='" + "CASH RETURN" + "'";
                    }
                    else
                    {
                        queary = "and a.reason!='" + "CASH RETURN" + "'";
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.* from receipt as a where a.customer='" + dataGridView2.Rows[i].Cells[0].Value + "' " + queary, conn).ExecuteReader();
                        //  MessageBox.Show("sasa");

                        while (reader.Read())
                        {
                            //  MessageBox.Show("sa");
                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[3] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), db.setAmountFormat(reader[5] + ""), reader.GetDateTime(1).ToShortDateString(), reader[0], reader[8]);
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader[3], db.setAmountFormat(reader[5] + ""), reader.GetDateTime(1).ToShortDateString(), reader[0], reader[8]);
                                }
                                conn2.Close();
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show(a.Message + "/" + a.StackTrace);
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    }
                }
                // totAmount.Text = db.setAmountFormat(amountCost + "");
                loadInvoice();
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                }
                else
                {
                    MessageBox.Show("Succefully Downloaded");
                }
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void searchALL_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sETTINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
        }

        private void itemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (customerID.Text.Equals(""))
                {
                    customerID.Focus();
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        name = reader[0] + " " + reader[1];
                    }
                    conn.Close();
                    if (dataGridView2.Rows.Count == 0)
                    {
                        dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                    }
                    else
                    {
                        states = false;
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            if (dataGridView2.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                            {
                                states = true;
                            }
                        }
                        if (!states)
                        {
                            dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                        }
                    }
                    customerID.Text = "";
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
                        customerID.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void brandName_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void name_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if ((MessageBox.Show("Are You Sure Delete this Payment ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        var a = 0.0;
                        MessageBox.Show("Sorry You DOnt Have Permission");
                        //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                        //conn.Open();
                        //new SqlCommand("delete from invoiceCreditPaid where tempid='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from invoiceCreditPaid where tempid='" + "OP/" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from overPayC where tempid='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from returnChequePayment where tempid='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from customerStatement where reason='" + "SETTELE-" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from chequeInvoiceRetail2 where invoiceID='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();

                        //conn.Open();
                        //new SqlCommand("delete from receipt where id='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();
                        //conn.Open();
                        //new SqlCommand("delete from cashSummery where reason='" + "Invoice Credit Paid-" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", conn).ExecuteNonQuery();
                        //conn.Close();

                        // dataGridView1.Rows.RemoveAt(e.RowIndex);
                        db.setCashBalance(DateTime.Now);

                        //totAmount.Text = db.setAmountFormat(amountCost + "");
                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    new invoicePrint().setprintReceiprt(dataGridView1.Rows[e.RowIndex].Cells[3].Value + "", conn, reader, dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Split(' ')[0]);
                }
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry, Empty Data to Print Report");
            }
            else
            {
                try
                {
                    var a = 0.0;
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();
                    dt.Columns.Add("cusID", typeof(string));
                    dt.Columns.Add("customer", typeof(string));
                    dt.Columns.Add("invoiceID", typeof(string));
                    dt.Columns.Add("amount", typeof(float));

                    dt.Columns.Add("paid", typeof(string));

                    dt.Columns.Add("balance", typeof(string));
                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("totAmount", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[6].Value);
                    }
                    ds.Tables.Add(dt);
                    //
                    //         ds.WriteXmlSchema("stockThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new creditPaidHistory();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);
                    if (radioAllDate.Checked)
                    {
                        pp.SetParameterValue("period", "ALL");
                    }
                    else
                    {
                        pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                    }
                    pp.PrintToPrinter(1, false, 0, 0);
                    // new test(pp).Visible = true;
                    //crystalReportViewer1.ReportSource = pp;
                    db.setCursoerDefault();
                    MessageBox.Show("Report Send to Print Succefully........");
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }
            }
        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry, Empty Data to Print Report");
            }
            else
            {
                try
                {
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();
                    dt.Columns.Add("cusID", typeof(string));
                    dt.Columns.Add("customer", typeof(string));
                    dt.Columns.Add("invoiceID", typeof(string));
                    dt.Columns.Add("amount", typeof(float));

                    dt.Columns.Add("paid", typeof(string));

                    dt.Columns.Add("balance", typeof(string));
                    dt.Columns.Add("date", typeof(string));
                    dt.Columns.Add("totAmount", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[6].Value);
                    }
                    ds.Tables.Add(dt);
                    //
                    //         ds.WriteXmlSchema("stockThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new creditPaidHistory();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);
                    if (radioAllDate.Checked)
                    {
                        pp.SetParameterValue("period", "ALL");
                    }
                    else
                    {
                        pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                    }
                    //  pp.PrintToPrinter(1, false, 0, 0);
                    this.Enabled = true;
                    new creditPaidHistoryView(this, userH, pp).Visible = true;
                    //crystalReportViewer1.ReportSource = pp;
                    db.setCursoerDefault();
                    //   MessageBox.Show("Report Send to Print Succefully........");
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }
            }
        }

        private void radioDateCustom_CheckedChanged(object sender, EventArgs e)
        {
            from.Enabled = radioDateCustom.Checked;
            to.Enabled = radioDateCustom.Checked;
        }

        private void itemCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMin_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMax_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCustomerID_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioName_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCompany_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void customerID_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customerID.Text.Equals("")))
            {
                db.setList(listBox1, customerID, customerID.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select ID,description from customer where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
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
            if (customerID.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void Name_KeyUp_1(object sender, KeyEventArgs e)
        {
        }

        private void company_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private string name;
        private Boolean states;
        private Point p;

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customerID.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                name = "";
                for (int i = 0; i < listBox1.SelectedItem.ToString().Split(' ').Length; i++)
                {
                    if (i != 0)
                    {
                        name = name + " " + listBox1.SelectedItem.ToString().Split(' ')[i];
                    }
                }

                customerID.Focus();
                customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                if (dataGridView2.Rows.Count == 0)
                {
                    dataGridView2.Rows.Add(customerID.Text, name);
                }
                else
                {
                    states = false;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString().Equals(customerID.Text))
                        {
                            states = true;
                        }
                    }
                    if (!states)
                    {
                        dataGridView2.Rows.Add(customerID.Text, name);
                    }
                }
                customerID.Text = "";
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            conn.Open();
            reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                name = reader[0] + " " + reader[1];
            }
            conn.Close();
            if (dataGridView2.Rows.Count == 0)
            {
                dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
            }
            else
            {
                states = false;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                    {
                        states = true;
                    }
                }
                if (!states)
                {
                    dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                }
            }
            customerID.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void cREDITPAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoiceCreditPay(this, userH).Visible = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void radioSettele_CheckedChanged(object sender, EventArgs e)
        {
            loadInvoice();
        }

        private void radioPending_CheckedChanged(object sender, EventArgs e)
        {
            loadInvoice();
        }
    }
}