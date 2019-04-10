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
    public partial class MyPham : Form
    {
        string baobao;
        public MyPham()
        {
            InitializeComponent();
        }
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
        private void MyPham_Load(object sender, EventArgs e)
        {
            btnHuy.PerformClick();
            hienthi();
            hienlencombobox();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        //string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        public void hienthi()
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
            //cmd.CommandText = "MyPham";
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvMyPham.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string ma = rd.GetString(0);
                string maloai = rd.GetString(1);
                string tenhang = rd.GetString(2);
                int sl = rd.GetInt32(3);
                Double dongia = rd.GetDouble(4);

                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(ma);
                lv.SubItems.Add(maloai);
                lv.SubItems.Add(tenhang);
                lv.SubItems.Add(sl + "");
                lv.SubItems.Add(dongia + "");
                lvMyPham.Items.Add(lv);
            }
            rd.Close();
        }
        private void lvMyPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMyPham.SelectedItems.Count == 0)
            {
                return;
            }
            if (lvMyPham.SelectedItems.Count > 0)
            {
                // cbMaLoai.Enabled = false;
                ListViewItem lvi = lvMyPham.SelectedItems[0];
                txtMaSp.Text = lvi.SubItems[2].Text;
                txtTenMp.Text = lvi.SubItems[1].Text;
                cbMaLoai.Text = lvi.SubItems[0].Text;
                txtSoLuong.Text = lvi.SubItems[3].Text;
                txtDonGia.Text = lvi.SubItems[4].Text;
            }
            btnSua.Enabled = true;
            //btnXoa.Enabled = true;
            // HienThiMpMa(ma);
        }
        private void hienlencombobox()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "LoaiHang";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", SqlDbType.VarChar).Value = "select";
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            cbMaLoai.DataSource = tb;
            cbMaLoai.DisplayMember = "sTenloaihang";
            cbMaLoai.ValueMember = "sMaloaihang";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cbMaLoai.Text = "";
            txtDonGia.Clear();
            txtMaSp.Clear();
            txtTenMp.Clear();
            txtSoLuong.Clear();
            btnSua.Enabled = false;
            txtTenMp.Enabled = false;
            string tiento = "MP";
            txtTenMp.Text = CreateKey(tiento);
            //btnXoa.Enabled = false;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = txtMaSp.Text;
            cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = txtTenMp.Text;
            cmd.Parameters.AddWithValue("@maloai", SqlDbType.NVarChar).Value = cbMaLoai.SelectedValue;
            cmd.Parameters.AddWithValue("@soluong", SqlDbType.NVarChar).Value = txtSoLuong.Text;
            cmd.Parameters.AddWithValue("@dongia", SqlDbType.NVarChar).Value = txtDonGia.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "insert";
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            btnHuy.PerformClick();
            hienthi();
        }

        private void btnSua_Click(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = txtMaSp.Text;
            cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = txtTenMp.Text;
            cmd.Parameters.AddWithValue("@maloai", SqlDbType.NVarChar).Value = cbMaLoai.SelectedValue;
            cmd.Parameters.AddWithValue("@soluong", SqlDbType.NVarChar).Value = txtSoLuong.Text;
            cmd.Parameters.AddWithValue("@dongia", SqlDbType.NVarChar).Value = txtDonGia.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "update";
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            btnHuy.PerformClick();
            hienthi();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim().Length == 0)
            {
                hienthi();
            }
            else
            {
                hientimkiem();
            }
        }
        private void hientimkiem()
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
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = txtTimKiem.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "search";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvMyPham.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string ma = rd.GetString(0);
                string maloai = rd.GetString(2);
                string tenhang = rd.GetString(1);
                int sl = rd.GetInt32(3);
                Double dongia = rd.GetDouble(4);

                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(ma);
                lv.SubItems.Add(maloai);
                lv.SubItems.Add(tenhang);
                lv.SubItems.Add(sl + "");
                lv.SubItems.Add(dongia + "");
                lvMyPham.Items.Add(lv);
            }
            rd.Close();
        }
    }
}
