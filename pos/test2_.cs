using System;
using System.Windows.Forms;

namespace pos
{
    public partial class test2_ : Form
    {
        private salarySummeryAudit formh;

        public test2_(salarySummeryAudit form)
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