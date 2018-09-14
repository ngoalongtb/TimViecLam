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

namespace TimViecLam
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private AppDB db = new AppDB();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginQuery = db.NhanViens.Where(x => x.TaiKhoan == txtUsername.Text && x.MatKhau == txtPassword.Text);
            if (loginQuery.Count() > 0)
            {
                this.Hide();
                NhanVien loginAccount = loginQuery.First();
                Session.LoginAccount = loginAccount;
                ManagerForm f = new ManagerForm();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác. Vui lòng kiểm tra lại!!!");
            }
        }
    }
}
