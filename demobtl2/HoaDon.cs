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
            ///txtMaHD.Enabled = false;
           hiendlcbkhachhang();
            
            hienlenlv();
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            hienmanv();
            btnHuy.PerformClick();
        }
        private void hienlenlv()
        {
            Connect();
            string query = "tthoadon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvHoaDon.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                //MessageBox.Show(rd.GetString(1));
                string mahd = rd.GetString(0);
                string manv = rd.GetString(1);
                DateTime dt = rd.GetDateTime(3);
                string makh = rd.GetString(2);
                           
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(mahd);
                lv.SubItems.Add(manv);
                lv.SubItems.Add(makh);
                lv.SubItems.Add(dt.ToString());
                lvHoaDon.Items.Add(lv);
            }
            rd.Close();
        }
        private void hiendlcbkhachhang()
        {

            Connect();
            string query = "KhachHang";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar);
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            cbMaKH.DataSource = tb;
            cbMaKH.DisplayMember = "sTenKH";
            cbMaKH.ValueMember = "sMaKH";
        }
        private void hienthimotkhachhang()
        {
            try
            {
                Connect();
                string query = "KhachHang";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaKH.SelectedValue.ToString();
                cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    //txtTenKH.Text = rd.GetString(0);
                    txtDiaChi.Text = rd.GetString(2);
                    txtDienThoai.Text = rd.GetString(3);
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void Action(string query, string action, string loi, string thanhcong)
        {
            Connect();
            //MessageBox.Show(cbMaNhanVien.Text);
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            MessageBox.Show(mskNgayBan.Text);
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
            cmd.Parameters.AddWithValue("@manv", SqlDbType.NVarChar).Value = txtMaNV.Text;
            cmd.Parameters.AddWithValue("@ngayban", SqlDbType.DateTime).Value = mskNgayBan.Text;
            cmd.Parameters.AddWithValue("@makh", SqlDbType.NVarChar).Value = cbMaKH.SelectedValue;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = action;
            //SqlDataReader rd = cmd.ExecuteReader();
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show(thanhcong);
            }
            else
            {
                MessageBox.Show(loi);
            }
            hienlenlv();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = "tthoadon";
            string action = "update";
            string loi = "Sửa không thành công";
            string thanhcong = "Sửa thành công";
            Action(query, action, loi, thanhcong);
        }
        public void hienmanv()
        {
            Connect();
            string query = "sp_nvdn";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader();
            string ma = null;
            string ten = null;
            while (rd.Read())
            {
                ma = rd.GetString(0);
                ten = rd.GetString(1);
            }
            txtMaNV.Text = ma;
            txtTenNV.Text = ten;
            rd.Close();
        }

        private void cbMaKH_TextChanged(object sender, EventArgs e)
        {
            hienthimotkhachhang();
        }

        private void lvHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHoaDon.SelectedItems.Count == 0)
            {
                return;
            }
            if (lvHoaDon.SelectedItems.Count > 0)
            {
                txtMaHD.Enabled = false;
                btnSua.Enabled = true;
                btnIn.Enabled = true;
                btnXemCT.Enabled = true;
                ListViewItem lvi1 = lvHoaDon.SelectedItems[0];
                txtMaHD.Text = lvi1.SubItems[0].Text;
                txtTenNV.Text = lvi1.SubItems[1].Text;
                mskNgayBan.Text = lvi1.SubItems[3].Text;
                cbMaKH.Text = lvi1.SubItems[2].Text;
                txtTenNV.Enabled = false;
            }   
        }
        public void Connect()
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
        public int kt1hang()
        {
            Connect();
            string query = "CTHD";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            int k = 0;
            if(rd.HasRows)
            {
                k = 1;
            }
            rd.Close();
            return k;
        }
        private void hientimkiemhoadon()
        {
            Connect();
            string query = "tthoadon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtTimkiem.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "search";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvHoaDon.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string mahd = rd.GetString(0);
                string manv = rd.GetString(1);
                DateTime dt = rd.GetDateTime(3);
                string makh = rd.GetString(2);
                ListViewItem lv = new ListViewItem(mahd);
                lv.SubItems.Add(manv);
                lv.SubItems.Add(makh);
                lv.SubItems.Add(dt.ToString());
                lvHoaDon.Items.Add(lv);
            }
            rd.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            inloaihang ilh =new inloaihang(txtMaHD.Text);
            ilh.Show();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            string query = "tthoadon";
            string action = "insert";
            string loi = "Thêm không thành công";
            string thanhcong = "Thêm thành công";
            Action(query, action, loi, thanhcong);
        }

        private void txtTimkiem_TextChanged_1(object sender, EventArgs e)
        {

            if (txtTimkiem.Text.Trim().Length == 0)
            {
                hienlenlv();
            }
            else
            {
                hientimkiemhoadon();
            }
            
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            string tiento = "HD";
            txtMaHD.Text=CreateKey(tiento);
            btnSua.Enabled = false;
            btnIn.Enabled = false;
            btnXemCT.Enabled = false;
            mskNgayBan.Clear();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            string query = "tthoadon";
            string action = "update";
            string loi = "Sửa không thành công";
            string thanhcong = "Sửa thành công";
            Action(query, action, loi, thanhcong);
        }

        private void btnIn_Click_1(object sender, EventArgs e)
        {
            inchitiethoadon incthd = new inchitiethoadon(txtMaHD.Text);
            incthd.Show();
        }

        private void btnXemCT_Click(object sender, EventArgs e)
        {
            if (kt1hang() == 1)
            {
                DialogResult rt = MessageBox.Show("Bạn chưa thêm chi tiết hóa đơn, Bạn có muốn thêm ngay không ?","Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rt == DialogResult.Yes)
                {
                    Chitiethoadon cthd = new Chitiethoadon(txtMaHD.Text);
                    cthd.MdiParent = this;
                    cthd.Show();
                }
                else
                {
                    return;
                }
            }
            else
            {
                Chitiethoadon cthd = new Chitiethoadon(txtMaHD.Text);                
                cthd.Show();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
