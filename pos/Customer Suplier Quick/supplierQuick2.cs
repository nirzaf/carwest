﻿using System;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace pos
{
    public partial class supplierQuick2 : Form
    {
        private grnEdit formH;

        public supplierQuick2(grnEdit form)
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
                reader = new SqlCommand("select * from supplier where description like '%" + pcCode.Text + "%'", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1].ToString().ToUpper() + " " + reader[2].ToString().ToUpper());
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

        private SqlConnection conn;
        private SqlDataReader reader;
        private DB db;

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
                reader = new SqlCommand("select * from supplier", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1].ToString().ToUpper() + " " + reader[2].ToString().ToUpper());
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
                // this.Dispose();

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
                // this.Dispose();

                //  invoice ink = (invoice)formH;
                formH.loadCustomer(dataGridView1.Rows[y].Cells[0].Value + "");
                formH.Enabled = true;
                formH.TopMost = true;
            }
            catch (Exception)
            {
            }
        }

        private void pcCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
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