using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_MainFrame : Form
    {
        public Frm_MainFrame()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Width = 1200;
            Height = 650;
        }

        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            pic_Add.MouseEnter += Pic_Add_MouseEnter;
            pic_Add.MouseLeave += Pic_Add_MouseLeave;
            pic_Import.MouseEnter += Pic_Add_MouseEnter;
            pic_Import.MouseLeave += Pic_Add_MouseLeave;
            pic_Export.MouseEnter += Pic_Add_MouseEnter;
            pic_Export.MouseLeave += Pic_Add_MouseLeave;
            pic_Check.MouseEnter += Pic_Add_MouseEnter;
            pic_Check.MouseLeave += Pic_Add_MouseLeave;

            DataTable dataTable = DataSourceHelper.GetDataTable("firstpage");
            dgv_DataList.DataSource = dataTable;
            lbl_TotalAmount.Text = "共有 " + dataTable.Rows.Count + " 条数据";

            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 70),
                new KeyValuePair<int, int>(2, 150)
            };
            DataGridViewStyleHelper.SetWidth(dgv_DataList, list);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_DataList, new int[] { 0 });

            tv_DataTree.Nodes[0].ExpandAll();

        }

        private void Pic_Add_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.ForeColor = System.Drawing.Color.Black;
            Cursor = Cursors.Default;
        }

        private void Pic_Add_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.ForeColor = System.Drawing.Color.MidnightBlue;
            Cursor = Cursors.Hand;
        }

        private void btn_SH_Click(object sender, EventArgs e)
        {
            //收拢
            if (tv_DataTree.Width == 205)
            {
                tv_DataTree.Width = 5;
                btn_SH.Left = -5;
                dgv_DataList.Width += 200;
                dgv_DataList.Left -= 200;
                btn_SH.Text = ">";
            }else
            {
                tv_DataTree.Width = 205;
                btn_SH.Left = 188;
                dgv_DataList.Width -= 200;
                dgv_DataList.Left += 200;
                btn_SH.Text = "<";
            }
        }

        private void pic_Add_Click(object sender, EventArgs e)
        {
            Frm_Wroking frm_Wroking = new Frm_Wroking();
            frm_Wroking.Show();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_DataList.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("确认要删除当前选中行的数据吗?","确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgv_DataList.SelectedRows)
                    {
                        dgv_DataList.Rows.Remove(row);
                    }
                }
            }
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            string queryCode = "".Equals(txt_Query_Code.Text) ? null : txt_Query_Code.Text;
            string queryName = "".Equals(txt_Query_Name.Text) ? null : txt_Query_Name.Text;
            if (queryCode == null && queryName == null)
            {
                DataTable dataTable = DataSourceHelper.GetDataTable("firstpage");
                dgv_DataList.DataSource = dataTable;
                lbl_TotalAmount.Text = "共有 " + dataTable.Rows.Count + " 条数据";
            }
            else
            {
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                for(int i = 0; i < dgv_DataList.Rows.Count; i++) {
                    string code = dgv_DataList.Rows[i].Cells[2].Value.ToString();
                    string name = dgv_DataList.Rows[i].Cells[3].Value.ToString();
                    if (queryCode != null && queryName == null)
                    {
                        if (!code.Contains(queryCode))
                        {
                            rows.Add(dgv_DataList.Rows[i]);
                        }
                    }
                    else if (queryCode == null && queryName != null)
                    {
                        if (!name.Contains(queryName))
                        {
                            rows.Add(dgv_DataList.Rows[i]);
                        }
                    }
                    else if (queryName != null && queryCode != null)
                    {
                        if (!code.Contains(queryCode) || !name.Contains(queryName))
                        {
                            rows.Add(dgv_DataList.Rows[i]);
                        }
                    }
                }
                for (int i = 0; i < rows.Count; i++)
                {
                    dgv_DataList.Rows.Remove(rows[i]);
                }
            }
        }

        private void pic_Import_Click(object sender, EventArgs e)
        {
            Frm_Import frm_Import = new Frm_Import();
            frm_Import.ShowDialog();
        }

        private void Frm_MainFrame_Shown(object sender, EventArgs e)
        {
            Frm_Explain explain = new Frm_Explain();
            explain.ShowDialog();
        }

        private void Frm_MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
