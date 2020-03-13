using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class cashOpeningBalancea : Form
    {
        public cashOpeningBalancea(string user, Form home)
        {
            InitializeComponent();
            userH = user; homeH = home;
        }

        private DB db, db2;
        private Form home;
        private Form homeH;
        private string userH;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private double total = 0;

        private void cashOpeningBalance_Load(object sender, EventArgs e)
        {
            db = new DB();
            conn = db.createSqlConnection();
            this.TopMost = true;
            load();
        }

        private void load()
        {
            conn.Open();
            try
            {
                total = 0;
                reader = new SqlCommand("select amount from cashOpeningBalance where id='" + userH + "' and date='" + DateTime.Now.ToShortDateString() + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    total = reader.GetDouble(0);
                }
                conn.Close();

                currentBalance.Text = total + "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openingBalance.Text.Equals(""))
                {
                    openingBalance.Text = "0";
                }
                conn.Open();
                new SqlCommand("delete from cashOpeningBalance where id='" + userH + "' and date='" + DateTime.Now.ToShortDateString() + "'", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                new SqlCommand("insert into cashOpeningBalance values('" + userH + "','" + DateTime.Now + "','" + openingBalance.Text + "')", conn).ExecuteNonQuery();
                conn.Close();
                openingBalance.Text = "0";
                load();
                MessageBox.Show("Account Value Updated Succefully");
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void openingBalance_TextChanged(object sender, EventArgs e)
        {
        }

        private void openingBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void cashOpeningBalance_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            homeH.Enabled = true;
            homeH.TopMost = true;
        }
    }
}