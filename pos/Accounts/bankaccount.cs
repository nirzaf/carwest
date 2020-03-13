using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class bankaccount : Form
    {
        public bankaccount(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private ArrayList arrayList;
        private string[] idArray, nameArray, addressArray, mobileNoArray, LandNoArray, emailArray, companyArray;

        private Boolean check, checkListBox, states;
        private string user, listBoxType;

        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();

            bankName.CharacterCasing = CharacterCasing.Upper;
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

        private void update()
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select bankCode from bank where bankCode='" + acountNo.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    //  MessageBox.Show("1");
                    conn.Close();

                    conn.Open();
                    //   MessageBox.Show(id+"");

                    new SqlCommand("update bank set BANKNAME='" + bankName.Text + "' where bankCode='" + acountNo.Text + "'", conn).ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into bank values ('" + bankName.Text + "','" + acountNo.Text + "')", conn).ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
                MessageBox.Show("Saved Succefully");
                bankName.Text = "";
                acountNo.Text = "";
                acountNo.Focus();

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.StackTrace + "/" + a.Message);
            }
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
            update();
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
        }

        //++++++ My Method Start+++
        private void loadAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select bankCode from bank ", conn).ExecuteReader();
                arrayList = new ArrayList();
                // MessageBox.Show("1 ");
                while (reader.Read())
                {
                    // MessageBox.Show("3");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                // MessageBox.Show("2");
                reader.Close();
                conn.Close();

                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(acountNo, idArray);
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void refresh()
        {
            try
            {
                loadAutoComplete();
                db.setTextBoxDefault(new TextBox[] { bankName, acountNo });
                // id = "0";
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
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void code_Leave(object sender, EventArgs e)
        {
        }

        private void itemProfile_MouseHover(object sender, EventArgs e)
        {
        }

        private void itemProfile_MouseClick(object sender, MouseEventArgs e)
        {
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
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                update();
            }
        }

        private void companyC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void addressC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from bank where bankCode='" + acountNo.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        bankName.Text = reader[1] + "";
                    }

                    bankName.Focus();
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
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
        }

        private void number_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
        }
    }
}