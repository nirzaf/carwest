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
    public partial class customerOutstanding2_ : Form
    {
        public customerOutstanding2_(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }
        //Variable
        Form formH;
        cusOuts pp;
        DB db, db2;
        string userH, queary, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "";
        SqlConnection conn, conn2;
        string[] idArray;
        SqlDataReader reader, reader2;
        DataTable dt;
        DataSet ds;
        ArrayList arrayList;
        double amountCost, amountPaid, balance, temp030, temp3060, temp6090, temp90up, a;
        Boolean isCompany;
        DataGridViewButtonColumn btn;
        //
        //++++++ My Method Start+++
        void setTempDates(Double aH)
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
        void loadUser()
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

            dataGridView1.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            radioAdvancedSearch.Checked = true;
            radioAllItem.Checked = true;


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

            //btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.Width = 80;
            //btn.Text = "STATEMENT";

            //btn.UseColumnTextForButtonValue = true;

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
            comReg = "";
            comContact2 = "";
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
                db.setCursoerWait();
                dataGridView1.Rows.Clear();

                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by b.Date";
                }

                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by b.invoiceID";
                }

                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }

                if (radioAllItem.Checked)
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select id from customer", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        amountCost = 0;
                        amountPaid = 0;
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + reader2[0] + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                amountCost = reader.GetDouble(0);
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select sum(amount2) from receipt where customer='" + reader2[0] + "'  ", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                amountPaid = reader.GetDouble(0);
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }

                        if ((amountCost - amountPaid) != 0)
                        {
                           
                            conn.Open();
                            reader = new SqlCommand("select id,name,company from customer where id='" + reader2[0] + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                if (reader[0].ToString().Equals("C-75"))
                                {
                                    var ddd = "";
                                }
                                dataGridView1.Rows.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper() + " " + reader[2].ToString().ToUpper(), amountCost, amountPaid, Math.Round((amountCost - amountPaid), 2));

                            }
                            else
                            {
                                dataGridView1.Rows.Add(reader2[0], amountCost, amountPaid, Math.Round((amountCost - amountPaid), 2));
                            }
                            conn.Close();
                        }

                    }
                    conn2.Close();


                }
                else if (radioAdvancedSearch.Checked)
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        amountCost = 0;
                        amountPaid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select id from customer where id='"+dataGridView2.Rows[i].Cells[0].Value+"'", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            amountCost = 0;
                            amountPaid = 0;
                            try
                            {
                                conn.Open();
                                reader = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + reader2[0] + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' ", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    amountCost = reader.GetDouble(0);
                                }
                                conn.Close();
                            }
                            catch (Exception)
                            {
                                conn.Close();
                            }
                            try
                            {
                                conn.Open();
                                reader = new SqlCommand("select sum(amount2) from receipt where customer='" + reader2[0] + "'  ", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    amountPaid = reader.GetDouble(0);
                                }
                                conn.Close();
                            }
                            catch (Exception)
                            {
                                conn.Close();
                            }

                            if ((amountCost - amountPaid) != 0)
                            {
                                conn.Open();
                                reader = new SqlCommand("select id,name,company from customer where id='" + reader2[0] + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper() + " " + reader[2].ToString().ToUpper(), amountCost, amountPaid, Math.Round((amountCost - amountPaid), 2));

                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader2[0], amountCost, amountPaid, Math.Round((amountCost - amountPaid), 2));
                                }
                                conn.Close();
                            }

                        }
                        conn2.Close();
                    }
                }
                var a = 0.0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    a = a + Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                total.Text = db.setAmountFormat(a + "");
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
                MessageBox.Show(a.StackTrace + "/" + queary);
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

        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                amountCost = 0;
                db.setCursoerWait();
                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("totalPayble", typeof(float));
                dt.Columns.Add("paid", typeof(float));
                dt.Columns.Add("balance", typeof(float));
                queary = "";

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dt.Rows.Add("", dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value);

                }

                ds.Tables.Add(dt);
                //
                //  ds.WriteXmlSchema("outsThisara10.xml");
                // MessageBox.Show("a");
                cusOuts2 pp = new cusOuts2();
                pp.SetDataSource(ds);
                pp.SetParameterValue("USER", userName);

                pp.SetParameterValue("comName", comName);
                pp.SetParameterValue("comAddress", comAddres);
                pp.SetParameterValue("comContact", comcontact);

                pp.SetParameterValue("comCntact2", comContact2);

                pp.SetParameterValue("comReg", comReg);

                pp.PrintToPrinter(1, false, 0, 0);
                //new test(pp).Visible = true;
                MessageBox.Show("Send to Prnt...........");
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + queary);
            }

        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radioDateCustom_CheckedChanged(object sender, EventArgs e)
        {

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
                    reader = new SqlCommand("select id,description from customer where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void radioCustomCustomer_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Enabled = radioAdvancedSearch.Checked;
            customerID.Enabled = radioAdvancedSearch.Checked;
            customerID.Focus();
        }

        private void customerID_KeyDown(object sender, KeyEventArgs e)
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

        private void customerID_TextChanged(object sender, EventArgs e)
        {

        }
        customerStatement statmentH;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                statmentH = new customerStatement(this, userH, true);
                statmentH.Visible = true;
                statmentH.loadBank(DateTime.Now, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Split(' ')[0].ToString());
                statmentH.Text = "STATEMENT OF " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + " ABOVE " + DateTime.Now.Year + "/" + db.getMOnthName(DateTime.Now.Month.ToString()).ToUpper();

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

        }


    }
}
