using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class supplierOutstanding : Form
    {
        public supplierOutstanding(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private supOuts pp;
        private DB db, db2;
        private string userH, queary, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "";
        private SqlConnection conn, conn2;
        private string[] idArray;
        private SqlDataReader reader, reader2;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private double amountCost, amountPaid, balance, temp030, temp3060, temp6090, temp90up, a;
        private Boolean isCompany;
        private DataGridViewButtonColumn btn;

        //
        //++++++ My Method Start+++
        private void setTempDates(Double aH)
        {
            a = aH;
            temp030 = 0;
            temp3060 = 0;
            temp6090 = 0;
            temp90up = 0;
            if (aH < 30)
            {
                temp030 = balance;
            }
            else if (aH < 60 & a > 30)
            {
                temp3060 = balance;
            }
            else if (aH < 60 & a > 90)
            {
                temp6090 = balance;
            }
            else
            {
                temp90up = balance;
            }
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    userName = reader.GetString(0).ToUpper();
                    isCompany = reader.GetBoolean(2);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                if (reader.Read())
                {
                    comName = reader.GetString(0).ToUpper();
                    comAddres = reader.GetString(1).ToUpper();
                    if (!reader.GetString(2).Equals(""))
                    {
                        comcontact = "Tel : " + reader[2] + " / ";
                    }
                    if (!reader.GetString(3).Equals(""))
                    {
                        comcontact = comcontact + "Fax : " + reader[3];
                    }
                    if (!reader.GetString(4).Equals(""))
                    {
                        comContact2 = "E-Mail : " + reader[4].ToString().ToUpper() + " / ";
                    }
                    if (!reader.GetString(5).Equals(""))
                    {
                        comContact2 = comContact2 + "Web : " + reader[5].ToString().ToUpper();
                    }
                    comReg = reader[6] + "";
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //
        private void stockReport_Load(object sender, EventArgs e)
        {
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

            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;
            ComboCreditTerm.SelectedIndex = 0;

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            conn.Open();
            reader = new SqlCommand("select * from company ", conn).ExecuteReader();
            if (reader.Read())
            {
                comName = reader.GetString(0).ToUpper();
                comAddres = reader.GetString(1).ToUpper();
                if (!reader.GetString(2).Equals(""))
                {
                    comcontact = "Tel : " + reader[2] + " / ";
                }
                if (!reader.GetString(3).Equals(""))
                {
                    comcontact = comcontact + "Fax : " + reader[3];
                }
            }
            reader.Close();
            conn.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amountCost = 0;
                db.setCursoerWait();
                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("invoiceID", typeof(string));
                dt.Columns.Add("invoiceAmount", typeof(float));
                dt.Columns.Add("creditAmount", typeof(float));
                dt.Columns.Add("paid", typeof(float));
                dt.Columns.Add("balance", typeof(float));
                dt.Columns.Add("date", typeof(string));
                dt.Columns.Add("period", typeof(int));
                dt.Columns.Add("dates", typeof(int));

                dt.Columns.Add("0-30", typeof(float));
                dt.Columns.Add("30-60", typeof(float));
                dt.Columns.Add("60-90", typeof(float));
                dt.Columns.Add("90-up", typeof(float));
                queary = "";
                if (radioDateCustom.Checked)
                {
                    queary = " and b.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                }
                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by b.Date";
                }
                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.invoiceID";
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
                    conn.Open();
                    reader = new SqlCommand("select b.customerID,a.balance,a.duration,b.subtotal,b.date,b.customerId,b.id from creditGrn as a,grn as b where b.id=a.invoiceID " + queary, conn).ExecuteReader();

                    while (reader.Read())
                    {
                        //  MessageBox.Show("sa");
                        amountPaid = 0;
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select paid,balance from grnCreditPaid where invoiceID='" + reader[6] + "'", conn2).ExecuteReader();
                            while (reader2.Read())
                            {
                                amountPaid = amountPaid + (reader2.GetDouble(0) - reader2.GetDouble(1));
                            }
                            conn2.Close();
                        }
                        catch (Exception)
                        {
                            conn2.Close();
                        }
                        balance = reader.GetDouble(1) - amountPaid;
                        //     MessageBox.Show(balance+"");
                        if ((balance != 0 && ComboCreditTerm.SelectedIndex == 0) || (balance != 0 && ComboCreditTerm.SelectedIndex == 2) || (balance == 0 && ComboCreditTerm.SelectedIndex == 1) || (balance == 0 && ComboCreditTerm.SelectedIndex == 2))
                        {
                            try
                            {
                                amountCost = amountCost + reader.GetDouble(1);

                                setTempDates((DateTime.Now - reader.GetDateTime(4)).TotalDays);
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from supplier where id='" + reader[5] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    dt.Rows.Add(reader2[0].ToString().ToUpper(), reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader[6] + "/" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(reader[1] + ""), db.setAmountFormat(amountPaid + ""), db.setAmountFormat(balance + ""), reader.GetDateTime(4).ToShortDateString(), reader[2], a, temp030, temp3060, temp6090, temp90up);
                                }
                                else
                                {
                                    dt.Rows.Add(reader[5], reader[5], reader[6] + "/" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(reader[1] + ""), db.setAmountFormat(amountPaid + ""), db.setAmountFormat(balance + ""), reader.GetDateTime(4).ToShortDateString(), reader[2], a, temp030, temp3060, temp6090, temp90up);
                                }
                                conn2.Close();
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show(a.Message + "/" + a.StackTrace);
                                conn2.Close();
                            }
                        }
                    }
                    conn.Close();
                }
                else if (radioAdvancedSearch.Checked)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();
                        reader = new SqlCommand("select b.customerID,a.balance,a.duration,b.subtotal,b.date,b.customerId,b.id from creditGrn as a,grn as b where b.id=a.invoiceID and b.customerId='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                        // reader = new SqlCommand("select b.customerID,a.balance,a.duration,b.subtotal,b.date,b.customerId,b.id from creditGrn as a,grn as b where b.id=a.invoiceID " + queary, conn).ExecuteReader();

                        while (reader.Read())
                        {
                            //  MessageBox.Show("sa");
                            amountPaid = 0;
                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select paid,balance from grnCreditPaid where invoiceID='" + reader[6] + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    amountPaid = amountPaid + (reader2.GetDouble(0) - reader2.GetDouble(1));
                                }
                                conn2.Close();
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                            balance = reader.GetDouble(1) - amountPaid;
                            //     MessageBox.Show(balance+"");
                            if ((balance != 0 && ComboCreditTerm.SelectedIndex == 0) || (balance != 0 && ComboCreditTerm.SelectedIndex == 2) || (balance == 0 && ComboCreditTerm.SelectedIndex == 1) || (balance == 0 && ComboCreditTerm.SelectedIndex == 2))
                            {
                                try
                                {
                                    amountCost = amountCost + reader.GetDouble(1);

                                    setTempDates((DateTime.Now - reader.GetDateTime(4)).TotalDays);
                                    conn2.Open();
                                    reader2 = new SqlCommand("select id,name,company from supplier where id='" + reader[5] + "'", conn2).ExecuteReader();
                                    if (reader2.Read())
                                    {
                                        dt.Rows.Add(reader2[0].ToString().ToUpper(), reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader[6] + "/" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(reader[1] + ""), db.setAmountFormat(amountPaid + ""), db.setAmountFormat(balance + ""), reader.GetDateTime(4).ToShortDateString(), reader[2], a, temp030, temp3060, temp6090, temp90up);
                                    }
                                    else
                                    {
                                        dt.Rows.Add(reader[5], reader[5], reader[6] + "/" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(reader[1] + ""), db.setAmountFormat(amountPaid + ""), db.setAmountFormat(balance + ""), reader.GetDateTime(4).ToShortDateString(), reader[2], a, temp030, temp3060, temp6090, temp90up);
                                    }
                                    conn2.Close();
                                }
                                catch (Exception a)
                                {
                                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                                    conn2.Close();
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                ds.Tables.Add(dt);
                //
                // ds.WriteXmlSchema("outsThisara3.xml");
                //   MessageBox.Show("a");
                pp = new supOuts();
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
                pp.SetParameterValue("comName", comName);
                pp.SetParameterValue("comAddress", comAddres);
                pp.SetParameterValue("comContact", comcontact);

                pp.SetParameterValue("comCntact2", comContact2);

                pp.SetParameterValue("comReg", comReg);
                crystalReportViewer1.ReportSource = pp;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                }
                else
                {
                    //   MessageBox.Show("Succefully Downloaded");
                }
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + queary + a.StackTrace);
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
                    reader = new SqlCommand("select name,company from supplier where id='" + customerID.Text + "'", conn).ExecuteReader();
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
        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data to Print");
                }
                else
                {
                    pp.PrintToPrinter(1, false, 0, 0);
                    MessageBox.Show("Send to Print Succesfully");
                }
            }
            catch (Exception)
            {
            }
        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                    reader = new SqlCommand("select id,description from supplier where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
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
            reader = new SqlCommand("select name,company from supplier where id='" + customerID.Text + "'", conn).ExecuteReader();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}