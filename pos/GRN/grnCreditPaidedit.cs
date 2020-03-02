using System;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class grnCreditPaidedit : Form
    {
        public grnCreditPaidedit(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }
        //Variable
        Form formH;
        creditPaidHistory pp;
        DB db, db2;
        string userH, queary, userName;
        SqlConnection conn, conn2;
        string[] idArray;
        SqlDataReader reader, reader2;
        DataTable dt;
        DataSet ds;
        ArrayList arrayList;
        double amountCost;
        DataGridViewButtonColumn btn;
        //
        //++++++ My Method Start+++

        void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {

                    userName = reader.GetString(0).ToUpper();
                    dataGridView1.Columns[8].Visible = reader.GetBoolean(18);
                    dataGridView1.Columns[7].Visible = reader.GetBoolean(17);

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
            dataGridView1.AllowUserToAddRows = false;

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
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";
            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "EDIT";
            btn.UseColumnTextForButtonValue = true;

           

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
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
                dataGridView1.Rows.Clear();
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
                    reader = new SqlCommand("select a.invoiceID,a.amount,a.paid,a.balance,a.date,b.supplierID,a.id from grnCreditPaid as a,grn as b where b.id=a.invoiceId " + queary, conn).ExecuteReader();
                    while (reader.Read())
                    {
                      //  MessageBox.Show("sa");
                        try
                        {
                            amountCost = amountCost + reader.GetDouble(2);
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from supplier where id='" + reader[5] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader[0], db.setAmountFormat(reader[1] + ""), reader.GetDateTime(4).ToShortDateString(), reader[6]);

                            }
                            else
                            {
                                dataGridView1.Rows.Add(reader[5],reader[0], db.setAmountFormat(reader[1] + ""),  reader.GetDateTime(4).ToShortDateString(), reader[6]);

                            }
                            conn2.Close();

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
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();
                        reader = new SqlCommand("select a.invoiceID,a.amount,a.paid,a.balance,a.date,b.supplierID,a.id from grnCreditPaid as a,grn as b where b.id=a.invoiceId and b.supplierID='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();
                        while (reader.Read())
                        {
                            amountCost = amountCost + reader.GetDouble(2);
                            //  MessageBox.Show("sa");
                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from supplier where id='" + reader[5] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + "-" + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(),  reader[0], db.setAmountFormat(reader[1] + ""),reader.GetDateTime(4).ToShortDateString(), reader[6]);

                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader[5], reader[0], db.setAmountFormat(reader[1] + ""), reader.GetDateTime(4).ToShortDateString(), reader[6]);

                                }
                                conn2.Close();

                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    }
                }
                totAmount.Text = db.setAmountFormat(amountCost + "");

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
                   
                    if (dataGridView2.Rows.Count == 0)
                    {
                        dataGridView2.Rows.Add(customerID.Text.ToUpper());
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
                            dataGridView2.Rows.Add(customerID.Text.ToUpper());
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
                if (e.ColumnIndex ==5)
                {
                    if ((MessageBox.Show("Are You Sure Delete this Payment ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        var a = 0.0;
                        conn.Open();
                        reader = new SqlCommand("select amount from grnCreditPaid where id='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            amountCost = amountCost - reader.GetDouble(0);
                            a = reader.GetDouble(0);
                        }
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from grnCreditPaid where id='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "'", conn).ExecuteNonQuery();
                        conn.Close();

                      
                        dataGridView1.Rows.RemoveAt(e.RowIndex);


                        totAmount.Text = db.setAmountFormat(amountCost + "");
                    }

                }
                else if (e.ColumnIndex==6)
                {
                    //this.Enabled = true;
                    new grnCreditPayEdit(this, userH, dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), e.RowIndex + "").Visible = true;
                }

            }
            catch (Exception a)
            {
                conn.Close();
                 MessageBox.Show(a.Message+"/"+a.StackTrace);
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

                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[5].Value, dataGridView1.Rows[i].Cells[6].Value);
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

                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value,dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[5].Value, dataGridView1.Rows[i].Cells[6].Value);
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
                    else {

                        pp.SetParameterValue("period", from.Value.ToShortDateString()+" - "+to.Value.ToShortDateString());
              
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
                    reader = new SqlCommand("select description from supplier where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper());
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
        string name;
        Boolean states;
        Point p;
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {

                customerID.Focus();

            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
               

                customerID.Focus();
                customerID.Text = listBox1.SelectedItem.ToString();
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
                        dataGridView2.Rows.Add(customerID.Text);
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

            customerID.Text = listBox1.SelectedItem.ToString();
           
            if (dataGridView2.Rows.Count == 0)
            {
                dataGridView2.Rows.Add(customerID.Text.ToUpper());
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
                    dataGridView2.Rows.Add(customerID.Text.ToUpper());
                }
            }
            customerID.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
