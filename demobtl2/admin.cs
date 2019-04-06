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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
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
        private void thêmThôngTinNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemNV nv = new ThemNV();
            if (CheckExistForm("ThemNV"))
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
            if (rt == DialogResult.Yes)
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
