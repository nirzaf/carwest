using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class test3 : Form
    {
        stockReportALL_2 formh;
        public test3(stockReportALL_2 form)
        {
            InitializeComponent();
            formh = form;
        }

        private void test_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = formh;
            this.TopMost = true;
        }
    }
}
