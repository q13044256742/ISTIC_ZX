using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Import : Form
    {
        public Frm_Import()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
      
        /// <summary>
        /// 路径选择
        /// </summary>
        private void btn_Import_Click(object sender, EventArgs e)
        {
            if(fbd_Data.ShowDialog() == DialogResult.OK)
            {
                txt_FilePath.Text = fbd_Data.SelectedPath;
                pro_Show.Value = pro_Show.Minimum;
            }
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        private void btn_Import_Click_1(object sender, EventArgs e)
        {
            string sPath = txt_FilePath.Text;
            if(!string.IsNullOrEmpty(sPath))
            {
                count = 0;
                pro_Show.Value = pro_Show.Minimum;
                CopyFile(sPath);
                new Thread(delegate ()
                {
                    do
                    {
                        pro_Show.Value += 5;
                        Thread.Sleep(100);
                    }
                    while(pro_Show.Value < pro_Show.Maximum);
                    MessageBox.Show($"读取完毕,共计{count}个文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    Thread.CurrentThread.Abort();
                }).Start();
            }
        }
        private int count = 0;
        private void CopyFile(string sPath)
        {
            DirectoryInfo info = new DirectoryInfo(sPath);
            FileInfo[] file = info.GetFiles();
            count += file.Length;

            DirectoryInfo[] infos = info.GetDirectories();
            for(int i = 0; i < infos.Length; i++)
            {
                CopyFile(infos[i].FullName);
            }
        }
    }
}
