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
    public partial class stockProfile : Form
    {
        public stockProfile(Form form, string user, string id)
        {
            InitializeComponent();
            home = form;
            idS = id;
        }

        // My Variable Start
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader;
        ArrayList arrayList;
        Boolean check, checkListBox;
        string user, listBoxType, idS, supplierID;
        String[] idArray;
        double qtyDb;
        // my Variable End

        private void stockProfile_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            db = new DB();
            conn = db.createSqlConnection();
            db2 = new DB();
            conn2 = db2.createSqlConnection();

            item.CharacterCasing = CharacterCasing.Upper;


            try
            {
                //  MessageBox.Show(idS);
                conn.Open();
                reader = new SqlCommand("select QTY,brand,categorey from item where code='" + idS + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    availbleQty.Text = reader[0] + "";
                    item.Text = reader[1] + " " + reader[2];

                }
                conn.Close();
                companyName.Focus();
            }
            catch (Exception)
            {
                conn.Close();
            }
            this.ActiveControl = companyName;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox3.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyName.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                loadCompany(listBox3.SelectedItem.ToString().Split(' ')[0]);
            }
        }

        //++++My Method start++++
        public void loadItem(string id)
        {

            try
            {
                listBox1.Visible = false;
                conn.Open();
                db.setCursoerWait();
                reader = new SqlCommand("select qty from item where code='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    companyName.Text = reader[0] + "";
                    reader.Close();
                    number.Focus();
                }
                else
                {

                    MessageBox.Show("Invalied Item Code");
                    item.Focus();


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
        void plus()
        {
            try
            {
                conn.Open();
                if (cutomerID.Equals(""))
                {
                    MessageBox.Show("Please Enter Supplier Code");
                    companyName.Focus();
                }
                else if (number.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Mobile Number");
                    number.Focus();
                }
                else if (requstedQty.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Requsted QTY");
                    requstedQty.Focus();
                }
                else {

                    new SqlCommand("insert into stockOrder values ('"+idS+"','"+item.Text+"','"+availbleQty.Text+"','"+requstedQty.Text+"','"+cutomerID+"','"+meassge.Text+"','"+DateTime.Now+"')", conn).ExecuteNonQuery();
                    MessageBox.Show("Meassge Sent to "+number.Text+" Succefully" );
                    this.Dispose();
                }
                conn.Close();

            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.StackTrace);
            }
        }
        Int32 tempCount;
        double tempPrice;
        void mins()
        {
            try
            {
                if (number.Text.Equals(""))
                {
                    MessageBox.Show("Please Enter Qty");
                    number.Focus();
                }

                else if ((MessageBox.Show("Are You Sure Less Price Detail and QTY ?", "Confirmation",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.setCursoerWait();
                    conn.Open();

                    new SqlCommand("update item set qty=qty-'" + number.Text + "' where code='" + item.Text + "'", conn).ExecuteNonQuery();
                    new SqlCommand("update item set qty='" + 0 + "' where qty<'" + 0 + "'", conn).ExecuteNonQuery();
                    MessageBox.Show("Saved Successfully");
                    item.Focus();
                    db.setTextBoxDefault(new TextBox[] { item, number });
                    conn.Close();
                    item.SelectionLength = item.SelectionLength;

                    reader.Close();
                    conn.Close();
                    db.setCursoerDefault();

                }

            }
            catch (Exception a)
            {
                conn.Close();
                MessageBox.Show(a.StackTrace);
            }
        }


        void refrsh()
        {
            item.Focus();
            MessageBox.Show("a");
            db.setTextBoxDefault(new TextBox[] { item, number, companyName });
            MessageBox.Show("a");
        }
        //++++My Method End+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void button4_Click(object sender, EventArgs e)
        {
            loadItem(item.Text);
        }

        private void purchasingPrice_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void purchasingPrice_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void minRetailPrice_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void maxRetailPrice_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox3.Visible = false;

            loadCompany(listBox3.SelectedItem.ToString().Split(' ')[0]);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyName.Text = listBox3.SelectedItem.ToString();
        }

        private void code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                check = true;
                loadItem(item.Text);
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
                        number.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void code_KeyUp(object sender, KeyEventArgs e)
        {
            listBoxType = "item";
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | item.Text.Equals("")))
            {
                db.setList(listBox1, item, item.Width * 3);

                try
                {
                    conn.Open();
                    listBox1.Items.Clear();
                    reader = new SqlCommand("select detail from item where detail like '%" + item.Text + "%' ", conn).ExecuteReader();
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
            if (item.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void stockProfile_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
        }

        private void stockProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plus();
        }

        private void qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                item.Focus();
            }
            else if (e.KeyValue == 40)
            {
                requstedQty.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                requstedQty.Focus();
            }
        }

        private void rEFRESHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show("c");
            item.Text = "";
            item.Focus();
            listBox1.Visible = false;

            item.Focus();
        }

        private void tOSTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plus();
        }

        private void purchasingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            //if ((e.KeyChar == '.')  ) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mins();
        }

        private void fROMSTOCKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mins();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            home.Enabled = true;
            home.TopMost = true;
        }
        string cutomerID;
        public void loadCompany(string id)
        {

            try
            {
                cutomerID = "";
                number.Text = "";
                companyName.Text = "";
                db.setCursoerWait();
                conn.Open();
                reader = new SqlCommand("select mobileNo,description from supplier where id='" + id + "'", conn).ExecuteReader();
                if (reader.Read())
                {
                    number.Text = reader[0] + "";
                    cutomerID = id;
                    companyName.Text = reader[1] + "";
                }

                reader.Close();
                conn.Close();
                db.setCursoerDefault();
                number.Focus();
            }
            catch (Exception A)
            {
                MessageBox.Show(A.Message);
                conn.Close();
            }

        }

        private void companyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox3.Visible = false;
                if (companyName.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Company Name");
                    companyName.Focus();
                }
                else
                {
                    loadCompany(companyName.Text);
                    number.Focus();
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
                        number.Focus();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void companyName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | companyName.Text.Equals("")))
            {

                db.setList(listBox3, companyName, companyName.Width);

                try
                {
                    listBox3.Items.Clear();
                    conn.Open();
                    reader = new SqlCommand("select id,description from supplier where description like '%" + companyName.Text + "%' ", conn).ExecuteReader();
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
            if (companyName.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox3.Visible = false;
            }
        }

        private void listBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox3.SelectedIndex == 0 && e.KeyValue == 38)
            {
                companyName.Focus();
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
            companyName.Text = listBox3.SelectedItem.ToString();
        }

        private void requstedQty_KeyDown(object sender, KeyEventArgs e)
        {
            db.setTextBoxPath(number, meassge, meassge, e.KeyValue);
        }

        private void meassge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38)
            {
                requstedQty.Focus();
            }
            else if (e.KeyValue == 12 || e.KeyValue == 13)
            {
                plus();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new stockOrderHistorey(this, user).Visible = true;
        }


    }
}
