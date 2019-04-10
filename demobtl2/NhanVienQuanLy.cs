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
    public partial class NhanVienQuanLy : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        //string constr = "Data Source=MRBAO;Initial Catalog=QLBHMP;Integrated Security=True";
        SqlConnection cnn = null;
        public NhanVienQuanLy()
        {
            InitializeComponent();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát", "Hỏi thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.Show();
        }
        private bool CheckExistForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        public Boolean KiemTraTonTai(string Frmname)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name.Equals(Frmname))
                    return true;
            }
            return false;
        }
        private void NhanVienQuanLy_Load(object sender, EventArgs e)
        {

            //MenuNV ql = new MenuNV();
            //ql.MdiParent = this;
            //ql.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //ql.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            // ql.Dock = DockStyle.Fill;
            //ql.Show();
            hienthongtinnv();
        }
        private void hienthongtinnv()
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
            string query = "sp_nvdn";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = cmd.ExecuteReader(); // Khai báo DataReader
           
            while (rd.Read())
            {
                // Khai báo các biến để lưu dữ liệu lấy từ SQL Sever về
              // lbMa.Text = rd.GetString(0);
               lbTen.Text = rd.GetString(1);
                
            }
            rd.Close();
        }
        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {   
           
           
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thongketheothang nv = new thongketheothang();
            if (CheckExistForm("thongketheothang"))
            {
                nv.MdiParent = this;
                nv.Show();
            }
            else
            {
                return;
            }
        }

        private void quảnLýHóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHang nv = new KhachHang();
            if (CheckExistForm("KhachHang"))
            {
                nv.MdiParent = this;
                nv.Show();
            }
            else
            {
                return;
            }
        }

        private void loạiMỹPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loaihang nv = new Loaihang();
            if (CheckExistForm("Loaihang"))
            {
                nv.MdiParent = this;
                nv.Show();
            }
            else
            {
                return;
            }
        }

        private void quảnLýMỹPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyPham nv = new MyPham();
            if (CheckExistForm("MyPham"))
            {
                nv.MdiParent = this;
                nv.Show();
            }
            else
            {
                return;
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không ?", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt==DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void quảnLýHóaĐơnToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            if (KiemTraTonTai("HoaDon") == true)
                hd.Activate();
            else
            {
                hd.MdiParent = this;
                hd.Show();
            }
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            KhachHang kh = new KhachHang();
            if (KiemTraTonTai("KhachHang") == true)
                kh.Activate();
            else
            {
                kh.MdiParent = this;
                kh.Show();
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            if (KiemTraTonTai("HoaDon") == true)
                hd.Activate();
            else
            {
                hd.MdiParent = this;
                hd.Show();
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            if (KiemTraTonTai("KhachHang") == true)
                kh.Activate();
            else
            {
                kh.MdiParent = this;
                kh.Show();
            }
          
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            MyPham tk = new MyPham();
            if (KiemTraTonTai("MyPham") == true)
                tk.Activate();
            else
            {
                tk.MdiParent = this;
                tk.Show();
            }
        }

        private void btnLoaiMP_Click(object sender, EventArgs e)
        {
            Loaihang lmp = new Loaihang();
            if (KiemTraTonTai("Loaihang") == true)
                lmp.Activate();
            else
            {
                lmp.MdiParent = this;
                lmp.Show();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát không? ","Hỏi thoát",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(rt==DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
           
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thongtinnv ttnv = new Thongtinnv();
            if (KiemTraTonTai("Thongtinnv") == true)
                ttnv.Activate();
            else
            {
                ttnv.MdiParent = this;
                ttnv.Show();
            }
        }

        private void quảnLýLoạiMỹPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loaihang lh = new Loaihang();
            if (KiemTraTonTai("Loaihang") == true)
                lh.Activate();
            else
            {
                lh.MdiParent = this;
                lh.Show();
            }
        }

        private void quảnLýMỹPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyPham mp = new MyPham();
            if (KiemTraTonTai("MyPham") == true)
                mp.Activate();
            else
            {
                mp.MdiParent = this;
                mp.Show();
            }
        }

        
    }
}
