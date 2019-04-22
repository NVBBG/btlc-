using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demobtl2
{
    public partial class Thongke : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;     
        SqlConnection cnn = null;
        double giabandau = 0;
        double giaketthuc = 0;     
        public Thongke()
        {
            InitializeComponent();
        }
        private void Thongke_Load(object sender, EventArgs e)
        {
           
        }
        public void kiemtra()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = null;                       
            if ((rdTonggiatri.Checked)==true)
            {
                query = "selectgia";
                try
                {
                    giabandau = Double.Parse(txtGiaBanDau.Text);
                    giaketthuc = Double.Parse(txtGiaKetThuc.Text);
                }
                catch(Exception ex)
                {

                }
            }
            else
            {
                query = "select";
                giabandau = 0;
                giaketthuc = 0;
            }
            
            hiendein(query,giabandau,giaketthuc);
        }       
       
        private void rdallgia_CheckedChanged(object sender, EventArgs e)
        {
            txtGiaBanDau.Enabled = false;
            txtGiaKetThuc.Enabled = false;
        }
        private void rdTonggiatri_CheckedChanged(object sender, EventArgs e)
        {
            txtGiaBanDau.Enabled = true;
            txtGiaKetThuc.Enabled = true;
        }
        private void hiendein(string query,double giabandau, double giaketthuc)
        {
            ReportDocument rpt = new ReportDocument();   //tạo mới 1 report document
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            rpt.Load(@"C:\Users\HP\source\repos\demobtl2\demobtl2\CrHD.rpt");   // đường dẫn load rpt
            Tables CrTables;
            CrTables = rpt.Database.Tables;
            crConnectionInfo.IntegratedSecurity = true;  /// đăng nhập với quyền windows
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            crystalReportViewer1.Refresh();            
            rpt.SetParameterValue("@action",query);
            rpt.SetParameterValue("@giabandau",giabandau);
            rpt.SetParameterValue("@giaketthuc",giaketthuc);
            crystalReportViewer1.ReportSource = rpt;//// chọ  nguồn dữ liệu cho crystal Report
        }

        private void Thongke_Load_1(object sender, EventArgs e)
        {
            string query = null;
            if (rdallgia.Checked)
            {
                txtGiaBanDau.Enabled = false;
                txtGiaKetThuc.Enabled = false;
            }
            query = "select";
            hiendein(query, giabandau, giaketthuc);
        }
    }
}
