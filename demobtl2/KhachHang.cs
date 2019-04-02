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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
            ToolTip tip = new ToolTip();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        //string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ? ", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {   

            
            hien(constr, cnn);
            if (txtMaKH.Text == "")
            {
              
                btnSua.Enabled = false;
                //btnLuu.Enabled = false;
                btnXoa.Enabled = false;
            }
           
            
        }
        private void hien(string constr, SqlConnection cnn)
        {
            if(cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if(cnn.State ==  ConnectionState.Closed)
            {
                cnn.Open();
            }
           string gt;
            string query = "Khachhang";
           // MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = query;
            cmd.Connection = cnn;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvKH.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string ma = rd.GetString(0);
                Boolean gioitinh = rd.GetBoolean(4);
                string ten = rd.GetString(1);
                string diachi = rd.GetString(2);
                string sdt = rd.GetString(3);
                
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(ma);
                lv.SubItems.Add(ten);
                lv.SubItems.Add(diachi);
                if(gioitinh)
                {
                    lv.SubItems.Add("Nam");
                }
                else
                {
                    lv.SubItems.Add("Nữ");
                }
                
                lv.SubItems.Add(sdt);
                lvKH.Items.Add(lv);
            }
            rd.Close();
        }
        private bool kiemtratontai()
        {
            bool kt = false;
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string maso = txtMaKH.Text;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("Select * from tblKhachhang", cnn);
           
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (maso == rd.GetString(0))
                {
                    kt = true;
                    break;
                }
            }
            rd.Close();
            con.Close();
            return kt;
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lvKH_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lvKH.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvKH.SelectedItems[0];
            string ma = lvi.SubItems[0].Text;
            txtMaKH.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
           // btnLuu.Enabled = true;
            HienThiKH(ma);
        }
        private void HienThiKH(string ma)
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
            cmd.CommandText = "Khachhang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtMaKH.Text = rd.GetString(0);
                txtTenKH.Text = rd.GetString(1);
                txtDiaChi.Text = rd.GetString(2);
                txtDienThoai.Text = rd.GetString(3);
                if (rd.GetBoolean(4))
                {
                    rdNam.Checked=true;
                }
                else
                {
                    rdNu.Checked=true;
                }
            }
           
            rd.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            rdNam.Checked = false;
            rdNu.Checked = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //btnLuu.Enabled = false;
            txtMaKH.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (kiemtratontai())
            {
                MessageBox.Show("Trùng Khóa Chính");
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
                string ma = txtMaKH.Text;
                string ten = txtTenKH.Text;
                string diachi = txtDiaChi.Text;
                string sdt = txtDienThoai.Text;
                Boolean gioitinh;
                if (rdNam.Checked == true)
                {
                    gioitinh = true;
                }
                else
                {
                    gioitinh = false;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Khachhang";
                cmd.Connection = cnn;
                cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = ma;
                cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
                cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
                cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = sdt;
                cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = gioitinh;
                cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "insert";
                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    hien(constr, cnn);
                    MessageBox.Show("Thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Thêm Thất Bại!");
                }
                btnHuy.PerformClick();
            }
            
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
            string ma = txtMaKH.Text;
            string ten = txtTenKH.Text;
            string diachi = txtDiaChi.Text;
            string sdt = txtDienThoai.Text;
            Boolean gioitinh;
            if (rdNam.Checked == true)
            {
                gioitinh = true;
            }
            else
            {
                gioitinh = false;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Khachhang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diachi;
            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = sdt;
            cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = gioitinh;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "update";
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                hien(constr, cnn);
                MessageBox.Show("Sửa thành công!");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại!");
            }
            btnHuy.PerformClick();
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
            string ma = txtMaKH.Text;
            string ten = txtTenKH.Text;
            string diachi = txtDiaChi.Text;
            string sdt = txtDienThoai.Text;
            Boolean gioitinh;
            if (rdNam.Checked == true)
            {
                gioitinh = true;
            }
            else
            {
                gioitinh = false;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Khachhang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "delete";
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                hien(constr, cnn);
                MessageBox.Show("Xóa thành công!");
            }
            else
            {
                MessageBox.Show("Xóa Thất Bại!");
            }
            btnHuy.PerformClick();
        }

        private void txtMaKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenKH_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void rdNam_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void rdNu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
