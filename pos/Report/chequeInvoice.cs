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
    public partial class chequeInvoice : Form
    {
        public chequeInvoice(Form home, String user)
        {
            InitializeComponent();
            formH = home;
            userH = user;
        }
        //Variable
        Form formH;
        cusCheque pp;
        DB db, db2;
        string userH, queary, userName, comName = "", comAddres = "", comcontact = "", comContact2 = "", comReg = "";
        SqlConnection conn, conn2;
        string[] idArray;
        SqlDataReader reader, reader2;
        DataTable dt;
        DataSet ds;
        ArrayList arrayList;
        double amountCost;
        DataGridViewButtonColumn btn;
        Boolean isCompany;
        //
        //++++++ My Method Start+++

        void loadUser()
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

            }
            catch (Exception)
            {
                conn.Close();
            }

        }


        //
        private void stockReport_Load(object sender, EventArgs e)
        {
        
            dataGridView2.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();

            radioAdvancedSearch.Checked = true;
            searchALL.Checked = true;

            checkInvoiceDate.Checked = true;
            checkCustomer.Checked = true;
            checkBox1.Checked = true;
            checkInvoiceDate.Checked = false;
            checkCustomer.Checked = false;
            checkBox1.Checked = false;
            this.TopMost = true;

            loadUser();
            comboOrderBY.SelectedIndex = 0;
            comboOrderTO.SelectedIndex = 0;
           
           

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;

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

            conn.Open();
            reader = new SqlCommand("select chequenumber from chequeInvoiceRetail ", conn).ExecuteReader();
            arrayList = new ArrayList();
            while (reader.Read())
            {
                //  MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");

            }
            reader.Close();
            idArray = arrayList.ToArray(typeof(string)) as string[];
            db.setAutoComplete2(toolStripTextBox1, idArray);
            conn.Close();
        }

        private void radioSearchByDate_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView2.Enabled = radioAdvancedSearch.Checked;
            customerID.Enabled = radioAdvancedSearch.Checked;
            customerID.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                amountCost = 0;
                db.setCursoerWait();
                dt = new DataTable();
                ds = new DataSet();

                dt.Columns.Add("cusID", typeof(string));
                dt.Columns.Add("customer", typeof(string));
                dt.Columns.Add("invoiceID", typeof(string));
                dt.Columns.Add("invoiceAmount", typeof(float));
                dt.Columns.Add("cheque", typeof(float));
                dt.Columns.Add("chequenumber", typeof(string));
                dt.Columns.Add("checkCodeNo", typeof(string));
                dt.Columns.Add("chequeDate", typeof(string));


                queary = "";
                if (checkInvoiceDate.Checked)
                {
                    queary = " and b.date between '" + dateInvoiceFrom.Value.ToShortDateString() + "' and '" + dateInvoiceTo.Value.ToShortDateString() + "'";
                }
                if (checkBox1.Checked)
                {
                    queary = " and a.chequeDate between '" + dateInvoiceFrom.Value.ToShortDateString() + "' and '" + dateChequeTo.Value.ToShortDateString() + "'";
                }
                if (comboOrderBY.SelectedIndex == 0)
                {
                    queary = queary + " order by b.Date";
                }

                else if (comboOrderBY.SelectedIndex == 1)
                {
                    queary = queary + " order by a.invoiceID";
                }
                else if (comboOrderBY.SelectedIndex == 2)
                {
                    queary = queary + " order by a.chequeDate";
                }
                if (comboOrderTO.SelectedIndex == 0)
                {
                    queary = queary + " asc ";
                }
                else
                {
                    queary = queary + " desc ";
                }
                if (!radioAdvancedSearch.Checked)
                {
                    conn.Open();
                    if (isCompany)
                    {
                        reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceRetail as b where b.id=a.invoiceId " + queary, conn).ExecuteReader();

                    }
                    else
                    {
                        reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceDump as b where b.id=a.invoiceId " + queary, conn).ExecuteReader();

                    }
                    while (reader.Read())
                    {

                        try
                        {
                            amountCost = amountCost + reader.GetDouble(2);
                            conn2.Open();
                            reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[6] + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                // MessageBox.Show("sa");
                                dt.Rows.Add(reader[6], reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                            }
                            else
                            {
                                dt.Rows.Add(reader[6], reader[6], "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                            }
                            conn2.Close();
                            //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                        }
                        catch (Exception)
                        {
                            conn2.Close();
                        }
                    }
                    conn.Close();
                }
                else
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        conn.Open();
                        if (isCompany)
                        {
                            reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceRetail as b where b.id=a.invoiceId and b.customerid='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();

                        }
                        else
                        {
                            reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceDump as b where b.id=a.invoiceId and b.customerid='" + dataGridView2.Rows[i].Cells[0].Value + "'" + queary, conn).ExecuteReader();

                        }
                        while (reader.Read())
                        {

                            try
                            {
                                amountCost = amountCost + reader.GetDouble(2);
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[6] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    // MessageBox.Show("sa");
                                    dt.Rows.Add(reader[6], reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                                }
                                else
                                {
                                    dt.Rows.Add(reader[6], reader[6], "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                                }
                                conn2.Close();
                                //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    }
                }
                ds.Tables.Add(dt);
                //
                // ds.WriteXmlSchema("outsThisara3.xml");
                //   MessageBox.Show("a");
                pp = new cusCheque();
                pp.SetDataSource(ds);
                pp.SetParameterValue("USER", userName);
                if (!checkInvoiceDate.Checked)
                {

                    pp.SetParameterValue("period", "ALL");
                }
                else
                {

                    pp.SetParameterValue("period", dateInvoiceFrom.Value.ToShortDateString() + " - " + dateInvoiceTo.Value.ToShortDateString());

                }
                if (!checkBox1.Checked)
                {

                    pp.SetParameterValue("period2", "ALL");
                }
                else
                {

                    pp.SetParameterValue("period2", dateChequeFrom.Value.ToShortDateString() + " - " + dateChequeTo.Value.ToShortDateString());

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
                listBox1.Visible = false;
                if (customerID.Text.Equals(""))
                {
                    customerID.Focus();
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        name = reader[0] + " " + reader[1];
                    }
                    conn.Close();
                    if (dataGridView2.Rows.Count == 0)
                    {
                        dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                    }
                    else
                    {
                        states = false;
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            if (dataGridView2.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                            {
                                states = true;
                            }
                        }
                        if (!states)
                        {
                            dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                        }
                    }
                    customerID.Text = "";
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
                        customerID.Focus();
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
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customerID.Text.Equals("")))
            {

                db.setList(listBox1, customerID, customerID.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select description from customer where description like '%" + customerID.Text + "%' ", conn).ExecuteReader();
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
            if (customerID.Text.Equals(""))
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
        string name;
        Boolean states;
        Point p;
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {

                customerID.Focus();

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

                customerID.Focus();
                customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                if (dataGridView2.Rows.Count == 0)
                {
                    dataGridView2.Rows.Add(customerID.Text, name);
                }
                else
                {
                    states = false;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[0].Value.ToString().Equals(customerID.Text))
                        {
                            states = true;
                        }
                    }
                    if (!states)
                    {
                        dataGridView2.Rows.Add(customerID.Text, name);
                    }
                }
                customerID.Text = "";

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            conn.Open();
            reader = new SqlCommand("select name,company from customer where id='" + customerID.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                name = reader[0] + " " + reader[1];
            }
            conn.Close();
            if (dataGridView2.Rows.Count == 0)
            {
                dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
            }
            else
            {
                states = false;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().ToUpper().Equals(customerID.Text.ToUpper()))
                    {
                        states = true;
                    }
                }
                if (!states)
                {
                    dataGridView2.Rows.Add(customerID.Text.ToUpper(), name);
                }
            }
            customerID.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }

        private void checkInvoiceDate_CheckedChanged(object sender, EventArgs e)
        {
            panelInvoiceDate.Enabled = checkInvoiceDate.Checked;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            panelCheque.Enabled = checkBox1.Checked;
        }

        private void checkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            panelCustomer.Enabled = checkCustomer.Checked;
            searchALL.Checked = false;
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==12 | e.KeyValue==13)
            {
                qUICKSEARCHToolStripMenuItem.DropDown.Close();
                try
                {
                    amountCost = 0;
                    db.setCursoerWait();
                    dt = new DataTable();
                    ds = new DataSet();

                    dt.Columns.Add("cusID", typeof(string));
                    dt.Columns.Add("customer", typeof(string));
                    dt.Columns.Add("invoiceID", typeof(string));
                    dt.Columns.Add("invoiceAmount", typeof(float));
                    dt.Columns.Add("cheque", typeof(float));
                    dt.Columns.Add("chequenumber", typeof(string));
                    dt.Columns.Add("checkCodeNo", typeof(string));
                    dt.Columns.Add("chequeDate", typeof(string));


                    queary = "";
                   
                    if (comboOrderBY.SelectedIndex == 0)
                    {
                        queary = queary + " order by b.Date";
                    }

                    else if (comboOrderBY.SelectedIndex == 1)
                    {
                        queary = queary + " order by a.invoiceID";
                    }
                    else if (comboOrderBY.SelectedIndex == 2)
                    {
                        queary = queary + " order by a.chequeDate";
                    }
                    if (comboOrderTO.SelectedIndex == 0)
                    {
                        queary = queary + " asc ";
                    }
                    else
                    {
                        queary = queary + " desc ";
                    }

                        conn.Open();
                        if (isCompany)
                        {
                            reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceRetail as b where b.id=a.invoiceId and a.chequenumber='"+toolStripTextBox1.Text+"'" + queary, conn).ExecuteReader();

                        }
                        else
                        {
                            reader = new SqlCommand("select a.invoiceID,b.subTotal,a.cheque,a.chequenumber,a.checkCodeNo,a.chequeDate,b.customerid from chequeInvoiceRetail as a,invoiceDump as b where b.id=a.invoiceId and a.chequenumber='" + toolStripTextBox1.Text + "'" + queary, conn).ExecuteReader();

                        }
                        while (reader.Read())
                        {

                            try
                            {
                                amountCost = amountCost + reader.GetDouble(2);
                                conn2.Open();
                                reader2 = new SqlCommand("select id,name,company from customer where id='" + reader[6] + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    // MessageBox.Show("sa");
                                    dt.Rows.Add(reader[6], reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                                }
                                else
                                {
                                    dt.Rows.Add(reader[6], reader[6], "R-" + reader[0], reader.GetDouble(1), reader.GetDouble(2), reader[3] + "", reader[4], reader.GetDateTime(5).ToShortDateString());

                                }
                                conn2.Close();
                                //    dt.Rows.Add("1", "2", "3", "4", "5", "6", "7", "8");
                            }
                            catch (Exception)
                            {
                                conn2.Close();
                            }
                        }
                        conn.Close();
                    
                  
                    ds.Tables.Add(dt);
                    //
                    // ds.WriteXmlSchema("outsThisara3.xml");
                    //   MessageBox.Show("a");
                    pp = new cusCheque();
                    pp.SetDataSource(ds);
                    pp.SetParameterValue("USER", userName);
                    if (!checkInvoiceDate.Checked)
                    {

                        pp.SetParameterValue("period", "ALL");
                    }
                    else
                    {

                        pp.SetParameterValue("period", dateInvoiceFrom.Value.ToShortDateString() + " - " + dateInvoiceTo.Value.ToShortDateString());

                    }
                    if (!checkBox1.Checked)
                    {

                        pp.SetParameterValue("period2", "ALL");
                    }
                    else
                    {

                        pp.SetParameterValue("period2", dateChequeFrom.Value.ToShortDateString() + " - " + dateChequeTo.Value.ToShortDateString());

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
                    MessageBox.Show(a.Message + "/" + queary);
                }
            }
        }


    }
}
