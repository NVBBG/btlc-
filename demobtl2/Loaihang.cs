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
    public partial class Loaihang : Form
    {
        public Loaihang()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        private void Loaihang_Load(object sender, EventArgs e)
        {
             if(txtMaLoaiHang.Text=="")
            {
                btnSua.Enabled = false;
                //btnXoa.Enabled = false;
               // btnLuu.Enabled = false;
            }
            Hien();
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
        public void Hien()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoaiHang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvMatHang.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string maloai = rd.GetString(0);
                string tenloai = rd.GetString(1);
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(maloai);
                lv.SubItems.Add(tenloai);
                lvMatHang.Items.Add(lv);
            }
            rd.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            InLoaiHang lh = new InLoaiHang();
            lh.Show();
        }

        private void lvMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMatHang.SelectedItems.Count == 0)
            {
                return;
            }
            if(lvMatHang.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvMatHang.SelectedItems[0];
                txtMaLoaiHang.Text = lvi.SubItems[0].Text;
                txtTenLoaiHang.Text = lvi.SubItems[1].Text;
            }
            txtMaLoaiHang.Enabled = false;
            btnSua.Enabled = true;
            //MessageBox.Show(ma);
            //HienThiLoai(ma);
        }
        private void KiemTraTextBox()
        {
            if(txtMaLoaiHang.Text !=null )
            {
                btnSua.Enabled = true;
               // btnXoa.Enabled = true;
               // btnLuu.Enabled = true;
            }
            else
            {
                btnSua.Enabled = false;
                //btnXoa.Enabled = false;
              //  btnLuu.Enabled = false;
            }
        }
        private void KiemTraRong()
        {
            if (txtMaLoaiHang == null || txtTenLoaiHang == null)
            {
                MessageBox.Show("Các Trường Không Được Để Trống");
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
            string ma = txtMaLoaiHang.Text;
            string ten = txtTenLoaiHang.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoaiHang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@malh", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@tenlh", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "insert";
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                Hien();
                MessageBox.Show("Thêm thành công!");
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại!");
            }
            btnHuy.PerformClick();
          
            
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
            string ma = txtMaLoaiHang.Text;
            
            string ten = txtTenLoaiHang.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoaiHang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@malh", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@tenlh", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "update";
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                Hien();
                MessageBox.Show("Sửa thành công!");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại!");
            }

            KiemTraTextBox();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaLoaiHang.Clear();
            txtTenLoaiHang.Clear();
            txtMaLoaiHang.Enabled = true;
            btnSua.Enabled = false;
           // btnXoa.Enabled = false;
           // btnLuu.Enabled = false;
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimkiem.Text.Trim().Length == 0)
            {
                Hien();
            }
            else
            {
                timkiemloaihang();
            }
        }

        private void timkiemloaihang()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string ma = txtTimkiem.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoaiHang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@malh", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "search";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvMatHang.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string maloai = rd.GetString(0);
                string tenloai = rd.GetString(1);
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(maloai);
                lv.SubItems.Add(tenloai);
                lvMatHang.Items.Add(lv);
            }
            rd.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }
    }
}
