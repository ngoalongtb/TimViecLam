using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimViecLam
{
    public partial class ManagerForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
