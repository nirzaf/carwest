﻿using System;
using System.Windows.Forms;

namespace pos
{
    public partial class cusStateView : Form
    {
        private cusStatement formh;

        public cusStateView(cusStatement form)
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