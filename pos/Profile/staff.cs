using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class staff : Form
    {
        public staff(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader;
        private ArrayList arrayList;
        private string[] idArray, nameArray, addressArray, mobileNoArray, LandNoArray, emailArray, companyArray;

        private Boolean check, checkListBox, states;
        private string user, listBoxType;

        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();
            this.ActiveControl = codeC;
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

        public Boolean loadCustomer(string id)
        {
            try
            {
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select * from staff where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    codeC.Text = id;
                    states = true;
                    //companyC.Text = reader.GetString(2);
                    nameC.Text = reader.GetString(1);
                    addressC.Text = reader.GetString(2);
                    mobileNumberC.Text = reader.GetString(3);
                    salary.Text = reader[4] + "";
                    bankingAmount.Text = reader[5] + "";
                    epf.Text = reader[6] + "";
                    etf.Text = reader[7] + "";
                    //                  landNumberC.Text = reader.GetString(5);
                    //                emailC.Text = reader.GetString(7);
                    try
                    {
                        //                  faxNumberC.Text = reader.GetString(8);
                    }
                    catch (Exception)
                    {
                    }
                    //            db.ToTitleCase(new TextBox[] { companyC, nameC, addressC, mobileNumberC, landNumberC, emailC, faxNumberC });
                }
                else
                {
                    states = false;
                    nameC.Focus();
                }
                reader.Close();
                conn.Close();
                db.setCursoerDefault();
            }
            catch (Exception)
            {
                conn.Close();
            }
            return states;
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
            if (codeC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Code Number Cant Be Empty Value");
                codeC.Focus();
            }
            else if (salary.Text.Equals("") | epf.Text.Equals("") | etf.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Salary Detail Missing");
                salary.Focus();
            }
            else if (nameC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Name Cant Be Empty Value");
                nameC.Focus();
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    db.setCursoerWait();
                    states = true;
                    conn.Open();
                    reader = new SqlCommand("select * from staff where id='" + codeC.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update staff set name='" + nameC.Text + "',address='" + addressC.Text + "',mobileNo='" + mobileNumberC.Text + "',detail='" + db.setItemDescriptionCusSupp(new TextBox[] { codeC, nameC }) + "',salary='" + salary.Text + "',bankingAmount='" + bankingAmount.Text + "',epf='" + epf.Text + "',etf='" + etf.Text + "' where id='" + codeC.Text + "'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sorry Invalied Staff ID");
                        codeC.Focus();
                    }
                    conn.Close();

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
                    codeC.Focus();
                }
            }
        }

        private void aDDNEWITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (codeC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Code Number Cant Be Empty Value");
                codeC.Focus();
            }
            else if (nameC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Name Cant Be Empty Value");
                nameC.Focus();
            }
            else if (salary.Text.Equals("") | epf.Text.Equals("") | etf.Text.Equals(""))
            {
                MessageBox.Show("Sorry, Salary Detail Missing");
                salary.Focus();
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
 MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    db.setCursoerWait();
                    states = true;
                    conn.Open();
                    new SqlCommand("insert into staff values ('" + codeC.Text + "','" + nameC.Text + "','" + addressC.Text + "','" + mobileNumberC.Text + "','" + salary.Text + "','" + bankingAmount.Text + "','" + epf.Text + "','" + etf.Text + "','" + db.setItemDescriptionCusSupp(new TextBox[] { codeC, nameC }) + "')", conn).ExecuteNonQuery();
                    conn.Close();
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
                    codeC.Focus();
                }
            }
        }

        //++++++ My Method Start+++
        private void loadAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select id from staff ", conn).ExecuteReader();
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

                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(codeC, idArray);
                // MessageBox.Show("" + idArray.Length);
                conn.Open();
                reader = new SqlCommand("select name from staff ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                nameArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(nameC, nameArray);
                conn.Open();
                reader = new SqlCommand("select address from staff ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                addressArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(addressC, addressArray);

                conn.Open();
                reader = new SqlCommand("select mobileNo from staff ", conn).ExecuteReader();
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

        private void refresh()
        {
            try
            {
                loadAutoComplete();
                db.setTextBoxDefault(new TextBox[] { codeC, nameC, salary, addressC, mobileNumberC, bankingAmount, epf, etf });
                codeC.Focus();
                listBox2.Items.Clear();
                listBox1.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        //++++++ My Method End++++
        private void code_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void brand_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void category_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void description_KeyUp(object sender, KeyEventArgs e)
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

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void brand_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void category_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
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
            if (codeC.Text.Equals(""))
            {
                MessageBox.Show("Invalied Staff ID to Delete");
                codeC.Focus();
            }
            else if ((MessageBox.Show("Are You Sure ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    db.setCursoerWait();
                    conn.Open();
                    new SqlCommand("delete from staff where id='" + codeC.Text + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Delete Succsfully");
                    refresh();
                    db.setCursoerDefault();
                    codeC.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry You Have enterd Invalied staff Code");
                    codeC.Focus();
                    conn.Close();
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
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void code_Leave(object sender, EventArgs e)
        {
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
            //  db.setTextBoxDefault(new TextBox[] { codeC, nameC, companyC, addressC, mobileNumberC, landNumberC, emailC, faxNumberC });
            codeC.Focus();
            listBox2.Items.Clear();
            loadAutoComplete(); listBox1.Visible = false;
        }

        private void Code_KeyDown_1(object sender, KeyEventArgs e)
        {
        }

        private void codeC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 | e.KeyValue == 12)
            {
                loadCustomer(codeC.Text);
                nameC.Focus();
            }
            else if (e.KeyValue == 40)
            {
                nameC.Focus();
            }
        }

        private void codeC_KeyUp(object sender, KeyEventArgs e)
        {
            conn.Open();
            db.loadLikeTextCustomer(conn, reader, listBox2, codeC);
            conn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (codeC.Text.Equals(""))
            {
                MessageBox.Show("Empty Customer Code to Search");
                codeC.Focus();
            }
            else
            {
                //db.setTextBoxDefault(new TextBox[] { nameC, companyC, addressC, mobileNumberC, landNumberC, emailC });

                if (!loadCustomer(codeC.Text))
                {
                    MessageBox.Show("Sorry You Have Enterd Invalied Customer Code");
                    codeC.Focus();
                }
                else
                {
                    MessageBox.Show("User Detail Download Succesfully");
                    nameC.Focus();
                }
            }
        }

        private void nameC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(codeC, addressC, addressC, e.KeyValue);
        }

        private void nameC_KeyUp(object sender, KeyEventArgs e)
        {
            conn.Open();
            db.loadLikeTextCustomer(conn, reader, listBox2, nameC);
            conn.Close();
        }

        private void companyC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(nameC, addressC, addressC, e.KeyValue);
        }

        private void companyC_KeyUp(object sender, KeyEventArgs e)
        {
            conn.Open();
            // db.loadLikeTextCustomer(conn, reader, listBox2, companyC);
            conn.Close();
        }

        private void addressC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(nameC, mobileNumberC, mobileNumberC, e.KeyValue);
        }

        private void mobileNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(addressC, salary, salary, e.KeyValue);
        }

        private void landNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            //db.setTextBoxPath(mobileNumberC, faxNumberC, faxNumberC, e.KeyValue);
        }

        private void faxNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            //db.setTextBoxPath(landNumberC, emailC, emailC, e.KeyValue);
        }

        private void emailC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                //       faxNumberC.Focus();
            }
            else if (e.KeyValue == 40)
            {
                button1.Focus();
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
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

        private void salary_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(mobileNumberC, bankingAmount, bankingAmount, e.KeyValue);
        }

        private void bankingAmount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(salary, epf, epf, e.KeyValue);
        }

        private void epf_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(salary, etf, etf, e.KeyValue);
        }

        private void etf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                epf.Focus();
            }
        }

        private void epf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }
    }
}