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

namespace demobtl2
{
    public partial class inchitiethoadon : Form
    {
        public inchitiethoadon()
        {
            InitializeComponent();
        }
        public string ma;
        public inchitiethoadon(string mahd)
        {
            InitializeComponent();
            ma = mahd;
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        SqlConnection cnn = null;
        private void inchitiethoadon_Load(object sender, EventArgs e)
        {
            hien();
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
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd",SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.AddWithValue("@action",SqlDbType.NVarChar).Value = "selectone";            
            DataSet ds = new DataSet();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            CrCtHoaDon rp = new CrCtHoaDon();
            ad.Fill(ds);
            rp.SetDataSource(ds.Tables["Chitiethoadon"]);
            crystalReportViewer1.ReportSource = rp;
        }
    }
}
