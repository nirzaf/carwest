using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class bankStatement : Form
    {
        public bankStatement()
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
            sqlconn.Open();
            reader = new SqlCommand("select * from bank", sqlconn).ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(2).ToUpper() + "-" + reader[1]);
            }
            sqlconn.Close();
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private double bf, recivedBf, depositbf, sendBf;

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.setCursoerWait();
            try
            {
                recivedBf = 0;
                sqlconn.Open();
                reader = new SqlCommand("select sum(amount) from chequeSummery where sendBank='" + comboBox1.SelectedItem.ToString().Split('-')[1] + "' and depositC='" + true + "' and resgin='" + false + "' and recived='" + true + "' and date <'" + dateTimePicker1.Value + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    recivedBf = reader.GetDouble(0);
                }
                sqlconn.Close();
            }
            catch (Exception)
            {
                sqlconn.Close();
            }
            try
            {
                depositbf = 0;
                sqlconn.Open();
                reader = new SqlCommand("select sum(amount) from chequeSummery where sendBank='" + comboBox1.SelectedItem.ToString().Split('-')[1] + "' and depositC='" + true + "' and resgin='" + false + "' and deposit='" + true + "' and date <'" + dateTimePicker1.Value + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    depositbf = reader.GetDouble(0);
                }
                sqlconn.Close();
            }
            catch (Exception)
            {
                sqlconn.Close();
            }
            try
            {
                sendBf = 0;
                sqlconn.Open();
                reader = new SqlCommand("select sum(amount) from chequeSummery where sendBank='" + comboBox1.SelectedItem.ToString().Split('-')[1] + "' and depositC='" + true + "' and resgin='" + false + "' and send='" + true + "' and date <'" + dateTimePicker1.Value + "'", sqlconn).ExecuteReader();
                if (reader.Read())
                {
                    sendBf = reader.GetDouble(0);
                }
                sqlconn.Close();
            }
            catch (Exception)
            {
                sqlconn.Close();
            }

            bf = (recivedBf + depositbf) - sendBf;

            dataGridView1.Rows.Add("B/F BALANCE", 0, 0, db.setAmountFormat(bf + ""));

            try
            {
                recivedBf = 0;
                sqlconn.Open();
                reader = new SqlCommand("select recived,deposit,send,amount,date from chequeSummery where sendBank='" + comboBox1.SelectedItem.ToString().Split('-')[1] + "' and depositC='" + true + "' and resgin='" + false + "'  and date  between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean(0) || reader.GetBoolean(1))
                    {
                        bf = bf + reader.GetDouble(3);
                        dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), db.setAmountFormat(reader[3] + ""), 0, db.setAmountFormat(bf + ""));
                    }
                    else if (reader.GetBoolean(2))
                    {
                        bf = bf - reader.GetDouble(3);
                        dataGridView1.Rows.Add(reader.GetDateTime(4).ToShortDateString(), 0, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(bf + ""));
                    }
                }
                sqlconn.Close();
                lastBalance.Text = db.setAmountFormat(bf + "");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                sqlconn.Close();
            }
            db.setCursoerDefault();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new bankaccount(this, "").Visible = true;
        }
    }
}