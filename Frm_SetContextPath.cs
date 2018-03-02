using System;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_SetContextPath : Form
    {
        public Frm_SetContextPath()
        {
            InitializeComponent();
        }

        private void btn_ChoosePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txt_ContextPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            MessageBox.Show("设置成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
