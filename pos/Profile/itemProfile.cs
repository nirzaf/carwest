using System;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace pos
{
    public partial class itemProfile : Form
    {
        public itemProfile(Form home, String user)
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
        private Boolean check, checkListBox;
        private string user, listBoxType;
        private String[] idArray;
        // my Variable End

        private void itemProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();
            loadAutoComplete();
            remark.CharacterCasing = CharacterCasing.Upper;
            code.CharacterCasing = CharacterCasing.Upper;
            SUB.Checked = true;
            SUB.Checked = false;

            try
            {
                conn.Open();
                reader = new SqlCommand("select * from custom ", conn).ExecuteReader();
                if (reader.Read())
                {
                    valueByDis.Visible = reader.GetBoolean(5);
                    lableValueByDistance.Visible = reader.GetBoolean(5);
                }
                else
                {
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
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
            UpdateItem();
        }

        private void aDDNEWITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveItem();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveItem();
        }

        //++++++ My Method Start+++
        private void loadAutoComplete()
        {
            try
            {
                conn.Open();
                reader = new SqlCommand("select brand from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(brand, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select categorey from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(category, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select description from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(description, idArray);
                conn.Close();
                conn.Open();
                reader = new SqlCommand("select rate from item ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];
                db.setAutoComplete(rate, idArray);
                conn.Close();

                conn.Open();
                reader = new SqlCommand("select rate from itemSub ", conn).ExecuteReader();
                arrayList = new ArrayList();
                while (reader.Read())
                {
                    // MessageBox.Show("m");
                    arrayList.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader[0].ToString().ToUpper()) + "");
                }
                reader.Close();
                idArray = arrayList.ToArray(typeof(string)) as string[];

                db.setAutoComplete(rateSub, idArray);
                conn.Close();
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
                db.setTextBoxDefault(new TextBox[] { code, brand, category, description, remark, rate, valueByDis, rateSub, separateCount });
                code.Focus();
                listBox1.Visible = false;
                SUB.Checked = false;
            }
            catch (Exception)
            {
            }
        }

        private void saveItem()
        {
            try
            {
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Code");
                    code.Focus();
                }
                else if (description.Equals(""))
                {
                    MessageBox.Show("Please Eneter Description");
                    description.Focus();
                }
                else if (rate.Equals(""))
                {
                    MessageBox.Show("Please Eneter Rate");
                    rate.Focus();
                }
                else if ((MessageBox.Show("Are You Sure Saved New Item ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    if (valueByDis.Text.Equals(""))
                    {
                        valueByDis.Text = "0";
                    }
                    conn.Open();
                    conn2.Open();
                    reader = new SqlCommand("select code from item where code='" + code.Text + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Sorry Duplicate Code NO");
                    }
                    else
                    {
                        reader.Close();
                        db.setCursoerWait();

                        new SqlCommand("insert into item values('" + code.Text + "','" + brand.Text + "','" + category.Text + "','" + description.Text + "','" + remark.Text + "','" + rate.Text + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + db.setItemDescriptionName(new TextBox[] { code, brand, category, description }) + "')", conn).ExecuteNonQuery();
                        new SqlCommand("insert into distance values('" + code.Text + "','" + valueByDis.Text + "')", conn).ExecuteNonQuery();
                        if (SUB.Checked)
                        {
                            new SqlCommand("insert into itemSub values('" + code.Text + "','" + separateCount.Text + "','" + 0 + "','" + 0 + "','" + rateSub.Text + "')", conn).ExecuteNonQuery();
                        }

                        db.setCursoerDefault();

                        code.Focus();
                        MessageBox.Show("New Item Saved Succefully");
                    }
                    reader.Close();
                    conn.Close();
                    conn2.Close();
                    refresh();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Item Detail Value Empty or Invalied Characters." + a.StackTrace);
                conn.Close();
                conn2.Close();
            }
        }

        private void UpdateItem()
        {
            try
            {
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Code");
                    code.Focus();
                }
                else if (description.Equals(""))
                {
                    MessageBox.Show("Please Eneter Description");
                    description.Focus();
                }
                else if (rate.Equals(""))
                {
                    MessageBox.Show("Please Eneter Rate");
                    rate.Focus();
                }
                else if ((MessageBox.Show("Are You Sure Update Current Item ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    if (valueByDis.Text.Equals(""))
                    {
                        valueByDis.Text = "0";
                    }
                    conn.Open();
                    conn2.Open();
                    reader = new SqlCommand("select code from item where code='" + code.Text + "'", conn).ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Sorry Invalied Code NO");
                        code.Focus();
                    }
                    else
                    {
                        reader.Close();
                        db.setCursoerWait();

                        new SqlCommand("update item set brand='" + brand.Text + "',categorey='" + category.Text + "',description='" + description.Text + "',remark='" + remark.Text + "',rate='" + rate.Text + "',detail='" + db.setItemDescriptionName(new TextBox[] { code, brand, category, description }) + "' where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        db.setCursoerDefault();
                        new SqlCommand("delete from distance where code='" + code.Text + "'", conn).ExecuteNonQuery();
                        new SqlCommand("insert into distance values('" + code.Text + "','" + valueByDis.Text + "')", conn).ExecuteNonQuery();
                        new SqlCommand("delete from itemSub where code='" + code.Text + "'", conn).ExecuteNonQuery();

                        if (SUB.Checked)
                        {
                            new SqlCommand("insert into itemSub values('" + code.Text + "','" + separateCount.Text + "','" + 0 + "','" + 0 + "','" + rateSub.Text + "')", conn).ExecuteNonQuery();
                        }

                        code.Focus();
                        MessageBox.Show("Current Item Updated Succefully");
                    }
                    reader.Close();
                    conn.Close();
                    conn2.Close();
                    refresh();
                }
            }
            catch (Exception a)
            {
                conn.Close();
                conn2.Close();
                MessageBox.Show("Item Detail Value Empty or Invalied Characters." + a.Message + "/ " + a.StackTrace);
            }
        }

        private void deleteItem()
        {
            try
            {
                if (code.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Code");
                    code.Focus();
                }
                else if (description.Equals(""))
                {
                    MessageBox.Show("Please Eneter Description");
                    description.Focus();
                }
                else if (rate.Equals(""))
                {
                    MessageBox.Show("Please Eneter Rate");
                    rate.Focus();
                }
                else if ((MessageBox.Show("Are You Sure Delete Current Item ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    conn.Open();
                    conn2.Open();
                    reader = new SqlCommand("select code from item where code='" + code.Text + "'", conn).ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Sorry Invalied Code NO");
                        code.Focus();
                    }
                    else
                    {
                        reader.Close();
                        db.setCursoerWait();
                        reader = new SqlCommand("select * from item where code='" + code.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            //   new SqlCommand("insert into auditItem values('" + reader[0] + "','" + reader[1] + "','" + reader[2] + "','" + reader[3] + "','" + reader[4] + "','" + reader[5] + "','" + reader[6] + "','" + reader[7] + "','" + reader[8] + "','" + reader[9] + "','" + reader[10] + "','" + user + "','" + "delete" + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();
                        }
                        reader.Close();
                        new SqlCommand("delete from item  where code='" + code.Text + "'", conn).ExecuteNonQuery();

                        db.setCursoerDefault();

                        code.Focus();
                        MessageBox.Show("Current Item Deleted Succefully");
                    }
                    reader.Close();
                    conn.Close();
                    conn2.Close();
                    refresh();
                }
            }
            catch (Exception a)
            {
                conn.Close();
                conn2.Close();
                MessageBox.Show("Item Detail Value Empty or Invalied Characters.");
            }
        }

        public void loadItem(string id)
        {
            try
            {
                listBox1.Visible = false;
                conn.Open();
                db.setCursoerWait();
                db.setTextBoxDefault(new TextBox[] { brand, category, description, remark, rate });
                reader = new SqlCommand("select * from item where code='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    brand.Text = reader.GetString(1).ToUpper();
                    category.Text = reader.GetString(2).ToUpper();
                    description.Text = reader.GetString(3).ToUpper();
                    remark.Text = reader.GetString(4).ToUpper();
                    rate.Text = reader.GetString(5).ToUpper();

                    if (code.Text.Equals(""))
                    {
                        code.Text = id;
                    }
                    else
                    {
                        MessageBox.Show("Item Detail Download Succesfully");
                    }

                    brand.Focus();
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select value from distance where code='" + id + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        valueByDis.Text = reader[0] + "";
                    }
                    conn.Close();
                    conn.Open();
                    reader = new SqlCommand("select * from itemSub where code='" + id + "'", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        SUB.Checked = true;
                        separateCount.Text = reader[1] + "";
                        rateSub.Text = reader[4] + "";
                    }
                    else
                    {
                        SUB.Checked = false;
                        separateCount.Text = "";
                        rateSub.Text = "";
                    }

                    brand.Focus();
                    conn.Close();
                }
                else
                {
                    reader.Close();
                    if (check)
                    {
                        brand.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Invalied Item Code");
                        code.Focus();
                    }
                }
                db.setCursoerDefault();
                reader.Close();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("You Have Enterd Invalied Charactores " + e.StackTrace + e.Message);
            }
        }

        //++++++ My Method End++++
        private void code_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            listBoxType = "item";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | code.Text.Equals("")))
            {
                db.setList(listBox1, code, code.Width * 3);

                try
                {
                    conn.Open();
                    listBox1.Items.Clear();
                    reader = new SqlCommand("select detail from item where detail like '%" + code.Text + "%' ", conn).ExecuteReader();
                    arrayList = new ArrayList();

                    while (reader.Read())
                    {
                        //   MessageBox.Show("2");
                        listBox1.Items.Add(reader[0]);
                        listBox1.Visible = true;
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception a)
                {
                    conn.Close();
                    MessageBox.Show(a.Message);
                }
            }
            if (code.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
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
            check = false;
            loadItem(code.Text);
        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                check = true;
                loadItem(code.Text);
            }
            else if (e.KeyValue == 40)
            {
                checkListBox = true;
                try
                {
                    if (listBox1.Visible)
                    {
                        listBox1.Focus();
                        listBox1.SelectedIndex = 0;
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

        private void brand_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, category, category, e.KeyValue);
        }

        private void category_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(brand, description, description, e.KeyValue);
        }

        private void description_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(category, remark, remark, e.KeyValue);
        }

        private void remark_TextChanged(object sender, EventArgs e)
        {
        }

        private void remark_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                if (valueByDis.Visible)
                {
                    valueByDis.Focus();
                }
                else
                {
                    SUB.Focus();
                }
            }
            else if (e.KeyValue == 38)
            {
                remark.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                if (valueByDis.Visible)
                {
                    valueByDis.Focus();
                }
                else
                {
                    button1_Click(sender, e);
                }
            }
        }

        private void sAVECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteItem();
        }

        private void dELETECURRENTITEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteItem();
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxType.Equals("item"))
            {
                code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxType.Equals("item"))
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
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
            if (listBoxType.Equals("item"))
            {
                code.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                code.SelectionLength = code.MaxLength;
                loadItem(code.Text);
            }
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
            code.Text = "";
            code.Focus();
            listBox1.Visible = false;
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void valueByDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void valueByDis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                code.Focus();
            }
            else if (e.KeyValue == 38)
            {
                rate.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 yk = 0;
                string tempDesc;
                string brand = "ALUMEX", categorys = category.Text, desc = description.Text;
                string[] readfromAddress = File.ReadAllLines("F:\\test.txt");
                string[] des = new string[] { "BA", "NA", "PC", "MF", "PBR", "PNA", "WF", "WOODEX" };
                for (int y = 0; y < des.Length; y++)
                {
                    tempDesc = desc + " " + des[y];
                    if (des[y].Equals("WF"))
                    {
                        tempDesc = tempDesc + " DW 2 SERIES";
                    }
                    else if (des[y].Equals("WOODEX"))
                    {
                        tempDesc = tempDesc + " DW 3 SERIES";
                    }
                    for (int i = 0; i < readfromAddress.Length; i++)
                    {
                        conn.Open();
                        conn2.Open();
                        reader = new SqlCommand("select code from item where code='" + readfromAddress[i] + "-" + des[y] + code.Text + "'", conn).ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("Sorry Duplicate Code  " + readfromAddress[i]);
                        }
                        else
                        {
                            yk++;
                            reader.Close();
                            db.setCursoerWait();
                            new SqlCommand("insert into auditItem values('" + readfromAddress[i] + "-" + des[y] + code.Text + "','" + brand + "','" + categorys + "','" + tempDesc + "','" + "" + "','" + "NOS" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "1" + "','" + "insert" + "','" + DateTime.Now + "')", conn2).ExecuteNonQuery();

                            new SqlCommand("insert into item values('" + readfromAddress[i] + "-" + des[y] + code.Text + "','" + brand + "','" + categorys + "','" + tempDesc + "','" + "" + "','" + "NOS" + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "" + "','" + db.setItemDescriptionNameByName(new string[] { readfromAddress[i].ToString() + "-" + des[y] + code.Text, brand, categorys, tempDesc }) + "')", conn).ExecuteNonQuery();
                            db.setCursoerDefault();

                            code.Focus();
                        }
                        reader.Close();
                        conn.Close();
                        conn2.Close();
                    }
                }

                MessageBox.Show("New Item Saved Succefully " + yk);
            }
            catch (Exception)
            {
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void brand_TextChanged(object sender, EventArgs e)
        {
        }

        private void SUB_CheckedChanged(object sender, EventArgs e)
        {
            panelSub.Enabled = SUB.Checked;
            if (SUB.Checked)
            {
                separateCount.Focus();
            }
        }

        private void separateCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void separateCount_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(valueByDis, rateSub, rateSub, e.KeyValue);
        }

        private void rateSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40)
            {
                code.Focus();
            }
            else if (e.KeyValue == 38)
            {
                separateCount.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void brand_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(code, category, category, e.KeyValue);
        }

        private void remark_KeyDown_1(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(description, rate, rate, e.KeyValue);
        }

        private string splitOne;
        private string[] a, a2;

        private void button5_Click_1(object sender, EventArgs e)
        {
        }
    }
}