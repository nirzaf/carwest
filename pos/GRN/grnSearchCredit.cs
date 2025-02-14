﻿using System;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

using System.Text;

using System.Windows.Forms;

namespace pos
{
    public partial class grnSearchCredit : Form
    {
        public grnSearchCredit(Form form, String user)
        {
            InitializeComponent();
            this.user = user;
            home = form;
        }
        //+++++++My Variable Start
        DB db, db2, db3, db4;
        Form home;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        ArrayList arrayList, stockList;
        Boolean check, checkListBox, states, item, checkStock;
        string user, type, cutomerID = "", supplierDetail, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount;
        Double amount, purchashingPrice, qtyTemp, tempPaid, profit, profitTotal;
        //+++++++++My Variable End

        //++My Method Start

        void loadInvoiceByID()
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                invoieNoTemp = invoiceNo.Text.ToString();

                conn.Open();
                reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where a.id = '" + invoieNoTemp + "' and a.id=b.invoiceid and b.credit='" + true + "' ", conn).ExecuteReader();
                if (reader.Read())
                {

                    supplierDetail = reader[2].ToString().ToUpper();

                    try
                    {
                        tempPaid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + invoieNoTemp + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            tempPaid = reader2.GetDouble(0);
                        }
                        conn2.Close();
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                    }
                    tempPaid = tempPaid + reader.GetDouble(7);
                    dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);


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
        void loadInvoiceByIDInvoice()
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                invoieNoTemp = invoiceNoSupplier.Text.ToString();

                conn.Open();
                reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where a.idSupplier = '" + invoieNoTemp + "' and a.id=b.invoiceid and b.credit='" + true + "' ", conn).ExecuteReader();
                if (reader.Read())
                {

                    supplierDetail = reader[2].ToString().ToUpper();

                    try
                    {
                        tempPaid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + invoieNoTemp + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            tempPaid = reader2.GetDouble(0);
                        }
                        conn2.Close();
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                    }
                    tempPaid = tempPaid + reader.GetDouble(7);
                    dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);


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
      public  void load()
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                invoieNoTemp = invoiceNo.Text.ToString();

                conn.Open();
                if (checkBox1.Checked)
                {
                    reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where  a.id=b.invoiceid and b.credit='" + true + "' and a.date between '"+from.Value+"' and '"+toDate.Value+"' order by a.date", conn).ExecuteReader();
                }
                else
                {

                    reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where  a.id=b.invoiceid and b.credit='" + true + "' order by a.date", conn).ExecuteReader();
               
                }
                while (reader.Read())
                {

                    supplierDetail = reader[2].ToString().ToUpper();


                    try
                    {
                        tempPaid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            tempPaid = reader2.GetDouble(0);
                        }
                        conn2.Close();
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                    }
                    tempPaid = tempPaid + reader.GetDouble(7);
                    if (radioPaid.Checked)
                    {
                        if (tempPaid >= reader.GetDouble(3))
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                        }
                    }
                    else if (radioNotPaid.Checked)
                    {
                        if (tempPaid < reader.GetDouble(3))
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                    }


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
        void loadWithSupp()
        {
            try
            {
                dataGridView1.Rows.Clear();
                db.setCursoerWait();

                invoieNoTemp = invoiceNo.Text.ToString();

                conn.Open();
                if (checkBox1.Checked)
                {
                    reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where  a.id=b.invoiceid and b.credit='" + true + "' and a.date between '" + from.Value + "' and '" + toDate.Value + "' and a.supplierID='"+customer.Text+"' order by a.date", conn).ExecuteReader();
                }
                else
                {

                    reader = new SqlCommand("select a.* from grn as a,grnTerm as b  where  a.id=b.invoiceid and b.credit='" + true + "' and a.supplierID='" + customer.Text + "' order by a.date", conn).ExecuteReader();

                }
                while (reader.Read())
                {

                    supplierDetail = reader[2].ToString().ToUpper();


                    try
                    {
                        tempPaid = 0;
                        conn2.Open();
                        reader2 = new SqlCommand("select sum(paid) from grnCreditPaid where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            tempPaid = reader2.GetDouble(0);
                        }
                        conn2.Close();
                    }
                    catch (Exception)
                    {
                        conn2.Close();
                    }
                    tempPaid = tempPaid + reader.GetDouble(7);
                    if (radioPaid.Checked)
                    {
                        if (tempPaid >= reader.GetDouble(3))
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                        }
                    }
                    else if (radioNotPaid.Checked)
                    {
                        if (tempPaid < reader.GetDouble(3))
                        {
                            dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1], supplierDetail, db.setAmountFormat(reader[3] + ""), db.setAmountFormat(tempPaid + ""), db.setAmountFormat(reader.GetDouble(3) - tempPaid + ""), reader[4].ToString().Split(' ')[0]);

                    }


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
        void setAutoComplete()
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
        void loadUser()
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
            btn.Width = 80;
            btn.Text = "VIEW GRN";
            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "PAY";
            btn.UseColumnTextForButtonValue = true;


            loadUser();
            setAutoComplete();
            checkBox1.Checked = true;
            checkBox1.Checked = false;

            this.TopMost = true;
            invoiceNo.CharacterCasing = CharacterCasing.Upper;
            customer.CharacterCasing = CharacterCasing.Upper;


            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            //MessageBox.Show(height+","+width);

            /// tabControl1.Width = width-(button2.Width+100);
            //  button2.Location = new Point(width - 100, height - 50);
            // panel2.Width = width - 3;
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
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadWithSupp();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                new grnEdit(this, user, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()).Visible = true;
                this.Enabled = true;
            }

            else if (e.ColumnIndex == 8)
            {
                new grnCreditPayEdit2(this, user, dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), e.RowIndex + "").Visible = true;
             
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
                    loadWithSupp();
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
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            {
                db.setList(listBox1, customer, customer.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select description from supplier where description like '%" + customer.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        //   MessageBox.Show("1");
                        listBox1.Items.Add(reader[0].ToString().ToUpper());
                        listBox1.Visible = true;
                        listBox1.Height = 50;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {//
                    MessageBox.Show(a.StackTrace + "/" + a.Message);
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
                customer.Text = listBox1.SelectedItem.ToString() ;
                customer.SelectionLength = customer.MaxLength;
                loadWithSupp();
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            customer.Text = listBox1.SelectedItem.ToString();
            customer.SelectionLength = customer.MaxLength;
            loadWithSupp();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer.Text = listBox1.SelectedItem.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            load();
        }

        private void invoiceNoSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                loadInvoiceByIDInvoice();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadInvoiceByIDInvoice();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }
    }
}
