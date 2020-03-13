using System;
using System.Windows.Forms;

namespace pos
{
    public partial class cashOutView : Form
    {
        private invoiceReportCashout formh;

        public cashOutView(invoiceReportCashout form)
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