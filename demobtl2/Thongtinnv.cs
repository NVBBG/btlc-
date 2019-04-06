using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demobtl2
{
    public partial class Thongtinnv : Form
    {
        public Thongtinnv()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        // string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Thongtinnv_Load(object sender, EventArgs e)
        {
            hienthithongtin();
        }
        private void hienthithongtin()
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
                txtMa.Enabled = false;
                txtMa.Text = rd.GetString(0);
                txtHoTen.Text = rd.GetString(1);
                if (rd.GetBoolean(2))
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
                txtDiaChi.Text = rd.GetString(3);
                mksNgaySinh.Text = rd.GetDateTime(4) + "";
                txtSdt.Text = rd.GetString(8);
                txtTk.Text = rd.GetString(5);
                txtMkc.Text = rd.GetString(6);
            }
            rd.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@ma",SqlDbType.VarChar).Value=txtMa.Text;
            cmd.Parameters.AddWithValue("@ten",SqlDbType.VarChar).Value=txtHoTen.Text;
            if (rdNam.Checked == true)
            {
                cmd.Parameters.AddWithValue("@gt", SqlDbType.Bit).Value = true;
            }
            else
            {
                cmd.Parameters.AddWithValue("@gt", SqlDbType.Bit).Value = false;
            }
            cmd.Parameters.AddWithValue("@diachi",SqlDbType.VarChar).Value=txtDiaChi.Text;
            cmd.Parameters.AddWithValue("@ngaysinh",SqlDbType.VarChar).Value=mksNgaySinh.Text;
            cmd.Parameters.AddWithValue("@tk",SqlDbType.VarChar).Value=txtTk.Text;
            cmd.Parameters.AddWithValue("@mk",SqlDbType.VarChar).Value=txtMkc.Text;
            cmd.Parameters.AddWithValue("@sdt",SqlDbType.VarChar).Value=txtSdt.Text;
            cmd.Parameters.AddWithValue("@trangthai",SqlDbType.VarChar).Value="nhanvien";
            cmd.Parameters.AddWithValue("@action",SqlDbType.VarChar).Value = "update";
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin thất bại!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMkm.Text != txtXnmkm.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không hợp lệ");
            }
            else
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
                cmd.Parameters.AddWithValue("@ma",SqlDbType.NVarChar).Value= txtMa.Text;
                cmd.Parameters.AddWithValue("@tk",SqlDbType.NVarChar).Value= txtTk.Text;
                cmd.Parameters.AddWithValue("@mk",SqlDbType.NVarChar).Value= txtMkm.Text;
                cmd.Parameters.AddWithValue("@action",SqlDbType.NVarChar).Value= "doimk";
                int i = cmd.ExecuteNonQuery();
                if(i>0){
                    MessageBox.Show("Đổi mật khẩu thành công");
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại");
                }
            }

        }
    }
}
