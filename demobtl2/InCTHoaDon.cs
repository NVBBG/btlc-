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
namespace demobtl2
{
    public partial class InCTHoaDon : Form
    {
        public delegate void SendMessage(string Message);
        
          //Hàm có nhiệm vụ lấy tham số truyền vào
       

        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        string strNhan;
        public InCTHoaDon(string text) : this()
        {
            InitializeComponent();
            strNhan = text;
            
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
            string query = "HoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = strNhan;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            DataSet ds = new DataSet();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            CrHoaDon rp = new CrHoaDon();
            ad.Fill(ds);
            rp.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rp;
        }
        private void inloaihang_Load(object sender, EventArgs e)
        {
            hien();
           // MessageBox.Show(strNhan);
        }
    }
}
