using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class UserSelect : Form
    {
        public UserSelect()
        {
            InitializeComponent();
        }

        private DB db, db2;
        private SqlDataReader reader, reader2;
        private SqlConnection sqlconn, sqlcon;
        public String type, salaryType, printType, company;

        private void UserSelect_Load(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy";
                db = new DB();
                left.Items.Clear();
                left.Items.Clear();
                using (sqlcon = db.createSqlConnection())
                {
                    if (salaryType.Equals("goverment"))
                    {
                        if (company.Equals("Rasa Bojun"))
                        {
                            reader = new SqlCommand("select name,id from emp where ispaysheet='" + 1 + "' and company='" + company + "'   order by epfNo ", sqlcon).ExecuteReader();
                        }
                        else
                        {
                            reader = new SqlCommand("select name,id from emp where ispaysheet='" + 1 + "' and company='" + company + "' or type='" + "timeBasedMultiCompany" + "'  order by epfNo ", sqlcon).ExecuteReader();
                        }
                    }
                    else
                    {
                        reader = new SqlCommand("select name,id from emp where company='" + company + "' or type='" + "timeBasedMultiCompany" + "' order by id ", sqlcon).ExecuteReader();
                    }
                    while (reader.Read())
                    {
                        left.Items.Add(reader[1].ToString().ToUpper() + "-" + reader[0].ToString().ToUpper());
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (left.Items.Count != 0)
            {
                for (int i = 0; i < left.Items.Count; i++)
                {
                    right.Items.Add(left.Items[i]);
                }
                left.Items.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (right.Items.Count != 0)
            {
                for (int i = 0; i < right.Items.Count; i++)
                {
                    left.Items.Add(right.Items[i]);
                }
                right.Items.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (left.Items.Count != 0 & left.SelectedIndex != -1)
            {
                right.Items.Add(left.SelectedItem);

                left.Items.RemoveAt(left.SelectedIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (right.Items.Count != 0 & right.SelectedIndex != -1)
            {
                left.Items.Add(right.SelectedItem);

                right.Items.RemoveAt(left.SelectedIndex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private string[] empID;

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Sorry You Havent Select Date Period");
                }
                else if (right.Items.Count == 0)
                {
                    MessageBox.Show("Sorry you Havent Select Employee's to Get PaySheet");
                }
                else
                {
                    empID = new string[right.Items.Count];
                    for (int i = 0; i < right.Items.Count; i++)
                    {
                        empID[i] = right.Items[i].ToString().Split('-')[0];
                    }
                    //MessageBox.Show(empID.Length+"cccccccccccccccccccccc");
                    Cursor.Current = Cursors.WaitCursor;
                    if (printType.Equals("paysheet"))
                    {
                        if (salaryType.Equals("goverment"))
                        {
                            if (type.Equals("singleView"))
                            {
                            }
                            else
                            {
                            }
                        }
                        else if (salaryType.Equals("office"))
                        {
                            //   MessageBox.Show(company);
                            Cursor.Current = Cursors.WaitCursor;

                            Cursor.Current = Cursors.Default;
                        }
                    }
                    else if (printType.Equals("timeSheet"))
                    {
                        if (salaryType.Equals("goverment"))
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            Cursor.Current = Cursors.Default;
                        }
                        else if (salaryType.Equals("office"))
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            Cursor.Current = Cursors.Default;
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception)
            {
            }
        }

        private string[] id;
        public string check = "5";

        private string getMOnth(string y)
        {
            string month = "";
            if (y.Equals("January"))
            {
                month = "01";
            }
            else if (y.Equals("February"))
            {
                month = "02";
            }
            else if (y.Equals("March"))
            {
                month = "03";
            }
            else if (y.Equals("April"))
            {
                month = "04";
            }
            else if (y.Equals("May"))
            {
                month = "05";
            }
            else if (y.Equals("June"))
            {
                month = "06";
            }
            else if (y.Equals("July"))
            {
                month = "07";
            }
            else if (y.Equals("August"))
            {
                month = "08";
            }
            else if (y.Equals("September"))
            {
                month = "09";
            }
            if (y.Equals("October"))
            {
                month = "10";
            }
            if (y.Equals("November"))
            {
                month = "11";
            }
            if (y.Equals("December"))
            {
                month = "12";
            }

            return month;
        }
    }
}