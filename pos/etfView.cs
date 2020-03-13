using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class etfView : Form
    {
        public etfView()
        {
            InitializeComponent();
        }

        private DB db, db2;
        private SqlDataReader reader, reader2;
        private SqlConnection sqlconn, sqlconn2, sqlconn3;
        public string[] idArray;
        public string company;
        private DataTable dt; private DataSet ds;

        private void cFormView_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";

            db = new DB();
            db2 = new DB();
            sqlconn = db.createSqlConnection();
            sqlconn2 = db2.createSqlConnection();
            sqlconn3 = new DB().createSqlConnection();
            surcharges.Text = "0.0";
            otherPayments.Text = "0.0";
            chequeNo.Text = "0";
            sqlconn.Open();
            sqlconn2.Open();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private string setAmountFormat(string amount)
        {
            string amountI = (int)Double.Parse(amount) + "";

            double amountD = Double.Parse(amount);
            if (amountI.Length == 1)
            {
                amount = String.Format("{0:0.00}", amountD);
            }
            else if (amountI.Length == 2)
            {
                amount = String.Format("{0:00.00}", amountD);
            }
            else if (amountI.Length == 3)
            {
                amount = String.Format("{0:000.00}", amountD);
            }
            else if (amountI.Length == 4)
            {
                amount = String.Format("{0:0,000.00}", amountD);
            }
            else if (amountI.Length == 5)
            {
                amount = String.Format("{0:00,000.00}", amountD);
                ///price = "hu";
            }
            else if (amountI.Length == 6)
            {
                amount = String.Format("{0:000,000.00}", amountD);
            }
            else if (amountI.Length == 7)
            {
                amount = String.Format("{0:0,000,000.00}", amountD);
            }
            else if (amountI.Length == 8)
            {
                amount = String.Format("{0:00,000,000.00}", amountD);
            }
            else if (amountI.Length == 9)
            {
                amount = String.Format("{0:000,000,000.00}", amountD);
            }
            else if (amountI.Length == 10)
            {
                amount = String.Format("{0:0,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 11)
            {
                amount = String.Format("{0:00,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 12)
            {
                amount = String.Format("{0:000,000,000,000.00}", amountD);
            }
            else if (amountI.Length == 13)
            {
                amount = String.Format("{0:0,000,000,000,000.00}", amountD);
            }

            return amount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Process Period");
            }
            else
            {
                try
                {
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();

                    dt.Columns.Add("employeeName", typeof(string));
                    dt.Columns.Add("NIC", typeof(string));
                    dt.Columns.Add("memberNo", typeof(string));
                    dt.Columns.Add("totalMember", typeof(float));
                    dt.Columns.Add("epf12", typeof(float));
                    dt.Columns.Add("epf8", typeof(float));
                    dt.Columns.Add("totalEarningEmp", typeof(float));
                    sqlconn.Close();
                    sqlconn.Open();
                    reader = new SqlCommand("select * from etfForm where monthAndYearOfContribution='" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "'  ", sqlconn).ExecuteReader();
                    dt.Rows.Clear();
                    if (reader.Read())
                    {
                        reader2 = new SqlCommand("select * from etfFormDetail where id='" + reader.GetInt32(9) + "'order by memberNO ", sqlconn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            //    MessageBox.Show(reader[3]+"");
                            //   dt.Rows.Add(reader[18], reader[15].ToString().Split(',')[0], reader[15].ToString().Split(',')[1], reader[15].ToString().Split(',')[2], reader[15].ToString().Split(',')[3], reader[16], reader[17], reader[18], "", reader[4], setAmountFormat(reader[5] + ""), setAmountFormat(reader[6] + ""), setAmountFormat(reader[7] + ""), reader[20], reader[20], reader[20], reader2[1], reader2[2], reader2[3], setAmountFormat(reader2[4] + ""), setAmountFormat(reader2[5] + ""), setAmountFormat(reader2[6] + ""), setAmountFormat(reader2[7] + ""), setAmountFormat(reader[11] + ""), setAmountFormat(reader[12] + ""), setAmountFormat(reader[13] + ""), setAmountFormat(reader[14] + ""));
                            //    dt.Rows.Add(reader[13], company.ToString().ToUpper() + ",", reader[15].ToString().Split(',')[0] + ",", reader[15].ToString().Split(',')[1] + ",", reader[15].ToString().Split(',')[2], reader[16], "", reader[17], reader[19], reader[20], dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem, setAmountFormat(reader.GetDouble(5) + ""), setAmountFormat(surcharges.Text + ""), setAmountFormat(reader.GetDouble(5) - Double.Parse(surcharges.Text) + ""), chequeNo.Text, reader[22], reader[23], reader2[1], reader2[2], reader2[3], setAmountFormat(reader2[4] + ""), setAmountFormat(reader2[5] + ""), setAmountFormat(reader2[6] + ""), setAmountFormat(reader2[7] + ""), setAmountFormat(reader.GetDouble(5) + ""), setAmountFormat(reader.GetDouble(11) + ""), setAmountFormat(reader.GetDouble(12) + ""), setAmountFormat(reader.GetDouble(13) + ""));
                            dt.Rows.Add(reader2[1], reader2[2], reader2[3]);
                        }

                        //ds.WriteXmlSchema("cForm.xml");
                        ds.Tables.Add(dt);
                        //ds.WriteXmlSchema("cForm.xml");
                        etf pp = new etf();
                        pp.SetDataSource(ds);
                        pp.SetParameterValue("month", dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem);
                        pp.SetParameterValue("contributions", setAmountFormat(reader.GetDouble(3) + ""));
                        pp.SetParameterValue("surcharges", setAmountFormat(reader.GetDouble(4) + ""));
                        pp.SetParameterValue("otherChargers", setAmountFormat(reader.GetDouble(5) + ""));

                        pp.SetParameterValue("totalRemittance", setAmountFormat(reader.GetDouble(6) + ""));

                        pp.SetParameterValue("cheequeNo", reader[7]);

                        crystalReportViewer1.ReportSource = pp;
                        reader2.Close();
                        db.setCursoerDefault();
                        MessageBox.Show("Downloaded Succesfully");
                    }
                    else
                    {
                        MessageBox.Show("Sorry, ETF-Form Not Found in Database for Selected Date");
                    }
                    reader.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.StackTrace);
                }
            }
        }

        private int tableID = 0;
        private Boolean check = false;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Process Period");
                }
                else if (surcharges.Text.Equals("") | chequeNo.Text.Equals(""))
                {
                    MessageBox.Show("Surcharges or Cheque No Cannot be Empty Value. please Enter atleast 0 value");
                }
                else if ((MessageBox.Show("Manual Process will be Delete all Manual Data ,Are you sure Contunie ?", "Confirmation",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question,
         MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.setCursoerWait();
                    try
                    {
                        reader = new SqlCommand("select max(id) from etfForm", sqlconn).ExecuteReader();
                        if (reader.Read())
                        {
                            tableID = reader.GetInt32(0);
                        }
                        reader.Close();
                        tableID++;
                    }
                    catch (Exception)
                    {
                        tableID = 1;
                        reader.Close();
                    }

                    check = false;
                    Double epf;

                    reader = new SqlCommand("select * from emp where resgin='" + false + "' and epf!='" + 0 + "'", sqlconn).ExecuteReader();

                    new SqlCommand("delete from etfFormDetail where id='" + tableID + "' ", sqlconn2).ExecuteNonQuery();
                    epf = 0;
                    while (reader.Read())
                    {
                        //   MessageBox.Show("2 " + reader.GetDouble(3) / 12 + " b " + reader.GetDouble(3)+" c "+13500/12 );
                        check = true;

                        new SqlCommand("insert into etfFormDetail values ('" + tableID + "','" + reader[0].ToString().ToUpper() + "','" + reader[20] + "','" + reader[3] + "','" + reader[7] + "')", sqlconn2).ExecuteNonQuery();
                    }
                    reader.Close();
                    reader = new SqlCommand("select sum(total),count(id) from etfFormDetail where id='" + tableID + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        if (check)
                        {
                            new SqlCommand("delete from etfForm where monthAndYearOfContribution='" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "' and company='" + company + "'", sqlconn2).ExecuteNonQuery();
                            new SqlCommand("insert into etfForm values ('" + company + "','" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "','" + reader.GetInt32(1) + "','" + reader.GetDouble(0) + "','" + surcharges.Text + "','" + otherPayments.Text + "','" + (reader.GetDouble(0) - (Double.Parse(surcharges.Text) + Double.Parse(otherPayments.Text))) + "','" + chequeNo.Text + "','" + reader.GetDouble(0) + "')", sqlconn2).ExecuteNonQuery();
                        }
                    }

                    button1_Click(sender, e);
                    db.setCursoerDefault();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void surcharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void chequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;

            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void otherPayments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void etfView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            //new home("",this).Visible = true;
        }
    }
}