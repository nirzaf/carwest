using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

using System.Windows.Forms;

namespace pos
{
    public partial class supplierProfile : Form
    {
        public supplierProfile(Form home, String user)
        {
            InitializeComponent();
            this.home = home;
            this.user = user;
        }

        // My Variable Start
        private DB db, db2;

        private Form home;
        private SqlConnection conn, conn2;
        private SqlDataReader reader, reader2;
        private ArrayList arrayList;
        private string[] mobileNoArray, LandNoArray, emailArray, companyArray;

        private Boolean states;
        private string user, codeC;

        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();
            this.ActiveControl = companyC;

            addressC.CharacterCasing = CharacterCasing.Upper;
            emailC.CharacterCasing = CharacterCasing.Upper;
            companyC.CharacterCasing = CharacterCasing.Upper;
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
                reader = new SqlCommand("select * from supplier where id='" + "S-" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    states = true;
                    codeC = id;
                    companyC.Text = reader.GetString(2);
                    //  nameC.Text = reader.GetString(1);
                    addressC.Text = reader.GetString(3);
                    mobileNumberC.Text = reader.GetString(4);
                    landNumberC.Text = reader.GetString(5);
                    emailC.Text = reader.GetString(7);
                    try
                    {
                        faxNumberC.Text = reader.GetString(8);
                    }
                    catch (Exception)
                    {
                    }
                    db.ToTitleCase(new TextBox[] { companyC, addressC, mobileNumberC, landNumberC, emailC, faxNumberC });
                }
                else
                {
                    states = false;
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
            if (companyC.Text.Equals(""))
            {
                MessageBox.Show("Sorry, COmpany Cant Be Empty Value");
                companyC.Focus();
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
                    reader = new SqlCommand("select * from supplier where id='" + "S-" + idTemp + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        conn.Open();
                        new SqlCommand("update supplier set company='" + companyC.Text + "',address='" + addressC.Text + "',mobileNo='" + mobileNumberC.Text + "',landNo='" + landNumberC.Text + "',description='" + "S-" + idTemp + " " + companyC.Text + "',email='" + emailC.Text + "' where id='" + "S-" + idTemp + "'", conn).ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sorry Invalied supplier ID");
                        companyC.Focus();
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
                    companyC.Focus();
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

        private Int32 idTemp = 0;

        private void getID()
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
                    new SqlCommand("insert into supplier values ('" + "S-" + idTemp + "','" + "" + "','" + companyC.Text + "','" + addressC.Text + "','" + mobileNumberC.Text + "','" + landNumberC.Text + "','" + "S-" + idTemp + " " + companyC.Text + "','" + emailC.Text + "','" + faxNumberC.Text + "','" + idTemp + "')", conn).ExecuteNonQuery();
                    conn.Close();
                    states = true;
                    db.setCursoerDefault();
                }
                catch (Exception a)
                {
                    MessageBox.Show("a");
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
                reader = new SqlCommand("select id from supplier ", conn).ExecuteReader();
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
                reader = new SqlCommand("select mobileNo from supplier ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                mobileNoArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(mobileNumberC, mobileNoArray);
                conn.Open();
                reader = new SqlCommand("select landNo from supplier ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                LandNoArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(landNumberC, LandNoArray);
                conn.Open();
                reader = new SqlCommand("select email from supplier ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToLower()) + "");
                }
                reader.Close();
                conn.Close();
                emailArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(emailC, emailArray);
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
                db.setTextBoxDefault(new TextBox[] { companyC, addressC, mobileNumberC, landNumberC, emailC, faxNumberC });
                companyC.Focus();
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
            if (companyC.Text.Equals(""))
            {
                MessageBox.Show("Invalied supplier ID to Delete");
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
                    new SqlCommand("delete from supplier where id='" + "S-" + idTemp + "'", conn).ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Delete Succsfully");
                    refresh();
                    db.setCursoerDefault();
                    companyC.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry You Have enterd Invalied supplier Code");
                    companyC.Focus();
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
            idTemp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
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
                idTemp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
                // companyC.SelectionLength code= code.MaxLength;
                loadCustomer(idTemp + "");
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;

            idTemp = Int32.Parse(listBox1.SelectedItem.ToString().Split(' ')[0]);
            loadCustomer(idTemp + "");
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
            db.setTextBoxDefault(new TextBox[] { companyC, addressC, mobileNumberC, landNumberC, emailC, faxNumberC });
            companyC.Focus();
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
                MessageBox.Show("Empty supplier Code to Search");
                companyC.Focus();
            }
            else
            {
                db.setTextBoxDefault(new TextBox[] { companyC, addressC, mobileNumberC, landNumberC, emailC });

                if (!loadCustomer(idTemp + ""))
                {
                    MessageBox.Show("Sorry You Have Enterd Invalied supplier Code");
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
                    MessageBox.Show("Sorry, Invalied Item ID");
                    companyC.Focus();
                }
                else
                {
                    loadCustomer(idTemp + "");
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
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyC.Text.Equals("")))
            {
                db.setList(listBox1, companyC, companyC.Width * 3);

                try
                {
                    listBox1.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select auto,description from supplier where description like '%" + companyC.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader[0] + " " + reader[1].ToString().ToUpper());
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
            db.setTextBoxPath(addressC, landNumberC, landNumberC, e.KeyValue);
        }

        private void landNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(mobileNumberC, faxNumberC, faxNumberC, e.KeyValue);
        }

        private void faxNumberC_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(landNumberC, emailC, emailC, e.KeyValue);
        }

        private void emailC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
            else if (e.KeyValue == 38)
            {
                faxNumberC.Focus();
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
    }
}