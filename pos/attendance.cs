using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class attendance : Form
    {
        public attendance()
        {
            InitializeComponent();
        }

        private DB db, db2;
        private SqlDataReader reader, reader2;
        private SqlConnection sqlconn, sqlconn2;

        private void attendance_Load(object sender, EventArgs e)
        {
            try
            {
                db = new DB();
                sqlconn = db.createSqlConnection();
                db2 = new DB();
                sqlconn2 = db2.createSqlConnection();
            }
            catch (Exception abc)
            {
                //sqlconn.Close();
                MessageBox.Show("Database Connectivity Error g" + abc.Message);
            }
            dataGridView1.AllowUserToAddRows = false;
            this.TopMost = true;
            dateTimePicker1_CloseUp(null, null);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sqlconn.Open();
                    reader = new SqlCommand("select * from attendance where empid='" + dataGridView1.Rows[i].Cells[0].Value + "' and date='" + dateTimePicker1.Value + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        if (password.Text.Equals("ra1471sika"))
                        {
                            sqlconn.Close();
                            sqlconn.Open();
                            new SqlCommand("delete from attendance where empid='" + dataGridView1.Rows[i].Cells[0].Value + "' and date='" + dateTimePicker1.Value + "'", sqlconn).ExecuteNonQuery();
                            sqlconn.Close();
                            sqlconn.Open();
                            new SqlCommand("insert into attendance values ('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dateTimePicker1.Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "')", sqlconn).ExecuteNonQuery();
                            sqlconn.Close();
                        }
                        else
                        {
                            MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value + " Detail will not be Saved .Password Required");
                            password.Focus();
                        }
                    }
                    else
                    {
                        sqlconn.Close();
                        sqlconn.Open();
                        new SqlCommand("insert into attendance values ('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dateTimePicker1.Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "')", sqlconn).ExecuteNonQuery();
                        sqlconn.Close();
                    }
                    sqlconn.Close();
                }
                dateTimePicker1_CloseUp(null, null);

                MessageBox.Show("Saved Succefully");
            }
            catch (Exception)
            {
                sqlconn.Close();
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                sqlconn.Open();
                reader = new SqlCommand("select name,empid from emp where resgin ='" + false + "'", sqlconn).ExecuteReader();
                while (reader.Read())
                {
                    sqlconn2.Open();
                    reader2 = new SqlCommand("select present,punish from attendance where empid='" + reader[1] + "' and date='" + dateTimePicker1.Value + "'", sqlconn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        dataGridView1.Rows.Add(reader[1], reader[0], reader2[0], reader2[1]);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[1], reader[0], false, false);
                    }

                    sqlconn2.Close();
                }
                sqlconn.Close();

                MessageBox.Show("Downloaded Succefully");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }
    }
}