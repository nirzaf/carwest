using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class bankSummery : Form
    {
        public bankSummery()
        {
            InitializeComponent();
        }

        private DataTable dt; private DataSet ds;
        private SqlConnection sqlconn, conn2;
        private SqlDataReader reader, reader2;
        private DB db, db2, db3;
        private SqlConnection conn;

        private void bankStatement_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            this.TopMost = true;
            db = new DB();
            sqlconn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn = db3.createSqlConnection();

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from bank ", conn).ExecuteReader();

                comboBox1.Items.Add("ALL BANKS");
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[2] + "-" + reader[1]);
                }
                conn.Close();
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private double bf, recivedBf, depositbf, sendBf, recivedBfT, depositbfT, sendBfT;
        private string customerName = "";

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.setCursoerWait();

            try
            {
                string name = "";

                recivedBfT = 0; depositbfT = 0; sendBfT = 0;
                sqlconn.Open();
                if (comboBox1.SelectedIndex == 0)
                {
                    reader = new SqlCommand("select recived,deposit,send,amount,date,sendBank,chequeNumeber,customer,address,number,cusID from chequeSummery where    date  between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", sqlconn).ExecuteReader();
                }
                else
                {
                    reader = new SqlCommand("select recived,deposit,send,amount,date,sendBank,chequeNumeber,customer,address,number,cusID from chequeSummery where    date  between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", sqlconn).ExecuteReader();
                }
                while (reader.Read())
                {
                    recivedBf = 0; depositbf = 0; sendBf = 0;
                    customerName = "";
                    conn2.Open();
                    reader2 = new SqlCommand("select BANKNAME from bank where bankCode='" + reader[5] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        name = reader2[0] + "";
                        customerName = reader[7] + "";
                        conn2.Close();
                        //   MessageBox.Show(customerName+"");
                        if (customerName.Equals(""))
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select company from customer where id='" + reader[10] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                customerName = reader2[0] + "";
                            }
                            conn2.Close();
                        }

                        if (radioAll.Checked)
                        {
                            if (reader.GetBoolean(0))
                            {
                                recivedBf = reader.GetDouble(3);
                            }
                            if (reader.GetBoolean(1))
                            {
                                depositbf = reader.GetDouble(3);
                            }
                            if (reader.GetBoolean(2))
                            {
                                sendBf = reader.GetDouble(3);
                            }
                            dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), customerName, reader[8], reader[9], name + " " + reader[5], reader[6], db.setAmountFormat(recivedBf + ""), db.setAmountFormat(sendBf + ""), db.setAmountFormat(depositbf + ""));
                        }
                        else if (radioRecevied.Checked && reader.GetBoolean(0))
                        {
                            recivedBf = reader.GetDouble(3);
                            dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), customerName, reader[8], reader[9], name + " " + reader[5], reader[6], db.setAmountFormat(recivedBf + ""), db.setAmountFormat(sendBf + ""), db.setAmountFormat(depositbf + ""));
                        }
                        else if (radioDeposit.Checked && reader.GetBoolean(1))
                        {
                            depositbf = reader.GetDouble(3);
                            dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), customerName, reader[8], reader[9], name + " " + reader[5], reader[6], db.setAmountFormat(recivedBf + ""), db.setAmountFormat(sendBf + ""), db.setAmountFormat(depositbf + ""));
                        }
                        else if (radioSend.Checked && reader.GetBoolean(2))
                        {
                            sendBf = reader.GetDouble(3);
                            dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), customerName, reader[8], reader[9], name + " " + reader[5], reader[6], db.setAmountFormat(recivedBf + ""), db.setAmountFormat(sendBf + ""), db.setAmountFormat(depositbf + ""));
                        }

                        sendBfT = sendBfT + sendBf;

                        recivedBfT = recivedBfT + recivedBf;
                        depositbfT = depositbfT + depositbf;
                    }
                    conn2.Close();
                }
                sqlconn.Close();

                sendTotal.Text = db.setAmountFormat(sendBfT + "");
                recivdTotal.Text = db.setAmountFormat(recivedBfT + "");
                depositTotal.Text = db.setAmountFormat(depositbfT + "");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                sqlconn.Close();
            }
            db.setCursoerDefault();
        }

        private bool states;
        private double odTotal, odLimit, odBalance;

        private bool checkOD()
        {
            states = false;
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Bank Name");
            }
            else if (amount.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Amount");
                amount.Focus();
            }
            else
            {
                odTotal = 0;
                odLimit = 0;
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select sum(amount) from chequeSummery where send='" + "true" + "' and sendBank='" + comboBox1.SelectedItem.ToString().Split('-')[0] + "' and date='" + chequeDate.Value + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        odTotal = reader.GetDouble(0);
                    }
                    conn.Close();
                }
                catch (Exception a)
                {
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select dayLimit from bank where bankCode='" + comboBox1.SelectedItem.ToString().Split('-')[0] + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        odLimit = reader.GetDouble(0);
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                }
                //  MessageBox.Show(odLimit + "/" + odTotal);
                odBalance = odLimit - odTotal;
                if (odBalance < Double.Parse(amount.Text))
                {
                    MessageBox.Show("Sorry You Have Only " + db.setAmountFormat(odBalance + "") + " Rupees  on Selected Day for the Cheque Issue.");
                }
                else
                {
                    states = true;
                }
            }

            return states;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!radioAll.Checked && comboBox1.SelectedIndex != 0)
                {
                    if (radioSend.Checked && checkOD())
                    {
                        conn.Open();
                        new SqlCommand("insert into chequeSummery values('" + "" + "','" + "" + "','" + "" + "','" + false + "','" + false + "','" + true + "','" + amount.Text + "','" + comboBox1.SelectedItem.ToString().Split('-')[0] + "','" + chequNumber.Text + "','" + chequeDate.Value + "','" + false + "','" + true + "','" + "" + "','" + cutomerID + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Saved");
                        chequNumber.Text = "";
                        chequeDate.Value = DateTime.Now;
                        amount.Text = "";
                        cutomerID = "";
                        customer.Text = "";
                        mobileNumber.Text = "";
                        address.Text = "";
                    }
                    else if (radioRecevied.Checked)
                    {
                        conn.Open();
                        new SqlCommand("insert into chequeSummery values('" + "" + "','" + "" + "','" + "" + "','" + radioRecevied.Checked + "','" + radioDeposit.Checked + "','" + false + "','" + amount.Text + "','" + comboBox1.SelectedItem.ToString().Split('-')[0] + "','" + chequNumber.Text + "','" + chequeDate.Value + "','" + false + "','" + false + "','" + "" + "','" + cutomerID + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Saved");
                        chequNumber.Text = "";
                        chequeDate.Value = DateTime.Now;
                        amount.Text = "";
                        cutomerID = "";
                        customer.Text = "";
                        mobileNumber.Text = "";
                        address.Text = "";
                    }
                    else if (radioDeposit.Checked)
                    {
                        conn.Open();
                        new SqlCommand("insert into chequeSummery values('" + "" + "','" + "" + "','" + "" + "','" + radioRecevied.Checked + "','" + radioDeposit.Checked + "','" + false + "','" + amount.Text + "','" + comboBox1.SelectedItem.ToString().Split('-')[0] + "','" + chequNumber.Text + "','" + chequeDate.Value + "','" + false + "','" + true + "','" + "" + "','" + cutomerID + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Saved");
                        chequNumber.Text = "";
                        chequeDate.Value = DateTime.Now;
                        amount.Text = "";
                        cutomerID = "";
                        customer.Text = "";
                        mobileNumber.Text = "";
                        address.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Pay Type Or Bank");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void chequNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(chequNumber, amount, amount, e.KeyValue);
        }

        private void customer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                if (customer.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    customer.Focus();
                }
                else
                {
                    address.Focus();
                    // loadCustomer(customer.Text);
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox2.Visible)
                    {
                        listBox2.Focus();
                        listBox2.SelectedIndex = 0;
                    }
                    else
                    {
                        customer.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private bool checkload;

        private void customer_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            {
                db.setList(listBox2, customer, customer.Width);
                listBox2.Visible = true;

                listBox2.Height = 55;
                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,company from customer where company like '%" + customer.Text + "%' ", conn).ExecuteReader();

                    states = true;
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states || checkload)
                    {
                        checkload = false;
                        listBox2.Visible = false;
                    }
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
            if (customer.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox2.Visible = false;
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customer.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;
            loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer.Text = listBox2.SelectedItem.ToString();
        }

        private string cutomerID;

        public Boolean loadCustomer(string id)
        {
            //   MessageBox.Show(id);
            try
            {
                try
                {
                    listBox2.Visible = false;
                }
                catch (Exception)
                {
                }
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    customer.Text = reader[2] + "";
                    address.Text = reader[3] + "";
                    mobileNumber.Text = reader[4] + "";
                    cutomerID = reader[0] + "";
                }
                else
                {
                    //  customer.Text = "[cash supplier]";
                    states = false;
                    cutomerID = "";
                }
                address.Focus();
                reader.Close();
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "nnnnnnnnnnnnnnnnnnnnnnn " + a.StackTrace);
                conn.Close();
                reader.Close();
            }
            return states;
        }

        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, mobileNumber, mobileNumber, e.KeyValue);
        }

        private void mobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(address, chequNumber, chequNumber, e.KeyValue);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new chequeD().Visible = true;
        }
    }
}