using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimViecLam.EF;

namespace TimViecLam.Screen
{
    public partial class PhieuDangTuyenForm : DevExpress.XtraEditors.XtraForm
    {
        public PhieuDangTuyenForm()
        {
            InitializeComponent();
        }
        private void PhieuDangTuyenForm_Load(object sender, EventArgs e)
        {

            LoadDtgv();
            dtgv.DataSource = bds;
            ChangHeader();
            LoadMore();
            HideColumn();
            LoadDataBinding();


        }

        private BindingSource bds = new BindingSource();
        private AppDB db = new AppDB();
        private OpenFileDialog open = new OpenFileDialog();

        public void LoadDtgv()
        {
            bds.DataSource = db.PhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.NgayLap, x.MaNhaTuyenDung, x.MaNhanVien, x.CtPhieuDangTuyen.MaDanhSachCongViec, x.CtPhieuDangTuyen.TrinhDo, x.CtPhieuDangTuyen.ThoiHan, x.CtPhieuDangTuyen.SoLuongTuyenDung, x.CtPhieuDangTuyen.NoiLamViec, x.CtPhieuDangTuyen.LuongKhoiDiem, x.CtPhieuDangTuyen.MoTaYeuCau, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaPhieuDangTuyen"].HeaderText = "Mã hồ sơ xin việc";
            dtgv.Columns["NgayLap"].HeaderText = "Ngày lập";
            dtgv.Columns["MaNhaTuyenDung"].HeaderText = "Mã NTV";
            dtgv.Columns["MaNhanVien"].HeaderText = "Mã NV";

            dtgv.Columns["TrinhDo"].HeaderText = "Trình độ";
            dtgv.Columns["ThoiHan"].HeaderText = "Thời hạn";
            dtgv.Columns["SoLuongTuyenDung"].HeaderText = "Số lượng tuyển dụng";
            dtgv.Columns["NoiLamViec"].HeaderText = "Nơi làm việc";
            dtgv.Columns["MoTaYeuCau"].HeaderText = "Mô tả yêu cầu";
            dtgv.Columns["LuongKhoiDiem"].HeaderText = "Lương khởi điểm";

        }
        public void LoadDataBinding()
        {
            txtMaPhieuDangTuyen.DataBindings.Add("Text", dtgv.DataSource, "MaPhieuDangTuyen", true, DataSourceUpdateMode.Never);
            dtpkNgayLap.DataBindings.Add("Value", dtgv.DataSource, "NgayLap", true, DataSourceUpdateMode.Never);
            cbxMaNTD.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhaTuyenDung", true, DataSourceUpdateMode.Never);
            cbxMaNhanVien.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhanVien", true, DataSourceUpdateMode.Never);

            cbxMaDanhSachCongViec.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaDanhSachCongViec", true, DataSourceUpdateMode.Never);
            txtTrinhDo.DataBindings.Add("Text", dtgv.DataSource, "TrinhDo", true, DataSourceUpdateMode.Never);
            dtpkThoiHan.DataBindings.Add("Value", dtgv.DataSource, "ThoiHan", true, DataSourceUpdateMode.Never);

            txtSoLuongTuyenDung.DataBindings.Add("Text", dtgv.DataSource, "SoLuongTuyenDung", true, DataSourceUpdateMode.Never);
            txtNoiLamViec.DataBindings.Add("Text", dtgv.DataSource, "NoiLamViec", true, DataSourceUpdateMode.Never);
            txtMoTaYeuCau.DataBindings.Add("Text", dtgv.DataSource, "MoTaYeuCau", true, DataSourceUpdateMode.Never);
            txtLuongKhoiDiem.DataBindings.Add("Text", dtgv.DataSource, "LuongKhoiDiem", true, DataSourceUpdateMode.Never);
            
        }

        public void LoadMore()
        {
            cbxMaNTD.DataSource = db.NhaTuyenDungs.ToList();
            cbxMaNTD.DisplayMember = "TenNhaTuyenDung";
            cbxMaNTD.ValueMember = "MaNhaTuyenDung";

            cbxMaNhanVien.DataSource = db.NhanViens.ToList();
            cbxMaNhanVien.DisplayMember = "TaiKhoan";
            cbxMaNhanVien.ValueMember = "MaNhanVien";

            cbxMaDanhSachCongViec.DataSource = db.DanhSachCongViecs.ToList();
            cbxMaDanhSachCongViec.DisplayMember = "ViTriViecLam";
            cbxMaDanhSachCongViec.ValueMember = "MaDanhSachCongViec";


        }

