using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimViecLam.AppCode;
using TimViecLam.Screen;

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

        private void btnHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.HOME);
        }

        public void Trigger(string screen)
        {
            Form form = null;

            switch (screen)
            {
                case ScreenName.HOME:
                    form = new HomeForm();
                    break;
                case ScreenName.NHAN_VIEN:
                    form = new NhanVienForm();
                    break;
                case ScreenName.NHA_TUYEN_DUNG:
                    form = new NhaTuyenDungForm();
                    break;
                default:
                    break;
            }

            form.MdiParent = this;
            form.Show();

        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.NHAN_VIEN);
        }

        private void btnNhaTuyenDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.NHA_TUYEN_DUNG);
        }
    }
}
