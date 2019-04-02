using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demobtl2
{
    public partial class Nhanvien : Form
    {
        public Nhanvien()
        {
            InitializeComponent();
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            ThemNV add = null;
            if (add == null || add.IsDisposed)
            {
                add = new ThemNV();
            }
          
            add.MdiParent = this;
            add.Show();
        }

        private void Nhanvien_Load(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Bạn có muốn thoát", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(rt == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }

        }
    }
}
