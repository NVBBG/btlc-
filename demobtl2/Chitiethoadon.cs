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
    public partial class Chitiethoadon : Form
    {
        
        public Chitiethoadon()
        {
            InitializeComponent();
        }
        public string mahd;
        public Chitiethoadon(string text)
        {
            InitializeComponent();
            //MessageBox.Show(text);
             mahd = text;
        }

        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        SqlConnection cnn = null;
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

        private void hienmotmahang()
        {
            //MessageBox.Show(mahd);
            Connect();
            string query = "add_ctHoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = mahd;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectallinone";
            SqlDataReader rd = cmd.ExecuteReader();
            lvCTHD.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                //MessageBox.Show(rd.GetString(1));
                string mahd = rd.GetString(0);
                string mahang = rd.GetString(1);
                string tenhang = rd.GetString(2);
                int sl = rd.GetInt32(3);
                Double giaban = rd.GetDouble(4);
                Double giamgia = rd.GetDouble(5);
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(mahd);
                lv.SubItems.Add(mahang);
                lv.SubItems.Add(tenhang);
                lv.SubItems.Add(sl+"");
                lv.SubItems.Add(giaban+"");
                lv.SubItems.Add(giamgia+"");
                lvCTHD.Items.Add(lv);
            }
            rd.Close();
        }
        private double kiemtradongia()
        {
            Connect();
            string query = "sp_ktdg";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue.ToString();
            SqlDataReader rd = cmd.ExecuteReader();
            double dg = 0;
            while (rd.Read())
            {
                dg = rd.GetDouble(0);
            }
            rd.Close();
            return dg;
        }
        private void Chitiethoadon_Load(object sender, EventArgs e)
        {
            txtMaHD.Enabled = false;
            btnSua.Enabled = false;
            btnIn.Enabled = true;
            txtMaHD.Text = mahd;
            hienmotmahang();
            hienmahang();
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
        private void hienthicthd()
        {
            
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
            int sl = 0;
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
            if (txtGiamgia.Text.Trim().Length == 0)
            {
                giamgia = 0;
            }
            else
            {
                giamgia = Int32.Parse(txtGiamgia.Text);
            }
            if (sl < 0 || sl > kiemtrasoluong())
            {
                MessageBox.Show("Số lượng phải là số nguyên dương và phải nhỏ hơn số lượng hiện có. Số lượng hàng hiện có:" + kiemtrasoluong());
                txtSoLuong.Clear();
            }
            if (giaban > kiemtradongia())
            {
                MessageBox.Show("Giá bán phải lớn hơn giá nhập vào.Giá nhập vào hiện tại :" + kiemtradongia());
                txtDonGia.Clear();
            }
            if (giamgia < 0 || giamgia > 100)
            {
                MessageBox.Show("Giảm giá phù hợp trong khoảng 0-100");
            }
            double tt = (giaban * sl) - ((giaban * sl) * (giamgia / 100));
            txtThanhTien.Text = tt.ToString();
        }
        

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }

        private void txtGiamgia_TextChanged(object sender, EventArgs e)
        {
            tinhtongtien();
        }

        private void lvCTHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCTHD.SelectedItems.Count == 0)
            {
                return;
            }
            if (lvCTHD.SelectedItems.Count > 0)
            {
                txtMaHD.Enabled = false;
                btnSua.Enabled = true;
                btnIn.Enabled = true;               
                ListViewItem lvi1 = lvCTHD.SelectedItems[0];
                txtMaHD.Text = lvi1.SubItems[0].Text;
                cbMaHang.Text = lvi1.SubItems[2].Text;
                txtDonGia.Text = lvi1.SubItems[4].Text;
                txtSoLuong.Text = lvi1.SubItems[3].Text;
                txtGiamgia.Text = lvi1.SubItems[5].Text;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnIn.Enabled = true;
            cbMaHang.Text = "";
            txtSoLuong.Clear();
            txtGiamgia.Clear();
            txtDonGia.Clear();
            //MessageBox.Show(kiemtrahang().ToString());
            //kiemtrahang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string action = "update";
            string loi = "Sửa thất bại";
            string ok = "Sửa thành công";
            Action(action, loi, ok);
        }
        private void Action(string action,string loi,string ok)
        {
            Connect();
            string query = "add_ctHoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
            cmd.Parameters.AddWithValue("@mahang", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue;
            cmd.Parameters.AddWithValue("@slban", SqlDbType.Int).Value = txtSoLuong.Text;
            cmd.Parameters.AddWithValue("@giaban", SqlDbType.Float).Value = txtDonGia.Text;
            cmd.Parameters.AddWithValue("@giamgia", SqlDbType.Float).Value = txtGiamgia.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = action;
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show(ok);
            }
            else
            {
                MessageBox.Show(loi);
            }
            hienmotmahang();
            btnHuy.PerformClick();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if(kiemtrahang().ToString()=="1")
            {
                MessageBox.Show("Chi tiết hóa đơn đã có mặt hàng này vui lòng chọn mặt hàng khách");
                return;
            }
            else
            {
                string action = "insert";
                string loi = "Thêm thất bại";
                string ok = "Thêm thành công";
                Action(action, loi, ok);
            }
        }
        private int kiemtrahang()
        {
            Connect();
            string query = "add_ctHoaDon";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //string txt = cbMaHang.SelectedValue.ToString();
            //MessageBox.Show(txt);
            cmd.Parameters.AddWithValue("@mahd", SqlDbType.NVarChar).Value = txtMaHD.Text;
            cmd.Parameters.AddWithValue("@mahang", SqlDbType.NVarChar).Value = cbMaHang.SelectedValue;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();           
            int mact = 0;
            while (rd.Read())
            {
                if (rd.HasRows)
                {
                    mact = 1;
                }
            }
            rd.Close();
            return mact;
        }

        private void cbMaHang_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không?","Hỏi thoát",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            inchitiethoadon ict = new inchitiethoadon(txtMaHD.Text);
            ict.MdiParent = this;
            ict.Show();
        }
    }
}
