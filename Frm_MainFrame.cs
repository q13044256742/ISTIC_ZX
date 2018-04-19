﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{

    public partial class Frm_MainFrame : Form
    {
        private string rootId;
        public Frm_MainFrame()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Pic_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.BackColor = System.Drawing.Color.FromName("control");
            Cursor = Cursors.Default;
        }

        private void Pic_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = (sender as PictureBox).Parent as Panel;
            panel.BackColor = System.Drawing.Color.Gainsboro;
            Cursor = Cursors.Hand;
        }

        private void Pic_Add_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode() { Name = rootId, Tag = ControlType.Plan };
            Frm_Wroking frm_Wroking = new Frm_Wroking(node, LoadTreeList);
            frm_Wroking.Show();
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_DataList.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("确认要删除当前选中行的数据吗?","确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    string ids = string.Empty;
                    foreach (DataGridViewRow row in dgv_DataList.SelectedRows)
                    {
                        object id = row.Cells["id"].Tag;
                        ids += $"'{id}',";
                    }
                    ids = ids.Substring(0, ids.Length - 1);

                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM project_info WHERE pi_id IN({ids});" +
                        $"DELETE FROM topic_info WHERE ti_id IN({ids});" +
                        $"DELETE FROM subject_info WHERE si_id IN({ids});");

                    MessageBox.Show("删除成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void Btn_Query_Click(object sender, EventArgs e)
        {
            int count = dgv_DataList.RowCount;
            string queryCode = txt_Query_Code.Text;
            string queryName = txt_Query_Name.Text;
            foreach(DataGridViewRow item in dgv_DataList.Rows)
                item.Visible = true;
            if(!string.IsNullOrEmpty(queryCode))
                foreach(DataGridViewRow item in dgv_DataList.Rows)
                    if(!GetValue(item.Cells["code"].Value).Contains(queryCode))
                    {
                        item.Visible = false;
                        count--;
                    }
            if(!string.IsNullOrEmpty(queryName))
                foreach(DataGridViewRow item in dgv_DataList.Rows)
                    if(!GetValue(item.Cells["name"].Value).Contains(queryName))
                    {
                        item.Visible = false;
                        count--;
                    }
            lbl_TotalAmount.Text = $"共有 {count} 条数据";
        }

        private void Pic_Import_Click(object sender, EventArgs e)
        {
            Frm_Import frm_Import = new Frm_Import();
            if(frm_Import.ShowDialog() == DialogResult.OK)
            {
                btn_Refresh_Click(sender, e);
            }
        }

        private void Frm_MainFrame_Shown(object sender, EventArgs e)
        {
            string querySql = $"SELECT spi_id, spi_name FROM special_info WHERE spi_id=(SELECT ui_special_id FROM user_info WHERE ui_id='{UserHelper.GetUser().UserId}')";
            object[] obj = SQLiteHelper.ExecuteRowsQuery(querySql);
            if(obj == null)
            {
                Frm_IdentityChoose frm_Identity = new Frm_IdentityChoose();
                if(frm_Identity.ShowDialog() == DialogResult.OK)
                    obj = SQLiteHelper.ExecuteRowsQuery(querySql);
            }
            if(obj != null)
            {
                rootId = GetValue(obj[0]);
                UserHelper.GetUser().UserSpecialId = rootId;
                UserHelper.GetUser().SpecialName = GetValue(obj[1]);
                LoadTreeList(rootId);
                Tv_DataTree_AfterSelect(sender, new TreeViewEventArgs(tv_DataTree.Nodes[0].Nodes[0]));

                new Frm_Explain().ShowDialog();
            }
        }
        
        /// <summary>
        /// 加载目录树
        /// </summary>
        /// <param name="specialId">专项ID</param>
        public void LoadTreeList(object specialId)
        {
            tv_DataTree.Nodes[0].Nodes.Clear();
            TreeNode rootNode = null;
            //【计划】
            DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_id, spi_code, spi_name FROM special_info WHERE spi_id='{specialId ?? rootId}'");
            if(row != null)
            {
                rootNode = new TreeNode()
                {
                    Name = GetValue(row["spi_id"]),
                    Text = GetValue(row["spi_name"]),
                    Tag = ControlType.Plan
                };
                //【项目】
                DataTable projectTable = SQLiteHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name FROM project_info WHERE pi_obj_id='{rootNode.Name}' GROUP BY pi_code");
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
                    DataTable table2 = SQLiteHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_name FROM topic_info WHERE ti_obj_id='{treeNode.Name}' GROUP BY ti_code");
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
                        DataTable table3 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode2.Name}' GROUP BY si_code");
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
                    DataTable table4 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode.Name}' GROUP BY si_code");
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
                DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_name FROM topic_info WHERE ti_obj_id='{rootNode.Name}' GROUP BY ti_code");
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
                    DataTable table3 = SQLiteHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{treeNode.Name}' GROUP BY si_code");
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
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {

            }
        }

        private void pic_Manager_Click(object sender, EventArgs e)
        {
            string specialId = UserHelper.GetUser().UserSpecialId;
            if(!string.IsNullOrEmpty(specialId))
            {
                Frm_Manager manager = new Frm_Manager(specialId);
                manager.ShowDialog();
            }
            else
                MessageBox.Show("当前用户尚未指定专项。");
        }

        private void pic_Export_Click(object sender, EventArgs e)
        {
            Frm_Export frm_Export = new Frm_Export();
            frm_Export.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定要备份当前系统数据吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                string destPath = Application.StartupPath + $"\\backupfile\\ISTIC{DateTime.Now.ToString(" yyyyMMdd-HHmm")}.db";
                if(!Directory.Exists(Path.GetDirectoryName(destPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                if(!File.Exists(destPath))
                    File.Create(destPath).Close();
                string sourFile = Application.StartupPath + @"\ISTIC.db";
                File.Copy(sourFile, destPath, true);
                MessageBox.Show("备份完毕。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
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
                Frm_Wroking frm = new Frm_Wroking(e.Node, LoadTreeList);
                frm.Show();
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
                }).Show();
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
                    int rowNumber = 1;
                    DataTable projectTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < projectTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Project;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = projectTable.Rows[i]["pi_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = rowNumber++;
                        dgv_DataList.Rows[rid].Cells["code"].Value = projectTable.Rows[i]["pi_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = projectTable.Rows[i]["pi_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = projectTable.Rows[i]["pi_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = projectTable.Rows[i]["pi_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = projectTable.Rows[i]["pi_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = GetFileAmount(projectTable.Rows[i]["pi_id"], true);
                        dgv_DataList.Rows[rid].Cells["eles"].Value = GetFileAmount(projectTable.Rows[i]["pi_id"], false);
                    }
                    DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM topic_info WHERE ti_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < topicTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = topicTable.Rows[i]["ti_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = rowNumber++;
                        dgv_DataList.Rows[rid].Cells["code"].Value = topicTable.Rows[i]["ti_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = topicTable.Rows[i]["ti_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = topicTable.Rows[i]["ti_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = topicTable.Rows[i]["ti_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = topicTable.Rows[i]["ti_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = GetFileAmount(topicTable.Rows[i]["ti_id"], true);
                        dgv_DataList.Rows[rid].Cells["eles"].Value = GetFileAmount(topicTable.Rows[i]["ti_id"], false);
                    }
                }
                else if(type == ControlType.Plan_Project)
                {
                    DataTable subjectTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_obj_id='{e.Node.Name}'");
                    int rowNumber = 1;
                    for(int i = 0; i < subjectTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic_Subject;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = subjectTable.Rows[i]["si_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = rowNumber++;
                        dgv_DataList.Rows[rid].Cells["code"].Value = subjectTable.Rows[i]["si_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = subjectTable.Rows[i]["si_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = subjectTable.Rows[i]["si_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = subjectTable.Rows[i]["si_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = subjectTable.Rows[i]["si_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = GetFileAmount(subjectTable.Rows[i]["si_id"], true);
                        dgv_DataList.Rows[rid].Cells["eles"].Value = GetFileAmount(subjectTable.Rows[i]["si_id"], false);
                    }
                    DataTable topicTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM topic_info WHERE ti_obj_id='{e.Node.Name}'");
                    for(int i = 0; i < topicTable.Rows.Count; i++)
                    {
                        int rid = dgv_DataList.Rows.Add();
                        dgv_DataList.Rows[rid].Tag = ControlType.Plan_Topic;
                        dgv_DataList.Rows[rid].Cells["id"].Tag = topicTable.Rows[i]["ti_id"];
                        dgv_DataList.Rows[rid].Cells["id"].Value = rowNumber++;
                        dgv_DataList.Rows[rid].Cells["code"].Value = topicTable.Rows[i]["ti_code"];
                        dgv_DataList.Rows[rid].Cells["name"].Value = topicTable.Rows[i]["ti_name"];
                        dgv_DataList.Rows[rid].Cells["unit"].Value = topicTable.Rows[i]["ti_unit"];
                        dgv_DataList.Rows[rid].Cells["user"].Value = topicTable.Rows[i]["ti_unit_user"];
                        dgv_DataList.Rows[rid].Cells["phone"].Value = topicTable.Rows[i]["ti_contacts_phone"];
                        dgv_DataList.Rows[rid].Cells["files"].Value = GetFileAmount(topicTable.Rows[i]["ti_id"], true);
                        dgv_DataList.Rows[rid].Cells["eles"].Value = GetFileAmount(topicTable.Rows[i]["ti_id"], false);
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
                        dgv_DataList.Rows[rid].Cells["files"].Value = GetFileAmount(subjectTable.Rows[i]["si_id"], true);
                        dgv_DataList.Rows[rid].Cells["eles"].Value = GetFileAmount(subjectTable.Rows[i]["si_id"], false);
                    }
                }
                lbl_TotalAmount.Text = $"共有 {dgv_DataList.RowCount} 条数据";
            }
        }

        /// <summary>
        /// 获取对象的文件数（纸本和电子）
        /// </summary>
        /// <param name="objid">对象主键</param>
        /// <param name="isPaper">是否纸本</param>
        /// <returns>文件数量</returns>
        private object GetFileAmount(object objid, bool isPaper)
        {
            string querySql = $"SELECT COUNT(*) FROM files_info fi LEFT JOIN data_dictionary dd ON fi.fi_carrier = dd.dd_id WHERE fi.fi_obj_id='{objid}' ";
            if(isPaper)
                querySql += "AND (dd_code = 'ZT_ZZ' OR dd_code = 'ZT_ALL')";
            else
                querySql += "AND (dd_code = 'ZT_DZ' OR dd_code = 'ZT_ALL')";
            return SQLiteHelper.ExecuteOnlyOneQuery(querySql); ;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadTreeList(tv_DataTree.Nodes[0].Nodes[0].Name);
        }

        private void pic_Query_Click(object sender, EventArgs e)
        {
            new Frm_Query(rootId).ShowDialog();
        }

        private void pic_Exit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定要注销当前登录用户吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Hide();
                new Frm_Login().Show();
            }
        }
    }
}
