using System;
using System.Windows.Forms;

namespace pos
{
    public partial class test2_2 : Form
    {
        private salarySummerySlip formh;

        public test2_2(salarySummerySlip form)
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