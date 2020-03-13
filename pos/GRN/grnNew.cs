using System;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;

namespace pos
{
    public partial class grnNew : Form
    {
        public grnNew(Form form, String user)
        {
            InitializeComponent();
            home = form;
            this.user = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private ArrayList arrayList, stockList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB;
        private string user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        private String[] idArray;
        public string[] creditDetail, chequeDetail, cardDetail;
        private string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        private DataGridViewButtonColumn btn;
        private Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice, count;
        public Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount, cashPaid;

        private Boolean loadItemCheck = false, discPrestage;
        // my Variable End
        //my Method Start++++++

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

        public Boolean saveCustomer(string id)
        {
            //MessageBox.Show(id);
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from supplier where company='" + id + "'", conn).ExecuteReader();
                if (!reader.Read())
                {
                    conn.Close();
                    conn.Open();
                    new SqlCommand("insert into supplier values ('" + "" + "','" + "" + "','" + customer.Text + "','" + address.Text + "','" + mobileNumber.Text + "','" + "" + "','" + customer.Text + "','" + "" + "','" + "" + "','" + 0 + "')", conn).ExecuteNonQuery();

                    conn.Close();
                }

                mobileNumber.Focus();
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
        }

        private void clear()
        {
            listBox1.Visible = false;

            listBox2.Visible = false;
            netTotal.Text = "0";
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
            qty.Text = "";

            tempDesc.Text = "";
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
                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {
                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }

                    tempDesc.Text = reader.GetString(4).ToUpper();
                    conn.Close();
                }
                else
                {
                    // var code=itemc
                    //   MessageBox.Show(codeValue);
                    item = false;
                    clearSub();
                    code.Text = codeValue;
                }
                reader.Close();
                conn.Close();

                qty.Focus();
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
                    else
                    {
                        if (dataGridView1.Rows.Count == 0)
                        {
                            rowCount++;

                            dataGridView1.Rows.Add(rowCount + "", code.Text, description, qty.Text);
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

                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[2].Value = description;
                                                dataGridView1.Rows[i].Cells[3].Value = qtyTemp;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rowCount++;

                                dataGridView1.Rows.Add(rowCount + "", code.Text, description, qty.Text);

                                var y = dataGridView1.RowCount;
                                y--;
                                dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.Azure;
                            }
                        }

                        clearSub();
                        code.Focus();
                    }
                }
                else
                {
                    if (qty.Text.Equals("") || Double.Parse(qty.Text) <= 0)
                    {
                        MessageBox.Show("Sorry Stock not Available on this Item to GRN ");
                        qty.Focus();
                    }
                    else
                    {
                        rowCount++;
                        var a = MessageBox.Show("You Have Enterd New Item and Do You Need to Save it to System", "Confirmation",
      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
      MessageBoxDefaultButton.Button1);
                        if (a == System.Windows.Forms.DialogResult.Yes)
                        {
                            newItemGRN2 ab = new newItemGRN2(this);
                            ab.code.Text = code.Text;

                            ab.Visible = true;
                        }
                        else if (a == System.Windows.Forms.DialogResult.No)
                        {
                            rowCount++;
                            dataGridView1.Rows.Add(rowCount + "", code.Text, description, qty.Text);

                            var y = dataGridView1.RowCount;
                            y--;
                            dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;

                            clearSub();
                            code.Focus();
                        }
                    }
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.StackTrace);
            }
        }

        private double purch;

        public void updateTableItem(string unitPrice, string discount, string qty, Int32 index)
        {
            dataGridView1.Rows[index].Cells[7].Value = qty;
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where loaduser='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private Double amountR;

        private Boolean checkTerm()
        {
            states = true;
            if (creditDetailB)
            {
                amountR = Double.Parse(creditDetail[0].ToString());
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
            if (!creditDetailB & !cardDetailB & !chequeDetailB)
            {
                amountR = Double.Parse(netTotal.Text);
            }

            amountR = amountR + cashPaid;
            if (amountR != Double.Parse(netTotal.Text))
            {
                states = false;
            }

            return states;
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
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice,rate from item where code='" + codeL + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;

                    tempDesc.Text = reader[1] + "";

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
                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, qty.Text);

                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;

                    clearSub();
                    code.Focus();
                }
                else
                {
                    item = false;

                    dataGridView1.Rows.Add(rowCount + "", code.Text, description, qty.Text);

                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;

                    clearSub();
                    code.Focus();
                }
                reader.Close();
                conn.Close();
                code.Focus();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        //   my Method End+++++++++
        private void invoiceNew_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            //   panel3.Width = width - 3;
            quickPanel.Width = width;
            dataGridView1.Width = width - 370;
            dataGridView1.Height = height - (435);

            dataGridView1.Columns[2].Width = dataGridView1.Width - 327;
            Point p = new Point();
            p = panel1.Location;
            p.X = quickPanel.Width - panel1.Width - 20;
            panel1.Location = p;
            panel2.Width = dataGridView1.Width;
            //   dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);

            p.X = width - panel4.Width - 15;
            p.Y = height - panel4.Height - 15;
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
            grnNo.CharacterCasing = CharacterCasing.Upper;
            comboDiscount.SelectedIndex = 0;
            customer.CharacterCasing = CharacterCasing.Upper;
            address.CharacterCasing = CharacterCasing.Upper;
            mobileNumber.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;
            loadUser();
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
            loadAccountList();
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
            new supplierQuick(this).Visible = true;
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
                if (e.ColumnIndex == 5)
                {
                    //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
                else if (e.ColumnIndex == 6)
                {
                    new itemTable4(this, "0", "0", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), e.RowIndex + "", "0").Visible = true;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
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
            new termXash4(this, Double.Parse(netTotal.Text), cashPaid, creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
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
                        qty.Focus();
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
                            listBox1.Items.Add(reader[1].ToString().ToUpper());
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
            if (e.KeyValue == 40)
            {
                code.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                addToTable();
            }
            else if (e.KeyValue == 38)
            {
                code.Focus();
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

        private Int32 tempCount;
        private double tempPrice;

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Empty Data for Generate GRN ?");
                code.Focus();
            }
            else if (!checkTerm())
            {
                MessageBox.Show("Please Enter Pay Detail on Term Section Before Genarate Invoice");
            }
            else if (netTotal.Text.Equals("") || Double.Parse(netTotal.Text) == 0)
            {
                MessageBox.Show("Please Enter GRN Amount");
                netTotal.Focus();
            }
            else if ((MessageBox.Show("Generate GRN ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                    loadInvoiceNoRetail();
                    invoieNoTemp = invoiceNo.ToString();

                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                            if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                            {
                                //    MessageBox.Show(invoieNoTemp);
                                conn2.Open();
                                new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + 0 + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + 0 + "','" + false + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "') ", conn2).ExecuteNonQuery();
                                //  new SqlCommand("insert into grnDetail values ('" +0 + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + 0 + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + 0 + "','" + false + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "') ", conn2).ExecuteNonQuery();

                                conn2.Close();
                            }
                            else
                            {
                                conn.Open();
                                new SqlCommand("insert into itemStatement values('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + false + "','" + qtyTemp + "','" + DateTime.Now + "','" + "GRN" + "','" + user + "')", conn).ExecuteNonQuery();

                                conn.Close();
                                conn.Open();

                                new SqlCommand("update item set qty=qty+'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();
                                conn.Close();

                                if (states)
                                {
                                    // MessageBox.Show("A" + invoieNoTemp + "B");
                                    conn2.Open();
                                    new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + 0 + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + 0 + "','" + false + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + "" + "','" + 0 + "') ", conn2).ExecuteNonQuery();
                                    conn2.Close();
                                }
                            }
                        }
                    }
                    catch (Exception na)
                    {
                        MessageBox.Show(na.StackTrace);
                        conn2.Close();
                        conn.Close();
                    }

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

                    var cashDetailB = true;
                    if (!creditDetailB & !chequeDetailB & !cardDetailB)
                    {
                        cashDetailB = true;
                        conn.Open();
                        new SqlCommand("insert into grn values('" + invoieNoTemp + "','" + grnNo.Text + "','" + customer.Text + "','" + netTotal.Text + "','" + dateTimePicker1.Value + "','" + "CASH- " + "','" + user + "','" + netTotal.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        cashDetailB = false;
                        conn.Open();
                        new SqlCommand("insert into grn values('" + invoieNoTemp + "','" + grnNo.Text + "','" + customer.Text + "','" + netTotal.Text + "','" + dateTimePicker1.Value + "','" + "CREDIT- " + "','" + user + "','" + cashPaid + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }

                    conn.Open();
                    new SqlCommand("delete from cardGrn where invoiceid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from creditGrn where grnid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from chequeGrn where grnid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from cashGrn where grnid='" + invoiceNo + "'", conn).ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    new SqlCommand("insert into grnTerm values('" + invoieNoTemp + "','" + cashDetailB + "','" + creditDetailB + "','" + chequeDetailB + "','" + cardDetailB + "')", conn).ExecuteNonQuery();
                    conn.Close();

                    //  MessageBox.Show("Invoice Succefully Generated and Queue to Print....");
                    if (cashDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into cashGrn values('" + invoieNoTemp + "','" + cutomerID + "','" + netTotal.Text + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }

                    if (creditDetailB)
                    {
                        conn.Open();
                        new SqlCommand("insert into creditgrn values ('" + invoieNoTemp + "','" + cutomerID + "','" + netTotal.Text + "','" + 0 + "','" + creditDetail[0] + "','" + creditDetail[1] + "','" + DateTime.Now + "')", conn).ExecuteNonQuery();
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
                            new SqlCommand("insert into chequeGrn values ('" + invoieNoTemp + "','" + cutomerID + "','" + netTotal.Text + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeDate + "','" + DateTime.Now + "','" + tempChequeCodeNo + "','" + tempChequeId + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboChequePayment.Items.Count != 0 && comboChequePayment.SelectedIndex != -1)
                            {
                                conn.Open();

                                new SqlCommand("insert into bankAccountStatment values('" + comboChequePayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + invoieNoTemp + "','" + "Purchasing" + "','" + cutomerID + "','" + "Cheque Payment :Cheque No-" + tempChequeNo + ",Cheque Date-" + tempChequeDate + "','" + false + "','" + true + "','" + tempChequeDate + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
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
                            new SqlCommand("insert into cardGRN values ('" + invoieNoTemp + "','" + cutomerID + "','" + netTotal.Text + "','" + DateTime.Now + "','" + 0 + "','" + tempChequeAmoun + "','" + tempChequeNo + "','" + tempChequeCodeNo + "','" + tempChequeDate + "')", conn).ExecuteNonQuery();
                            conn.Close();
                            if (comboCardPayment.Items.Count != 0 && comboCardPayment.SelectedIndex != -1)
                            {
                                conn.Open();

                                new SqlCommand("insert into bankAccountStatment values('" + comboCardPayment.SelectedItem.ToString().Split('(')[1].Split(')')[0] + "','" + invoieNoTemp + "','" + "Purchasing" + "','" + cutomerID + "','" + "Card Payment" + "','" + false + "','" + true + "','" + DateTime.Now + "','" + tempChequeAmoun + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }

                    clear();
                    MessageBox.Show("Sucessfully Created New GRN");
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

        private void unitPrice_TextChanged(object sender, EventArgs e)
        {
        }

        private void grnNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                netTotal.Focus();
            }
            else if (e.KeyValue == 40)
            {
                netTotal.Focus();
            }
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

        private void nEWSUPPLIERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierProfile(this, user).Visible = true;
        }

        private void sellingPrice_KeyDown(object sender, KeyEventArgs e)
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

        private void discountTotal_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void subTotal_TextChanged(object sender, EventArgs e)
        {
        }

        private void vatPre_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void vatPre_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void discountTotal_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nbtNew_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nbtNew_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void nbtNew_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void discountTotal_KeyUp_1(object sender, KeyEventArgs e)
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
                    reader = new SqlCommand("select id,company from supplier where company like '%" + customer.Text + "%' ", conn).ExecuteReader();
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

        private void customer_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void mobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(address, code, code, e.KeyValue);
        }

        private void address_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(customer, mobileNumber, mobileNumber, e.KeyValue);
        }

        private void subTotal_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void subTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void subTotal_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void netTotal_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void netTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void cREDITCARDPAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}