using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Manager : Form
    {
        public Frm_Manager()
        {
            InitializeComponent();
            
        }

        private void Frm_Manager_Load(object sender, System.EventArgs e)
        {
            
        }

        private void Btn_UserManager_Click(object sender, System.EventArgs e)
        {
            Frm_UserManager frm = new Frm_UserManager();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}                       
