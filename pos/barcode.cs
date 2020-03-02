using System;

using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class barcode : Form
    {
        public barcode(Form form)
        {
            InitializeComponent();
            formH = form;
        }
        Form formH;
        private void button1_Click(object sender, EventArgs e)
        {

        }
        DB db, db2;
        Form home;
        SqlConnection conn, conn2;
        SqlDataReader reader, reader2;
        private void barcode_Load(object sender, EventArgs e)
        {
            db = new DB();
            conn = db.createSqlConnection();

            this.TopMost = true;
            conn.Open();
            new SqlCommand("delete from im", conn).ExecuteNonQuery();

            conn.Close();

        }
        Int32 i = 0;
        private void button2_Click(object sender, EventArgs e)
        {

        }
        OpenFileDialog openFileDialog12;
        string price, name, code;
        private void button3_Click(object sender, EventArgs e)
        {
            db.setCursoerWait();
            
            conn.Open();
            reader = new SqlCommand("select billingPrice,remark,brand,categorey,description,code from item where code='" + itemCode.Text + "'", conn).ExecuteReader();
            if (reader.Read())
            {
                price = db.setAmountFormat(unitPrice.Text) + "";
                name = reader[2].ToString().ToUpper() + " " + reader[3].ToString().ToUpper() + " " + reader[4].ToString().ToUpper();
                code = itemCode.Text + "";
                conn.Close();
                {
                    try
                    {
                       // MessageBox.Show(openFileDialog12.FileName);

                        pictureBox1.Image = new Bitmap("C:\\2.jpg");  // imageFullPath = System.IO.Path.GetDirectoryName(openFileDialog12.FileName);

                        //imagePath = System.IO.Path.GetFileName(openFileDialog12.FileName);

                    }
                    catch (Exception ex)
                    {
                        // imagePath = "default.png";
                        MessageBox.Show("Error loading image" + ex.Message);
                    }
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                Int32 x = 0, x2;
                try
                {
                    conn.Open();
                    reader = new SqlCommand("Select max(id) from im", conn).ExecuteReader();
                    if (reader.Read())
                    {
                        x = reader.GetInt32(0);
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    x = 0;
                    conn.Close();
                }

                try
                {
                    for (int i = 0; i < Int32.Parse(qty.Text); i++)
                    {
                        x2 = x + i;
                        SqlCommand com = new SqlCommand("insert into im VALUES ('" + x2 + "',@Pic,'" + db.setAmountFormat(price) + "','" + code + "','" + name.ToUpper() + "','" + itemCode.Text + "')", conn);

                        MemoryStream stream = new MemoryStream();

                        pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        byte[] pic = stream.ToArray();

                        com.Parameters.AddWithValue("@pic", pic);

                        conn.Open();
                        com.ExecuteNonQuery();
                        conn.Close();
                    }

                }
                catch (Exception ex)
                {
                    //    throw;
                }
                finally
                {
                    conn.Close();
                }


                {
                    payslip3 emp = new payslip3();
                    try
                    {
                        conn.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter("select * from im order by codeitem ", conn);
                        //    MessageBox.Show("1");
                        // adapter.
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        // MessageBox.Show("2");

                        emp.SetDataSource(dataSet.Tables[0]);
                        //crystalReportViewer1.ReportSource = emp;
                        // crystalReportViewer1.DataBindings();
                        conn.Close();
                    }
                    catch (Exception v)
                    {
                        MessageBox.Show(v.Message);
                    }



                    crystalReportViewer1.ReportSource = emp;
                }
                itemCode.Focus();
                itemCode.Text = "";

            }
            else
            {
                conn.Close();
                MessageBox.Show("Please Eneter Valied Item Code");
                itemCode.Focus();
            }

            // crystalReportViewer1.ReportSource = emp;
            //   Cursor.Current = Cursors.WaitCursor;



        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            if (Char.IsControl(e.KeyChar)) return;
            ////if ((e.KeyChar == '.')  ) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.Contains(".") == false)) return;
            //if ((e.KeyChar == '.') && ((sender as TextBox).SelectionLength == (sender as TextBox).TextLength)) return;

            e.Handled = true;
        }

        private void barcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            qty.Text = "20";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            qty.Text = "70";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            new SqlCommand("delete from im", conn).ExecuteNonQuery();

            conn.Close();
            MessageBox.Show("Deleted Succefully");
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }
        string cutomerID;
        void loadItem(string codeValue)
        {
            try
            {


                cutomerID = "";
                conn.Open();
                reader = new SqlCommand("select retailPrice from item where code='" + codeValue + "'", conn).ExecuteReader();
                if (reader.Read())
                {

                    unitPrice.Text = reader.GetDouble(0) + "";

                    qty.Focus();
                    conn.Close();

                }
                else
                {
                   
                }
                reader.Close();
                conn.Close();



            }
            catch (Exception a)
            {
                //    throw;
                reader.Close();
                conn.Close();
                MessageBox.Show(a.Message);
            }
        }


        private void itemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                if (itemCode.Text.Equals(""))
                {
                    MessageBox.Show("Sorry, Invalied Item ID");
                    itemCode.Focus();
                }
                else
                {
                    loadItem(itemCode.Text);
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
        bool states;
        private void itemCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 12 | e.KeyValue == 13 | itemCode.Text.Equals("")))
            {
                
                {
                    db.setList(listBox1, itemCode, itemCode.Width * 2);

                    try
                    {
                        listBox1.Items.Clear();
                        listBox1.Visible = true;
                        conn.Open();
                        reader = new SqlCommand("select code,detail from item where detail like '%" + itemCode.Text + "%' ", conn).ExecuteReader();
                        
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
            if (itemCode.Text.Equals(""))
            {
                //   MessageBox.Show("2");
                listBox1.Visible = false;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex == 0 && e.KeyValue == 38)
            {
                itemCode.Focus();
            }
            else if (e.KeyValue == 12 | e.KeyValue == 13)
            {
                listBox1.Visible = false;
                itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
                itemCode.SelectionLength = itemCode.MaxLength;
                loadItem(itemCode.Text);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Visible = false;
            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
            itemCode.SelectionLength = itemCode.MaxLength;
            loadItem(itemCode.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemCode.Text = listBox1.SelectedItem.ToString().Split(' ')[0];
        }
    }
}
