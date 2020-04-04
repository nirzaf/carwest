using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pos
{
    public partial class home : Form
    {
        public home(string user, Form form)
        {
            InitializeComponent();
            formH = form;
            userH = user;
        }

        //Variable
        private Form formH;

        private DB db, db2, db3;
        private string userH;
        private SqlConnection conn, conn2, conn3;
        private SqlDataReader reader, reader2, reader3;

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + userH + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    // button1.Enabled = reader.GetBoolean(3);
                    // button3.Enabled = reader.GetBoolean(22);
                    // pROFILEToolStripMenuItem.Enabled = reader.GetBoolean(22);
                    // button4.Enabled = reader.GetBoolean(12);
                    // nEWINVOICEToolStripMenuItem1.Enabled = reader.GetBoolean(12);
                    // button5.Enabled = reader.GetBoolean(15);
                    // sEARCHEDITRETURNINVOICEToolStripMenuItem.Enabled = reader.GetBoolean(15);

                    // button2.Enabled = reader.GetBoolean(16);
                    // nEWGRNToolStripMenuItem.Enabled = reader.GetBoolean(16);
                    //sEARCHEDITRETURNGRNToolStripMenuItem.Enabled = reader.GetBoolean(19);
                    // uSERSToolStripMenuItem.Enabled = reader.GetBoolean(23);
                    // iNVOICECREDITPAYToolStripMenuItem.Enabled = reader.GetBoolean(13);
                    // gRNCREDITPAYToolStripMenuItem.Enabled = reader.GetBoolean(17);
                    // qUICKCHECKDEPOSITToolStripMenuItem.Enabled = reader.GetBoolean(25);

                    // qUICKCHEQUEDEPOSITToolStripMenuItem.Enabled = reader.GetBoolean(25);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void home_Load(object sender, EventArgs e)
        {
            //  MessageBox.Show("1");
            //  MessageBox.Show(userH);
            // formH.Close();
            this.TopMost = true;
            //    this.WindowState = FormWindowState.Normal;
            // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //  this.Bounds = Screen.PrimaryScreen.Bounds;

            //int height = Screen.PrimaryScreen.Bounds.Height;
            //int width = Screen.PrimaryScreen.Bounds.Width;
            ////MessageBox.Show(height+","+width);

            /// tabControl1.Width = width-(button2.Width+100);
            //  button2.Location = new Point(width - 100, height - 50);
            // quickPanel.Width = width - 3;

            //button8.Location = new Point(quickPanel.Width - button8.Width - 10, button8.Location.Y);
            //label7.Location = new Point(quickPanel.Width - label7.Width - 15, label7.Location.Y);
            //panel1.Width = width;
            //panel1.Height = height - quickPanel.Height;
            db = new DB();
            db2 = new DB();
            db3 = new DB();
            db.home = this;
            conn = db.createSqlConnection();

            conn2 = db2.createSqlConnection();
            conn3 = db3.createSqlConnection();
            loadUser();

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {
                    //     buttonClearInvoice.Visible = reader.GetBoolean(6);
                }
                else
                {
                    //   buttonClearInvoice.Visible = false;
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerProfile(this, userH).Visible = true;
        }

        private void home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Enabled = true;
            new invoiceNew(this, userH, "CASH").Visible = true;
        }

        private void newItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void stockPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new purchasing(this, userH, "").Visible = true;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(button1, "NEW ITEM (F1)");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            this.toolTip2.SetToolTip(button3, "PRICE / STOCK (F2)");
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            this.toolTip3.SetToolTip(button4, "NEW INVOICE (F3)");
        }

        private void newInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new ExpensesNormal(this, userH).Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoicePay(this, userH).Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new barcode(this).Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void searchEditInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void payInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void barcodeGeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnNew(this, userH).Visible = true;
        }

        private void grnNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void quickPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void searchEditGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnSearch(this, userH).Visible = true;
        }

        private void uSERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new USERS(this, userH).Visible = true;
        }

        private void nEWITEMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                this.Enabled = true;
                new itemProfile(this, userH).Visible = true;
            }
        }

        private void bARCODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void nEWINVOICEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CASH").Visible = true;
        }

        private void sEARCHEDITRETURNINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button5.Enabled)
            {
                this.Enabled = true;
                new invoiceSearch(this, userH).Visible = true;
            }
        }

        private void pAYINVOICEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void sTOCKPRICEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //if (button3.Enabled)
            //{
            //    this.Enabled = true;
            //    new stockProfile(this, userH).Visible = true;
            //}
        }

        private void nEWGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                this.Enabled = true;
                new grnNew(this, userH).Visible = true;
            }
        }

        private void sEARCHEDITRETURNGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnSearch(this, userH).Visible = true;
        }

        private void pROFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            new login().Visible = true;
        }

        private void nEWSUPPLIERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierProfile(this, userH).Visible = true;
        }

        private void nEWCUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerProfile(this, userH).Visible = true;
        }

        private void eXITToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void sTOCKREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new stockReport(this, userH).Visible = true;
        }

        private void cUSTOMERREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void sUPPLIERREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void iNVOICECREDITPAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Enabled = true;
            new invoiceCreditPay(this, userH).Visible = true;
        }

        private void gRNCREDITPAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnCreditPay(this, userH).Visible = true;
        }

        private void iNVOICECREDITPAYMENTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void iNVOICECREDITPAYMENTSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void cUSTOMERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerOutstanding(this, userH).Visible = true;
        }

        private void sUPPLIERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierOutstanding(this, userH).Visible = true;
        }

        private void cUSTOMERSREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerReport(this, userH).Visible = true;
        }

        private void sUPPLIERSREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierReport(this, userH).Visible = true;
        }

        private void iNVOICEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoiceCreditPaidedit(this, userH).Visible = true;
        }

        private void gRNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnCreditPaidedit(this, userH).Visible = true;
        }

        private void qUICKCHEQUEDEPOSITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new chequeDeposit(this, userH).Visible = true;
        }

        private void qUICKCHECKDEPOSITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new chequeDeposit(this, userH).Visible = true;
        }

        private void cUSTOMERSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new chequeInvoice2(this, userH).Visible = true;
        }

        private void gRNToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new chequeGrn(this, userH).Visible = true;
        }

        private void sALESUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new saleSummery(this, userH).Visible = true;
        }

        private void bANKACCOUNTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new bankaccount(this, userH).Visible = true;
        }

        private void aCCOUNTLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Enabled = true;
            //new statement(this, userH).Visible = true;
        }

        private void sALESREFToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void iNCOMEACCOUNTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new income(this, userH).Visible = true;
        }

        private void eXPENSESACCOUNTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new Expenses(this, userH).Visible = true;
        }

        private void bANKACCOUNTSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new bankaccount(this, userH).Visible = true;
        }

        private void fIXEDASSETSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new fixedAsset(this, userH).Visible = true;
        }

        private void aCCOUNTLISTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            // new accountList(this, userH).Visible = true;
        }

        private void sALESREFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new salesRef(this, userH).Visible = true;
        }

        private void nEWQUATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new Qua(this, userH).Visible = true;
        }

        private void sTOCKSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new stockSummmery(this, userH).Visible = true;
        }

        private void mYOPENINGBALANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cASHBALANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new cashOpeningBalancea(userH, this).Visible = true;
        }

        private void cASHSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new cashSummeryNew(this, userH).Visible = true;
        }

        private void tIMESHEETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new timeSheet(this, userH).Visible = true;
        }

        private void nEWEDITDELETESTAFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new staff(this, userH).Visible = true;
        }

        private void eXPENSESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new ExpensesNormal(this, userH).Visible = true;
        }

        private void buttonClearInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                new SqlCommand("delete from canselInvoice", conn).ExecuteNonQuery();
                new SqlCommand("delete from cardInvoiceRetail", conn).ExecuteNonQuery();
                new SqlCommand("delete from cashBalance", conn).ExecuteNonQuery();
                new SqlCommand("delete from cashInvoice", conn).ExecuteNonQuery();
                new SqlCommand("delete from cashInvoiceRetail", conn).ExecuteNonQuery();
                new SqlCommand("delete from cashSummery  ", conn).ExecuteNonQuery();
                new SqlCommand("delete from chequeInvoiceRetail", conn).ExecuteNonQuery();

                new SqlCommand("delete from creditInvoiceRetail", conn).ExecuteNonQuery();
                new SqlCommand("delete from deletedInvoice", conn).ExecuteNonQuery();
                new SqlCommand("delete from dumpInvoiceCount", conn).ExecuteNonQuery();
                new SqlCommand("delete from invoiceDump", conn).ExecuteNonQuery();
                new SqlCommand("delete from invoiceRetail", conn).ExecuteNonQuery();
                new SqlCommand("delete from invoiceRetailDetail", conn).ExecuteNonQuery();
                new SqlCommand("delete from invoiceTerm", conn).ExecuteNonQuery();
                new SqlCommand("delete from invoiceType", conn).ExecuteNonQuery();
                new SqlCommand("delete from paidInvoice", conn).ExecuteNonQuery();
                new SqlCommand("delete from sale", conn).ExecuteNonQuery();
                new SqlCommand("delete from vehicle", conn).ExecuteNonQuery();
                new SqlCommand("delete from companyInvoice", conn).ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void pURCHASINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnSearchCredit(this, userH).Visible = true;
        }

        private void cREDITBILLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnSearchCredit(this, userH).Visible = true;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Enabled = true;
            new grnSearchCredit(this, userH).Visible = true;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            this.Enabled = true;
            //new accountList(this, userH).Visible = true;
        }

        private void cASHBOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Enabled = true;
            new cashBook(this, userH).Visible = true;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoiceSearchCredit(this, userH).Visible = true;
        }

        private void iNVOICEQUEARYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoiceSearchQ(this, userH).Visible = true;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            new salary(userH).Visible = true;
        }

        private void oUTSTANDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cUSTOMERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerOutstanding2_(this, userH).Visible = true;
        }

        private void rECEPITSUMMERYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new invoiceCreditPaidedit(this, userH).Visible = true;
        }

        private void bYINVOICEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new customerOutstanding(this, userH).Visible = true;
        }

        private void cHEQUESPAYMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                Int32 i = 0;
                string cus = "";
                conn.Open();
                reader = new SqlCommand("select * from invoiceCreditPaid", conn).ExecuteReader();
                while (reader.Read())
                {
                    conn2.Open();
                    reader2 = new SqlCommand("select customerID from invoiceRetail where id='" + reader[1] + "'", conn2).ExecuteReader();
                    if (reader2.Read())
                    {
                        cus = reader2[0] + "";
                    }
                    conn2.Close();
                    i++;

                    conn2.Open();
                    //MessageBox.Show(amountD+"");
                    new SqlCommand("insert into receipt values('" + i + "','" + reader[6] + "','" + "" + "','" + cus + "','" + new amountByName().setAmountName(reader[3] + "") + "','" + reader[3] + "','" + "" + "','" + cus + "','" + "CASH" + "','" + userH + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();
                    conn2.Close();

                    conn2.Open();
                    new SqlCommand("insert into customerStatement values('" + "SETTELE-" + i + "','" + "Settelemnt for Invoice " + reader[1] + "','" + 0 + "','" + reader[3] + "','" + true + "','" + reader[6] + "','" + cus + "')", conn2).ExecuteNonQuery();
                    conn2.Close();
                }
                conn.Close();
                db.setCursoerDefault();
                MessageBox.Show("COMPLTED");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void cUSTOMERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierOutstanding2(this, userH).Visible = true;
        }

        private void bYGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new supplierOutstanding(this, userH).Visible = true;
        }

        private void aTTENDANCEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new attendance().Visible = true;
        }

        private void cHEQUEPRINTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            new ChequeView().Visible = true;
        }

        private void bARCODEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Enabled = true;
            new barcode(this).Visible = true;
        }

        private Int32 serverID, LocalID;

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                db.setCursoerWait();
                conn3.Open();
                reader3 = new SqlCommand("select * from item", conn3).ExecuteReader();
                while (reader3.Read())
                {
                    try
                    {
                        conn2.Open();
                        reader2 = new SqlCommand("select * from item where code='" + reader3[0] + "'", conn2).ExecuteReader();
                        if (!reader2.Read())
                        {
                            conn2.Close();
                            conn2.Open();
                            new SqlCommand("insert into item values('" + reader3[0] + "','" + reader3[1] + "','" + reader3[2] + "','" + reader3[3] + "','" + reader3[4] + "','" + reader3[5] + "','" + reader3[6] + "','" + reader3[7] + "','" + reader3[8] + "','" + reader3[9] + "','" + reader3[10] + "','" + reader3[11] + "')", conn2).ExecuteNonQuery();
                            conn2.Close();
                        }
                        conn2.Close();
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
                conn3.Close();
                bool matchInvoiceID = true;
                checkDuplicate = false;
                while (matchInvoiceID)
                {
                    loadInvoiceNoRetailFromServer();
                    if (checkDuplicate)
                    {
                        conn3.Open();
                        new SqlCommand("delete from invoiceRetail where id='" + invoiceMaxNo + "'", conn3).ExecuteNonQuery();
                        conn3.Close();
                    }
                    serverID = invoiceMaxNo;
                    loadInvoiceNoRetailFromLocal();
                    LocalID = invoiceMaxNo;
                    if (LocalID > serverID)
                    {
                        invoiceMaxNo = serverID;
                        if (invoiceMaxNo != 0)
                        {
                            conn2.Open();
                            reader2 = new SqlCommand("select * from invoiceRetail where id='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                            if (reader2.Read())
                            {
                                conn3.Open();
                                new SqlCommand("insert into invoiceRetail values('" + invoiceMaxNo + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "','" + reader2[8] + "','" + reader2[9] + "','" + reader2[10] + "','" + reader2[11] + "','" + reader2[12] + "','" + reader2[13] + "','" + reader2[14] + "')", conn3).ExecuteNonQuery();
                                conn3.Close();

                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from cashSummery where reason='" + "New Invoice" + "' and remark='" + "Invoice No-" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into cashSummery values('" + "New Invoice" + "','" + "Invoice No-" + invoiceMaxNo + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();

                                conn2.Open();
                                reader2 = new SqlCommand("select * from invoiceTerm where invoiceid='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into invoiceTerm values('" + invoiceMaxNo + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from cashBalance where id='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into cashBalance values('" + "Invoice R-" + invoiceMaxNo + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from bankAccountStatment where id='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into bankAccountStatment values('" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "','" + reader2[8] + "','" + reader2[9] + "','" + reader2[10] + "','" + reader2[11] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from vehicle where invoiceid='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into vehicle values('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from cashInvoiceRetail where invoiceid='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                if (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into cashInvoiceRetail values('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from creditInvoiceRetail where invoiceid='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into creditInvoiceRetail values ('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceid='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into chequeInvoiceRetail values ('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "','" + reader2[8] + "','" + reader2[9] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from customerStatement where reason='" + "R-" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into customerStatement values('" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from invoiceRetailDetail where invoiceID='" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into invoiceRetailDetail values ('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "','" + reader2[8] + "','" + reader2[9] + "','" + reader2[10] + "','" + reader2[11] + "','" + reader2[12] + "','" + reader2[13] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                                conn2.Open();
                                reader2 = new SqlCommand("select * from itemStatement where invoiceID='" + "R-" + invoiceMaxNo + "'", conn2).ExecuteReader();
                                while (reader2.Read())
                                {
                                    conn3.Open();
                                    new SqlCommand("insert into itemStatement values('" + "R-" + invoiceMaxNo + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[4] + "','" + reader2[5] + "','" + reader2[6] + "','" + reader2[7] + "')", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                    conn3.Open();
                                    new SqlCommand("update item set qty=qty-'" + reader2[4] + "' where code='" + reader2[2] + "'", conn3).ExecuteNonQuery();
                                    conn3.Close();
                                }
                                conn2.Close();
                            }
                            else
                            {
                                checkDuplicate = true;
                                conn3.Open();
                                new SqlCommand("insert into invoiceRetail values('" + invoiceMaxNo + "','" + "" + "','" + true + "','" + 0 + "','" + DateTime.Now + "','" + true + "','" + "" + "','" + DateTime.Now + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + 0 + "','" + 0 + "','" + "" + "')", conn3).ExecuteNonQuery();
                                conn3.Close();
                            }
                            conn2.Close();
                        }
                        conn3.Open();
                        new SqlCommand("delete from cashSummery", conn3).ExecuteReader();
                        conn3.Close();
                        conn2.Open();
                        reader2 = new SqlCommand("select * from cashSummery ", conn2).ExecuteReader();
                        while (reader2.Read())
                        {
                            conn3.Open();
                            new SqlCommand("insert into cashSummery values('" + reader2[0] + "','" + reader2[1] + "','" + reader2[2] + "','" + reader2[3] + "','" + reader2[3] + "')", conn3).ExecuteNonQuery();
                            conn3.Close();
                        }
                        conn2.Close();
                    }
                    else
                    {
                        matchInvoiceID = false;
                    }
                }
                db.setCursoerDefault();
                MessageBox.Show("ok");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
            }
        }

        private Int32 invoiceMaxNo;
        private bool checkDuplicate = false;
        private string invoiceNo;

        private void loadInvoiceNoRetailFromServer()
        {
            try
            {
                invoiceMaxNo = 0;

                conn3.Open();
                reader3 = new SqlCommand("select max(id) from invoiceRetail", conn3).ExecuteReader();
                if (reader3.Read())
                {
                    invoiceMaxNo = reader3.GetInt32(0);
                }
                invoiceMaxNo++;

                reader3.Close();
                conn3.Close();
            }
            catch (Exception)
            {
                invoiceMaxNo = 0;

                reader3.Close();
                conn3.Close();
            }
        }

        private void loadInvoiceNoRetailFromLocal()
        {
            try
            {
                invoiceMaxNo = 0;

                conn2.Open();
                reader2 = new SqlCommand("select max(id) from invoiceRetail", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    invoiceMaxNo = reader2.GetInt32(0);
                }
                invoiceMaxNo++;

                reader2.Close();
                conn2.Close();
            }
            catch (Exception)
            {
                invoiceMaxNo = 0;

                reader2.Close();
                conn2.Close();
            }
        }

        private bool creditDetailB, chequeDetailB, cardDetailB, cashDetailB;

        public void loadInvoice(string id)
        {
            try
            {
                creditDetailB = false;
                chequeDetailB = false;
                cardDetailB = false;
                conn.Open();

                reader = new SqlCommand("select * from creditInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                if (reader.Read())
                {
                    creditDetailB = true;
                }
                conn2.Close();

                conn2.Open();
                reader2 = new SqlCommand("select * from chequeInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();
                while (reader2.Read())
                {
                    chequeDetailB = true;
                }
                conn2.Close();

                conn2.Open();
                reader2 = new SqlCommand("select * from cardInvoiceRetail where invoiceID='" + id + "' ", conn2).ExecuteReader();

                while (reader2.Read())
                {
                    cardDetailB = true;
                }
                conn2.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalied Invoice ID " + a.Message + " //" + a.StackTrace);
                conn2.Close();
            }
        }

        private void button10_Click_2(object sender, EventArgs e)
        {
            button10_Click_1(null, null);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                new returnInvoice(this, "", textBox1.Text).Visible = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CHEQUE").Visible = true;
        }

        private void nEWINVOICECASHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CHEQUE").Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CREDIT").Visible = true;
        }

        private void nEWINVOICECHEQUEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CREDIT").Visible = true;
        }

        private void nEWINVOICECREDITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CARD").Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            new invoiceNew(this, userH, "CARD").Visible = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
        }
    }
}