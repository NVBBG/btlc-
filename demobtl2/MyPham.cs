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
        public MyPham()
        {
            InitializeComponent();
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
            SqlCommand cmd = new SqlCommand(query,cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "MyPham";
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "select";
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
                lv.SubItems.Add(sl+ "");
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
                txtMaSp.Text = lvi.SubItems[1].Text;
                txtTenMp.Text = lvi.SubItems[2].Text;
                cbMaLoai.Text = lvi.SubItems[0].Text;
                txtSoLuong.Text = lvi.SubItems[3].Text;
                txtDonGia.Text = lvi.SubItems[4].Text;
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
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
            cbMaLoai.Text="";
            txtDonGia.Clear();
            txtMaSp.Clear();
            txtTenMp.Clear();
            txtSoLuong.Clear();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
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
            if(i>0)
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

        private void btnXoa_Click(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "delete";
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            btnHuy.PerformClick();
            hienthi();
        }
    }
}
