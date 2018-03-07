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
        }

        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            
        }

        private void Pic_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.ForeColor = System.Drawing.Color.Black;
            Cursor = Cursors.Default;
        }

        private void Pic_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.ForeColor = System.Drawing.Color.MidnightBlue;
            Cursor = Cursors.Hand;
        }

        private void Pic_Add_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode() { Name = "ADMIN13044256742", Tag = ControlType.Plan };
            Frm_Wroking frm_Wroking = new Frm_Wroking(node);
            if(frm_Wroking.ShowDialog(this) == DialogResult.OK)
                LoadTreeList("ADMIN13044256742");
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
            //Frm_IdentityChoose frm_Identity = new Frm_IdentityChoose();
            //if(frm_Identity.ShowDialog() == DialogResult.OK)
            //{
            //    object obj = frm_Identity.Identity;
            //    tv_DataTree.Nodes[0].Nodes.Add(GetValue(obj));
            //    tv_DataTree.ExpandAll();
            //}

            //new Frm_Explain().ShowDialog();
            string SpecialId = "ADMIN13044256742";
            LoadTreeList(SpecialId);
            Tv_DataTree_AfterSelect(sender, new TreeViewEventArgs(tv_DataTree.Nodes[0].Nodes[0]));
        }
        
        /// <summary>
        /// 加载目录树
        /// </summary>
        /// <param name="specialId">专项ID</param>
        public void LoadTreeList(string specialId)
        {
            tv_DataTree.Nodes[0].Nodes.Clear();
            TreeNode rootNode = null;
            //【计划】
            DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_id, spi_code, spi_name FROM special_info WHERE spi_id='{specialId}'");
            if(row != null)
            {
                rootNode = new TreeNode()
                {
                    Name = GetValue(row["spi_id"]),
                    Text = GetValue(row["spi_code"]),
                    Tag = ControlType.Plan
                };
                //【项目】
                DataTable projectTable = SQLiteHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name FROM project_info WHERE pi_obj_id='{rootNode.Name}'");
                for(int i = 0; i < projectTable.Rows.Count; i++)
                {
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = GetValue(projectTable.Rows[i]["pi_id"]),
                        Text = GetValue(projectTable.Rows[i]["pi_code"]),
                        Tag = ControlType.Plan_Project
                    };
                    rootNode.Nodes.Add(treeNode);
                    //【课题】
                    DataTable table2 = SQLiteHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_name FROM topic_info WHERE ti_obj_id='{treeNode.Name}'");
                    for(int j = 0; j < table2.Rows.Count; j++)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(table2.Rows[j]["ti_id"]),
                            Text = GetValue(table2.Rows[j]["ti_code"]),
                            Tag = ControlType.Plan_Topic
                        };
                        treeNode.Nodes.Add(treeNode2);
                        //【子课题】
                        DataTable table3 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode2.Name}'");
                        for(int k = 0; k < table3.Rows.Count; k++)
                        {
                            treeNode2.Nodes.Add(new TreeNode()
                            {
                                Name = GetValue(table3.Rows[k]["si_id"]),
                                Text = GetValue(table3.Rows[k]["si_code"]),
                                Tag = ControlType.Plan_Topic_Subject
                            });
                        }
                    }
                    //【子课题】
                    DataTable table4 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode.Name}'");
                    for(int k = 0; k < table4.Rows.Count; k++)
                    {
                        treeNode.Nodes.Add(new TreeNode()
                        {
                            Name = GetValue(table4.Rows[k]["si_id"]),
                            Text = GetValue(table4.Rows[k]["si_code"]),
                            Tag = ControlType.Plan_Topic_Subject
                        });
                    }
                }
                //【课题】
                DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_name FROM topic_info WHERE ti_obj_id='{rootNode.Name}'");
                for(int j = 0; j < topicTable.Rows.Count; j++)
                {
                    TreeNode treeNode = new TreeNode()
                    {
                        Name = GetValue(topicTable.Rows[j]["ti_id"]),
                        Text = GetValue(topicTable.Rows[j]["ti_code"]),
                        Tag = ControlType.Plan_Topic
                    };
                    rootNode.Nodes.Add(treeNode);
                    //【子课题】
                    DataTable table3 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode.Name}'");
                    for(int k = 0; k < table3.Rows.Count; k++)
                    {
                        treeNode.Nodes.Add(new TreeNode()
                        {
                            Name = GetValue(table3.Rows[k]["si_id"]),
                            Text = GetValue(table3.Rows[k]["si_code"]),
                            Tag = ControlType.Plan_Topic_Subject
                        });
                    }
                }
                tv_DataTree.Nodes[0].Nodes.Add(rootNode);
            }
            tv_DataTree.ExpandAll();
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

        string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        private void Tv_DataTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.Tag != null)
            {
                Frm_Wroking frm = new Frm_Wroking(e.Node);
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            int index = dgv_DataList.SelectedRows.Count;
            if(index == 1)
            {
                new Frm_Wroking(new TreeNode()
                {
                    Name = GetValue(dgv_DataList.CurrentRow.Cells["id"].Tag),
                    Tag = dgv_DataList.CurrentRow.Tag
                }).ShowDialog();
            }
        }

        private void Tv_DataTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag != null)
            {
                dgv_DataList.Rows.Clear();
                object objId = e.Node.Name;
                ControlType type = (ControlType)e.Node.Tag;
                if(type == ControlType.Plan)
                {
                    DataTable projectTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < projectTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Project;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = projectTable.Rows[i]["pi_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = i + 1;
                        dgv_DataList.Rows[rid].Cells["code"].Value = projectTable.Rows[i]["pi_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = projectTable.Rows[i]["pi_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = projectTable.Rows[i]["pi_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = projectTable.Rows[i]["pi_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = projectTable.Rows[i]["pi_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["eles"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["control"].Value = "查看";
                    }
                    DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM topic_info WHERE ti_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < topicTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = topicTable.Rows[i]["ti_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = i + 1;
                        dgv_DataList.Rows[rid].Cells["code"].Value = topicTable.Rows[i]["ti_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = topicTable.Rows[i]["ti_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = topicTable.Rows[i]["ti_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = topicTable.Rows[i]["ti_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = topicTable.Rows[i]["ti_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["eles"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["control"].Value = "查看";
                    }
                }
                else if(type == ControlType.Plan_Project)
                {
                    DataTable subjectTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < subjectTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic_Subject;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = subjectTable.Rows[i]["si_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = i + 1;
                        dgv_DataList.Rows[rid].Cells["code"].Value = subjectTable.Rows[i]["si_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = subjectTable.Rows[i]["si_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = subjectTable.Rows[i]["si_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = subjectTable.Rows[i]["si_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = subjectTable.Rows[i]["si_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["eles"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["control"].Value = "查看";
                    }
                    DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM topic_info WHERE ti_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < topicTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = topicTable.Rows[i]["ti_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = i + 1;
                        dgv_DataList.Rows[rid].Cells["code"].Value = topicTable.Rows[i]["ti_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = topicTable.Rows[i]["ti_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = topicTable.Rows[i]["ti_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = topicTable.Rows[i]["ti_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = topicTable.Rows[i]["ti_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["eles"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["control"].Value = "查看";
                    }
                }
                else if(type == ControlType.Plan_Topic)
                {
                    DataTable subjectTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < subjectTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic_Subject;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = subjectTable.Rows[i]["si_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = i + 1;
                        dgv_DataList.Rows[rid].Cells["code"].Value = subjectTable.Rows[i]["si_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = subjectTable.Rows[i]["si_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = subjectTable.Rows[i]["si_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = subjectTable.Rows[i]["si_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = subjectTable.Rows[i]["si_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["eles"].Value = 0;
                        dgv_DataList.Rows[rid].Cells["control"].Value = "查看";
                    }
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadTreeList(tv_DataTree.Nodes[0].Nodes[0].Name);
        }
    }
}
