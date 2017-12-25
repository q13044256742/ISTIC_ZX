using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Wroking : Form
    {
        /// <summary>
        /// 右侧选项卡合集
        /// </summary>
        List<TabPage> tabList = new List<TabPage>();

        public Frm_Wroking()
        {
            InitializeComponent();
        }

        private void Frm_Wroking_Load(object sender, EventArgs e)
        {
            LoadInitialDataTable();
            FileListHelper.InitialFileList(dgv_XM_FileList);
        }

        /// <summary>
        /// 默认加载数据
        /// </summary>
        private void LoadInitialDataTable()
        {
            //文件列表
            dgv_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_XM_FileCheck.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_KT_FileCheck.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_ZKT_FileCheck.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_XM_FileList, new int[] { 0, 4, 7, 8, 9 });

            dgv_XM_FileList.Rows.Add("1", "规划阶段", "A01", "科技计划预算书", "崔文健", "财务", "公开", "10", "5", "2010-10-10", "重大办，专项办", "纸质", "PDF", "原件", "查看", "文件不齐全");
            dgv_XM_FileList.DataError += Dgv_XM_FileList_DataError;
            dgv_XM_FileCheck.DataError += Dgv_XM_FileList_DataError;

            //文件核查
            dgv_XM_FileCheck.Rows.Add("1", "008ZX02103", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A01", "项目验收承诺书", "文件损坏");
            dgv_XM_FileCheck.Rows.Add("2", "008ZX02104", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A02", "项目任务合同书", "文件损坏");
            dgv_XM_FileCheck.Rows.Add("3", "008ZX02105", "90-65nm 等离子体增强化学气相沉积设备研发与应用", "A03", "用户使用报告", "文件损坏");

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_XM_FileCheck, new int[] { 0, 1, 3 });

            //盒信息
            lsv_XM_DGD.Items.Add(new ListViewItem(new string[] { "APTX-4869", "项目预算书" }));
            lsv_XM_DGD.Items.Add(new ListViewItem(new string[] { "APTX-4870", "项目预算书" }));
            lsv_XM_DGD.Items.Add(new ListViewItem(new string[] { "APTX-4871", "项目预算书" }));
            lsv_XM_DGD.Items.Add(new ListViewItem(new string[] { "APTX-4872", "项目预算书" }));
            lsv_XM_DGD.Items.Add(new ListViewItem(new string[] { "APTX-4873", "项目预算书" }));
            cbo_XM_BoxId.SelectedIndex = 0;
            FileBoxHelper.MoveFile(lsv_XM_DGD, lsv_XM_YGD, new Button[] { btn_XM_RightMove, btn_XM_RightMoveAll, btn_XM_LeftMove, btn_XM_LeftMoveAll });


            //左侧树
            tv_Working.Nodes[0].ExpandAll();

            //有无课题下拉框事件
            cbo_KT_HasNext.SelectedIndex = 1;
            cbo_XM_HasNext.SelectedIndex = 0;

            //右侧菜单，默认从项目开始
            foreach (TabPage item in tab_Menu.TabPages)
                tabList.Add(item);
            tab_Menu.TabPages.Clear();
            ShowTab(false, 0, "jh_xm");
        }

        /// <summary>
        /// 根据指定的索引和名称显示指定的选项卡
        /// </summary>
        /// <param name="index">要显示选项卡的位置</param>
        /// <param name="name">选项卡的名称</param>
        /// <param name="isClear">是否只清除</param>
        private void ShowTab( bool isClear,int index, string name)
        {
            if (isClear)
                while (tab_Menu.TabPages.Count > index+1)
                    tab_Menu.TabPages.RemoveAt(tab_Menu.TabPages.Count - 1);
            else
            {
                for (int i = 0; i < tabList.Count; i++)
                {
                    if (tabList[i].Name.Equals(name))
                    {
                        ShowTab(true, index -1, null);
                        tab_Menu.TabPages.Add(tabList[i]);
                        return;
                    }
                }
            }
        }

        private void Dgv_XM_FileList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            
        }

        private void Dgv_XM_FileList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
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

        private void Dgv_XM_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
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

        /// <summary>
        /// 有无课题事件
        /// </summary>
        private void Cbo_XM_HasNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_XM_HasNext.SelectedIndex > 0)
            {
                string xmCode = txt_XM_Code.Text;
                if (cbo_XM_HasNext.SelectedIndex == 1)//课题
                {
                    txt_XM_Name.Text = txt_XM_Code.Text;
                    ShowTab(false, 1, "jh_kt");
                }
                else if (cbo_XM_HasNext.SelectedIndex == 2)//子课题
                {
                    txt_KT_Name.Text = txt_XM_Code.Text;
                    ShowTab(false, 1, "jh_zkt");
                }
            }
            else
            {
                ShowTab(true, 0, null);
            }
        }

        /// <summary>
        /// 有无子课题事件
        /// </summary>
        private void cbo_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string ktCode = txt_KT_Code.Text;
            if (cbo_KT_HasNext.SelectedIndex == 0)
            {
                txt_KT_Name.Text = txt_KT_Code.Text;
                ShowTab(false, 2, "jh_zkt");
            }
            else
            {
                if (MessageBox.Show("此操作会清除子项下的数据，请确认是否继续？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    TreeNode[] treeNodes = tv_Working.Nodes.Find(ktCode, true);
                    if (treeNodes.Length > 0)
                        treeNodes[0].Nodes.Clear();
                    ShowTab(true, 1, null);
                }
                else
                    cbo_KT_HasNext.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 提交项目信息
        /// </summary>
        private void btn_XM_Submit_Click(object sender, EventArgs e)
        {
            string xmCode = txt_XM_Code.Text;
            string jhName = txt_JH_Name.Text;
            if (xmCode == null || "".Equals(xmCode))
                MessageBox.Show("请先完善课题信息！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                AddTreeNode(jhName, xmCode);
        }

        /// <summary>
        /// 添加节点到指定父节点下
        /// </summary>
        /// <param name="pName">父节点name属性值</param>
        /// <param name="name">待添加节点的name值</param>
        private void AddTreeNode(string pName, string name)
        {
            TreeNode[] treeNodes = tv_Working.Nodes.Find(name, true);
            if (treeNodes.Length > 0)
            {
                MessageBox.Show("信息编号已存在，请重新输入！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                treeNodes = tv_Working.Nodes.Find(pName, true);
                if (treeNodes.Length > 0)
                {
                    treeNodes[0].Nodes.Add(new TreeNode() { Name = name, Text = name });
                }
            }
            tv_Working.ExpandAll();
        }

        /// <summary>
        /// 项目保存操作
        /// </summary>
        private void btn_XM_Save_Click(object sender, EventArgs e)
        {
            btn_XM_Submit_Click(sender, e);
        }

        /// <summary>
        /// 课题提交操作
        /// </summary>
        private void btn_KT_Submit_Click(object sender, EventArgs e)
        {
            string ktCode = txt_KT_Code.Text;
            string xmName = txt_XM_Name.Text;
            if (ktCode == null || "".Equals(ktCode))
                MessageBox.Show("请先完善课题信息！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                AddTreeNode(xmName, ktCode);
        }

        /// <summary>
        /// 子课题提交
        /// </summary>
        private void btn_ZKT_Submit_Click(object sender, EventArgs e)
        {
            string zktCode = txt_ZKT_Code.Text;
            string ktName = txt_KT_Name.Text;
            if (zktCode == null || "".Equals(zktCode))
                MessageBox.Show("请先完善子课题信息！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                AddTreeNode(ktName, zktCode);
        }

        /// <summary>
        /// 打印案卷信息
        /// </summary>
        private void btn_XM_Print_Click(object sender, EventArgs e)
        {
            Frm_Print frm = new Frm_Print();
            frm.ShowDialog();
        }

        private void Frm_Wroking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("请确保所有数据均已保存！", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
