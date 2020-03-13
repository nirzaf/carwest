using System;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace pos
{
    public partial class invoiceSelection : Form
    {
        public invoiceSelection(string cus, string amo, string use, string tempCreID, invoiceCreditPay form)
        {
            InitializeComponent();
            customer = cus;
            amount2 = Double.Parse(amo);
            amountTemp2 = amount2;
            user = use;
            tempcreditPaidID = tempCreID;
            home = form;
        }

        private string customer, user, tempcreditPaidID;
        private double amount, amount2;

        private void invoiceSelection_Load(object sender, EventArgs e)
        {
            // MessageBox.Show("1");

            dataGridView1.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db = new DB();
            conn2 = db.createSqlConnection();
            // MessageBox.Show("1c");
            load();
            this.TopMost = true;
            //MessageBox.Show("1b");
        }

        private DB db, db2;
        private invoiceCreditPay home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private double amountTemp, amountTemp2;
        private string tempInvoiceNO = "";

        private void load()
        {
            try
            {
                conn2.Open();
                // MessageBox.Show(customer);
                reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + customer + "' order by requstdate", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    amountTemp = 0;
                    conn.Open();
                    reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        amountTemp = amountTemp + reader.GetDouble(0);
                    }

                    if (amountTemp < reader2.GetDouble(1))
                    {
                        dataGridView1.Rows.Add("R-" + reader2[0], db.setAmountFormat(reader2[1] + ""), db.setAmountFormat(amountTemp + ""), db.setAmountFormat((reader2.GetDouble(1) - amountTemp) + ""), false, (reader2.GetDouble(1) - amountTemp), reader2[1]);
                    }
                    conn.Close();
                }
                conn2.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        if (amount2 != 0)
                        {
                            amount = 0;

                            amount = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                            if (amount != 0)
                            {
                                if (amount2 <= amount)
                                {
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount2 + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                    // MessageBox.Show(tempInvoiceNO);
                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                    }
                                    amount2 = 0;
                                }
                                else
                                {
                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1] + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + amount + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + dataGridView1.Rows[i].Cells[0].Value.ToString().Split('-')[1];
                                    }
                                    amount2 = amount2 - amount;
                                }
                            }
                        }
                    }
                }

                if (amount2 > 0)
                {
                    conn2.Open();
                    bool states = false; ;
                    reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + customer + "' order by requstdate", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        if (amount2 != 0)
                        {
                            amount = 0;

                            conn.Open();
                            reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                states = true;
                                amount = amount + reader.GetDouble(0);
                            }

                            conn.Close();
                            if (!states)
                            {
                                amount = amount + reader2.GetDouble(1);
                            }
                            else
                            {
                                amount = reader2.GetDouble(1) - amount;
                            }

                            if (amount != 0)
                            {
                                if (amount2 <= amount)
                                {
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                    //  MessageBox.Show("1" + tempInvoiceNO);
                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                    }

                                    amount2 = 0;
                                }
                                else
                                {
                                    //  MessageBox.Show("2" + tempInvoiceNO);
                                    conn.Open();
                                    new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                    conn.Close();

                                    if (tempInvoiceNO.Equals(""))
                                    {
                                        tempInvoiceNO = "R-" + reader2[0];
                                    }
                                    else
                                    {
                                        tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                    }
                                    amount2 = amount2 - amount;
                                }
                            }
                        }
                    }
                    conn2.Close();
                }
                home.saveSub(amountTemp2, tempInvoiceNO);
                this.Dispose();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + " " + a.StackTrace);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 4)
            //{
            //    var a = 0.0;
            //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().ToUpper());
            //        if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().ToUpper().Equals("TRUE"))
            //        {
            //            a = a + Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            //        }
            //    }
            //    balance.Text = db.setAmountFormat((amountTemp2 - a) + "");

            //}
        }

        private void dataGridView1_CausesValidationChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
        }

        private void invoiceSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (amount2 > 0)
            {
                conn2.Open();
                bool states = false; ;
                reader2 = new SqlCommand("select invoiceid,balance,amount from creditInvoiceRetail where customerid='" + customer + "' order by requstdate", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    if (amount2 != 0)
                    {
                        amount = 0;

                        conn.Open();
                        reader = new SqlCommand("select paid from invoiceCreditPaid where invoiceid='" + reader2[0] + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            states = true;
                            amount = amount + reader.GetDouble(0);
                        }

                        conn.Close();
                        if (!states)
                        {
                            amount = amount + reader2.GetDouble(1);
                        }
                        else
                        {
                            amount = reader2.GetDouble(1) - amount;
                        }

                        if (amount != 0)
                        {
                            if (amount2 <= amount)
                            {
                                conn.Open();
                                new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount2 + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                conn.Close();
                                //  MessageBox.Show("1" + tempInvoiceNO);
                                if (tempInvoiceNO.Equals(""))
                                {
                                    tempInvoiceNO = "R-" + reader2[0];
                                }
                                else
                                {
                                    tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                }

                                amount2 = 0;
                            }
                            else
                            {
                                //  MessageBox.Show("2" + tempInvoiceNO);
                                conn.Open();
                                new SqlCommand("insert into invoiceCreditPaid values('" + reader2[0] + "','" + reader2[2] + "','" + amount + "','" + 0 + "','" + user + "','" + DateTime.Now + "','" + DateTime.Now + "','" + tempcreditPaidID + "')", conn).ExecuteNonQuery();
                                conn.Close();

                                if (tempInvoiceNO.Equals(""))
                                {
                                    tempInvoiceNO = "R-" + reader2[0];
                                }
                                else
                                {
                                    tempInvoiceNO = tempInvoiceNO + "/R-" + reader2[0];
                                }
                                amount2 = amount2 - amount;
                            }
                        }
                    }
                }
                conn2.Close();
            }
            home.saveSub(amountTemp2, tempInvoiceNO);
            this.Dispose();
        }
    }
}