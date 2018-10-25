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
    public partial class NguoiTimViecForm : DevExpress.XtraEditors.XtraForm
    {
        public NguoiTimViecForm()
        {
            InitializeComponent();
        }
        private void NguoiTimViecForm_Load(object sender, EventArgs e)
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
            bds.DataSource = db.NguoiTimViecs.Select(x => new { x.MaNguoiTimViec, x.HoTen, x.NgaySinh, x.GioiTinh, x.DiaChi, x.DienThoai, x.HinhAnh, x.TrinhDo, x.ChuyenNganh, x.BangCap, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaNguoiTimViec"].HeaderText = "Mã NTV";
            dtgv.Columns["HoTen"].HeaderText = "Họ tên";
            dtgv.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dtgv.Columns["GioiTinh"].HeaderText = "Giới tính";
            dtgv.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dtgv.Columns["DienThoai"].HeaderText = "Điện thoại";
            dtgv.Columns["HinhAnh"].HeaderText = "Hình ảnh";
            dtgv.Columns["TrinhDo"].HeaderText = "Trình độ";
            dtgv.Columns["ChuyenNganh"].HeaderText = "Chuyên ngành";
            dtgv.Columns["BangCap"].HeaderText = "Bằng cấp";
        }
        public void LoadDataBinding()
        {
            txtMaNguoiTimViec.DataBindings.Add("Text", dtgv.DataSource, "MaNguoiTimViec", true, DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text", dtgv.DataSource, "HoTen", true, DataSourceUpdateMode.Never);
            dtpkNgaySinh.DataBindings.Add("Value", dtgv.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never);
            txtGioiTinh.DataBindings.Add("Text", dtgv.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dtgv.DataSource, "DiaChi", true, DataSourceUpdateMode.Never);
            txtDienThoai.DataBindings.Add("Text", dtgv.DataSource, "DienThoai", true, DataSourceUpdateMode.Never);
            txtHinhAnh.DataBindings.Add("Text", dtgv.DataSource, "HinhAnh", true, DataSourceUpdateMode.Never);
            txtTrinhDo.DataBindings.Add("Text", dtgv.DataSource, "TrinhDo", true, DataSourceUpdateMode.Never);
            txtChuyenNganh.DataBindings.Add("Text", dtgv.DataSource, "ChuyenNganh", true, DataSourceUpdateMode.Never);
            txtBangCap.DataBindings.Add("Text", dtgv.DataSource, "BangCap", true, DataSourceUpdateMode.Never);
        }

        public void LoadMore()
        {

        }

        public void HideColumn()
        {
            dtgv.Columns["MaNguoiTimViec"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!MyRegular.CheckRequired(txtTen.Text, "Bắt buộc nhập vào tên danh mục"))
            //    return;
            //else
            NguoiTimViec service = new NguoiTimViec();
            service.HoTen = txtHoTen.Text;
            service.NgaySinh = dtpkNgaySinh.Value;
            service.GioiTinh = txtGioiTinh.Text;
            service.DiaChi = txtDiaChi.Text;
            service.DienThoai = txtDienThoai.Text;
            service.HinhAnh = txtHinhAnh.Text;
            service.TrinhDo = txtTrinhDo.Text;
            service.ChuyenNganh = txtChuyenNganh.Text;
            service.BangCap = txtBangCap.Text;

            try
            {
                db.NguoiTimViecs.Add(service);
                db.SaveChanges();
                //if (open.CheckFileExists)
                //{
                //    string directory = AppDomain.CurrentDomain.BaseDirectory;
                //    File.Copy(open.FileName, directory + service.MaDichVu + open.SafeFileName);
                //    service.HinhAnh = open.SafeFileName;
                //}
                db.SaveChanges();
                MessageBox.Show("Thêm mới thành công");
                LoadDtgv();
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm mới không thành công. Vui lòng kiểm tra lại");
            }
        }

 
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                NguoiTimViec service = db.NguoiTimViecs.Find(int.Parse(txtMaNguoiTimViec.Text));
                service.HoTen = txtHoTen.Text;
                service.NgaySinh = dtpkNgaySinh.Value;
                service.GioiTinh = txtGioiTinh.Text;
                service.DiaChi = txtDiaChi.Text;
                service.DienThoai = txtDienThoai.Text;
                service.HinhAnh = txtHinhAnh.Text;
                service.TrinhDo = txtTrinhDo.Text;
                service.ChuyenNganh = txtChuyenNganh.Text;
                service.BangCap = txtBangCap.Text;

                //if (open.CheckFileExists)
                //{
                //    string directory = AppDomain.CurrentDomain.BaseDirectory;
                //    File.Copy(open.FileName, directory + service.MaDichVu + open.SafeFileName);
                //    service.HinhAnh = open.SafeFileName;
                //}
                db.SaveChanges();
                MessageBox.Show("Cập nhật thành công");
                LoadDtgv();
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật không thành công. Vui lòng kiểm tra lại");
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
                    NguoiTimViec service = db.NguoiTimViecs.Find( int.Parse(txtMaNguoiTimViec.Text));
                    db.NguoiTimViecs.Remove(service);
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
            bds.DataSource = db.NguoiTimViecs.Select(x => new { x.MaNguoiTimViec, x.HoTen, x.NgaySinh, x.DiaChi, x.DienThoai, x.GioiTinh, x.HinhAnh, x.TrinhDo, x.ChuyenNganh, x.BangCap }).Where(x => x.MaNguoiTimViec.ToString().Contains(txtTimKiem.Text) || x.HoTen.Contains(txtTimKiem.Text)).ToList();
        }

     

        //private void btnUploadImage_Click(object sender, EventArgs e)
        //{
        //    open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
        //    if (open.ShowDialog() == DialogResult.OK)
        //    {
        //        txtHinhAnh.Text = open.SafeFileName;
        //    }
        //}
    }
}
