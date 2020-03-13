using System;
using System.Windows.Forms;

namespace pos
{
    public partial class expensesView : Form
    {
        private invoiceReportEx formh;

        public expensesView(invoiceReportEx form)
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