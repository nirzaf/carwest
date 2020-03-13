﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class newItemInvoice : Form
    {
        public newItemInvoice(invoiceNew form)
        {
            InitializeComponent();
            formH = form;
        }

        private invoiceNew formH;

        private void newItemInvoice_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopMost = true;
            formH.Enabled = false;
            db = new DB();
            conn = db.createSqlConnection();
        }

        private SqlConnection conn;
        private SqlDataReader reader;
        private DB db;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                new SqlCommand("insert into item values ('" + code.Text + "','" + brand.Text + "','" + description.Text + "','" + "" + "','" + "" + "','" + "" + "','" + costPrice.Text + "','" + sellingPrice.Text + "','" + 0 + "','" + 0 + "','" + "" + "','" + db.setItemDescriptionName(new TextBox[] { code, brand, description }) + "')", conn).ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            formH.addItemNew(code.Text);
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
            //  MessageBox.Show(code.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formH.addItemNew(code.Text);
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, brand, brand, e.KeyValue);
        }

        private void brand_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, description, description, e.KeyValue);
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(brand, costPrice, costPrice, e.KeyValue);
        }

        private void costPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(description, sellingPrice, sellingPrice, e.KeyValue);
        }

        private void sellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                costPrice.Focus();
            }
            else if (e.KeyValue == 40 | e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
        }
    }
}