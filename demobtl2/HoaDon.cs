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
using System.Data.SqlClient;
namespace demobtl2
{
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        SqlConnection cnn = null;
        //string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
        
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            string tiento = "HD";
            txtMaHD.Text = CreateKey(tiento);
            txtMaHD.Enabled = false;
            hiendlcbkhachhang();
            hienmahang();
        }
        private void hiendlcbkhachhang()
        {
            
                if (cnn == null)
                {
                    cnn = new SqlConnection(constr);
                }
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                string query = "KhachHang";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar);
                cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value ="select";
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable();
                ad.Fill(tb);
                cbMaKH.DataSource = tb;
                cbMaKH.DisplayMember = "sMaKH";
                cbMaKH.ValueMember = "sMaKH";
        }
        private void hienthimotkhachhang()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "KhachHang";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar).Value = cbMaKH.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtTenKH.Text = rd.GetString(1);
                txtDiaChi.Text = rd.GetString(2);
                txtDienThoai.Text = rd.GetString(3);
            }
            rd.Close();
        }
        private void hienmahang()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "MyPham";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar);
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            cbMaHang.DataSource = tb;
            cbMaHang.DisplayMember = "sMahang";
            cbMaHang.ValueMember = "sMahang";
        }
        private void hienmotmahang()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "MyPham";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtTenHang.Text = rd.GetString(1);
            }
            rd.Close();
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void cbMaKH_TextChanged(object sender, EventArgs e)
        {
            hienthimotkhachhang();
        }

        private void cbMaHang_TextChanged(object sender, EventArgs e)
        {
            hienmotmahang();
        }
    }
}
