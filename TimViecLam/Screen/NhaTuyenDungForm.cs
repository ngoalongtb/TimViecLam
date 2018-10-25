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
    public partial class NhaTuyenDungForm : DevExpress.XtraEditors.XtraForm
    {
        public NhaTuyenDungForm()
        {
            InitializeComponent();
        }

        private void NhaTuyenDungForm_Load(object sender, EventArgs e)
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
            bds.DataSource = db.NhaTuyenDungs.Select(x => new { x.MaNhaTuyenDung, x.TenNhaTuyenDung, x.DiaChi, x.DienThoai, x.DiaChiWeb, x.GioiThieu, x.HinhAnh, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaNhaTuyenDung"].HeaderText = "Mã NTD";
            dtgv.Columns["TenNhaTuyenDung"].HeaderText = "Tên nhà tuyển dụng";
            dtgv.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dtgv.Columns["DienThoai"].HeaderText = "Điện thoại";
            dtgv.Columns["DiaChiWeb"].HeaderText = "Địa chỉ web";
            dtgv.Columns["GioiThieu"].HeaderText = "Giới thiệu";
            dtgv.Columns["HinhAnh"].HeaderText = "Hình ảnh";
           

        }
        public void LoadDataBinding()
        {
            txtMaNhaTuyenDung.DataBindings.Add("Text", dtgv.DataSource, "MaNhaTuyenDung", true, DataSourceUpdateMode.Never);
            txtTenNhaTuyenDung.DataBindings.Add("Text", dtgv.DataSource, "TenNhaTuyenDung", true, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dtgv.DataSource, "DiaChi", true, DataSourceUpdateMode.Never);
            txtDienThoai.DataBindings.Add("Text", dtgv.DataSource, "DienThoai", true, DataSourceUpdateMode.Never);
            txtDiaChiWeb.DataBindings.Add("Text", dtgv.DataSource, "DiaChiWeb", true, DataSourceUpdateMode.Never);
            txtGioiThieu.DataBindings.Add("Text", dtgv.DataSource, "GioiThieu", true, DataSourceUpdateMode.Never);
            txtHinhAnh.DataBindings.Add("Text", dtgv.DataSource, "HinhAnh", true, DataSourceUpdateMode.Never);
        
        }

        public void LoadMore()
        {

        }

        public void HideColumn()
        {
            dtgv.Columns["MaNhaTuyenDung"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!MyRegular.CheckRequired(txtTen.Text, "Bắt buộc nhập vào tên danh mục"))
            //    return;
            //else
            NhaTuyenDung service = new NhaTuyenDung();
            service.TenNhaTuyenDung = txtTenNhaTuyenDung.Text;
            service.DiaChi = txtDiaChi.Text;
            service.DienThoai = txtDienThoai.Text;
            service.DiaChiWeb = txtDiaChi.Text;
            service.GioiThieu = txtGioiThieu.Text;
            service.HinhAnh = txtHinhAnh.Text;
           
            try
            {
                db.NhaTuyenDungs.Add(service);
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
                NhaTuyenDung service = db.NhaTuyenDungs.Find(int.Parse(txtMaNhaTuyenDung.Text));
                service.TenNhaTuyenDung = txtTenNhaTuyenDung.Text;
                service.DiaChi = txtDiaChi.Text;
                service.DienThoai = txtDienThoai.Text;
                service.DiaChiWeb = txtDiaChiWeb.Text;
                service.GioiThieu = txtGioiThieu.Text;
                service.HinhAnh = txtHinhAnh.Text;

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
                    NhaTuyenDung service = db.NhaTuyenDungs.SingleOrDefault(x => x.TenNhaTuyenDung == txtTenNhaTuyenDung.Text);
                    db.NhaTuyenDungs.Remove(service);
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
            bds.DataSource = db.NhaTuyenDungs.Select(x => new { x.MaNhaTuyenDung, x.TenNhaTuyenDung, x.DiaChi, x.DienThoai, x.DiaChiWeb, x.GioiThieu, x.HinhAnh }).Where(x => x.MaNhaTuyenDung.ToString().Contains(txtTimKiem.Text) || x.TenNhaTuyenDung.Contains(txtTimKiem.Text)).ToList();
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
