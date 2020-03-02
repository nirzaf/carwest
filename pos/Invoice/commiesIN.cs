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
    public partial class commiesIN : Form
    {
        public commiesIN(invoiceNew form, String user, string invoiceType, string invoino, Int32 gridID,string customer,string rate)
        {
            InitializeComponent();
            home = form;
            this.user = user;
            gridIDDB = gridID;
            customerDB = customer;
            rateDB = rate;

        }
        // My Variable Start
        DB db, db2, db3, db4;
        invoiceNew home;
        SqlConnection conn, conn2, conn3, conn4;
        SqlDataReader reader, reader2, reader3, reader4;
        ArrayList arrayList, stockList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, cashFLowAuto;
        string invoiceTypeDB, invoiceNoDB, user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        Double amount, amount2, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        int count, gridIDDB;
        string type = "",customerDB,rateDB;
        Boolean loadItemCheck = false, discPrestage, isNBT, isTax;
        public double paidAmount = 0, taxpre, nbtpre, purchashingPrice, cashPaidDB;
        DateTime invoiceDate;
        // my Variable End
        //my Method Start++++++
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
                MessageBox.Show(a.StackTrace);
                conn.Close();
            }
        }
        Boolean checkUser()
        {
            try
            {
                // MessageBox.Show(creditDetailB + "/" + cardDetailB + "/" + chequeDetailB);
                states = false;
                if (!creditDetailB)
                {
                    states = true;
                    //    MessageBox.Show("1");
                }
                else
                {
                    //MessageBox.Show("2");
                    if (!cutomerID.Equals(""))
                    {
                        states = true;
                    }




                }

            }
            catch (Exception)
            {
                states = false;
                conn.Close();
            }
            return states;
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


        }
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
        string tempCustomer = "";
        public Boolean loadCustomer(string id)
        {
            // MessageBox.Show(id);
            try
            {
                conn.Open();

                reader = new SqlCommand("select empid,nickName from emp where empid='" + id + "'", conn).ExecuteReader();


                if (reader.Read())
                {
                    states = true;
                    customer.Text = reader[1] + "";

                    cutomerID = reader[0] + "";
                    tempCustomer = reader[0] + "";

                }
                else
                {
                    //  customer.Text = "[cash supplier]";
                    states = false;
                    cutomerID = "";
                    tempCustomer = "";
                }
                unitPrice.Focus();
                reader.Close();
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "nnnnnnnnnnnnnnnnnnnnnnn " + a.StackTrace);
                conn.Close();
                reader.Close();
            }
            return states;
        }
        string pendingNo = "";

        void loadInvoiceNoRetail()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from returnGoods", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceNo = invoiceMaxNo + "";
                reader.Close();
                conn.Close();

            }
            catch (Exception)
            {
                // throw;
                invoiceNo = "1";
                reader.Close();
                conn.Close();
            }
        }
        Double amountR;
        void clear()
        {
            costPrice.Text = "";
            LOADcHECK = false;
            pendingNo = "";
            invoieNoTemp = "";
            invoiceNo = "";
            qtyOut.Text = "";

            if (discPrestage)
            {
                comboDiscount.SelectedIndex = 0;

            }
            else
            {
                comboDiscount.SelectedIndex = 1;
            }

            customer.Text = "[CASH SUPPLIER]";
            mobileNumber.Text = "";
            address.Text = "";

            cutomerID = "";
            dataGridView1.Rows.Clear();
            // loadInvoiceNoRetail();
            clearSub();
            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;

            comboSaleAccount.SelectedIndex = -1;
            stock.Text = "0";
            qtyOut.Text = "0";
            customer.Focus();
        }
        void clearSub()
        {
            code.Text = "";
            unitPrice.Text = "0.0";
            qtyIn.Text = "";
            discount.Text = "0";
            code.Focus();
            costPrice.Text = "";

        }
        void loadItem(string codeValue)
        {

        }
        Int16 itemCount = 0;
        string uom;
        void addToTable()
        {
            button10_Click(null, null);


        }

        public void addItemNew(string codeL)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    rowCount++;
                }
                //   MessageBox.Show(codeL);
                uom = "";
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,rate from item where code='" + codeL + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;

                    //       tempDesc.Text = reader[1] + "";
                    uom = reader[4] + "";

                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {

                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }

                    conn.Close();

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qtyIn.Text)));
                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qtyIn.Text)));

                    }
                    amount = Math.Round(amount, 2);
                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qtyIn.Text, amount, uom, qtyIn.Text, 1);

                }
                else
                {
                    item = false;

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qtyIn.Text)));

                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qtyIn.Text)));

                    }
                    dataGridView1.Rows.Add(rowCount + "", "#", code.Text, unitPrice.Text, discount.Text, qtyIn.Text, amount, "", qtyIn.Text, 1);
                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                reader.Close();
                conn.Close();
                amount = 0;
                amount2 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                }


                clearSub();

                code.Focus();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }
        public void addToTableSep(double qtyH, double unitPriceH, double discountH, double qtyFull)
        {
            if (true)
            {
                try
                {

                    if (qtyH <= 0)
                    {
                        MessageBox.Show("Sorry Stock not Available on this Item to Invoice ");
                        qtyIn.Focus();
                    }


                    else
                    {


                        if (dataGridView1.Rows.Count == 0)
                        {
                            amount2 = (unitPriceH * qtyH);

                            rowCount++;
                            if (discPrestage)
                            {
                                amount = ((unitPriceH - ((unitPriceH / 100) * discountH)) * (qtyH));
                            }
                            else
                            {
                                amount = ((unitPriceH - discountH) * (qtyH));

                            }
                            amount = Math.Round(amount, 2);
                            dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPriceH, discountH, qtyH, amount, uom, qtyFull, 0);


                        }
                        else
                        {
                            states = true;

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                //   MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString()+"b"+code.Text+"c");
                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                {
                                    states = false;
                                }
                            }
                            // MessageBox.Show(states+"");
                            if (!states)
                            {
                                if ((MessageBox.Show("This Item Already ADD to Invoice, Do you Need to Update Current Record", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                                {
                                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                    {
                                        if (!states)
                                        {
                                            if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(code.Text))
                                            {
                                                states = true;
                                                qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + qtyH;


                                                if (discPrestage)
                                                {
                                                    amountTemp = ((unitPriceH - ((unitPriceH / 100) * discountH)) * qtyTemp);

                                                }
                                                else
                                                {
                                                    amountTemp = (Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * qtyTemp;

                                                }
                                                amountTemp = Math.Round(amountTemp, 2);
                                                //   amount = Double.Parse(subTotal.Text) + amountTemp;
                                                //dataGridView1.Rows.RemoveAt(i);
                                                //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);

                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[3].Value = description;
                                                dataGridView1.Rows[i].Cells[4].Value = unitPriceH;
                                                dataGridView1.Rows[i].Cells[5].Value = discountH;
                                                dataGridView1.Rows[i].Cells[6].Value = qtyTemp;

                                                dataGridView1.Rows[i].Cells[7].Value = amountTemp;
                                                dataGridView1.Rows[i].Cells[9].Value = qtyFull;
                                                dataGridView1.Rows[i].Cells[8].Value = uom;
                                                dataGridView1.Rows[i].Cells[10].Value = 0;
                                            }
                                        }


                                    }


                                }
                            }
                            else
                            {
                                rowCount++;
                                if (discPrestage)
                                {
                                    amount = ((unitPriceH - ((unitPriceH / 100) * discountH)) * (qtyH));

                                }
                                else
                                {
                                    amount = ((unitPriceH - discountH) * (qtyH));


                                }
                                amount = Math.Round(amount, 2);
                                dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPriceH, discountH, qtyH, amount, uom, qtyFull, 0);

                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Azure;


                            }
                        }

                        amount = 0;
                        amount2 = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            if (Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) != 0)
                            {

                                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

                            }
                        }




                        clearSub();
                        code.Focus();

                    }


                }
                catch (Exception s)
                {
                    MessageBox.Show("Please Enter Value " + s.Message + "/" + s.StackTrace);

                }
            }
            else
            {
                MessageBox.Show("Sorry , Maximum Item's Count Per Invoice Hav Exceed.please Genarate New Invoice");
            }



        }


        public void updateTableItem(string unitPrice, string discount, string qty, Int32 index)
        {
            if (discPrestage)
            {
                amountTemp = ((Double.Parse(unitPrice) - ((Double.Parse(unitPrice) / 100) * Double.Parse(discount))) * Double.Parse(qty));

            }
            else
            {
                amountTemp = (Double.Parse(unitPrice) - Double.Parse(discount)) * Double.Parse(qty);

            }
            amountTemp = Math.Round(amountTemp, 2);
            //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);


            dataGridView1.Rows[index].Cells[4].Value = unitPrice;
            dataGridView1.Rows[index].Cells[5].Value = discount;
            dataGridView1.Rows[index].Cells[6].Value = qty;

            dataGridView1.Rows[index].Cells[7].Value = (amountTemp + "");
            dataGridView1.Rows[index].Cells[9].Value = qty;
            // MessageBox.Show(amountTemp + "");
            amount = 0;
            amount2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

            }

            //  total.Text = amount + amount2 + "";

        }
        public void updateTableItemSep(string unitPrice, string discount, string qty, Int32 index, double qtyAll)
        {
            if (discPrestage)
            {
                amountTemp = ((Double.Parse(unitPrice) - ((Double.Parse(unitPrice) / 100) * Double.Parse(discount))) * Double.Parse(qty));

            }
            else
            {
                amountTemp = (Double.Parse(unitPrice) - Double.Parse(discount)) * Double.Parse(qty);

            }
            amountTemp = Math.Round(amountTemp, 2);
            //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);


            dataGridView1.Rows[index].Cells[4].Value = unitPrice;
            dataGridView1.Rows[index].Cells[5].Value = discount;
            dataGridView1.Rows[index].Cells[6].Value = qty;

            dataGridView1.Rows[index].Cells[7].Value = (amountTemp + "");
            dataGridView1.Rows[index].Cells[9].Value = qtyAll;
            // MessageBox.Show(amountTemp + "");
            amount = 0;
            amount2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

            }

            // total.Text = amount + amount2 + "";

        }

        void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    //  nEWCUSTOMERToolStripMenuItem.Enabled = reader.GetBoolean(6);

                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }

        }


        bool checkload = false;
        //   my Method End+++++++++
        private void invoiceNew_Load(object sender, EventArgs e)
        {
            checkload = true;

            invoiceDate = DateTime.Now;
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            //this.WindowState = FormWindowState.Normal;
            ////  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.Bounds;

            //int height = Screen.PrimaryScreen.Bounds.Height;
            //int width = Screen.PrimaryScreen.Bounds.Width;
            //quickPanel.Width = width;
            //dataGridView1.Width = width - 370;
            //dataGridView1.Height = height - (400);

            //dataGridView1.Columns[2].Width = dataGridView1.Width - 615;
            //Point p = new Point();


            //p.X = width - panel4.Width - 15;
            //p.Y = height - panel4.Height - 15;
            //panel4.Location = p;
            //p = panelTax.Location;
            //p.X = width - panelTax.Width - 10;
            //panelTax.Location = p;

            //btn = new DataGridViewButtonColumn();
            //dataGridView1.Columns.Add(btn);
            //btn.Width = 60;
            //btn.Text = "REMOVE";

            //btn.UseColumnTextForButtonValue = true;

            db = new DB();
            conn = db.createSqlConnection2();
            db2 = new DB();
            conn2 = db.createSqlConnection2();
            db3 = new DB();
            conn3 = db3.createSqlConnection2();
            db4 = new DB();
            conn4 = db4.createSqlConnection2();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;


            address.CharacterCasing = CharacterCasing.Upper;
            comboPrinter.SelectedIndex = 0;
            loadUser();
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {

                    saveInvoiceWithoutPay = reader.GetBoolean(0);
                    discPrestage = reader.GetBoolean(4);
                    loadItemCheck = reader.GetBoolean(9);
                }
                else
                {
                    saveInvoiceWithoutPay = false;
                }
                conn.Close();

                if (discPrestage)
                {
                    comboDiscount.SelectedIndex = 0;

                }
                else
                {
                    comboDiscount.SelectedIndex = 1;
                }
            }
            catch (Exception)
            {
                conn.Close();
            }


            loadCompany();
            loadAccountList();
            this.ActiveControl = customer;
            if (!customerDB.Equals(""))
            {
                loadCustomer(customerDB);
            }

            unitPrice.Text = rateDB;
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

        private void button1_Click(object sender, EventArgs e)
        {
            // new cusomerQuick(this).Visible = true;

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
            if (e.ColumnIndex == 11)
            {
                //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                amount = 0.0;
                amount2 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    dataGridView1.Rows[i].Cells[0].Value = ++i;
                    i--;

                }

                //   total.Text = amount + amount2 + "";

                rowCount--;
            }
            else if (e.ColumnIndex == 12)
            {

                // new itemTable(this, dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), e.RowIndex + "", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;


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

            //new termXash(this, Double.Parse(total.Text), Double.Parse(cashPaid.Text), creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
            //     MessageBox.Show(creditDetailB+"");
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aDDITEMTOINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Focus();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void code_ImeModeChanged(object sender, EventArgs e)
        {




        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(reason, unitPrice, unitPrice, e.KeyValue);
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            //db.setTextBoxPath(code, qtyOut, qtyOut, e.KeyValue);      
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    var a = unitPrice.Text;
                    var aa = 0.0;
                    if (a.Equals(""))
                    {
                        aa = 0;
                    }

                    else
                    {
                        aa = Double.Parse(unitPrice.Text);
                    }

                    home.setCoomis(gridIDDB, cutomerID, aa + "");
                    this.Dispose();

                }

                catch (Exception)
                {


                }
            }

        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                unitPrice.Focus();
            }
            else if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    costPrice.Text = (Double.Parse(unitPrice.Text)) - (Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text) + "";
                    qtyIn.Focus();
                }
                catch (Exception)
                {

                }
            }

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
            if (e.KeyValue == 40)
            {
                qtyOut.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                qtyOut.Focus();
            }
            else if (e.KeyValue == 38)
            {
                discount.Focus();
            }
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
            listBox1.Visible = false;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customer.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                customer.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                customer.SelectionLength = customer.MaxLength;
                loadCustomer(customer.Text);
            }

        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            customer.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            customer.SelectionLength = customer.MaxLength;
            loadCustomer(customer.Text);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            customer.Text = listBox1.SelectedItem.ToString().Split(' ')[0];

        }

        private void warrentyCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void warrentyCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                dataGridView1.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
            }
            else if (e.KeyValue == 38)
            {
                qtyIn.Focus();
            }
        }

        private void cashPaid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        Int32 idTemp = 0;
        void getID()
        {

            try
            {
                conn2.Open();
                reader2 = new SqlCommand("select max(auto) from customer", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    idTemp = reader2.GetInt32(0);
                    idTemp++;
                }
                conn2.Close();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }
        private void cREATEINVOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void cashPaid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void unitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void sELECTCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void eDITTERMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);

        }

        private void eNABLEDISABLEAUTOLOADINGITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadItemCheck)
            {
                loadItemCheck = false;
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Visible = true;
                loadItemCheck = true;
            }
        }

        private void nEWCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerProfile(this, user).Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cASHPAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //  MessageBox.Show(comboCompany.SelectedItem.ToString().Split('-')[0].ToString());
                reader = new SqlCommand("select name,istax,taxpre,isNBT,nbtPre,defa from company where  id='" + comboCompany.SelectedItem.ToString().Split('-')[0].ToString() + "'", conn).ExecuteReader();
                if (reader.Read())
                {


                    isNBT = reader.GetBoolean(3);
                    isTax = reader.GetBoolean(1);
                    taxpre = reader.GetDouble(2);
                    nbtpre = reader.GetDouble(4);

                }
                conn.Close();

            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void saleRef_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void saleRef_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void poNumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void saleRef_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void saleRef_Leave(object sender, EventArgs e)
        {

        }

        private void saleRef_MouseLeave(object sender, EventArgs e)
        {

        }

        private void saleRef_KeyDown_1(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboCompany_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboChequePayment_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboCardPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            //       e.SuppressKeyPress = true;
        }

        private void comboSaleAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //      e.SuppressKeyPress = true;
        }

        private void comboCardPayment_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void comboSaleAccount_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void disc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void subTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void vEHICLENUMBERToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void vehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void vehicleDescrition_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void metreNow_KeyDown(object sender, KeyEventArgs e)
        {

        }
        bool meterCheck = false;
        private void metreNext_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void metreNow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;

        }

        private void vehicleNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void meterNextOn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void meterNextOn_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void metreNext_TextChanged(object sender, EventArgs e)
        {

        }

        private void qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void iTEMPROFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new itemProfile(this, user).Visible = true;
        }

        private void comboDiscount_DropDownClosed(object sender, EventArgs e)
        {
            if (comboDiscount.SelectedIndex == 0)
            {
                discPrestage = true;
            }
            else
            {
                discPrestage = false;
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void customer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (customer.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    customer.Focus();
                }
                else
                {
                    loadCustomer(customer.Text);
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
                        customer.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        private void customer_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | customer.Text.Equals("")))
            {
                db.setList(listBox1, customer, customer.Width);
                listBox1.Visible = true;

                listBox1.Height = 55;
                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    /// MessageBox.Show("12");
                    reader = new SqlCommand("select empid,nickName from emp where nickName like '%" + customer.Text + "%' ", conn).ExecuteReader();


                    arrayList = new ArrayList();
                    states = true;
                    while (reader.Read())
                    {
                        //  MessageBox.Show("12a");
                        listBox1.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states)
                    {
                        //  MessageBox.Show("12");
                        checkload = false;
                        listBox1.Visible = false;
                    }
                }
                catch (Exception a)
                {//
                    // MessageBox.Show(a.Message);
                    conn.Close();
                }

            }
            if (customer.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, mobileNumber, mobileNumber, e.KeyValue);
        }

        private void mobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //db.setTextBoxPath(address, code, code, e.KeyValue);

        }

        private void listBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                customer.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox2_MouseClick_1(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;
            loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            customer.Text = listBox2.SelectedItem.ToString();
        }

        private void metreNow_KeyUp(object sender, KeyEventArgs e)
        {
            meterCheck = true;

        }

        private void metreNext_KeyUp(object sender, KeyEventArgs e)
        {
            meterCheck = true;
        }

        private void vehicleNumber_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void radioCredit_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void checkOF_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void cASHPAIDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void cashPaid_KeyUp_1(object sender, KeyEventArgs e)
        {

        }

        private void chequeAmount_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cardAmount_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cashPaid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cashPaid_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void creditAmount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void creditAmount_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void chequeAmount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chequeDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void creditPeriod_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void balance_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cardAmount2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            commisSerch a = new commisSerch(this, user);
            a.Visible = true;
            a.TopMost = true;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void cASHBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cHEQUEBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cREDITBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cARDBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void eXPENSESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExpensesNormal(this, user).Visible = true;
        }

        private void pURCHASINGBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new purchasing(this, user, "").Visible = true;
        }

        private void pURCHASINGPAIDBILLPAIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new purchasing(this, user, "").Visible = true;
        }

        private void rETURNGODDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invoiceSearch(this, user).Visible = true;
        }

        private void rEGISTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new customerProfile(this, user).Visible = true;
        }

        private void pRICELISTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ExpensesNormal(this, user).Visible = true;
        }

        void clearIN()
        {
            try
            {

                {
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,credit from returnGoods where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(2))
                        {
                            conn2.Open();
                            new SqlCommand("update item set qty=qty-'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }
                        else
                        {
                            conn2.Open();
                            new SqlCommand("update item set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }



                    }
                    reader.Close();
                    conn.Close();
                    var cashPaid = 0.0;


                    conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceID='" + "RE-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();


                }
                conn.Close();


            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + " " + a.StackTrace);
                conn.Close();
            }
        }



        private void button10_Click(object sender, EventArgs e)
        {

            {
                db.setCursoerWait();
                try
                {
                    conn.Open();
                    new SqlCommand("insert into commis values ('" + cutomerID + "','" + reason.Text + "','" + code.Text + "','" + unitPrice.Text + "','" + qtyOut.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Saved Succefully");


                    clear();

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



        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cREATEPENDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cRETAEINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button10_Click(null, null);
        }

        private void cLEARToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void customer_TextChanged(object sender, EventArgs e)
        {

        }

        private void iTEMADDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            code.Focus();
        }

        private void vEHICLEADDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sEARCHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click_2(null, null);
        }
        bool LOADcHECK = false;
        public void loadInvoice(string id)
        {
            try
            {
                // MessageBox.Show(id);
                clear();
                invoieNoTemp = id.ToString();
                //    MessageBox.Show("1");
                db.setCursoerWait();
                invoiceTypeDB = "CASH";
                {
                    conn3.Open();
                    reader3 = new SqlCommand("select * from GRN where id='" + invoieNoTemp + "'", conn3).ExecuteReader();
                    if (reader3.Read())
                    {

                        LOADcHECK = true;
                        customer.Text = reader3[1] + "";

                        //    total.Text = reader3[9] + "";
                        this.Text = id + "  (" + reader3.GetDateTime(4).ToShortDateString() + " " + reader3.GetTimeSpan(7) + ")";
                        var ter = reader3[6] + "";



                        cashPaidDB = reader3.GetDouble(12);
                        invoiceDate = reader3.GetDateTime(4);
                        reader3.Close();
                        conn3.Close();

                        loadCustomer(customer.Text);


                        conn4.Open();

                        reader4 = new SqlCommand("select * from creditgrn where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();
                        if (reader4.Read())
                        {


                            paidAmount = paidAmount + reader4.GetDouble(4);

                            invoiceTypeDB = "CREDIT";
                        }
                        reader4.Close();
                        conn4.Close();







                        conn3.Open();
                        reader3 = new SqlCommand("select * from grnDetail where invoiceId='" + invoieNoTemp + "' and pc='" + false + "'", conn3).ExecuteReader();
                        Int32 count = 0;
                        rowCount = 0;
                        while (reader3.Read())
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount, reader3[1], reader3[10], reader3[3], reader3[7], reader3[2], reader3[14], reader3[4], reader3[15]);

                        }
                        conn3.Close();

                    }
                    else
                    {

                        MessageBox.Show("GRN not Loading Correctlly");




                    }
                    code.Focus();
                    conn3.Close();
                }



                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied GRN ID " + a.Message + " //" + a.StackTrace);
                conn3.Close();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //     loadInvoice(textBox1.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                //          loadCustomer("S-"+textBox1.Text);

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new bankSummery().Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new customerProfile(this, user).Visible = true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // new purchasingNew(this,user,"CASH").Visible = true;

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void total_KeyDown(object sender, KeyEventArgs e)
        {
            //  db.setTextBoxPath(textBox1, invoiceNumberSupplier, invoiceNumberSupplier, e.KeyValue);
        }

        private void invoiceNumberSupplier_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void invoceDateSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                code.Focus();
            }
            else if (e.KeyValue == 38)
            {
                ///        invoceDateSupplier.Focus();
            }
        }

        private void qtyIn_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void qtyOut_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void qtyOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                unitPrice.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
            }
            else if (e.KeyValue == 38)
            {
                qtyIn.Focus();
            }

        }


        private void qtyOut_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cutomerID = "";
            customer.Text = "";
            address.Text = "";
            mobileNumber.Text = "";
            customer.Focus();
        }

        private void reason_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, code, code, e.KeyValue);
        }
    }
}
