using System;
using System.Windows.Forms;

namespace pos
{
    public partial class test3 : Form
    {
        private stockReportALL_2 formh;

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