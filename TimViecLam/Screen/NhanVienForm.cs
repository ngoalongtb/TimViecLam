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
using TimViecLam.EF;

namespace TimViecLam.Screen
{
    public partial class NhanVienForm : DevExpress.XtraEditors.XtraForm
    {
        public NhanVienForm()
        {
            InitializeComponent();
        }
        private void NhanVienForm_Load(object sender, EventArgs e)
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
            bds.DataSource = db.NhanViens.Select(x => new { x.MaNhanVien, x.TaiKhoan, x.HoTen, x.NgaySinh, x.DiaChi, x.HinhAnh, x.MatKhau, x.LoaiTaiKhoan, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaNhanVien"].HeaderText = "Mã NV";
            dtgv.Columns["TaiKhoan"].HeaderText = "Tài khoản";
            dtgv.Columns["HoTen"].HeaderText = "Họ tên";
            dtgv.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dtgv.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dtgv.Columns["HinhAnh"].HeaderText = "HinhAnh";
            dtgv.Columns["MatKhau"].HeaderText = "MatKhau";
            dtgv.Columns["LoaiTaiKhoan"].HeaderText = "Loại tài khoản";
        }
        public void LoadDataBinding()
        {
            txtTaiKhoan.DataBindings.Add("Text", dtgv.DataSource, "TaiKhoan", true, DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text", dtgv.DataSource, "HoTen", true, DataSourceUpdateMode.Never);
            dtpkNgaySinh.DataBindings.Add("Value", dtgv.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dtgv.DataSource, "DiaChi", true, DataSourceUpdateMode.Never);
            txtHinhAnh.DataBindings.Add("Text", dtgv.DataSource, "HinhAnh", true, DataSourceUpdateMode.Never);
            txtMatKhau.DataBindings.Add("Text", dtgv.DataSource, "MatKhau", true, DataSourceUpdateMode.Never);
            txtLoaiTaiKhoan.DataBindings.Add("Text", dtgv.DataSource, "x.LoaiTaiKhoan", true, DataSourceUpdateMode.Never);
        }

        public void LoadMore()
        {
            
        }

        public void HideColumn()
        {
            dtgv.Columns["MaNhanvien"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!MyRegular.CheckRequired(txtTen.Text, "Bắt buộc nhập vào tên danh mục"))
            //    return;
            //else
            NhanVien service = new NhanVien();
            service.TaiKhoan = txtTaiKhoan.Text;
            service.MatKhau = txtMatKhau.Text;
            service.HoTen = txtHoTen.Text;
            service.DiaChi = txtDiaChi.Text;
            service.LoaiTaiKhoan = int.Parse(txtLoaiTaiKhoan.Text);
            service.HinhAnh = txtHinhAnh.Text;
            service.NgaySinh = dtpkNgaySinh.Value;

            try
            {
                db.NhanViens.Add(service);
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
                NhanVien service = db.NhanViens.SingleOrDefault(x => x.TaiKhoan == txtTaiKhoan.Text);
                service.TaiKhoan = txtTaiKhoan.Text;
                service.MatKhau = txtMatKhau.Text;
                service.HoTen = txtHoTen.Text;
                service.DiaChi = txtDiaChi.Text;
                service.LoaiTaiKhoan = int.Parse(txtLoaiTaiKhoan.Text);
                service.HinhAnh = txtHinhAnh.Text;
                service.NgaySinh = dtpkNgaySinh.Value;

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
                    NhanVien service = db.NhanViens.SingleOrDefault(x=>x.TaiKhoan == txtTaiKhoan.Text);
                    db.NhanViens.Remove(service);
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
            bds.DataSource = db.NhanViens.Select(x => new { x.MaNhanVien, x.TaiKhoan, x.MatKhau, x.HoTen, x.NgaySinh, x.DiaChi, x.HinhAnh, x.LoaiTaiKhoan }).Where(x => x.MaNhanVien.ToString().Contains(txtTimKiem.Text) || x.TaiKhoan.Contains(txtTimKiem.Text)).ToList();
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
