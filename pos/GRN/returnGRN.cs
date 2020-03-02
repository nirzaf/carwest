using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace pos
{
    public partial class returnGRN : Form
    {
        public returnGRN(Form form, String user, string id)
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
        public Boolean check, checkListBox, states, item, checkStock, termCheck, creditDetailB, chequeDetailB, cardDetailB;
        string user, listBoxType, cutomerID = "", invoiceino, description, invoieNoTemp;
        String[] idArray;

        public string[] creditDetail, chequeDetail, cardDetail;
        string brand, tempChequeAmoun, tempChequeNo, tempChequeCodeNo, tempChequeDate, tempChequeId;
        DataGridViewButtonColumn btn;
        Int32 invoiceMaxNo, rowCount, no, countDB, dumpInvoice, count;
        Double amount, purchashingPrice, qtyTemp, amountTemp, profit, profitTotal, maxAmount;

        Boolean loadItemCheck = false, discPrestage;
        public double cashPaid;
        public Double paidAmount, cashPaidDB, amountTrue, amountFalse;
        // my Variable End
        //my Method Start++++++

        public void loadInvoice(string id)
        {
          //  MessageBox.Show("1");
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

                    this.Text = id + "  (" + reader.GetDateTime(4) + ")";
                    var ter = reader[5] + "";

                    reader.Close();
                    conn.Close();
                    loadCustomer(customer.Text);
                    conn2.Open();

                    reader2 = new SqlCommand("select * from creditGRN where grnID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
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
                    reader2 = new SqlCommand("select * from cardGrn where invoiceID='" + invoieNoTemp + "' ", conn2).ExecuteReader();
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
                        dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[8], reader[2], reader[10], reader[5], reader[6], "false");
                        amountFalse = amountFalse + reader.GetDouble(6);
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from grnDetail where grnID='" + invoieNoTemp + "' and type='" + true + "'", conn).ExecuteReader();

                    while (reader.Read())
                    {
                        rowCount++;
                        dataGridView1.Rows.Add(rowCount, reader[1], reader[9], reader[8], reader[2], reader[10], reader[5], reader[6], "true");
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Silver;
                        amountTrue = amountTrue + reader.GetDouble(6);
                    }
                    conn.Close();
                    returnAmount.Text = amountTrue + "";
                 //   balanceAmount.Text = amountFalse + "";
                    subTotal.Text = amountFalse + amountTrue + "";
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



        public Boolean loadCustomer(string id)
        {

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
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

        void clear()
        {
            subTotal.Text = "0.0";
          //  balanceAmount.Text = "0";
            returnAmount.Text = "0";
            customer.Text = "[CASH SUPPLIER]";
            term.Text = "CASH";
            cutomerID = "";
            dataGridView1.Rows.Clear();
            // loadRetail();
            clearSub();
        }
        void clearSub()
        {

            minRetailPrice.Text = "0.0";
            maxRetailPrice.Text = "0.0";
            availebleQty.Text = "0";
            unitPrice.Text = "0.0";
            warrentyCode.Text = "";
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

                            if (discPrestage)
                            {
                                amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                            }
                            else
                            {
                                amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));

                            } dataGridView1.Rows.Add(rowCount + "", code.Text, warrentyCode.Text, description, unitPrice.Text, discount.Text, qty.Text, amount);


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
                                                dataGridView1.Rows[i].Cells[1].Value = code.Text;
                                                dataGridView1.Rows[i].Cells[2].Value = warrentyCode.Text;
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

                                } dataGridView1.Rows.Add(rowCount + "", code.Text, warrentyCode.Text, description, unitPrice.Text, discount.Text, qty.Text, amount);

                                amount = amount + (Double.Parse(subTotal.Text));
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
                    rowCount++;

                    if (discPrestage)
                    {
                        amount = ((Double.Parse(unitPrice.Text) - ((Double.Parse(unitPrice.Text) / 100) * Double.Parse(discount.Text))) * (Double.Parse(qty.Text)));

                    }
                    else
                    {
                        amount = ((Double.Parse(unitPrice.Text) - Double.Parse(discount.Text)) * (Double.Parse(qty.Text)));

                    } dataGridView1.Rows.Add(rowCount + "", "#", warrentyCode.Text, code.Text, unitPrice.Text, discount.Text, qty.Text, amount);

                    amount = amount + (Double.Parse(subTotal.Text));
                    var y = dataGridView1.RowCount;
                    y--;
                    dataGridView1.Rows[y].DefaultCellStyle.BackColor = Color.AliceBlue;



                    clearSub();
                    code.Focus();
                }
                amountFalse = 0;
                amountTrue = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        amountTrue = amountTrue + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                    }
                    else
                    {

                        amountFalse = amountFalse + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                    }
                }
                subTotal.Text = amountFalse + amountTrue + "";
                returnAmount.Text = amountTrue + "";
                //balanceAmount.Text = amountFalse + "";
            }
            catch (Exception s)
            {
                MessageBox.Show(s.StackTrace);
            }

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


            amountFalse = 0;
            amountTrue = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    amountTrue = amountTrue + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                }
                else
                {

                    amountFalse = amountFalse + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                }
            }
            subTotal.Text = amountFalse + amountTrue + "";
            returnAmount.Text = amountTrue + "";
            //balanceAmount.Text = amountFalse + "";


        }
        //   my Method End+++++++++
        private void invoiceNew_Load(object sender, EventArgs e)
        {
            MessageBox.Show("a");
          
            termCheck = true;
            this.TopMost = true;
            dataGridView1.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Normal;
            this.ControlBox = false;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            int height = Screen.PrimaryScreen.Bounds.Height;
            int width = Screen.PrimaryScreen.Bounds.Width;
            quickPanel.Width = width;
            //   dataGridView1.Width = width - 370;
            dataGridView1.Height = height - (250);

            dataGridView1.Columns[3].Width = dataGridView1.Width - 680;
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
            btn.Width = 60;
            btn.Text = "RETURN";

            btn.UseColumnTextForButtonValue = true;

            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            clear();

            customer.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;
           loadInvoice(invoiceino);
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {

                    discPrestage = reader.GetBoolean(4);
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
                if (e.ColumnIndex == 9)
                {
                    //     MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                    // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("false"))
                    {
                        MessageBox.Show("Sorry, Cant Edit GRN Row Data On Retuen Note Function");
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
                        amountFalse = 0;
                        amountTrue = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                            {
                                amountTrue = amountTrue + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            }
                            else
                            {

                                amountFalse = amountFalse + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());

                            }
                        }
                        subTotal.Text = amountFalse + amountTrue + "";
                        returnAmount.Text = amountTrue + "";
                        //.Text = amountFalse + "";

                    }

                }
                else if (e.ColumnIndex == 10)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString().Equals("true"))
                    {
                        MessageBox.Show("Sorry,You Have Selected already Return Row Data");
                    }
                    else
                    {
                        new itemTable6(this, dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), e.RowIndex + "").Visible = true;

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
           // new termXash6(this, Double.Parse(balanceAmount.Text), cashPaid, creditDetail, chequeDetail, cardDetail, creditDetailB, chequeDetailB, cardDetailB).Visible = true;

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry Emprt Data for Generate GRN");
                code.Focus();
            }
           
            else if ((MessageBox.Show("Generate GRN ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                db.setCursoerWait();
                try
                {
                    invoieNoTemp = invoiceino.ToString();
                    //+++++Intial OLD INVOice++++
                    conn.Open();
                    new SqlCommand("delete from itemStatement where invoiceid= '" +  invoieNoTemp + "' and type='"+"GRN-RETURN"+"'", conn).ExecuteNonQuery();

                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from grnDetail where grnID='" + invoieNoTemp + "' AND type='"+false+"'", conn).ExecuteReader();
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
                    reader = new SqlCommand("select itemCode,qty,purchasingPrice from grnDetail where grnID='" + invoieNoTemp + "' AND type='" + true + "'", conn).ExecuteReader();
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
                    new SqlCommand("delete from grnDetail where grnID='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
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
                        amount = amount + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        //  profit = profit + Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    }



                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        qtyTemp = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());

                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals("#"))
                        {
                            conn2.Open();
                            new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + dataGridView1.Rows[i].Cells[8].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "') ", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }
                        else
                        {
                            if (dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                            {
                                conn.Open();
                                new SqlCommand("insert into itemStatement values('" +  invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + true + "','" + qtyTemp + "','" + DateTime.Now + "','" + "GRN-RETURN" + "','" + user + "')", conn).ExecuteNonQuery();

                                conn.Close();
                                conn.Open();
                                new SqlCommand("update item set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();

                                conn.Close();
                                conn.Open();
                                states = true;
                                reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {

                                    conn2.Open();

                                    new SqlCommand("update purchasingPriceList set qty=qty-'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn2).ExecuteNonQuery();
                                    conn2.Close();

                                }
                               
                                conn.Close();
                            }
                           // conn.Open();
                            if (!dataGridView1.Rows[i].Cells[8].Value.ToString().ToUpper().Equals("TRUE"))
                            {
                                conn.Open();
                                new SqlCommand("update item set qty=qty+'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'", conn).ExecuteNonQuery();

                                conn.Close();
                                conn.Open();
                                states = true;
                                reader = new SqlCommand("select purchasingprice,qty from purchasingPriceList where code='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn).ExecuteReader();
                                if (reader.Read())
                                {

                                    conn2.Open();

                                    new SqlCommand("update purchasingPriceList set qty=qty+'" + qtyTemp + "' where code='" + dataGridView1.Rows[i].Cells[1].Value + "' and purchasingprice='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "'", conn2).ExecuteNonQuery();
                                    conn2.Close();

                                }
                                else
                                {
                                    conn2.Open();
                                    new SqlCommand("insert into purchasingPriceList values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();
                                    conn2.Close();
                                }
                                reader.Close();
                                conn.Close();
                            }
                            
                                conn2.Open();
                                new SqlCommand("insert into grnDetail values ('" + invoieNoTemp + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + 0 + "','" + 0 + "','" + qtyTemp + "','" + dataGridView1.Rows[i].Cells[7].Value + "','" + dataGridView1.Rows[i].Cells[8].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "')", conn2).ExecuteNonQuery();
                                conn2.Close();

                            
                        }

                    }

                    var a = Double.Parse(subTotal.Text) - Double.Parse(returnAmount.Text);
                    conn.Open();
                    new SqlCommand("update grn set subtotal='" + a + "' where  id='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    //   MessageBox.Show(a+"");
                    conn.Open();
                    new SqlCommand("update creditGRN set amount='" + a + "',balance='" + a + "'-paid where  grnid='" + invoieNoTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();

                   
                    conn.Open();
                    new SqlCommand("update purchasingPriceList set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    new SqlCommand("delete from purchasingPriceList where qty='"+0+"'", conn).ExecuteNonQuery();
                    conn.Close();
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

        private void button1_Click_1(object sender, EventArgs e)
        {

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
    }
}