        public void HideColumn()
        {
            dtgv.Columns["MaPhieuDangTuyen"].Visible = false;
            dtgv.Columns["MaDanhSachCongViec"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            PhieuDangTuyen phieuDangTuyen = new PhieuDangTuyen();
            phieuDangTuyen.NgayLap = DateTime.Now;
            phieuDangTuyen.MaNhaTuyenDung = (int)cbxMaNTD.SelectedValue;
            phieuDangTuyen.MaNhanVien = (int)cbxMaNhanVien.SelectedValue;

            phieuDangTuyen.CtPhieuDangTuyen = new CtPhieuDangTuyen();
            phieuDangTuyen.CtPhieuDangTuyen.MaDanhSachCongViec = (int)cbxMaDanhSachCongViec.SelectedValue;
            phieuDangTuyen.CtPhieuDangTuyen.MoTaYeuCau = txtMoTaYeuCau.Text;
            phieuDangTuyen.CtPhieuDangTuyen.NoiLamViec = txtNoiLamViec.Text;
            phieuDangTuyen.CtPhieuDangTuyen.ThoiHan = dtpkThoiHan.Value;
            phieuDangTuyen.CtPhieuDangTuyen.TrinhDo = txtTrinhDo.Text;
            phieuDangTuyen.CtPhieuDangTuyen.LuongKhoiDiem = int.Parse(txtLuongKhoiDiem.Text);
            phieuDangTuyen.CtPhieuDangTuyen.SoLuongTuyenDung = int.Parse(txtSoLuongTuyenDung.Text);


            try
            {
                db.PhieuDangTuyens.Add(phieuDangTuyen);
                db.SaveChanges();
                
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công");
                LoadDtgv();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm mới không thành công. Vui lòng kiểm tra lại");
            }
        }


        private void btnsua_click(object sender, EventArgs e)
        {
            try
            {
                PhieuDangTuyen phieuDangTuyen = db.PhieuDangTuyens.Find(int.Parse(txtMaPhieuDangTuyen.Text));
                phieuDangTuyen.NgayLap = DateTime.Now;
                phieuDangTuyen.MaNhaTuyenDung = (int)cbxMaNTD.SelectedValue;
                phieuDangTuyen.MaNhanVien = (int)cbxMaNhanVien.SelectedValue;
                
                phieuDangTuyen.CtPhieuDangTuyen.MaDanhSachCongViec = (int)cbxMaDanhSachCongViec.SelectedValue;
                phieuDangTuyen.CtPhieuDangTuyen.MoTaYeuCau = txtMoTaYeuCau.Text;
                phieuDangTuyen.CtPhieuDangTuyen.NoiLamViec = txtNoiLamViec.Text;
                phieuDangTuyen.CtPhieuDangTuyen.ThoiHan = dtpkThoiHan.Value;
                phieuDangTuyen.CtPhieuDangTuyen.TrinhDo = txtTrinhDo.Text;
                phieuDangTuyen.CtPhieuDangTuyen.LuongKhoiDiem = int.Parse(txtLuongKhoiDiem.Text);
                phieuDangTuyen.CtPhieuDangTuyen.SoLuongTuyenDung = int.Parse(txtSoLuongTuyenDung.Text);


                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công");
                LoadDtgv();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật không thành công. vui lòng kiểm tra lại");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa",
                                     "Xác nhận!!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    PhieuDangTuyen phieuDangTuyen = db.PhieuDangTuyens.Find(int.Parse(txtMaPhieuDangTuyen.Text));
                    db.PhieuDangTuyens.Remove(phieuDangTuyen);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    LoadDtgv();
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadDtgv();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            bds.DataSource = db.PhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.NgayLap, x.MaNhaTuyenDung, x.MaNhanVien, x.CtPhieuDangTuyen.MaDanhSachCongViec, x.CtPhieuDangTuyen.TrinhDo, x.CtPhieuDangTuyen.ThoiHan, x.CtPhieuDangTuyen.SoLuongTuyenDung, x.CtPhieuDangTuyen.NoiLamViec, x.CtPhieuDangTuyen.LuongKhoiDiem, x.CtPhieuDangTuyen.MoTaYeuCau, x }).Where(x => x.MaPhieuDangTuyen.ToString().Contains(txtTimKiem.Text)).ToList();
        }

    }
}
