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
            string query = "NhanVien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = laythongtindn();
            cmd.Parameters.AddWithValue("@action", SqlDbType.NVarChar).Value = "selectone";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txtMa.Enabled = false;
                txtMa.Text = rd.GetString(0);
                txtHoTen.Text = rd.GetString(1);
                if (rd.GetBoolean(2))
                {
                    rdNu.Checked = true;
                }
                else
                {
                    rdNam.Checked = true;
                }
                txtDiaChi.Text = rd.GetString(3);
                mksNgaySinh.Text = rd.GetDateTime(4) + "";
                txtSdt.Text = rd.GetString(8);
                txtTk.Text = rd.GetString(5);
                txtMkc.Text = rd.GetString(6);
            }
            rd.Close();
        }
        public string laythongtindn()
        {
            if (cnn == null)
            {
                cnn = new SqlConnection(constr);
            }
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            string query = "get_session";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader();
            string ma = null;
            while (rd.Read())
            {
                ma = rd.GetString(0);
            }
            rd.Close();
            return ma;
        }
    }
}
