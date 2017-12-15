using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_MyWork : Form
    {
        public Frm_MyWork()
        {
            InitializeComponent();
            Width = 900;
            Height = 400;
        }

        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            DataTable dataTable = DataSourceHelper.GetDataTable("wdjg");
            dgv_MyWork.DataSource = dataTable;
            dgv_MyWork.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_MyWork, new int[] { 0, 3 });
            DataGridViewStyleHelper.SetLinkStyle(dgv_MyWork, new int[] { 0, 4, 5 });
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            list.Add(new KeyValuePair<int, int>(1, 250));
            list.Add(new KeyValuePair<int, int>(2, 200));
            DataGridViewStyleHelper.SetWidth(dgv_MyWork, list);
        }

        private void dgv_MyWork_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //提交质检
                if(e.ColumnIndex == dgv_MyWork.Columns.Count - 1)
                {
                    if(MessageBox.Show("确认将当前选中数据提交至质检吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        dgv_MyWork.Rows.RemoveAt(e.RowIndex);
                    }
                }
                //编辑
                else if(e.ColumnIndex == dgv_MyWork.Columns.Count - 2)
                {
                    Frm_Wroking frm_Wroking = new Frm_Wroking();
                    frm_Wroking.ShowDialog();
                }
            }
        }
    }
}
