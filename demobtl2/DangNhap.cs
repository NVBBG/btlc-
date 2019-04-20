using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace demobtl2
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát ?", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }
        private string laythongtin()
        {
            Connect();
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = txtTaiKhoan.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selecttk";
            //MessageBox.Show(txtTaiKhoan.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            string matk=null;
            while (rd.Read())
            {
                matk = rd.GetString(0);
            }
            rd.Close();
            //MessageBox.Show(matk);
            return matk;
        }
        private string layquyen()
        {
            Connect();
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = txtTaiKhoan.Text;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selecttk";
            //MessageBox.Show(txtTaiKhoan.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            string quyen = null;
            while (rd.Read())
            {
                quyen = rd.GetString(7);
            }
            rd.Close();
            //MessageBox.Show(matk);
            return quyen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ktluutk();
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                btnDangNhap.Enabled = false;
            }
            //string hello = "coa to ndm";
            // MyPham my = new MyPham(hello);
        }

        private void txtTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((Char)Keys.Enter))
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void txtTaiKhoan_KeyUp(object sender, KeyEventArgs e)
        {

            //btnDangNhap.PerformClick();
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text)) {
                errorProvider1.SetError(txtTaiKhoan, "Tên đăng nhập không được bỏ trống!!");
                btnDangNhap.Enabled = false;
            }
            else if (txtMatKhau.Text.Trim().Length < 6)
            {
                errorProvider1.SetError(txtMatKhau, "Mật khẩu quá ngắn!");
                btnDangNhap.Enabled = false;
            }
            else
            {  
                errorProvider1.SetError(txtTaiKhoan, string.Empty);
                errorProvider1.SetError(txtMatKhau, string.Empty);
                btnDangNhap.Enabled = true;
            }

        }

        private void txtMatKhau_KeyUp(object sender, KeyEventArgs e)
        {

        
            if (txtMatKhau.Text.Trim().Length < 6)
            {
                errorProvider1.SetError(txtMatKhau, "Mật khẩu quá ngắn!");
                btnDangNhap.Enabled = false;
            }
            else if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                errorProvider1.SetError(txtTaiKhoan, "Tên đăng nhập không được bỏ trống!!");
                btnDangNhap.Enabled = false;
            }
            else
            {
                
                string sua = txtTaiKhoan.Text;
                string[] lines = sua.Split(' ');
                txtTaiKhoan.Text = "";
                foreach (string x in lines)
                {
                    txtTaiKhoan.Text += x;
                }
                errorProvider1.SetError(txtTaiKhoan, string.Empty);
                errorProvider1.SetError(txtMatKhau, string.Empty);
                btnDangNhap.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void luudangnhap()
        {
            Connect();
            string query = "sp_session";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@manv", SqlDbType.NVarChar).Value = laythongtin();
            if(ckLuumk.Checked)
            {
            cmd.Parameters.AddWithValue("@trangthai", SqlDbType.NVarChar).Value = 1;
            }
            else
            {
            cmd.Parameters.AddWithValue("@trangthai", SqlDbType.NVarChar).Value = 0;
            }
            int i2 = cmd.ExecuteNonQuery();
            if ((i2 > 0) && (layquyen().Trim() == "nhanvien"))
            {
                //this.Dispose();
                NhanVienQuanLy nv = new NhanVienQuanLy();
                nv.Show();
                //MessageBox.Show("Đăng nhập thành công!");
            }
            else if((i2 > 0) && (layquyen().Trim() == "admin"))
            {
                admin admin = new admin();
                admin.Show();
            }
            else if((i2 > 0) && (layquyen().Trim() == "khoa"))
            {
                MessageBox.Show("Nhân viên này đã nghỉ việc.Vui lòng đăng nhập với tài khoản khác");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!");
            }
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            kiemtradangnhap();  
        }
        private void ktluutk()
        {
            try
            {
                Connect();
                string query = "get_session";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmd.ExecuteReader();
                //cnn.Close();
                while (rd.Read())
                {
                    int num = rd.GetInt32(1);
                    // MessageBox.Show(num.ToString());
                    if ((int.Parse(num.ToString())) == 1)
                    {
                        rd.Close();
                        hientthisession();
                    }
                    else
                    {
                        txtMatKhau.Text = "";
                        txtTaiKhoan.Text = "";

                    }
                }
                rd.Close();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            
         
        }
        private void hientthisession()
        {
           if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "sp_nvdn";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtTaiKhoan.Text = rd.GetString(5);
                txtMatKhau.Text = rd.GetString(6);
            }
            ckLuumk.Checked = true;
            rd.Close();
        }
        private void Connect()
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
        private void kiemtradangnhap()
        {
            Connect();
            string query = "NhanVien";
            string tk = txtTaiKhoan.Text;
            string mk = txtMatKhau.Text;
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tk", SqlDbType.NVarChar).Value = tk;
            cmd.Parameters.AddWithValue("@mk", SqlDbType.NVarChar).Value = mk;
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "kiemtradangnhap";
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Close();
                luudangnhap();
            }
            else
            {
                rd.Close();
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                txtTaiKhoan.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = char.Parse("\0");
        }

        private void checkBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtMatKhau.PasswordChar = char.Parse("\0");
            else
                txtMatKhau.PasswordChar = char.Parse("*");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtMatKhau.PasswordChar = char.Parse("\0");
            else
                txtMatKhau.PasswordChar = char.Parse("*");
        }

        private void ckLuumk_CheckedChanged(object sender, EventArgs e)
        {
            int ma;
            if (ckLuumk.Checked)
            {
                ma = 1;
                CapNhat(ma);
                
            }
            else
            {
                ma = 0;
                CapNhat(ma);
            }
        }
        private void CapNhat(int ma)
        {
            Connect();
            string query = "changedn";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.Int).Value = ma;
            
        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((Char)Keys.Enter))
            {
                btnDangNhap_Click(sender, e);
            }
        }
    }
}
