using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_WorkLog : Form
    {

        private int LastTableIndex = 0;
        public Frm_WorkLog()
        {
            InitializeComponent();
            Width = 1100;
            Height = 600;
        }

        private void Frm_WorkLog_Load(object sender, EventArgs e)
        {
            LoadDataGridView(0);
        }

        private void dgv_WorkLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4)
                {
                    if ("光盘数".Equals(dgv_WorkLog.Columns[e.ColumnIndex].HeaderText))
                    {
                        LoadDataGridView(1);
                    }else if ("总数".Equals(dgv_WorkLog.Columns[e.ColumnIndex].HeaderText))
                    {
                        LoadDataGridView(3);
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if ("总数".Equals(dgv_WorkLog.Columns[e.ColumnIndex].HeaderText))
                    {
                        LoadDataGridView(2);
                    }
                }
                if(e.ColumnIndex == dgv_WorkLog.Columns.Count - 1)
                {
                    if(MessageBox.Show("是否确认对当前选中数据进行加工", "确认提示", MessageBoxButtons.YesNo,  MessageBoxIcon.Asterisk)== DialogResult.Yes)
                    {
                        WindowState = FormWindowState.Minimized;
                        Frm_MyWork frm_MyWork = new Frm_MyWork();
                        frm_MyWork.MdiParent = Frm_Main.ActiveForm;
                        frm_MyWork.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 根据指定索引加载对应的表数据
        /// </summary>
        /// <param name="index">
        /// 0）jgdj
        /// 1）jgdj1
        /// 2）jgdj2
        /// 3) jgdj3
        /// </param>
        private void LoadDataGridView(int index)
        {
            if (index == 0)
            {
                DataTable dataTable = DataSourceHelper.GetDataTable("jgdj");
                dgv_WorkLog.DataSource = dataTable;
                dgv_WorkLog.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new int[] { 4, 5 });
                DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new int[] { 2, 3 });

                List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                list.Add(new KeyValuePair<int, int>(0, 300));
                list.Add(new KeyValuePair<int, int>(1, 200));
                DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

                LastTableIndex = 0;
            }
            else if (index == 1)
            {
                DataTable dataTable = DataSourceHelper.GetDataTable("jgdj1");
                dgv_WorkLog.DataSource = null;
                dgv_WorkLog.DataSource = dataTable;

                List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                list.Add(new KeyValuePair<int, int>(0, 300));
                DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

                DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new int[] { 1, 2, 4, 5 });
                DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new int[] { 3, 6 });

                LastTableIndex = 0;
            }
            else if (index == 2)
            {
                DataTable dataTable = DataSourceHelper.GetDataTable("jgdj2");
                dgv_WorkLog.DataSource = null;
                dgv_WorkLog.DataSource = dataTable;

                List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                list.Add(new KeyValuePair<int, int>(0, 300));
                list.Add(new KeyValuePair<int, int>(2, 250));
                DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

                DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new int[] { 1, 3, 5, 6, 7 });
                DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new int[] { 4, 7 });

                LastTableIndex = 1;
            }
            else if (index == 3)
            {
                DataTable dataTable = DataSourceHelper.GetDataTable("jgdj3");
                dgv_WorkLog.DataSource = null;
                dgv_WorkLog.DataSource = dataTable;

                List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                list.Add(new KeyValuePair<int, int>(0, 300));
                list.Add(new KeyValuePair<int, int>(2, 200));
                list.Add(new KeyValuePair<int, int>(3, 100));
                list.Add(new KeyValuePair<int, int>(4, 100));
                DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

                DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new int[] { 1, 3 });
                DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new int[] { 4 });

                LastTableIndex = 2;
            }
        }

        /// <summary>
        /// 返回按钮事件
        /// 根据索引返回上一个表单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, EventArgs e)
        {
            LoadDataGridView(LastTableIndex);
        }
    }
}
