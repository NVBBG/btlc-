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
    public partial class Form1 : Form
    {
        public Form1()
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
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
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
       

        private void Form1_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                btnDangNhap.Enabled = false;
            }
        }

        private void txtTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
           
    
        }

        private void txtTaiKhoan_KeyUp(object sender, KeyEventArgs e)
        {

            btnDangNhap.PerformClick();
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
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "sp_session";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@manv", SqlDbType.NVarChar).Value = laythongtin();
            int i2 = cmd.ExecuteNonQuery();
            if (i2 > 0)
            {
                //this.Dispose();
                NhanVienQuanLy nv = new NhanVienQuanLy();
                nv.Show();
                //MessageBox.Show("Đăng nhập thành công!");
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
        private void kiemtradangnhap()
        {

            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
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
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                txtTaiKhoan.Focus();
            }
        }
    }
}
