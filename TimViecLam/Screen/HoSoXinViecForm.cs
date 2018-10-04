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
    public partial class HoSoXinViecForm : DevExpress.XtraEditors.XtraForm
    {
        public HoSoXinViecForm()
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
            bds.DataSource = db.HoSoXinViecs.Select(x => new { x.MaHoSoXinViec, x.NgayLap, x.MaNguoiTimViec, x.MaNhanVien, x.MaDanhSachCongViec, x.MoTa, x.TrangThai, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaHoSoXinViec"].HeaderText = "Mã hồ sơ xin việc";
            dtgv.Columns["NgayLap"].HeaderText = "Ngày lập";
            dtgv.Columns["MaNguoiTimViec"].HeaderText = "Mã NTV";
            dtgv.Columns["MaNhanVien"].HeaderText = "Mã NV";
            dtgv.Columns["MaDanhSachCongViec"].HeaderText = "Mã danh sách CV";
            dtgv.Columns["MoTa"].HeaderText = "Mô tả";
            dtgv.Columns["TrangThai"].HeaderText = "Trạng thái";

        }
        public void LoadDataBinding()
        {
            txtMaHoSoXinViec.DataBindings.Add("Text", dtgv.DataSource, "MaHoSoXinViec", true, DataSourceUpdateMode.Never);
            dtpkNgayLap.DataBindings.Add("Value", dtgv.DataSource, "NgayLap", true, DataSourceUpdateMode.Never);
            cbxMaNTV.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNguoiTimViec", true, DataSourceUpdateMode.Never);
            cbxMaNhanVien.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaNhanVien", true, DataSourceUpdateMode.Never);
            cbxMaDanhSachCV.DataBindings.Add("SelectedValue", dtgv.DataSource, "MaDanhSachCongViec", true, DataSourceUpdateMode.Never);
            txtMoTa.DataBindings.Add("Text", dtgv.DataSource, "MoTa", true, DataSourceUpdateMode.Never);
            cbxTrangThai.DataBindings.Add("SelectedValue", dtgv.DataSource, "TrangThai", true, DataSourceUpdateMode.Never);
        }

        public void LoadMore()
        {
            cbxMaNTV.DataSource = db.NguoiTimViecs.ToList();
            cbxMaNTV.DisplayMember = "HoTen";
            cbxMaNTV.ValueMember = "MaNguoiTimViec";

            cbxMaNhanVien.DataSource = db.NhanViens.ToList();
            cbxMaNhanVien.DisplayMember = "TaiKhoan";
            cbxMaNhanVien.ValueMember = "MaNhanVien";

            cbxMaDanhSachCV.DataSource = db.DanhSachCongViecs.ToList();
            cbxMaDanhSachCV.DisplayMember = "ViTriViecLam";
            cbxMaDanhSachCV.ValueMember = "MaDanhSachCongViec";
        }

        public void HideColumn()
        {
            dtgv.Columns["MaHoSoXinViec"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            HoSoXinViec service = new HoSoXinViec();
            service.NgayLap = DateTime.Now;
            service.MaNguoiTimViec = (int)cbxMaNTV.SelectedValue;
            service.MaNhanVien = (int)cbxMaNhanVien.SelectedValue;
            service.MaDanhSachCongViec = (int)cbxMaDanhSachCV.SelectedValue;
            service.MoTa = txtMoTa.Text;
            service.TrangThai = 0;

            try
            {
                db.HoSoXinViecs.Add(service);
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
                HoSoXinViec service = db.HoSoXinViecs.Find(int.Parse(txtMaHoSoXinViec.Text));
                service.NgayLap = DateTime.Now;
                service.MaNguoiTimViec = (int)cbxMaNTV.SelectedValue;
                service.MaNhanVien = (int)cbxMaNhanVien.SelectedValue;
                service.MaDanhSachCongViec = (int)cbxMaDanhSachCV.SelectedValue;
                service.MoTa = txtMoTa.Text;
                service.TrangThai = 0;


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
                    HoSoXinViec service = db.HoSoXinViecs.Find(int.Parse(txtMaHoSoXinViec.Text));
                    db.HoSoXinViecs.Remove(service);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công");
                    LoadDtgv();
                }
                catch (Exception)
                {
                    MessageBox.Show("Tồn tại Máy tính trong danh mục này");
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadDtgv();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            bds.DataSource = db.HoSoXinViecs.Select(x => new { x.MaHoSoXinViec, x.NgayLap, x.MaNhanVien, x.MaNguoiTimViec, x.MaDanhSachCongViec, x.MoTa, x.TrangThai,}).Where(x => x.MaHoSoXinViec.ToString().Contains(txtTimKiem.Text) || x.MoTa.Contains(txtTimKiem.Text)).ToList();
        }

    }

       
    }
