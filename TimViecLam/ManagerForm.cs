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
                case ScreenName.NGUOI_TIM_VIEC:
                    form = new NguoiTimViecForm();
                    break;
                case ScreenName.PHIEU_DANG_TUYEN:
                    form = new PhieuDangTuyenForm();
                    break;
                case ScreenName.HO_SO_XIN_VIEC:
                    form = new HoSoXinViecForm();
                    break;
                case ScreenName.DANH_SACH_CONG_VIEC:
                    form = new DanhSachCongViecForm();
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

        private void btnNguoiTimViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.NGUOI_TIM_VIEC);
        }

        private void btnDanhSachCongViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.DANH_SACH_CONG_VIEC);
        }

        private void btnPhieuDangTuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.PHIEU_DANG_TUYEN);
        }

        private void btnHoSoXinViec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Trigger(ScreenName.HO_SO_XIN_VIEC);
        }
    }
}
