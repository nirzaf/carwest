using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class ExpensesNormal_ : Form
    {
        private System.Windows.Forms.Button button1;
        private Form formH;
        private DB db, db2;
        private SqlConnection conn, conn2;
        private double amount;
        private Int32 maxID, statementH;
        private bool openingBalance, isIncomeH, isExpensesH;
        private SqlDataReader reader, reader2;
        public string indexH, accountNO, payeeName, userH;

        public ExpensesNormal_(Form form, string user)
        {
            InitializeComponent();
            formH = form;
            userH = user;
        }

        private void save()
        {
            try
            {
                var tempID = 0;

                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please Select Account");
                }
                else
                {
                    try
                    {
                        conn.Open();
                        reader = new SqlCommand("select max(id) from receipt ", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            tempID = reader.GetInt32(0);
                            tempID++;
                        }
                        conn.Close();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                        tempID = 1;
                    }
                    //   MessageBox.Show(tempID);
                    conn.Open();
                    new SqlCommand("insert into receipt values('" + tempID + "','" + date.Value + "','" + "DEPRECIATION" + "','" + comboBox1.SelectedItem.ToString().Split('.')[0] + "','" + new amountByName().setAmountName(amountCash.Text) + "','" + amountCash.Text + "','" + payee.Text + "','" + "" + "','" + "" + "','" + userH + "','" + DateTime.Now + "')", conn).ExecuteReader();
                    conn.Close();

                    conn2.Open();
                    new SqlCommand("insert into cashSummery values('" + payee.Text + "/" + tempID + "','" + "DEPRECIATION-MANUAL" + "','" + Double.Parse(amountCash.Text) + "','" + date.Value.ToString() + "','" + userH + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                    // new invoicePrint().setprintReceiprt(tempID + "", conn2, reader, userH);

                    date.Value = DateTime.Now;
                    payee.Text = "";
                    amountCash.Text = "";

                    MessageBox.Show("Saved");
                    load();
                }
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private DataGridViewButtonColumn btn;

        private void itemTable_Load(object sender, EventArgs e)
        {
            formH.Enabled = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();

            payee.CharacterCasing = CharacterCasing.Upper;
            dataGridView1.AllowUserToAddRows = false;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            //dataGridView1.AllowUserToAddRows = false;
            //btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.Width = 60;
            //btn.Text = "PRINT";

            //btn.UseColumnTextForButtonValue = true;
            load();
            conn.Open();
            reader = new SqlCommand("select * from accounts where type='" + "DEPRECIATION" + "' and name!='" + "DEPRECIATION" + "'", conn).ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0] + "." + reader[1]);
            }
            conn.Close();
        }

        private string dateFromS, dateToS;
        private double totalS;

        private void load()
        {
            try
            {
                totalS = 0;
                dateFromS = date.Value.ToShortDateString();
                dateToS = date.Value.ToShortDateString();

                dataGridView1.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select * from cashSummery where date='" + date.Value.ToShortDateString() + "' and remark='" + "DEPRECIATION-MANUAL" + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0).Split('/').Length > 1)
                    {
                        dataGridView1.Rows.Add(reader[0] + "", reader[2] + "", reader.GetString(0).Split('/')[1]);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[0] + "", reader[2] + "", "0");
                    }
                    totalS = totalS + reader.GetDouble(2);
                }
                conn.Close();

                total.Text = db.setAmountFormat(totalS + "");
            }
            catch (Exception)
            {
            }
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsDigit(e.KeyChar)) return;
            //if (Char.IsControl(e.KeyChar)) return;
            ////if ((e.KeyChar == '.')  ) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            //e.Handled = true;
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void itemTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            formH.Enabled = true;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formH.Enabled = true;
            this.Dispose();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void radioCHeque_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void radioCash_CheckedChanged(object sender, EventArgs e)
        {
        }

        private bool check;

        private void payee_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(payee, amountCash, amountCash, e.KeyValue);
        }

        private void payee_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void payee_ImeModeChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void amountCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
            else if (e.KeyValue == 38)
            {
                payee.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    conn.Open();
                    new SqlCommand("delete from cashSummery where date='" + date.Value.ToShortDateString() + "' and remark='" + "DEPRECIATION-MANUAL" + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        conn.Open();
                        new SqlCommand("insert into cashSummery values('" + dataGridView1.Rows[i].Cells[0].Value + "','" + "DEPRECIATION-MANUAL" + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + date.Value.ToShortDateString() + "','" + userH + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Saved Succesfully");
                }
                else if (e.ColumnIndex == 4)
                {
                    new invoicePrint().setprintReceiprt(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), conn, reader, userH);
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            load();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                totalS = 0;
                dateFromS = date.Value.ToShortDateString();
                dateToS = date.Value.ToShortDateString();

                dataGridView1.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select * from cashSummery where date between '" + dateFrom.Value.ToShortDateString() + "' and  '" + dateTo.Value.ToShortDateString() + "' and remark='" + "DEPRECIATION-MANUAL" + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0] + "", reader[2] + "");
                    totalS = totalS + reader.GetDouble(2);
                }
                conn.Close();

                total.Text = db.setAmountFormat(totalS + "");
            }
            catch (Exception)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                try
                {
                    db.setCursoerWait();
                    new invoicePrint().setprintExpenses(conn, reader, dateToS, dateFromS);

                    db.setCursoerDefault();
                }
                catch (Exception)
                {
                    conn.Open();
                }
            }
        }
    }
}