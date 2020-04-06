using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class bankaccount : Form
    {
        public bankaccount(Form home, string user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        private DB db;

        private readonly Form home;
        private SqlConnection conn;
        private SqlDataReader reader;
        private ArrayList arrayList;
        private string[] idArray;
        private readonly string user;


        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            loadAutoComplete();
            bankName.CharacterCasing = CharacterCasing.Upper;
            loadUser();
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
            //button1_Click(sender, e);
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
            catch
            {
                MessageBox.Show("1");
            }
        }

        //++++++ My Method End++++

        private void sAVECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void dELETECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // button3_Click(sender, e);
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void rEFRESHToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            refresh();
        }


        private void companyC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                update();
            }
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

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }
    }
}