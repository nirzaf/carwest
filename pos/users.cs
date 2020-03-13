using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class USERS : Form
    {
        public USERS(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;

        // my Variable End
        private void label27_Click(object sender, EventArgs e)
        {
        }

        private void label24_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (customer.Checked)
            {
                customerNew.Checked = true;
                customerEdit.Checked = true;
                customerDelete.Checked = true;

                customerNew.Enabled = true;
                customerEdit.Enabled = true;
                customerDelete.Enabled = true;
            }
            else
            {
                customerNew.Checked = false;
                customerEdit.Checked = false;
                customerDelete.Checked = false;

                customerNew.Enabled = false;
                customerEdit.Enabled = false;
                customerDelete.Enabled = false;
            }
        }

        private void supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (supplier.Checked)
            {
                supplierNew.Checked = true;
                supplierEdit.Checked = true;
                supplierDelete.Checked = true;

                supplierNew.Enabled = true;
                supplierEdit.Enabled = true;
                supplierDelete.Enabled = true;
            }
            else
            {
                supplierNew.Checked = false;
                supplierEdit.Checked = false;
                supplierDelete.Checked = false;

                supplierNew.Enabled = false;
                supplierEdit.Enabled = false;
                supplierDelete.Enabled = false;
            }
        }

        private void item_CheckedChanged(object sender, EventArgs e)
        {
            if (item.Checked)
            {
                itemNew.Checked = true;
                itemEdit.Checked = true;
                itemDelete.Checked = true;

                itemNew.Enabled = true;
                itemEdit.Enabled = true;
                itemDelete.Enabled = true;
            }
            else
            {
                itemNew.Checked = false;
                itemEdit.Checked = false;
                itemDelete.Checked = false;

                itemNew.Enabled = false;
                itemEdit.Enabled = false;
                itemDelete.Enabled = false;
            }
        }

        private void invoice_CheckedChanged(object sender, EventArgs e)
        {
            if (invoice.Checked)
            {
                invoiceNew.Checked = true;
                invoiceEdit.Checked = true;
                invoiceDelete.Checked = true;
                invoiceReturn.Checked = true;

                invoiceNew.Enabled = true;
                invoiceEdit.Enabled = true;
                invoiceDelete.Enabled = true;
                invoiceReturn.Enabled = true;
            }
            else
            {
                invoiceNew.Checked = false;
                invoiceEdit.Checked = false;
                invoiceDelete.Checked = false;
                invoiceReturn.Checked = false;

                invoiceNew.Enabled = false;
                invoiceEdit.Enabled = false;
                invoiceDelete.Enabled = false;
                invoiceReturn.Enabled = false;
            }
        }

        private void grn_CheckedChanged(object sender, EventArgs e)
        {
            if (grn.Checked)
            {
                grnNew.Checked = true;
                grnEdit.Checked = true;
                grnDelete.Checked = true;
                grnReturn.Checked = true;

                grnNew.Enabled = true;
                grnEdit.Enabled = true;
                grnDelete.Enabled = true;
                grnReturn.Enabled = true;
            }
            else
            {
                grnNew.Checked = false;
                grnEdit.Checked = false;
                grnDelete.Checked = false;
                grnReturn.Checked = false;

                grnNew.Enabled = false;
                grnEdit.Enabled = false;
                grnDelete.Enabled = false;
                grnReturn.Enabled = false;
            }
        }

        private void USERS_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            loadUsers();
            clear();
            this.ActiveControl = name;

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom", conn).ExecuteReader();
                if (reader.Read())
                {
                    isCompany.Visible = reader.GetBoolean(2);
                    label1.Visible = reader.GetBoolean(2);
                    dataGridView1.Columns[1].Visible = reader.GetBoolean(2);
                }
                else
                {
                    isCompany.Visible = false;
                    label1.Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                }
                conn.Close();
            }
            catch (Exception)
            {
                isCompany.Visible = false;
                label1.Visible = false;
                dataGridView1.Columns[1].Visible = false;

                conn.Close();
            }
        }

        private void loadUsers()
        {
            try
            {
                conn.Open();
                dataGridView1.Rows.Clear();
                reader = new SqlCommand("select * from users", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString().ToUpper(), reader[1], reader[2], reader[6], reader[7], reader[8], reader[9], reader[10], reader[11], reader[3], reader[4], reader[5], reader[12], reader[13], reader[14], reader[20], reader[16], reader[17], reader[18], reader[21], reader[22], reader[23], reader[24], reader[25], reader[15], reader[19], reader[26]);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/ " + a.StackTrace);
                conn.Close();
            }
        }

        private void clear()
        {
            name.Text = "";
            password.Text = "";
            password2.Text = "";

            item.Checked = true;
            customer.Checked = true;
            supplier.Checked = true;
            invoice.Checked = true;
            grn.Checked = true;

            item.Checked = false;
            customer.Checked = false;
            supplier.Checked = false;
            invoice.Checked = false;
            grn.Checked = false;

            stockUpdate.Checked = false;
            payInvoice.Checked = false;
            user.Checked = false;
            barcode.Checked = false;
            isCompany.Checked = false;
            searchInvoice.Checked = false;
            searchGrn.Checked = false;
            name.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text.Equals(""))
            {
                MessageBox.Show("Please Enter User Name");
                name.Focus();
            }
            else if (password.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Password");
                password.Focus();
            }
            else if (!password.Text.ToUpper().Equals(password2.Text.ToUpper()))
            {
                MessageBox.Show("Sorry , Password Not Match");
                password2.Focus();
            }
            else
            {
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select username from users where username='" + name.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        if ((MessageBox.Show("This User Already in System, Update User? ", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                        {
                            reader.Close();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("delete from users where username='" + name.Text + "'", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("insert into users values('" + name.Text + "','" + password.Text + "','" + isCompany.Checked + "','" + itemNew.Checked + "','" + itemEdit.Checked + "','" + itemDelete.Checked + "','" + customerNew.Checked + "','" + customerEdit.Checked + "','" + customerDelete.Checked + "','" + supplierNew.Checked + "','" + supplierEdit.Checked + "','" + supplierDelete.Checked + "','" + invoiceNew.Checked + "','" + invoiceEdit.Checked + "','" + invoiceDelete.Checked + "','" + searchInvoice.Checked + "','" + grnNew.Checked + "','" + grnEdit.Checked + "','" + grnDelete.Checked + "','" + searchGrn.Checked + "','" + invoiceReturn.Checked + "','" + grnReturn.Checked + "','" + stockUpdate.Checked + "','" + user.Checked + "','" + payInvoice.Checked + "','" + barcode.Checked + "','" + checkBoxChequeDeposit.Checked + "')", conn).ExecuteNonQuery();
                            conn.Close();

                            loadUsers();
                            clear();
                            MessageBox.Show("Succesfuly Updated");
                        }
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into users values('" + name.Text + "','" + password.Text + "','" + isCompany.Checked + "','" + itemNew.Checked + "','" + itemEdit.Checked + "','" + itemDelete.Checked + "','" + customerNew.Checked + "','" + customerEdit.Checked + "','" + customerDelete.Checked + "','" + supplierNew.Checked + "','" + supplierEdit.Checked + "','" + supplierDelete.Checked + "','" + invoiceNew.Checked + "','" + invoiceEdit.Checked + "','" + invoiceDelete.Checked + "','" + searchInvoice.Checked + "','" + grnNew.Checked + "','" + grnEdit.Checked + "','" + grnDelete.Checked + "','" + searchGrn.Checked + "','" + invoiceReturn.Checked + "','" + grnReturn.Checked + "','" + stockUpdate.Checked + "','" + user.Checked + "','" + payInvoice.Checked + "','" + barcode.Checked + "','" + checkBoxChequeDeposit.Checked + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        loadUsers();
                        clear();
                        MessageBox.Show("Succesfuly Updated");
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                    conn.Close();
                }
            }
        }

        private void name_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(name, password, password, e.KeyValue);
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(name, password2, password2, e.KeyValue);
        }

        private void password2_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(password, password2, password2, e.KeyValue);
        }

        private void USERS_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }
    }
}