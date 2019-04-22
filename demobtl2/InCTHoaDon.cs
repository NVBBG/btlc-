using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace demobtl2
{
    public partial class InCTHoaDon : Form
    {
        //public delegate void SendMessage(string Message);
        
          //Hàm có nhiệm vụ lấy tham số truyền vào
       

        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        string ma;
        public InCTHoaDon(string text)
        {
            ma = text;
            InitializeComponent();
        }
        public InCTHoaDon()
        {
            InitializeComponent();
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
            //int ma = Convert.ToInt32(txloc.Text);
            string query = "getHoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = ma;
            //cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            DataSet ds = new DataSet();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            CrCTHD rp = new CrCTHD();
            ad.Fill(ds);
            rp.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rp;
        }
        private void inloaihang_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(strNhan.ToString());
            hienct(ma);
           // MessageBox.Show(strNhan);
        }
        private void hienct(string ma)
        {
            ReportDocument rpt = new ReportDocument();   //tạo mới 1 report document
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            rpt.Load(@"C:\Users\HP\source\repos\demobtl2\demobtl2\CrCTHD.rpt");   // đường dẫn load rpt
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
            rpt.SetParameterValue("@mahd", ma);
            //rpt.SetParameterValue("@action", "selectone");
            crystalReportViewer1.ReportSource = rpt;
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
