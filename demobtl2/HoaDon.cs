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
            hienmahang();
            hienlenlv();
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            hienmanv();
        }
        private void hienlenlv()
        {
            Connect();
            string query = "HoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvHoaDon.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string mahd = rd.GetString(0);
                string manv = rd.GetString(1);
                string makh = rd.GetString(2);
                string mahang = rd.GetString(3);
                DateTime dt = rd.GetDateTime(4);
                double giaban = rd.GetDouble(5);
                int sl = rd.GetInt32(6);
                double giamgia = rd.GetDouble(7);
                double tt = (giaban * sl) - ((giaban * sl) * (giamgia / 100));
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(mahd);
                lv.SubItems.Add(manv);
                lv.SubItems.Add(makh);
                lv.SubItems.Add(mahang);
                lv.SubItems.Add(dt.ToString());
                lv.SubItems.Add(giaban + "");
                lv.SubItems.Add(sl + "");
                lv.SubItems.Add(giamgia + "");
                lv.SubItems.Add(tt + "");
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
        private void hienmahang()
        {
            Connect();
            string query = "MyPham";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar);
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            cbMaHang.DataSource = tb;
            cbMaHang.DisplayMember = "sTenhang";
            cbMaHang.ValueMember = "sMahang";
        }
        private void hienmotmahang()
        {
            Connect();
            string query = "MyPham";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    // txtTenHang.Text = rd.GetString(0);
                }
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
            string query = "HoaDon";
            string action = "insert";
            string loi = "Thêm không thành công";
            string thanhcong = "Thêm thành công";
            Action(query, action, loi, thanhcong);
        }
        private void Action(string query, string action, string loi, string thanhcong)
        {
            Connect();
            //MessageBox.Show(cbMaNhanVien.Text);

            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
            cmd.Parameters.AddWithValue("@manv", SqlDbType.NVarChar).Value = txtMaNV.Text;
            cmd.Parameters.AddWithValue("@makh", SqlDbType.NVarChar).Value = cbMaKH.SelectedValue;
            cmd.Parameters.AddWithValue("@mahang", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue;
            cmd.Parameters.AddWithValue("@ngayban", SqlDbType.DateTime).Value = mskNgayBan.TextMaskFormat;
            cmd.Parameters.AddWithValue("@soluong", SqlDbType.Int).Value = txtSoLuong.Text;
            cmd.Parameters.AddWithValue("@giaban", SqlDbType.Float).Value = txtDonGia.Text;
            cmd.Parameters.AddWithValue("@giamgia", SqlDbType.Float).Value = txtGiamGia.Text;
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
            string query = "HoaDon";
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cbMaHang.SelectedValue.ToString());
            btnIn.Enabled = false;
            btnSua.Enabled = false;
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

        private void lvHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHoaDon.SelectedItems.Count == 0)
            {
                return;
            }
            if (lvHoaDon.SelectedItems.Count > 0)
            {
                txtMaHD.Enabled = false;
                ListViewItem lvi1 = lvHoaDon.SelectedItems[0];
                txtMaHD.Text = lvi1.SubItems[0].Text;
                txtTenNV.Text = lvi1.SubItems[1].Text;
                cbMaKH.Text = lvi1.SubItems[2].Text;
                cbMaHang.Text = lvi1.SubItems[3].Text;
                mskNgayBan.Text = lvi1.SubItems[4].Text;
                txtDonGia.Text = lvi1.SubItems[5].Text;
                txtSoLuong.Text = lvi1.SubItems[6].Text;
                txtGiamGia.Text = lvi1.SubItems[7].Text;
                txttt.Text = lvi1.SubItems[8].Text;
                //txtTenKH.Enabled = false;
                txtTenNV.Enabled = false;
                //txtTenHang.Enabled = false;
            }
            // ListViewItem lvi = lvHoaDon.SelectedItems[0];
            //string ma = lvi.SubItems[0].Text;
            //.Enabled = false;
            btnIn.Enabled = true;
            btnSua.Enabled = true;
            // btnXoa.Enabled = true;
            // btnLuu.Enabled = true;
            //HienThiHD(ma);
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }
        private double kiemtradongia()
        {
            Connect();
            string query = "sp_ktdg";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue.ToString();
            SqlDataReader rd = cmd.ExecuteReader();
            double dg=0;
            while(rd.Read())
            {
                dg = rd.GetDouble(0);
            }
            rd.Close();
            return dg;
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
        private int kiemtrasoluong()
        {
            Connect();
            string query = "sp_ktsl";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue.ToString();
            SqlDataReader rd = cmd.ExecuteReader();
            //MessageBox.Show(rd.GetInt32(0).ToString());
            int sl=0;
            while (rd.Read())
            {
                 sl = rd.GetInt32(0);
            }
            rd.Close();
            return sl;
        }
        private void tinhtongtien()
        {
            int sl = 0;
            double giamgia = 0;
            double giaban = 0;
            if (txtDonGia.Text.Trim().Length == 0)
            {
                giaban = 0;
            }
            else
            {
                giaban = Double.Parse(txtDonGia.Text);
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                sl = 0;
            }
            else
            {
                sl = Int32.Parse(txtSoLuong.Text);
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                giamgia = 0;
            }
            else
            {
                giamgia = Int32.Parse(txtGiamGia.Text);
            }
            if(sl < 0 || sl > kiemtrasoluong())
            {
                MessageBox.Show("Số lượng phải là số nguyên dương và phải nhỏ hơn số lượng hiện có. Số lượng hàng hiện có:"+kiemtrasoluong());
                txtSoLuong.Clear();
            }
            if (giaban > kiemtradongia())
            {
                MessageBox.Show("Giá bán phải lớn hơn giá nhập vào.Giá nhập vào hiện tại :"+kiemtradongia());
                txtDonGia.Clear();
            }
            if (giamgia <0 || giamgia >100)
            {
                MessageBox.Show("Giảm giá phù hợp trong khoảng 0-100");
            }
            double tt = (giaban * sl) - ((giaban * sl) * (giamgia / 100));
            txttt.Text = tt.ToString();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            hientimkiemhoadon();
        }

        private void hientimkiemhoadon()
        {
            Connect();
            string query = "HoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtTimKiem.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "search";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvHoaDon.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string mahd = rd.GetString(0);
                string manv = rd.GetString(1);
                string makh = rd.GetString(2);
                string mahang = rd.GetString(3);
                DateTime dt = rd.GetDateTime(4);
                double giaban = rd.GetDouble(5);
                int sl = rd.GetInt32(6);
                double giamgia = rd.GetDouble(7);
                double tt = (giaban * sl) - ((giaban * sl) * (giamgia / 100));
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(mahd);
                lv.SubItems.Add(manv);
                lv.SubItems.Add(makh);
                lv.SubItems.Add(mahang);
                lv.SubItems.Add(dt.ToString());
                lv.SubItems.Add(giaban + "");
                lv.SubItems.Add(sl + "");
                lv.SubItems.Add(giamgia + "");
                lv.SubItems.Add(tt + "");
                lvHoaDon.Items.Add(lv);
            }
            rd.Close();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }
    }
}
