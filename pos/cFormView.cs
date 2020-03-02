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
    public partial class cFormView : Form
    {
        public cFormView()
        {
            InitializeComponent();
        }
        //Variable Start
        DB db, db2, db3;
        SqlDataReader reader, reader2, reader3;
        SqlConnection sqlconn, sqlconn2, sqlconn3;
        public string[] idArray;
        public string company;

        int tableID = 0;
        Boolean check = false;
        DataTable dt; DataSet ds;
        //Variable End

        //Method Start
        string setAmountFormat(string amount)
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

        //Method End
        private void cFormView_Load(object sender, EventArgs e)
        {
             this.TopMost = true;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";

            db = new DB();
            db2 = new DB();
            db3 = new DB();
            db.setCursoerWait();
            sqlconn = db.createSqlConnection();
            sqlconn2 = db2.createSqlConnection();
            sqlconn3 = db3.createSqlConnection();
            surcharges.Text = "0.0";
            chequeNo.Text = "0";

            sqlconn.Close();
            sqlconn2.Close();
            sqlconn3.Close();
            db.setCursoerDefault();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Process Period");
            }
            else
            {
                //    MessageBox.Show(company);
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




                    sqlconn.Open();
                    reader = new SqlCommand("select * from cForm where monthAndYearOfContribution='" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "'  ", sqlconn).ExecuteReader();
                    dt.Rows.Clear();
                    if (reader.Read())
                    {
                        sqlconn2.Open();
                        reader2 = new SqlCommand("select * from cFormDetail where id='" + reader.GetInt32(14) + "'order by memberNO ", sqlconn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            //     MessageBox.Show(reader[18]+"n  vvvvvv");
                            //   dt.Rows.Add(reader[18], reader[15].ToString().Split(',')[0], reader[15].ToString().Split(',')[1], reader[15].ToString().Split(',')[2], reader[15].ToString().Split(',')[3], reader[16], reader[17], reader[18], "", reader[4], setAmountFormat(reader[5] + ""), setAmountFormat(reader[6] + ""), setAmountFormat(reader[7] + ""), reader[20], reader[20], reader[20], reader2[1], reader2[2], reader2[3], setAmountFormat(reader2[4] + ""), setAmountFormat(reader2[5] + ""), setAmountFormat(reader2[6] + ""), setAmountFormat(reader2[7] + ""), setAmountFormat(reader[11] + ""), setAmountFormat(reader[12] + ""), setAmountFormat(reader[13] + ""), setAmountFormat(reader[14] + ""));
                            dt.Rows.Add(reader2[1], reader2[2], reader2[3], reader2[4], reader2[5], reader2[6], reader2[7]);

                        }
                        sqlconn2.Close();


                        ds.Tables.Add(dt);
                        //ds.WriteXmlSchema("cForm__A.xml");
                        //this.Dispose();

                        //ds.WriteXmlSchema("cForm.xml");
                        //dt.Columns.Add("month", typeof(string));
                        //dt.Columns.Add("contributions", typeof(string));
                        //dt.Columns.Add("surcharges", typeof(string));
                        //dt.Columns.Add("totalRemittance", typeof(string));
                        //dt.Columns.Add("cheequeNo", typeof(string));
                        //dt.Columns.Add("bankName", typeof(string));
                        //dt.Columns.Add("bankBranchName", typeof(string));
                        MessageBox.Show("Downloaded Succesfully");
                        cForm pp = new cForm();
                        pp.SetDataSource(ds);

                          pp.SetParameterValue("month", dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem);
                          pp.SetParameterValue("contributions", setAmountFormat(reader.GetDouble(5) + ""));
                          pp.SetParameterValue("surcharges", setAmountFormat(reader.GetDouble(6) + ""));
                          pp.SetParameterValue("totalRemittance", setAmountFormat(reader.GetDouble(7) + ""));
                          pp.SetParameterValue("cheequeNo", reader[8] );
                          pp.SetParameterValue("bankName", reader[9] );
                          pp.SetParameterValue("bankBranchName", reader[10]);

                        crystalReportViewer1.ReportSource = pp;
                        db.setCursoerDefault();
                    }
                    else
                    {
                        MessageBox.Show("Sorry, C-Form Not Found in Database for Selected Date");
                        // MessageBox.Show(dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem+"/");
                        db.setCursoerDefault();
                    }
                    sqlconn.Close();
                }
                catch (Exception a)
                {
                    sqlconn.Close();
                    sqlconn2.Close();
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                try
                {
                    db.setCursoerWait();
                    sqlconn.Open();
                    reader = new SqlCommand("select max(id) from cForm", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {
                        tableID = reader.GetInt32(0);
                    }
                    sqlconn.Close();
                    tableID++;
                }
                catch (Exception)
                {
                    sqlconn.Close();

                    tableID = 1;
                }

                check = false;
                try
                {
                    sqlconn.Open();
                    reader = new SqlCommand("select * from emp where resgin='" + false + "' and epf!='"+0+"'", sqlconn).ExecuteReader();
                    //MessageBox.Show("1 "+company);
                    sqlconn2.Open();
                    new SqlCommand("delete from cFormDetail where id='" + tableID + "' ", sqlconn2).ExecuteNonQuery();
                    sqlconn2.Close();
                    Double epf = 0;
                    while (reader.Read())
                    {
                        //   MessageBox.Show("2 " + reader.GetDouble(3) / 12 + " b " + reader.GetDouble(3)+" c "+13500/12 );
                        check = true;
                        sqlconn2.Close();
                        sqlconn2.Open();
                        new SqlCommand("insert into cFormDetail values ('" + tableID + "','" + reader[0].ToString().ToUpper() + "','" + reader[20] + "','" + reader[3] + "','" + ((reader.GetDouble(9) + reader.GetDouble(6)) / 20) * 100 + "','" + reader[9] + "','" + reader[6] + "','" + (reader.GetDouble(9) + reader.GetDouble(6)) + "')", sqlconn2).ExecuteNonQuery();
                        sqlconn2.Close();

                    }
                    sqlconn.Close();
                    sqlconn.Open();
                    reader = new SqlCommand("select sum(total),sum(epf12),sum(epf8),sum(totalEarning) from cFormDetail where id='" + tableID + "'", sqlconn).ExecuteReader();
                    if (reader.Read())
                    {


                        if (check)
                        {
                            sqlconn2.Open();
                            new SqlCommand("delete from cForm where monthAndYearOfContribution='" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "' ", sqlconn2).ExecuteNonQuery();
                            sqlconn2.Close();
                            sqlconn2.Open();
                            new SqlCommand("insert into cForm values ('" + "" + "','" + company + "','" + "" + "','" + "" + "','" + dateTimePicker1.Value.ToString("d").Split('/')[2] + "/" + comboBox1.SelectedItem + "','" + reader.GetDouble(0) + "','" + surcharges.Text + "','" + (reader.GetDouble(0) - Double.Parse(surcharges.Text)) + "','" + chequeNo.Text + "','" + "" + "','" + reader.GetDouble(0) + "','" + reader.GetDouble(1) + "','" + reader.GetDouble(2) + "','" + reader.GetDouble(3) + "')", sqlconn2).ExecuteNonQuery();
                            sqlconn2.Close();
                        }
                    }
                    sqlconn.Close();
                    button1_Click(sender, e);
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    sqlconn.Close();
                    sqlconn2.Close();
                }
                db.setCursoerDefault();
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cFormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            // new home("", this).Visible = true;


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }
    }
}
