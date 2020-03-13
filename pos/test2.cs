using System;
using System.Windows.Forms;

namespace pos
{
    public partial class test2 : Form
    {
        private salarySummery formh;

        public test2(salarySummery form)
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