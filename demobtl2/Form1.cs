using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace demobtl2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtTaiKhoan.Text) == 1)
                {
                    NhanVienQuanLy nv = new NhanVienQuanLy();
                    nv.Show();
                }
                else
                {
                    NhanVienQuanLy hd = new NhanVienQuanLy();
                    hd.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi");
                return;
            }
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
    }
}
