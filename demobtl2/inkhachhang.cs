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
    public partial class InKhachHang : Form
    {
        public InKhachHang()
        {
            InitializeComponent();
        }

        private void inkhachhang_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.Refresh();
        }
    }
}
