using System;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace pos
{
    public partial class returnInvoice : Form
    {
        public returnInvoice(Form form, String user, string id)
        {
            InitializeComponent();
            home = form;
            this.user = user;
            invoiceino = id;
        }
        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        ArrayList arrayList, stockList, detaiArrayList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, changeInvoiceDifDate;
        string user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount, amountTrue, amountFalse, cashPaidDB;
        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId, invoiceino;
        int count;
        string type = "";
        Boolean loadItemCheck = false, dateNow, discPrestage;
        public double paidAmount = 0;

        Boolean isNBT, isTax;
        public Double taxpre, nbtpre, amount2;
        // my Variable End
        //my Method Start++++++
        public Int32 checkDumpInvoice(double amount, string date, string id)
        {
            no = 0;
            countDB = 0;
            maxAmount = 0;

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from dumpInvoiceSetting", conn).ExecuteReader();
                if (reader.Read())
                {
                    countDB = reader.GetInt32(0);
                    maxAmount = reader.GetDouble(1);
                }
                reader.Close();
                conn.Close();
                if (countDB != 0 & maxAmount != 0)
                {
                    if (maxAmount >= amount)
                    {
                        conn.Open();
                        reader = new SqlCommand("select count from dumpinvoiceCount where date='" + date + "' ", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            no = reader.GetInt32(0);
                            reader.Close();
                        }
                        conn.Close();
                        reader.Close();
                        no++;
                        if (no < countDB)
                        {
                            //MessageBox.Show("1");
                            conn.Open();
                            reader = new SqlCommand("select * from dumpInvoiceCount where id='" + id + "'", conn).ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                conn.Close();
                                conn.Open();
                                //  MessageBox.Show("2");
                                reader = new SqlCommand("select * from dumpInvoiceCount where date='" + date + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    reader.Close();
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update dumpInvoiceCount set count=count+'" + 1 + "',id='" + id + "' where date='" + date + "' ", conn).ExecuteNonQuery();
                                    conn.Close();
                                    no = 0;
                                }
                                else
                                {

                                    reader.Close();
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("insert into dumpInvoiceCount values('" + date + "','" + 1 + "','" + id + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }

                            }
                            else
                            {

                                no = 0;

                            }

                            reader.Close();
                            conn.Close();
                            if (no == 1)
                            {
                                try
                                {
                                    conn.Open();
                                    reader = new SqlCommand("select max(dumpNO) from invoiceDump", conn).ExecuteReader();
                                    if (reader.Read())
                                    {
                                        no = reader.GetInt32(0);
                                    }
                                    else
                                    {
                                        no = 0;
                                    }
                                    reader.Close();
                                    conn.Close();
                                }
                                catch (Exception)
                                {

                                    reader.Close();
                                    conn.Close();
                                    no = 0;
                                }
                                no++;
                            }
                            else
                            {
                                no = 0;
                            }
                        }

                        else
                        {
                            no = 0;
                            conn.Open();
                            new SqlCommand("delete from dumpinvoiceCount", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                }



            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.Message + "/ " + a.StackTrace + " /sas " + date + " " + amount);
            }
            conn.Close();
            return no;

        }

        public void loadInvoice(string id)
        {

            try
            {
                invoieNoTemp = id.ToString().Split('-')[1].ToString();
                //    MessageBox.Show("1");
                db.setCursoerWait();
                if (id.Split('-')[0].ToString().ToUpper().Equals("R"))
                {
                    conn.Open();
                    reader = new SqlCommand("select * from invoiceRetail where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {


                        customer.Text = reader[1] + "";
                    //    total.Text = reader[3] + "";
                        subTotal.Text = reader[9] + "";
                      //  disc.Text = reader[8] + "";

                        poNumber.Text = reader[14] + "";
                        this.Text = id + "  (" + reader.GetDateTime(4).ToShortDateString() + " " + reader.GetTimeSpan(7) + ")";
                        var ter = reader[6] + "";

                       // cashPaid.Text = reader[12] + "";
                      //  balance.Text = reader[13] + "";
                        cashPaidDB = reader.GetDouble(12);

                        reader.Close();
                        conn.Close();
                        loadCustomer(customer.Text);
                       

                        conn2.Open();
                        reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            creditDetail = new string[2];
                            creditDetail[0] = reader2[4] + "";
                            creditDetail[1] = reader2[5] + "";
                            creditDetailB = true;
                            paidAmount = paidAmount + reader2.GetDouble(4);
                        }
                        reader2.Close();
                        conn2.Close();



                        conn2.Open();
                        reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
                        detaiArrayList = new ArrayList();

                        while (reader2.Read())
                        {
                            chequeDetailB = true;
                            detaiArrayList.Add(reader2[4].ToString());
                            detaiArrayList.Add(reader2[5].ToString());
                            detaiArrayList.Add(reader2[8].ToString());
                            detaiArrayList.Add(reader2.GetDateTime(6).ToShortDateString().Split(' ')[0]);
                            detaiArrayList.Add(reader2[9].ToString());
                            paidAmount = paidAmount + reader2.GetDouble(4);
                        }
                        chequeDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                        reader2.Close();
                        conn2.Close();

                        conn2.Open();
                        reader2 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
                        detaiArrayList = new ArrayList();

                        while (reader2.Read())
                        {
                            cardDetailB = true;
                            detaiArrayList.Add(reader2[5].ToString());
                            detaiArrayList.Add(reader2[6].ToString());
                            detaiArrayList.Add(reader2[7].ToString());
                            detaiArrayList.Add(reader2[8].ToString());
                            paidAmount = paidAmount + reader2.GetDouble(5);
                        }
                        cardDetail = detaiArrayList.ToArray(typeof(string)) as string[];
                        reader2.Close();
                        conn2.Close();

                        amountFalse = 0;
                        conn.Open();
                        reader = new SqlCommand("select * from invoiceRetailDetail where invoiceId='" + invoieNoTemp + "' and pc='" + false + "' ", conn).ExecuteReader();
                        Int32 count = 0;
                        while (reader.Read())
                        {
                            count++;
                            dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[10], reader[3], reader[7], reader[2], reader[4], "false", reader[11], reader[12], reader[13]);
                            amountFalse = amountFalse + reader.GetDouble(4);
                        }
                        conn.Close();
                        amountTrue = 0;
                        conn.Open();
                        reader = new SqlCommand("select * from invoiceRetailDetail where invoiceId='" + invoieNoTemp + "' and pc='" + true + "' ", conn).ExecuteReader();

                        while (reader.Read())
                        {
                            count++;
                            dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[10], reader[3], reader[7], reader[2], reader[4], "true", reader[11], reader[12], reader[13]);
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                            amountTrue = amountTrue + reader.GetDouble(4);
                        }
                        conn.Close();
                        setTermBack(true);

                        subTotal.Text = amountTrue + amountFalse + "";
                        returnAmount.Text = amountTrue + "";

                        conn.Open();
                        reader = new SqlCommand("select * from invoiceRetail where date='" + DateTime.Now.ToShortDateString() + "' and id='" + invoieNoTemp + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            dateNow = true;
                        }
                        else
                        {

                            dateNow = false;
                        }
                        reader.Close();
                        conn.Close();

                        conn.Open();
                        reader = new SqlCommand("select refCode from sale where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            // MessageBox.Show(invoieNoTemp);
                            for (int i = 0; i < saleRef.Items.Count; i++)
                            {
                                //    MessageBox.Show(saleRef.Items[i].ToString().Split(' ')[0] + "/" + reader.GetString(0).ToUpper());
                                if (saleRef.Items[i].ToString().Split(' ')[0].ToString().ToUpper().Equals(reader.GetString(0).ToUpper()))
                                {
                                    saleRef.SelectedIndex = i;

                                    try
                                    {
                                        comboSaleAccount.SelectedIndex = 0;
                                        if (saleRef.SelectedIndex != -1)
                                        {
                                            //  MessageBox.Show("1");
                                            for (int v = 0; v < comboSaleAccount.Items.Count; v++)
                                            {
                                                //   MessageBox.Show("2");
                                                if (comboSaleAccount.Items[v].ToString().Split('.')[0].ToString().Equals("SA"))
                                                {
                                                    //// MessageBox.Show("3" + comboSaleAccount.Items[i].ToString().Split('.')[1].ToString().ToUpper() + "3" + saleRef.Text.ToString().Split(' ')[0].ToString().ToUpper());
                                                    if (saleRef.SelectedItem.ToString().Split(' ')[0].ToString().ToUpper().Equals(comboSaleAccount.Items[v].ToString().Split('.')[1].ToString().ToUpper()))
                                                    {
                                                        //  MessageBox.Show("4");
                                                        comboSaleAccount.SelectedIndex = v;
                                                    }

                                                }


                                            }
                                        }
                                    }
                                    catch (Exception a)
                                    {
                                        // MessageBox.Show(a.Message + "/" + a.StackTrace);
                                        comboSaleAccount.SelectedIndex = 0;
                                    }
                                }
                            }
                        }

                        conn.Close();
                    }
                    else
                    {

                        MessageBox.Show("Invoice not Loading Correctlly");

                        this.Dispose();
                        home.Enabled = true;
                        home.TopMost = true;


                    }

                    conn.Close();
                }



                db.setCursoerDefault();

            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied Invoice ID " + a.Message + " //" + a.StackTrace);
                conn.Close();

            }
        }

        public Boolean loadCustomer(string id)
        {

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    customer.Text = "";
                    customer.AppendText(id + Environment.NewLine);
                    customer.AppendText(reader[1] + Environment.NewLine);
                    customer.AppendText(reader[2] + Environment.NewLine);
                    cutomerID = id;
                }
                else
                {
                    customer.Text = id;
                    states = false;
                    cutomerID = "";
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                reader.Close();
            }
            return states;
        }
        public void setTermBack(Boolean check)
        {
            term.Text = "";
            if (!creditDetailB & !chequeDetailB & !cardDetailB)
            {
                term.Text = "CASH";
            }
            else
            {

                if (creditDetailB)
                {
                    term.Text = "CREDIT";
                }
                if (chequeDetailB)
                {
                    term.Text = term.Text + "/ CHEQUE";
                }
                if (cardDetailB)
                {
                    term.Text = term.Text + "/ CARD";
                }
            }
        }
        void loadRetail()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from invoiceRetail", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceino = "R-" + invoiceMaxNo + "";
                reader.Close();
                conn.Close();
                conn.Open();
                new SqlCommand("delete from pcInvoice where invoiceid='" + invoiceino + "'", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                new SqlCommand("delete from pcItemInvoiceDetail where invoiceid='" + invoiceino + "'", conn).ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                // throw;
                invoiceino = "R-1";
                reader.Close();
                conn.Close();
            }
        }

        void clear()
        {
            subTotal.Text = "0.0";
           /// balance.Text = "0.0";
            returnAmount.Text = "0";
           
            customer.Text = "[CASH CUSTOMER]";
            term.Text = "CASH";
            cutomerID = "";
            dataGridView1.Rows.Clear();

        }

        public void updateTableItem(string qty, Int32 index)
        {
            if (dataGridView1.Rows[index].Cells[6].Value.ToString().Equals(qty))
            {

                dataGridView1.Rows[index].Cells[8].Value = "true";
                dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Silver;

            }
            else
            {

                dataGridView1.Rows.Add(dataGridView1.Rows.Count, dataGridView1.Rows[index].Cells[1].Value, "", dataGridView1.Rows[index].Cells[3].Value, dataGridView1.Rows[index].Cells[4].Value, dataGridView1.Rows[index].Cells[5].Value, qty, (Double.Parse(dataGridView1.Rows[index].Cells[4].Value.ToString()) - Double.Parse(dataGridView1.Rows[index].Cells[5].Value.ToString())) * Double.Parse(qty), "true");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;

                dataGridView1.Rows[index].Cells[6].Value = Double.Parse(dataGridView1.Rows[index].Cells[6].Value.ToString()) - Double.Parse(qty + "") + "";
                dataGridView1.Rows[index].Cells[7].Value = (Double.Parse(dataGridView1.Rows[index].Cells[4].Value.ToString()) - Double.Parse(dataGridView1.Rows[index].Cells[5].Value.ToString())) * Double.Parse(dataGridView1.Rows[index].Cells[6].Value.ToString());
            }


            amountTrue = 0.0;
            amountFalse = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[8].Value.ToString().Equals("false"))
                {
                    amountFalse = amountFalse + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                }
                else
                {

                    amountTrue = amountTrue + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                }
                dataGridView1.Rows[i].Cells[0].Value = ++i;
                i--;
            }
            //   subTotal.Text = amountFalse + amountTrue + "";
            returnAmount.Text = amountTrue + "";


        }
        void loadCompany()
        {
            try
            {
                conn.Open();
                isNBT = false;
                isTax = false;
                taxpre = 0;
                nbtpre = 0;
                reader = new SqlCommand("select name,istax,taxpre,isNBT,nbtPre,defa,id from company ", conn).ExecuteReader();
                while (reader.Read())
                {
                    //   MessageBox.Show("1");
                    comboCompany.Items.Add(reader[6] + "-" + reader.GetString(0).ToUpper());
                    if (reader.GetBoolean(5))
                    {
                        //   MessageBox.Show("2");
                        isNBT = reader.GetBoolean(3);
                        isTax = reader.GetBoolean(1);
                        taxpre = reader.GetDouble(2);
                        nbtpre = reader.GetDouble(4);
                        comboCompany.SelectedIndex = comboCompany.Items.Count - 1;
                    }
                }
                conn.Close();
                if (comboCompany.Items.Count != 0)
                {
                    comboCompany.SelectedIndex = 0;
                }

              
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }
        void loadAccountList()
        {

            try
            {
                comboChequePayment.Items.Clear();
                comboChequePayment.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select a.isDefa,b.name,b.bankName,a.accountID from accountChequePayment as a,bankAccounts as b where a.accountid=b.id", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboChequePayment.Items.Add(reader.GetString(1).ToUpper() + "-" + reader.GetString(2).ToUpper() + "(" + reader[3] + ")");
                    if (reader.GetBoolean(0))
                    {
                        comboChequePayment.SelectedIndex = comboChequePayment.Items.Count - 1;
                    }
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {

                comboCardPayment.Items.Clear();
                comboCardPayment.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select a.isDefa,b.name,b.bankName,a.accountID from accountCardPayment as a,bankAccounts as b where a.accountid=b.id", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboCardPayment.Items.Add(reader.GetString(1).ToUpper() + "-" + reader.GetString(2).ToUpper() + "(" + reader[3] + ")");
                    if (reader.GetBoolean(0))
                    {
                        comboCardPayment.SelectedIndex = comboChequePayment.Items.Count - 1;
                    }
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {

                comboSaleAccount.Items.Clear();
                comboSaleAccount.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select id,name from salesRef", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboSaleAccount.Items.Add("SA." + reader[0] + "." + reader[1].ToString().ToUpper());

                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {


                conn.Open();
                reader = new SqlCommand("select id,name from incomeAccounts", conn).ExecuteReader();
                while (reader.Read())
                {
                    comboSaleAccount.Items.Add("IN." + reader[0] + "." + reader[1].ToString().ToUpper());

                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }

            try
            {

                saleRef.Items.Clear();
                saleRef.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select id,name from salesRef", conn).ExecuteReader();
                while (reader.Read())
                {
                    saleRef.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());

                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //   my Method End+++++++++
        private void invoiceNew_Load(object sender, EventArgs e)
        {

            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            this.ControlBox = false;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            quickPanel.Width = width;
            //   dataGridView1.Width = width - 370;
            dataGridView1.Height = height - quickPanel.Height - 100;

            dataGridView1.Columns[3].Width = dataGridView1.Width - 680;
            Point p = new Point();
            p = panel1.Location;
            p.X = quickPanel.Width - panel1.Width - 35;
            panel1.Location = p;
            //panel1.Width = dataGridView1.Width;



            p.X = width - panel4.Width - 30;
            p.Y = height - panel4.Height - 60;
            panel4.Location = p;

            p = panelTax.Location;
            p.X = panel1.Location.X;
            panelTax.Location = p;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";
            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "RETURN";

            btn.UseColumnTextForButtonValue = true;

            db = new DB();
            conn = db.createSqlConnection2();
            db2 = new DB();
            conn2 = db2.createSqlConnection2();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;


            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {
                    saveInvoiceWithoutPay = reader.GetBoolean(0);
                    changeInvoiceDifDate = reader.GetBoolean(1);
                    discPrestage = reader.GetBoolean(4);
                }
                else
                {
                    saveInvoiceWithoutPay = false;
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            loadCompany();
            loadAccountList();
            saleRef.Cursor = null;

            loadInvoice(invoiceino);
        }

        private void invoiceNew_FormClosing(object sender, FormClosingEventArgs e)
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

        private void quickPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  new cusomerQuick2(this).Visible = true;

        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            customer.SelectAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 12)
                {
                    //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                    // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("false"))
                    {
                        MessageBox.Show("Sorry, Cant Edit Invoice Row Data On Retuen Note Function");
                    }
                    else
                    {
                        var code = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        var warrenty = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        var des = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        var uPrice = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                        var disc = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                        var qty = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                        var tAmount = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                        check = false;
                        // MessageBox.Show(code + "/" + dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (!check)
                            {
                                if (code.Equals("#"))
                                {

                                }
                                else
                                {

                                    if (code.ToUpper().Equals(dataGridView1.Rows[i].Cells[1].Value.ToString().ToUpper()) & dataGridView1.Rows[i].Cells[8].Value.ToString().Equals("false"))
                                    {
                                        check = true;
                                        dataGridView1.Rows[i].Cells[6].Value = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + Double.Parse(qty);
                                        dataGridView1.Rows[i].Cells[7].Value = (Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())) * Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                                    }
                                }


                            }
                        }
                        //MessageBox.Show(check+"");
                        if (!check)
                        {
                            dataGridView1.Rows.Add(dataGridView1.Rows.Count, code, warrenty, des, uPrice, disc, qty, tAmount, "false");

                        }
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        rowCount--;
                        amountTrue = 0.0;
                        amountFalse = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[8].Value.ToString().Equals("false"))
                            {
                                amountFalse = amountFalse + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            }
                            else
                            {

                                amountTrue = amountTrue + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            }
                            dataGridView1.Rows[i].Cells[0].Value = ++i;
                            i--;
                        }
                        //  subTotal.Text = amountFalse + amountTrue + "";
                        returnAmount.Text = amountTrue + "";
                        // cashPaid.Text = "0";
                        // balance.Text = "0.0";
                    }

                }
                else if (e.ColumnIndex == 13)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("true"))
                    {
                        MessageBox.Show("Sorry,You Have Selected already Return Row Data");
                    }
                    else
                    {
                        new itemTable3(this, dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), e.RowIndex + "").Visible = true;

                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + " /" + a.StackTrace);
            }
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bILLTOAREAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///cashPaid.Focus();
        }

        private void aDDITEMTOINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void code_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void unitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void availebleQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void warrentyCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cashPaid_KeyUp(object sender, KeyEventArgs e)
        {



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Emprt Data for Generate Invoice");
                //code.Focus();
            }
            else if (!dateNow & !changeInvoiceDifDate)
            {
                MessageBox.Show("Sorry This is and Past Genarate Invoice and You Havent Permission to EDIT/ DELETE OR RETURN");
            }

            else if ((MessageBox.Show("Update Invoice ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                    invoieNoTemp = invoiceino.ToString().Split('-')[1].ToString();
                    //+++++Intial OLD INVOice++++
                    conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceid= '" + "R-" + invoieNoTemp + "' and type='" + "INVOICE-RETURN" + "'", conn).ExecuteNonQuery();

                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + false + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select qty from purchasingPriceList where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            reader2.Close();
                            conn2.Close();
                            conn2.Open();
                            new SqlCommand("update purchasingPriceList set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }

                        conn2.Close();
                        conn2.Open();
                        new SqlCommand("update item set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + true + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select qty from purchasingPriceList where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteReader();
                        if (reader2.Read())
                        {
                            reader2.Close();
                            conn2.Close();
                            conn2.Open();
                            new SqlCommand("update purchasingPriceList set qty=qty-'" + reader[1] + "' where code='" + reader[0] + "' and purchasingprice='" + reader[2] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }

                        conn2.Close();
                        conn2.Open();
                        new SqlCommand("update item set qty=qty-'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from warrenty where invoiceid='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    ///+++++++++++++++++++++++++   
                    amount = 0;
                    profit = 0;
                    profitTotal = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    }

                    //  MessageBox.Show("1");

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                        {
                            //    MessageBox.Show("1");
                            conn2.Open();
                            new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[11].Value + "','" + dataGridView1.Rows[i].Cells[12].Value + "')", conn2).ExecuteNonQuery();
                          //  new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[10].Value + "','" + dataGridView1.Rows[i].Cells[11].Value + "')", conn2).ExecuteNonQuery();
                      
                            conn2.Close();

                        }
                        else
                        {
                            conn2.Open();

                            new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[11].Value + "','" + dataGridView1.Rows[i].Cells[12].Value + "')", conn2).ExecuteNonQuery();
                       
                            conn2.Close();

                            if (dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("true"))
                            {
                                conn2.Open();
                                new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + false + "','" + qtyTemp + "','" + DateTime.Now + "','" + "INVOICE-RETURN" + "','" + user + "','" + 0 + "')", conn2).ExecuteNonQuery();
                                conn2.Close();
                                conn.Open();
                                reader = new SqlCommand("select qty from item where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    if (reader.GetDouble(0) < qtyTemp)
                                    {
                                        conn.Close();
                                        conn.Open();
                                        new SqlCommand("update item set qty='" + 0 + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        reader.Close();
                                        conn.Close();
                                        conn.Open();
                                        new SqlCommand("update item set qty=qty+'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                    }
                                }


                                reader.Close();
                                reader.Close();
                                conn.Close();
                                conn.Open();
                                states = true;
                                reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' order by date", conn).ExecuteReader();
                                while (reader.Read())
                                {
                                    var price = reader.GetDouble(0);


                                    states = false;
                                    if (qtyTemp == 0)
                                    {

                                    }
                                    else if (qtyTemp <= reader.GetDouble(1))
                                    {
                                        profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;
                                        profitTotal = profitTotal + profit;

                                        // reader.Close();
                                        //   MessageBox.Show("2");

                                        conn2.Open();

                                        new SqlCommand("update purchasingPriceList set qty=qty+'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        conn2.Open();
                                        new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        qtyTemp = 0;
                                    }
                                    else
                                    {
                                        profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;

                                        profitTotal = profitTotal + profit;

                                        qtyTemp = qtyTemp - reader.GetDouble(1);
                                        //   reader.Close();
                                        //    MessageBox.Show("3");

                                        conn2.Open();
                                        new SqlCommand("update purchasingPriceList set qty=qty+'" + reader.GetDouble(1) + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        conn2.Open();
                                        new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();

                                    }
                                }
                                reader.Close();
                                conn.Close();
                            }
                            if (!dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("true"))
                            {
                                conn.Open();
                                reader = new SqlCommand("select qty from item where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    if (reader.GetDouble(0) < qtyTemp)
                                    {
                                        conn.Close();
                                        conn.Open();
                                        new SqlCommand("update item set qty='" + 0 + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        reader.Close();
                                        conn.Close();
                                        conn.Open();
                                        new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                    }
                                }


                                reader.Close();
                                reader.Close();
                                conn.Close();
                                conn.Open();
                                states = true;
                                reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' order by date", conn).ExecuteReader();
                                while (reader.Read())
                                {
                                    var price = reader.GetDouble(0);


                                    states = false;
                                    if (qtyTemp == 0)
                                    {

                                    }
                                    else if (qtyTemp <= reader.GetDouble(1))
                                    {
                                        profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;
                                        profitTotal = profitTotal + profit;

                                        // reader.Close();
                                        //   MessageBox.Show("2");

                                        conn2.Open();

                                        new SqlCommand("update purchasingPriceList set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        conn2.Open();
                                        new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        qtyTemp = 0;
                                    }
                                    else
                                    {
                                        profit = (((Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) / 100) * (100 - Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()))) - reader.GetDouble(0)) * qtyTemp;

                                        profitTotal = profitTotal + profit;

                                        qtyTemp = qtyTemp - reader.GetDouble(1);
                                        //   reader.Close();
                                        //    MessageBox.Show("3");

                                        conn2.Open();
                                        new SqlCommand("update purchasingPriceList set qty=qty-'" + reader.GetDouble(1) + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                        conn2.Open();
                                        new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();

                                    }
                                }
                                reader.Close();
                                conn.Close();
                            }

                          
                        }

                    }
                  


                  
                    //++++++++++++++++++++Tax Invoice Start
                    if (cutomerID.Equals(""))
                    {
                        cutomerID = customer.Text;
                    }
                    conn.Open();
                    new SqlCommand("delete from cashSummery where reason='" + "CASH PAID" + "' and remark='" + "CASH RETURN FOR RETURN INVOICE R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    if ((MessageBox.Show("Do you need Return Cash ??", "Confirmation",
       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        conn2.Open();
                        new SqlCommand("insert into cashSummery values ('" + "CASH PAID" + "','" + "CASH RETURN FOR RETURN INVOICE R-" + invoieNoTemp + "','" + returnAmount.Text + "','" + DateTime.Now + "','" + user + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    else {
                        var a = Double.Parse(subTotal.Text) - Double.Parse(returnAmount.Text);
                        conn.Open();
                        new SqlCommand("update invoiceRetail set subtotal='" + a + "',netTotal='" + a + "' where  id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        //   MessageBox.Show(a+"");
                        conn.Open();
                        new SqlCommand("update creditInvoiceRetail set amount='" + a + "',balance='" + a + "'-paid where  invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                   
                    conn.Open();
                    new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    if ((MessageBox.Show("REeturn-Invoice Succefully Generated , Do You want to Print it", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        // testPrint a = new testPrint();
                        //    conn.Open();
                        var aa = "";

                        new invoicePrint().setprintHalfInvoiceReturn("VAR" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, "0", "0", DateTime.Now, conn, reader, user, saleRef.Text, poNumber.Text, "0", "0", "0", "0", returnAmount.Text);

                       
                        //  a.Visible = true;
                    }
                    //++++++++++++++++++++Tax Inoice End
                    //  clear();


                }
                catch (Exception a)
                {
                    MessageBox.Show("Sorry, You Have Make Mistake,Try Again " + a.StackTrace + "/" + a.Message);
                    conn.Close();
                    conn2.Close();
                }
                db.setCursoerDefault();

            }
        }

        private void cREATEINVOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void cashPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button3_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                term.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button3.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void term_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void eDITTERMEDITTERMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void eNABLEDISABLEAUTOLOADINGITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadItemCheck)
            {
                loadItemCheck = false;
            }
            else
            {

                loadItemCheck = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            customer.Focus();
        }

        private void cashPaid_Leave(object sender, EventArgs e)
        {
            //   MessageBox.Show("SASA");
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var aa = "";

            new invoicePrint().setprintHalfInvoiceReturn("VAR" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, "0", "0", DateTime.Now, conn, reader, user, saleRef.Text, poNumber.Text, "0", "0", "0", "0", returnAmount.Text);

        }
    }
}
