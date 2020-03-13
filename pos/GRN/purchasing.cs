using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;

namespace pos
{
    public partial class purchasing : Form
    {
        public purchasing(Form form, String user, string id)
        {
            InitializeComponent();
            home = form;
            this.user = user;
            invoiceino = id;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private ArrayList arrayList, stockList, detaiArrayList;

        public string[] creditDetail, chequeDetail, cardDetail;
        public Boolean check, checkListBox, states, item, checkStock, termCheck, creditDetailB, chequeDetailB, cardDetailB;
        private string user, listBoxType, cutomerID = "", invoiceino, description, invoieNoTemp, tempId;
        private String[] idArray;
        private string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;

        private DataGridViewButtonColumn btn;
        private Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice, count;
        private Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        private Boolean loadItemCheck = false, discPrestage;
        public double cashPaid;
        public Double paidAmount, cashPaidDB;
        // my Variable End
        //my Method Start++++++

        public void loadInvoice(string id)
        {
            try
            {
                invoieNoTemp = id.ToString();
                //    MessageBox.Show("1");
                db.setCursoerWait();

                conn.Open();
                reader = new SqlCommand("select * from GRN where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    customer.Text = reader[2] + "";

                    subTotal.Text = reader[3] + "";
                    dateTimePicker1.Value = reader.GetDateTime(4);
                    grnNo.Text = reader[1] + "";
                    this.Text = id + "  (" + reader.GetDateTime(4) + ")";
                    var ter = reader[5] + "";
                    cashPaidDB = reader.GetDouble(7);
                    cashPaid = cashPaidDB;
                    reader.Close();
                    conn.Close();
                    loadCustomer(customer.Text);
                    conn2.Open();

                    reader2 = new SqlCommand("select * from creditGRN where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
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
                    reader2 = new SqlCommand("select * from chequeGRN where grnID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
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
                    reader2 = new SqlCommand("select * from cardGRN where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
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
                    setTermBack(true);
                    conn.Open();

                    reader = new SqlCommand("select * from grnDetail where grnID='" + invoieNoTemp + "' and type='" + false + "'", conn).ExecuteReader();
                    Int32 count = 0;
                    while (reader.Read())
                    {
                        rowCount++;
                        dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[8], reader[2], reader[3], reader[10], reader[5], reader[6], "FALSE");
                    }
                    conn.Close();

                    conn.Open();

                    //conn.Open();

                    reader = new SqlCommand("select * from grnDetail where grnID='" + invoieNoTemp + "' and type='" + true + "'", conn).ExecuteReader();

                    while (reader.Read())
                    {
                        rowCount++;
                        dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[8], reader[2], reader[3], reader[10], reader[5], reader[6], "TRUE");
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                    }
                    conn.Close();

                    conn.Open();
                    reader = new SqlCommand("select * from companyGRN where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        discountTotal.Text = reader[7] + "";
                        nbtNew.Text = reader.GetDouble(5) + "";
                        vat.Text = Math.Round(((reader.GetDouble(6) - reader.GetDouble(7) + reader.GetDouble(5)) / 100) * reader.GetDouble(3), 2) + "";
                        vatPre.Text = reader[3] + "";

                        subTotal.Text = reader[6] + "";
                        payble.Text = Math.Round((reader.GetDouble(6) - reader.GetDouble(7)) + (Double.Parse(nbtNew.Text) + Double.Parse(vat.Text)), 2) + "";
                    }
                    else
                    {
                        discountTotal.Text = "0";
                        nbtNew.Text = "0";
                        vat.Text = "0";
                        vatPre.Text = "0";
                        payble.Text = subTotal.Text;
                    }
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("GRN not Loading Correctlly");

                    this.Dispose();
                    home.Enabled = true;
                    home.TopMost = true;
                }
                code.Focus();
                conn.Close();

                db.setCursoerDefault();
                termCheck = false;
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied GRN ID " + a.Message + " //" + a.StackTrace);
                conn.Close();
                termCheck = false;
            }
        }

        private void setDisk()
        {
            try
            {
                var sub = Double.Parse(subTotal.Text);
                if (!discountTotal.Text.Equals(""))
                {
                    if (Double.Parse(discountTotal.Text) > (sub))
                    {
                        discountTotal.Focus();
                        discountTotal.Text = "0";

                        discountTotal.SelectAll();
                    }

                    sub = sub - (Double.Parse(discountTotal.Text));
                }
                else
                {
                    discountTotal.Text = "0";

                    discountTotal.SelectAll();
                }
                sub = sub + Double.Parse(nbtNew.Text);
                //    MessageBox.Show(sub+"");
                vat.Text = Math.Round((sub / 100) * Double.Parse(vatPre.Text), 2) + "";

                payble.Text = Math.Round(sub + (Double.Parse(vat.Text)), 2) + "";
            }
            catch (Exception)
            {
                discountTotal.Text = "0";
                nbtNew.Text = "0";
                vat.Text = "0";
                nbtNew.Text = "0";
                vatPre.Text = "0";
                payble.Text = subTotal.Text;
            }
        }

        private string tempCustomer;

        public Boolean loadCustomer(string id)
        {
            //MessageBox.Show(id);
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    customer.Text = id;
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

        private void loadInvoiceNoRetail()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from grn", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceino = invoiceMaxNo + "";
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                // throw;
                invoiceino = "1";
                reader.Close();
                conn.Close();
            }
        }

        private void clear()
        {
            listBox1.Visible = false;

            listBox2.Visible = false;
            payble.Text = "0";
            address.Text = "";
            mobileNumber.Text = "";
            customer.Text = "[CASH SUPPLIER]";
            term.Text = "CASH";
            cutomerID = "";
            grnNo.Text = "";
            dataGridView1.Rows.Clear();
            // loadInvoiceNoRetail();
            dateTimePicker1.Value = DateTime.Now;
            if (discPrestage)
            {
                comboDiscount.SelectedIndex = 0;
            }
            else
            {
                comboDiscount.SelectedIndex = 1;
            }
            clearSub();
            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;
            mobileNumber.Text = "";
            address.Text = "";
            cashPaid = 0;
            rowCount = 0;
        }

        private void clearSub()
        {
            sellingPrice.Text = "0";
            tempDesc.Text = "";
            unitPrice.Text = "0.0";
            warrentyCode.Text = "";
            qty.Text = "";
            discount.Text = "0";
            code.Text = "";
            code.Focus();
        }

        private void loadItem(string codeValue)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,detail from item where code='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;
                    code.Text = codeValue + "";
                    //availebleQty.Text = reader.GetDouble(0) + "";
                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {
                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }
                    //  minRetailPrice.Text = db.setAmountFormat(reader.GetDouble(2) + "");
                    //  maxRetailPrice.Text = db.setAmountFormat(reader.GetDouble(3) + "");
                    unitPrice.Text = reader.GetDouble(3) + "";
                    sellingPrice.Text = reader.GetDouble(3) + "";
                    tempDesc.Text = reader.GetString(4).ToUpper();
                    unitPrice.Focus();
                    sellingPrice.SelectionLength = sellingPrice.TextLength;
                    conn.Close();
                }
                else
                {
                    // var code=itemc
                    //  MessageBox.Show("Invalied Item Codea");
                    item = false;
                    clearSub();
                    code.Text = codeValue;
                    sellingPrice.Focus();
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
                //MessageBox.Show(a.Message);
            }
        }

