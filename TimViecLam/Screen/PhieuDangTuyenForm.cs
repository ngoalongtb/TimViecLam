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
        private void HoSoXinViecForm_Load(object sender, EventArgs e)
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
            bds.DataSource = db.PhieuDangTuyens.Select(x => new { x.MaPhieuDangTuyen, x.NgayLap, x.MaNhaTuyenDung, x.MaNhanVien, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaPhieuDangTuyen"].HeaderText = "Mã hồ sơ xin việc";
            dtgv.Columns["NgayLap"].HeaderText = "Ngày lập";
            dtgv.Columns["MaNhaTuyenDung"].HeaderText = "Mã NTV";
            dtgv.Columns["MaNhanVien"].HeaderText = "Mã NV";
            

        }
        public void LoadDataBinding()
        {
            txtMaPhieuDangTuyen.DataBindings.Add("Text", dtgv.DataSource, "MaPhieuDangTuyen", true, DataSourceUpdateMode.Never);
            dtpkNgayLap.DataBindings.Add("Value", dtgv.DataSource, "NgayLap", true, DataSourceUpdateMode.Never);
            cbxMaNTD.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhaTuyenDung", true, DataSourceUpdateMode.Never);
            cbxMaNhanVien.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhanVien", true, DataSourceUpdateMode.Never);
           
        }

        public void LoadMore()
        {
            cbxMaNTD.DataSource = db.NhaTuyenDungs.ToList();
            cbxMaNTD.DisplayMember = "TenNhaTuyenDung";
            cbxMaNTD.ValueMember = "MaNhaTuyenDung";

            cbxMaNhanVien.DataSource = db.NhanViens.ToList();
            cbxMaNhanVien.DisplayMember = "TaiKhoan";
            cbxMaNhanVien.ValueMember = "MaNhanVien";

 
        }

        public void HideColumn()
        {
            dtgv.Columns["MaPhieuDangTuyen"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            PhieuDangTuyen service = new PhieuDangTuyen();
            service.NgayLap = DateTime.Now;
            service.MaNhaTuyenDung = (int)cbxMaNTD.SelectedValue;
            service.MaNhanVien = (int)cbxMaNhanVien.SelectedValue; 
        

            try
            {
                db.PhieuDangTuyens.Add(service);
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


        //private void btnsua_click(object sender, eventargs e)
        //{
        //    try
        //    {
        //        hosoxinviec service = db.hosoxinviecs.find(int.parse(txtmahosoxinviec.text));
        //        service.ngaylap = datetime.now;
        //        service.manguoitimviec = (int)cbxmantv.selectedvalue;
        //        service.manhanvien = (int)cbxmanhanvien.selectedvalue;
        //        service.madanhsachcongviec = (int)cbxmadanhsachcv.selectedvalue;
        //        service.mota = txtmota.text;
        //        service.trangthai = 0;


        //        db.savechanges();
        //        messagebox.show("cập nhật thành công");
        //        loaddtgv();
        //    }
        //    catch (exception)
        //    {
        //        messagebox.show("cập nhật không thành công. vui lòng kiểm tra lại");
        //    }
        //}


        //private void btnXoa_Click(object sender, EventArgs e)
        //{
        //    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa",
        //                             "Xác nhận!!",
        //                             MessageBoxButtons.YesNo);

        //    if (confirmResult == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            HoSoXinViec service = db.HoSoXinViecs.Find(int.Parse(txtMaHoSoXinViec.Text));
        //            db.HoSoXinViecs.Remove(service);
        //            db.SaveChanges();
        //            MessageBox.Show("Xóa thành công");
        //            LoadDtgv();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Tồn tại Máy tính trong danh mục này");
        //        }
        //    }
        //}

        //private void btnXem_Click(object sender, EventArgs e)
        //{
        //    LoadDtgv();
        //}

        //private void btnTimKiem_Click(object sender, EventArgs e)
        //{
        //    bds.DataSource = db.HoSoXinViecs.Select(x => new { x.MaHoSoXinViec, x.NgayLap, x.MaNhanVien, x.MaNguoiTimViec, x.MaDanhSachCongViec, x.MoTa, x.TrangThai, }).Where(x => x.MaHoSoXinViec.ToString().Contains(txtTimKiem.Text) || x.MoTa.Contains(txtTimKiem.Text)).ToList();
        //}

    }
}
