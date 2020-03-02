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
    public partial class selectItem : Form
    {
        SqlConnection conn;
        SqlDataReader reader;
        DB db;
        string idH;
        invoiceNew homeH;
        public selectItem(string iD, invoiceNew home)
        {
            InitializeComponent();
            idH = iD;
            homeH = home;
        }

        private void selectItem_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            
            db = new DB();
            dataGridView1.AllowUserToAddRows = false;
            try
            {
                conn = db.createSqlConnection();
                conn.Open();
                reader = new SqlCommand("select detail from item where code='" + idH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader.GetString(0).ToUpper();
                }
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select purchasingPrice,qty,date from purchasingPriceList where code='" + idH + "' order by date", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0] + "", reader[1] + "", reader[2] + "");
                }
                conn.Close();

            }
            catch (Exception)
            {

                throw;
            }

        }
        void save()
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            homeH.purchashingPrice = Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
           // homeH.add();
            this.Close();
            homeH.Enabled=true;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==12| e.KeyValue==13)
            {
              
            }
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {



        }
    }
}