        private void addToTable()
        {
            try
            {
                // MessageBox.Show(item+"");

                if (item)
                {
                    if (qty.Text.Equals("") || Double.Parse(qty.Text) <= 0)
                    {
                        MessageBox.Show("Sorry Stock not Available on this Item to GRN ");
                        qty.Focus();
                    }
                    else if (unitPrice.Text.Equals(""))
                    {
                        MessageBox.Show("Sorry Unit Price Cannot be Emprty Or Zero");
                        unitPrice.Focus();
                    }
                    else
                    {
                        if (dataGridView1.Rows.Count == 0)
                        {
                            rowCount++;

                            amount = ((Double.Parse(unitPrice.Text)) * (Double.Parse(qty.Text)));
                            amount = Math.Round(amount, 2);
                            dataGridView1.Rows.Add(rowCount + "", code.Text, warrentyCode.Text, description, Math.Round(Double.Parse(unitPrice.Text), 2), sellingPrice.Text, discount.Text, qty.Text, amount, "FALSE");

                            //amount = (Double.Parse(subTotal.Text));
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
                                                qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()) + Double.Parse(qty.Text);

                                                amountTemp = ((Double.Parse(unitPrice.Text)) * qtyTemp);
                                                amountTemp = Math.Round(amountTemp, 2);
                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[2].Value = warrentyCode.Text;
                                                dataGridView1.Rows[i].Cells[3].Value = description;
                                                dataGridView1.Rows[i].Cells[4].Value = Math.Round(Double.Parse(unitPrice.Text), 2);
                                                dataGridView1.Rows[i].Cells[6].Value = discount.Text;
                                                dataGridView1.Rows[i].Cells[7].Value = qtyTemp;

                                                dataGridView1.Rows[i].Cells[8].Value = amountTemp;
                                                dataGridView1.Rows[i].Cells[5].Value = sellingPrice.Text;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rowCount++;

                                amount = ((Double.Parse(unitPrice.Text)) * (Double.Parse(qty.Text)));
                                amount = Math.Round(amount, 2);
                                dataGridView1.Rows.Add(rowCount + "", code.Text, warrentyCode.Text, description, Math.Round(Double.Parse(unitPrice.Text), 2), sellingPrice.Text, discount.Text, qty.Text, amount, "FALSE");

                                amount = amount + (Double.Parse(subTotal.Text));

                                var y = dataGridView1.RowCount;
                                y--;
                                dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.Azure;
                            }
                        }

                        amount = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                        }
                        subTotal.Text = amount + "";

                        clearSub();
                        code.Focus();
                    }
                }
                else
                {
                    rowCount++;

                    amount = ((Double.Parse(unitPrice.Text)) * (Double.Parse(qty.Text)));
                    amount = Math.Round(amount, 2);
                    dataGridView1.Rows.Add(rowCount + "", "#", warrentyCode.Text, code.Text, Math.Round(Double.Parse(unitPrice.Text), 2), sellingPrice.Text, discount.Text, qty.Text, amount, "FALSE");

                    amount = amount + (Double.Parse(subTotal.Text));

                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;

                    amount = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                    }
                    subTotal.Text = amount + "";

                    clearSub();
                    code.Focus();
                }

                setDisk();
            }
            catch (Exception s)
            {
                MessageBox.Show(s.StackTrace);
            }
        }

        private double purch;

        public void updateTableItem(string unitPrice, string discount, string qty, Int32 index)
        {
            if (discPrestage)
            {
                purch = (Double.Parse(unitPrice) / 100) * (100 - Double.Parse(discount));
            }
            else
            {
                purch = Double.Parse(unitPrice) - Double.Parse(discount);
            }
            amountTemp = purch * Double.Parse(qty);
            amountTemp = Math.Round(amountTemp, 2);
            //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);

            dataGridView1.Rows[index].Cells[4].Value = Math.Round(purch, 2);

            dataGridView1.Rows[index].Cells[5].Value = unitPrice;
            dataGridView1.Rows[index].Cells[6].Value = discount;
            dataGridView1.Rows[index].Cells[7].Value = qty;

            dataGridView1.Rows[index].Cells[8].Value = (amountTemp + "");

            amount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            subTotal.Text = amount + "";
            setDisk();
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where loaduser='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    nEWSUPPLIERToolStripMenuItem.Enabled = reader.GetBoolean(9);
                    button3.Enabled = reader.GetBoolean(17);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        //   my Method End+++++++++loasas
        private void invoiceNew_Load(object sender, EventArgs e)
        {
            termCheck = true;
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            this.ControlBox = false;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            quickPanel.Width = width;
            dataGridView1.Width = width - 370;
            dataGridView1.Height = height - (380);
            //  button1.BackColor = Color.Transparent;
            dataGridView1.Columns[3].Width = dataGridView1.Width - 615;
            Point p = new Point();
            p = panel1.Location;
            p.X = quickPanel.Width - panel1.Width - 35;
            panel1.Location = p;
            panel2.Width = dataGridView1.Width;
            //   dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            p = panel3.Location;
            p.X = panel2.Width - panel3.Width;
            panel3.Location = p;

            p.X = width - panel4.Width - 30;
            p.Y = height - panel4.Height - 60;
            panel4.Location = p;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 60;
            btn.Text = "REMOVE";

            btn.UseColumnTextForButtonValue = true;
            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.Width = 40;
            btn.Text = "EDIT";

            btn.UseColumnTextForButtonValue = true;

            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;
            grnNo.CharacterCasing = CharacterCasing.Upper;
            if (!invoiceino.Equals(""))
            {
                loadInvoice(invoiceino);
            }

            loadUser();
            //  MessageBox.Show("sas");
            // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {
                    discPrestage = reader.GetBoolean(4);
                    loadItemCheck = reader.GetBoolean(9);
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
            // this.toolTip1.SetToolTip(button1, "ADD SUPPLIER");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new supplierQuick2(this).Visible = true;
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
                if (dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString().Equals("TRUE"))
                {
                    MessageBox.Show("Sorry Return Item cant Edit");
                }
                else
                {
                    if (e.ColumnIndex == 10)
                    {
                        //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        var sub = 0.0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sub = sub + Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                            dataGridView1.Rows[i].Cells[0].Value = ++i;
                            i--;
                        }
                        subTotal.Text = sub + "";
                        setDisk();
                    }
                    else if (e.ColumnIndex == 11)
                    {
                        // new itemTable5(this, dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString(), e.RowIndex + "", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;
                    }
                }
            }
            catch (Exception)
            {
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
            new termXash5(this, Double.Parse(payble.Text), cashPaid, creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grnNo.Focus();
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
                        sellingPrice.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                if (loadItemCheck)
                {
                    db.setList(listBox1, code, code.Width * 2);
                    listBox1.Visible = true;
                    listBox1.Height = panel2.Height - 30;
                    try
                    {
                        listBox1.Items.Clear();
                        conn.Open();
                        reader = new SqlCommand("select code,detail from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
                        arrayList = new ArrayList();
                        states = true;
                        while (reader.Read())
                        {
                            states = false;
                            listBox1.Items.Add(reader[1].ToString().ToUpper());
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
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    if (!discount.Text.Equals("") & !sellingPrice.Text.Equals(""))
                    {
                        if (discPrestage)
                        {
                            unitPrice.Text = (Double.Parse(sellingPrice.Text) / 100) * (100 - Double.Parse(discount.Text)) + "";
                        }
                        else
                        {
                            unitPrice.Text = Double.Parse(sellingPrice.Text) - Double.Parse(discount.Text) + "";
                        }
                        qty.Focus();
                    }
                    else
                    {
                        discount.Text = "0";
                        sellingPrice.Text = "0";
                        unitPrice.Focus();
                    }
                }
                catch (Exception)
                {
                    discount.Text = "0";
                    sellingPrice.Text = "0";
                    unitPrice.Focus();
                }
            }
            else if (e.KeyValue == 38)
            {
                sellingPrice.Focus();
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
                warrentyCode.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
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
        }

        private Double amountR;

        private Boolean checkTerm()
        {
            //  MessageBox.Show(creditDetailB+"");
            amountR = 0;
            if (creditDetailB)
            {
                amountR = amountR + Double.Parse(creditDetail[0].ToString());
            }
            if (chequeDetailB)
            {
                count = 0;
                for (int i = 0; i < (chequeDetail.Length) / 5; i++)
                {
                    amountR = amountR + Double.Parse(chequeDetail[count].ToString());
                    count++;
                    count++;
                    count++;
                    count++;
                    count++;
                }
            }
            if (cardDetailB)
            {
                count = 0;
                for (int i = 0; i < (cardDetail.Length) / 4; i++)
                {
                    amountR = amountR + Double.Parse(cardDetail[count].ToString());
                    count++;
                    count++;
                    count++;
                    count++;
                }
            }

            MessageBox.Show(creditDetailB + "/" + chequeDetailB + "/" + cardDetailB + "/" + amountR);
            if (!creditDetailB && !cardDetailB && !chequeDetailB)
            {
                amountR = (Double.Parse(payble.Text));
            }
            states = true;
            if (amountR != Double.Parse(payble.Text))
            {
                states = false;
            }

            return states;
        }

        private Int32 tempCount;
        private double tempPrice;
        private int idTemp;

        private void getID2()
        {
            try
            {
                conn2.Open();
                reader2 = new SqlCommand("select max(auto) from supplier", conn2).ExecuteReader();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Emprt Data for Generate GRN");
                code.Focus();
            }
            else if (!checkTerm())
            {
                MessageBox.Show("Please Enter Pay Detail on Term Section Before Genarate Invoice");
            }
            else if ((MessageBox.Show("Generate GRN ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                    if (invoiceino.Equals(""))
                    {
                        loadInvoiceNoRetail();
                    }

                    invoieNoTemp = invoiceino.ToString();

                    //+++++Intial OLD INVOice++++
                    conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceid= '" + invoieNoTemp + "'", conn).ExecuteNonQuery();

                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from grnDetail where grnID='" + invoieNoTemp + "' and type='" + false + "'", conn).ExecuteReader();
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
                    new SqlCommand("delete from grnDetail where grnID='" + invoieNoTemp + "' and type='" + false + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from warrentyGRN where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    ///+++++++++++++++++++++++++
                    amount = 0;
                    profit = 0;
                    profitTotal = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                        //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    }

                    //  MessageBox.Show("1");
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[9].Value.ToString().Equals("FALSE"))
                            {
                                qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                                {
                                    conn2.Open();
                                    new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[8].Value + "','" + false + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "') ", conn2).ExecuteNonQuery();
                                    conn2.Close();
                                }
                                else
                                {
                                    conn.Open();
                                    new SqlCommand("insert into itemStatement values('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + false + "','" + qtyTemp + "','" + DateTime.Now + "','" + "GRN" + "','" + user + "')", conn).ExecuteNonQuery();

                                    conn.Close();
                                    conn.Open();

                                    new SqlCommand("update item set qty=qty+'" + qtyTemp + "',retailPrice='" + dataGridView1.Rows[i].Cells[5].Value + "',billingPrice='" + dataGridView1.Rows[i].Cells[5].Value + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();

                                    conn.Close();
                                    conn.Open();
                                    reader = new SqlCommand("select * from itemSUb where code='" + dataGridView1.Rows[i].Cells[1].Value + "'", conn).ExecuteReader();
                                    if (reader.Read())
                                    {
                                        tempCount = reader.GetInt32(1);
                                        tempPrice = Math.Round((Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) / tempCount), 2);
                                        conn.Close();
                                    }
                                    conn.Close();
                                    conn.Open();
                                    new SqlCommand("update itemSub set Tprice='" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "',uPrice='" + tempCount + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();

                                    conn.Close();
                                    conn.Close();

                                    conn.Open();
                                    states = true;
                                    reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn).ExecuteReader();
                                    if (reader.Read())
                                    {
                                        conn2.Open();

                                        new SqlCommand("update purchasingPriceList set qty=qty+'" + qtyTemp + "',minRetailPrice='" + dataGridView1.Rows[i].Cells[5].Value + "',maxRetailPrice='" + dataGridView1.Rows[i].Cells[5].Value + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                    }
                                    else
                                    {
                                        conn2.Open();
                                        new SqlCommand("insert into purchasingPriceList values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + qtyTemp + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                    }
                                    reader.Close();
                                    conn.Close();
                                    if (states)
                                    {
                                        // MessageBox.Show(qtyTemp+"");
                                        conn2.Open();
                                        new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[8].Value + "','" + false + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "')", conn2).ExecuteNonQuery();
                                        conn2.Close();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception na)
                    {
                        MessageBox.Show(na.Message + "00000000 " + na.StackTrace);
                    }
                    ///  MessageBox.Show("2");
                    String[] a;
                    String inv = invoieNoTemp;
                    conn.Open();
                    reader = new SqlCommand("select * from supplier where id='" + tempCustomer + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update supplier set address='" + address.Text + "',mobileNo='" + mobileNumber.Text + "',company='" + customer.Text + "' where id='" + tempCustomer + "'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        getID2();
                        conn.Open();
                        new SqlCommand("insert into supplier values ('" + "S-" + idTemp + "','" + "" + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "','" + "" + "','" + customer.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        tempCustomer = "S-" + idTemp;
                        cutomerID = "S-" + idTemp;
                    }
                    conn.Close();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        a = dataGridView1.Rows[i].Cells[2].Value.ToString().Split('-');

                        if (a.Length == 1)
                        {
                            a = dataGridView1.Rows[i].Cells[2].Value.ToString().Split('/');

                            if (a.Length == 1)
                            {
                                conn.Open();

                                new SqlCommand("insert into warrentyGRN values('" + inv + "','" + a[0] + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                for (int z = 0; z < a.Length; z++)
                                {
                                    conn.Open();

                                    new SqlCommand("insert into warrentyGRN values('" + inv + "','" + a[z] + "')", conn).ExecuteNonQuery();

                                    conn.Close();
                                }
                            }
                        }
                        else
                        {
                            Int32 b = Int32.Parse(a[0]);
                            Int32 c = Int32.Parse(a[a.Length - 1]);
                            for (int y = b; y <= c; y++)
                            {
                                conn.Open();

                                new SqlCommand("insert into warrentyGRN values('" + inv + "','" + b + "')", conn).ExecuteNonQuery();
                                b++;
                                conn.Close();
                            }
                        }
                    }
                    if (cutomerID.Equals(""))
                    {
                        cutomerID = customer.Text;
                    }
                    conn.Open();
                    new SqlCommand("delete from companyGRN where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into companyGRN values('" + invoieNoTemp + "','" + "" + "','" + true + "','" + vatPre.Text + "','" + true + "','" + nbtNew.Text + "','" + subTotal.Text + "','" + discountTotal.Text + "','" + DateTime.Now.ToShortDateString() + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    var cashDetailB = true;
                    conn.Open();
                    reader = new SqlCommand("select * from grn where id='" + invoieNoTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();

                        if (!creditDetailB & !chequeDetailB & !cardDetailB)
                        {
                            cashDetailB = true;
                            conn.Open();
                            new SqlCommand("update grn set supplierID='" + cutomerID + "',subTotal='" + payble.Text + "',idSupplier='" + grnNo.Text + "',userid='" + user + "',date='" + dateTimePicker1.Value + "',cash='" + payble.Text + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            cashDetailB = false;
                            conn.Open();
                            new SqlCommand("update grn set supplierID='" + cutomerID + "',subTotal='" + payble.Text + "',idSupplier='" + grnNo.Text + "',userid='" + user + "',date='" + dateTimePicker1.Value + "',cash='" + cashPaid + "' where id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    else
                    {
                        conn.Close();

                        if (!creditDetailB & !chequeDetailB & !cardDetailB)
                        {
                            cashDetailB = true;
                            conn.Open();
                            new SqlCommand("insert into grn values('" + invoieNoTemp + "','" + grnNo.Text + "','" + customer.Text + "','" + payble.Text + "','" + dateTimePicker1.Value + "','" + "CASH- " + "','" + user + "','" + payble.Text + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            cashDetailB = false;
                            conn.Open();
                            new SqlCommand("insert into grn values('" + invoieNoTemp + "','" + grnNo.Text + "','" + customer.Text + "','" + payble.Text + "','" + dateTimePicker1.Value + "','" + "CREDIT- " + "','" + user + "','" + cashPaid + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    conn.Close();

                    String invoiceNo = invoieNoTemp;
                    conn.Open();
                    new SqlCommand("delete from cardGrn where invoiceid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from creditGrn where invoiceid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from chequeGrn where grnid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from cashGrn where grnid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    if (!creditDetailB & !chequeDetailB & !cardDetailB)
                    {
                        cashDetailB = true;
                    }
                    else
                    {
                        cashDetailB = false;
                    }
                    conn.Open();
                    new SqlCommand("delete from grnTerm where invoiceid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into grnTerm values('" + invoieNoTemp + "','" + cashDetailB + "','" + creditDetailB + "','" + chequeDetailB + "','" + cardDetailB + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    //  MessageBox.Show("Invoice Succefully Generated and Queue to Print....");
                    conn.Open();
                    new SqlCommand("insert into supplierStatement values('" + invoieNoTemp + "','" + "GRN Amount" + "','" + 0 + "','" + payble.Text + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("insert into supplierStatement values('" + invoieNoTemp + "','" + "Cash Payment of GRN" + "','" + payble.Text + "','" + 0 + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    if (cashDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into cashGrn values('" + invoieNoTemp + "','" + cutomerID + "','" + payble.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }

                    if (creditDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into creditGrn values ('" + invoieNoTemp + "','" + cutomerID + "','" + payble.Text + "','" + 0 + "','" + creditDetail[0] + "','" + creditDetail[1] + "','" + DateTime.Now + "','" + DateTime.Now.AddDays(Int32.Parse(creditDetail[1])) + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    if (chequeDetailB)
                    {
                        count = 0;
                        for (int i = 0; i < (chequeDetail.Length) / 5; i++)
                        {
                            tempChequeAmoun = chequeDetail[count];
                            count++;
                            tempChequeNo = chequeDetail[count];
                            count++;
                            tempChequeCodeNo = chequeDetail[count];
                            count++;
                            tempChequeDate = chequeDetail[count];
                            count++;
                            tempChequeId = chequeDetail[count];
                            count++;
                            conn.Open();
                            new SqlCommand("insert into chequeGrn values ('" + invoieNoTemp + "','" + cutomerID + "','" + payble.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeDate + "','" + DateTime.Now + "','" + tempChequeCodeNo + "','" + tempChequeId + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                            {
                                try
                                {
                                    conn.Open();
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + invoieNoTemp + "','" + "GRN-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                                catch (Exception)
                                {
                                    new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + invoieNoTemp + "','" + "GRN-Pay" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + false + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                    conn.Close();
                                }
                            }

                            conn.Open();
                            new SqlCommand("insert into supplierStatement values('" + invoieNoTemp + "','" + "Cheque for Balance Amount of GRN" + "','" + tempChequeAmoun + "','" + 0 + "','" + true + "','" + DateTime.Now + "','" + cutomerID + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    if (cardDetailB)
                    {
                        count = 0;
                        for (int i = 0; i < (cardDetail.Length) / 4; i++)
                        {
                            tempChequeAmoun = cardDetail[count];
                            count++;
                            tempChequeNo = cardDetail[count];
                            count++;
                            tempChequeCodeNo = cardDetail[count];
                            count++;
                            tempChequeDate = cardDetail[count];
                            count++;
                            conn.Open();
                            new SqlCommand("insert into cardGrn values ('" + invoieNoTemp + "','" + cutomerID + "','" + payble.Text + "','" + DateTime.Now + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeCodeNo + "','" + tempChequeDate + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                    conn.Open();
                    new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from purchasingPriceList where qty='" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    clear();
                    MessageBox.Show("Sccesfully Updated GRN");
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

        private void nEWSUPPLIERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierProfile(this, user).Visible = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void sELECTSUPPLIERToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void iTEMPROFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new itemProfile(this, user).Visible = true;
        }

        private void sellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, discount, discount, e.KeyValue);
        }

        private void sellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
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

        private void discountTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void grnNo_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(grnNo, discountTotal, discountTotal, e.KeyValue);
        }

        private void discountTotal_KeyUp(object sender, KeyEventArgs e)
        {
            setDisk();
        }

        private void discountTotal_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(grnNo, vatPre, vatPre, e.KeyValue);
        }

        private void vatPre_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vatPre, nbtNew, nbtNew, e.KeyValue);
        }

        private void nbtNew_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(vatPre, code, code, e.KeyValue);
        }

        private void customer_TextChanged(object sender, EventArgs e)
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
                    loadCustomer(customer.Text);
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

        private void customer_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
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

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;
            loadCustomer(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer.Text = listBox2.SelectedItem.ToString();
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
                    reader = new SqlCommand("select id,description from supplier where description like '%" + customer.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();
                    states = true;
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        states = false;
                    }
                    reader.Close();
                    conn.Close();
                    if (states)
                    {
                        listBox2.Visible = false;
                    }
                }
                catch (Exception a)
                {//
                    MessageBox.Show(a.Message);
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
            db.setTextBoxPath(address, code, code, e.KeyValue);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void mINIMIZEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}