using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
namespace demobtl2
{
    public partial class MenuNV : Form
    {
        public MenuNV()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            HoaDon hd = new HoaDon();
            hd.MdiParent = ParentForm;
            hd.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //nv.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //hd.Dock = DockStyle.Fill;
            hd.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Thongtinnv nv = new Thongtinnv();
            nv.MdiParent = ParentForm;
            nv.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //nv.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            nv.Dock = DockStyle.Fill;
            nv.Show();
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            thongketheothang tk = new thongketheothang();
            tk.MdiParent = ParentForm;
            //tk.StartPosition = FormStartPosition.CenterScreen;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //nv.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //tk.Dock = DockStyle.Fill;
            tk.Show();


        }

        private void MenuNV_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Loaihang lh = new Loaihang();
            lh.MdiParent = ParentForm;
            lh.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.MdiParent = ParentForm;
            kh.Show();
        }
    }
}
