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
            btnHuy.PerformClick();
            hien();
            if (txtMaKH.Text == "")
            {  
                btnSua.Enabled = false;
                //btnLuu.Enabled = false;
                //btnXoa.Enabled = false;
            }
           
            
        }
        private void hien()
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
            if (lvKH.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvKH.SelectedItems[0];
                txtMaKH.Text = lvi.SubItems[0].Text;
                txtTenKH.Text = lvi.SubItems[1].Text;
                txtDiaChi.Text = lvi.SubItems[2].Text;
                txtDienThoai.Text = lvi.SubItems[4].Text;
                //MessageBox.Show(lvi.SubItems[3].Text);
                if ((lvi.SubItems[3].Text)=="Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }
            }
            txtMaKH.Enabled = false;
            btnSua.Enabled = true;
           // btnXoa.Enabled = true;
           // btnLuu.Enabled = true;
           // HienThiKH(ma);
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
        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            rdNam.Checked = false;
            rdNu.Checked = false;
            btnSua.Enabled = false;
            //btnXoa.Enabled = false;
            //btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
            string tiento = "KH";
            txtMaKH.Text = CreateKey(tiento);
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
                    hien();
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
                hien();
                MessageBox.Show("Sửa thành công!");
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại!");
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

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimkiem.Text.Trim().Length == 0)
            {
                hien();
            }
            else
            {
            timkiemkhachhang();

            }
        }

        private void timkiemkhachhang()
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
            cmd.CommandText = "Khachhang";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = ma;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "search";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvKH.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                ma = rd.GetString(0);
                Boolean gioitinh = rd.GetBoolean(4);
                string ten = rd.GetString(1);
                string diachi = rd.GetString(2);
                string sdt = rd.GetString(3);

                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(ma);
                lv.SubItems.Add(ten);
                lv.SubItems.Add(diachi);
                if (gioitinh)
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
    }
}
