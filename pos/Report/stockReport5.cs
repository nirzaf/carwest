using System;
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
    public partial class stockReport5 : Form
    {
        public stockReport5(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }
        //Variable
        Form formH;
        stockReportALL pp;
        DB db, db2, db3;
        string userH, queary, userName;
        SqlConnection conn, conn2, conn3;
        string[] idArray;
        SqlDataReader reader, reader2, reader3;
        DataTable dt;
        DataSet ds;
        ArrayList arrayList;
        double amountCost;
        DataGridViewButtonColumn btn;
        //
        //++++++ My Method Start+++
        void loadAutoComplete()
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
        void loadCostAll()
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
        void loadUser()
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
            conn3 = db3.createSqlConnection2();
            this.ActiveControl = name;
            radioSearchByDate.Checked = true;
            radioAdvancedSearch.Checked = true;
            searchALL.Checked = true;
            this.TopMost = true;
            loadAutoComplete();
            //loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                queary = "";
                if (radioSearchByDate.Checked)
                {
                    queary = "code='" + itemCode.Text + "'";
                }
                else if (radioAdvancedSearch.Checked)
                {
                    if (checkBrand.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "brand='" + brandName.Text + "'";
                        }
                        else
                        {
                            queary = " and brand='" + brandName.Text + "'";

                        }
                    }

                    if (checkCategory.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "categorey='" + categoryName.Text + "'";
                        }
                        else
                        {
                            queary = " and categorey='" + categoryName.Text + "'";

                        }
                    }
                    if (checkDescription.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            queary = "description='" + descriptionName.Text + "'";
                        }
                        else
                        {
                            queary = " and description='" + descriptionName.Text + "'";

                        }
                    }
                    if (checkQty.Checked)
                    {
                        if (queary.Equals(""))
                        {
                            if (radioMinValue.Checked)
                            {
                                queary = " qty<='" + minValue.Text + "'";

                            }
                            else
                            {
                                queary = " qty>='" + maxValue.Text + "'";

                            }
                        }
                        else
                        {
                            if (radioMinValue.Checked)
                            {
                                queary = " and qty<='" + minValue.Text + "'";

                            }
                            else
                            {
                                queary = " and qty>='" + maxValue.Text + "'";

                            }

                        }
                    }

                }

                if (!queary.Equals(""))
                {
                    queary = "where isitem='" + true + "'" + queary;
                }
                queary = queary + "where isitem='" + true + "'  order by " + comboOrderBY.SelectedItem;
                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }

                double stockValue = 0, stockValueU = 0;
                dataGridView1.Rows.Clear();


                {
                    conn.Open();
                    reader = new SqlCommand("select * from item  " + queary + "", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        //double stockValue = 0, stockValueU = 0;

                        stockValueU = 0;
                        if (reader.GetDouble(9) > 0)
                        {

                            stockValueU = reader.GetDouble(9) * reader.GetDouble(6);
                            stockValue = stockValue + (stockValueU);

                        }
                        conn2.Open();
                        reader2 = new SqlCommand("select qty from itemStatementStock where itemcode='" + reader.GetString(0).ToUpper() + "' and date='" + DateTime.Now + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader2[0], reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), db.setAmountFormat(reader[6] + ""), db.setAmountFormat((stockValueU) + ""));

                        }
                        else
                        {
                            dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), 0, reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), db.setAmountFormat(reader[6] + ""), db.setAmountFormat((stockValueU) + ""));


                        }
                        conn2.Close();
                    }
                    conn.Close();

                }


                totStockValue.Text = db.setAmountFormat(stockValue + "");
                if (dataGridView1.Rows.Count == 0)
                {
                    db.setCursoerDefault();
                    MessageBox.Show("Sorry, No Data Re-Present on Your Selected Key-Words.");
                }
                else
                {
                    loadCostAll();
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
                double stockValue = 0, stockValueU = 0;
                {
                    conn.Open();
                    dataGridView1.Rows.Clear();
                    reader = new SqlCommand("select * from item where detail like '%" + name.Text + "%' " + queary + "", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        //  dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader[9] + " " + reader.GetString(5).ToUpper(), reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), reader.GetString(4).ToUpper());
                        //dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader[9] + " " + reader.GetString(5).ToUpper(), reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), db.setAmountFormat(reader[6] + ""), db.setAmountFormat(reader[7] + ""));

                        stockValueU = 0;
                        if (reader.GetDouble(9) > 0)
                        {

                            stockValueU = reader.GetDouble(9) * reader.GetDouble(6);
                            stockValue = stockValue + (stockValueU);

                        }
                        conn2.Open();
                        reader2 = new SqlCommand("select qty from itemStatementStock where itemcode='" + reader.GetString(0).ToUpper() + "' and date='" + DateTime.Now + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), reader2[0], reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), db.setAmountFormat(reader[6] + ""), db.setAmountFormat((stockValueU) + ""));

                        }
                        else
                        {
                            dataGridView1.Rows.Add(reader.GetString(0).ToUpper(), 0, reader.GetString(1).ToUpper(), reader.GetString(2).ToUpper(), reader.GetString(3).ToUpper(), db.setAmountFormat(reader[6] + ""), db.setAmountFormat((stockValueU) + ""));


                        }
                        conn2.Close();
                    }
                    conn.Close();
                }

                // loadCostAll();
                totStockValue.Text = db.setAmountFormat(stockValue + "");
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
            db.setCursoerWait();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    conn.Open();
                    new SqlCommand("delete from itemStatementStock where itemcode='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "' and date='" + DateTime.Now + "' and type='" + "STOCK-UPDATE" + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert itemStatementStock values ('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + DateTime.Now + "','" + "STOCK-UPDATE" + "') ", conn).ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            db.setCursoerDefault();
            MessageBox.Show("ok");
        }

        double credit = 0, debit = 0, stock = 0, stockDB = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            db.setCursoerWait();

            conn.Open();
            reader = new SqlCommand("select * from itemStatementStock where  date='" + DateTime.Now + "' and qty!='"+0+"'", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                new SqlCommand("update item set qty='" + 0 + "' where code='" + reader[1] + "'", conn2).ExecuteNonQuery();
                conn2.Close();
                conn2.Open();
                new SqlCommand("delete from itemStatement where date='" + DateTime.Now + "' and type='" + "STOCK-BF" + "' and itemCode='"+reader[1]+"'", conn2).ExecuteNonQuery();
                conn2.Close();
                credit = 0;
                debit = 0;
                stock = 0;
                try
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select sum(qty) from itemStatement where credit='" + true + "' and itemcode='" + reader[1] + "' and date<'" + DateTime.Now + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        credit = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }
                try
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select sum(qty) from itemStatement where credit='" + false + "' and itemcode='" + reader[1] + "' and date<'" + DateTime.Now + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        debit = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }
                try
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select qty from itemStatementStock where  itemcode='" + reader[1] + "' and date='" + DateTime.Now + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        stock = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }

                stockDB = debit - credit;
                var ss = reader[0].ToString();
                if (reader[0].ToString().Equals("WP1"))
                {
                    var s = "";
                }
                if (stockDB > stock)
                {
                    conn2.Open();
                    new SqlCommand("insert into itemStatement values ('" + "" + "','" + reader[1] + "','" + true + "','" + (stockDB - stock) + "','" + DateTime.Now + "','" + "STOCK-BF" + "','" + "ADMIN" + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                else if (stockDB < stock)
                {
                    conn2.Open();
                    new SqlCommand("insert into itemStatement values ('" + "" + "','" + reader[1] + "','" + false + "','" + (stock - stockDB) + "','" + DateTime.Now + "','" + "STOCK-BF" + "','" + "ADMIN" + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
            }
            conn.Close();
            db.setCursoerDefault();
            MessageBox.Show("ok");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = 0;
            conn.Open();
            reader = new SqlCommand("select * from itemStatementStock", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select * from itemStatementStock where itemcode='" + reader[1] + "'", conn2).ExecuteReader();
                reader2.Read();
                if (reader2.Read())
                {
                    MessageBox.Show(reader[1] + "");
                }
                conn2.Close();
            }
            conn.Close();

        }


    }
}
