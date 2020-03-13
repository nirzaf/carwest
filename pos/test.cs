using System;
using System.Windows.Forms;

namespace pos
{
    public partial class test : Form
    {
        private invoiceFormatHalfService_ formh;

        public test(invoiceFormatHalfService_ form)
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