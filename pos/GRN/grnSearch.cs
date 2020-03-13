using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class grnSearch : Form
    {
        public grnSearch(Form form, String user)
        {
            InitializeComponent();
            this.user = user;
            home = form;
        }

        //+++++++My Variable Start
        private DB db, db2, db3, db4;

        private Form home;
        private SqlConnection conn, conn2, conn3, conn4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private ArrayList arrayList, stockList;
        private Boolean check, checkListBox, states, item, checkStock;
        private string user, type, cutomerID = "", description, invoieNoTemp;
        private String[] idArray;
        private DataGridViewButtonColumn btn;
        private Int32 invoiceMaxNo, rowCount;
        private Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal;
        //+++++++++My Variable End

        //++My Method Start

        private void loadInvoiceByID()
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                invoieNoTemp = invoiceNo.Text.ToString();

                conn.Open();
                reader = new SqlCommand("select * from grn   where id = '" + invoieNoTemp + "' ", conn).ExecuteReader();
                if (reader.Read())
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select name,company from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], reader[5] + "(" + reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + ")", db.setAmountFormat(reader[3] + ""), reader[4].ToString().Split(' ')[0]);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], reader[5] + "(" + reader[2].ToString().ToUpper() + ")", db.setAmountFormat(reader[3] + ""), reader[4].ToString().Split(' ')[0]);
                    }
                    reader2.Close();
                    conn2.Close();
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalied Invoice ID");
                conn.Close();
                conn2.Close();
            }
        }

        private void setAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select name from supplier ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(customer, idArray);
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Columns[6].Visible = reader.GetBoolean(18);
                    dataGridView1.Columns[7].Visible = reader.GetBoolean(21);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //+++++++My Method End
        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void invoiceSearch_Load(object sender, EventArgs e)
        {
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn3 = db3.createSqlConnection();
            db4 = new DB();
            conn4 = db4.createSqlConnection();

            dataGridView1.AllowUserToAddRows = false;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 50;
            btn.Text = "VIEW";
            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "DELETE";
            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "RETURN";
            btn.UseColumnTextForButtonValue = true;

            loadUser();
            setAutoComplete();
            checkBox1.Checked = true;
            checkBox1.Checked = false;

            this.TopMost = true;
            invoiceNo.CharacterCasing = CharacterCasing.Upper;
            customer.CharacterCasing = CharacterCasing.Upper;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadInvoiceByID();
        }

        private void invoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                loadInvoiceByID();
            }
        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                from.Enabled = true;
                toDate.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                from.Enabled = false;
                toDate.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void invoiceSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();
                invoieNoTemp = invoiceNo.Text.ToString();

                conn.Open();
                reader = new SqlCommand("select * from grn   where date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select name,company from supplier where id='" + reader[2] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], reader[5] + "(" + reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + ")", db.setAmountFormat(reader[3] + ""), reader[4].ToString().Split(' ')[0]);
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], reader[5] + "(" + reader[2].ToString().ToUpper() + ")", db.setAmountFormat(reader[3] + ""), reader[4].ToString().Split(' ')[0]);
                    }
                    reader2.Close();
                    conn2.Close();
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();
                conn.Close();

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                // throw;
                conn.Close();
                conn2.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();
                // MessageBox.Show(from.Value.ToShortDateString());

                if (checkBox1.Checked)
                {
                    conn.Open();
                    reader = new SqlCommand("select DISTINCT a.id from grn as a ,supplier as b where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' and a.supplierid = '" + customer.Text + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        // MessageBox.Show(reader[0] + " " + reader4[0]);
                        conn2.Open();
                        reader2 = new SqlCommand("select * from grn as a  where a.id='" + reader[0] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select name,company from supplier where id='" + reader2[2] + "'", conn3).ExecuteReader();
                            if (reader3.Read())
                            {
                                dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader3[0].ToString().ToUpper() + " " + reader3[1].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                            }
                            else
                            {
                                dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader2[2].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                            }
                            reader3.Close();
                            conn3.Close();
                        }

                        reader2.Close();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select DISTINCT a.id from grn as a ,supplier as b where  a.supplierid = '" + customer.Text + "' ", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        // MessageBox.Show(reader[0] + " " + reader4[0]);
                        conn2.Open();
                        reader2 = new SqlCommand("select * from grn as a  where a.id='" + reader[0] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            conn3.Open();
                            reader3 = new SqlCommand("select name,company from supplier where id='" + reader2[2] + "'", conn3).ExecuteReader();
                            if (reader3.Read())
                            {
                                dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader3[0].ToString().ToUpper() + " " + reader3[1].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                            }
                            else
                            {
                                dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader2[2].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                            }
                            reader3.Close();
                            conn3.Close();
                        }

                        reader2.Close();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                // throw;
                conn.Close(); conn2.Close(); conn3.Close(); conn4.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                new purchasing(this, user, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()).Visible = true;
                this.Enabled = true;
            }
            else if (e.ColumnIndex == 6)
            {
                if ((MessageBox.Show("Are you Sure, Delete ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.setCursoerWait();
                    try
                    {
                        conn.Open();
                        invoieNoTemp = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        reader = new SqlCommand("select itemCode,qty,purchasingPrice from grnDetail where grnID='" + invoieNoTemp + "'", conn).ExecuteReader();
                        while (reader.Read())
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select qty from purchasingPriceList where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                reader2.Close();
                                conn2.Close();
                                conn2.Open();
                                new SqlCommand("update purchasingPriceList set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteNonQuery();
                                conn2.Close();
                            }

                            conn2.Close();
                            conn2.Open();
                            new SqlCommand("update item set qty=qty-'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from grnDetail where grnid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from grn where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cardGrn where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from creditGrn where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from chequeGrn where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cashGrn where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into deletedInvoice values('" + invoieNoTemp + "','" + user + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        db.setCursoerDefault();
                        MessageBox.Show("Selected GRN Deleted Succesfully");
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message + " " + a.StackTrace);
                        conn.Close();
                    }
                }
            }
            else if (e.ColumnIndex == 7)
            {
                try
                {
                    new returnGRN(this, user, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()).Visible = true;
                    this.Enabled = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);

                    // throw;
                }
            }
        }

        private void customer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (customer.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    customer.Focus();
                }
                else
                {
                    loadCustomer(customer.Text);
                    button2_Click(sender, e);
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox1.Visible)
                    {
                        listBox1.Focus();
                        listBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        customer.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private string tempCustomer;

        public Boolean loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    //  codeC = id;
                    customer.Text = reader.GetString(2);
                    //  nameC.Text = reader.GetString(1);
                    //  a//ddressC.Text = reader.GetString(3);
                    // mobileNumberC.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    //addressC.SelectionLength = addressC.TextLength;
                    //  db.ToTitleCase(new TextBox[] { companyC, addressC, mobileNumberC });
                }
                else
                {
                    states = false;
                    tempCustomer = "";
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                //  addressC.Focus();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
            return states;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();
                // MessageBox.Show(from.Value.ToShortDateString());

                conn.Open();
                reader = new SqlCommand("select DISTINCT a.id from grn as a ,supplier as b where a.date between '" + from.Value.ToShortDateString() + "' and '" + toDate.Value.ToShortDateString() + "' ", conn).ExecuteReader();
                while (reader.Read())
                {
                    // MessageBox.Show(reader[0] + " " + reader4[0]);
                    conn2.Open();
                    reader2 = new SqlCommand("select * from grn as a  where a.id='" + reader[0] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        conn3.Open();
                        reader3 = new SqlCommand("select name,company from supplier where id='" + reader2[2] + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader3[0].ToString().ToUpper() + " " + reader3[1].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                        }
                        else
                        {
                            dataGridView1.Rows.Add(reader2[0], reader2[1], reader2[5] + "(" + reader2[2].ToString().ToUpper() + ")", db.setAmountFormat(reader2[3] + ""), reader2[4].ToString().Split(' ')[0]);
                        }
                        reader3.Close();
                        conn3.Close();
                    }

                    reader2.Close();
                    conn2.Close();
                }
                reader.Close();
                conn.Close();

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Invalied Date Range");
                }
                else
                {
                    MessageBox.Show("Data Downloaded Succesfully ");
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                // throw;
                conn.Close(); conn2.Close(); conn3.Close(); conn4.Close();
            }
        }

        private void cutomerUnSaved_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cutomerUnSaved_KeyUp(object sender, KeyEventArgs e)
        {
            //
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cutomerUnSaved_TextChanged(object sender, EventArgs e)
        {
        }

        private void customer_TextChanged(object sender, EventArgs e)
        {
        }

        private void customer_KeyUp(object sender, KeyEventArgs e)
        {
            tempCustomer = "";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            {
                db.setList(listBox1, customer, customer.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,description from supplier where description like '%" + customer.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox1.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }
            }
            if (customer.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customer.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
                button2_Click(sender, e);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
            button2_Click(sender, e);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer.Text = listBox1.SelectedItem.ToString();
        }
    }
}