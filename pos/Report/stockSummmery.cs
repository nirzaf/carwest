using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace pos
{
    public partial class stockSummmery : Form
    {
        public stockSummmery(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }

        //Variable
        private Form formH;

        private cusOuts pp;
        private DB db, db2, db3;
        private string userH, queary, tableName, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "", term, customer, item;
        private SqlConnection conn, conn2, conn3;
        private string[] idArray;
        private SqlDataReader reader, reader2, reader3;
        private DataTable dt;
        private DataSet ds;
        private ArrayList arrayList;
        private double amountCost, amountPaid, balance, temp030, temp3060, temp6090, temp90up, a;
        private Boolean isCompany, creditDetailB, chequeDetailB, cardDetailB;
        private DataGridViewButtonColumn btn;

        //
        //++++++ My Method Start+++
        private void getCustomer(string id)
        {
            try
            {
                customer = "";
                conn2.Open();
                reader2 = new SqlCommand("select id,name,company from customer where id='" + id + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    customer = reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper();
                }
                else
                {
                    customer = id;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                conn2.Close();
            }
        }

        private void getItem(string id)
        {
            try
            {
                item = "";
                conn2.Open();
                reader2 = new SqlCommand("select detail from item where code='" + id + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    item = reader2[0].ToString().ToUpper();
                }
                else
                {
                    item = id;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                conn2.Close();
            }
        }

        private void getInvoicePayType(string id)
        {
            try
            {
                conn2.Open();

                reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    creditDetailB = true;
                }
                conn2.Close();
                conn2.Open();
                reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    chequeDetailB = true;
                }
                conn2.Close();
                conn2.Open();
                reader2 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    cardDetailB = true;
                }
                conn2.Close();

                term = "";
                if (!creditDetailB & !chequeDetailB & !cardDetailB)
                {
                    term = "CASH";
                }
                else
                {
                    if (creditDetailB)
                    {
                        term = "CREDIT";
                    }
                    if (chequeDetailB)
                    {
                        term = term + "/ CHEQUE";
                    }
                    if (cardDetailB)
                    {
                        term = term + "/ CARD";
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void setTempDates(Double aH)
        {
            a = aH;
            temp030 = 0;
            temp3060 = 0;
            temp6090 = 0;
            temp90up = 0;
            if (aH < 30)
            {
                temp030 = balance;
            }
            else if (aH < 60 & a > 30)
            {
                temp3060 = balance;
            }
            else if (aH < 60 & a > 90)
            {
                temp6090 = balance;
            }
            else
            {
                temp90up = balance;
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
                    isCompany = reader.GetBoolean(2);
                }
                reader.Close();
                conn.Close();
                if (isCompany)
                {
                    tableName = "invoiceRetail";
                }
                else
                {
                    tableName = "invoiceDump";
                }
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from company ", conn).ExecuteReader();
                if (reader.Read())
                {
                    comName = reader.GetString(0).ToUpper();
                    comAddres = reader.GetString(1).ToUpper();
                    if (!reader.GetString(2).Equals(""))
                    {
                        comcontact = "Tel : " + reader[2] + " / ";
                    }
                    if (!reader.GetString(3).Equals(""))
                    {
                        comcontact = comcontact + "Fax : " + reader[3];
                    }
                    if (!reader.GetString(4).Equals(""))
                    {
                        comContact2 = "E-Mail : " + reader[4].ToString().ToUpper() + " / ";
                    }
                    if (!reader.GetString(5).Equals(""))
                    {
                        comContact2 = comContact2 + "Web : " + reader[5].ToString().ToUpper();
                    }
                    comReg = reader[6] + "";
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
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            db3 = new DB();
            conn3 = db3.createSqlConnection();

            radioDateCustom.Checked = true;
            radioAllDate.Checked = true;

            this.TopMost = true;

            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;
            comboGroupBy.SelectedIndex = 0;

            conn.Open();
            reader = new SqlCommand("select * from company ", conn).ExecuteReader();
            if (reader.Read())
            {
                comName = reader.GetString(0).ToUpper();
                comAddres = reader.GetString(1).ToUpper();
                if (!reader.GetString(2).Equals(""))
                {
                    comcontact = "Tel : " + reader[2] + " / ";
                }
                if (!reader.GetString(3).Equals(""))
                {
                    comcontact = comcontact + "Fax : " + reader[3];
                }
                comReg = "";
                comContact2 = "";
            }
            reader.Close();
            conn.Close();
        }

        private void radioSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBrand_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkCategory_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkDescription_CheckedChanged(object sender, EventArgs e)
        {
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

        private double total;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amountCost = 0;
                db.setCursoerWait();

                queary = "";
                if (radioDateCustom.Checked)
                {
                    queary = " and b.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                }
                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by b.Date";
                }
                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.invoiceID";
                }

                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }

                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("invoiceID", typeof(string));
                dt.Columns.Add("term", typeof(string));
                dt.Columns.Add("invoiceAmount", typeof(string));
                dt.Columns.Add("profit", typeof(string));
                dt.Columns.Add("date", typeof(string));
                conn.Open();
                queary = "";
                if (radioDateCustom.Checked)
                {
                    queary = " and a.date between '" + from.Value.ToShortDateString() + "' and '" + to.Value.ToShortDateString() + "'";
                }
                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by a.Date";
                }
                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.itemCode";
                }

                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }
                if (!queary.Equals(""))
                {
                    queary = " " + queary;
                }

                total = 0;
                reader = new SqlCommand("select a.*,b.detail from itemStatement as a,item as b where a.itemCode=b.code and a.itemcode='" + itemCode.Text + "'   " + queary, conn).ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean(3))
                    {
                        total = total - reader.GetDouble(4);
                        dt.Rows.Add(reader[1], reader.GetString(9).ToUpper(), reader.GetDateTime(5).ToShortDateString(), reader.GetString(6), reader[4], "", total);
                    }
                    else
                    {
                        total = total + reader.GetDouble(4);
                        dt.Rows.Add(reader[1], reader.GetString(9).ToUpper(), reader.GetDateTime(5).ToShortDateString(), reader.GetString(6), "", reader[4], total);
                    }
                }
                conn.Close();

                ds.Tables.Add(dt);
                //
                ds.WriteXmlSchema("saleSummery.xml");
                //   MessageBox.Show("a");
                stockSummeryH pp = new stockSummeryH();
                pp.SetDataSource(ds);
                pp.SetParameterValue("USER", userName);
                if (radioAllDate.Checked)
                {
                    pp.SetParameterValue("period", "ALL");
                }
                else
                {
                    pp.SetParameterValue("period", from.Value.ToShortDateString() + " - " + to.Value.ToShortDateString());
                }
                pp.SetParameterValue("comName", comName);
                pp.SetParameterValue("comAddress", comAddres);

                pp.SetParameterValue("comContact", comcontact);

                pp.SetParameterValue("comCntact2", comContact2);

                pp.SetParameterValue("comReg", comReg);
                crystalReportViewer1.ReportSource = pp;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data on your Seletion Value");
                }
                else
                {
                    //   MessageBox.Show("Succefully Downloaded");
                }

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/" + a.StackTrace);
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
                listBox1.Visible = false;
                if (itemCode.Text.Equals(""))
                {
                    itemCode.Focus();
                }
                else
                {
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
                        itemCode.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void brandName_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void name_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void qUICKPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry, Empty Data to Print");
                }
                else
                {
                    pp.PrintToPrinter(1, false, 0, 0);
                    MessageBox.Show("Send to Print Succesfully");
                }
            }
            catch (Exception)
            {
            }
        }

        private void pRINTPREVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void radioDateCustom_CheckedChanged(object sender, EventArgs e)
        {
            from.Enabled = radioDateCustom.Checked;
            to.Enabled = radioDateCustom.Checked;
        }

        private void itemCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMin_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioMax_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCustomerID_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioName_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioCompany_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void customerID_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | itemCode.Text.Equals("")))
            {
                db.setList(listBox1, itemCode, itemCode.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select detail from item where detail like '%" + itemCode.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper());
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
            if (itemCode.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void Name_KeyUp_1(object sender, KeyEventArgs e)
        {
        }

        private void company_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private string name;
        private Boolean states;
        private Point p;

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                itemCode.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                name = "";
                for (int i = 0; i < listBox1.SelectedItem.ToString().Split(' ').Length; i++)
                {
                    if (i != 0)
                    {
                        name = name + " " + listBox1.SelectedItem.ToString().Split(' ')[i];
                    }
                }

                itemCode.Focus();
                itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            conn.Open();
            reader = new SqlCommand("select brand,categorey,description from item where code='" + itemCode.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                name = reader[0] + " " + reader[1] + " " + reader[2];
            }
            conn.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void radioCustomCustomer_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void customerID_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void customerID_KeyUp_1(object sender, KeyEventArgs e)
        {
        }

        private void listCustomer_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listCustomer_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void listCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
    }
}