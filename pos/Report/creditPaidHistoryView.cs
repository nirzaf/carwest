using System;
using System.Windows.Forms;

namespace pos
{
    public partial class creditPaidHistoryView : Form
    {
        public creditPaidHistoryView(Form home, String user, creditPaidHistory report)
        {
            InitializeComponent();
            formH = home;
            userH = user;
            reportH = report;
        }

        private Form formH;
        private creditPaidHistory reportH;
        private DB db;
        private string userH, queary, userName;

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void stockReportView_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = reportH;
            this.TopMost = true;
        }

        private void stockReportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            formH.Enabled = true;
            formH.TopMost = true;
        }
    }
}