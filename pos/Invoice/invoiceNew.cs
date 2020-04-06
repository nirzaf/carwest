using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace pos
{
    public partial class invoiceNew : Form
    {
        public invoiceNew(Form form, string user, string invoiceType)
        {
            InitializeComponent();
            home = form;
            this.user = user;
            invoiceTypeDB = invoiceType;
        }

        // My Variable Start
        private DB db, db2, db3, db4;

        private Form home;
        private SqlConnection conn, conn2, conn3, conn4;
        private SqlDataReader reader, reader2, reader3, reader4;
        private ArrayList arrayList, stockList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, cashFLowAuto;
        public string invoiceTypeDB, user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        private String[] idArray;
        private DataGridViewButtonColumn btn;
        private Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        private Double amount, amount2, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        public string[] creditDetail, chequeDetail, cardDetail;
        private string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        private int count;
        private string type = "";
        private Boolean loadItemCheck = false, discPrestage, isNBT, isTax;
        public double paidAmount = 0, taxpre, nbtpre, purchashingPrice, cashPaidDB;
        private DateTime invoiceDate;

        // my Variable End
        //my Method Start++++++
        private void loadCompany()
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

        private Boolean checkUser()
        {
            try
            {
                // MessageBox.Show();
                ///  MessageBox.Show(creditDetailB + "/" + cardDetailB + "/" + chequeDetailB);
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

        private void loadAccountList()
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

        private string tempCustomer = "";

        public Boolean loadCustomer(string id)
        {
            //   MessageBox.Show(id);
            try
            {
                try
                {
                    listBox1.Visible = false;

                    listBox2.Visible = false;
                }
                catch (Exception)
                {
                }
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    customer.Text = reader[2] + "";
                    address.Text = reader[3] + "";
                    mobileNumber.Text = reader[4] + "";
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
                address.Focus();
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

        private string pendingNo = "";

        public void loadPending(string id)
        {
            try
            {
                invoiceTypeDB = "CASH";
                loadInvoiceType();

                invoieNoTemp = id.ToString();
                pendingNo = invoieNoTemp + "";
                //    MessageBox.Show("1");
                db.setCursoerWait();

                {
                    conn3.Open();
                    reader3 = new SqlCommand("select * from pending where id='" + invoieNoTemp + "'", conn3).ExecuteReader();
                    if (reader3.Read())
                    {
                        customer.Text = reader3[1] + "";

                        total.Text = reader3[9] + "";
                        this.Text = id + "  (" + reader3.GetDateTime(4).ToShortDateString() + " " + reader3.GetTimeSpan(7) + ")";
                        var ter = reader3[6] + "";

                        cashPaid.Text = reader3[12] + "";
                        balance.Text = reader3[13] + "";
                        cashPaidDB = reader3.GetDouble(12);
                        invoiceDate = reader3.GetDateTime(4);
                        serviceBY.SelectedItem = reader3[14] + "";
                        reader3.Close();
                        conn3.Close();

                        loadCustomer(customer.Text);

                        dataGridView1.Rows.Clear();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from pendingDetail where invoiceId='" + invoieNoTemp + "' ", conn3).ExecuteReader();
                        Int32 count = 0;
                        rowCount = 0;
                        while (reader3.Read())
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount, reader3[1], reader3[10], reader3[3], reader3[7], reader3[2], reader3[4], "FALSE", reader3[11], reader3[12], reader3[13]);
                        }
                        conn3.Close();

                        conn3.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from vehiclePending where invoiceId='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            vehicleNumber.Text = reader3[1] + "";
                            vehicleDescrition.Text = reader3[2] + "";
                            metreNow.Text = reader3[3] + "";
                            metreNext.Text = reader3[4] + "";
                            this.Text = vehicleNumber.Text;
                        }
                        conn3.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from serviceBy where invoiceId='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            serviceByList.Rows.Add(reader3[1]);
                        }
                        conn3.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invoice not Loading Correctlly");

                        this.Dispose();
                        home.Enabled = true;
                        home.TopMost = true;
                    }
                    code.Focus();
                    conn3.Close();
                }

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied Invoice ID " + a.Message + " //" + a.StackTrace);
                conn3.Close();
            }
        }

        private void loadInvoiceNoRetail()
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
                invoiceNo = "R-" + invoiceMaxNo + "";
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                // throw;
                invoiceNo = "R-1";
                reader.Close();
                conn.Close();
            }
        }

        private void loadInvoiceNoPending()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from pending", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceNo = "R-" + invoiceMaxNo + "";
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                // throw;
                invoiceNo = "R-1";
                reader.Close();
                conn.Close();
            }
        }

        private Double amountR;

        private Boolean checkTerm()
        {
            if (creditAmount.Text.Equals(""))
            {
                creditAmount.Text = "0";
            }
            if (chequeAmount.Text.Equals(""))
            {
                chequeAmount.Text = "0";
            }
            if (cardAmount.Text.Equals(""))
            {
                cardAmount.Text = "0";
            }
            if (creditPeriod.Text.Equals(""))
            {
                creditPeriod.Text = "0";
            }

            //  MessageBox.Show(creditDetailB+"");
            try
            {
                amountR = (Double.Parse(cashPaid.Text));
            }
            catch (Exception)
            {
                amountR = 0;
            }
            if (creditDetailB)
            {
                amountR = amountR + Double.Parse(creditAmount.Text);
            }
            if (chequeDetailB)
            {
                count = 0;

                amountR = amountR + Double.Parse(chequeAmount.Text);
            }
            // MessageBox.Show(cardDetailB+"/"+chequeDetailB+"/"+creditDetailB);
            if (cardDetailB)
            {
                count = 0;

                amountR = amountR + Double.Parse(cardAmount.Text);
            }

            states = true;
            // MessageBox.Show(amountR + "/" + total.Text);
            if (creditDetailB || cardDetailB || chequeDetailB)
            {
                if (amountR != Double.Parse(total.Text))
                {
                    states = false;
                }
            }
            else
            {
                if (amountR < Double.Parse(total.Text))
                {
                    states = false;
                }
            }

            if (states && creditDetailB)
            {
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select dayLimit from customer where id='" + cutomerID + "' and block='" + false + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        load(cutomerID);
                        if ((Double.Parse(total.Text) + totalOut) > reader.GetDouble(0))
                        {
                            MessageBox.Show("Sorry , Selected Customer Will Be Exceed Credit Limit.Current Total outstanding is " + db.setAmountFormat(totalOut + "") + " and Credit Limit is " + db.setAmountFormat(reader[0] + ""));
                            states = false;
                        }
                    }
                    else
                    {
                        states = false;
                        MessageBox.Show("Sorry Selected User Blocked");
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }

            return states;
        }

        private double load(string cust)
        {
            try
            {
                //  MessageBox.Show("3");
                totalOut = 0;
                //dataGridView1.Rows.Clear();
                try
                {
                    conn2.Open();
                    amount = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select sum(a.balance) from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + cust + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + " 00:00:00" + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        amount = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount = 0;
                    conn2.Close();
                }
                try
                {
                    conn2.Open();
                    amount2 = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select sum(amount2) from receipt where customer='" + cust + "' and date<'" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-1" + "" + "' ", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        amount2 = reader2.GetDouble(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    amount2 = 0;
                    conn2.Close();
                }
                totalOut = amount - amount2;

                for (int i = 1; i <= Int32.Parse(db.getLastDate(DateTime.Now.Month, DateTime.Now.Year)); i++)
                {
                    // MessageBox.Show(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i);
                    conn2.Open();
                    amount = 0;
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select a.* from creditInvoiceRetail as a,invoiceTerm as b where a.customerid='" + cust + "' and a.invoiceID=b.invoiceid and b.cheque='" + false + "' and b.card='" + false + "' and B.credit='" + true + "' and a.date between '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + " 00:00:00" + "' and '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + " 23:59:59" + "' ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        totalOut = totalOut + reader2.GetDouble(4);
                    }
                    conn2.Close();

                    conn2.Open();
                    amount2 = 0;

                    //  MessageBox.Show("5");
                    // MessageBox.Show(customer);
                    arrayList = new ArrayList();
                    reader2 = new SqlCommand("select * from receipt where customer='" + cust + "' and date='" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + i + "" + "' ", conn2).ExecuteReader();
                    while (reader2.Read())
                    {
                        totalOut = totalOut - reader2.GetDouble(5);
                    }
                    conn2.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }

            return totalOut;
            //  MessageBox.Show("5");
        }

        private double totalOut = 0;

        private void clear()
        {
            checkBoxView.Checked = true;
            try
            {
                serviceBY.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // throw;
            }
            cardAmount.Text = "";
            cashPaid.Text = "";
            checkPay = false;
            textBox1.Text = "";
            LOADcHECK = false;
            serviceByList.Rows.Clear();
            pendingNo = "";
            availebleQty_.Text = "";
            creditAmount.Text = "";
            invoieNoTemp = "";
            invoiceNo = "";
            lastPrice.Text = "";
            sendPeriod.Text = "0";
            vehicleServiceCheck.Checked = false;
            checkDF.Checked = false;
            checkOF.Checked = false;
            checkEO.Checked = false;
            checkGO.Checked = false;
            checkAF.Checked = false;
            checkGreesen.Checked = false;
            balance.Text = "0.0";
            total.Text = "0.0";
            vehicleDescrition.Text = "";
            vehicleNumber.Text = "";
            metreNext.Text = "";
            metreNow.Text = "";
            cashPaid.Text = "";
            if (discPrestage)
            {
                comboDiscount.SelectedIndex = 0;
            }
            else
            {
                comboDiscount.SelectedIndex = 1;
            }
            balance.Text = "0.0";
            customer.Text = "[CASH CUSTOMER]";
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
            dataGridView2.Rows.Clear();
            messege.Text = "";
            vehicleNumber.Focus();
            // checkStockOD();
            vehicleNumber.Focus();
        }

        private void checkStockOD()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from item where qty<'" + 2 + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    new stockReport2(this, user).Visible = true;
                }
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void clearSub()
        {
            code.Text = "";
            unitPrice.Text = "0.0";
            tempDesc.Text = "";
            qty.Text = "";
            discount.Text = "0";
            code.Focus();
            availebleQty_.Text = "0";
            unitPrice.Enabled = false;
            passwordText.Text = "";
        }

        private bool isItemDB = false;

        private void loadItem(string codeValue)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select a.unitPrice from invoiceRetailDetail as a,invoiceRetail as b where b.customerID='" + cutomerID + "'  and a.invoiceID=b.id and a.itemcode='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    lastPrice.Text = db.setAmountFormat(reader[0] + "");
                }
                else
                {
                    lastPrice.Text = "";
                }
                conn.Close();

                uom = "";
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,rate,isitem from item where code='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    isItemDB = reader.GetBoolean(5);
                    item = true;
                    code.Text = codeValue + "";
                    tempDesc.Text = reader[1] + "";
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
                    unitPrice.Text = reader.GetDouble(2) + "";
                    availebleQty_.Text = reader[0] + "";
                    qty.Focus();
                    conn.Close();
                    qty.Text = "";
                }
                else
                {
                    // var code=itemc
                    //  MessageBox.Show("Invalied Item Codea");
                    item = false;
                    clearSub();
                    code.Text = codeValue;
                    unitPrice.Focus();
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception a)
            {
                //    throw;
                reader.Close();
                conn.Close();
                code.Focus();
                MessageBox.Show(a.Message);
            }
        }

        private Int16 itemCount = 0;
        private string uom;

        private void addToTable()
        {
            if (true)
            {
                try
                {
                    // MessageBox.Show(item+"");

                    if (item)
                    {
                        if (qty.Text.Equals("") || Double.Parse(qty.Text) <= 0)
                        {
                            MessageBox.Show("Sorry Stock not Available on this Item to Invoice ");
                            qty.Focus();
                        }
                        else if (isItemDB && Double.Parse(qty.Text) > Double.Parse(availebleQty_.Text))
                        {
                            MessageBox.Show("Sorry Stock not Available on this Item to Invoice ");
                            qty.Focus();
                        }
                        else
                            if (unitPrice.Text.Equals(""))
                        {
                            MessageBox.Show("Sorry Unit Price Cannot be Emprty Or Zero");
                            unitPrice.Focus();
                        }
                        else
                        {
                            if (dataGridView1.Rows.Count == 0)
                            {
                                amount2 = (Double.Parse(unitPrice.Text) * Double.Parse(qty.Text));

                                rowCount++;
                                if (discPrestage)
                                {
                                    amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                                }
                                else
                                {
                                    amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));
                                }
                                amount = Math.Round(amount, 2);
                                dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);
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
                                                    qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) + Double.Parse(qty.Text);

                                                    if (discPrestage)
                                                    {
                                                        amountTemp = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * qtyTemp);
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
                                                    dataGridView1.Rows[i].Cells[2].Value = description;
                                                    dataGridView1.Rows[i].Cells[3].Value = unitPrice.Text;
                                                    dataGridView1.Rows[i].Cells[4].Value = discount.Text;
                                                    dataGridView1.Rows[i].Cells[5].Value = qtyTemp;

                                                    dataGridView1.Rows[i].Cells[6].Value = amountTemp;

                                                    dataGridView1.Rows[i].Cells[8].Value = qtyTemp;
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
                                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                                    }
                                    else
                                    {
                                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));
                                    }
                                    amount = Math.Round(amount, 2);
                                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);

                                    amount = amount + (Double.Parse(total.Text));
                                    var y = dataGridView1.RowCount;
                                    y--;
                                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.Azure;
                                }
                            }
                        }
                        amount = 0;
                        amount2 = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        }

                        total.Text = amount + "";
                    }
                    else
                    {
                        //                 if (discPrestage)
                        //                 {
                        //                     amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                        //                 }
                        //                 else
                        //                 {
                        //                     amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                        //                 }
                        //                 amount = Math.Round(amount, 2);
                        //                 var a = MessageBox.Show("You Have Enterd New Item and Do You Need to Save it to System", "Confirmation",
                        //MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                        //MessageBoxDefaultButton.Button1);
                        //                 if (a == System.Windows.Forms.DialogResult.Yes)
                        //                 {
                        //                     newItemInvoice ab = new newItemInvoice(this);
                        //                     ab.code.Text = code.Text;
                        //                     ab.sellingPrice.Text = unitPrice.Text;
                        //                     ab.Visible = true;
                        //                 }
                        //                 else if (a == System.Windows.Forms.DialogResult.No)
                        //                 {
                        //                     rowCount++;
                        //                     dataGridView1.Rows.Add(rowCount + "", "#", code.Text, unitPrice.Text, discount.Text, qty.Text, amount, "", qty.Text, 1);

                        //                     var y = dataGridView1.RowCount;
                        //                     y--;
                        //                     dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;
                        //                     amount = 0;
                        //                     amount2 = 0;
                        //                     for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        //                     {
                        //                         amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                        //                     }

                        //                     total.Text = amount + "";

                        //                 }
                    }

                    //  updateInvoice();
                    clearSub();

                    cashPaid.Text = "";
                    balance.Text = "0.0";
                    if (cashPaid.Text.Equals(""))
                    {
                    }
                    else

                        if (Double.Parse(cashPaid.Text) == 0)
                    {
                        // MessageBox.Show("1");

                        // MessageBox.Show("2");
                        chequeDetailB = false;
                        cardDetailB = false;
                        creditDetailB = false;
                        cardDetail = null;
                        chequeDetail = null;
                        creditAmount.Text = "0";
                        cashPaid.SelectionLength = cashPaid.TextLength;
                        chequeAmount.Text = "0";
                        cardAmount.Text = "0";
                        balance.Text = Double.Parse(total.Text) * -1 + ""; ;
                        // MessageBox.Show("3");
                    }
                    else
                    {
                        // MessageBox.Show("3");
                        var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);
                        balance.Text = (a * -1) + "";
                    }
                    code.Focus();
                }
                catch (Exception s)
                {
                    MessageBox.Show("Please Enter QTY ");
                }
            }
            else
            {
                MessageBox.Show("Sorry , Maximum Item's Count Per Invoice Hav Exceed.please Genarate New Invoice");
            }
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

                    tempDesc.Text = reader[1] + "";
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
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));
                    }
                    amount = Math.Round(amount, 2);
                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, unitPrice.Text, discount.Text, qty.Text, amount, uom, qty.Text, 1);
                }
                else
                {
                    item = false;

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                    }
                    dataGridView1.Rows.Add(rowCount + "", "#", code.Text, unitPrice.Text, discount.Text, qty.Text, amount, "", qty.Text, 1);
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

                total.Text = amount + "";

                clearSub();

                cashPaid.Text = "";
                balance.Text = "0.0";
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
                        qty.Focus();
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

                            conn.Open();
                            reader = new SqlCommand("select value from distance where code='" + code.Text + "'", conn).ExecuteReader();
                            if (reader.Read())
                            {
                                if (Double.Parse(reader.GetString(0)) != 0)
                                {
                                    if (metreNow.Text.Equals(""))
                                    {
                                        metreNext.Text = reader[0] + "";
                                    }
                                    else
                                    {
                                        metreNext.Text = Int64.Parse(metreNow.Text) + Double.Parse(reader.GetString(0)) + "";
                                    }
                                }
                            }
                            conn.Close();
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
                                conn.Open();
                                reader = new SqlCommand("select value from distance where code='" + code.Text + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    if (Double.Parse(reader.GetString(0)) != 0)
                                    {
                                        if (metreNow.Text.Equals(""))
                                        {
                                            metreNext.Text = reader[0] + "";
                                        }
                                        else
                                        {
                                            metreNext.Text = Int64.Parse(metreNow.Text) + Int64.Parse(metreNext.Text) + "";
                                        }
                                    }
                                }
                                conn.Close();
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

                        total.Text = amount + amount2 + "";

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

            total.Text = amount + amount2 + "";
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

            total.Text = amount + amount2 + "";
        }

        private void loadUser()
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

        private void loadInvoiceType()
        {
            chequeDetailB = false;
            cardDetailB = false;
            creditDetailB = false;

            creditPeriod.Visible = true;
            label10.Visible = true;
            panel3.Visible = false;
            label41.Visible = true;
            creditAmount.Visible = true;
            label39.Visible = true;
            chequeAmount.Visible = true;
            label38.Visible = true;
            cardAmount.Visible = true;
            label3.Visible = true;
            cardAmount2.Visible = true;
            label1.Visible = true;
            label4.Visible = true;
            label9.Visible = true;
            bankName.Visible = true;
            cheQueNumber.Visible = true;
            chequeDate.Visible = true;
            comboBank.Visible = false;
            label55.Visible = false;
            button24.Visible = false;
            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;

            payType.Text = invoiceTypeDB;
            if (invoiceTypeDB.Equals("CASH"))
            {
                panel3.Visible = true;
                creditPeriod.Visible = false;
                label10.Visible = false;
                label41.Visible = false;
                creditAmount.Visible = false;
                label39.Visible = false;
                chequeAmount.Visible = false;
                label38.Visible = false;
                cardAmount.Visible = false;
                label3.Visible = false;
                cardAmount2.Visible = false;
                label1.Visible = false;
                label4.Visible = false;
                label9.Visible = false;
                bankName.Visible = false;
                cheQueNumber.Visible = false;
                chequeDate.Visible = false;
                this.Text = "[ CASH INVOICE ]";
                comboBox1.SelectedItem = "CASH";
                cashPaid.Focus();
            }
            else if (invoiceTypeDB.Equals("CHEQUE"))
            {
                creditPeriod.Visible = false;
                label10.Visible = false;
                label41.Visible = false;
                creditAmount.Visible = false;
                //label39.Visible = false;
                //chequeAmount.Visible = false;
                label38.Visible = false;
                cardAmount.Visible = false;
                label3.Visible = false;
                cardAmount2.Visible = false;
                //label1.Visible = false;
                //label4.Visible = false;
                //label9.Visible = false;
                //bankName.Visible = false;
                //cheQueNumber.Visible = false;
                //chequeDate.Visible = false;
                this.Text = "[ CHEQUE INVOICE ]";
                chequeDetailB = true;
                comboBox1.SelectedItem = "CHEQUE";
                comboBank.Visible = true;
                label55.Visible = true;
                button24.Visible = true;
                chequeAmount.Focus();
            }
            else if (invoiceTypeDB.Equals("CREDIT"))
            {
                // label41.Visible = false;
                //creditAmount.Visible = false;
                label39.Visible = false;
                chequeAmount.Visible = false;
                label38.Visible = false;
                cardAmount.Visible = false;
                label3.Visible = false;
                cardAmount2.Visible = false;
                label1.Visible = false;
                label4.Visible = false;
                label9.Visible = false;
                bankName.Visible = false;
                cheQueNumber.Visible = false;
                chequeDate.Visible = false;
                this.Text = "[ CREDIT INVOICE ]";
                comboBox1.SelectedItem = "CREDIT";
                creditDetailB = true;
                creditAmount.Focus();
            }
            else if (invoiceTypeDB.Equals("CARD"))
            {
                creditPeriod.Visible = false;
                label10.Visible = false;
                label41.Visible = false;
                creditAmount.Visible = false;
                label39.Visible = false;
                chequeAmount.Visible = false;
                //  label38.Visible = false;
                //cardAmount.Visible = false;
                // label3.Visible = false;
                //cardAmount2.Visible = false;
                label1.Visible = false;
                label4.Visible = false;
                label9.Visible = false;
                bankName.Visible = false;
                cheQueNumber.Visible = false;
                chequeDate.Visible = false;
                this.Text = "[ CARD INVOICE ]";
                comboBox1.SelectedItem = "CARD";
                cardDetailB = true;
                cardAmount.Focus();
            }
        }

        private double amountCost, temp030, temp3060, temp6090, temp90up, a;

        private bool checkload = false, checkPay = false, isCompany = false;

        //   my Method End+++++++++
        private double amountPaid, balance_;

        private void setTempDates(Double aH)
        {
            a = aH;

            if (aH < 30)
            {
                temp030 = temp030 + balance_;
            }
            else if (aH < 60 & a > 30)
            {
                temp3060 = temp3060 + balance_;
            }
            else if (aH < 60 & a > 90)
            {
                temp6090 = temp6090 + balance_;
            }
            else
            {
                temp90up = +temp90up + balance_;
            }
        }

        public void loadOutsPay()
        {
            conn3.Open();
            reader3 = new SqlCommand("SELECT ID,company FROM CUSTOMER", conn3).ExecuteReader();
            while (reader3.Read())
            {
                temp030 = 0;
                temp3060 = 0;
                temp6090 = 0;
                temp90up = 0;

                conn.Open();

                reader = new SqlCommand("select a.invoiceID,a.balance,a.duration,b.subtotal,b.date,b.customerid from creditInvoiceRetail as a,invoiceRetail as b where b.id=a.invoiceId AND b.customerID='" + reader3[0] + "'", conn).ExecuteReader();

                while (reader.Read())
                {
                    //  MessageBox.Show("sa");

                    amountPaid = 0;
                    try
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select paid,amount from invoiceCreditPaid where invoiceID='" + reader[0] + "'", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            amountPaid = amountPaid + (reader2.GetDouble(0));
                        }
                        conn2.Close();
                        //conn2.Open();
                        //reader2 = new SqlCommand("select cheque from chequeInvoiceRetail2 where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                        //while (reader2.Read())
                        //{
                        //    amountPaid = amountPaid + (reader2.GetDouble(0));
                        //}
                        //conn2.Close();
                        conn2.Open();
                        reader2 = new SqlCommand("select cheque from chequeInvoiceRetail where invoiceid='" + reader[0] + "'", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            amountPaid = amountPaid + (reader2.GetDouble(0));
                        }
                        conn2.Close();
                    }
                    catch (Exception a)
                    {
                        //  MessageBox.Show(a.Message+"aq");
                        conn2.Close();
                    }
                    balance_ = reader.GetDouble(1) - amountPaid;
                    //     MessageBox.Show(balance+"");
                    {
                        if (balance_ > 0)
                        {
                            try
                            {
                                amountCost = amountCost + reader.GetDouble(1);

                                setTempDates((DateTime.Now - reader.GetDateTime(4)).TotalDays);

                                //  dt.Rows.Add(reader2[0].ToString().ToUpper(), reader2[0].ToString().ToUpper() + " " + reader2[1].ToString().ToUpper() + " " + reader2[2].ToString().ToUpper(), "R-" + reader[0], db.setAmountFormat(reader[3] + ""), db.setAmountFormat(reader[1] + ""), db.setAmountFormat(amountPaid + ""), db.setAmountFormat(balance + ""), reader.GetDateTime(4).ToShortDateString(), reader[2], a, temp030, temp3060, temp6090, temp90up);
                            }
                            catch (Exception a)
                            {
                                //  MessageBox.Show(balance + "");
                                MessageBox.Show(a.Message + "/ae" + a.StackTrace);
                                conn2.Close();
                            }
                        }
                    }
                }
                conn.Close();

                if (temp90up != 0)
                {
                    combo90.Items.Add(reader3[0] + "-" + reader3.GetString(1) + "_" + db.setAmountFormat(temp90up + ""));
                }
                if (temp6090 != 0)
                {
                    combo60.Items.Add(reader3[0] + "-" + reader3.GetString(1) + "_" + db.setAmountFormat(temp6090 + ""));
                }
                if (temp3060 != 0)
                {
                    combo30.Items.Add(reader3[0] + "-" + reader3.GetString(1) + "_" + db.setAmountFormat(temp3060 + ""));
                }
            }
            conn3.Close();
        }

        private void invoiceNew_Load(object sender, EventArgs e)
        {
            if (user.ToUpper().Equals("RASIKA"))
            {
                panel5.BackColor = Color.Blue;
            }
            else if (user.ToUpper().Equals("HARSHANI"))
            {
                panel5.BackColor = Color.Pink;
            }
            else if (user.ToUpper().Equals("PAVITHRA"))
            {
                panel5.BackColor = Color.LightBlue;
            }
            else if (user.ToUpper().Equals("BASNAYAKE"))
            {
                panel5.BackColor = Color.Green;
            }
            payType.Text = "";
            checkload = true;
            loadInvoiceType();

            invoiceDate = DateTime.Now;
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            serviceByList.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            //  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
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
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView2.Columns.Add(btn);
            btn.Width = 50;
            btn.Text = "OPEN";

            btn.UseColumnTextForButtonValue = true;

            dataGridView2.AllowUserToAddRows = false;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db.createSqlConnection();
            db3 = new DB();
            conn3 = db3.createSqlConnection();
            db4 = new DB();
            conn4 = db4.createSqlConnection();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;

            vehicleNumber.CharacterCasing = CharacterCasing.Upper;

            vehicleDescrition.CharacterCasing = CharacterCasing.Upper;

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
                    panel7.Visible = reader.GetBoolean(5);

                    //vEHICLENUMBERToolStripMenuItem.Visible = reader.GetBoolean(5);

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

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from bank ", conn).ExecuteReader();
                if (reader.Read())
                {
                    comboBank.Items.Add(reader[2] + "-" + reader[1]);
                }
                comboBank.SelectedIndex = 0;
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            try
            {
                serviceBY.Items.Add("");
                conn.Open();
                reader = new SqlCommand("select name from emp where resgin='" + false + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    serviceBY.Items.Add(reader[0] + "");
                }
                comboBank.SelectedIndex = 0;
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
            loadCompany();
            loadAccountList();
            //
            conn.Open();
            reader = new SqlCommand("select distinct(vehicleNO) from vehicle ", conn).ExecuteReader();
            arrayList = new ArrayList();
            while (reader.Read())
            {
                // MessageBox.Show("m");
                arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
            }
            reader.Close();
            idArray = arrayList.ToArray(typeof(string)) as string[];
            db.setAutoComplete(vehicleNumber, idArray);
            db.setAutoComplete(textBox2, idArray);
            conn.Close();
            this.ActiveControl = vehicleNumber;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 80;
            btn.Text = "COMMIS";

            btn.UseColumnTextForButtonValue = true;

            if (user.Equals("rasika"))
            {
                unitPrice.Enabled = true;
                pLToolStripMenuItem.Enabled = true;
            }
            else
            {
                unitPrice.Enabled = false;
                pLToolStripMenuItem.Enabled = false;
            }
            //   loadOutsPay();
            dataGridView1.Height = height - quickPanel.Height - panel5.Height - 50;
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
            new cusomerQuick(this).Visible = true;
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            customer.SelectAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        public void setCoomis(Int32 gridID, string value, string value2)
        {
            dataGridView1.Rows[gridID].Cells[10].Value = value;
            dataGridView1.Rows[gridID].Cells[11].Value = value2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
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

                total.Text = amount + amount2 + "";

                cashPaid.Text = "";
                balance.Text = "0.0";
                rowCount--;
            }
            else if (e.ColumnIndex == 13)
            {
                //if ( !dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Equals("#"))
                //{
                //    new commiesIN(this, user, "", "",e.RowIndex, dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString()).Visible = true;
                //}
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
            new termXash(this, Double.Parse(total.Text), Double.Parse(cashPaid.Text), creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
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
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    code.Focus();
                }
                else
                {
                    loadItem(code.Text);
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
                        unitPrice.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (e.KeyValue == 46 || e.KeyValue == 110)
            {
                code.Text = "";
                unitPrice.Text = "";
                qty.Text = "";
                discount.Text = "";
                listBox1.Visible = false;
                item = false;
                code.Focus();
            }
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                if (loadItemCheck)
                {
                    db.setList(listBox1, code, code.Width * 2);

                    try
                    {
                        listBox1.Items.Clear();
                        listBox1.Visible = true;
                        listBox1.Height = panel2.Height - 30;
                        conn.Open();
                        if (vehicleServiceCheck.Checked)
                        {
                            reader = new SqlCommand("select code,detail,retailprice from item where detail like '%" + code.Text + "%' and id!='" + 0 + "' and isitem='" + false + "' order by id ", conn).ExecuteReader();
                        }
                        else
                        {
                            reader = new SqlCommand("select code,detail,retailprice from item where detail like '%" + code.Text + "%' and id!='" + 0 + "' order by id ", conn).ExecuteReader();
                        }
                        arrayList = new ArrayList();
                        states = true;
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader[1].ToString().ToUpper() + "(" + reader[2] + ")");
                            states = false;
                        }
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        if (vehicleServiceCheck.Checked)
                        {
                            reader = new SqlCommand("select code,detail,retailprice from item where detail like '%" + code.Text + "%' and id='" + 0 + "' and isItem='" + false + "' order by id ", conn).ExecuteReader();
                        }
                        else
                        {
                            reader = new SqlCommand("select code,detail,retailprice from item where detail like '%" + code.Text + "%' and id='" + 0 + "' order by id ", conn).ExecuteReader();
                        }
                        arrayList = new ArrayList();
                        states = true;
                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader[1].ToString().ToUpper() + "(" + reader[2] + ")");
                            states = false;
                        }
                        reader.Close();
                        conn.Close();
                        if (states)
                        {
                            listBox1.Visible = false;
                        }
                    }
                    catch (Exception a)
                    {//
                        // MessageBox.Show(a.Message);
                        conn.Close();
                    }
                }
            }
            if (code.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, qty, qty, e.KeyValue);
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                // discount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
                // discount.Focus();
            }
            else if (e.KeyValue == 38)
            {
                qty.Focus();
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
                discount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                //  addToTable();
                discount.Focus();
            }
            else if (e.KeyValue == 38)
            {
                unitPrice.Focus();
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
                code.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                code.SelectionLength = code.MaxLength;
                loadItem(code.Text);
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            code.SelectionLength = code.MaxLength;
            loadItem(code.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
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
                qty.Focus();
            }
        }

        private void cashPaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (!cashPaid.Text.Equals(""))
            {
                amount = (Double.Parse(cashPaid.Text)) - (Double.Parse(total.Text));

                if (amount <= 0)
                {
                    balance.Text = "0";
                }
                else
                {
                    balance.Text = amount + "";
                }
                if (Double.Parse(cashPaid.Text) >= Double.Parse(total.Text))
                {
                }
                else
                {
                }
            }
            else
            {
                cashPaid.Text = "";
                cashPaid.SelectAll();
            }
        }

        private void updateInvoice()
        {
            try
            {
                invoieNoTemp = invoiceNo.ToString().Split('-')[1].ToString();

                if (discPrestage)
                {
                    amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                }
                else
                {
                    amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));
                }
                amount = Math.Round(amount, 2);
                qtyTemp = Double.Parse(qty.Text);

                if (!item)
                {
                    conn2.Open();
                    new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + code.Text + "','" + qty.Text + "','" + unitPrice.Text + "','" + amount + "','" + 0 + "','" + 0 + "','" + discount.Text + "','" + 0 + "','" + "" + "','" + description + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select qty from item where code='" + code.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.GetDouble(0) < qtyTemp)
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set qty='" + 0 + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        }
                    }

                    reader.Close();
                    reader.Close();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + code.Text + "','" + true + "','" + qtyTemp + "','" + DateTime.Now + "','" + "INVOICE" + "','" + user + "','" + 0 + "')", conn).ExecuteNonQuery();

                    conn.Close();
                    conn2.Open();
                    new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + code.Text + "','" + qty.Text + "','" + unitPrice.Text + "','" + amount + "','" + 0 + "','" + 0 + "','" + discount.Text + "','" + 0 + "','" + "" + "','" + description + "','" + "" + "','" + 0 + "','" + 0 + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                    conn.Open();
                    states = true;
                    reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + code.Text + "' order by date", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        states = false;
                        if (qtyTemp == 0)
                        {
                        }
                        else if (qtyTemp <= reader.GetDouble(1))
                        {
                            var price = reader.GetDouble(0);
                            // reader.Close();

                            conn2.Open();

                            new SqlCommand("update purchasingPriceList set qty=qty-'" + qtyTemp + "' where code='" + code.Text + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                            conn2.Close();

                            qtyTemp = 0;
                        }
                        else
                        {
                            var price = reader.GetDouble(0);

                            qtyTemp = qtyTemp - reader.GetDouble(1);

                            conn2.Open();
                            new SqlCommand("update purchasingPriceList set qty=qty-'" + reader.GetDouble(1) + "' where code='" + code.Text + "' and purchasingprice='" + price + "'", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }
                    }
                    reader.Close();
                    conn.Close();
                }
                if (cutomerID.Equals(""))
                {
                    cutomerID = customer.Text;
                }
                conn.Open();
                new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CASH - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private Int32 idTemp = 0;

        private void getID()
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
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button3_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
            }
            else if (e.KeyValue == 40)
            {
                button3.Focus();
            }
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
            cashPaid.Focus();
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
            db.setTextBoxPath(code, cashPaid, cashPaid, e.KeyValue);
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
            vehicleNumber.Focus();
        }

        private void vehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //  db.setTextBoxPath(vehicleNumber, vehicleDescrition, vehicleDescrition, e.KeyValue);

            if (e.KeyValue == 12 || e.KeyValue == 13 || e.KeyValue == 40)
            {
                if (checkVehicleNo())
                {
                    vehicleDescrition.Focus();
                }
                else
                {
                    MessageBox.Show("Vehicle No is Already Saved for PEnding JOB");
                    vehicleNumber.Focus();
                    vehicleNumber.SelectionLength = vehicleNumber.Text.Length;
                }

                conn2.Open();
                reader2 = new SqlCommand("select customerID from vehicle where vehicleno='" + vehicleNumber.Text + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    loadCustomer(reader2[0] + "");
                }
                conn2.Close();

                vehicleDescrition.Focus();
            }
        }

        private void vehicleDescrition_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vehicleNumber, metreNow, metreNow, e.KeyValue);
        }

        private void metreNow_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vehicleDescrition, metreNext, metreNext, e.KeyValue);
        }

        private bool meterCheck = false;

        private void metreNext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    if (meterCheck)
                    {
                        metreNext.Text = Double.Parse(metreNow.Text) + Double.Parse(metreNext.Text) + "";
                        meterCheck = false;
                    }
                }
                catch (Exception)
                {
                }
                code.Focus();
            }
            else if (e.KeyValue == 38)
            {
                metreNow.Focus();
            }
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
                listBox2.Visible = false;
                if (customer.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    customer.Focus();
                }
                else
                {
                    address.Focus();
                    // loadCustomer(customer.Text);
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox2.Visible)
                    {
                        listBox2.Focus();
                        listBox2.SelectedIndex = 0;
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
                db.setList(listBox2, customer, customer.Width);
                listBox2.Visible = true;

                listBox2.Height = 55;
                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,company,mobileNo from customer where description like '%" + customer.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();
                    states = true;
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper() + " " + reader[2]);
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states || checkload)
                    {
                        checkload = false;
                        listBox2.Visible = false;
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
                listBox2.Visible = false;
            }
        }

        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, mobileNumber, mobileNumber, e.KeyValue);
        }

        private void mobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //db.setTextBoxPath(address, code, code, e.KeyValue);
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                button10_Click(sender, e);
                //  loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
            }
            else if (e.KeyValue == 38)
            {
                address.Focus();
            }
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
            this.Text = vehicleNumber.Text;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var cashDetailB = true;

            if (!creditDetailB & !chequeDetailB & !cardDetailB)
            {
                cashDetailB = true;
                var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);
            }
            else
            {
                cashDetailB = false;
                var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);
            }
            if ((MessageBox.Show("Invoice Succefully Generated , Do You want to Print it", "Confirmation",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                var aa = "";
                if (cashDetailB)
                {
                    aa = "CASH MEMO";
                }
                else
                {
                    aa = "CREDIT MEMO";
                }
                if (balance.Text.Equals("0") || Double.Parse(balance.Text) == 0)
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, Double.Parse(total.Text) - Double.Parse(cashPaid.Text) + "", DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                }
                else
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                }
            }
        }

        private void radioCredit_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkOF_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cASHPAIDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cashPaid.Focus();
        }

        private bool tempCheck;

        private bool checkVehicleNo()
        {
            try
            {
                tempCheck = true;
                conn.Open();
                reader = new SqlCommand("select * from   vehiclePending where  vehicleno='" + vehicleNumber.Text + "' and date='" + DateTime.Now + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    tempCheck = false;
                }
                conn.Close();
            }
            catch (Exception)
            {
                tempCheck = false;
                conn.Close();
            }

            return tempCheck;
        }

        private void cashPaid_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (cashPaid.Text.Equals(""))
            {
                cashPaid.Text = "0";
            }
            if (creditAmount.Text.Equals(""))
            {
                creditAmount.Text = "0";
            }
            if (chequeAmount.Text.Equals(""))
            {
                chequeAmount.Text = "0";
            }
            if (cardAmount.Text.Equals(""))
            {
                cardAmount.Text = "0";
            }
            if (!checkPay)
            {
                cashPaid.Text = "";
                MessageBox.Show("Plesae Select Paymnet Method");
                cashPaid.Focus();
            }
            else
            {
                try
                {
                    if (cashPaid.Text.Equals(""))
                    {
                    }
                    else

                        if (Double.Parse(cashPaid.Text) == 0)
                    {
                        // MessageBox.Show("1");

                        // MessageBox.Show("2");
                        chequeDetailB = false;
                        cardDetailB = false;
                        creditDetailB = false;
                        cardDetail = null;
                        chequeDetail = null;
                        creditAmount.Text = "0";
                        cashPaid.SelectionLength = cashPaid.TextLength;
                        chequeAmount.Text = "0";
                        cardAmount.Text = "0";
                        balance.Text = "0";
                        balance.Text = Double.Parse(total.Text) * -1 + "";
                        // MessageBox.Show("3");
                    }
                    else
                    {
                        // MessageBox.Show("3");
                        var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);
                        balance.Text = (a * -1) + "";
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void chequeAmount_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (chequeAmount.Text.Equals(""))
                {
                    chequeAmount.Text = "0";
                    chequeAmount.SelectionLength = chequeAmount.TextLength;
                }

                var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);

                if (a < 0)
                {
                    chequeAmount.Text = "0";

                    chequeAmount.SelectionLength = chequeAmount.TextLength;
                    balance.Text = Math.Round(Double.Parse(total.Text) - (Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2) + "";
                }
                else
                {
                    balance.Text = (a * -1) + "";
                }

                if (Double.Parse(chequeAmount.Text) != 0)
                {
                    chequeDetailB = true;
                }
                else
                {
                    chequeDetailB = false;
                    chequeDetail = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void cardAmount_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cashPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cashPaid_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                total.Focus();
                total.SelectionLength = total.TextLength;
            }
            else if (e.KeyValue == 40)
            {
                if (invoiceTypeDB.Equals("CHEQUE"))
                {
                    chequeAmount.Focus();
                    chequeAmount.Focus();
                    chequeAmount.SelectionLength = creditAmount.TextLength;
                }
                else if (invoiceTypeDB.Equals("CREDIT"))
                {
                    creditAmount.Focus();
                    creditAmount.Focus();
                    creditAmount.SelectionLength = creditAmount.TextLength;
                }
                else if (invoiceTypeDB.Equals("CARD"))
                {
                    cardAmount.Focus();
                    cardAmount.Focus();
                    cardAmount.SelectionLength = creditAmount.TextLength;
                }
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                if (invoiceTypeDB.Equals("CHEQUE"))
                {
                    chequeAmount.Focus();
                    chequeAmount.Focus();
                    chequeAmount.SelectionLength = creditAmount.TextLength;
                }
                else if (invoiceTypeDB.Equals("CREDIT"))
                {
                    creditAmount.Focus();
                    creditAmount.Focus();
                    creditAmount.SelectionLength = creditAmount.TextLength;
                }
                else if (invoiceTypeDB.Equals("CARD"))
                {
                    cardAmount.Focus();
                    cardAmount.Focus();
                    cardAmount.SelectionLength = creditAmount.TextLength;
                }
                else
                {
                    //if (checkBalance())
                    //{
                    //}
                    //else
                    //{
                    //    creditAmount.Focus();
                    //    creditAmount.SelectionLength = creditAmount.TextLength;
                    //}
                    customer.Focus();
                }
            }
        }

        private bool checkBalance()
        {
            try
            {
                check = true;
                var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);
                if (a <= 0)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            catch (Exception)
            {
                check = false;
            }
            return check;
        }

        private void creditAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                cashPaid.Focus();
                cashPaid.SelectionLength = cashPaid.TextLength;
            }
            else if (e.KeyValue == 40)
            {
                creditPeriod.Focus();
                creditPeriod.SelectionLength = chequeAmount.TextLength;
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                creditPeriod.Focus();
                creditPeriod.SelectionLength = chequeAmount.TextLength;
            }
        }

        private void creditAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (cashPaid.Text.Equals(""))
            {
                cashPaid.Text = "0";
            }
            if (creditAmount.Text.Equals(""))
            {
                creditAmount.Text = "0";
            }
            if (chequeAmount.Text.Equals(""))
            {
                chequeAmount.Text = "0";
            }
            if (cardAmount.Text.Equals(""))
            {
                cardAmount.Text = "0";
            }
            try
            {
                if (creditAmount.Text.Equals(""))
                {
                    creditAmount.Text = "0";
                    creditAmount.SelectionLength = creditAmount.TextLength;
                }

                var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);

                if (a < 0)
                {
                    creditAmount.Text = "0";

                    creditAmount.SelectionLength = creditAmount.TextLength;
                    balance.Text = Math.Round(Double.Parse(total.Text) - (Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2) + "";
                }
                else
                {
                    balance.Text = (a * -1) + "";
                }

                if (Double.Parse(creditAmount.Text) != 0)
                {
                    creditDetailB = true;
                }
                else
                {
                    creditDetailB = false;
                    creditDetail = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void chequeAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                creditAmount.Focus();
                creditAmount.SelectionLength = creditAmount.TextLength;
            }
            else if (e.KeyValue == 40)
            {
                if (invoiceTypeDB.Equals("CHEQUE"))
                {
                    bankName.Focus();
                }
                else
                {
                    cardAmount.Focus();
                    cardAmount.SelectionLength = cardAmount.TextLength;
                }
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                if (invoiceTypeDB.Equals("CHEQUE"))
                {
                    bankName.Focus();
                }
                else
                {
                    if (checkBalance())
                    {
                        button3_Click(sender, e);
                    }
                    else
                    {
                        cardAmount.Focus();
                        cardAmount.SelectionLength = cardAmount.TextLength;
                    }
                }
            }
        }

        private void cardAmount_ImeModeChanged(object sender, EventArgs e)
        {
        }

        private void cardAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                chequeAmount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                //  button3_Click(sender, e);
                cardAmount2.Focus();
            }
        }

        private void bankName_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(chequeAmount, cheQueNumber, cheQueNumber, e.KeyValue);
        }

        private void cheQueNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                bankName.Focus();
            }
            else if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                chequeDate.Focus();
            }
        }

        private void chequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                cheQueNumber.Focus();
            }
            else if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                // button3_Click(sender, e);
                customer.Focus();
            }
        }

        private void creditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                creditAmount.Focus();
            }
            else if (e.KeyValue == 40 || e.KeyValue == 12 || e.KeyValue == 13)
            {
                // button3_Click(sender, e);
                customer.Focus();
            }
        }

        private void balance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                cardAmount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                button3_Click(sender, e);
                // cardAmount2.Focus();
            }
        }

        private void cardAmount2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                cardAmount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                // button3_Click(sender, e);
                customer.Focus();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //if (invoiceTypeDB.Equals("CASH") || invoiceTypeDB.Equals("CARD"))
            //{
            //    new invoiceSearchCash(this, user).Visible = true;
            //}
            //else if (invoiceTypeDB.Equals("CREDIT"))
            //{
            //    new invoiceSearchCredit(this, user).Visible = true;
            //}
            //else if (invoiceTypeDB.Equals("CHEQUE"))
            //{
            //    new invoiceSearchCh(this, user).Visible = true;
            //}

            new invoiceSearchCash(this, user).Visible = true;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            invoiceTypeDB = comboBox1.SelectedItem.ToString();
            loadInvoiceType();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void cASHBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear();
            checkPay = true;
            invoiceTypeDB = "CASH";
            loadInvoiceType();
        }

        private void cHEQUEBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear();
            checkPay = true;
            invoiceTypeDB = "CHEQUE";
            loadInvoiceType();
        }

        private void cREDITBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear();
            checkPay = true;
            invoiceTypeDB = "CREDIT";
            loadInvoiceType();
        }

        private void cARDBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear();
            checkPay = true;
            invoiceTypeDB = "CARD";
            loadInvoiceType();
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
            new customerProfile2(this, user).Visible = true;
        }

        private void pRICELISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //clear();
            invoiceTypeDB = "CASH";
            loadInvoiceType();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // clear();
            invoiceTypeDB = "CHEQUE";
            loadInvoiceType();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //clear();
            invoiceTypeDB = "CREDIT";
            loadInvoiceType();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // clear();
            invoiceTypeDB = "CARD";
            loadInvoiceType();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new ExpensesNormal(this, user).Visible = true;
        }

        private void clearIN()
        {
            try
            {
                {
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("update item set qty=qty+'" + reader[1] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    reader.Close();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from customerStatement where reason='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from fullservice where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from cardInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from creditInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from chequeInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from cashInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from companyInvoice where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from vehicle where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from invoiceTerm where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close(); conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from invoiceRetail where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from invoiceRetaildetail where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from sale where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from incomeAccountStatement where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete   from cashSummery  where  remark='" + "Invoice No-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
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
                invoiceDate = DateTime.Now;
                if (cashPaid.Text.Equals(""))
                {
                    cashPaid.Text = "0";
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry Emprt Data for Generate Invoice");
                    code.Focus();
                }
                else if (!checkTerm())
                {
                    MessageBox.Show("Please Enter Pay Detail on Term Section Before Genarate Invoice");
                    cashPaid.Focus();
                    cashPaid.Text = "";
                }
                else if (!checkUser())
                {
                    MessageBox.Show("Please Enter a Registerd Customer for a Credit Invoice");
                    customer.Focus();
                }
                else if (MessageBox.Show("Generate Invoice? ", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    db.setCursoerWait();
                    try
                    {
                        if (invoiceNo.ToString().Equals(""))
                        {
                            loadInvoiceNoRetail();
                            if (!string.IsNullOrWhiteSpace(mobileNumber.Text.Trim()))
                            {
                                if (invoiceTypeDB != "CREDIT")
                                {
                                    string Message = "Thank you for choosing Car West Auto Service. Your total bill amount is Rs." + total.Text.Trim() + " Thank you, Come Again.";
                                    SMSSender.SendWebRequest(mobileNumber.Text.Trim(), Message);
                                }
                                else
                                {
                                    string Message = "Thank you for choosing Car West Auto Service. Your pending outstanding balance amount is Rs." + total.Text.Trim() + " Please pay your due as soon as possible";
                                    SMSSender.SendWebRequest(mobileNumber.Text.Trim(), Message);
                                }
                            }
                        }
                        invoieNoTemp = invoiceNo.ToString().Split('-')[1].ToString();
                        clearIN();
                        conn.Open();
                        new SqlCommand("delete from itemStatement where invoiceid= '" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from fullService where invoiceID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into fullservice values('" + invoieNoTemp + "','" + checkDF.Checked + "','" + checkOF.Checked + "','" + checkEO.Checked + "','" + checkGO.Checked + "','" + checkAF.Checked + "','" + checkGreesen.Checked + "','" + sendPeriod.Text + "','" + messege.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from invoiceRetailDetail where invoiceID='" + invoieNoTemp + "' and pc='" + false + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cashSummery where reason='" + "New Invoice" + "' and remark='" + "Invoice No-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        amount = 0;
                        profit = 0;
                        profitTotal = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount += double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                            //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        }

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            qtyTemp = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                            if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                            {
                                conn2.Open();
                                new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[10].Value + "','" + dataGridView1.Rows[i].Cells[11].Value + "')", conn2).ExecuteNonQuery();
                                conn2.Close();
                            }
                            else
                            {
                                conn.Open();
                                reader = new SqlCommand("select qty from item where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                }

                                reader.Close();
                                reader.Close();
                                conn.Close();

                                conn.Open();
                                new SqlCommand("insert into itemStatement values('" + "R-" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + true + "','" + qtyTemp + "','" + DateTime.Now + "','" + "INVOICE" + "','" + user + "','" + 0 + "')", conn).ExecuteNonQuery();

                                conn.Close();
                                conn2.Open();
                                new SqlCommand("insert into invoiceRetailDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[10].Value + "','" + dataGridView1.Rows[i].Cells[11].Value + "')", conn2).ExecuteNonQuery();
                                conn2.Close();
                            }
                        }

                        //conn.Open();
                        //reader = new SqlCommand("select * from customer where id='" + tempCustomer + "'", conn).ExecuteReader();
                        //if (reader.Read())
                        //{
                        //    // MessageBox.Show(tempCustomer);
                        //    conn.Close();
                        //    conn.Open();
                        //    new SqlCommand("update customer set address='" + address.Text + "',mobileNo='" + mobileNumber.Text + "', company='" + customer.Text + "' where id='" + tempCustomer + "'", conn).ExecuteNonQuery();
                        //    conn.Close();
                        //}
                        //else
                        //{
                        //    conn.Close();
                        //    if (!customer.Text.Equals("[CASH CUSTOMER]"))
                        //    {
                        //        getID();
                        //        conn.Open();
                        //        new SqlCommand("insert into customer values ('" + "C-" + idTemp + "','" + "" + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "','" + "" + "','" + customer.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                        //        conn.Close();
                        //        tempCustomer = "C-" + idTemp;
                        //        cutomerID = "C-" + idTemp;
                        //    }

                        //}
                        String[] a;
                        String inv = "R-" + invoieNoTemp;

                        if (cutomerID.Equals(""))
                        {
                            cutomerID = customer.Text;
                        }

                        var cashDetailB = true;
                        conn.Open();
                        new SqlCommand("insert into invoiceRetail values('" + invoieNoTemp + "','" + "" + "','" + checkBoxView.Checked + "','" + 0 + "','" + DateTime.Now + "','" + true + "','" + "" + "','" + DateTime.Now + "','" + 0 + "','" + 0 + "','" + 0 + "','" + user + "','" + 0 + "','" + 0 + "','" + serviceBY.SelectedItem + "')", conn).ExecuteNonQuery();
                        conn.Close();

                        if (!creditDetailB & !chequeDetailB & !cardDetailB)
                        {
                            cashDetailB = true;
                            var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);

                            conn.Open();
                            new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CASH - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into cashSummery values('" + "New Invoice" + "','" + "Invoice No-" + invoieNoTemp + "','" + total.Text + "','" + invoiceDate + "','" + user + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into cashBalance values('" + user + "','" + "Invoice R-" + invoieNoTemp + "','" + false + "','" + total.Text + "','" + invoiceDate + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            cashDetailB = false;
                            var amountD = Double.Parse(cashPaid.Text) - Double.Parse(balance.Text);

                            conn.Open();
                            new SqlCommand("update invoiceRetail set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CREDIT - " + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("insert into cashSummery values('" + "New Invoice" + "','" + "Invoice No-" + invoieNoTemp + "','" + amountD + "','" + invoiceDate + "','" + user + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into cashBalance values('" + user + "','" + "Invoice R-" + invoieNoTemp + "','" + false + "','" + amountD + "','" + invoiceDate + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }

                        conn.Open();
                        new SqlCommand("delete from companyInvoice where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("delete from serviceBy where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        for (int i = 0; i < serviceByList.Rows.Count; i++)
                        {
                            conn.Open();
                            new SqlCommand("insert into serviceBy values('" + invoieNoTemp + "','" + serviceByList.Rows[i].Cells[0].Value + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        conn.Open();
                        new SqlCommand("delete from sale where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from incomeAccountStatement where invoiceID='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("delete from bankAccountStatment where number='" + "R-" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into vehicle values('" + invoieNoTemp + "','" + vehicleNumber.Text + "','" + vehicleDescrition.Text + "','" + metreNow.Text + "','" + metreNext.Text + "','" + cutomerID + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("delete from invoiceTerm where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cashInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from cardInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from creditInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from chequeInvoiceRetail where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from chequeSummery where bank='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        //    MessageBox.Show("");
                        conn.Open();
                        new SqlCommand("insert into invoiceTerm values('" + invoieNoTemp + "','" + cashDetailB + "','" + creditDetailB + "','" + chequeDetailB + "','" + cardDetailB + "','" + user + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Invoice Amount" + "','" + total.Text + "','" + 0 + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        if (Double.Parse(cashPaid.Text) != 0)
                        {
                            if (Double.Parse(cashPaid.Text) > Double.Parse(total.Text))
                            {
                                conn.Open();
                                new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cash Payment of Invoice" + "','" + 0 + "','" + total.Text + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                conn.Open();
                                new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cash Payment of Invoice" + "','" + 0 + "','" + cashPaid.Text + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                        }

                        if (cashDetailB)
                        {
                            conn.Open();
                            new SqlCommand("insert into cashInvoiceRetail values('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        if (creditDetailB)
                        {
                            conn.Open();
                            new SqlCommand("insert into creditInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + 0 + "','" + creditAmount.Text + "','" + creditPeriod.Text + "','" + DateTime.Now + "','" + DateTime.Now.AddDays(Int32.Parse(creditPeriod.Text)) + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        if (chequeDetailB)
                        {
                            tempChequeAmoun = chequeAmount.Text;

                            tempChequeNo = cheQueNumber.Text;
                            count++;
                            tempChequeCodeNo = bankName.Text;
                            count++;
                            tempChequeDate = chequeDate.Value.ToShortDateString();
                            count++;
                            tempChequeId = "";
                            count++;
                            //conn.Open();
                            //new SqlCommand("insert into creditInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + 0 + "','" + DateTime.Now + "','" + DateTime.Now.AddDays(0) + "')", conn).ExecuteNonQuery();
                            //conn.Close();
                            conn.Open();
                            new SqlCommand("insert into chequeInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeDate + "','" + DateTime.Now + "','" + tempChequeCodeNo + "','" + tempChequeId + "','" + comboBank.SelectedItem.ToString().Split('-')[0] + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                            {
                                try
                                {
                                    conn.Open();
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + comboSaleAccount.Text.ToString().Split('.')[1].ToString() + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                catch (Exception)
                                {
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                            conn.Open();
                            new SqlCommand("insert into chequeSummery values('" + invoieNoTemp + "','" + "" + "','" + "" + "','" + true + "','" + false + "','" + false + "','" + chequeAmount.Text + "','" + comboBank.SelectedItem.ToString().Split('-')[0] + "','" + cheQueNumber.Text + "','" + chequeDate.Value + "','" + false + "','" + true + "','" + cutomerID + "','" + cutomerID + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "')", conn).ExecuteNonQuery();
                            conn.Close();

                            conn.Open();
                            new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cheque for Balance Amount of Invoice" + "','" + 0 + "','" + tempChequeAmoun + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        if (cardDetailB)
                        {
                            tempChequeAmoun = chequeAmount.Text;

                            tempChequeNo = cheQueNumber.Text;
                            count++;
                            tempChequeCodeNo = bankName.Text;
                            count++;
                            tempChequeDate = chequeDate.Value.ToShortDateString();
                            count++;
                            tempChequeId = "";
                            count++;
                            conn.Open();
                            new SqlCommand("insert into cardInvoiceRetail values ('" + invoieNoTemp + "','" + cutomerID + "','" + total.Text + "','" + DateTime.Now + "','" + cardAmount.Text + "','" + 0 + "','" + cardAmount2.Text + "','" + 0 + "','" + 0 + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                            {
                                try
                                {
                                    conn.Open();
                                    new SqlCommand("insert into bankAccountStatment values('" + comboCardPayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Card Payment :Card No-" + cardAmount2.Text + "','" + false + "','" + false + "','" + DateTime.Now + "','" + cardAmount.Text + "','" + comboSaleAccount.Text.ToString().Split('.')[1].ToString() + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                catch (Exception)
                                {
                                    // new SqlCommand("insert into bankAccountStatment values('" + comboCardPayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + "R-" + invoieNoTemp + "','" + "Invoice-Pay" + "','" + cutomerID + "','" + "Card Payment :Card No-" + cardAmount2.Text + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                            }

                            conn.Open();
                            new SqlCommand("insert into customerStatement values('" + "R-" + invoieNoTemp + "','" + "Cheque for Balance Amount of Invoice" + "','" + 0 + "','" + tempChequeAmoun + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }

                        if ((MessageBox.Show("Invoice Succefully Generated , Do You want to Print it", "Confirmation",
              MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                        {
                            var aa = "";
                            if (cashDetailB)
                            {
                                aa = "CASH MEMO";
                            }
                            else
                            {
                                aa = "CREDIT MEMO";
                            }
                            if (balance.Text.Equals("0") || Double.Parse(balance.Text) == 0)
                            {
                                new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, Double.Parse(total.Text) - Double.Parse(cashPaid.Text) + "", DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                            }
                            else
                            {
                                new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                            }

                            // conn.Close();
                            //  a.Visible = true;
                        }
                        //++++++++++++++++++++Tax Invoice Start

                        conn.Open();
                        reader = new SqlCommand("select id from invoicedump where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            conn.Close();

                            conn.Open();
                            new SqlCommand("update invoiceDump set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        //++++++++++++++++++++Tax Inoice End
                        //  clear();
                        conn.Open();
                        new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("delete from pending where id='" + pendingNo + "' ", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from pendingDetail where invoiceid='" + pendingNo + "' ", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from vehiclePending where invoiceid='" + pendingNo + "' ", conn).ExecuteNonQuery();
                        conn.Close(); clear();
                        db.setCashBalance(DateTime.Now);
                        // MessageBox.Show("1");
                        try
                        {
                            dataGridView2.Rows.Clear();
                            conn.Open();
                            reader = new SqlCommand("select a.id,b.vehicleno,b.descrip from pending as a,vehiclePending as b where a.date='" + DateTime.Now + "' and a.id=b.invoiceID ", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dataGridView2.Rows.Add(reader[0], reader[1] + " " + reader[2]);
                            }
                            conn.Close();
                        }
                        catch (Exception aaa)
                        {
                            MessageBox.Show(aaa.Message);
                            conn.Close();
                        }
                        // MessageBox.Show("1");

                        invoiceTypeDB = "CASH";
                        //loadInvoiceType();
                        panel3.Visible = true;
                        creditPeriod.Visible = false;
                        label10.Visible = false;
                        label41.Visible = false;
                        creditAmount.Visible = false;
                        label39.Visible = false;
                        chequeAmount.Visible = false;
                        label38.Visible = false;
                        cardAmount.Visible = false;
                        label3.Visible = false;
                        cardAmount2.Visible = false;
                        label1.Visible = false;
                        label4.Visible = false;
                        label9.Visible = false;
                        bankName.Visible = false;
                        cheQueNumber.Visible = false;
                        chequeDate.Visible = false;
                        this.Text = "[ CASH INVOICE ]";
                        comboBox1.SelectedItem = "CASH";
                        //  cashPaid.Focus();
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
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!invoiceNo.ToString().Equals(""))
            {
                MessageBox.Show("Sorry , You Have No Permission to Edit Past Invoice");
            }
            else
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Sorry Emprt Data for Generate Pending Job");
                    code.Focus();
                }
                else if (vehicleNumber.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Vehicle Number");
                    vehicleNumber.Focus();
                }
                else if (LOADcHECK)
                {
                }
                else if ((MessageBox.Show("Generate Job ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.setCursoerWait();
                    try
                    {
                        if (invoieNoTemp.Equals(""))
                        {
                            loadInvoiceNoPending();
                            invoieNoTemp = invoiceNo.ToString().Split('-')[1].ToString();
                        }

                        // invoiceNo = "R-16985";

                        // invoieNoTemp = "14182";

                        //+++++Intial OLD INVOice++++
                        conn.Open();
                        new SqlCommand("delete from pending where id='" + invoieNoTemp + "' ", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from pendingDetail where invoiceID='" + invoieNoTemp + "' and pc='" + false + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from vehiclePending where invoiceid='" + invoieNoTemp + "' ", conn).ExecuteNonQuery();
                        conn.Close();

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                            conn2.Open();
                            new SqlCommand("insert into pendingDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + "" + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "','" + 0 + "','" + dataGridView1.Rows[i].Cells[10].Value + "','" + dataGridView1.Rows[i].Cells[11].Value + "')", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }

                        String[] a;
                        String inv = "R-" + invoieNoTemp;

                        if (cutomerID.Equals(""))
                        {
                            cutomerID = customer.Text;
                        }

                        var cashDetailB = true;
                        conn.Open();
                        new SqlCommand("insert into pending values('" + invoieNoTemp + "','" + "" + "','" + true + "','" + 0 + "','" + DateTime.Now + "','" + true + "','" + "" + "','" + DateTime.Now + "','" + 0 + "','" + 0 + "','" + 0 + "','" + user + "','" + 0 + "','" + 0 + "','" + serviceBY.SelectedItem + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update pending set customerID='" + cutomerID + "',subTotal='" + total.Text + "',profit='" + profitTotal + "',cash='" + cashPaid.Text + "',balance='" + balance.Text + "',netTotal='" + total.Text + "',discount='" + "0" + "',paytype='" + "CASH - " + "',pono='" + serviceBY.SelectedItem + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into vehiclePending values('" + invoieNoTemp + "','" + vehicleNumber.Text + "','" + vehicleDescrition.Text + "','" + metreNow.Text + "','" + metreNext.Text + "','" + cutomerID + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("delete from serviceBy where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        for (int i = 0; i < serviceByList.Rows.Count; i++)
                        {
                            conn.Open();
                            new SqlCommand("insert into serviceBy values('" + invoieNoTemp + "','" + serviceByList.Rows[i].Cells[0].Value + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        MessageBox.Show("Done");

                        clear();
                        invoiceTypeDB = "CASH";
                        loadInvoiceType();
                        checkPay = true;
                        try
                        {
                            dataGridView2.Rows.Clear();
                            conn.Open();
                            reader = new SqlCommand("select a.id,b.vehicleno,b.descrip from pending as a,vehiclePending as b where a.date='" + DateTime.Now + "' and a.id=b.invoiceID ", conn).ExecuteReader();
                            while (reader.Read())
                            {
                                dataGridView2.Rows.Add(reader[0], reader[1] + " " + reader[2]);
                            }
                            conn.Close();
                        }
                        catch (Exception aaa)
                        {
                            MessageBox.Show(aaa.Message);
                            conn.Close();
                        }
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
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select a.id,b.vehicleno,b.descrip from pending as a,vehiclePending as b where a.date='" + DateTime.Now + "' and a.id=b.invoiceID ", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(reader[0], reader[1] + " " + reader[2]);
                }
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                loadPending(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void cREATEPENDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button20_Click(null, null);
        }

        private void cRETAEINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button10_Click(null, null);
        }

        private void cLEARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear();
            invoiceTypeDB = "CASH";
            loadInvoiceType();
            try
            {
                dataGridView2.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select a.id,b.vehicleno,b.descrip from pending as a,vehiclePending as b where a.date='" + DateTime.Now + "' and a.id=b.invoiceID ", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(reader[0], reader[1] + " " + reader[2]);
                }
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            clear();
            invoiceTypeDB = "CASH";
            loadInvoiceType();
            try
            {
                dataGridView2.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select a.id,b.vehicleno,b.descrip from pending as a,vehiclePending as b where a.date='" + DateTime.Now + "' and a.id=b.invoiceID ", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(reader[0], reader[1] + " " + reader[2]);
                }
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
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
            vehicleNumber.Focus();
        }

        private void sEARCHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click_2(null, null);
        }

        private bool LOADcHECK = false;

        public void loadInvoice(string id)
        {
            try
            {
                invoiceNo = "R-" + id.ToString();
                invoieNoTemp = id.ToString();
                dataGridView1.Rows.Clear();
                //    MessageBox.Show("1");
                db.setCursoerWait();
                invoiceTypeDB = "CASH";
                {
                    conn3.Open();
                    reader3 = new SqlCommand("select * from invoiceRetail where id='" + invoieNoTemp + "'", conn3).ExecuteReader();
                    if (reader3.Read())
                    {
                        LOADcHECK = true;
                        customer.Text = reader3[1] + "";

                        total.Text = reader3[9] + "";
                        this.Text = id + "  (" + reader3.GetDateTime(4).ToShortDateString() + " " + reader3.GetTimeSpan(7) + ")";
                        var ter = reader3[6] + "";

                        cashPaid.Text = reader3[12] + "";
                        balance.Text = reader3[13] + "";
                        cashPaidDB = reader3.GetDouble(12);
                        invoiceDate = reader3.GetDateTime(4);
                        reader3.Close();
                        conn3.Close();
                        conn3.Open();
                        reader3 = new SqlCommand("select * from fullservice where invoiceid='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            checkDF.Checked = reader3.GetBoolean(2);
                            checkOF.Checked = reader3.GetBoolean(3);
                            checkEO.Checked = reader3.GetBoolean(4);
                            checkGO.Checked = reader3.GetBoolean(5);
                            checkAF.Checked = reader3.GetBoolean(6);
                            checkGreesen.Checked = reader3.GetBoolean(7);
                            sendPeriod.Text = reader3[8] + "";
                            messege.Text = reader3[9] + "";
                        }
                        conn3.Close();
                        loadCustomer(customer.Text);
                        conn4.Open();
                        reader4 = new SqlCommand("select a.vatAmount,a.nbtAmount,b.isTax,b.taxPre,b.isNbt,b.nbtPre from companyInvoice as a,company as b  where a.id='" + invoieNoTemp + "' and  a.companyID=b.id", conn4).ExecuteReader();
                        if (reader4.Read())
                        {
                            isTax = reader4.GetBoolean(2);
                            taxpre = reader4.GetDouble(3);
                            isNBT = reader4.GetBoolean(4);
                            nbtpre = reader4.GetDouble(5);

                            // taxLabel.Text = taxpre+"";
                            //nbtLabel.Text = nbtpre + "";
                        }
                        conn4.Close();

                        conn4.Open();

                        reader4 = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();
                        if (reader4.Read())
                        {
                            creditAmount.Text = reader4[4] + "";
                            creditPeriod.Text = reader4[5] + "";
                            paidAmount = paidAmount + reader4.GetDouble(4);

                            invoiceTypeDB = "CREDIT";
                        }
                        reader4.Close();
                        conn4.Close();

                        conn4.Open();
                        reader4 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();

                        while (reader4.Read())
                        {
                            chequeDetailB = true;
                            bankName.Text = reader4[8] + "";
                            cheQueNumber.Text = reader4[5] + "";
                            chequeDate.Value = reader4.GetDateTime(6);
                            chequeAmount.Text = reader4[4] + "";
                            paidAmount = paidAmount + reader4.GetDouble(4);
                            for (int i = 0; i < comboBank.Items.Count; i++)
                            {
                                if (reader4.GetString(10).Equals(comboBox1.Items[i].ToString().Split('-')[0].ToString()))
                                {
                                    comboBox1.SelectedIndex = i;
                                }
                                else
                                {
                                    comboBox1.SelectedIndex = -1;
                                }
                            }
                            invoiceTypeDB = "CHEQUE";
                        }
                        reader4.Close();
                        conn4.Close();

                        conn4.Open();
                        reader4 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + invoieNoTemp + "' ", conn4).ExecuteReader();

                        while (reader4.Read())
                        {
                            cardDetailB = true;
                            cardAmount.Text = reader4[5] + "";
                            invoiceTypeDB = "CARD";
                            paidAmount = paidAmount + reader4.GetDouble(5);
                        }

                        reader4.Close();
                        conn4.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from invoiceRetailDetail where invoiceId='" + invoieNoTemp + "' and pc='" + false + "'", conn3).ExecuteReader();
                        Int32 count = 0;
                        rowCount = 0;
                        while (reader3.Read())
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount, reader3[1], reader3[10], reader3[3], reader3[7], reader3[2], reader3[4], "FALSE", reader3[11], reader3[12], reader3[13]);
                        }
                        conn3.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from vehicle where invoiceId='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        if (reader3.Read())
                        {
                            vehicleNumber.Text = reader3[1] + "";
                            vehicleDescrition.Text = reader3[2] + "";
                            metreNow.Text = reader3[3] + "";
                            metreNext.Text = reader3[4] + "";
                            this.Text = vehicleNumber.Text;
                        }
                        conn3.Close();

                        conn3.Open();
                        reader3 = new SqlCommand("select * from serviceBy where invoiceId='" + invoieNoTemp + "'", conn3).ExecuteReader();
                        while (reader3.Read())
                        {
                            serviceByList.Rows.Add(reader3[1]);
                        }
                        conn3.Close();

                        loadInvoiceType();
                    }
                    else
                    {
                        MessageBox.Show("Invoice not Loading Correctlly");
                    }
                    code.Focus();
                    conn3.Close();
                }

                db.setCursoerDefault();
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied Invoice ID " + a.Message + " //" + a.StackTrace);
                conn3.Close();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            loadInvoice(textBox1.Text);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                loadInvoice(textBox1.Text);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new chequeList(this, user).Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new customerProfile2(this, user).Visible = true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new invoiceNewPURCH(this, user, "CASH", "").Visible = true;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("SELECT invoiceID from chequeInvoiceRetail where chequenumber='" + cheQueNumber.Text + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    var a = reader[0] + "";
                    conn.Close();
                    loadInvoice(a);
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            new invoiceSearchCard(this, user).Visible = true;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            new invoiceSearchCredit(this, user).Visible = true;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            new invoiceSearchAll(this, user).Visible = true;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            new invoiceSearchCh(this, user).Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new cashBook(this, user).Visible = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            new invoiceSearch(this, user).Visible = true;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(" Do You want to Print it", "Confirmation",
         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
         MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                bool cashDetailB;
                if (!creditDetailB & !chequeDetailB & !cardDetailB)
                {
                    cashDetailB = true;
                }
                else
                {
                    cashDetailB = false;
                }

                string aa = string.Empty;
                if (cashDetailB)
                {
                    aa = "CASH MEMO";
                }
                else
                {
                    aa = "CREDIT MEMO";
                }
                if (balance.Text.Equals("0") || double.Parse(balance.Text) == 0)
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, Double.Parse(total.Text) - Double.Parse(cashPaid.Text) + "", DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                }
                else
                {
                    new invoicePrint().setprintHalfInvoiceService("RA/" + DateTime.Now.Year + DateTime.Now.Month + "R-" + invoieNoTemp, cutomerID, aa, dataGridView1, total.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user, vehicleNumber.Text, vehicleDescrition.Text, metreNow.Text, metreNext.Text);
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            checkPay = true;
            invoiceTypeDB = "CASH";
            loadInvoiceType();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            checkPay = true;
            invoiceTypeDB = "CHEQUE";
            loadInvoiceType();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            checkPay = true;
            invoiceTypeDB = "CREDIT";
            loadInvoiceType();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            checkPay = true;
            invoiceTypeDB = "CARD";
            loadInvoiceType();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button31_Click(object sender, EventArgs e)
        {
            new invoiceCreditPay(this, user).Visible = true;
        }

        private void checkAF_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button32_Click(object sender, EventArgs e)
        {
            new invoiceSearchCashv(this, user, textBox2.Text).Visible = true;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                new invoiceSearchCashv(this, user, textBox2.Text).Visible = true;
            }
        }

        private void cardAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (cardAmount.Text.Equals(""))
                {
                    cardAmount.Text = "0";
                    cardAmount.SelectionLength = cardAmount.TextLength;
                }
                var a = Math.Round(Double.Parse(total.Text) - (Double.Parse(creditAmount.Text) + Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2);

                if (a < 0)
                {
                    cardAmount.Text = "0";

                    cardAmount.SelectionLength = cardAmount.TextLength;
                    balance.Text = Math.Round(Double.Parse(total.Text) - (Double.Parse(chequeAmount.Text) + Double.Parse(cardAmount.Text) + Double.Parse(cashPaid.Text)), 2) + "";
                }
                else
                {
                    balance.Text = (a * -1) + "";
                }

                if (Double.Parse(cardAmount.Text) != 0)
                {
                    cardDetailB = true;
                }
                else
                {
                    cardDetailB = false;
                    cardDetail = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new ChequeView().Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            new grnCreditPay_(this, user).Visible = true;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            new stockSummmery(this, user).Visible = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            new stockReport(this, user).Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            new qut(this, "", "").Visible = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            checkStockOD();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            new returnGood(this, user, "", "").Visible = true;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            new commies(this, user, "", "").Visible = true;
        }

        private void bARCODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new cashOut(this, user).Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new salary(user).Visible = true;
        }

        private void cUSTOMEROUTSTANDINGBALANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new cusBalance(this, user, "CASH", "").Visible = true;
        }

        private void aTTENDANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new attendance().Visible = true;
        }

        private void ePFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new cFormView().Visible = true;
        }

        private void eTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new etfView().Visible = true;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            new fullService(this, "").Visible = true;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            new stockReport4(this, "").Visible = true;
        }

        private void sETqTYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                new SqlCommand("update item set qty='" + 0 + "' ", conn).ExecuteNonQuery();
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select credit,qty,itemCode from itemStatement ", conn).ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean(0))
                    {
                        conn2.Open();
                        new SqlCommand("update item set qty=qty-'" + reader.GetDouble(1) + "' where code='" + reader[2] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    else
                    {
                        conn2.Open();
                        new SqlCommand("update item set qty=qty+'" + reader.GetDouble(1) + "' where code='" + reader[2] + "'", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                }
                conn.Close();
                db.setCursoerDefault();
                MessageBox.Show("Complted");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            conn.Open();
            reader = new SqlCommand("select * from itemTemp_", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select * from item where code='" + reader[0] + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    conn2.Close();
                    conn2.Open();
                    new SqlCommand("update item set description='" + reader[3] + "',categorey='" + reader[1] + "',brand='" + reader[2] + "',qty='" + reader[5] + "',detail='" + reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                else
                {
                    conn2.Close();
                    conn2.Open();
                    new SqlCommand("insert into item values ('" + reader[0] + "','" + reader[2] + "','" + reader[1] + "','" + reader[3] + "','" + reader[4] + "','" + "" + "','" + reader[6] + "','" + reader[7] + "','" + 0 + "','" + reader[5] + "','" + "" + "','" + reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn2.Close();
                try
                {
                    conn2.Open();
                    new SqlCommand("insert into itemStatement values('" + 0 + "','" + reader[0] + "','" + true + "','" + Double.Parse(reader[5] + "") + "','" + DateTime.Now + "','" + "BF" + "','" + "" + "','" + 0 + "')", conn2).ExecuteNonQuery();

                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }
            }
            conn.Close();
            MessageBox.Show("ok");
        }

        private void button38_Click_1(object sender, EventArgs e)
        {
            db.setCursoerWait();
            conn.Open();
            reader = new SqlCommand("select * from itemfeed", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select * from item where code='" + reader[0] + "'", conn2).ExecuteReader();
                if (!reader2.Read())
                {
                    conn2.Close();
                    conn2.Open();
                    new SqlCommand("insert into item values ('" + reader[0] + "','" + "JS" + "','" + "FUEL IN TANK FILTER" + "','" + reader[1] + "','" + "" + "','" + "" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + reader[0] + " JS FUEL IN TANK FILTER " + reader[1] + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn2.Close();
            }
            conn.Close();
            MessageBox.Show("Saved");
        }

        private void button38_Click_2(object sender, EventArgs e)
        {
            button38_Click(null, null);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            button38_Click(null, null);
        }

        private void button38_Click_3(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select retailPrice,code from item where brand='" + "sakura" + "' AND RETAILPRICE!='" + 0 + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    var a = (reader.GetDouble(0) / 100) * 75;
                    conn2.Open();
                    new SqlCommand("update item set purchasingPrice='" + a + "' where code='" + reader[1] + "'", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn.Close();
                MessageBox.Show("done");
            }
            catch (Exception)
            {
                // throw;
            }
        }

        private void iNVESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invest(this, user).Visible = true;
        }

        private void button38_Click_4(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from item WHERE BRAND='" + "JS" + "' AND categorey='" + "air filter" + "'", conn).ExecuteReader();
                while (reader.Read())
                {
                    //  var a = (reader.GetDouble(0) / 100) * 75;
                    conn2.Open();
                    new SqlCommand("update item set detail='" + reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn.Close();
                MessageBox.Show("done");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button38_Click_5(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from item ", conn).ExecuteReader();
                while (reader.Read())
                {
                    //  var a = (reader.GetDouble(0) / 100) * 75;
                    conn2.Open();
                    new SqlCommand("update item set detail='" + reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + "' where code='" + reader[0] + "'", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn.Close();
                MessageBox.Show("done");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button38_Click_6(object sender, EventArgs e)
        {
            conn.Open();
            reader = new SqlCommand("select company from customer", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select * from customer where company='" + reader[0] + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    if (reader2.Read())
                    {
                        MessageBox.Show(reader2[2] + "" + reader2[0]);
                    }
                }
                conn2.Close();
            }
            conn.Close();
        }

        private void button38_Click_7(object sender, EventArgs e)
        {
            string set = "C-" + vehicleDescrition.Text, get = "C-" + vehicleNumber.Text;

            conn.Open();
            new SqlCommand("update invoiceRetail set customerID='" + set + "' where customerID='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update vehicle set customerID='" + set + "' where customerID='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();

            conn.Open();
            new SqlCommand("update customerStatement set customerID='" + set + "' where customerID='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update cashInvoiceRetail set cutomerID='" + set + "' where cutomerID='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update chequeInvoiceRetail set cutomerId='" + set + "' where cutomerId='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update chequeSummery set customerID='" + set + "' where customerID='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update chequeInvoiceRetail2 set cutomerId='" + set + "' where cutomerId='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();

            conn.Open();
            new SqlCommand("update receipt set customer='" + set + "' where customer='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update chequeInvoiceRetail2 set cutomerId='" + set + "' where cutomerId='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            conn.Open();
            new SqlCommand("update chequeInvoiceRetail2 set cutomerId='" + set + "' where cutomerId='" + get + "'", conn).ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("ok");
            vehicleNumber.Text = "";
            vehicleDescrition.Text = "";
        }

        private void sALESUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new saleSummery(this, user).Visible = true;
        }

        private void pLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new profitandLost(this, user).Visible = true;
        }

        private void eXPENSESACCOUNTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Expenses(this, user).Visible = true;
        }

        private void dEPRECIATIONACCOUNTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Expenses_(this, user).Visible = true;
        }

        private void aDDDEPRECIATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExpensesNormal_(this, user).Visible = true;
        }

        private void aDDNONCURRENTASSETSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new nonCurrentAssets(this, user).Visible = true;
        }

        private void button28_Click_1(object sender, EventArgs e)
        {
            db.setCursoerWait();
            conn.Open();
            reader = new SqlCommand("select itemCode,id from itemStatement", conn).ExecuteReader();
            while (reader.Read())
            {
                conn2.Open();
                reader2 = new SqlCommand("select purchasingPrice from item where code='" + reader[0] + "'", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    conn3.Open();
                    new SqlCommand("update itemStatement set purchsingPrice='" + reader2[0] + "' where id='" + reader[1] + "'", conn3).ExecuteNonQuery();
                    conn3.Close();
                }
                conn2.Close();
            }
            conn.Close();
            db.setCursoerDefault();
            MessageBox.Show("ok");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                if (passwordText.Text.Equals("ra1471sika"))
                {
                    unitPrice.Enabled = true;
                }
            }
        }

        private void button28_Click_2(object sender, EventArgs e)
        {
            new stockReport5(this, user).Visible = true;
        }

        private void pLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new profitandLost(this, user).Visible = true;
        }

        private void tRILBALANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new trialBalance(this, user).Visible = true;
        }

        private void cASHSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new cashSummery(this, user).Visible = true;
        }

        private void button38_Click_8(object sender, EventArgs e)
        {
        }

        private void button38_Click_9(object sender, EventArgs e)
        {
            new LoanAdvanced().Visible = true;
        }

        private void chequeAmount_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (cashPaid.Text.Equals(""))
            {
                cashPaid.Text = "0";
            }
            if (creditAmount.Text.Equals(""))
            {
                creditAmount.Text = "0";
            }
            if (chequeAmount.Text.Equals(""))
            {
                chequeAmount.Text = "0";
            }
            if (cardAmount.Text.Equals(""))
            {
                cardAmount.Text = "0";
            }
        }

        private void button39_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (serviceByList.Rows.Count == 0)
                {
                    serviceByList.Rows.Add(serviceBY.SelectedItem.ToString());
                }
                else
                {
                    var chek = false;
                    for (int i = 0; i < serviceByList.Rows.Count; i++)
                    {
                        if (serviceByList.Rows[i].Cells[0].Value.ToString().Equals(serviceBY.SelectedItem.ToString()))
                        {
                            check = true;
                        }
                    }
                    if (!check)
                    {
                        serviceByList.Rows.Add(serviceBY.SelectedItem.ToString());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            new invoiceCreditPay(this, user).Visible = true;
        }

        private void button47_Click(object sender, EventArgs e)
        {
            new invoiceSearch(this, user).Visible = true;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            new returnGood(this, user, "", "").Visible = true;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            new invoiceNewPURCH(this, user, "CASH", "").Visible = true;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            new ExpensesNormal(this, user).Visible = true;
        }

        private void button56_Click(object sender, EventArgs e)
        {
            new grnCreditPay_(this, user).Visible = true;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            new customerProfile2(this, user).Visible = true;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            new stockReport(this, user).Visible = true;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            new stockReport4(this, "").Visible = true;
        }

        private void button50_Click(object sender, EventArgs e)
        {
            new qut(this, "", "").Visible = true;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            new stockReport5(this, user).Visible = true;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            new chequeList(this, user).Visible = true;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            new ChequeView().Visible = true;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            new salary(user).Visible = true;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            new LoanAdvanced().Visible = true;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            new cashBook(this, user).Visible = true;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            new stockSummmery(this, user).Visible = true;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            new commies(this, user, "", "").Visible = true;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            conn.Open();
            reader = new SqlCommand("select * from a_tempCustomer", conn).ExecuteReader();
            while (reader.Read())
            {
                var tempcusID = 0;
                try
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select max(auto) from customer", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        tempcusID = reader2.GetInt32(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }
                tempcusID++;
                conn2.Open();
                new SqlCommand("insert into customer values ('" + "C-" + tempcusID + "','" + "" + "','" + reader[0] + "','" + "" + "','" + reader[1] + "','" + "" + "','" + reader[0] + " " + reader[1] + "','" + "" + "','" + "" + "','" + false + "','" + false + "','" + false + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "')", conn2).ExecuteNonQuery();
                conn2.Close();
                var tempInvoiceID = 0;

                try
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select max(id) from invoiceretail", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        tempInvoiceID = reader2.GetInt32(0);
                    }
                    conn2.Close();
                }
                catch (Exception)
                {
                    conn2.Close();
                }
                tempInvoiceID++;
                conn2.Open();
                new SqlCommand("insert into invoiceRetail values('" + tempInvoiceID + "','" + "C-" + tempcusID + "','" + checkBoxView.Checked + "','" + reader[2] + "','" + DateTime.Now + "','" + true + "','" + "CREDIT" + "','" + DateTime.Now + "','" + 0 + "','" + reader[2] + "','" + 0 + "','" + user + "','" + 0 + "','" + 0 + "','" + serviceBY.SelectedItem + "')", conn2).ExecuteNonQuery();
                conn2.Close();

                conn2.Open();
                new SqlCommand("insert into creditInvoiceRetail values ('" + tempInvoiceID + "','" + "C-" + tempcusID + "','" + reader[2] + "','" + 0 + "','" + reader[2] + "','" + 0 + "','" + DateTime.Now + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();
                conn2.Close();
            }
            conn.Close();
            MessageBox.Show("ok");
        }
    }
}