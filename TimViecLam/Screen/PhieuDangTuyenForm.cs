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
            dtgvDetail.DataSource = bds2;
            ChangHeader();
            LoadMore();
            HideColumn();
            LoadDataBinding();


        }

        private BindingSource bds = new BindingSource();
        private AppDB db = new AppDB();
        private OpenFileDialog open = new OpenFileDialog();

        //for dtgvDetail
        private BindingSource bds2 = new BindingSource();

        public void LoadDtgv()
        {
            bds.DataSource = db.PhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.NgayLap, x.MaNhaTuyenDung, x.MaNhanVien, x }).ToList();
            bds2.DataSource = db.CtPhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.TrinhDo, x.ThoiHan, x.SoLuongTuyenDung, x.NoiLamViec, x.MoTaYeuCau, x.LuongKhoiDiem, x}).Where(x => false).ToList();
        }

        public void LoadDtgvDetail()
        {
            txtMaPhieuDangTuyen_TextChanged(null, null);
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaPhieuDangTuyen"].HeaderText = "Mã hồ sơ xin việc";
            dtgv.Columns["NgayLap"].HeaderText = "Ngày lập";
            dtgv.Columns["MaNhaTuyenDung"].HeaderText = "Mã NTV";
            dtgv.Columns["MaNhanVien"].HeaderText = "Mã NV";

            dtgvDetail.Columns["TrinhDo"].HeaderText = "Trình độ";
            dtgvDetail.Columns["ThoiHan"].HeaderText = "Thời hạn";
            dtgvDetail.Columns["SoLuongTuyenDung"].HeaderText = "Số lượng tuyển dụng";
            dtgvDetail.Columns["NoiLamViec"].HeaderText = "Nơi làm việc";
            dtgvDetail.Columns["MoTaYeuCau"].HeaderText = "Mô tả yêu cầu";
            dtgvDetail.Columns["LuongKhoiDiem"].HeaderText = "Lương khởi điểm";

        }
        public void LoadDataBinding()
        {
            txtMaPhieuDangTuyen.DataBindings.Add("Text", dtgv.DataSource, "MaPhieuDangTuyen", true, DataSourceUpdateMode.Never);
            cbxMaNTD.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhaTuyenDung", true, DataSourceUpdateMode.Never);
            cbxMaNhanVien.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhanVien", true, DataSourceUpdateMode.Never);

            cbxMaDanhSachCongViec.DataBindings.Add("SelectedValue", dtgvDetail.DataSource, "x.MaDanhSachCongViec", true, DataSourceUpdateMode.Never);
            txtTrinhDo.DataBindings.Add("Text", dtgvDetail.DataSource, "TrinhDo", true, DataSourceUpdateMode.Never);
            dtpkThoiHan.DataBindings.Add("Value", dtgvDetail.DataSource, "ThoiHan", true, DataSourceUpdateMode.Never);

            txtSoLuongTuyenDung.DataBindings.Add("Text", dtgvDetail.DataSource, "SoLuongTuyenDung", true, DataSourceUpdateMode.Never);
            txtNoiLamViec.DataBindings.Add("Text", dtgvDetail.DataSource, "NoiLamViec", true, DataSourceUpdateMode.Never);
            txtMoTaYeuCau.DataBindings.Add("Text", dtgvDetail.DataSource, "MoTaYeuCau", true, DataSourceUpdateMode.Never);
            txtLuongKhoiDiem.DataBindings.Add("Text", dtgvDetail.DataSource, "LuongKhoiDiem", true, DataSourceUpdateMode.Never);

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
            dtgvDetail.Columns["MaPhieuDangTuyen"].Visible = false;
            dtgvDetail.Columns["x"].Visible = false;
            dtgv.Columns["x"].Visible = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            PhieuDangTuyen phieuDangTuyen = new PhieuDangTuyen();
            phieuDangTuyen.NgayLap = DateTime.Now;
            phieuDangTuyen.MaNhaTuyenDung = (int)cbxMaNTD.SelectedValue;
            phieuDangTuyen.MaNhanVien = (int)cbxMaNhanVien.SelectedValue;
            
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
            bds.DataSource = db.PhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.NgayLap, x.MaNhaTuyenDung, x.MaNhanVien, x }).Where(x => x.MaPhieuDangTuyen.ToString().Contains(txtTimKiem.Text)).ToList();
        }

        private void txtMaPhieuDangTuyen_TextChanged(object sender, EventArgs e)
        {
            int maPhieuDangTuyen = int.Parse(txtMaPhieuDangTuyen.Text);
            bds2.DataSource = db.CtPhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.TrinhDo, x.ThoiHan, x.SoLuongTuyenDung,
                x.NoiLamViec, x.MoTaYeuCau, x.LuongKhoiDiem, x }).Where(x => x.MaPhieuDangTuyen == maPhieuDangTuyen).ToList();
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            int maPhieuDangTuyen = int.Parse(txtMaPhieuDangTuyen.Text);
            CtPhieuDangTuyen ctPhieuDangTuyen = new CtPhieuDangTuyen();

            ctPhieuDangTuyen.MaPhieuDangTuyen = maPhieuDangTuyen;
            ctPhieuDangTuyen.MaDanhSachCongViec = (int)cbxMaDanhSachCongViec.SelectedValue;
            ctPhieuDangTuyen.MoTaYeuCau = txtMoTaYeuCau.Text;
            ctPhieuDangTuyen.NoiLamViec = txtNoiLamViec.Text;
            ctPhieuDangTuyen.ThoiHan = dtpkThoiHan.Value;
            ctPhieuDangTuyen.TrinhDo = txtTrinhDo.Text;
            ctPhieuDangTuyen.LuongKhoiDiem = int.Parse(txtLuongKhoiDiem.Text);
            ctPhieuDangTuyen.SoLuongTuyenDung = int.Parse(txtSoLuongTuyenDung.Text);


            try
            {
                db.CtPhieuDangTuyens.Add(ctPhieuDangTuyen);
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công");
                LoadDtgvDetail();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm mới không thành công. Vui lòng kiểm tra lại");
            }
        }

        private void btnCapNhatChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                int maPhieuDangTuyen = int.Parse(txtMaPhieuDangTuyen.Text);
                int maDanhSachCongViec = (int)cbxMaDanhSachCongViec.SelectedValue;
                CtPhieuDangTuyen ctPhieuDangTuyen = db.CtPhieuDangTuyens.SingleOrDefault(x => x.MaDanhSachCongViec == maDanhSachCongViec && x.MaPhieuDangTuyen == maPhieuDangTuyen);
                ctPhieuDangTuyen.MoTaYeuCau = txtMoTaYeuCau.Text;
                ctPhieuDangTuyen.NoiLamViec = txtNoiLamViec.Text;
                ctPhieuDangTuyen.ThoiHan = dtpkThoiHan.Value;
                ctPhieuDangTuyen.TrinhDo = txtTrinhDo.Text;
                ctPhieuDangTuyen.LuongKhoiDiem = int.Parse(txtLuongKhoiDiem.Text);
                ctPhieuDangTuyen.SoLuongTuyenDung = int.Parse(txtSoLuongTuyenDung.Text);


                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công");
                LoadDtgvDetail();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật không thành công. vui lòng kiểm tra lại");
            }
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa",
                                     "Xác nhận!!",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int maPhieuDangTuyen = int.Parse(txtMaPhieuDangTuyen.Text);
                    int maDanhSachCongViec = (int)cbxMaDanhSachCongViec.SelectedValue;
                    CtPhieuDangTuyen ctPhieuDangTuyen = db.CtPhieuDangTuyens.SingleOrDefault(x => x.MaDanhSachCongViec == maDanhSachCongViec && x.MaPhieuDangTuyen == maPhieuDangTuyen);
                    
                    db.CtPhieuDangTuyens.Remove(ctPhieuDangTuyen);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    LoadDtgvDetail();
                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
        }

        private void btnTaiLaiChiTiet_Click(object sender, EventArgs e)
        {
            LoadDtgvDetail();
        }
    }
}
