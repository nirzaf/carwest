using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class supplierReport : Form
    {
        public supplierReport(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private supplierReportALL pp;
        private DB db;
        private string userH, queary, userName;
        private SqlConnection conn;
        private string[] idArray;
        private SqlDataReader reader;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private double amountCost;
        private DataGridViewButtonColumn btn;

        //
        //++++++ My Method Start+++
        private void loadAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select brand from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(brandName, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select categorey from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(categoryName, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select description from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(descriptionName, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select code from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(itemCode, idArray);
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    userName = reader.GetString(0).ToUpper();
                    dataGridView1.Columns[8].Visible = reader.GetBoolean(6);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //
        private void stockReport_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            this.ActiveControl = name;
            radioSearchByDate.Checked = true;
            radioAdvancedSearch.Checked = true;
            searchALL.Checked = true;
            this.TopMost = true;
            loadAutoComplete();
            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "EDIT";
            btn.UseColumnTextForButtonValue = true;
        }

        private void radioSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            itemCode.Enabled = radioSearchByDate.Checked;
            itemCode.Focus();
        }

        private void radioAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            checkBrand.Checked = true;
            checkCategory.Checked = true;
            checkDescription.Checked = true;

            checkBrand.Checked = false;
            checkCategory.Checked = false;
            checkDescription.Checked = false;
            checkBrand.Enabled = radioAdvancedSearch.Checked;
            checkCategory.Enabled = radioAdvancedSearch.Checked;
            checkDescription.Enabled = radioAdvancedSearch.Checked;
        }

        private void checkBrand_CheckedChanged(object sender, EventArgs e)
        {
            brandName.Enabled = checkBrand.Checked;
            brandName.Focus();
        }

        private void checkCategory_CheckedChanged(object sender, EventArgs e)
        {
            categoryName.Enabled = checkCategory.Checked;
            categoryName.Focus();
        }

        private void checkDescription_CheckedChanged(object sender, EventArgs e)
        {
            descriptionName.Enabled = checkDescription.Checked;
            descriptionName.Focus();
        }

        private void checkQty_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMinValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMaxValue_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void stockReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void checkQty_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                queary = "";
                if (radioSearchByDate.Checked)
                {
                    queary = "id='" + itemCode.Text + "'";
                }
                else if (radioAdvancedSearch.Checked)
                {
                    if (checkBrand.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "name='" + brandName.Text + "'";
                        }
                        else
                        {
                            queary = " and name='" + brandName.Text + "'";
                        }
                    }

                    if (checkCategory.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "company='" + categoryName.Text + "'";
                        }
                        else
                        {
                            queary = " and company='" + categoryName.Text + "'";
                        }
                    }
                    if (checkDescription.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "email='" + descriptionName.Text + "'";
                        }
                        else
                        {
                            queary = " and email='" + descriptionName.Text + "'";
                        }
                    }
                }

                if (!queary.Equals(""))
                {
                    queary = "where " + queary;
                }
                queary = queary + " order by " + comboOrderBY.SelectedItem;
                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }

                dataGridView1.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select * from supplier  " + queary + "", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), reader.GetString(4).ToUpper(), reader.GetString(5).ToUpper(), reader.GetString(6).ToUpper(), reader.GetString(7).ToUpper());
                }
                conn.Close();

                if (dataGridView1.Rows.Count == 0)
                {
                    db.setCursoerDefault();
                    MessageBox.Show("Sorry, No Data Re-Present on Your Selected Key-Words.");
                }
                else
                {
                    db.setCursoerDefault();
                    MessageBox.Show("Download Succesfully");
                }
            }
            catch (Exception a)
            {
                conn.Close();
                ///MessageBox.Show(a.Message+"/"+queary);
            }
        }

        private void searchALL_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void sETTINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
        }

        private void itemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void brandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void name_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                db.setCursoerWait();
                queary = "";
                queary = queary + " order by " + comboOrderBY.SelectedItem;
                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }
                conn.Open();
                dataGridView1.Rows.Clear();
                reader = new SqlCommand("select * from supplier where description like '%" + name.Text + "%' " + queary + "", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), reader.GetString(4).ToUpper(), reader.GetString(5).ToUpper(), reader.GetString(6).ToUpper(), reader.GetString(7).ToUpper());
                }
                conn.Close();

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                // MessageBox.Show(a.Message+"/"+a.StackTrace);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8)
                {
                    this.Enabled = true;
                    supplierProfile a = new supplierProfile(this, userH);
                    a.Visible = true;
                    a.loadCustomer(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch (Exception a)
            {
                //  MessageBox.Show(a.Message);
            }
        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry, Empty Data to Print Report");
            }
            else
            {
                try
                {
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();
                    dt.Columns.Add("id", typeof(string));
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("company", typeof(string));
                    dt.Columns.Add("address", typeof(string));

                    dt.Columns.Add("mobile", typeof(string));

                    dt.Columns.Add("land", typeof(string));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("fax", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[5].Value, dataGridView1.Rows[i].Cells[6].Value, dataGridView1.Rows[i].Cells[7].Value);
                    }
                    ds.Tables.Add(dt);
                    //
                    ds.WriteXmlSchema("customerThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new supplierReportALL();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);
                    pp.PrintToPrinter(1, false, 0, 0);
                    // new test(pp).Visible = true;
                    //crystalReportViewer1.ReportSource = pp;
                    db.setCursoerDefault();
                    MessageBox.Show("Report Send to Print Succefully........");
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }
            }
        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry, Empty Data to Print Report");
            }
            else
            {
                try
                {
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();
                    dt.Columns.Add("id", typeof(string));
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("company", typeof(string));
                    dt.Columns.Add("address", typeof(string));

                    dt.Columns.Add("mobile", typeof(string));

                    dt.Columns.Add("land", typeof(string));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("fax", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value, dataGridView1.Rows[i].Cells[3].Value, dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[5].Value, dataGridView1.Rows[i].Cells[6].Value, dataGridView1.Rows[i].Cells[7].Value);
                    }
                    ds.Tables.Add(dt);
                    //
                    //         ds.WriteXmlSchema("stockThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new supplierReportALL();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);
                    //  pp.PrintToPrinter(1, false, 0, 0);
                    this.Enabled = true;
                    new supplierReportView(this, userH, pp).Visible = true;
                    //crystalReportViewer1.ReportSource = pp;
                    db.setCursoerDefault();
                    //   MessageBox.Show("Report Send to Print Succefully........");
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message + "/" + a.StackTrace);
                }
            }
        }
    }
}