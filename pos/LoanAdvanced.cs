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
    public partial class LoanAdvanced : Form
    {
        public LoanAdvanced()
        {
            InitializeComponent();
        }

        private void LoanAdvanced_Load(object sender, EventArgs e)
        {
            this.loanTable.AllowUserToAddRows = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy";
            db = new DB();
            sqlconn = db.createSqlConnection();


            try
            {
                using (sqlconn = new DB().createSqlConnection())
                {

                    sqlconn.Open();
                    reader = new SqlCommand("select EMPID,NAME from EMP WHERE resgin='" + false + "'", sqlconn).ExecuteReader();

                    while (reader.Read())
                    {
                        left.Items.Add(reader[0] + "-" + reader.GetString(1).ToUpper());
                    }
                    reader.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

            loanTable.AllowUserToAddRows = false;
            btn = new DataGridViewButtonColumn();
            loanTable.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            this.TopMost = true;
        }
        DataGridViewButtonColumn btn;
        void clear()
        {

            dateTimePicker1.Value = DateTime.Now;
            loanTable.Rows.Clear();
            loanAmount.Text = "";
            noOfInstallment.Text = "";
            //   loanDate.Value = DateTime.Now;
            left.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {

        }
        DB db;
        SqlDataReader reader;
        SqlConnection sqlconn;
        private void listBox1_Click(object sender, EventArgs e)
        {

            try
            {
                {
                    using (sqlconn = db.createSqlConnection())
                    {
                        sqlconn.Open();
                        loanAmount.Text = "";
                        noOfInstallment.Text = "";
                        paid.Text = "";
                        //  loanDate.Value = DateTime.Now;
                        reader = new SqlCommand("select * from loan where  empid='" + left.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteReader();
                        loanTable.Rows.Clear();
                        while (reader.Read())
                        {
                            loanTable.Rows.Add(reader[0], reader[5], reader[3], db.setAmountFormat(reader[1] + ""), reader[2], reader[6]);
                        }
                        sqlconn.Close();
                        loanAmount.Focus();
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                sqlconn.Close();
            }
        }

        private void LoanAdvanced_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Month");
                }
                else if (comboLoanType.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Loan type");
                }
                else if (left.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Employee");
                }

                else if ((MessageBox.Show("Are you Sure Save these Loan Values for Select User", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {


                    using (sqlconn = db.createSqlConnection())
                    {
                        // new SqlCommand("delete from loan where month='" + dateTimePicker1.Value.ToString("d").ToString().Split('/')[2] + "/" + comboBox2.SelectedItem + "' and empid='" + left.SelectedItem.ToString().Split('-')[0] + "' ", sqlconn).ExecuteNonQuery();


                        sqlconn.Open();
                        new SqlCommand("insert into loan values('" + left.SelectedItem.ToString().Split('-')[0] + "','" + loanAmount.Text + "','" + noOfInstallment.Text + "','" + dateTimePicker1.Value.ToString("d").ToString().Split('/')[2] + "/" + comboBox2.SelectedItem + "','" + DateTime.Now + "','" + comboLoanType.SelectedItem + "','" + paid.Text + "')", sqlconn).ExecuteNonQuery();


                        sqlconn.Close();
                        MessageBox.Show("Succefully Upated Loan Values");
                      
                        ///textBox1.Focus();

                    }
                    var tempID = 0;
                    using (sqlconn = db.createSqlConnection())
                    {
                        try
                        {
                            sqlconn.Open();
                            reader = new SqlCommand("select max(id) from receipt ", sqlconn).ExecuteReader();
                            if (reader.Read())
                            {
                                tempID = reader.GetInt32(0);
                                tempID++;
                            }
                             sqlconn.Close();
                        }
                        catch (Exception a)
                        {
                          //  MessageBox.Show(a.Message);
                            tempID = 1;
                        }
                        //   MessageBox.Show(tempID);
                        sqlconn.Open();
                        new SqlCommand("insert into receipt values('" + tempID + "','" + DateTime.Now + "','" + "EXPENCES" + "','" + "" + "','" + new amountByName().setAmountName(loanAmount.Text) + "','" + loanAmount.Text + "','" + "LOAN-" + left.SelectedItem + "','" + "" + "','" + "" + "','" + "" + "','" + DateTime.Now + "')", sqlconn).ExecuteReader();
                        sqlconn.Close();

                        sqlconn.Open();
                        new SqlCommand("insert into cashSummery values('" + "LOAN "+left.SelectedItem.ToString().Split('-')[1] + "/" + tempID + "','" + "EXPENCES-MANUAL" + "','" + Double.Parse(loanAmount.Text) + "','" + DateTime.Now + "','" + "" + "')", sqlconn).ExecuteNonQuery();
                        sqlconn.Close();
                    }
                    listBox1_Click(sender, e);
                }

            }
            catch (Exception abc)
            {
                sqlconn.Close();
                MessageBox.Show("Internal Error from Saving Earning " + abc.Message + "/" + abc.StackTrace);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (loanAmount.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Loan Amount cant be Empty value");
            }
            else if (noOfInstallment.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Laon Installment cant be Empty value");
            }
            else if (left.SelectedIndex == -1)
            {
                MessageBox.Show("Sorry, Before Updating Deduct Value You must Select Relevant YEAR / MONTH ");
            }
            else
            {
                //"29-"+ getMOnth(comboBox2.SelectedItem.ToString())+"-"+dateTimePicker1.Value.ToString("d").ToString().Split('/')[2]
                loanTable.Rows.Add(loanAmount.Text, noOfInstallment.Text, dateTimePicker1.Value.ToString("d").ToString().Split('/')[2] + "-" + getMOnth(comboBox2.SelectedItem.ToString()) + "-20");

            }
        }
        string getMOnth(string y)
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
            } if (y.Equals("October"))
            {
                month = "10";
            } if (y.Equals("November"))
            {
                month = "11";
            } if (y.Equals("December"))
            {
                month = "12";
            }

            return month;


        }
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(loanTable.RowCount == 0))
                {
                    var index = loanTable.SelectedRows[0].Index;

                    loanTable.Rows.RemoveAt(index);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry , You havnet selected any Row to Delete");
            }
        }

        private void loanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void noOfInstallment_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void LoanAdvanced_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            // new Home("").Visible = true;
        }

        private void LoanAdvanced_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void loanAmount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void comboLine_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        bool tempcheck = false;
        private void loanTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
     
            if (e.ColumnIndex == 6)
            {
                if ((MessageBox.Show("Are You Sure Remove Loan Detail For Selected User?", "Confirmation",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        using (sqlconn = db.createSqlConnection())
                        {

                            new SqlCommand("delete from loan where  id='" + loanTable.Rows[e.RowIndex].Cells[0].Value + "'", sqlconn).ExecuteNonQuery();

                        }
                        listBox1_Click(sender, e);
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        sqlconn.Close();
                    }
                }
            }
        }

        private void loanAmount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(loanAmount, noOfInstallment, noOfInstallment, e.KeyValue);
        }

        private void noOfInstallment_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(loanAmount, paid, paid, e.KeyValue);
        }

        private void paid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void paid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button22_Click(null, null);
            }
            else if (e.KeyValue == 38)
            {
                noOfInstallment.Focus();
            }
        }

        private void left_Click(object sender, EventArgs e)
        {
            listBox1_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                //try
                //{
                //    using (sqlconn = new DB().createSqlConnection())
                //    {

                //        reader = new SqlCommand("select id from emp where empid='" + textBox1.Text + "'", sqlconn).ExecuteReader();
                //        if (reader.Read())
                //        {
                //            sqlconn.Close();
                //            bool test = true;
                //            for (int i = 0; i < left.Items.Count; i++)
                //            {
                //                if (test)
                //                {
                //                    if (left.Items[i].ToString().Split('-')[0].ToString().Equals(textBox1.Text))
                //                    {
                //                        left.SelectedIndex = i;
                //                        listBox1_Click(sender, e);
                //                        test = false;
                //                    }


                //                }
                //            }
                //            loanAmount.Focus();
                //        }
                //        else
                //        {
                //            textBox1.Focus();
                //        }
                //        reader.Close();
                //    }
                //}
                //catch (Exception)
                //{

                //    sqlconn.Close();
                //}
                tempcheck = true;
            }
        }
    }
}
