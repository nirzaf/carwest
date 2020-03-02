using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace pos
{
    public partial class customerReportView : Form
    {
        public customerReportView(Form home, String user, customerReportALL report)
        {
            InitializeComponent();
            formH = home;
            userH = user;
            reportH = report;
        }
        Form formH;
        customerReportALL reportH;
        DB db;
        string userH, queary, userName;
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
