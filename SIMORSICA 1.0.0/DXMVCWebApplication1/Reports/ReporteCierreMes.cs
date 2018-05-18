using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace DXMVCWebApplication1.Reports
{
    public partial class ReporteCierreMes : DevExpress.XtraReports.UI.XtraReport
    {
        public ReporteCierreMes()
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
