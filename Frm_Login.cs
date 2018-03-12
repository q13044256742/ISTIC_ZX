using System;
using System.Data;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_Username.Text;
            string password = txt_Password.Text;
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                object uid = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT ui_id FROM user_info WHERE ui_username='{username}' AND ui_password='{password}'");
                if(!string.IsNullOrEmpty(GetValue(uid)))
                {
                    Frm_MainFrame frm = new Frm_MainFrame(uid);
                    frm.Show();
                    Hide();
                }
                else
                    MessageBox.Show("用户名或密码错误。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private string GetValue(object uid)
        {
            return uid == null ? string.Empty : uid.ToString();
        }
    }
}
