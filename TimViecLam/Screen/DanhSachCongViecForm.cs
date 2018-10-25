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
    public partial class DanhSachCongViecForm : DevExpress.XtraEditors.XtraForm
    {
        public DanhSachCongViecForm()
        {
            InitializeComponent();
        }
        private void DanhSachCongViecForm_Load(object sender, EventArgs e)
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
            bds.DataSource = db.DanhSachCongViecs.Select(x => new { x.MaDanhSachCongViec, x.ViTriViecLam, x }).ToList();
        }
        public void ChangHeader()
        {
            dtgv.Columns["MaDanhSachCongViec"].HeaderText = "Mã Danh sách công việc";
            dtgv.Columns["ViTriViecLam"].HeaderText = "Vị trí việc làm";



        }
        public void LoadDataBinding()
        {
            txtMaDanhSachCongViec.DataBindings.Add("Text", dtgv.DataSource, "MaDanhSachCongViec", true, DataSourceUpdateMode.Never);
            txtViTriViecLam.DataBindings.Add("Text", dtgv.DataSource, "ViTriViecLam", true, DataSourceUpdateMode.Never);


        }

        public void LoadMore()
        {

        }

        public void HideColumn()
        {
            dtgv.Columns["MaDanhSachCongViec"].Visible = false;
            dtgv.Columns["x"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!MyRegular.CheckRequired(txtTen.Text, "Bắt buộc nhập vào tên danh mục"))
            //    return;
            //else
            DanhSachCongViec service = new DanhSachCongViec();
            service.ViTriViecLam = txtViTriViecLam.Text;
            try
            {
                db.DanhSachCongViecs.Add(service);
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
                DanhSachCongViec service = db.DanhSachCongViecs.Find(int.Parse(txtMaDanhSachCongViec.Text));
                service.ViTriViecLam = txtViTriViecLam.Text;

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
                    DanhSachCongViec service = db.DanhSachCongViecs.Find(int.Parse(txtMaDanhSachCongViec.Text));
                    db.DanhSachCongViecs.Remove(service);
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
            bds.DataSource = db.DanhSachCongViecs.Select(x => new { x.MaDanhSachCongViec, x.ViTriViecLam }).Where(x => x.MaDanhSachCongViec.ToString().Contains(txtTimKiem.Text) || x.ViTriViecLam.Contains(txtTimKiem.Text)).ToList();
        }
    }
}
