using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Backup : Form
    {
        public Frm_Backup()
        {
            InitializeComponent();
        }

        private void lbl_SetPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(fbd_Data.ShowDialog() == DialogResult.OK)
            {
                txt_FilePath.Text = fbd_Data.SelectedPath;
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {

        }
    }
}
