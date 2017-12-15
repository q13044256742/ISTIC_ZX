using System;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Wroking : Form
    {
        public Frm_Wroking()
        {
            InitializeComponent();
            FileListHelper.InitialFileList(dgv_JH_FileList);
        }

        private void Frm_Wroking_Load(object sender, EventArgs e)
        {
            LoadInitialDataTable();
        }

        /// <summary>
        /// 默认加载数据
        /// </summary>
        private void LoadInitialDataTable()
        {
            //文件列表
            dgv_JH_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FIilCheck.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_XM_FileCheck.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_JH_FileList, new int[] { 0, 4, 7, 8, 9, 14 });
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_XM_FileList, new int[] { 0, 4, 7, 8, 9, 14 });

            DataGridViewStyleHelper.SetLinkStyle(dgv_JH_FileList, new int[] { 14 });
            DataGridViewStyleHelper.SetLinkStyle(dgv_XM_FileList, new int[] { 14 });

            dgv_JH_FileList.Rows.Add("1", "规划阶段", "A01", "科技计划预算书", "崔文健", "财务", "公开", "10", "5", "2010-10-10", "重大办，专项办", "纸质", "PDF", "原件", "查看", "文件不齐全");
            dgv_JH_FileList.DataError += Dgv_JH_FileList_DataError;

            //文件核查
            dgv_JH_FIilCheck.Rows.Add("1", "008ZX02103", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A01", "项目验收承诺书", "文件损坏");
            dgv_JH_FIilCheck.Rows.Add("2", "008ZX02104", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A02", "项目任务合同书", "文件损坏");
            dgv_JH_FIilCheck.Rows.Add("3", "008ZX02105", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A03", "用户使用报告", "文件损坏");

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_JH_FIilCheck, new int[] { 0, 1, 3 });
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_XM_FileCheck, new int[] { 0, 1, 3 });

            //盒信息
            lsv_JH_Box.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            lsv_JH_Box.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            lsv_JH_Box.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            lsv_JH_Box.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            lsv_JH_Box.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            cbo_JH_BoxId.SelectedIndex = 0;

            //左侧树
            tv_Working.Nodes[0].ExpandAll();
        }

        private void Dgv_JH_FileList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }

        private void dgv_JH_FileList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 14)
                {
                    Cursor = Cursors.Hand;
                }
                else Cursor = Cursors.Default;
            }
            else Cursor = Cursors.Default;
        }

        private void dgv_JH_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                //查看上一次的意见
                if(e.ColumnIndex == 14)
                {
                    Frm_FIle_Opinion frm = new Frm_FIle_Opinion();
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 根据盒号显示不同馆藏号和列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_JH_BoxId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_JH_BoxId.SelectedIndex == 0)
            {
                lbl_GCH.Text = "A13268110";
            }
            else if (cbo_JH_BoxId.SelectedIndex == 1)
            {
                lbl_GCH.Text = "A13268101";
            }
            else if (cbo_JH_BoxId.SelectedIndex == 2)
            {
                lbl_GCH.Text = "A13268021";
            }
        }
    }
}
