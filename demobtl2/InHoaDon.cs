using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;

namespace demobtl2
{
    public partial class InHoaDon : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        SqlConnection cnn = null;
        public InHoaDon()
        {
            InitializeComponent();
        }
        public string ma;
        public InHoaDon(string mahd)
        {
            InitializeComponent();
            ma = mahd;
        }
        private void inchitiethoadon_Load(object sender, EventArgs e)
        {
            hiendein();
        }
        private void hien()
        {

            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "HoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            //MessageBox.Show(ma.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            CrHoaDon rp = new CrHoaDon();
            //rp.Refresh();
            crystalReportViewer1.Refresh();
            rp.SetDataSource(tb);
            crystalReportViewer1.ReportSource = rp;
        }
        private void hiendein()
        {

            // MessageBox.Show(ma);
            ReportDocument rpt = new ReportDocument();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            rpt.Load(@"C:\Users\HP\source\repos\demobtl2\demobtl2\CrHoaDon.rpt");
            Tables CrTables;
            CrTables = rpt.Database.Tables;
            crConnectionInfo.IntegratedSecurity = true;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            crystalReportViewer1.Refresh();
            rpt.SetParameterValue("@mahd",ma);
            //srpt.SetParameterValue("@action","selectone");
            crystalReportViewer1.ReportSource = rpt;
        }

   
    }
}
