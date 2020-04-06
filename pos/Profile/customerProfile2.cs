using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class customerProfile2 : Form
    {
        public customerProfile2(invoiceNew home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        private DB db, db2;
        private invoiceNew home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private ArrayList arrayList;
        private string[] mobileNoArray;
        private bool states;
        private string user;

        private void itemProfile_Load(object sender, EventArgs e)
        {
            comboHoliday.SelectedIndex = 0;
            inTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            outTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            maxLate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            maxOt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            inTime.Format = DateTimePickerFormat.Custom;
            inTime.CustomFormat = "HH:mm"; // Only use hours and minutes
            inTime.ShowUpDown = true;
            outTime.Format = DateTimePickerFormat.Custom;
            outTime.CustomFormat = "HH:mm"; // Only use hours and minutes
            outTime.ShowUpDown = true;
            maxLate.Format = DateTimePickerFormat.Custom;
            maxLate.CustomFormat = "HH:mm"; // Only use hours and minutes
            maxLate.ShowUpDown = true;
            maxOt.Format = DateTimePickerFormat.Custom;
            maxOt.CustomFormat = "HH:mm"; // Only use hours and minutes
            maxOt.ShowUpDown = true;
            TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();
            ActiveControl = companyC;
            companyC.Focus();
            addressC.CharacterCasing = CharacterCasing.Upper;
            addressS.CharacterCasing = CharacterCasing.Upper;
            companyS.CharacterCasing = CharacterCasing.Upper;
            companyC.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;
            brand.CharacterCasing = CharacterCasing.Upper;
            description.CharacterCasing = CharacterCasing.Upper;
            companyCo.CharacterCasing = CharacterCasing.Upper;
            addressCo.CharacterCasing = CharacterCasing.Upper;
            dataGridView1.AllowUserToAddRows = false;
            getID();
            getStaff();
        }

        private void loadUser()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select * from users where username='" + user + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    button2.Enabled = reader.GetBoolean(4);
                    sAVECURRENTITEMToolStripMenuItem.Enabled = reader.GetBoolean(4);
                    button3.Enabled = reader.GetBoolean(5);
                    dELETECURRENTITEMToolStripMenuItem.Enabled = reader.GetBoolean(5);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private string tempCustomer;

        public bool loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    companyC.Text = reader.GetString(2);
                    addressC.Text = reader.GetString(3);
                    mobileNumberC.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    contat1Name.Text = reader[13] + "";
                    contact2Name.Text = reader[14] + "";
                    contact3Name.Text = reader[15] + "";
                    contact2.Text = reader[16] + "";
                    contact3.Text = reader[17] + "";
                    db.ToTitleCase(new TextBox[] { companyC, addressC, mobileNumberC, contat1Name, contact2Name, contact3Name });
                    ID.Text = reader[0].ToString().Split('-')[1] + "";
                }
                else
                {
                    states = false;
                    tempCustomer = "";
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                addressC.Focus();
            }
            catch (Exception)
            {
                conn.Close();
            }
            return states;
        }

        public bool loadCompany(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    companyCo.Text = reader.GetString(2);
                    addressCo.Text = reader.GetString(3);
                    mobileCo.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    db.ToTitleCase(new TextBox[] { companyCo, addressCo, mobileNumberC, addressCo });
                }
                else
                {
                    states = false;
                    tempCustomer = "";
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                companyCo.Focus();
            }
            catch (Exception A)
            {
                MessageBox.Show(A.Message);
                conn.Close();
            }
            return states;
        }

        public bool loadStaff(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select empid,name,residentialAddress,mobileNUmber,type,epfbasic,bankingAmount,epf,etf,epf12,resgin,incentive,allowances,meal,gross,resgin,bouns,punish,holiday,advanced,NIC,isEpf,isExecutive,epfNO from emp where empid='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    companyS.Text = reader.GetString(1);
                    addressS.Text = reader.GetString(2);
                    mobileS.Text = reader.GetString(3);
                    salary.Text = reader[5] + "";
                    bankingAmount.Text = reader[6] + "";
                    epf.Text = reader[7] + "";
                    epf_.Text = reader[9] + "";
                    etf.Text = reader[8] + "";
                    checkBox1.Checked = reader.GetBoolean(10);
                    fixedIncntive.Text = reader[11] + "";
                    allowances.Text = reader[12] + "";
                    mealAllowance.Text = reader[13] + "";
                    grossSalary.Text = reader[14] + "";
                    bonus.Text = reader[16] + "";
                    punish.Text = reader[17] + "";
                    comboHoliday.SelectedItem = reader.GetString(18);
                    category.Text = "";
                    advanced.Text = reader[19] + "";
                    nic.Text = reader[20] + "";
                    checkBoxEpfPay.Checked = reader.GetBoolean(21);
                    checkBoxeXCECUTIVE.Checked = reader.GetBoolean(22);
                    epfNo.Text = reader[23] + "";
                    db.ToTitleCase(new TextBox[] { companyS, addressS, mobileS });
                }
                else
                {
                    states = false;
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                companyS.Focus();
                attendanceNo.Text = id + "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                conn.Close();
            }
            return states;
        }

        public bool loadItem(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from item where code='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    //  codeC = id;
                    isItem.Checked = reader.GetBoolean(13);
                    isSparePart.Checked = reader.GetBoolean(14);
                    brand.Text = reader.GetString(1);
                    //  nameC.Text = reader.GetString(1);
                    category.Text = reader.GetString(2);
                    description.Text = reader.GetString(3);
                    remark.Text = reader.GetString(4);
                    costPrice.Text = reader[6] + "";
                    sellingPrice.Text = reader[7] + "";
                    discount.Text = (100 - (reader.GetDouble(6) * 100) / reader.GetDouble(7)) + "";
                    //addressC.SelectionLength = addressC.TextLength;
                    db.ToTitleCase(new TextBox[] { description, brand });
                }
                else
                {
                    states = false;
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                brand.Focus();
            }
            catch (Exception)
            {
                conn.Close();
            }
            return states;
        }

        private string cutomerID = "";

        private void save()
        {
            try
            {
                conn.Open();
                if (tabControl1.SelectedIndex == 0)
                {
                    if (companyC.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Name for a Customer");
                        companyC.Focus();
                    }
                    else if (MessageBox.Show("Are You Sure? ", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        new SqlCommand("delete from customer where id='" + "C-" + ID.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into customer values ('" + "C-" + ID.Text + "','" + "" + "','" + companyC.Text + "','" + addressC.Text + "','" + mobileNumberC.Text + "','" + "" + "','" + companyC.Text + " " + mobileNumberC.Text + "','" + "" + "','" + "" + "','" + checkBoxEpfPay.Checked + "','" + checkBoxeXCECUTIVE.Checked + "','" + epfNo.Text + "','" + contat1Name.Text + "','" + contact2Name.Text + "','" + contact3Name.Text + "','" + contact2.Text + "','" + contact3.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();
                        string message = "Greetings " + companyC.Text.Trim() + ", Welcome to the Car West Auto Service.";
                        SMSSender.SendWebRequest(mobileNumberC.Text.Trim(), message);
                        ID.Focus();
                        cutomerID = "C-" + ID.Text;
                        getID();
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (companyS.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Name for a Staff Member");
                        companyS.Focus();
                    }
                    else if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        new SqlCommand("delete from emp where empid='" + attendanceNo.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("insert into emp values ('" + companyS.Text + "','" + addressS.Text + "','" + mobileS.Text + "','" + attendanceNo.Text + "','" + salary.Text + "','" + bankingAmount.Text + "','" + epf.Text + "','" + etf.Text + "','" + "" + "','" + epf_.Text + "','" + fixedIncntive.Text + "','" + allowances.Text + "','" + mealAllowance.Text + "','" + grossSalary.Text + "','" + checkBox1.Checked + "','" + bonus.Text + "','" + punish.Text + "','" + comboHoliday.SelectedItem + "','" + advanced.Text + "','" + loan.Text + "','" + nic.Text + "','" + checkBoxEpfPay.Checked + "','" + checkBoxeXCECUTIVE.Checked + "','" + epfNo.Text + "')", conn).ExecuteNonQuery();
                        conn.Close();

                        ID.Focus();
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (companyCo.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Name for a Company");
                        companyCo.Focus();
                    }
                    else if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        new SqlCommand("delete from supplier where id='" + "S-" + IDS.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();

                        conn.Open();
                        new SqlCommand("insert into supplier values ('" + "S-" + IDS.Text + "','" + "" + "','" + companyCo.Text + "','" + addressCo.Text + "','" + mobileCo.Text + "','" + "" + "','" + companyCo.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                        conn.Close();

                        IDS.Focus();
                        getID2();
                    }
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    if (code.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Code");
                        code.Focus();
                    }
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                    {
                        reader = new SqlCommand("select * from item where code='" + code.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set sparePart='" + isSparePart.Checked + "',remark='" + remark.Text + "',brand='" + brand.Text + "',categorey='" + category.Text + "',description='" + description.Text + "',purchasingPrice='" + costPrice.Text + "',retailPrice='" + sellingPrice.Text + "',detail='" + db.setItemDescriptionName(new TextBox[] { code, brand, category, description, remark }) + "',isItem='" + isItem.Checked + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into item values ('" + code.Text + "','" + brand.Text + "','" + category.Text + "','" + description.Text + "','" + remark.Text + "','" + "" + "','" + costPrice.Text + "','" + sellingPrice.Text + "','" + 0 + "','" + 0 + "','" + "" + "','" + db.setItemDescriptionName(new TextBox[] { code, brand, category, description, remark }) + "','" + 0 + "','" + isItem.Checked + "','" + isSparePart.Checked + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        code.Focus();
                    }
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    //   MessageBox.Show("sa");
                    if (accountNumber.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Account Number");
                        accountNumber.Focus();
                    }
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                    {
                        reader = new SqlCommand("select * from bank where bankCode='" + accountNumber.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("update bank set BANKNAME='" + bankName.Text + "',dayLimit='" + dayLimit.Text + "' where bankCode='" + accountNumber.Text + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {
                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into bank values ('" + bankName.Text + "','" + accountNumber.Text + "','" + dayLimit.Text + "')", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        code.Focus();
                    }
                }
                else if (tabControl1.SelectedIndex == 5)
                {
                    if ((MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                    {
                        conn.Close();
                        conn.Open();
                        new SqlCommand("insert into bank values ('" + bankNormal.Text + "','" + 0 + "','" + 0 + "')", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                }
                conn.Close();
                MessageBox.Show("Saved Succefully");
                refresh();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                conn.Close();
            }
        }

        private void itemProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            home.Enabled = true;
            home.TopMost = true;
            home.cutomerID = cutomerID;
            home.loadCustomer(cutomerID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            save();
        }

        private void aDDNEWITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private int idTemp = 0;

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

                ID.Text = idTemp + "";
                companyC.Focus();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }

        private void getStaff()
        {
            try
            {
                conn2.Open();
                reader2 = new SqlCommand("select max(empid) from emp", conn2).ExecuteReader();
                if (reader2.Read())
                {
                    idTemp = reader2.GetInt32(0);
                    idTemp++;
                }
                conn2.Close();

                attendanceNo.Text = idTemp + "";
                companyS.Focus();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }

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

                IDS.Text = idTemp + "";
                companyCo.Focus();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (companyC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, company Name Cant Be Empty Value");
                companyC.Focus();
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    db.setCursoerWait();
                    getID();
                    conn.Open();
                    new SqlCommand("insert into customer values ('" + "C-" + idTemp + "','" + "" + "','" + companyC.Text + "','" + addressC.Text + "','" + mobileNumberC.Text + "','" + "" + "','" + "C-" + idTemp + " " + companyC.Text + "','" + "" + "','" + "" + "','" + idTemp + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    states = true;
                    db.setCursoerDefault();
                }
                catch (Exception)
                {
                    conn.Close();
                }
                if (states)
                {
                    MessageBox.Show("Saved Succefully");
                    refresh();
                    companyC.Focus();
                }
            }
        }

        //++++++ My Method Start+++
        private void loadAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select id from customer ", conn).ExecuteReader();
                arrayList = new ArrayList();
                // MessageBox.Show("1 ");
                while (reader.Read())
                {
                    // MessageBox.Show("3");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                // MessageBox.Show("2");
                reader.Close();
                conn.Close();

                conn.Open();
                reader = new SqlCommand("select description from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                mobileNoArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(description, mobileNoArray);

                conn.Open();
                reader = new SqlCommand("select categorey from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                mobileNoArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(category, mobileNoArray);
            }
            catch (Exception)
            {
                conn.Close();
            }
            category.CharacterCasing = CharacterCasing.Upper;
        }

        private void refresh()
        {
            try
            {
                remark.Text = "";
                category.Text = "";
                // cutomerID = "";
                if (tabControl1.SelectedIndex == 0)
                {
                    ID.Focus();
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    attendanceNo.Focus();
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    getID2();
                    IDS.Focus();
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    code.Focus();
                }
                isItem.Checked = false;
                isSparePart.Checked = false;
                checkBox1.Checked = false;
                tempCustomer = "";
                checkBoxeXCECUTIVE.Checked = false;
                checkBoxEpfPay.Checked = false;
                epfNo.Text = "";
                checkBox1.Checked = false;
                comboHoliday.SelectedIndex = 0;
                checkBox2.Checked = false;
                db.setTextBoxDefault(new TextBox[] { nic, contact2, contact3, contact2Name, contact3Name, contat1Name, advanced, loan, grossSalary, fixedIncntive, nicName, allowances, mealAllowance, bonus, bankingAmount, punish, creditPeriod, accountNumber, dayLimitValue, dayLimit, bankName, IDS, ID, discount, mealAllowance, fixedIncntive, allowances, attendanceNo, epf_, companyC, addressC, code, brand, description, costPrice, sellingPrice, mobileNumberC, companyS, addressS, mobileS, salary, epf, etf, bankingAmount, companyCo, addressCo, mobileCo });
                companyC.Focus();
                inTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                outTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                maxLate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                maxOt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                listBox1.Visible = false;
                loadAutoComplete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //++++++ My Method End++++

        private void brand_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void remark_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void rate_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void brand_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void remark_TextChanged(object sender, EventArgs e)
        {
        }

        private void remark_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void rate_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void sAVECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (companyC.Text.Equals(""))
                {
                    MessageBox.Show("Invalied customer ID to Delete");
                    companyC.Focus();
                }
                else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        db.setCursoerWait();
                        conn.Open();
                        new SqlCommand("delete from customer where id='" + "C-" + ID.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Delete Succesfully");
                        refresh();
                        db.setCursoerDefault();
                        companyC.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sorry You Have enterd Invalied customer Code");
                        companyC.Focus();
                        conn.Close();
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (companyS.Text.Equals(""))
                {
                    MessageBox.Show("Invalied Staff for Delete");
                    companyS.Focus();
                }
                else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        db.setCursoerWait();
                        conn.Open();
                        new SqlCommand("delete from emp where empid='" + attendanceNo.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Delete Succesfully");
                        refresh();
                        db.setCursoerDefault();
                        companyS.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sorry You Have enterd Invalied Staff");
                        companyS.Focus();
                        conn.Close();
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (companyCo.Text.Equals(""))
                {
                    MessageBox.Show("Invalied Company Name to Delete");
                    companyCo.Focus();
                }
                else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        db.setCursoerWait();
                        conn.Open();
                        new SqlCommand("delete from supplier where id='" + IDS.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Delete Succesfully");
                        refresh();
                        db.setCursoerDefault();
                        companyCo.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sorry You Have enterd Invalied Supplier");
                        companyCo.Focus();
                        conn.Close();
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Invalied Item Code to Delete");
                    companyC.Focus();
                }
                else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    try
                    {
                        db.setCursoerWait();
                        conn.Open();
                        new SqlCommand("delete from item where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Delete Succesfully");
                        refresh();
                        db.setCursoerDefault();
                        code.Focus();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sorry You Have enterd Invalied Item Code");
                        code.Focus();
                        conn.Close();
                    }
                }
            }
        }

        private void dELETECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyC.Text = listBox1.SelectedItem.ToString().Split(' ')[1];
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyC.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            loadCustomer(listBox1.SelectedItem.ToString().Split(' ')[0]);
        }

        private void itemProfile_MouseHover(object sender, EventArgs e)
        {
        }

        private void itemProfile_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
        }

        private void itemProfile_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void rEFRESHToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            db.setTextBoxDefault(new TextBox[] { companyC, addressC, mobileNumberC });
            companyC.Focus();
            loadAutoComplete(); listBox1.Visible = false;
        }

        private void Code_KeyDown_1(object sender, KeyEventArgs e)
        {
        }

        private void codeC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 | e.KeyValue == 12)
            {
                loadCustomer(idTemp + "");
                companyC.Focus();
            }
            else if (e.KeyValue == 40)
            {
                companyC.Focus();
            }
        }

        private void codeC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (idTemp == 0)
            {
                MessageBox.Show("Empty customer Code to Search");
                companyC.Focus();
            }
            else
            {
                db.setTextBoxDefault(new TextBox[] { companyC, addressC, mobileNumberC });

                if (!loadCustomer(idTemp + ""))
                {
                    MessageBox.Show("Sorry You Have Enterd Invalied customer Code");
                    companyC.Focus();
                }
                else
                {
                    MessageBox.Show("User Detail Download Succesfully");
                    companyC.Focus();
                }
            }
        }

        private void nameC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void nameC_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void companyC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (companyC.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    companyC.Focus();
                }
                else
                {
                    loadCustomer(companyC.Text);
                    addressC.Focus();
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
                        addressC.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void companyC_KeyUp(object sender, KeyEventArgs e)
        {
            tempCustomer = "";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyC.Text.Equals("")))
            {
                db.setList(listBox1, companyC, companyC.Width);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,description from customer where description like '%" + companyC.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
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
            if (companyC.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void addressC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(companyC, mobileNumberC, mobileNumberC, e.KeyValue);
        }

        private void mobileNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                addressC.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue == 40)
            {
                dayLimitValue.Focus();
            }
        }

        private void landNumberC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void faxNumberC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void emailC_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                getID();
                ID.Focus();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                attendanceNo.Focus();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                getID2();
                IDS.Focus();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                code.Focus();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                accountNumber.Focus();
            }
        }

        private void mobileS_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(nicName, nic, nic, e.KeyValue);
        }

        private void companyS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                if (companyS.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Staff Member");
                    companyS.Focus();
                }
                else
                {
                    addressS.Focus();
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
                        addressS.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (e.KeyValue == 38)
            {
                attendanceNo.Focus();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void addressS_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(companyS, nicName, nicName, e.KeyValue);
        }

        private void addressS_TextChanged(object sender, EventArgs e)
        {
        }

        private void salary_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(bankingAmount, epf, epf, e.KeyValue);
        }

        private void bankingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(salary, epf, epf, e.KeyValue);
        }

        private void epf_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(salary, epf_, epf_, e.KeyValue);
        }

        private void etf_TextChanged(object sender, EventArgs e)
        {
        }

        private void etf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                epf_.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue == 40)
            {
                fixedIncntive.Focus();
            }
        }

        private void companyS_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyS.Text.Equals("")))
            {
                db.setList(listBox2, companyS, companyS.Width);

                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select empid,name from emp where name like '%" + companyS.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox2.Visible = true;
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
            if (companyS.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox2.Visible = false;
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox2.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyS.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                loadStaff(listBox2.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            listBox2.Visible = false;
            loadStaff(listBox2.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyS.Text = listBox2.SelectedItem.ToString().Split(' ')[0];
        }

        private void companyCo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                if (companyCo.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Company Name");
                    companyCo.Focus();
                }
                else
                {
                    loadCompany(companyCo.Text);
                    addressCo.Focus();
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox3.Visible)
                    {
                        listBox3.Focus();
                        listBox3.SelectedIndex = 0;
                    }
                    else
                    {
                        addressCo.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void companyCo_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyCo.Text.Equals("")))
            {
                db.setList(listBox3, companyCo, companyCo.Width);

                try
                {
                    listBox3.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,description from supplier where description like '%" + companyCo.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox3.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox3.Visible = true;
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
            if (companyCo.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox3.Visible = false;
            }
        }

        private void addressCo_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(companyCo, mobileCo, mobileCo, e.KeyValue);
        }

        private void mobileCo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                addressCo.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox3.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyCo.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                loadCompany(listBox3.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            listBox3.Visible = false;

            loadCompany(listBox3.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyCo.Text = listBox3.SelectedItem.ToString();
        }

        private void companyC_TextChanged(object sender, EventArgs e)
        {
        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox4.Visible = false;
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item Code");
                    code.Focus();
                }
                else
                {
                    loadItem(code.Text);
                    brand.Focus();
                }
            }
            else if (e.KeyValue == 40)
            {
                try
                {
                    if (listBox4.Visible)
                    {
                        listBox4.Focus();
                        listBox4.SelectedIndex = 0;
                    }
                    else
                    {
                        brand.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void brand_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, category, category, e.KeyValue);
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(category, remark, remark, e.KeyValue);
        }

        private void costPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                discount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
            else if (e.KeyValue == 40)
            {
                //discount.Focus();
            }
        }

        private void sellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                remark.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                discount.Focus();
            }
            else if (e.KeyValue == 40)
            {
                discount.Focus();
            }
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                db.setList(listBox4, code, code.Width * 2);

                try
                {
                    listBox4.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select detail,code from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox4.Items.Add(reader[0].ToString().ToUpper());
                        listBox4.Visible = true;
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
            if (code.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox4.Visible = false;
            }
        }

        private void listBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox4.SelectedIndex == 0 && e.KeyValue == 38)
            {
                code.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox4.Visible = false;
                loadItem(listBox4.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        private void listBox4_MouseClick(object sender, MouseEventArgs e)
        {
            listBox4.Visible = false;

            loadItem(listBox4.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.Text = listBox4.SelectedItem.ToString().Split(' ')[0];
        }

        private void mobileNumberC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void mobileS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void mobileCo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void costPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void attendanceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox2.Visible = false;
                if (attendanceNo.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Staff Member");
                    attendanceNo.Focus();
                }
                else
                {
                    loadStaff(attendanceNo.Text);
                    companyS.Focus();
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
                        companyS.Focus();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void attendanceNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | attendanceNo.Text.Equals("")))
            {
                db.setList(listBox2, attendanceNo, attendanceNo.Width * 3);

                try
                {
                    listBox2.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select empid,name from emp where empid like '%" + attendanceNo.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader[0].ToString().ToUpper() + " " + reader[1].ToString().ToUpper());
                        listBox2.Visible = true;
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
            if (attendanceNo.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox2.Visible = false;
            }
        }

        private void epf__KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                epf.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue == 40)
            {
                etf.Focus();
            }
        }

        private void epf__KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void discount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                sellingPrice.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                try
                {
                    costPrice.Text = (Double.Parse(sellingPrice.Text) / 100) * (100 - Double.Parse(discount.Text)) + "";
                }
                catch (Exception)
                {
                }
                costPrice.Focus();
            }
            else if (e.KeyValue == 40)
            {
                costPrice.Focus();
            }
        }

        private void ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (ID.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Customer");
                    ID.Focus();
                }
                else
                {
                    loadCustomer("C-" + ID.Text);
                    companyC.Focus();
                }
            }
            else if (e.KeyValue == 40)
            {
                companyC.Focus();
            }
        }

        private void IDS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                if (IDS.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied sUPPLIER");
                    IDS.Focus();
                }
                else
                {
                    loadCompany("S-" + IDS.Text);
                    companyCo.Focus();
                }
            }
            else if (e.KeyValue == 40)
            {
                companyCo.Focus();
            }
        }

        private void IDS_TextChanged(object sender, EventArgs e)
        {
        }

        private void accountNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                try
                {
                    conn.Open();
                    reader = new SqlCommand("select * from bank where bankCode='" + accountNumber.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        bankName.Text = reader[1] + "";
                        dayLimit.Text = reader[3] + "";
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
            }
            db.setTextBoxPath(accountNumber, bankName, bankName, e.KeyValue);
        }

        private void bankName_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(accountNumber, dayLimit, dayLimit, e.KeyValue);
        }

        private void dayLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                save();
            }
            else if (e.KeyValue == 38)
            {
                bankName.Focus();
            }
        }

        private void mobileNumberC_CursorChanged(object sender, EventArgs e)
        {
        }

        private void dayLimitValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                mobileNumberC.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue == 40)
            {
                creditPeriod.Focus();
            }
        }

        private void checkBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                dayLimitValue.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void creditPeriod_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void creditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                dayLimitValue.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue == 40)
            {
                checkBox2.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label21_Click(object sender, EventArgs e)
        {
        }

        private void label27_Click(object sender, EventArgs e)
        {
        }

        private void epf_TextChanged(object sender, EventArgs e)
        {
        }

        private void fixedIncntive_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(etf, allowances, allowances, e.KeyValue);
        }

        private void allowances_ImeModeChanged(object sender, EventArgs e)
        {
        }

        private void allowances_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(fixedIncntive, mealAllowance, mealAllowance, e.KeyValue);
        }

        private void mealAllowance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                allowances.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                checkBox1.Focus();
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                mealAllowance.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                loan.Focus();
            }
        }

        private void bonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                advanced.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                punish.Focus();
            }
        }

        private void punish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                bonus.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                comboHoliday.Focus();
            }
        }

        private void comboHoliday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                comboHoliday.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                save();
            }
        }

        private void bankingAmount_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(nic, salary, salary, e.KeyValue);
        }

        private double temp = 0;

        private void setGross()
        {
            try
            {
                temp = 0;
                if (!salary.Text.Equals(""))
                {
                    temp = temp + Double.Parse(salary.Text);
                }
                if (!bankingAmount.Text.Equals(""))
                {
                    temp = temp + Double.Parse(bankingAmount.Text);
                }
                if (!fixedIncntive.Text.Equals(""))
                {
                    temp = temp + Double.Parse(fixedIncntive.Text);
                }
                if (!allowances.Text.Equals(""))
                {
                    temp = temp + Double.Parse(allowances.Text);
                }
                if (!mealAllowance.Text.Equals(""))
                {
                    temp = temp + Double.Parse(mealAllowance.Text);
                }
                grossSalary.Text = temp + "";
                //   temp=
            }
            catch (Exception)
            {
                grossSalary.Text = "0";
            }
        }

        private void bankingAmount_KeyUp(object sender, KeyEventArgs e)
        {
            setGross();
        }

        private void salary_KeyUp(object sender, KeyEventArgs e)
        {
            setGross();
        }

        private void loan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                checkBox1.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                advanced.Focus();
            }
        }

        private void advanced_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                loan.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 || e.KeyValue == 40)
            {
                bonus.Focus();
            }
        }

        private void label41_Click(object sender, EventArgs e)
        {
        }

        private void nic_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(mobileS, bankingAmount, bankingAmount, e.KeyValue);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if ((MessageBox.Show("Are you Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    new SqlCommand("update item set qty='" + 0 + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from itemStatement where itemCode='" + code.Text + "'", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        conn2.Open();
                        new SqlCommand("insert into itemStatement_ values ('" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "')", conn2).ExecuteNonQuery();
                        conn2.Close();
                    }
                    conn.Close();
                    conn.Open();

                    new SqlCommand("delete from itemStatement  where itemCode='" + code.Text + "'", conn).ExecuteNonQuery();
                    conn.Close();
                }
                conn.Close();
                MessageBox.Show("Cleard Succefully");
                refresh();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message + "/" + a.StackTrace);
                conn.Close();
            }
        }

        private void nicName_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(addressS, mobileS, mobileS, e.KeyValue);
        }

        private void label44_Click(object sender, EventArgs e)
        {
        }

        private void category_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void category_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(brand, description, description, e.KeyValue);
        }

        private void remark_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(description, sellingPrice, sellingPrice, e.KeyValue);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                conn.Open();
                reader = new SqlCommand("select * from item where categorey like '" + textBox1.Text + "%' order by id", conn).ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0], reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4], reader[12]);
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {
                        conn.Open();
                        new SqlCommand("update item set id='" + dataGridView1.Rows[i].Cells[2].Value + "' where code='" + dataGridView1.Rows[i].Cells[0].Value + "'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                    }
                }
                try
                {
                    dataGridView1.Rows.Clear();
                    conn.Open();
                    reader = new SqlCommand("select * from item where categorey like '" + textBox1.Text + "%' order by id", conn).ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader[0], reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4], reader[12]);
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                }
                MessageBox.Show("Updated Succefully");
            }
            catch (Exception)
            {
                conn.Close();
            }
        }
    }
}