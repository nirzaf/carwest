using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class timeSheet : Form
    {
        public timeSheet(Form home, string user)
        {
            InitializeComponent();
            homeH = home;
            userH = user;
        }
        DB db, db2;
        Form homeH;
        string userH;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        DataGridViewTextBoxColumn btn;
        Int32 count;
        private void timeSheet_Load(object sender, EventArgs e)
        {
            db = new DB();
            conn = db.createSqlConnection();

            db2 = new DB();
            conn2 = db2.createSqlConnection();

            dataGridView1.AllowUserToAddRows = false;
            try
            {
                // MessageBox.Show(db.getMOnthName(DateTime.Now.Month.ToString()));


                comboBox1.SelectedItem = db.getMOnthName(DateTime.Now.Month.ToString());
                this.TopMost = true;
                year.Format = DateTimePickerFormat.Custom;
                year.CustomFormat = "yyyy";



                load();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
            // btn.UseColumnTextForButtonValue = true;


        }
        void load()
        {

            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                btn = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(btn);
                btn.Width = 300;
                btn.HeaderText = "NAME";
                count = 0;
                for (int i = 1; i <= Int32.Parse(db.getLastDate(Int32.Parse(db.getMOnth(comboBox1.SelectedItem.ToString())), year.Value.Year)); i++)
                {
                    count++;
                    count++;
                    btn = new DataGridViewTextBoxColumn();
                    dataGridView1.Columns.Add(btn);
                    btn.Width = 60;
                    btn.HeaderText = i + " (IN)";
                    btn = new DataGridViewTextBoxColumn();
                    dataGridView1.Columns.Add(btn);
                    btn.Width = 60;
                    btn.HeaderText = i + " (OUT)";
                }

                count = 0;

                conn.Open();
                reader = new SqlCommand("select id,name from staff ", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = reader[0] + " " + reader[1].ToString().ToUpper();
                    try
                    {
                        count = 0;
                        for (int i = 1; i <= Int32.Parse(db.getLastDate(Int32.Parse(db.getMOnth(comboBox1.SelectedItem.ToString())), year.Value.Year)); i++)
                        {

                            conn2.Open();
                            reader2 = new SqlCommand("select inTime,OutTime from Timesheet where id='" + reader[0] + "' and date='" + year.Value.Year + "-" + db.getMOnth(comboBox1.SelectedItem.ToString()) + "-" + i + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                count++;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[count].Value = reader2.GetTimeSpan(0).ToString().Split(':')[0] + ":" + reader2.GetTimeSpan(0).ToString().Split(':')[1];
                                count++;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[count].Value = reader2.GetTimeSpan(1).ToString().Split(':')[0] + ":" + reader2.GetTimeSpan(1).ToString().Split(':')[1];

                            }
                            else
                            {
                                count++;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[count].Value = "00:00";
                                count++;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[count].Value = "00:00";

                            }
                            conn2.Close();
                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + "/" + a.StackTrace + "/" + reader[0]);
                        conn2.Close();
                    }
                }
                conn.Close();


            }
            catch (Exception)
            {

            }
        }

        private void timeSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            homeH.Enabled = true;
            homeH.TopMost = true;
        }
        bool check;
        bool checkCell()
        {
            check = true;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
               // MessageBox.Show("1");
                if (check)
                {
                    count = 0;
                    for (int y = 1; y <= Int32.Parse(db.getLastDate(Int32.Parse(db.getMOnth(comboBox1.SelectedItem.ToString())), year.Value.Year)); y++)
                    {
                        if (check)
                        {
                            try
                            {
                                count++;
                                conn.Open();
                                new SqlCommand("delete from testCell",conn).ExecuteNonQuery();
                                conn.Close();

                                conn.Open();
                                new SqlCommand("insert into testCell values('" + dataGridView1.Rows[i].Cells[count].Value + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (Exception a)
                            {
                               // MessageBox.Show(a.Message);
                                check = false;
                                conn.Close();
                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }

            return check;
        }
        Int32 present;
        Int32 getPresent(string inTime, string outTime) {
            try
            {
                present=1;
                if (TimeSpan.Parse(inTime).Seconds==0 | TimeSpan.Parse(outTime).Seconds==0 )
                {
                    present=0;
                }
                if (TimeSpan.Parse(inTime).Seconds>TimeSpan.Parse(outTime).Seconds)
                {
                    present=0;
                }
            }
            catch (Exception)
            {
                present = 0;

            }
            return present;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkCell())
                {
                    if (dataGridView1.Rows.Count != 0)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            count = 0;
                            for (int x = 1; x <= Int32.Parse(db.getLastDate(Int32.Parse(db.getMOnth(comboBox1.SelectedItem.ToString())), year.Value.Year)); x++)
                            {
                                try
                                {
                                    conn.Open();
                                    reader = new SqlCommand("select *  from timesheet where id='" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split(' ')[0] + "' and date='" + year.Value.Year + "-" + db.getMOnth(comboBox1.SelectedItem.ToString()) + "-" + x + "'", conn).ExecuteReader();
                                    if (reader.Read())
                                    {
                                        conn.Close();
                                        conn.Open();
                                        count++;
                                        var a = dataGridView1.Rows[i].Cells[count].Value;
                                        count++;
                                        var b = dataGridView1.Rows[i].Cells[count].Value;
                                        new SqlCommand("update timesheet set inTime='" + a + "',outtime='"+b+"',day='"+getPresent(a+"",b+"")+"' where id='" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split(' ')[0] + "' and date='" + year.Value.Year + "-" + db.getMOnth(comboBox1.SelectedItem.ToString()) + "-" + x + "' ", conn).ExecuteNonQuery();
                                        conn.Close();

                                       
                                    }
                                    else
                                    {
                                        //  MessageBox.Show("2");
                                        conn.Close();
                                        conn.Open();
                                        count++;
                                        var a = dataGridView1.Rows[i].Cells[count].Value;
                                        count++;
                                        var b = dataGridView1.Rows[i].Cells[count].Value;
                                        new SqlCommand("insert into timesheet values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split(' ')[0] + "','" + year.Value.Year + "-" + db.getMOnth(comboBox1.SelectedItem.ToString()) + "-" + x + "','" + a + "','" + b + "','" + getPresent(a + "", b + "") + "')", conn).ExecuteNonQuery();
                                        conn.Close();

                                    }
                                    conn.Close();


                                }
                                catch (Exception a)
                                {
                                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                                }
                            }
                        }
                        MessageBox.Show("Saved Succefully");
                        load();

                    }
                }
                else {
                    MessageBox.Show("Sorry, you Have Enterd Invalied Time");
                }
            }
            catch (Exception)
            {

                //  throw;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            load();
        }

        private void year_ValueChanged(object sender, EventArgs e)
        {
            load();
        }
    }
}
