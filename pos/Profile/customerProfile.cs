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
    public partial class customerProfile : Form
    {
        public customerProfile(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        ArrayList arrayList;
        string[] mobileNoArray, LandNoArray, emailArray, companyArray;

        Boolean states;
        string user, codeC;

        // my Variable End


        private void itemProfile_Load(object sender, EventArgs e)
        {
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

            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();
            this.ActiveControl = companyC;
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


            getID();
        }
        void loadUser()
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
        string tempCustomer;
        public Boolean loadCustomer(string id)
        {

            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from customer where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    //  codeC = id;
                    companyC.Text = reader.GetString(2);
                    //  nameC.Text = reader.GetString(1);
                    addressC.Text = reader.GetString(3);
                    mobileNumberC.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    //addressC.SelectionLength = addressC.TextLength;
                    db.ToTitleCase(new TextBox[] { companyC, addressC, mobileNumberC });
                    ID.Text=reader[0].ToString().Split('-')[1]+"";
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
        public Boolean loadCompany(string id)
        {

            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    // code = id;
                    companyCo.Text = reader.GetString(2);
                    //  nameC.Text = reader.GetString(1);
                    addressCo.Text = reader.GetString(3);
                    mobileCo.Text = reader.GetString(4);
                    tempCustomer = reader[0] + "";
                    //addressC.SelectionLength = addressC.TextLength;
                    db.ToTitleCase(new TextBox[] { companyCo, addressCo, mobileNumberC, addressCo });
                }
                else
                {
                   // MessageBox.Show(id);
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

        public Boolean loadStaff(string id)
        {

            try
            {
                db.setCursoerWait();
                refresh();
                conn.Open();
                reader = new SqlCommand("select empid,name,residentialAddress,mobileNUmber,type,epfbasic,bankingAmount,epf,etf,epf12,resign from emp where empid='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {

                    states = true;
                    // codeC = id;
                    companyS.Text = reader.GetString(1);
                    //  nameC.Text = reader.GetString(1);
                    addressS.Text = reader.GetString(2);
                    mobileS.Text = reader.GetString(3);
                    salary.Text = reader[5] + "";
                    bankingAmount.Text = reader[6] + "";
                    epf.Text = reader[7] + "";
                    epf_.Text=reader[9]+"";
                    etf.Text = reader[8] + "";
                    checkBox1.Checked = reader.GetBoolean(10);
                    if (reader.GetString(4).Equals("timeBased"))
                    {
                        radioMonthly.Checked = true;
                        conn.Close();
                        conn.Open();
                        reader = new SqlCommand("select * from TimeBasedAttandance where empid='" + id + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            inTime.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[1].ToString().Split(':')[0]), Int32.Parse(reader[1].ToString().Split(':')[1]), 00);
                            outTime.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[2].ToString().Split(':')[0]), Int32.Parse(reader[2].ToString().Split(':')[1]), 00);
                            maxLate.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[3].ToString().Split(':')[0]), Int32.Parse(reader[3].ToString().Split(':')[1]), 00);
                            maxOt.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[4].ToString().Split(':')[0]), Int32.Parse(reader[4].ToString().Split(':')[1]), 00);

                        }
                        conn2.Close();
                    }
                    else
                    {
                        radioDay.Checked = true;
                        conn.Close();
                        conn.Open();
                        reader = new SqlCommand("select * from dayBasedAttandance where empid='" + id + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            inTime.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[1].ToString().Split(':')[0]), Int32.Parse(reader[1].ToString().Split(':')[1]), 00);
                            outTime.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[2].ToString().Split(':')[0]), Int32.Parse(reader[2].ToString().Split(':')[1]), 00);
                            maxLate.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[4].ToString().Split(':')[0]), Int32.Parse(reader[4].ToString().Split(':')[1]), 00);
                            maxOt.Value = new DateTime(2014, 12, 12, Int32.Parse(reader[3].ToString().Split(':')[0]), Int32.Parse(reader[3].ToString().Split(':')[1]), 00);

                        }
                        conn.Close();
                    }

                    //addressC.SelectionLength = addressC.TextLength;
                    db.ToTitleCase(new TextBox[] { companyS, addressS, mobileS });



                }
                else
                {
                    states = false;
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                addressS.Focus();
                attendanceNo.Text = id + "";
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message+"/"+a.StackTrace);
                conn.Close();
            }
            return states;
        }
        public Boolean loadItem(string id)
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
                    brand.Text = reader.GetString(1);
                    //  nameC.Text = reader.GetString(1);
                    description.Text = reader.GetString(2);
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
        void save()
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
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {
                      //  MessageBox.Show(tempCustomer);

                       // conn.Open();
                        new SqlCommand("delete from customer where id='" + "C-" + ID.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();

                       
                            conn.Open();
                            new SqlCommand("insert into customer values ('" + "C-"+ID.Text + "','" + "" + "','" + companyC.Text + "','" + addressC.Text + "','" + mobileNumberC.Text + "','" + "" + "','" + companyC.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                            conn.Close();

                        
                        ID.Focus();
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (companyS.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Name for a Staff Member");
                        companyS.Focus();
                    }
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {

                        string type = "";
                        if (radioDay.Checked)
                        {
                            type = "dayBased";
                        }
                        else
                        {
                            type = "timeBased";
                        }

                        reader = new SqlCommand("select * from emp where empid='" + attendanceNo.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {

                            conn.Close();
                            conn.Open();
                            new SqlCommand("update emp set resign='" + checkBox1.Checked+ "',name='" + companyS.Text + "',residentialAddress='" + addressS.Text + "',mobileNUmber='" + mobileS.Text + "',type='" + type + "',epfBasic='" + salary.Text + "',bankingAmount='" + bankingAmount.Text + "',epf='" + epf.Text + "',etf='" + etf.Text + "',epf12='" + epf_.Text + "' where empid='" + attendanceNo.Text + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {

                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into emp values ('" + companyS.Text + "','" + addressS.Text + "','" + mobileS.Text + "','" + attendanceNo.Text + "','" + salary.Text + "','" + bankingAmount.Text + "','" + epf.Text + "','" + etf.Text + "','" + type + "','"+epf_.Text+"','"+checkBox1.Checked+"')", conn).ExecuteNonQuery();
                            conn.Close();

                        }
                        attendanceNo.Focus();
                        conn.Open();
                        if (radioMonthly.Checked)
                        {
                            new SqlCommand("delete from TimeBasedAttandance where empid='" + attendanceNo.Text + "'", conn).ExecuteNonQuery();
                            new SqlCommand("insert into TimeBasedAttandance values('" + inTime.Value.ToString("H:mm") + "','" + outTime.Value.ToString("H:mm") + "','" + maxLate.Value.ToString("H:mm") + "','" + maxOt.Value.ToString("H:mm") + "','" + salary.Text + "','" + false + "','" + true + "','" + 0 + "','" + 0 + "','" + attendanceNo.Text + "','" + 0 + "','" + 0 + "','" + false + "','" + true + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "')", conn).ExecuteNonQuery();

                        }
                        else
                        {
                            new SqlCommand("delete from dayBasedAttandance where empid='" + attendanceNo.Text + "'", conn).ExecuteNonQuery();
                            new SqlCommand("insert into dayBasedAttandance values('" + inTime.Value.ToString("H:mm") + "','" + outTime.Value.ToString("H:mm") + "','" + maxOt.Value.ToString("H:mm") + "','" + maxLate.Value.ToString("H:mm") + "','" + salary.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + true + "','" + false + "','" + true + "','" + attendanceNo.Text + "','" + "0" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "','" + "0:00" + "')", conn).ExecuteNonQuery();

                        }

                        conn.Close();
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (companyCo.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Name for a Company");
                        companyCo.Focus();
                    }
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {

                        new SqlCommand("delete from supplier where id='" + "S-" + IDS.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();


                        conn.Open();
                        new SqlCommand("insert into supplier values ('" + "S-" + IDS.Text + "','" + "" + "','" + companyCo.Text + "','" + addressCo.Text + "','" + mobileCo.Text + "','" + "" + "','" + companyCo.Text + "','" + "" + "','" + "" + "')", conn).ExecuteNonQuery();
                        conn.Close();


                        IDS.Focus();
                    }
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    if (code.Text.Equals(""))
                    {
                        MessageBox.Show("Please Enter Code");
                        code.Focus();
                    }
                    else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                    {

                        reader = new SqlCommand("select * from item where code='" + code.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {

                            conn.Close();
                            conn.Open();
                            new SqlCommand("update item set brand='" + brand.Text + "',categorey='" + description.Text + "',purchasingPrice='" + costPrice.Text + "',retailPrice='" + sellingPrice.Text + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                            conn.Close();
                        }
                        else
                        {

                            conn.Close();
                            conn.Open();
                            new SqlCommand("insert into item values ('" + code.Text + "','" + brand.Text + "','" + description.Text + "','" + "" + "','" + "" + "','" + "" + "','" + costPrice.Text + "','" + sellingPrice.Text + "','" + 0 + "','" + 0 + "','" + "" + "','" + db.setItemDescriptionName(new TextBox[] { code, brand, description }) + "')", conn).ExecuteNonQuery();
                            conn.Close();

                        }
                        code.Focus();
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
        private void itemProfile_Activated(object sender, EventArgs e)
        {

        }

        private void itemProfile_Deactivate(object sender, EventArgs e)
        {

        }

        private void itemProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            save();
        }

        private void aDDNEWITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
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

                ID.Text = idTemp + "";
                companyC.Focus();
            }
            catch (Exception)
            {
                idTemp = 1;
                conn2.Close();
            }
        }
        void getID2()
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
        void loadAutoComplete()
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
                reader = new SqlCommand("select mobileNo from customer ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");

                }
                reader.Close();
                conn.Close();
                mobileNoArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(mobileNumberC, mobileNoArray);

            }
            catch (Exception)
            {
                conn.Close();
            }
        }


        void refresh()
        {
            try
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
                tabControl1.SelectedIndex = 0;
                
                checkBox1.Checked = false;
                tempCustomer = "";
                //loadAutoComplete();
                db.setTextBoxDefault(new TextBox[] { IDS,ID,discount,mealAllowance,fixedIncntive,allowances,attendanceNo, epf_,companyC, addressC, code, brand, description, costPrice, sellingPrice, mobileNumberC, companyS, addressS, mobileS, salary, epf, etf, bankingAmount, companyCo, addressCo, mobileCo });
                companyC.Focus();
                inTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                outTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                maxLate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                maxOt.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
           
                listBox1.Visible = false;
            }
            catch (Exception)
            {

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
                        new SqlCommand("delete from customer where id='" + "C-"+ID.Text + "'", conn).ExecuteNonQuery();
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
                        new SqlCommand("delete from staff where name='" + companyS.Text + "'", conn).ExecuteNonQuery();
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
                        listBox1.Items.Add(reader[0].ToString().ToUpper()+" "+reader[1].ToString().ToUpper());
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
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
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
        }

        private void mobileS_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(addressS, salary, salary, e.KeyValue);
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
            db.setTextBoxPath(companyS, mobileS, mobileS, e.KeyValue);
        }

        private void addressS_TextChanged(object sender, EventArgs e)
        {

        }

        private void salary_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(mobileS, bankingAmount, bankingAmount, e.KeyValue);
        }

        private void bankingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(salary, epf, epf, e.KeyValue);
        }

        private void epf_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(bankingAmount, etf, etf, e.KeyValue);
        }

        private void etf_TextChanged(object sender, EventArgs e)
        {

        }

        private void etf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                epf.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13 | e.KeyValue==40)
            {
                epf_.Focus();
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
                        listBox3.Items.Add(reader[0].ToString().ToUpper()+" "+reader[1].ToString().ToUpper());
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
            db.setTextBoxPath(code, description, description, e.KeyValue);
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(brand, sellingPrice, sellingPrice, e.KeyValue);
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
                description.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                discount.Focus();
            }
            else if (e.KeyValue==40)
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
                    reader = new SqlCommand("select detail from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
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
                etf.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                save();
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
                    costPrice.Text = (Double.Parse(sellingPrice.Text)/100)*(100-Double.Parse(discount.Text))+"";
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

                    loadCustomer("C-"+ID.Text);
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

    }
}
