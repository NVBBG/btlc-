﻿using System;
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
    public partial class ThemNV : Form
    {
        public ThemNV()
        {
            InitializeComponent();
        }
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        //string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        private void hien(SqlConnection cnn, string constr)
        {
            // checkcnn();
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string gt;
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@action", SqlDbType.VarChar).Value = "select";
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            lvNhanVien.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
                string ma = rd.GetString(0);
                string ten = rd.GetString(1);
               // MessageBox.Show(rd.GetBoolean(2).ToString());
                Boolean gioitinh = rd.GetBoolean(2);
                string diachi = rd.GetString(3);
                DateTime ns = rd.GetDateTime(4);
                string tk = rd.GetString(5);
                string mk = rd.GetString(6);
                string tinhtrang = rd.GetString(7);
                string sdt = rd.GetString(8);
                //  Khai báo List View để hiển thị dữ liệu
                ListViewItem lv = new ListViewItem(ma);
                lv.SubItems.Add(ten);
                if (gioitinh)
                {
                    lv.SubItems.Add("Nam");
                }
                else
                {
                   lv.SubItems.Add("Nữ");
                }
                lv.SubItems.Add(diachi);
                lv.SubItems.Add(ns.ToString());
                lv.SubItems.Add(tk);
                lv.SubItems.Add(mk);
                lv.SubItems.Add(tinhtrang);
                lv.SubItems.Add(sdt);
                lvNhanVien.Items.Add(lv);
            }
            rd.Close();
        }
        private void ThemNV_Load(object sender, EventArgs e)
        {
            btnHuy.PerformClick();
            hien(cnn,constr);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void checkcnn()
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
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát ?","Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void lvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNhanVien.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvNhanVien.SelectedItems[0]; //ấn vào cột thứ nhất
            string ma = lvi.SubItems[0].Text; // chuyển dữ liệu từ cột đó về dạng int
            Hiennut();
            HienThiChiTietSP(ma);
        }
        private void HienThiChiTietSP(string ma)
        {
            //checkcnn();
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string gt;
            string query = "laynvma";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = ma;
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
           // lvNhanVien.Items.Clear(); // Xóa hết dữ liệu trên list view để chèn dữ liệu mới
            ListViewItem lvi = lvNhanVien.SelectedItems[0];
            txtMaNv.Text = lvi.SubItems[0].Text;
            txtTenNV.Text = lvi.SubItems[1].Text;
            if(lvi.SubItems[2].Text == "Nam")
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
            txtDiaChi.Text = lvi.SubItems[3].Text;
            mskns.Text = lvi.SubItems[4].Text;
            txtTaiKhoan.Text = lvi.SubItems[5].Text;
            txtXacNhanMK.Text = lvi.SubItems[6].Text;
            txtMatKhau.Text = lvi.SubItems[6].Text;
            if(lvi.SubItems[7].Text.Trim()=="khoa")
            {
                Mokhoa();
            }
            else if(lvi.SubItems[7].Text.Trim() == "nhanvien")
            {
                Khoa();
            }
            else
            {
                //MessageBox.Show(lvi.SubItems[7].Text);
                btnKhoa.Enabled = false;
                btnMoKhoa.Enabled = false;
            }
            txtSdt.Text = lvi.SubItems[8].Text;
            rd.Close();
        }
        private void Khoa()
        {
            btnKhoa.Enabled = true;
            btnMoKhoa.Enabled = false;
        }
        private void Mokhoa()
        {
            btnKhoa.Enabled = false;
            btnMoKhoa.Enabled = true;
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMaNv.Clear();
            txtTenNV.Clear();
            txtDiaChi.Clear();
            txtTaiKhoan.Clear();
            txtXacNhanMK.Clear();
            txtMatKhau.Clear();
            txtSdt.Clear();
            mskns.Clear();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnKhoa.Enabled = false;
            btnMoKhoa.Enabled = false;
        }
        private void Hiennut()
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //checkcnn();
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string gt;
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "insert";
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = txtMaNv.Text;
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtTenNV.Text;
            if (rdNam.Checked)
            {
                cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = true;
            }
            else
            {
                cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = false;
            }
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = mskns.Text;
            cmd.Parameters.Add("@tk", SqlDbType.NVarChar).Value = txtTaiKhoan.Text;
            cmd.Parameters.Add("@mk", SqlDbType.NVarChar).Value = txtXacNhanMK.Text;
            if (btnKhoa.Enabled)
            {
                cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = "nhanvien";
            }
            else
            {
                cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = "khoa";
            }
            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = txtSdt.Text;
            //SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
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
            hien(cnn, constr);
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            //checkcnn();
            string query = "khoatk";
            KhoaOrMoKhoa(query);
        }
        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            string query = "motk";
            KhoaOrMoKhoa(query);
        }
        private void KhoaOrMoKhoa(string query)
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            //string gt;
            //string query = "khoatk";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = txtMaNv.Text;
            //MessageBox.Show(txtMaNv.Text);
            int i = cmd.ExecuteNonQuery();
            //MessageBox.Show(i+"");
            if (i > 0)
            {
                if (query == "khoatk")
                {
                    MessageBox.Show("Đã khóa tài khoản");
                    Mokhoa();
                }
                else
                {
                    MessageBox.Show("Đã mở tài khoản");
                    Khoa();
                }
            }
            else
            {
                if (query == "khoatk")
                {
                    MessageBox.Show("Khóa tài khoản thất bại");
                }
                else
                {
                    MessageBox.Show("Mở tài khoản thất bại");
                }
               //MessageBox.Show("Khóa tài khoản thất bại");
            }
            //btnHuy.PerformClick();
            hien(cnn,constr);
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
            string gt;
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "update";
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = txtMaNv.Text;
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtTenNV.Text;
            if (rdNam.Checked)
            {
                cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = true;
            }
            else
            {
                cmd.Parameters.Add("@gt", SqlDbType.Bit).Value = false;
            }
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = mskns.Text;
            cmd.Parameters.Add("@tk", SqlDbType.NVarChar).Value = txtTaiKhoan.Text;
            cmd.Parameters.Add("@mk", SqlDbType.NVarChar).Value = txtXacNhanMK.Text;
            if (btnKhoa.Enabled)
            {
                cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = "nhanvien";
            }
            else
            {
                cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = "khoa";
            }
            cmd.Parameters.Add("@sdt", SqlDbType.NVarChar).Value = txtSdt.Text;
            //SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            lvNhanVien.Refresh();
            hien(cnn, constr);
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
            string gt;
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ma", SqlDbType.NVarChar).Value = txtMaNv.Text;
            cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = "delete";
            int i = cmd.ExecuteNonQuery();
            if(i>0)
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            hien(cnn, constr);
        }
    }
}
