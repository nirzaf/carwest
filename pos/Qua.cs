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
    public partial class Qua : Form
    {
        public Qua(Form form, String user)
        {
            InitializeComponent();
            home = form;
            this.user = user;
        }
        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        ArrayList arrayList, stockList;
        public Boolean check, checkListBox, states, item, checkStock, creditDetailB, chequeDetailB, cardDetailB, saveInvoiceWithoutPay, cashFLowAuto;
        string user, listBoxType, cutomerID = "", invoiceNo, description, invoieNoTemp;
        String[] idArray;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice;
        Double amount, amount2, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount;
        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        int count;
        string type = "";
        Boolean loadItemCheck = false, discPrestage, isNBT, isTax;
        public double paidAmount = 0, taxpre, nbtpre;
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

                taxPreLabel.Text = taxpre + " %";
                nbtPreLabel.Text = nbtpre + "%";
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
                states = false;
                if (!creditDetailB & !cardDetailB & !chequeDetailB)
                {
                    states = true;
                }
                else
                {
                    conn.Open();
                    reader = new SqlCommand("select name from customer where id='" + cutomerID + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        states = true;
                    }
                    conn.Close();

                }

            }
            catch (Exception)
            {
                states = false;
                conn.Close();
            }
            return states;
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

        public Boolean loadCustomer(string id)
        {
            //MessageBox.Show(id);
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
                    customer.Text = "[cash customer]";
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

        void loadInvoiceNoRetail()
        {
            try
            {
                // panel1.BackColor = Color.Red;

                conn.Open();
                reader = new SqlCommand("select max(id) from qua", conn).ExecuteReader();
                if (reader.Read())
                {
                    invoiceMaxNo = reader.GetInt32(0);
                }
                invoiceMaxNo++;
                invoiceNo =  invoiceMaxNo + "";
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
            subTotal.Text = "0.0";
            total.Text = "0.0";
            disc.Text = "0.0";
            saleRef.Text = "";
            customer.Text = "[CASH CUSTOMER]";
            cutomerID = "";
            dataGridView1.Rows.Clear();
            // loadInvoiceNoRetail();
            clearSub();
            creditDetailB = false;
            chequeDetailB = false;
            cardDetailB = false;
            nbt.Text = "0";
            tax.Text = "0";
            saleRef.SelectedIndex = -1;
        }
        void clearSub()
        {

            minRetailPrice.Text = "0.0";
            maxRetailPrice.Text = "0.0";
            availebleQty.Text = "0";
            unitPrice.Text = "0.0";
            qty.Text = "";
            discount.Text = "0";
            code.Text = "";
            code.Focus();
            availebleQty.Text = "0";
        }
        void loadItem(string codeValue)
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select qty,detail,retailPrice,billingPrice from item where code='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    item = true;
                    code.Text = codeValue + "";
                    availebleQty.Text = reader.GetDouble(0) + "";
                    var ab = reader.GetString(1).ToUpper().Split(' ');
                    description = "";
                    for (int i = 0; i < ab.Length; i++)
                    {

                        if (i != 0)
                        {
                            description = description + " " + ab[i];
                        }
                    }
                    minRetailPrice.Text = db.setAmountFormat(reader.GetDouble(2) + "");
                    maxRetailPrice.Text = db.setAmountFormat(reader.GetDouble(3) + "");
                    unitPrice.Text = reader.GetDouble(3) + "";

                    discount.Focus();
                    conn.Close();

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
                //MessageBox.Show(a.Message);
            }
        }
        void addToTable()
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

                    else if (unitPrice.Text.Equals(""))
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

                            dataGridView1.Rows.Add(rowCount + "", code.Text, "", description, unitPrice.Text, discount.Text, qty.Text, amount);

                            amount = (Double.Parse(subTotal.Text));

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
                                                qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + Double.Parse(qty.Text);


                                                if (discPrestage)
                                                {
                                                    amountTemp = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * qtyTemp);

                                                }
                                                else
                                                {
                                                    amountTemp = (Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * qtyTemp;

                                                }
                                                //   amount = Double.Parse(subTotal.Text) + amountTemp;
                                                //dataGridView1.Rows.RemoveAt(i);
                                                //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);

                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[3].Value = description;
                                                dataGridView1.Rows[i].Cells[4].Value = unitPrice.Text;
                                                dataGridView1.Rows[i].Cells[5].Value = discount.Text;
                                                dataGridView1.Rows[i].Cells[6].Value = qtyTemp;

                                                dataGridView1.Rows[i].Cells[7].Value = amountTemp;



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
                                dataGridView1.Rows.Add(rowCount + "", code.Text, "", description, unitPrice.Text, discount.Text, qty.Text, amount);

                                amount = amount + (Double.Parse(subTotal.Text));
                                var y = dataGridView1.RowCount;
                                y--;
                                dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.Azure;


                            }
                        }

                        amount = 0;
                        amount2 = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                            amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

                        }
                        subTotal.Text = amount + "";
                        disc.Text = amount2 + "";
                        total.Text = amount + amount2 + "";
                        tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
                        nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";
                        clearSub();
                        code.Focus();

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
                        amount = ((Double.Parse(unitPrice.Text) - (Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                    }
                    dataGridView1.Rows.Add(rowCount + "", "#", "", code.Text, unitPrice.Text, discount.Text, qty.Text, amount);

                    amount = amount + (Double.Parse(subTotal.Text));
                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;

                    amount = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

                    }
                    subTotal.Text = amount + "";
                    disc.Text = amount2 + "";
                    total.Text = amount + amount2 + "";
                    tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
                    nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";
                    clearSub();
                    code.Focus();
                }


            }
            catch (Exception s)
            {
                MessageBox.Show("Please Enter Value");

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

            }  //   amount = Double.Parse(subTotal.Text) + amountTemp;
            //dataGridView1.Rows.RemoveAt(i);
            //dataGridView1.Rows.Add(code.Text, brand, description, qtyTemp, retailPrice.Text, Double.Parse(disc2.Text), amountTemp, amountTemp - (purchashingPrice * qtyTemp), purchashingPrice, 2);


            dataGridView1.Rows[index].Cells[4].Value = unitPrice;
            dataGridView1.Rows[index].Cells[5].Value = discount;
            dataGridView1.Rows[index].Cells[6].Value = qty;

            dataGridView1.Rows[index].Cells[7].Value = (amountTemp + "");

            MessageBox.Show(amountTemp + "");
            amount = 0;
            amount2 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));

            }
            subTotal.Text = amount + "";
            disc.Text = amount2 + "";
            total.Text = amount + amount2 + "";
            tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
            nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";


        }
        void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    nEWCUSTOMERToolStripMenuItem.Enabled = reader.GetBoolean(6);

                }
                reader.Close();
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            quickPanel.Width = width;
            dataGridView1.Width = width - 370;
            dataGridView1.Height = height - (335);
            button1.BackColor = Color.Transparent;
            dataGridView1.Columns[3].Width = dataGridView1.Width - 615;
            Point p = new Point();
            p = panel1.Location;
            p.X = quickPanel.Width - panel1.Width - 20;
            panel1.Location = p;
            panel2.Width = dataGridView1.Width;
            //   dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            p = panel3.Location;
            p.X = panel2.Width - panel3.Width;
            panel3.Location = p;


            p.X = width - panel4.Width - 15;
            p.Y = height - panel4.Height - 15;
            panel4.Location = p;

            p = panelTax.Location;
            p.X = width - panel3.Width + 15;
            panelTax.Location = p;
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
            loadUser();


            loadCompany();
            dataGridView1.Columns[5].Visible = false;
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
            this.toolTip1.SetToolTip(button1, "ADD CUSTOMER");
        }

        private void button1_Click(object sender, EventArgs e)
        {
              new cusomerQuick3(this).Visible = true;

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
            if (e.ColumnIndex == 8)
            {
                //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                amount = 0.0;
                amount2 = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                    amount2 = amount2 + ((Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) * Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) - Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()));
                    dataGridView1.Rows[i].Cells[0].Value = ++i;
                    i--;

                }
                subTotal.Text = amount + "";

                disc.Text = amount2 + "";
                total.Text = amount + amount2 + "";
                tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
                nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";

                rowCount--;
            }
            else if (e.ColumnIndex == 9)
            {
                // new itemTable(this, dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), e.RowIndex + "", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()).Visible = true;
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

            //    new termXash(this, Double.Parse(subTotal.Text), Double.Parse(cashPaid.Text), creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;
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
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                if (loadItemCheck)
                {
                    db.setList(listBox1, code, code.Width * 3);

                    try
                    {
                        listBox1.Items.Clear();
                        conn.Open();
                        reader = new SqlCommand("select code,detail from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
                        arrayList = new ArrayList();

                        while (reader.Read())
                        {
                            listBox1.Items.Add(reader[1].ToString().ToUpper());
                            listBox1.Visible = true;
                            listBox1.Height = panel2.Height - 30;
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
            }
            if (code.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void unitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, discount, discount, e.KeyValue);

        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(unitPrice, qty, qty, e.KeyValue);

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
                // warrentyCode.Focus();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Emprt Data for Generate Quation");
                code.Focus();
            }

            else if (!checkUser())
            {
                MessageBox.Show("Please Enter a Registerd Customer for a Credit Invoice");

            }

            else if ((MessageBox.Show("Generate Quation ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                    loadInvoiceNoRetail();
                    invoieNoTemp = invoiceNo;
                    amount = 0;
                    profit = 0;
                    profitTotal = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    }



                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        conn2.Open();
                        new SqlCommand("insert into quaDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }


                    if (cutomerID.Equals(""))
                    {
                        cutomerID = customer.Text;
                    }

                    conn.Open();
                    new SqlCommand("insert into qua values('" + invoieNoTemp + "','" + cutomerID + "','" + subTotal.Text + "','" + DateTime.Now.ToShortDateString() + "','" + dateExpire.Value + "','" + disc.Text + "','" + total.Text + "','" + user + "')", conn).ExecuteNonQuery();
                    conn.Close();



                    if ((MessageBox.Show("Quation Succefully Generated , Do You want to Print it", "Confirmation",
         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
         MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                        // testPrint a = new testPrint();


                        //  new invoicePrint().setprintCheque("R-" + invoieNoTemp, customer.Text, term.Text, dataGridView1, subTotal.Text, cashPaid.Text, balance.Text, DateTime.Now, conn, reader, user);
                        new invoicePrint().setprintqUATE(invoieNoTemp, customer.Text, dataGridView1, DateTime.Now, conn, reader, user,dateExpire.Value);



                        conn.Close();
                        //  a.Visible = true;
                    }

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
            }
            else
            {

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

                reader = new SqlCommand("select name,istax,taxpre,isNBT,nbtPre,default from company where  name='" + comboCompany.SelectedItem.ToString() + "'", conn).ExecuteReader();
                if (reader.Read())
                {


                    isNBT = reader.GetBoolean(3);
                    isTax = reader.GetBoolean(1);
                    taxpre = reader.GetDouble(2);
                    nbtpre = reader.GetDouble(4);

                }
                conn.Close();

                tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
                nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";
                taxPreLabel.Text = taxpre + " %";
                nbtPreLabel.Text = nbtpre + "%";
            }
            catch (Exception)
            {
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
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                saleRef.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                saleRef.Text = listBox2.SelectedItem.ToString();
                saleRef.SelectionLength = saleRef.MaxLength;

            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;

            saleRef.Text = listBox2.SelectedItem.ToString();
            saleRef.SelectionLength = saleRef.MaxLength;

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            saleRef.Text = listBox2.SelectedItem.ToString();

        }

        private void poNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                saleRef.Focus();
            }
            else if (e.KeyValue == 40)
            {
                saleRef.Focus();
            }
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

        private void disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void disc_KeyUp(object sender, KeyEventArgs e)
        {
            var sub = Double.Parse(total.Text);
            if (!disc.Text.Equals(""))
            {



                if (Double.Parse(disc.Text) > (sub))
                {

                    disc.Focus();
                    disc.Text = "0";
                    subTotal.Text = total.Text;
                    disc.SelectAll();
                }



                subTotal.Text = sub - (Double.Parse(disc.Text)) + "";

            }
            else
            {


                disc.Text = "0";
                subTotal.Text = total.Text;
                disc.SelectAll();
            }
            tax.Text = (Double.Parse(subTotal.Text) / 100) * taxpre + "";
            nbt.Text = (Double.Parse(subTotal.Text) / 100) * nbtpre + "";

        }

        private void subTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
