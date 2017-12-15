using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版.Tools
{
    class FileListHelper
    {
        public static Dictionary<string, string> dictionary;
        /// <summary>
        /// 根据指定的文件类别自动识别文件类型
        /// </summary>
        /// <param name="type">阶段类别号</param>
        public static Dictionary<string, string> GetFileType()
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, string>();
                dictionary.Add("A01", "实施方案（含总概算和阶段概算）及相关材料");
                dictionary.Add("A02", "阶段实施计划（含分年度概算）");
                dictionary.Add("A03", "年度计划（含年度预算）");
                dictionary.Add("A04", "管理办法、制度");
                dictionary.Add("A05", "年度指南");
                dictionary.Add("B01", "申报书");
                dictionary.Add("B02", "评审专家综合意见、专家打分表、专家签到表等申报立项评审材料及视频资料");
                dictionary.Add("B03", "预算评审报告");
                dictionary.Add("B04", "预算申诉材料");
                dictionary.Add("B05", "立项批复（含预算）");
                dictionary.Add("B06", "保密协议");
                dictionary.Add("B07", "任务合同书（含预算书）");
                dictionary.Add("B08", "会议纪要和重要往来函件");
                dictionary.Add("C01", "实验任务书、实验大纲");
                dictionary.Add("C02", "实验、探测、测试、观测、观察、野外调查、考察等原始记录、整理记录和综合分析报告等");
                dictionary.Add("C03", "设计文件和图纸");
                dictionary.Add("C04", "计算文件、数据处理文件，照片、底片、录音带、录像带等声像文件");
                dictionary.Add("C05", "样品、标本等实物的目录");
                dictionary.Add("C06", "人员/项目变更申请、变更批复、变更审查会专家组意见、审查委员会专家名单等各类调整、变更材料");
                dictionary.Add("C07", "与其他单位的协作协议、合同等相关文件");
                dictionary.Add("C08", "三部门监督评估报告");
                dictionary.Add("C09", "年度/阶段执行情况报告、检查报告");
                dictionary.Add("C10", "专项阶段执行情况报告/专项阶段总结报告");
                dictionary.Add("C11", "专项年度监督自查报告");
                dictionary.Add("D01", "验收申请书、验收承诺书");
                dictionary.Add("D02", "验收通知");
                dictionary.Add("D03", "自评价报告及相关材料");
                dictionary.Add("D04", "科技报告");
                dictionary.Add("D05", "知识产权报告、专利及说明书（复印件）、软件著作权、技术标准、论文及研究报告、查新报告等知识产权及证明类");
                dictionary.Add("D06", "第三方检测/测试/评估报告");
                dictionary.Add("D07", "验收现场测试报告");
                dictionary.Add("D08", "用户使用报告及证明/典型用户报告、产业化审核报告等成果产业化证明类");
                dictionary.Add("D09", "专家打分表、专家（组）意见表、专家签到表、专家承诺书等验收评审类");
                dictionary.Add("D10", "验收结论书");
                dictionary.Add("D11", "任务验收报告/验收技术报告");
                dictionary.Add("D12", "整改验收会形成材料（复核通知、专家组意见、专家打分表）");
            }
            return dictionary;
        }
        /// <summary>
        /// 初始化文件列表控件
        /// </summary>
        /// <param name="dgv_JH_FileList"></param>
        public static void InitialFileList(DataGridView dgv_JH_FileList)
        {
            dgv_JH_FileList.EditingControlShowing += Dgv_JH_FileList_EditingControlShowing;
        }
        /// <summary>
        /// 下拉框联动事件
        /// </summary>
        private static void Dgv_JH_FileList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv_JH_FileList = sender as DataGridView;
            if (dgv_JH_FileList.CurrentCell.RowIndex != -1)
            {
                if (dgv_JH_FileList.CurrentCell.ColumnIndex == 1)
                {
                    ComboBox cb = e.Control as ComboBox;
                    cb.SelectedIndexChanged -= Cb_SelectedIndexChanged;
                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged;
                }
                else if (dgv_JH_FileList.CurrentCell.ColumnIndex == 2)
                {
                    ComboBox cb = e.Control as ComboBox;
                    cb.SelectedIndexChanged -= Cb_SelectedIndexChanged1;
                    cb.SelectedIndexChanged += Cb_SelectedIndexChanged1;
                }
            }
        }

        /// <summary>
        /// 根据文件类别自动读取文件名称
        /// </summary>
        private static void Cb_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            DataGridView dgv_JH_FileList = comboBox.Parent.Parent as DataGridView;
            string value = null;
            bool b = FileListHelper.GetFileType().TryGetValue(comboBox.SelectedItem.ToString(), out value);
            if (b) dgv_JH_FileList.CurrentRow.Cells[3].Value = value;
        }

        /// <summary>
        /// 根据阶段，自动刷新文件类别
        /// </summary>
        private static void Cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            DataGridView dgv_JH_FileList = comboBox.Parent.Parent as DataGridView;
            DataGridViewComboBoxCell comboBoxCell = dgv_JH_FileList.CurrentRow.Cells[2] as DataGridViewComboBoxCell;
            comboBoxCell.Items.Clear();
            if (comboBox.SelectedIndex == 0)
                comboBoxCell.Items.AddRange("A01", "A02", "A03", "A04", "A05");
            else if (comboBox.SelectedIndex == 1)
                comboBoxCell.Items.AddRange("B01", "B02", "B03", "B04", "B05", "B06", "B07", "B08");
            else if (comboBox.SelectedIndex == 2)
                comboBoxCell.Items.AddRange("C01", "C02", "C03", "C04", "C05", "C06", "C07", "C08", "C09", "C10", "C11");
            else if (comboBox.SelectedIndex == 3)
                comboBoxCell.Items.AddRange("D01", "D02", "D03", "D04", "D05", "D06", "D07", "D08", "D09", "D10", "D11");
        }
    }
}
