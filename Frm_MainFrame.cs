using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;
using MSWord = Microsoft.Office.Interop.Word;

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
           

            DataTable dataTable = DataSourceHelper.GetDataTable("firstpage");
            dgv_DataList.DataSource = dataTable;
            lbl_TotalAmount.Text = "共有 " + dataTable.Rows.Count + " 条数据";
           
            //添加验证和合成
            //int a = dgv_DataList.Columns.Add("inspect","文件校验");
            int b = dgv_DataList.Columns.Add("synthesis", "操作");

            foreach (DataGridViewRow row in dgv_DataList.Rows)
            {
               // row.Cells[a].Value = "检验";
                row.Cells[b].Value = "查看";
            }

            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>
            {
                new KeyValuePair<int, int>(0, 70),
                new KeyValuePair<int, int>(1, 150),
                new KeyValuePair<int, int>(3, 200),
                new KeyValuePair<int, int>(5, 100)
               // new KeyValuePair<int, int>(6, 100)
            };
            DataGridViewStyleHelper.SetWidth(dgv_DataList, list);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_DataList, new int[] { 0 ,8});
            DataGridViewStyleHelper.SetLinkStyle(dgv_DataList, new int[] { 8});
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

        private void Btn_SH_Click(object sender, EventArgs e)
        {
            //收拢
            if (tv_DataTree.Width == 205)
            {
                tv_DataTree.Width = 5;
                btn_SH.Left = -1;
                dgv_DataList.Width += 200;
                dgv_DataList.Left -= 200;
                btn_SH.Text = ">";
            }else
            {
                tv_DataTree.Width = 205;
                btn_SH.Left = 197;
                dgv_DataList.Width -= 200;
                dgv_DataList.Left += 200;
                btn_SH.Text = "<";
            }
        }

        private void Pic_Add_Click(object sender, EventArgs e)
        {
            Frm_Wroking frm_Wroking = new Frm_Wroking();
            frm_Wroking.Show();
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
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

        private void Btn_Query_Click(object sender, EventArgs e)
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

        private void Pic_Import_Click(object sender, EventArgs e)
        {
            Frm_Import frm_Import = new Frm_Import();
            frm_Import.ShowDialog();
        }

        private void Frm_MainFrame_Shown(object sender, EventArgs e)
        {
            Frm_IdentityChoose frm_Identity = new Frm_IdentityChoose();
            frm_Identity.ShowDialog();
            Frm_Explain explain = new Frm_Explain();
            explain.ShowDialog();
        }

        private void Frm_MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Dgv_DataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //打开word
                if (e.ColumnIndex == 8 && "synthesis".Equals(dgv_DataList.Columns[e.ColumnIndex].Name))
                {
                    Object filename = "合成word示例.docx";
                    Object filefullname = Application.StartupPath + "\\temp\\" + filename;

                    string[] str = new string[] { dgv_DataList.Rows[e.RowIndex].Cells[1].Value.ToString() };
                    MicrosoftWordHelper.WriteDocument(filefullname.ToString(), str);
                }
              
            }
        }

        private void pic_Manager_Click(object sender, EventArgs e)
        {
            Frm_Manager manager = new Frm_Manager();
            manager.ShowDialog();
        }

        private void pic_Export_Click(object sender, EventArgs e)
        {
            Frm_Export frm_Export = new Frm_Export();
            frm_Export.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Frm_Backup frm_Backup = new Frm_Backup();
            frm_Backup.ShowDialog();
        }

        private void pic_Editpassword(object sender, EventArgs e)
        {
            Frm_EditPassword frm_EditPassword = new Frm_EditPassword();
            frm_EditPassword.ShowDialog();
        }

        private void pic_Update(object sender, EventArgs e)
        {
            Frm_Wroking frm_Wroking = new Frm_Wroking();
            frm_Wroking.Show();
        }
    }
}
