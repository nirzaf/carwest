using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class chequeList : Form
    {
        public chequeList(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private stockReportALL pp;
        private DB db, db2, db3;
        private string userH, queary, userName;
        private SqlConnection conn, conn2, conn3;
        private string[] idArray;
        private SqlDataReader reader, reader2, reader3;
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

        private void loadCostAll()
        {
            amountCost = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select purchasingPrice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[0].Value + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        amountCost = amountCost + (reader.GetDouble(0) * reader.GetDouble(1));
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            stockValue.Text = db.setAmountFormat(amountCost + "");
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
                    panelStockValue.Visible = reader.GetBoolean(2);
                    dataGridView1.Columns[6].Visible = reader.GetBoolean(3);
                    dataGridView1.Columns[7].Visible = reader.GetBoolean(22);
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
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn3 = db3.createSqlConnection();
            // this.ActiveControl = name;
            radioSearchByDate.Checked = true;
            radioAdvancedSearch.Checked = true;
            searchALL.Checked = true;
            this.TopMost = true;
            loadAutoComplete();
            //loadUser();

            //btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.Width = 60;
            //btn.Text = "EDIT";
            //btn.UseColumnTextForButtonValue = true;
            //btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.Width = 60;
            //btn.Text = "STOCK";
            //btn.UseColumnTextForButtonValue = true;
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
            checkQty.Checked = true;
            checkQty.Checked = false;
            checkBrand.Enabled = radioAdvancedSearch.Checked;
            checkCategory.Enabled = radioAdvancedSearch.Checked;
            checkDescription.Enabled = radioAdvancedSearch.Checked;
            checkQty.Enabled = radioAdvancedSearch.Checked;
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
            minValue.Enabled = checkQty.Checked;
            maxValue.Enabled = checkQty.Checked;
            radioMinValue.Enabled = checkQty.Checked;
            radioMaxValue.Enabled = checkQty.Checked;

            if (checkQty.Checked)
            {
                radioMaxValue.Checked = true;
                radioMinValue.Checked = true;
            }
        }

        private void radioMinValue_CheckedChanged(object sender, EventArgs e)
        {
            minValue.Enabled = radioMinValue.Checked;
            minValue.Focus();
        }

        private void radioMaxValue_CheckedChanged(object sender, EventArgs e)
        {
            maxValue.Enabled = radioMaxValue.Checked;
            maxValue.Focus();
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

        private double total = 0.0;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                total = 0;
                db.setCursoerWait();
                dataGridView1.Rows.Clear();
                if (radioButton1.Checked)
                {
                    conn.Open();
                    new SqlCommand("delete from chequeInvoiceRetailLoad ", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from chequeInvoiceRetail where chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("insert into chequeInvoiceRetailLoad values ('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from chequeInvoiceRetail2 where chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("insert into chequeInvoiceRetailLoad values ('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select a.*,b.company from chequeInvoiceRetailLoad as a,customer as b  where a.chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "' and a.cutomerId=b.id order by chequeDate", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        total = total + reader.GetDouble(4);
                        dataGridView1.Rows.Add(reader.GetDateTime(6).ToShortDateString(), reader[10], reader[5], reader[8], db.setAmountFormat(reader[4] + ""));
                    }
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    new SqlCommand("delete from chequeInvoiceRetailLoad ", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from chequeGRN where chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("insert into chequeInvoiceRetailLoad values ('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from chequeGRN2 where chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("insert into chequeInvoiceRetailLoad values ('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select a.*,b.company from chequeInvoiceRetailLoad as a,supplier as b  where a.chequeDate between '" + dateTimePicker1.Value + "' and '" + dateTimePicker2.Value + "' and a.cutomerId=b.id order by chequeDate", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        total = total + reader.GetDouble(4);
                        dataGridView1.Rows.Add(reader.GetDateTime(6).ToShortDateString(), reader[10], reader[5], reader[8], db.setAmountFormat(reader[4] + ""));
                    }
                    conn.Close();
                }

                totalValue.Text = db.setAmountFormat(total + "");
                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + queary);
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == 6)
            //    {
            //        this.Enabled = true;
            //        itemProfile a = new itemProfile(this, userH);
            //        a.Visible = true;
            //        a.loadItem(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //    }
            //    else if (e.ColumnIndex == 7)
            //    {
            //        this.Enabled = true;
            //        stockProfile a = new stockProfile(this, userH);
            //        a.Visible = true;
            //        a.loadItem(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //    }
            //}
            //catch (Exception a)
            //{
            //    //  MessageBox.Show(a.Message);
            //}
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
                    dt.Columns.Add("code", typeof(string));
                    dt.Columns.Add("qty", typeof(string));
                    dt.Columns.Add("brand", typeof(string));
                    dt.Columns.Add("cate", typeof(string));

                    dt.Columns.Add("desc", typeof(string));

                    dt.Columns.Add("remark", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select purchasingPrice,QTY from item where code='" + dataGridView1.Rows[i].Cells[0].Value + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value + " " + dataGridView1.Rows[i].Cells[3].Value + " " + dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[3].Value, db.setAmountFormat(reader[0] + ""), db.setAmountFormat(reader.GetDouble(0) * reader.GetDouble(1) + ""));
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }
                    }
                    ds.Tables.Add(dt);
                    //
                    //         ds.WriteXmlSchema("stockThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new stockReportALL();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", "");
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
                    dt.Columns.Add("code", typeof(string));
                    dt.Columns.Add("qty", typeof(string));
                    dt.Columns.Add("brand", typeof(string));
                    dt.Columns.Add("cate", typeof(string));

                    dt.Columns.Add("desc", typeof(string));

                    dt.Columns.Add("remark", typeof(string));
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            conn.Open();
                            reader = new SqlCommand("select purchasingPrice,QTY from item where code='" + dataGridView1.Rows[i].Cells[0].Value + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                dt.Rows.Add(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[2].Value + " " + dataGridView1.Rows[i].Cells[3].Value + " " + dataGridView1.Rows[i].Cells[4].Value, dataGridView1.Rows[i].Cells[3].Value, db.setAmountFormat(reader[0] + ""), db.setAmountFormat(reader.GetDouble(0) * reader.GetDouble(1) + ""));
                            }
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            conn.Close();
                        }
                    }
                    ds.Tables.Add(dt);
                    //
                    //         ds.WriteXmlSchema("stockThisara.xml");
                    //         MessageBox.Show("a");
                    pp = new stockReportALL();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", "");
                    //  pp.PrintToPrinter(1, false, 0, 0);
                    this.Enabled = true;
                    new stockReportView(this, userH, pp).Visible = true;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}