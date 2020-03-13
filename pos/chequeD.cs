using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class chequeD : Form
    {
        public chequeD()
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
        }

        private double bf, recivedBf, depositbf, sendBf;

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            db.setCursoerWait();

            try
            {
                string name = "";

                sqlconn.Open();
                reader = new SqlCommand("select recived,deposit,send,amount,date,sendBank,chequeNumeber,id from chequeSummery where    date  <= '" + DateTime.Now + "' and resgin='" + false + "' and deposit='" + false + "' and recived='" + true + "'", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    recivedBf = 0; depositbf = 0; sendBf = 0;
                    conn2.Open();
                    reader2 = new SqlCommand("select bankname from bank where bankCode='" + reader[5] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        name = reader2[0] + "";
                    }
                    conn2.Close();

                    dataGridView1.Rows.Add(reader[7], reader.GetDateTime(4).ToShortDateString(), name + " " + reader[5], reader[6], db.setAmountFormat(reader[3] + ""), false);
                }
                sqlconn.Close();
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
            try
            {
                db.setCursoerWait();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        sqlconn.Open();
                        new SqlCommand("update chequeSummery set deposit='" + true + "' where id='" + dataGridView1.Rows[i].Cells[0].Value + "'", sqlconn).ExecuteNonQuery();
                        sqlconn.Close();
                    }
                }
                button1_Click(null, null);
                db.setCursoerDefault();
                MessageBox.Show("Deposited");
            }
            catch (Exception)
            {
                sqlconn.Close();
            }
        }
    }
}