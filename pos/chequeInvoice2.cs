using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class chequeInvoice2 : Form
    {
        public chequeInvoice2(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private cusCheque pp;
        private DB db, db2;
        private string userH, queary, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "";
        private SqlConnection conn, conn2;
        private string[] idArray;
        private SqlDataReader reader, reader2;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private double amountCost;
        private DataGridViewButtonColumn btn;
        private Boolean isCompany;
        //
        //++++++ My Method Start+++

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

            checkCustomer.Checked = true;
            checkBox1.Checked = true;

            checkCustomer.Checked = false;
            checkBox1.Checked = false;
            this.TopMost = true;

            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "SAVE";

            btn.UseColumnTextForButtonValue = true;
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

            conn.Open();
            reader = new SqlCommand("select chequenumber from chequeInvoiceRetail ", conn).ExecuteReader();
            arrayList = new ArrayList();
            while (reader.Read())
            {
                //  MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
            }
            reader.Close();
            idArray = arrayList.ToArray(typeof(string)) as string[];
            db.setAutoComplete2(toolStripTextBox1, idArray);
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

                dataGridView1.Rows.Clear();
                queary = "";

                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by a.Datetime";
                }
                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.invoiceID";
                }
                else if (comboOrderBY.SelectedIndex == 2)
                {
                    queary = queary + " order by a.chequeDate";
                }
                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }
                if (!radioAdvancedSearch.Checked)
                {
                    conn.Open();

                    reader = new SqlCommand("select a.* from chequeInvoiceRetail as a " + queary, conn).ExecuteReader();

                    while (reader.Read())
                    {
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select * from returnCheck where chequeNumber='" + reader[5] + "' and cheQueCodeNumber='" + reader[8] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                states = true;
                            }
                            else
                            {
                                states = false;
                            }
                            conn2.Close();
                            amountCost = amountCost + reader.GetDouble(2);
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[1] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                // MessageBox.Show("sa");
                                if (states)
                                {
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, "1", reader[0] + "/" + reader[1]);
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, "1", reader[0] + "/" + reader[1]);
                                }
                            }
                            else
                            {
                                if (states)
                                {
                                    dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, "1", reader[0] + "/" + reader[1]);
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                }
                                else
                                {
                                    dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, "1", reader[0] + "/" + reader[1]);
                                }
                            }
                            conn2.Close();
                            //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                        }
                        catch (Exception)
                        {
                            conn2.Close();
                        }
                    }
                    conn.Close();
                    //++++++++++++++++++++++++++++++++++++++++++++++
                    conn.Open();

                    reader = new SqlCommand("select a.* from chequeInvoiceRetail2 as a " + queary, conn).ExecuteReader();

                    //   MessageBox.Show("1sas");
                    while (reader.Read())
                    {
                        //   MessageBox.Show("1");
                        //  this.Dispose();
                        try
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select * from returnCheck where chequeNumber='" + reader[5] + "' and cheQueCodeNumber='" + reader[8] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                states = true;
                            }
                            else
                            {
                                states = false;
                            }
                            conn2.Close();
                            amountCost = amountCost + reader.GetDouble(2);
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[1] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                // MessageBox.Show("2");
                                // MessageBox.Show("sa");
                                if (states)
                                {
                                    //   MessageBox.Show("3");
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, reader[0], reader[1]);
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                }
                                else
                                {
                                    //   MessageBox.Show("4");
                                    dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, reader[0], reader[1]);
                                }
                            }
                            else
                            {
                                //  MessageBox.Show("5");
                                if (states)
                                {
                                    //  MessageBox.Show("6");
                                    dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, reader[0], reader[1]);
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                }
                                else
                                {
                                    // MessageBox.Show("7");
                                    dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, reader[0], reader[1]);
                                }
                            }
                            conn2.Close();
                            //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message);
                            conn2.Close();
                        }
                    }
                    conn.Close();
                }
                else
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();

                        reader = new SqlCommand("select a.* from chequeInvoiceRetail as a where a.cutomerId='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();

                        while (reader.Read())
                        {
                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select * from returnCheck where chequeNumber='" + reader[5] + "' and cheQueCodeNumber='" + reader[8] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    states = true;
                                }
                                else
                                {
                                    states = false;
                                }
                                conn2.Close();
                                amountCost = amountCost + reader.GetDouble(2);
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[1] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    // MessageBox.Show("sa");
                                    if (states)
                                    {
                                        dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, "1", reader[0] + "/" + reader[1]);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, "1", reader[0] + "/" + reader[1]);
                                    }
                                }
                                else
                                {
                                    if (states)
                                    {
                                        dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, "1", reader[0] + "/" + reader[1]);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, "1", reader[0] + "/" + reader[1]);
                                    }
                                }
                                conn2.Close();
                                //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                        //++++++++++++++++++++++++++++++++++++++++++++++
                        conn.Open();

                        reader = new SqlCommand("select a.* from chequeInvoiceRetail2 as a where a.cutomerId='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();

                        while (reader.Read())
                        {
                            //  MessageBox.Show("1");
                            try
                            {
                                conn2.Open();
                                reader2 = new SqlCommand("select * from returnCheck where chequeNumber='" + reader[5] + "' and cheQueCodeNumber='" + reader[8] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    states = true;
                                }
                                else
                                {
                                    states = false;
                                }
                                conn2.Close();
                                amountCost = amountCost + reader.GetDouble(2);
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[1] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    // MessageBox.Show("2");
                                    // MessageBox.Show("sa");
                                    if (states)
                                    {
                                        //   MessageBox.Show("3");
                                        dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, reader[0], reader[1]);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                    else
                                    {
                                        //   MessageBox.Show("4");
                                        dataGridView1.Rows.Add(reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, reader[0], reader[1]);
                                    }
                                }
                                else
                                {
                                    // MessageBox.Show("5");
                                    if (states)
                                    {
                                        //  MessageBox.Show("6");
                                        dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), true, reader[0], reader[1]);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                                    }
                                    else
                                    {
                                        // MessageBox.Show("7");
                                        dataGridView1.Rows.Add(reader[1], reader.GetDateTime(6).ToShortDateString(), reader.GetDouble(4), reader[5] + "", reader[8], reader.GetDateTime(7).ToShortDateString(), false, reader[0], reader[1]);
                                    }
                                }
                                conn2.Close();
                                //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    }
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date");
                }
                else
                {
                    MessageBox.Show("Downloaded");
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
                    reader = new SqlCommand("select description from customer where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
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

        private void checkInvoiceDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            panelCheque.Enabled = checkBox1.Checked;
        }

        private void checkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            panelCustomer.Enabled = checkCustomer.Checked;
            searchALL.Checked = false;
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                qUICKSEARCHToolStripMenuItem.DropDown.Close();
                try
                {
                    amountCost = 0;
                    db.setCursoerWait();

                    queary = "";

                    if (comboOrderBY.SelectedIndex == 0)
                    {
                        queary = queary + " order by b.Date";
                    }
                    else if (comboOrderBY.SelectedIndex == 1)
                    {
                        queary = queary + " order by a.invoiceID";
                    }
                    else if (comboOrderBY.SelectedIndex == 2)
                    {
                        queary = queary + " order by a.chequeDate";
                    }
                    if (comboOrderTO.SelectedIndex == 0)
                    {
                        queary = queary + " asc ";
                    }
                    else
                    {
                        queary = queary + " desc ";
                    }

                    conn.Open();
                    if (isCompany)
                    {
                        reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceRetail as b where b.id=a.invoiceId and a.chequenumber='" + toolStripTextBox1.Text + "'" + queary, conn).ExecuteReader();
                    }
                    else
                    {
                        reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceDump as b where b.id=a.invoiceId and a.chequenumber='" + toolStripTextBox1.Text + "'" + queary, conn).ExecuteReader();
                    }
                    while (reader.Read())
                    {
                        try
                        {
                            amountCost = amountCost + reader.GetDouble(2);
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[6] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                // MessageBox.Show("sa");
                                dt.Rows.Add(reader[6], reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());
                            }
                            else
                            {
                                dt.Rows.Add(reader[6], reader[6], "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());
                            }
                            conn2.Close();
                            //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                        }
                        catch (Exception)
                        {
                            conn2.Close();
                        }
                    }
                    conn.Close();

                    ds.Tables.Add(dt);
                    //
                    // ds.WriteXmlSchema("outsThisara3.xml");
                    //   MessageBox.Show("a");
                    pp = new cusCheque();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);

                    if (!checkBox1.Checked)
                    {
                        pp.SetParameterValue("period2", "ALL");
                    }
                    else
                    {
                        pp.SetParameterValue("period2", dateChequeFrom.Value.ToShortDateString() + " - " + dateChequeTo.Value.ToShortDateString());
                    }

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
                    MessageBox.Show(a.Message + "/" + queary);
                }
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString().Equals("1"))
                    {
                        MessageBox.Show("Sorry, This is an Invoice Genarated Cheque ..you cant modify ");
                    }
                    else
                    {
                        if ((MessageBox.Show("Are you Sure ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                        {
                            try
                            {
                                conn.Open();
                                new SqlCommand("delete from  chequeInvoiceRetail2 where chequenumber='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "' and checkCodeNo='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "'", conn).ExecuteReader();
                                conn.Close();
                                conn.Open();
                                new SqlCommand("delete from  returnCheck where chequeNumber='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "' and cheQueCodeNumber='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "'", conn).ExecuteReader();
                                conn.Close();
                                conn.Open();
                                new SqlCommand("delete from  bankAccountStatment where number='" + dataGridView1.Rows[e.RowIndex].Cells[7].Value + "'", conn).ExecuteReader();
                                conn.Close();
                                conn.Open();
                                new SqlCommand("delete from  customerStatement where reason='" + "SETTELE-" + dataGridView1.Rows[e.RowIndex].Cells[7].Value + "'", conn).ExecuteReader();
                                conn.Close();
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                                MessageBox.Show("Cheque Deleted Succefully");
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show(a.Message + "/" + a.StackTrace);
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 10)
                {
                    {
                        if ((MessageBox.Show("Are you Sure Mark as RETURN Selected Cheque ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                        {
                            conn.Open();
                            new SqlCommand("delete from customerStatement where reason='" + "RETURN-" + dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Split('/')[0] + "'", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("delete from returnCheck where chequeNumber='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "' and cheQueCodeNumber='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Equals("True"))
                            {
                                if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString().Equals("1"))
                                {
                                    conn.Open();
                                    new SqlCommand("insert into customerStatement values('" + "RETURN-" + dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Split('/')[0] + "','" + "Return Cheque Amount" + "','" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + "','" + 0 + "','" + 1 + "','" + DateTime.Now + "','" + dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Split('/')[1] + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                else
                                {
                                    conn.Open();
                                    new SqlCommand("update customerStatement set states='" + false + "' where reason='" + "SETTELE-" + dataGridView1.Rows[e.RowIndex].Cells[7].Value + "'", conn).ExecuteNonQuery();
                                    conn.Close();
                                    //conn.Open();
                                    //new SqlCommand("insert into customerStatement values('" + "RETURN-" + dataGridView1.Rows[e.RowIndex].Cells[7].Value+ "','" + "Return Cheque Amount" + "','" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + "','" + 0 + "','" + 1 + "','" + DateTime.Now + "','" + dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString() + "')", conn).ExecuteNonQuery();
                                    //conn.Close();
                                }
                                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Silver;
                                conn.Open();
                                new SqlCommand("INSERT into returnCheck values('" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "','" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + "','" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + "')", conn).ExecuteNonQuery();
                                conn.Close();
                                conn.Open();
                                new SqlCommand("update bankAccountStatment set isDeposit='" + false + "' where memo='" + "Cheque Payment :Cheque No-" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + ",Cheque Date-" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + "' and type='" + "Invoice-Pay" + "'", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                conn.Open();
                                new SqlCommand("update customerStatement set states='" + true + "' where reason='" + "SETTELE-" + dataGridView1.Rows[e.RowIndex].Cells[7].Value + "'", conn).ExecuteNonQuery();
                                conn.Close();
                                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                                conn.Open();
                                new SqlCommand("update bankAccountStatment set isDeposit='" + true + "' where memo='" + "Cheque Payment :Cheque No-" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + ",Cheque Date-" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + "' and type='" + "Invoice-Pay" + "'", conn).ExecuteNonQuery();
                                conn.Close();
                            }

                            MessageBox.Show("Selected CHeque Marked as a Return");
                        }
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
    }
}