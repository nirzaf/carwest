using System;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace pos
{
    public partial class equity : Form
    {
        public equity(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader;

        private Boolean states;
        private string user;

        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            this.ActiveControl = number;
            checkOpeningBalance.Checked = true;
            checkOpeningBalance.Checked = false;

            name.CharacterCasing = CharacterCasing.Upper;
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void update(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select id from accounts where id='" + number.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    conn.Close();
                    states = true;

                    conn.Open();
                    //   MessageBox.Show(id+"");

                    new SqlCommand("update equity set name='" + name.Text + "',openingBalance='" + amount.Text + "',openingDate='" + date.Value + "' where id='" + id + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into accounts values('" + number.Text + "','" + name.Text + "','" + "EQUITY" + "')", conn).ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Sorry Invalied Account Number");
                }
                conn.Close();

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.StackTrace + "/" + a.Message);
            }
            if (states)
            {
                MessageBox.Show("Saved Succefully");
                refresh(); name.Focus();
            }
        }

        public Boolean loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from equity where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    number.Text = reader[0] + "";
                    name.Text = reader.GetString(1);
                    if (reader.GetDouble(2) != 0)
                    {
                        checkOpeningBalance.Checked = true;
                        amount.Text = reader[2] + "";
                        date.Value = reader.GetDateTime(3);
                    }
                    else
                    {
                        checkOpeningBalance.Checked = false;
                    }
                }
                else
                {
                    states = false;
                    name.Focus();
                }
                reader.Close();
                conn.Close();

                db.setCursoerDefault();
                // MessageBox.Show(id+"");
            }
            catch (Exception)
            {
                conn.Close();
            }
            return states;
        }

        private void itemProfile_Activated(object sender, EventArgs e)
        {
        }

        private void itemProfile_Deactivate(object sender, EventArgs e)
        {
        }

        private void itemProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (name.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Name Cant Be Empty Value");
                name.Focus();
            }
            else if (number.Text.Equals(""))
            {
                MessageBox.Show("Sorry, number Cant Be Empty Value");
                number.Focus();
            }
            else if (checkOpeningBalance.Checked & amount.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Amuount Cant be Emty Value");
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                update(number.Text);
            }
        }

        private void aDDNEWITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private string subAcount;

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Name Cant Be Empty Value");
                name.Focus();
            }
            else if (number.Text.Equals(""))
            {
                MessageBox.Show("Sorry, number Cant Be Empty Value");
                number.Focus();
            }
            else if (checkOpeningBalance.Checked & amount.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Amuount Cant be Emty Value");
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    //    MessageBox.Show(comboAcount.SelectedIndex+"");
                    //  MessageBox.Show(comboAcount.Items.Count+"");
                    db.setCursoerWait();
                    states = true;

                    conn.Open();
                    reader = new SqlCommand("select id from accounts where id='" + number.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        MessageBox.Show("Sorry this Account Number is Already in System ");
                        number.Focus();
                    }
                    else
                    {
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into equity values ('" + number.Text + "','" + name.Text + "','" + amount.Text + "','" + date.Value + "')", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("insert into accounts values('" + number.Text + "','" + name.Text + "','" + "EQUITY" + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    conn.Close();

                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    conn.Close();
                    MessageBox.Show("Sorry, Account Number Already in System ");
                }
                if (states)
                {
                    MessageBox.Show("Saved Succefully");
                    refresh();
                    number.Focus();
                }
            }
        }

        //++++++ My Method Start+++

        private void refresh()
        {
            try
            {
                db.setTextBoxDefault(new TextBox[] { name, amount });
                // id = "0";
                listBox1.Visible = false;
                checkOpeningBalance.Checked = false;
                number.Focus();
            }
            catch (Exception a)
            {
                MessageBox.Show("1");
            }
        }

        //++++++ My Method End++++
        private void code_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void brand_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void category_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void description_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void remark_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void rate_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void brand_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void category_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void remark_TextChanged(object sender, EventArgs e)
        {
        }

        private void remark_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void rate_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void sAVECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void dELETECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                try
                {
                    loadCustomer(listBox1.SelectedItem.ToString().Split('-')[1]);

                    name.Focus();
                }
                catch (Exception)
                {
                }
            }
            else if (e.KeyValue == 38)
            {
                if (listBox1.SelectedIndex == 0)
                {
                    listBox1.Visible = false;
                    name.Focus();
                }
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
            try
            {
                loadCustomer(listBox1.SelectedItem.ToString().Split('-')[1]);
                name.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void code_Leave(object sender, EventArgs e)
        {
        }

        private void itemProfile_MouseHover(object sender, EventArgs e)
        {
        }

        private void itemProfile_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
        }

        private void itemProfile_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void rEFRESHToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void Code_KeyDown_1(object sender, KeyEventArgs e)
        {
        }

        private void codeC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void codeC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void nameC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nameC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void companyC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void companyC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void addressC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void mobileNumberC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void landNumberC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void faxNumberC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void emailC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            amount.Enabled = checkOpeningBalance.Checked;
            date.Enabled = checkOpeningBalance.Checked;

            if (checkOpeningBalance.Checked)
            {
                amount.Focus();
                amount.Text = "0";
            }
            amount.Text = "0";
            date.Value = DateTime.Now;
        }

        private void checkMethod_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void checkChequePaymnet_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkCardPayment_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                name.Focus();
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox1.Visible && listBox1.Items.Count != 0)
                    {
                        listBox1.Focus();
                        listBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        name.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void number_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                db.setList(listBox1, number, name.Width);
                listBox1.Items.Clear();
                conn.Open();
                reader = new SqlCommand("select * from equity ", conn).ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader[1].ToString().ToUpper() + "-" + reader[0]);
                }
                conn.Close();
                if (listBox1.Items.Count != 0)
                {
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Visible = false;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
        }
    }
}