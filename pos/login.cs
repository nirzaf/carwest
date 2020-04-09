using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        // My Variable Start
        private DB db;
        private SqlConnection conn;
        private SqlDataReader reader;

        private void login_Load(object sender, EventArgs e)
        {
            db = new DB();
            conn = db.createSqlConnection();
            ActiveControl = userName;
        }

        private void userName_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(userName, password, password, e.KeyValue);
        }

        private void loginC()
        {
            conn.Open();
            reader = new SqlCommand("select username,password from users where username='" + userName.Text + "' and password='" + password.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                Hide();
                new invoiceNew(this, reader[0].ToString(), "CASH").Visible = true;
            }
            else
            {
                MessageBox.Show("Sorry, User Name or Password Invalied");
                userName.Focus();
            }
            conn.Close();
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                userName.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button1.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                loginC();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginC();
        }
    }
}