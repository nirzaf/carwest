﻿using System;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace pos
{
    public partial class cusomerQuick : Form
    {
        invoiceNew formH;
        public cusomerQuick(invoiceNew form)
        {
            InitializeComponent();
            formH = form;
        }

        private void pcCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                db.setCursoerWait();
                conn = db.createSqlConnection();
                dataGridView1.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select * from customer where description like '%" + pcCode.Text + "%'", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1].ToString().ToUpper()+" "+reader[2].ToString().ToUpper());
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }
        SqlConnection conn;
        SqlDataReader reader;
        DB db;
        private void cusomerQuick_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            formH.Enabled = false;
            pcCode.CharacterCasing = CharacterCasing.Upper;

            try
            {
                db = new DB();
                db.setCursoerWait();
                conn = db.createSqlConnection();
                dataGridView1.AllowUserToAddRows = false;
                conn.Open();
                new SqlCommand("delete from cashTemp ", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select * from customer", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1].ToString().ToUpper()+" "+reader[2].ToString().ToUpper());
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();

            }
            db.setCursoerDefault();
        }

        private void cusomerQuick_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var y = dataGridView1.SelectedRows[0].Index;
               


                //  invoice ink = (invoice)formH;
                formH.loadCustomer(dataGridView1.Rows[y].Cells[0].Value + "");
                formH.Enabled = true;
                formH.TopMost = true;
                this.Dispose();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var y = dataGridView1.SelectedRows[0].Index;
               


                //  invoice ink = (invoice)formH;
                formH.loadCustomer(dataGridView1.Rows[y].Cells[0].Value + "");
                formH.Enabled = true;
                formH.TopMost = true;
                this.Dispose();
            }
            catch (Exception)
            {

            }
        }

        private void pcCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==12 | e.KeyValue==13)
            {
                  formH.loadCustomer(pcCode.Text);
                formH.Enabled = true;
                formH.TopMost = true;
                this.Dispose();
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }
    }
}
