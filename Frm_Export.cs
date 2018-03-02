using System;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Export : Form
    {
        public Frm_Export()
        {
            InitializeComponent();
        }

        private void btn_Choose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_Export_Path.Text = dialog.SelectedPath;
            }
        }
    }
}
