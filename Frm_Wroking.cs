﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Wroking : Form
    {
        /// <summary>
        /// 选项卡合集
        /// </summary>
        Dictionary<string, TabPage> tabPages;

        public Frm_Wroking(TreeNode treeNode)
        {
            InitializeComponent();
            InitialFrom(treeNode);
        }

        private void InitialFrom(TreeNode treeNode)
        {
            InitialBaseList();
            this.tabPages = new Dictionary<string, TabPage>();
            foreach(TabPage item in tab_Menu.TabPages)
                tabPages.Add(item.Name, item);
            tab_Menu.TabPages.Clear();
            LoadBasicInfo(treeNode);
        }

        /// <summary>
        /// 初始化下拉框数据源
        /// </summary>
        private void InitialBaseList()
        {
            //【阶段】
            InitialStageList(dgv_Special_FileList.Columns["dgv_Special_FL_stage"]);
            InitialStageList(dgv_Project_FileList.Columns["dgv_Project_FL_stage"]);
            InitialStageList(dgv_Topic_FileList.Columns["dgv_Topic_FL_stage"]);
            InitialStageList(dgv_Subject_FileList.Columns["dgv_Subject_FL_stage"]);
            //【文件类别】
            InitialCategorList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialCategorList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialCategorList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialCategorList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //【文件类型】
            InitialTypeList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialTypeList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialTypeList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialTypeList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //【密级】
            InitialSecretList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialSecretList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialSecretList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialSecretList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //【载体】
            InitialCarrierList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialCarrierList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialCarrierList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialCarrierList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //【文件格式】
            InitialFormatList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialFormatList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialFormatList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialFormatList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //文件形态
            InitialFormList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialFormList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialFormList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialFormList(dgv_Subject_FileList, "dgv_Subject_FL_");
            //文件核查原因列表
            InitialLostReasonList(dgv_Special_FileValid, "dgv_Special_FV_");
            InitialLostReasonList(dgv_Project_FileValid, "dgv_Project_FV_");
            InitialLostReasonList(dgv_Topic_FileValid, "dgv_Topic_FV_");
            InitialLostReasonList(dgv_Subject_FileValid, "dgv_Subject_FV_");

            //下拉框默认
            cbo_Special_HasNext.SelectedIndex = 0;
            cbo_Project_HasNext.SelectedIndex = 0;
            cbo_Topic_HasNext.SelectedIndex = 0;
        }

        private void LoadBasicInfo(TreeNode treeNode)
        {
            ControlType type = (ControlType)treeNode.Tag;
            if(type == ControlType.Plan)
            {
                ShowTabPageByName("special", 0);
                DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_code, spi_name, spi_unit, spi_intro FROM SPECIAL_INFO WHERE spi_id='{treeNode.Name}'");
                if(row != null)
                {
                    LoadBasicInfoInstince(type, treeNode.Name, row);
                }
            }
            else if(type == ControlType.Plan_Project)
            {
                DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{treeNode.Name}'");
                if(row != null)
                {
                    DataRow rootRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_id, spi_code, spi_name, spi_unit, spi_intro FROM SPECIAL_INFO WHERE spi_id='{row["pi_obj_id"]}'");
                    if(rootRow != null)
                    {
                        ShowTabPageByName("special", 0);
                        LoadBasicInfoInstince(ControlType.Plan, rootRow["spi_id"], rootRow);

                        ShowTabPageByName("project", 1);
                        gro_Project_Btns.Tag = 1;
                        project.Tag = row["pi_obj_id"];
                        LoadBasicInfoInstince(ControlType.Plan_Project, row["pi_id"], row);

                        tab_Menu.SelectedIndex = tab_Menu.TabCount - 1;
                    }
                }
            }
            else if(type == ControlType.Plan_Topic)
            {
                //当前节点
                DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM topic_info WHERE ti_id='{treeNode.Name}'");
                if(row != null)
                {
                    DataRow rootRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_id, spi_code, spi_name, spi_unit, spi_intro FROM special_info WHERE spi_id='{row["ti_obj_id"]}'");
                    if(rootRow != null)// 计划 >> 课题
                    {
                        ShowTabPageByName("special", 0);
                        LoadBasicInfoInstince(ControlType.Plan, rootRow["spi_id"], rootRow);

                        ShowTabPageByName("topic", 1);
                        gro_Topic_Btns.Tag = 1;
                        topic.Tag = row["ti_obj_id"];
                        LoadBasicInfoInstince(ControlType.Plan_Topic, row["ti_id"], row);
                    }
                    else
                    {
                        DataRow projectRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{row["ti_obj_id"]}'");
                        if(projectRow != null)// 项目 >> 课题
                        {
                            DataRow specialRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_id, spi_code, spi_name, spi_unit, spi_intro FROM special_info WHERE spi_id='{projectRow["pi_obj_id"]}'");
                            if(specialRow != null)
                            {
                                ShowTabPageByName("special", 0);
                                LoadBasicInfoInstince(ControlType.Plan, specialRow["spi_id"], specialRow);

                                ShowTabPageByName("project", 1);
                                gro_Project_Btns.Tag = 1;
                                project.Tag = projectRow["pi_obj_id"];
                                LoadBasicInfoInstince(ControlType.Plan_Project, projectRow["pi_id"], projectRow);

                                ShowTabPageByName("topic", 2);
                                gro_Topic_Btns.Tag = 2;
                                topic.Tag = row["ti_obj_id"];
                                LoadBasicInfoInstince(ControlType.Plan_Topic, row["ti_id"], row);

                            }
                        }
                    }
                    tab_Menu.SelectedIndex = tab_Menu.TabCount - 1;
                }
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                DataRow subjectRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM subject_info WHERE si_id='{treeNode.Name}'");
                if(subjectRow != null)
                {
                    DataRow topicRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM topic_info WHERE ti_id='{subjectRow["si_obj_id"]}'");
                    if(topicRow != null)//课题 >> 子课题
                    {
                        DataRow projectRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{topicRow["ti_obj_id"]}'");
                        //计划 >> 项目 >> 课题 >> 子课题
                        if(projectRow != null)
                        {
                            DataRow specialRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM special_info WHERE spi_id='{projectRow["pi_obj_id"]}'");

                            ShowTabPageByName("special", 0);
                            LoadBasicInfoInstince(ControlType.Plan, specialRow["spi_id"], specialRow);

                            ShowTabPageByName("project", 1);
                            gro_Project_Btns.Tag = 1;
                            project.Tag = projectRow["pi_obj_id"];
                            LoadBasicInfoInstince(ControlType.Plan_Project, projectRow["pi_id"], projectRow);

                            ShowTabPageByName("topic", 2);
                            gro_Topic_Btns.Tag = 2;
                            topic.Tag = topicRow["ti_obj_id"];
                            LoadBasicInfoInstince(ControlType.Plan_Topic, topicRow["ti_id"], topicRow);

                            ShowTabPageByName("Subject", 3);
                            LoadBasicInfoInstince(ControlType.Plan_Topic_Subject, subjectRow["si_id"], subjectRow);
                        }
                        else
                        {
                            DataRow specialRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM special_info WHERE spi_id='{topicRow["ti_obj_id"]}'");
                            //计划 >> 课题 >> 子课题
                            if(specialRow != null)
                            {
                                ShowTabPageByName("special", 0);
                                LoadBasicInfoInstince(ControlType.Plan, specialRow["spi_id"], specialRow);

                                ShowTabPageByName("topic", 1);
                                gro_Topic_Btns.Tag = 1;
                                topic.Tag = topicRow["ti_obj_id"];
                                LoadBasicInfoInstince(ControlType.Plan_Topic, topicRow["ti_id"], topicRow);

                                ShowTabPageByName("Subject", 2);
                                LoadBasicInfoInstince(ControlType.Plan_Topic_Subject, subjectRow["si_id"], subjectRow);

                            }
                        }
                    }
                    else
                    {
                        DataRow projectRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{subjectRow["si_obj_id"]}'");
                        //计划 >> 项目 >> 子课题
                        if(projectRow != null)
                        {
                            DataRow specialRow = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM special_info WHERE spi_id='{projectRow["pi_obj_id"]}'");
                            if(specialRow != null)
                            {
                                ShowTabPageByName("special", 0);
                                LoadBasicInfoInstince(ControlType.Plan, specialRow["spi_id"], specialRow);

                                ShowTabPageByName("project", 1);
                                gro_Project_Btns.Tag = 1;
                                project.Tag = projectRow["pi_obj_id"];
                                LoadBasicInfoInstince(ControlType.Plan_Project, projectRow["pi_id"], projectRow);

                                ShowTabPageByName("Subject", 2);
                                LoadBasicInfoInstince(ControlType.Plan_Topic_Subject, subjectRow["si_id"], subjectRow);
                            }
                        }
                    }
                    tab_Menu.SelectedIndex = tab_Menu.TabCount - 1;
                }
            }
            else
                tab_Menu.TabPages.Clear();
        }

        /// <summary>
        /// 基本信息下拉框
        /// </summary>
        private void InitialFieldList(object key, ComboBox comboBox)
        {
            DataTable table = SQLiteHelper.ExecuteQuery($"SELECT dd_id, dd_name FROM DATA_DICTIONARY WHERE dd_pId='{key}'");
            comboBox.DataSource = table;
            comboBox.DisplayMember = "dd_name";
            comboBox.ValueMember = "dd_id";
        }

        /// <summary>
        /// 加载基础信息和文件信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="objId">主键</param>
        private void LoadBasicInfoInstince(ControlType type, object objId, DataRow row)
        {
            if(type == ControlType.Plan)
            {
                txt_Special_Code.Text = GetValue(row["spi_code"]);
                txt_Special_Name.Text = GetValue(row["spi_name"]);
                txt_Special_Unit.Text = GetValue(row["spi_unit"]);
                txt_Special_Intro.Text = GetValue(row["spi_intro"]);

                tab_Special_Info.Tag = objId;
                LoadFileInfoById(dgv_Special_FileList, "dgv_Special_FL_", objId);
            }
            else if(type == ControlType.Plan_Project)
            {
                txt_Project_Code.Text = GetValue(row["pi_code"]);
                txt_Project_Code.Tag = Convert.ToInt32(row["pi_source_type"]);
                txt_Project_Name.Text = GetValue(row["pi_name"]);
                cbo_Project_Field.Text = GetValue(row["pi_field"]);
                txt_Project_Theme.Text = GetValue(row["pi_theme"]);
                txt_Project_Funds.Text = GetValue(row["pi_funds"]);
                dtp_Project_StartDate.Value = GetDateTimeValue(row["pi_startdate"]);
                dtp_Project_FinishDate.Value = GetDateTimeValue(row["pi_finishdate"]);
                txt_Project_Year.Text = GetValue(row["pi_year"]);
                cbo_Project_Unit.Text = GetValue(row["pi_unit"]);
                cbo_Project_Province.Text = GetValue(row["pi_province"]);
                txt_Project_Uniter.Text = GetValue(row["pi_unit_user"]);
                txt_Project_Proer.Text = GetValue(row["pi_project_user"]);
                txt_Project_Connecter.Text = GetValue(row["pi_contacts"]);
                txt_Project_ConPhone.Text = GetValue(row["pi_contacts_phone"]);
                txt_Project_Intro.Text = GetValue(row["pi_introduction"]);
                tab_Project_Info.Tag = objId;
                LoadFileInfoById(dgv_Project_FileList, "dgv_Project_FL_", objId);
            }
            else if(type == ControlType.Plan_Topic)
            {
                txt_Topic_Code.Text = GetValue(row["ti_code"]);
                txt_Topic_Code.Tag = Convert.ToInt32(row["ti_source_type"]);
                txt_Topic_Name.Text = GetValue(row["ti_name"]);
                cbo_Topic_Field.Text = GetValue(row["ti_field"]);
                txt_Topic_Theme.Text = GetValue(row["ti_theme"]);
                txt_Topic_Funds.Text = GetValue(row["ti_funds"]);
                dtp_Topic_StartDate.Value = GetDateTimeValue(row["ti_startdate"]);
                dtp_Topic_FinishDate.Value = GetDateTimeValue(row["ti_finishdate"]);
                txt_Topic_Year.Text = GetValue(row["ti_year"]);
                cbo_Topic_Unit.Text = GetValue(row["ti_unit"]);
                cbo_Topic_Province.Text = GetValue(row["ti_province"]);
                txt_Topic_Uniter.Text = GetValue(row["ti_unit_user"]);
                txt_Topic_Proer.Text = GetValue(row["ti_project_user"]);
                txt_Topic_Connecter.Text = GetValue(row["ti_contacts"]);
                txt_Topic_ConnertPhone.Text = GetValue(row["ti_contacts_phone"]);
                txt_Topic_Intro.Text = GetValue(row["ti_introduction"]);

                tab_Topic_Info.Tag = objId;
                LoadFileInfoById(dgv_Topic_FileList, "dgv_Topic_FL_", objId);
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                txt_Subject_Code.Text = GetValue(row["si_code"]);
                txt_Subject_Code.Tag = Convert.ToInt32(row["si_source_type"]);
                txt_Subject_Name.Text = GetValue(row["si_name"]);
                cbo_Subject_Field.Text = GetValue(row["si_field"]);
                txt_Subject_Theme.Text = GetValue(row["si_theme"]);
                txt_Subject_Funds.Text = GetValue(row["si_funds"]);
                dtp_Subject_StartDate.Value = GetDateTimeValue(row["si_startdate"]);
                dtp_Subject_FinishDate.Value = GetDateTimeValue(row["si_finishdate"]);
                txt_Subject_Year.Text = GetValue(row["si_year"]);
                cbo_Subject_Unit.Text = GetValue(row["si_unit"]);
                cbo_Subject_Province.Text = GetValue(row["si_province"]);
                txt_Subject_Uniter.Text = GetValue(row["si_unit_user"]);
                txt_Subject_Proer.Text = GetValue(row["si_project_user"]);
                txt_Subject_Connecter.Text = GetValue(row["si_contacts"]);
                txt_Subject_ConnectPhone.Text = GetValue(row["si_contacts_phone"]);
                txt_Subject_Intro.Text = GetValue(row["si_introduction"]);

                tab_Subject_Info.Tag = objId;
                LoadFileInfoById(dgv_Subject_FileList, "dgv_Subject_FL_", objId);
            }
            SetFileDetail(type, 0);
        }

        private void SetText(string msg) => Text = "我的加工" + msg;

        private void SetFileDetail(ControlType type, int rowIndex)
        {
            int count = 0;
            Label label = null;
            if(type == ControlType.Plan)
            {
                label = lbl_Special_FileDetail;
                count = dgv_Special_FileList.RowCount;
            }
            else if(type == ControlType.Plan_Project)
            {
                label = lbl_Project_FileDetail;
                count = dgv_Project_FileList.RowCount;
            }
            else if(type == ControlType.Plan_Topic)
            {
                label = lbl_Topic_FileDetail;
                count = dgv_Topic_FileList.RowCount;
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                label = lbl_Subject_FileDetail;
                count = dgv_Subject_FileList.RowCount;
            }
            label.Text = $"共计 {count - 1} 份文件，当前选中 {rowIndex + 1}";
        }

        /// <summary>
        /// 将对象转换成时间
        /// </summary>
        private DateTime GetDateTimeValue(object date)
        {
            DateTime _date = DateTime.Now;
            if(date != null)
                DateTime.TryParse(date.ToString(), out _date);
            return _date;
        }

        /// <summary>
        /// 根据指定ID加载文件相关信息
        /// </summary>
        /// <param name="pid">对象ID</param>
        /// <param name="key">关键字</param>
        private void LoadFileInfoById(DataGridView dataGridView, string key, object pid)
        {
            dataGridView.Rows.Clear();
            dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular);
            DataTable dataTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM files_info WHERE fi_obj_id='{pid}'");
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[index].Cells[key + "id"].Tag = dataTable.Rows[i]["fi_id"];
                dataGridView.Rows[index].Cells[key + "stage"].Value = dataTable.Rows[i]["fi_stage"];
                SetCategorByStage(dataTable.Rows[i]["fi_stage"], dataGridView.Rows[index], key);

                dataGridView.Rows[index].Cells[key + "categor"].Value = dataTable.Rows[i]["fi_categor"];
                dataGridView.Rows[index].Cells[key + "name"].Value = dataTable.Rows[i]["fi_name"];
                dataGridView.Rows[index].Cells[key + "user"].Value = dataTable.Rows[i]["fi_user"];
                dataGridView.Rows[index].Cells[key + "type"].Value = dataTable.Rows[i]["fi_type"];
                dataGridView.Rows[index].Cells[key + "secret"].Value = dataTable.Rows[i]["fi_secret"];
                dataGridView.Rows[index].Cells[key + "pages"].Value = dataTable.Rows[i]["fi_pages"];
                dataGridView.Rows[index].Cells[key + "number"].Value = dataTable.Rows[i]["fi_number"];
                object _date = dataTable.Rows[i]["fi_create_date"];
                if(_date != null)
                    dataGridView.Rows[index].Cells[key + "date"].Value = Convert.ToDateTime(_date).ToString("yyyyMMdd");
                dataGridView.Rows[index].Cells[key + "unit"].Value = dataTable.Rows[i]["fi_unit"];
                dataGridView.Rows[index].Cells[key + "carrier"].Value = dataTable.Rows[i]["fi_carrier"];
                dataGridView.Rows[index].Cells[key + "format"].Value = dataTable.Rows[i]["fi_format"];
                dataGridView.Rows[index].Cells[key + "form"].Value = dataTable.Rows[i]["fi_form"];
                dataGridView.Rows[index].Cells[key + "link"].Value = dataTable.Rows[i]["fi_link"];
            }
        }

        /// <summary>
        /// 将Object对象转换成String
        /// </summary>
        private string GetValue(object v) => v == null ? string.Empty : v.ToString();

        /// <summary>
        /// 将指定文本转换成指定日期格式
        /// </summary>
        /// <param name="date">待转换的日期对象</param>
        /// <param name="format">转换格式</param>
        private string GetDateValue(object date, string format)
        {
            string _formatDate = null, value = GetValue(date);
            if(!string.IsNullOrEmpty(value))
                _formatDate = Convert.ToDateTime(value).ToString(format);
            return _formatDate;
        }

        /// <summary>
        /// 根据指定的索引和名称显示指定的选项卡
        /// </summary>
        /// <param name="tabName">选项卡的名称</param>
        /// <param name="tabIndex">选项卡的索引</param>
        private void ShowTabPageByName(string tabName, int tabIndex)
        {
            //删除指定索引后的选项卡
            for(int i = 0; i < tab_Menu.TabCount; i++)
                if(i >= tabIndex)
                    tab_Menu.TabPages[i].Tag = true;
            foreach(TabPage item in tab_Menu.TabPages)
                if(item.Tag != null && bool.TryParse(item.Tag.ToString(), out bool temp))
                {
                    tab_Menu.TabPages.Remove(item);
                    item.Tag = null;
                }
            if(!string.IsNullOrEmpty(tabName) && tabPages.TryGetValue(tabName, out TabPage tabPage))
            {
                ClearText(tabPage, true);
                if(tabIndex > tab_Menu.TabCount)
                    tab_Menu.TabPages.Add(tabPage);
                else
                    tab_Menu.TabPages.Insert(tabIndex, tabPage);
            }
        }

        /// <summary>
        /// 清空输入板
        /// </summary>
        /// <param name="isClearBaseId">是否清空主键</param>
        private void ClearText(TabPage tabPage, bool isClearBaseId)
        {
            foreach(Control item in tabPage.Controls)
            {
                if(item is TextBox || item is ComboBox)
                    item.ResetText();
                else if(item is TabControl)
                {
                    if(isClearBaseId) item.Tag = null;
                    foreach(TabPage page in item.Controls)
                    {
                        foreach(Control tab in page.Controls)
                        {
                            if(tab is DataGridView)
                                (tab as DataGridView).Rows.Clear();
                            else if(tab is TextBox || item is ComboBox)
                                tab.ResetText();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ---
        /// </summary>
        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        /// <summary>
        /// 专项 >> 项目|课题
        /// </summary>
        private void Cbo_Special_HasNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbo_Special_HasNext.SelectedIndex;
            int sort = 0;
            if(index == 0 || index == 1)
                ShowTabPageByName(string.Empty, sort + 1);
            else
            {
                object id = tab_Special_Info.Tag;
                if(id != null)
                {
                    if(index == 2)//项目
                    {
                        ShowTabPageByName("project", sort + 1);
                        gro_Project_Btns.Tag = sort + 1;
                        project.Tag = id;
                    }
                    else if(index == 3)//课题
                    {
                        ShowTabPageByName("topic", sort + 1);
                        gro_Topic_Btns.Tag = sort + 1;
                        topic.Tag = id;
                    }
                }
                else
                {
                    MessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbo_Special_HasNext.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 有无子课题事件
        /// </summary>
        private void Cbo_Topic_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Topic_HasNext.SelectedIndex;
            int sort = Convert.ToInt32(gro_Topic_Btns.Tag);
            if(index == 0)
            {
                ShowTabPageByName(string.Empty, sort + 1);
            }
            else
            {
                object id = tab_Topic_Info.Tag;
                if(id != null)
                {
                    ShowTabPageByName("Subject", sort + 1);
                    gro_Subject_Btns.Tag = sort + 1;
                    Subject.Tag = id;
                }
                else
                {
                    MessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbo_Topic_HasNext.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 添加节点到指定父节点下
        /// </summary>
        /// <param name="pName">父节点name属性值</param>
        /// <param name="name">待添加节点的name值</param>
        private void AddTreeNode(string pName, string name)
        {
            TreeNode[] treeNodes = tv_Working.Nodes.Find(name, true);
            if(treeNodes.Length > 0)
            {
                MessageBox.Show("信息编号已存在，请重新输入！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                treeNodes = tv_Working.Nodes.Find(pName, true);
                if(treeNodes.Length > 0)
                {
                    treeNodes[0].Nodes.Add(new TreeNode() { Name = name, Text = name });
                }
            }
            tv_Working.ExpandAll();
        }

        /// <summary>
        /// 项目保存操作
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                int index = tab_Special_Info.SelectedIndex;
                string key = "dgv_Special_FL_";
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if(index == 0)
                    {
                        int maxLength = dgv_Special_FileList.Rows.Count;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Special_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Special_FileList.Rows[i];
                                object fileId = row.Cells[$"{key}id"].Tag;
                                if(fileId == null)
                                {
                                    fileId = AddFileInfo(key, row, objId);
                                    row.Cells[$"{key}id"].Value = row.Index + 1;
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                                else
                                    UpdateFileInfo(key, row);
                            }
                        }
                        if(maxLength > 1)
                            MessageBox.Show("文件保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 1)
                    {
                        ModifyFileValid(dgv_Special_FileValid, objId, "dgv_Special_FV_");
                        MessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {
                        string code = txt_Special_AJ_Code.Text;
                        if(!string.IsNullOrEmpty(code))
                        {
                            object aid = txt_Special_AJ_Code.Tag;
                            string _name = txt_Special_AJ_Name.Text;
                            string term = txt_Special_AJ_Term.Text;
                            string secret = txt_Special_AJ_Secret.Text;
                            string user = txt_Special_AJ_User.Text;
                            string unit = txt_Special_AJ_Unit.Text;
                            if(aid == null)
                            {
                                aid = Guid.NewGuid().ToString();
                                string insertSql = $"INSERT INTO files_tag_info(pt_id, pt_code, pt_name, pt_term, pt_secret, pt_user, pt_unit, pt_obj_id, pt_special_id) " +
                                $"VALUES ('{aid}','{code}','{_name}','{term}','{secret}','{user}','{unit}','{objId}', '{UserHelper.GetUser().UserSpecialId}')";
                                SQLiteHelper.ExecuteNonQuery(insertSql);
                                txt_Special_AJ_Code.Tag = aid;
                            }
                            else
                            {
                                string updateSql = $"UPDATE files_tag_info SET pt_code='{code}',pt_name='{_name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                                SQLiteHelper.ExecuteNonQuery(updateSql);
                            }
                            MessageBox.Show("案卷信息保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            MessageBox.Show("案卷编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 3)
                    {
                        object boxId = cbo_Special_BoxId.SelectedValue;
                        if(boxId != null)
                        {
                            if(!string.IsNullOrEmpty(txt_Special_GCID.Text.Trim()))
                            {
                                //先将当前盒中所有文件置为未归档状态
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=-1 WHERE fi_id IN(SELECT pb_obj_id FROM files_box_info WHERE pb_id='{boxId}')");

                                string ids = string.Empty;
                                foreach(ListViewItem item in lsv_Special_Right.Items)
                                    ids += "'" + item.SubItems[0].Text + "',";
                                if(!string.IsNullOrEmpty(ids))
                                {
                                    ids = ids.Substring(0, ids.Length - 1);
                                    SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=1 WHERE fi_id IN({ids})");
                                    SQLiteHelper.ExecuteNonQuery($"UPDATE files_box_info SET pb_files_id='{ids.Replace("'", string.Empty)}' WHERE pb_id='{boxId}'");

                                    MessageBox.Show("保存案卷盒成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    LoadFileBoxTable(boxId, objId, ControlType.Plan);
                                }
                            }
                            else
                                MessageBox.Show("馆藏号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            MessageBox.Show("请先添加案卷盒。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                    MessageBox.Show("未找到当前专项信息", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(name.Contains("Project"))
            {
                int index = tab_Project_Info.SelectedIndex;
                string key = "dgv_Project_FL_";
                object objId = tab_Project_Info.Tag;
                if(index == 0)
                {
                    if(!string.IsNullOrEmpty(txt_Project_Code.Text))
                    {
                        objId = tab_Project_Info.Tag = ModifyBasicInfo(ControlType.Plan_Project, objId, project.Tag);
                        MessageBox.Show("基本信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //保存文件信息
                        int maxLength = dgv_Project_FileList.Rows.Count;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Project_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Project_FileList.Rows[i];
                                object fileId = row.Cells[$"{key}id"].Tag;
                                if(fileId == null)
                                {
                                    fileId = AddFileInfo(key, row, objId);
                                    row.Cells[$"{key}id"].Value = row.Index + 1;
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                                else
                                    UpdateFileInfo(key, row);
                            }
                        }
                        if(maxLength > 1)
                            MessageBox.Show("文件保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("文件编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 1)
                {
                    ModifyFileValid(dgv_Project_FileValid, objId, "dgv_Project_FV_");
                    MessageBox.Show("文件核查信息保存成功。");
                }
                else if(index == 2)
                {
                    string code = txt_Project_AJ_Code.Text;
                    if(!string.IsNullOrEmpty(code))
                    {
                        object aid = txt_Project_AJ_Code.Tag;
                        string _name = txt_Project_AJ_Name.Text;
                        string term = txt_Project_AJ_Term.Text;
                        string secret = txt_Project_AJ_Secret.Text;
                        string user = txt_Project_AJ_User.Text;
                        string unit = txt_Project_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO files_tag_info(pt_id, pt_code, pt_name, pt_term, pt_secret, pt_user, pt_unit, pt_obj_id, pt_special_id, pt_source_type) " +
                                $"VALUES ('{aid}','{code}','{_name}','{term}','{secret}','{user}','{unit}','{objId}', '{UserHelper.GetUser().UserSpecialId}', 0)";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            txt_Project_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            if((int)txt_Project_AJ_Name.Tag == 1)//课题组历史数据
                            {
                                txt_Project_AJ_Code.Tag = null;
                                Btn_Save_Click(sender, e);
                                txt_Project_AJ_Name.Tag = 0;
                            }
                            else
                            {
                                string updateSql = $"UPDATE files_tag_info SET pt_code='{code}',pt_name='{_name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                                SQLiteHelper.ExecuteNonQuery(updateSql);
                            }
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                    else
                        MessageBox.Show("案卷编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 3)
                {
                    object boxId = cbo_Project_BoxId.SelectedValue;
                    if(boxId != null)
                    {
                        if(!string.IsNullOrEmpty(txt_Project_GCID.Text.Trim()))
                        {
                            //先将当前盒中所有文件置为未归档状态
                            SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=-1 WHERE fi_id IN(SELECT pb_obj_id FROM files_box_info WHERE pb_id='{boxId}')");

                            string ids = string.Empty;
                            foreach(ListViewItem item in lsv_Project_Right.Items)
                                ids += "'" + item.SubItems[0].Text + "',";
                            if(!string.IsNullOrEmpty(ids))
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=1 WHERE fi_id IN({ids})");
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_box_info SET pb_files_id='{ids.Replace("'", string.Empty)}' WHERE pb_id='{boxId}'");

                                MessageBox.Show("保存案卷盒成功。");
                                LoadFileBoxTable(boxId, objId, ControlType.Plan);
                            }
                        }
                        else
                            MessageBox.Show("馆藏号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("请先添加案卷盒。");
                }
            }
            else if(name.Contains("Topic"))
            {
                int index = tab_Topic_Info.SelectedIndex;
                string key = "dgv_Topic_FL_";
                object objId = tab_Topic_Info.Tag;
                if(index == 0)
                {
                    if(!string.IsNullOrEmpty(txt_Topic_Code.Text))
                    {
                        objId = tab_Topic_Info.Tag = ModifyBasicInfo(ControlType.Plan_Topic, objId, topic.Tag);
                        MessageBox.Show("基本信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //保存文件信息
                        int maxLength = dgv_Topic_FileList.Rows.Count;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Topic_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Topic_FileList.Rows[i];
                                object fileId = row.Cells[$"{key}id"].Tag;
                                if(fileId == null)
                                {
                                    fileId = AddFileInfo(key, row, objId);
                                    row.Cells[$"{key}id"].Value = row.Index + 1;
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                                else
                                    UpdateFileInfo(key, row);
                            }
                        }
                        if(maxLength > 1)
                            MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("文件保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 1)
                {
                    ModifyFileValid(dgv_Topic_FileValid, objId, "dgv_Topic_FV_");
                    MessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 2)
                {
                    string code = txt_Topic_AJ_Code.Text;
                    if(!string.IsNullOrEmpty(code))
                    {
                        object aid = txt_Topic_AJ_Code.Tag;
                        string _name = txt_Topic_AJ_Name.Text;
                        string term = txt_Topic_AJ_Term.Text;
                        string secret = txt_Topic_AJ_Secret.Text;
                        string user = txt_Topic_AJ_User.Text;
                        string unit = txt_Topic_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO files_tag_info(pt_id, pt_code, pt_name, pt_term, pt_secret, pt_user, pt_unit, pt_obj_id, pt_special_id, pt_source_type) " +
                                $"VALUES ('{aid}','{code}','{_name}','{term}','{secret}','{user}','{unit}','{objId}', '{UserHelper.GetUser().UserSpecialId}', 0)";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            txt_Topic_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            if((int)txt_Topic_AJ_Name.Tag == 1)//课题组历史数据
                            {
                                txt_Topic_AJ_Code.Tag = null;
                                Btn_Save_Click(sender, e);
                                txt_Topic_AJ_Name.Tag = 0;
                            }
                            else
                            {
                                string updateSql = $"UPDATE files_tag_info SET pt_code='{code}',pt_name='{_name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                                SQLiteHelper.ExecuteNonQuery(updateSql);
                            }
                        }
                        MessageBox.Show($"案卷信息保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("案卷编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 3)
                {
                    object boxId = cbo_Topic_BoxId.SelectedValue;
                    if(boxId != null)
                    {
                        if(!string.IsNullOrEmpty(txt_Topic_GCID.Text.Trim()))
                        {
                            //先将当前盒中所有文件置为未归档状态
                            SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=-1 WHERE fi_id IN(SELECT pb_obj_id FROM files_box_info WHERE pb_id='{boxId}')");

                            string ids = string.Empty;
                            foreach(ListViewItem item in lsv_Topic_Right.Items)
                                ids += "'" + item.SubItems[0].Text + "',";
                            if(!string.IsNullOrEmpty(ids))
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=1 WHERE fi_id IN({ids})");
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_box_info SET pb_files_id='{ids.Replace("'", string.Empty)}' WHERE pb_id='{boxId}'");

                                MessageBox.Show("保存案卷盒成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                LoadFileBoxTable(boxId, objId, ControlType.Plan);
                            }
                        }
                        else
                            MessageBox.Show("馆藏号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("请先添加案卷盒。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else if(name.Contains("Subject"))
            {
                int index = tab_Subject_Info.SelectedIndex;
                string key = "dgv_Subject_FL_";
                object objId = tab_Subject_Info.Tag;
                if(index == 0)
                {
                    if(!string.IsNullOrEmpty(txt_Subject_Code.Text))
                    {
                        objId = tab_Subject_Info.Tag = ModifyBasicInfo(ControlType.Plan_Topic_Subject, objId, Subject.Tag);
                        MessageBox.Show("基本信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //保存文件信息
                        int maxLength = dgv_Subject_FileList.Rows.Count;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Subject_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Subject_FileList.Rows[i];
                                object fileId = row.Cells[$"{key}id"].Tag;
                                if(fileId == null)
                                {
                                    fileId = AddFileInfo(key, row, objId);
                                    row.Cells[$"{key}id"].Value = row.Index + 1;
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                                else
                                    UpdateFileInfo(key, row);
                            }
                        }
                        if(maxLength > 1)
                            MessageBox.Show("文件保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 1)
                {
                    ModifyFileValid(dgv_Subject_FileValid, objId, "dgv_Subject_FV_");
                    MessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 2)
                {
                    string code = txt_Subject_AJ_Code.Text;
                    if(!string.IsNullOrEmpty(code))
                    {
                        object aid = txt_Subject_AJ_Code.Tag;
                        string _name = txt_Subject_AJ_Name.Text;
                        string term = txt_Subject_AJ_Term.Text;
                        string secret = txt_Subject_AJ_Secret.Text;
                        string user = txt_Subject_AJ_User.Text;
                        string unit = txt_Subject_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO files_tag_info(pt_id, pt_code, pt_name, pt_term, pt_secret, pt_user, pt_unit, pt_obj_id, pt_special_id) " +
                                $"VALUES ('{aid}','{code}','{_name}','{term}','{secret}','{user}','{unit}','{objId}', '{UserHelper.GetUser().UserSpecialId}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            txt_Subject_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            if((int)txt_Subject_AJ_Name.Tag == 1)//课题组历史数据
                            {
                                txt_Subject_AJ_Code.Tag = null;
                                Btn_Save_Click(sender, e);
                                txt_Subject_AJ_Name.Tag = 0;
                            }
                            else
                            {
                                string updateSql = $"UPDATE files_tag_info SET pt_code='{code}',pt_name='{_name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                                SQLiteHelper.ExecuteNonQuery(updateSql);
                            }
                        }
                        MessageBox.Show($"案卷信息保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("案卷编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(index == 3)
                {
                    object boxId = cbo_Subject_BoxId.SelectedValue;
                    if(boxId != null)
                    {
                        if(!string.IsNullOrEmpty(txt_Subject_GCID.Text.Trim()))
                        {
                            //先将当前盒中所有文件置为未归档状态
                            SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=-1 WHERE fi_id IN(SELECT pb_obj_id FROM files_box_info WHERE pb_id='{boxId}')");

                            string ids = string.Empty;
                            foreach(ListViewItem item in lsv_Subject_Right.Items)
                                ids += "'" + item.SubItems[0].Text + "',";
                            if(!string.IsNullOrEmpty(ids))
                            {
                                ids = ids.Substring(0, ids.Length - 1);
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_info SET fi_status=1 WHERE fi_id IN({ids})");
                                SQLiteHelper.ExecuteNonQuery($"UPDATE files_box_info SET pb_files_id='{ids.Replace("'", string.Empty)}' WHERE pb_id='{boxId}'");

                                MessageBox.Show("保存案卷盒成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                LoadFileBoxTable(boxId, objId, ControlType.Plan);
                            }
                        }
                        else
                            MessageBox.Show("馆藏号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("请先添加案卷盒。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 保存或修改基本信息
        /// </summary>
        /// <param name="type">基本信息类型</param>
        /// <param name="objId">主键</param>
        /// <param name="parentId">父级主键</param>
        private object ModifyBasicInfo(ControlType type, object objId, object parentId)
        {
            if(type == ControlType.Plan_Project)
            {
                object code = txt_Project_Code.Text;
                object name = txt_Project_Name.Text;
                object field = cbo_Project_Field.Text;
                object theme = txt_Project_Theme.Text;
                object funds = txt_Project_Funds.Text;
                object sdate = dtp_Project_StartDate.Value.ToString("s");
                object fdate = dtp_Project_FinishDate.Value.ToString("s");
                object year = txt_Project_Year.Text;
                object unit = cbo_Project_Unit.Text;
                object province = cbo_Project_Province.Text;
                object uniter = txt_Project_Uniter.Text;
                object proer = txt_Project_Proer.Text;
                object coner = txt_Project_Connecter.Text;
                object conphone = txt_Project_ConPhone.Text;
                object intro = txt_Project_Intro.Text;
                if(objId == null)
                {
                    objId = Guid.NewGuid().ToString();
                    string insertSql = "INSERT INTO project_info (pi_id, pi_code, pi_name, pi_field, pi_theme, pi_funds, pi_startdate, pi_finishdate, pi_year, pi_unit, pi_province, pi_unit_user, pi_project_user, pi_contacts, pi_contacts_phone, pi_introduction, pi_obj_id, pi_source_type) " +
                        $"VALUES('{objId}', '{code}', '{name}', '{field}', '{theme}', '{funds}', '{sdate}', '{fdate}', '{year}', '{unit}', '{province}', '{uniter}', '{proer}', '{coner}', '{conphone}', '{intro}', '{parentId}', 0)";
                    SQLiteHelper.ExecuteNonQuery(insertSql);
                }
                else
                {
                    if((int)txt_Project_Code.Tag == 1)//如果是课题组的，则为历史记录，执行新增操作
                    {
                        object souId = objId;
                        object newId = ModifyBasicInfo(type, null, parentId);
                        SQLiteHelper.ExecuteNonQuery(
                            $"UPDATE project_info SET pi_id='xxxxxxx' WHERE pi_id='{souId}';" +
                            $"UPDATE project_info SET pi_id='{souId}' WHERE pi_id='{newId}';" +
                            $"UPDATE project_info SET pi_id='{newId}' WHERE pi_id='xxxxxxx';");

                        txt_Project_Code.Tag = 0;
                    }
                    else
                    {
                        string updateSql = "UPDATE project_info SET " +
                            $"pi_code = '{code}', " +
                            $"pi_name = '{name}', " +
                            $"pi_field = '{field}', " +
                            $"pi_theme = '{theme}', " +
                            $"pi_funds = '{funds}', " +
                            $"pi_startdate = '{sdate}', " +
                            $"pi_finishdate = '{fdate}', " +
                            $"pi_year = '{year}', " +
                            $"pi_unit = '{unit}', " +
                            $"pi_province = '{province}', " +
                            $"pi_unit_user = '{uniter}', " +
                            $"pi_project_user = '{proer}', " +
                            $"pi_contacts = '{coner}', " +
                            $"pi_contacts_phone = '{conphone}', " +
                            $"pi_introduction = '{intro}' " +
                            $"WHERE pi_id='{objId}'";
                        SQLiteHelper.ExecuteNonQuery(updateSql);
                    }
                }
            }
            else if(type == ControlType.Plan_Topic)
            {
                object code = txt_Topic_Code.Text;
                object name = txt_Topic_Name.Text;
                object field = cbo_Topic_Field.Text;
                object theme = txt_Topic_Theme.Text;
                object funds = txt_Topic_Funds.Text;
                object sdate = dtp_Topic_StartDate.Value.ToString("s");
                object fdate = dtp_Topic_FinishDate.Value.ToString("s");
                object year = txt_Topic_Year.Text;
                object unit = cbo_Topic_Unit.Text;
                object province = cbo_Topic_Province.Text;
                object uniter = txt_Topic_Uniter.Text;
                object proer = txt_Topic_Proer.Text;
                object coner = txt_Topic_Connecter.Text;
                object conphone = txt_Topic_ConnertPhone.Text;
                object intro = txt_Topic_Intro.Text;
                if(objId == null)
                {
                    objId = Guid.NewGuid().ToString();
                    string insertSql = "INSERT INTO topic_info (ti_id, ti_code, ti_name, ti_field, ti_theme, ti_funds, ti_startdate, ti_finishdate, ti_year, ti_unit, ti_province, ti_unit_user, ti_project_user, ti_contacts, ti_contacts_phone, ti_introduction, ti_obj_id, ti_source_type) " +
                        $"VALUES('{objId}', '{code}', '{name}', '{field}', '{theme}', '{funds}', '{sdate}', '{fdate}', '{year}', '{unit}', '{province}', '{uniter}', '{proer}', '{coner}', '{conphone}', '{intro}', '{parentId}', 0)";
                    SQLiteHelper.ExecuteNonQuery(insertSql);
                }
                else
                {
                    if((int)txt_Topic_Code.Tag == 1)//如果是课题组的，则为历史记录，执行新增操作
                    {
                        object souId = objId;
                        object newId = ModifyBasicInfo(type, null, parentId);
                        SQLiteHelper.ExecuteNonQuery(
                            $"UPDATE topic_info SET ti_id='xxxxxxx' WHERE ti_id='{souId}';" +
                            $"UPDATE topic_info SET ti_id='{souId}' WHERE ti_id='{newId}';" +
                            $"UPDATE topic_info SET ti_id='{newId}' WHERE ti_id='xxxxxxx';");

                        txt_Topic_Code.Tag = 0;
                    }
                    else
                    {
                        string updateSql = "UPDATE topic_info SET " +
                        $"ti_code = '{code}', " +
                        $"ti_name = '{name}', " +
                        $"ti_field = '{field}', " +
                        $"ti_theme = '{theme}', " +
                        $"ti_funds = '{funds}', " +
                        $"ti_startdate = '{sdate}', " +
                        $"ti_finishdate = '{fdate}', " +
                        $"ti_year = '{year}', " +
                        $"ti_unit = '{unit}', " +
                        $"ti_province = '{province}', " +
                        $"ti_unit_user = '{uniter}', " +
                        $"ti_project_user = '{proer}', " +
                        $"ti_contacts = '{coner}', " +
                        $"ti_contacts_phone = '{conphone}', " +
                        $"ti_introduction = '{intro}' " +
                        $"WHERE ti_id='{objId}'";
                        SQLiteHelper.ExecuteNonQuery(updateSql);
                    }
                }
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                object code = txt_Subject_Code.Text;
                object name = txt_Subject_Name.Text;
                object field = cbo_Subject_Field.Text;
                object theme = txt_Subject_Theme.Text;
                object funds = txt_Subject_Funds.Text;
                object sdate = dtp_Subject_StartDate.Value.ToString("s");
                object fdate = dtp_Subject_FinishDate.Value.ToString("s");
                object year = txt_Subject_Year.Text;
                object unit = cbo_Subject_Unit.Text;
                object province = cbo_Subject_Province.Text;
                object uniter = txt_Subject_Uniter.Text;
                object proer = txt_Subject_Proer.Text;
                object coner = txt_Subject_Connecter.Text;
                object conphone = txt_Subject_ConnectPhone.Text;
                object intro = txt_Subject_Intro.Text;
                if(objId == null)
                {
                    objId = Guid.NewGuid().ToString();
                    string insertSql = "INSERT INTO subject_info (si_id, si_code, si_name, si_field, si_theme, si_funds, si_startdate, si_finishdate, si_year, si_unit, si_province, si_unit_user, si_project_user, si_contacts, si_contacts_phone, si_introduction, si_obj_id, si_source_type) " +
                        $"VALUES('{objId}', '{code}', '{name}', '{field}', '{theme}', '{funds}', '{sdate}', '{fdate}', '{year}', '{unit}', '{province}', '{uniter}', '{proer}', '{coner}', '{conphone}', '{intro}', '{parentId}', 0)";
                    SQLiteHelper.ExecuteNonQuery(insertSql);
                }
                else
                {
                    if((int)txt_Subject_Code.Tag == 1)//如果是课题组的，则为历史记录，执行新增操作
                    {
                        object souId = objId;
                        object newId = ModifyBasicInfo(type, null, parentId);
                        SQLiteHelper.ExecuteNonQuery(
                            $"UPDATE subject_info SET si_id='xxxxxxx' WHERE si_id='{souId}';" +
                            $"UPDATE subject_info SET si_id='{souId}' WHERE si_id='{newId}';" +
                            $"UPDATE subject_info SET si_id='{newId}' WHERE si_id='xxxxxxx';");
                        txt_Subject_Code.Tag = 0;
                    }
                    else
                    {
                        string updateSql = "UPDATE subject_info SET " +
                        $"si_code = '{code}', " +
                        $"si_name = '{name}', " +
                        $"si_field = '{field}', " +
                        $"si_theme = '{theme}', " +
                        $"si_funds = '{funds}', " +
                        $"si_startdate = '{sdate}', " +
                        $"si_finishdate = '{fdate}', " +
                        $"si_year = '{year}', " +
                        $"si_unit = '{unit}', " +
                        $"si_province = '{province}', " +
                        $"si_unit_user = '{uniter}', " +
                        $"si_project_user = '{proer}', " +
                        $"si_contacts = '{coner}', " +
                        $"si_contacts_phone = '{conphone}', " +
                        $"si_introduction = '{intro}' " +
                        $"WHERE si_id='{objId}'";
                        SQLiteHelper.ExecuteNonQuery(updateSql);
                    }
                }
            }
            return objId;
        }

        /// <summary>
        /// 打印案卷信息
        /// </summary>
        private void btn_XM_Print_Click(object sender, EventArgs e)
        {
            Frm_Print frm = new Frm_Print();
            frm.ShowDialog();
        }

        /// <summary>
        /// 根据阶段初始化文件类别
        /// </summary>
        /// <param name="dataGridView">表格</param>
        /// <param name="key">关键字</param>
        private void InitialCategorList(DataGridView dataGridView, string key)
        {
            for(int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dataGridView.Rows[i].Cells[key + "stage"];
                object stageId = satgeCell.Value;
                if(stageId != null)
                    SetCategorByStage(stageId, dataGridView.Rows[i], key);
            }
        }

        /// <summary>
        /// 初始化阶段下拉字段
        /// </summary>
        /// <param name="dataGridViewColumn">指定列</param>
        private void InitialStageList(DataGridViewColumn dataGridViewColumn)
        {
            DataGridViewComboBoxColumn comboBoxColumn = dataGridViewColumn as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
            comboBoxColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("微软雅黑", 10.5f) };
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, string key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SQLiteHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
            if(categorCell.Items.Count > 0)
                categorCell.Style.NullValue = categorCell.Items[0];
        }

        /// <summary>
        /// 单元格事件绑定
        /// </summary>
        private void Dgv_File_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if("dgv_Special_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Special_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan;
                if("dgv_Special_FL_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("dgv_Special_FL_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Project;
                if("dgv_Project_FL_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("dgv_Project_FL_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Topic;
                if("dgv_Topic_FL_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("dgv_Topic_FL_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Topic_Subject;
                if("dgv_Subject_FL_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("dgv_Subject_FL_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
        }

        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetCategorByStage(comboBox.SelectedValue, dgv_Special_FileList.CurrentRow, "dgv_Special_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_Project_FileList.CurrentRow, "dgv_Project_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_Topic_FileList.CurrentRow, "dgv_Topic_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subject)
                SetCategorByStage(comboBox.SelectedValue, dgv_Subject_FileList.CurrentRow, "dgv_Subject_FL_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                ComboBox _comboBox = obj as ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(StageComboBox_SelectionChangeCommitted);
            });
        }

        /// <summary>
        /// 文件类别 下拉事件
        /// </summary>
        private void CategorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetNameByCategor(comboBox.SelectedValue, dgv_Special_FileList.CurrentRow, "dgv_Special_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetNameByCategor(comboBox.SelectedValue, dgv_Project_FileList.CurrentRow, "dgv_Project_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetNameByCategor(comboBox.SelectedValue, dgv_Topic_FileList.CurrentRow, "dgv_Topic_FL_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subject)
                SetNameByCategor(comboBox.SelectedValue, dgv_Subject_FileList.CurrentRow, "dgv_Subject_FL_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                ComboBox _comboBox = obj as ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }

        /// <summary>
        /// 根据文件类别设置文件名称
        /// </summary>
        /// <param name="catogerCode">文件类别编号</param>
        /// <param name="currentRow">当前行</param>
        private void SetNameByCategor(object catogerId, DataGridViewRow currentRow, string key)
        {
            string value = GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{catogerId}'"));
            currentRow.Cells[key + "name"].Value = value;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        private void InitialTypeList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn filetypeColumn = dataGridView.Columns[key + "type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
            filetypeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        /// <summary>
        /// 密级
        /// </summary>
        private void InitialSecretList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn secretColumn = dataGridView.Columns[key + "secret"] as DataGridViewComboBoxColumn;
            secretColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn.DisplayMember = "dd_name";
            secretColumn.ValueMember = "dd_id";
            secretColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        /// <summary>
        /// 载体
        /// </summary>
        private void InitialCarrierList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn carrierColumn = dataGridView.Columns[key + "carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
            carrierColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        /// <summary>
        /// 形态
        /// </summary>
        private void InitialFormList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn formColumn = dataGridView.Columns[key + "form"] as DataGridViewComboBoxColumn;
            formColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            formColumn.DisplayMember = "dd_name";
            formColumn.ValueMember = "dd_id";
            formColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        /// <summary>
        /// 格式
        /// </summary>
        private void InitialFormatList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn formatColumn = dataGridView.Columns[key + "format"] as DataGridViewComboBoxColumn;
            formatColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn.DisplayMember = "dd_name";
            formatColumn.ValueMember = "dd_id";
            formatColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        /// <summary>
        /// 初始化文件核查原因
        /// </summary>
        private void InitialLostReasonList(DataGridView view, string key)
        {
            string code = "dic_file_lostreason";
            DataTable table = SQLiteHelper.ExecuteQuery($"SELECT * FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{code}') ORDER BY dd_sort");
            DataGridViewComboBoxColumn comboBoxColumn = view.Columns[key + "reason"] as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = table;
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
        }

        private void Frm_Wroking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("请确保所有数据均已保存！", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
                e.Cancel = true;
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Cbo_Project_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Project_HasNext.SelectedIndex;
            int sort = Convert.ToInt32(gro_Project_Btns.Tag);
            if(index == 0 || index == 1)
            {
                ShowTabPageByName(string.Empty, sort + 1);
            }
            else
            {
                object id = tab_Project_Info.Tag;
                if(id != null)
                {
                    if(index == 2)//课题
                    {
                        ShowTabPageByName("topic", sort + 1);
                        gro_Topic_Btns.Tag = sort + 1;
                        topic.Tag = id;

                    }
                    else if(index == 3)//子课题
                    {
                        ShowTabPageByName("Subject", sort + 1);
                        gro_Subject_Btns.Tag = sort + 1;
                        Subject.Tag = id;
                    }
                }
                else
                {
                    MessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    cbo_Project_HasNext.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        private void UpdateFileInfo(string key, DataGridViewRow row)
        {
            object primaryKey = row.Cells[key + "id"].Tag;
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object secret = row.Cells[key + "secret"].Value;
            object pages = row.Cells[key + "pages"].Value;
            object number = row.Cells[key + "number"].Value;
            DateTime date = DateTime.Now;
            string _date = GetValue(row.Cells[key + "date"].Value);
            if(!string.IsNullOrEmpty(_date))
            {
                if(_date.Length == 6)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-01";
                if(_date.Length == 8)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                DateTime.TryParse(_date, out date);
            }
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
            object format = row.Cells[key + "format"].Value;
            object form = row.Cells[key + "form"].Value;
            object link = row.Cells[key + "link"].Value;

            string updateSql = "UPDATE files_info SET " +
                $"fi_stage = '{stage}', " +
                $"fi_categor = '{categor}', " +
                $"fi_name = '{name}', " +
                $"fi_user = '{user}', " +
                $"fi_type = '{type}', " +
                $"fi_secret = '{secret}', " +
                $"fi_pages = '{pages}', " +
                $"fi_number = '{number}', " +
                $"fi_create_date = '{date.ToString("s")}', " +
                $"fi_unit = '{unit}', " +
                $"fi_carrier = '{carrier}', " +
                $"fi_format = '{format}', " +
                $"fi_form = '{form}', " +
                $"fi_link = '{link}' " +
                $"WHERE fi_id = '{primaryKey}'";
            SQLiteHelper.ExecuteNonQuery(updateSql);
        }

        /// <summary>
        /// 新增文件信息
        /// </summary>
        /// <param name="key">当前表格列名前缀</param>
        /// <param name="row">当前待保存的行</param>
        /// <param name="parentId">父对象ID</param>
        /// <returns>新增信息主键</returns>
        private object AddFileInfo(string key, DataGridViewRow row, object parentId)
        {
            object primaryKey = Guid.NewGuid().ToString();
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object secret = row.Cells[key + "secret"].Value;
            object pages = row.Cells[key + "pages"].Value;
            object number = row.Cells[key + "number"].Value;
            DateTime date = DateTime.Now;
            string _date = GetValue(row.Cells[key + "date"].Value);
            if(!string.IsNullOrEmpty(_date))
            {
                if(_date.Length == 6)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-01";
                if(_date.Length == 8)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                DateTime.TryParse(_date, out date);
            }
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
            object format = row.Cells[key + "format"].Value;
            object form = row.Cells[key + "form"].Value;
            object link = row.Cells[key + "link"].Value;

            string insertSql = "INSERT INTO files_info (" +
            "fi_id, fi_code, fi_stage, fi_categor, fi_name, fi_user, fi_type, fi_secret, fi_pages, fi_number, fi_create_date, fi_unit, fi_carrier, fi_format, fi_form, fi_link, fi_obj_id) " +
            $"VALUES( '{primaryKey}', '', '{stage}', '{categor}', '{name}', '{user}', '{type}', '{secret}', '{pages}', '{number}', '{date.ToString("s")}', '{unit}', '{carrier}', '{format}', '{form}', '{link}','{parentId}')";
            SQLiteHelper.ExecuteNonQuery(insertSql);
            return primaryKey;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        private void Btn_AddFile_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                if(dgv_Special_FileList.SelectedRows.Count == 1)
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Special_FileList, "dgv_Special_FL_", dgv_Special_FileList.CurrentRow.Cells[0].Tag);
                    frm.ShowDialog();
                }
                else
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Special_FileList, "dgv_Special_FL_", null);
                    frm.parentId = tab_Special_Info.Tag;
                    frm.ShowDialog();
                }
            }
            else if(name.Contains("Project"))
            {
                if(dgv_Project_FileList.SelectedRows.Count == 1)
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Project_FileList, "dgv_Project_FL_", dgv_Project_FileList.CurrentRow.Cells[0].Tag);
                    frm.ShowDialog();
                }
                else
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Project_FileList, "dgv_Project_FL_", null);
                    frm.parentId = tab_Project_Info.Tag;
                    frm.ShowDialog();
                }
            }
            else if(name.Contains("Topic"))
            {
                if(dgv_Topic_FileList.SelectedRows.Count == 1)
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Topic_FileList, "dgv_Topic_FL_", dgv_Topic_FileList.CurrentRow.Cells[0].Tag);
                    frm.ShowDialog();
                }
                else
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Topic_FileList, "dgv_Topic_FL_", null);
                    frm.parentId = tab_Topic_Info.Tag;
                    frm.ShowDialog();
                }
            }
            else if(name.Contains("Subject"))
            {
                if(dgv_Subject_FileList.SelectedRows.Count == 1)
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Subject_FileList, "dgv_Subject_FL_", dgv_Subject_FileList.CurrentRow.Cells[0].Tag);
                    frm.ShowDialog();
                }
                else
                {
                    Frm_AddFile frm = new Frm_AddFile(dgv_Subject_FileList, "dgv_Subject_FL_", null);
                    frm.parentId = tab_Subject_Info.Tag;
                    frm.ShowDialog();
                }
            }
        }

        private void Tab_Info_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                btn_Special_AddFile.Visible = lbl_Special_FileDetail.Visible = false;
                int index = tab_Special_Info.SelectedIndex;
                object objid = tab_Special_Info.Tag;
                if(objid != null)
                {
                    if(index == 0)
                    {
                        btn_Special_AddFile.Visible = lbl_Special_FileDetail.Visible = true;
                    }
                    if(index == 1)
                    {
                        LoadFileValidList(dgv_Special_FileValid, objid, "dgv_Special_FV_", txt_Project_Code.Text, txt_Project_Name.Text);
                        dgv_Special_FileValid.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular);
                    }
                    else if(index == 2)
                    {
                        DataTable dataTable = SQLiteHelper.ExecuteQuery($"SELECT * FROM files_tag_info WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_Special_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_Special_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_Special_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Special_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Special_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_Special_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_Special_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            string code = GetAJCode(objid, txt_Special_Code.Text, 0);
                            if(!string.IsNullOrEmpty(code))
                            {
                                txt_Special_AJ_Code.Text = code;
                                txt_Special_AJ_Secret.Text = GetMaxSecretById(objid);
                                txt_Special_AJ_User.Text = UserHelper.GetUser().RealName;
                                txt_Special_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                            }
                            else
                                MessageBox.Show("生成案卷编号失败,请检查是否预设规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else if(index == 3)
                    {
                        object[] obj = SQLiteHelper.ExecuteRowsQuery($"SELECT pt_code, pt_name FROM files_tag_info WHERE pt_obj_id='{objid}'");
                        if(obj != null)
                        {
                            lbl_Special_AJ_Code.Text = GetValue(obj[0]);
                            lbl_Special_AJ_Name.Text = GetValue(obj[1]);
                        }
                        else
                        {
                            lbl_Special_AJ_Code.Text = string.Empty;
                            lbl_Special_AJ_Name.Text = string.Empty;
                        }
                        LoadBoxList(objid, ControlType.Plan);
                        LoadFileBoxTable(cbo_Special_BoxId.SelectedValue, objid, ControlType.Plan);
                    }
                }
            }
            else if(name.Contains("Project"))
            {
                btn_Project_AddFile.Visible = lbl_Project_FileDetail.Visible = false;
                int index = tab_Project_Info.SelectedIndex;
                object objid = tab_Project_Info.Tag;
                if(objid != null)
                {
                    if(index == 0)
                    {
                        btn_Project_AddFile.Visible = lbl_Project_FileDetail.Visible = true;
                    }
                    if(index == 1)
                    {
                        LoadFileValidList(dgv_Project_FileValid, objid, "dgv_Project_FV_", txt_Project_Code.Text, txt_Project_Name.Text);
                        dgv_Project_FileValid.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular);
                    }
                    else if(index == 2)
                    {
                        string code = GetAJCode(objid, txt_Project_Code.Text, 0);
                        DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM files_tag_info WHERE pt_obj_id='{objid}' ORDER BY pt_source_type");
                        if(row != null)
                        {
                            int param = Convert.ToInt32(row["pt_source_type"]);
                            txt_Project_AJ_Name.Tag = param;
                            txt_Project_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Project_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Project_AJ_Code.Tag = GetValue(row["pt_id"]);
                            if(param == 1)//历史
                            {
                                if(!string.IsNullOrEmpty(code))
                                {
                                    txt_Project_AJ_Code.Text = code;
                                    txt_Project_AJ_Secret.Text = GetMaxSecretById(objid);
                                    txt_Project_AJ_User.Text = UserHelper.GetUser().RealName;
                                    txt_Project_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                                }
                                else
                                    MessageBox.Show("生成案卷编号失败,请检查是否预设规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else if(param == 0)
                            {
                                txt_Project_AJ_Code.Text = GetValue(row["pt_code"]);
                                txt_Project_AJ_Secret.Text = GetValue(row["pt_secret"]);
                                txt_Project_AJ_User.Text = GetValue(row["pt_user"]);
                                txt_Project_AJ_Unit.Text = GetValue(row["pt_unit"]);
                            }
                        }
                        else
                        {
                            txt_Project_AJ_Code.Text = code;
                            txt_Project_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_Project_AJ_User.Text = UserHelper.GetUser().RealName;
                            txt_Project_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                        }
                    }
                    else if(index == 3)
                    {
                        object[] obj = SQLiteHelper.ExecuteRowsQuery($"SELECT pt_code, pt_name FROM files_tag_info WHERE pt_obj_id='{objid}' ORDER BY pt_source_type");
                        if(obj != null)
                        {
                            lbl_Project_AJ_Code.Text = GetValue(obj[0]);
                            lbl_Project_AJ_Name.Text = GetValue(obj[1]);
                        }
                        else
                        {
                            lbl_Project_AJ_Code.Text = string.Empty;
                            lbl_Project_AJ_Name.Text = string.Empty;
                        }
                        LoadBoxList(objid, ControlType.Plan_Project);
                        LoadFileBoxTable(cbo_Project_BoxId.SelectedValue, objid, ControlType.Plan_Project);
                    }
                }
            }
            else if(name.Contains("Topic"))
            {
                btn_Topic_AddFile.Visible = lbl_Topic_FileDetail.Visible = false;
                int index = tab_Topic_Info.SelectedIndex;
                object objid = tab_Topic_Info.Tag;
                if(objid != null)
                {
                    if(index == 0)
                    {
                        btn_Topic_AddFile.Visible = lbl_Topic_FileDetail.Visible = true;
                    }
                    if(index == 1)
                    {
                        LoadFileValidList(dgv_Topic_FileValid, objid, "dgv_Topic_FV_", txt_Topic_Code.Text, txt_Topic_Name.Text);
                        dgv_Topic_FileValid.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular);
                    }
                    else if(index == 2)
                    {
                        string code = GetAJCode(objid, txt_Topic_Code.Text, 0);
                        DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM files_tag_info WHERE pt_obj_id='{objid}' ORDER BY pt_source_type");
                        if(row != null)
                        {
                            int param = Convert.ToInt32(row["pt_source_type"]);
                            txt_Topic_AJ_Name.Tag = param;
                            txt_Topic_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Topic_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Topic_AJ_Code.Tag = GetValue(row["pt_id"]);
                            if(param == 1)//历史
                            {
                                if(!string.IsNullOrEmpty(code))
                                {
                                    txt_Topic_AJ_Code.Text = code;
                                    txt_Topic_AJ_Secret.Text = GetMaxSecretById(objid);
                                    txt_Topic_AJ_User.Text = UserHelper.GetUser().RealName;
                                    txt_Topic_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                                }
                                else
                                    MessageBox.Show("生成案卷编号失败,请检查是否预设规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else if(param == 0)
                            {
                                txt_Topic_AJ_Code.Text = GetValue(row["pt_code"]);
                                txt_Topic_AJ_Secret.Text = GetValue(row["pt_secret"]);
                                txt_Topic_AJ_User.Text = GetValue(row["pt_user"]);
                                txt_Topic_AJ_Unit.Text = GetValue(row["pt_unit"]);
                            }
                        }
                        else
                        {
                            txt_Topic_AJ_Code.Text = code;
                            txt_Topic_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_Topic_AJ_User.Text = UserHelper.GetUser().RealName;
                            txt_Topic_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                        }
                    }
                    else if(index == 3)
                    {
                        object[] obj = SQLiteHelper.ExecuteRowsQuery($"SELECT pt_code, pt_name FROM files_tag_info WHERE pt_obj_id='{objid}' ORDER BY pt_source_type");
                        if(obj != null)
                        {
                            lbl_Topic_AJ_Code.Text = GetValue(obj[0]);
                            lbl_Topic_AJ_Name.Text = GetValue(obj[1]);
                        }
                        else
                        {
                            lbl_Topic_AJ_Code.Text = string.Empty;
                            lbl_Topic_AJ_Name.Text = string.Empty;
                        }
                        LoadBoxList(objid, ControlType.Plan_Topic);
                        LoadFileBoxTable(cbo_Topic_BoxId.SelectedValue, objid, ControlType.Plan_Topic);
                    }
                }
            }
            else if(name.Contains("Subject"))
            {
                btn_Subject_AddFile.Visible = lbl_Subject_FileDetail.Visible = false;
                int index = tab_Subject_Info.SelectedIndex;
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if(index == 0)
                    {
                        btn_Subject_AddFile.Visible = lbl_Subject_FileDetail.Visible = true;
                    }
                    if(index == 1)
                    {
                        LoadFileValidList(dgv_Subject_FileValid, objId, "dgv_Subject_FV_", txt_Subject_Code.Text, txt_Subject_Name.Text);
                        dgv_Subject_FileValid.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular);
                    }
                    else if(index == 2)
                    {
                        string code = GetAJCode(objId, txt_Subject_Code.Text, 0);
                        DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM files_tag_info WHERE pt_obj_id='{objId}' ORDER BY pt_source_type");
                        if(row != null)
                        {
                            int param = Convert.ToInt32(row["pt_source_type"]);
                            txt_Subject_AJ_Name.Tag = param;
                            txt_Subject_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Subject_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Subject_AJ_Code.Tag = GetValue(row["pt_id"]);
                            if(param == 1)//历史
                            {
                                if(!string.IsNullOrEmpty(code))
                                {
                                    txt_Subject_AJ_Code.Text = code;
                                    txt_Subject_AJ_Secret.Text = GetMaxSecretById(objId);
                                    txt_Subject_AJ_User.Text = UserHelper.GetUser().RealName;
                                    txt_Subject_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                                }
                                else
                                    MessageBox.Show("生成案卷编号失败,请检查是否预设规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else if(param == 0)
                            {
                                txt_Subject_AJ_Code.Text = GetValue(row["pt_code"]);
                                txt_Subject_AJ_Secret.Text = GetValue(row["pt_secret"]);
                                txt_Subject_AJ_User.Text = GetValue(row["pt_user"]);
                                txt_Subject_AJ_Unit.Text = GetValue(row["pt_unit"]);
                            }
                        }
                        else
                        {
                            txt_Subject_AJ_Code.Text = code;
                            txt_Subject_AJ_Secret.Text = GetMaxSecretById(objId);
                            txt_Subject_AJ_User.Text = UserHelper.GetUser().RealName;
                            txt_Subject_AJ_Unit.Text = UserHelper.GetUser().UserUnitName;
                        }
                    }
                    else if(index == 3)
                    {
                        object[] obj = SQLiteHelper.ExecuteRowsQuery($"SELECT pt_code, pt_name FROM files_tag_info WHERE pt_obj_id='{objId}' ORDER BY pt_source_type");
                        if(obj != null)
                        {
                            lbl_Subject_AJ_Code.Text = GetValue(obj[0]);
                            lbl_Subject_AJ_Name.Text = GetValue(obj[1]);
                        }
                        else
                        {
                            lbl_Subject_AJ_Code.Text = string.Empty;
                            lbl_Subject_AJ_Name.Text = string.Empty;
                        }
                        LoadBoxList(objId, ControlType.Plan_Topic_Subject);
                        LoadFileBoxTable(cbo_Subject_BoxId.SelectedValue, objId, ControlType.Plan_Topic_Subject);
                    }
                }
            }
        }

        /// <summary>
        /// 根据预设规则获取编码
        /// </summary>
        /// <param name="type">0：案卷 1：馆藏号</param>
        private string GetAJCode(object objId, object objCode, int type)
        {
            string code = string.Empty;
            DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM code_rule WHERE cr_type='{type}' AND cr_special_id='{UserHelper.GetUser().UserSpecialId}'");
            if(row != null)
            {
                string symbol = GetValue(row["cr_split_symbol"]);
                string template = GetValue(row["cr_template"]);
                string[] strs = template.Split(symbol.ToCharArray());
                for(int i = 0; i < strs.Length; i++)
                {
                    if("AAAA".Equals(strs[i]))//专项编号
                        code += GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT spi_code FROM special_info WHERE spi_id='{UserHelper.GetUser().UserSpecialId}'"));
                    else if("BBBB".Equals(strs[i]))//项目/课题编号
                        code += objCode;
                    else if("CCCC".Equals(strs[i]))//来源单位
                        code += GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{UserHelper.GetUser().UserUnitId}'"));
                    else if("2018".Equals(strs[i]))
                        code += DateTime.Now.Year;
                    else
                    {
                        int length = strs[i].Length;
                        int amount = 0;
                        if(type == 0)
                        {
                            amount = SQLiteHelper.ExecuteCountQuery($"SELECT COUNT(pt_id) FROM files_tag_info WHERE pt_special_id='{UserHelper.GetUser().UserSpecialId}' AND pt_source_type=0") + 1;
                        }else if(type == 1)
                        {
                            amount = SQLiteHelper.ExecuteCountQuery($"SELECT COUNT(pb_id) FROM files_box_info WHERE pb_special_id='{UserHelper.GetUser().UserSpecialId}' AND pb_source_type=0") + 1;
                        }
                        code += amount.ToString().PadLeft(length, '0');
                    }
                    code += symbol;
                }
            }
            return code.Length == 0 ? code : code.Substring(0, code.Length - 1);
        }

        /// <summary>
        /// 加载计划-案卷盒归档表
        /// </summary>
        /// <param name="pbId">案卷盒ID</param>
        /// <param name="objId">所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            if(type == ControlType.Plan)
            {
                LoadFileBoxTableInstance(lsv_Special_Left, lsv_Special_Right, "special_", pbId, objId);
            }
            else if(type == ControlType.Plan_Project)
            {
                LoadFileBoxTableInstance(lsv_Project_Left, lsv_Project_Right, "project_", pbId, objId);
            }
            else if(type == ControlType.Plan_Topic)
            {
                LoadFileBoxTableInstance(lsv_Topic_Left, lsv_Topic_Right, "topic_", pbId, objId);
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                LoadFileBoxTableInstance(lsv_Subject_Left, lsv_Subject_Right, "subject_", pbId, objId);
            }
        }

        /// <summary>
        /// 加载案卷盒归档表
        /// </summary>
        /// <param name="leftView">待归档列表</param>
        /// <param name="rightView">已归档列表</param>
        /// <param name="key">关键字</param>
        /// <param name="pbId">盒ID</param>
        /// <param name="objId">所属对象ID</param>
        private void LoadFileBoxTableInstance(ListView leftView, ListView rightView, string key, object pbId, object objId)
        {
            leftView.Items.Clear();
            leftView.Columns.Clear();
            rightView.Items.Clear();
            rightView.Columns.Clear();

            leftView.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = $"{key}_file1_id", Text = "主键", Width = 0},
                    new ColumnHeader{ Name = $"{key}_file1_type", Text = "文件类别", TextAlign = HorizontalAlignment.Center ,Width = 75},
                    new ColumnHeader{ Name = $"{key}_file1_name", Text = "文件名称", Width = 250},
                    new ColumnHeader{ Name = $"{key}_file1_date", Text = "形成日期", Width = 100}
            });
            rightView.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = $"{key}_file2_id", Text = "主键", Width = 0},
                    new ColumnHeader{ Name = $"{key}_file2_type", Text = "文件类别", TextAlign = HorizontalAlignment.Center ,Width = 75},
                    new ColumnHeader{ Name = $"{key}_file2_name", Text = "文件名称", Width = 250},
                    new ColumnHeader{ Name = $"{key}_file2_date", Text = "形成日期", Width = 100}
            });
            //未归档
            string querySql = $"SELECT fi_id, dd_name, fi_name, fi_create_date FROM files_info LEFT JOIN data_dictionary " +
                $"ON fi_categor = dd_id WHERE fi_obj_id = '{objId}' AND fi_status = -1 ORDER BY fi_create_date";
            DataTable dataTable = SQLiteHelper.ExecuteQuery(querySql);
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = leftView.Items.Add(GetValue(dataTable.Rows[i]["fi_id"]));
                item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                {
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(dataTable.Rows[i]["dd_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(dataTable.Rows[i]["fi_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetDateValue(dataTable.Rows[i]["fi_create_date"], "yyyy-MM-dd") },
                });
            }
            //已归档
            object id = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM files_box_info WHERE pb_id = '{pbId}'");
            if(id != null)
            {
                string[] ids = GetValue(id).Split(',');
                for(int i = 0; i < ids.Length; i++)
                {
                    querySql = $"SELECT fi_id, dd_name, fi_name, fi_create_date FROM files_info LEFT JOIN data_dictionary ON fi_categor=dd_id WHERE fi_id ='{ids[i]}'";
                    DataRow row = SQLiteHelper.ExecuteSingleRowQuery(querySql);
                    if(row != null)
                    {
                        ListViewItem item = rightView.Items.Add(GetValue(row["fi_id"]));
                        item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(row["dd_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(row["fi_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetDateValue(row["fi_create_date"], "yyyy-MM-dd") },
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 计划 - 加载案卷盒列表
        /// </summary>
        /// <param name="objId">案卷盒所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadBoxList(object objId, ControlType type)
        {
            DataTable table = SQLiteHelper.ExecuteQuery($"SELECT pb_id, pb_box_number FROM files_box_info WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if(type == ControlType.Plan)
            {
                cbo_Special_BoxId.DataSource = table;
                cbo_Special_BoxId.DisplayMember = "pb_box_number";
                cbo_Special_BoxId.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Special_BoxId.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Project)
            {
                cbo_Project_BoxId.DataSource = table;
                cbo_Project_BoxId.DisplayMember = "pb_box_number";
                cbo_Project_BoxId.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Project_BoxId.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Topic)
            {
                cbo_Topic_BoxId.DataSource = table;
                cbo_Topic_BoxId.DisplayMember = "pb_box_number";
                cbo_Topic_BoxId.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Topic_BoxId.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Topic_Subject)
            {
                cbo_Subject_BoxId.DataSource = table;
                cbo_Subject_BoxId.DisplayMember = "pb_box_number";
                cbo_Subject_BoxId.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Subject_BoxId.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 获取最高密级
        /// </summary>
        private string GetMaxSecretById(object objid) => GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT dd_name FROM files_info LEFT JOIN data_dictionary ON fi_secret = dd_id WHERE fi_obj_id = '{objid}' ORDER BY dd_sort DESC LIMIT(1)"));

        /// <summary>
        /// 加载文件缺失校验列表
        /// </summary>
        /// <param name="dataGridView">待校验表格</param>
        /// <param name="objid">主键</param>
        private void LoadFileValidList(DataGridView dataGridView, object objid, string key, string code, string name)
        {
            dataGridView.Rows.Clear();

            string querySql = "SELECT dd_name, dd_note FROM data_dictionary WHERE dd_pId in(" +
                "SELECT dd_id FROM data_dictionary WHERE dd_pId = (" +
                "SELECT dd_id FROM data_dictionary  WHERE dd_code = 'dic_file_jd')) AND dd_name NOT IN(" +
                $"SELECT dd.dd_name FROM files_info fi LEFT JOIN data_dictionary dd ON fi.fi_categor = dd.dd_id where fi.fi_obj_id='{objid}')" +
                $" ORDER BY dd_name";
            DataTable table = SQLiteHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                int indexRow = dataGridView.Rows.Add();
                dataGridView.Rows[indexRow].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[indexRow].Cells[key + "categor"].Value = table.Rows[i]["dd_name"];
                dataGridView.Rows[indexRow].Cells[key + "name"].Value = table.Rows[i]["dd_note"];
                dataGridView.Rows[indexRow].Cells[key + "pcode"].Value = code;
                dataGridView.Rows[indexRow].Cells[key + "pname"].Value = name;
                string queryReasonSql = $"SELECT pfo_id, pfo_reason, pfo_remark FROM files_lost_info WHERE pfo_obj_id='{objid}' AND pfo_categor='{table.Rows[i]["dd_name"]}'";
                object[] _obj = SQLiteHelper.ExecuteRowsQuery(queryReasonSql);
                if(_obj != null)
                {
                    dataGridView.Rows[indexRow].Cells[key + "id"].Tag = GetValue(_obj[0]);
                    dataGridView.Rows[indexRow].Cells[key + "reason"].Value = GetValue(_obj[1]);
                    dataGridView.Rows[indexRow].Cells[key + "remark"].Value = GetValue(_obj[2]);
                }
            }
        }

        /// <summary>
        /// 修改或保存指定文件校验列表
        /// </summary>
        /// <param name="dataGridView">指定的表格</param>
        /// <param name="objid">主键</param>
        /// <param name="key">关键字</param>
        private void ModifyFileValid(DataGridView dataGridView, object objid, string key)
        {
            int rowCount = dataGridView.Rows.Count;
            for(int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                object name = row.Cells[key + "name"].Value;
                if(name != null)
                {
                    object rid = dataGridView.Rows[i].Cells[key + "id"].Tag;
                    object pcode = row.Cells[key + "pcode"].Value;
                    object pname = row.Cells[key + "pname"].Value;
                    object categor = row.Cells[key + "categor"].Value;
                    object reason = row.Cells[key + "reason"].Value;
                    object remark = row.Cells[key + "remark"].Value;
                    if(rid == null)
                    {
                        rid = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO files_lost_info VALUES('{rid}','{categor}','{name}','{reason}','{remark}','{objid}')";
                        SQLiteHelper.ExecuteNonQuery(insertSql);
                        dataGridView.Rows[i].Cells[key + "id"].Tag = rid;
                    }
                    else
                    {
                        string updateSql = $"UPDATE files_lost_info SET " +
                            $"pfo_categor='{categor}'," +
                            $"pfo_name='{name}'," +
                            $"pfo_reason='{reason}'," +
                            $"pfo_remark='{remark}'" +
                            $" WHERE pfo_id='{rid}'";
                        SQLiteHelper.ExecuteNonQuery(updateSql);
                    }
                }
            }
        }

        /// <summary>
        /// 案卷盒归档移动
        /// </summary>
        private void btn_BoxMove_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                object objId = tab_Special_Info.Tag;
                object boxId = cbo_Special_BoxId.SelectedValue;
                if(objId != null)
                {
                    if(boxId != null)
                    {
                        if(name.Contains("RightMove"))
                        {
                            foreach(ListViewItem item in lsv_Special_Left.SelectedItems)
                            {
                                lsv_Special_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftMove"))
                        {
                            foreach(ListViewItem item in lsv_Special_Right.SelectedItems)
                            {
                                lsv_Special_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("RightAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Special_Left.Items)
                            {
                                lsv_Special_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Special_Right.Items)
                            {
                                lsv_Special_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("Top"))
                        {
                            foreach(ListViewItem item in lsv_Special_Right.SelectedItems)
                            {
                                int index = item.Index;
                                if(index > 0)
                                {
                                    lsv_Special_Right.Items.RemoveAt(index);
                                    lsv_Special_Right.Items.Insert(index - 1, item);
                                }
                            }
                        }
                        else if(name.Contains("Bottom"))
                        {
                            int size = lsv_Special_Right.Items.Count - 1;
                            for(int i = size; i >= 0; i--)
                            {
                                ListViewItem item = lsv_Special_Right.Items[i];
                                if(item.Selected)
                                {
                                    int index = item.Index;
                                    if(index < size)
                                    {
                                        lsv_Special_Right.Items.RemoveAt(index);
                                        lsv_Special_Right.Items.Insert(index + 1, item);
                                    }
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("请先添加案卷盒！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else if(name.Contains("Project"))
            {
                object objId = tab_Project_Info.Tag;
                object boxId = cbo_Project_BoxId.SelectedValue;
                if(objId != null)
                {
                    if(boxId != null)
                    {
                        if(name.Contains("RightMove"))
                        {
                            foreach(ListViewItem item in lsv_Project_Left.SelectedItems)
                            {
                                lsv_Project_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftMove"))
                        {
                            foreach(ListViewItem item in lsv_Project_Right.SelectedItems)
                            {
                                lsv_Project_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("RightAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Project_Left.Items)
                            {
                                lsv_Project_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Project_Right.Items)
                            {
                                lsv_Project_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("Top"))
                        {
                            foreach(ListViewItem item in lsv_Project_Right.SelectedItems)
                            {
                                int index = item.Index;
                                if(index > 0)
                                {
                                    lsv_Project_Right.Items.RemoveAt(index);
                                    lsv_Project_Right.Items.Insert(index - 1, item);
                                }
                            }
                        }
                        else if(name.Contains("Bottom"))
                        {
                            int size = lsv_Project_Right.Items.Count - 1;
                            for(int i = size; i >= 0; i--)
                            {
                                ListViewItem item = lsv_Project_Right.Items[i];
                                if(item.Selected)
                                {
                                    int index = item.Index;
                                    if(index < size)
                                    {
                                        lsv_Project_Right.Items.RemoveAt(index);
                                        lsv_Project_Right.Items.Insert(index + 1, item);
                                    }
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("请先添加案卷盒！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else if(name.Contains("Topic"))
            {
                object objId = tab_Topic_Info.Tag;
                object boxId = cbo_Topic_BoxId.SelectedValue;
                if(objId != null)
                {
                    if(boxId != null)
                    {
                        if(name.Contains("RightMove"))
                        {
                            foreach(ListViewItem item in lsv_Topic_Left.SelectedItems)
                            {
                                lsv_Topic_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftMove"))
                        {
                            foreach(ListViewItem item in lsv_Topic_Right.SelectedItems)
                            {
                                lsv_Topic_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("RightAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Topic_Left.Items)
                            {
                                lsv_Topic_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Topic_Right.Items)
                            {
                                lsv_Topic_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("Top"))
                        {
                            foreach(ListViewItem item in lsv_Topic_Right.SelectedItems)
                            {
                                int index = item.Index;
                                if(index > 0)
                                {
                                    lsv_Topic_Right.Items.RemoveAt(index);
                                    lsv_Topic_Right.Items.Insert(index - 1, item);
                                }
                            }
                        }
                        else if(name.Contains("Bottom"))
                        {
                            int size = lsv_Topic_Right.Items.Count - 1;
                            for(int i = size; i >= 0; i--)
                            {
                                ListViewItem item = lsv_Topic_Right.Items[i];
                                if(item.Selected)
                                {
                                    int index = item.Index;
                                    if(index < size)
                                    {
                                        lsv_Topic_Right.Items.RemoveAt(index);
                                        lsv_Topic_Right.Items.Insert(index + 1, item);
                                    }
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("请先添加案卷盒！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else if(name.Contains("Subject"))
            {
                object objId = tab_Subject_Info.Tag;
                object boxId = cbo_Subject_BoxId.SelectedValue;
                if(objId != null)
                {
                    if(boxId != null)
                    {
                        if(name.Contains("RightMove"))
                        {
                            foreach(ListViewItem item in lsv_Subject_Left.SelectedItems)
                            {
                                lsv_Subject_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftMove"))
                        {
                            foreach(ListViewItem item in lsv_Subject_Right.SelectedItems)
                            {
                                lsv_Subject_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("RightAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Subject_Left.Items)
                            {
                                lsv_Subject_Right.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("LeftAllMove"))
                        {
                            foreach(ListViewItem item in lsv_Subject_Right.Items)
                            {
                                lsv_Subject_Left.Items.Add((ListViewItem)item.Clone());
                                item.Remove();
                            }
                        }
                        else if(name.Contains("Top"))
                        {
                            foreach(ListViewItem item in lsv_Subject_Right.SelectedItems)
                            {
                                int index = item.Index;
                                if(index > 0)
                                {
                                    lsv_Subject_Right.Items.RemoveAt(index);
                                    lsv_Subject_Right.Items.Insert(index - 1, item);
                                }
                            }
                        }
                        else if(name.Contains("Bottom"))
                        {
                            int size = lsv_Subject_Right.Items.Count - 1;
                            for(int i = size; i >= 0; i--)
                            {
                                ListViewItem item = lsv_Subject_Right.Items[i];
                                if(item.Selected)
                                {
                                    int index = item.Index;
                                    if(index < size)
                                    {
                                        lsv_Subject_Right.Items.RemoveAt(index);
                                        lsv_Subject_Right.Items.Insert(index + 1, item);
                                    }
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("请先添加案卷盒！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 添加/删除案卷盒
        /// </summary>
        private void Lnk_BoxId_Edit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if(name.Contains("Add"))
                    {
                        string gch = GetAJCode(objId, txt_Special_Code.Text, 1);
                        if(!string.IsNullOrEmpty(gch))
                        {
                            int amount = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'"));
                            string unitCode = string.Empty;
                            string insertSql = $"INSERT INTO files_box_info VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            MessageBox.Show("添加案卷盒成功。");
                        }
                        else
                            MessageBox.Show("生成馆藏号失败，请检查是否预设生成规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(name.Contains("Delete"))
                    {
                        object boxId = cbo_Special_BoxId.SelectedValue;
                        if(boxId != null)
                        {
                            object _temp = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'");
                            if(_temp != null)
                            {
                                int currentBoxId = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM files_box_info WHERE pb_id='{boxId}'"));
                                if(Convert.ToInt32(_temp) > currentBoxId)
                                    MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM files_box_info WHERE pb_obj_id='{objId}' AND pb_id='{boxId}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE files_info SET fi_status = -1 WHERE fi_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SQLiteHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{boxId}'");

                                    MessageBox.Show("删除案卷盒成功。");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan);
                    LoadFileBoxTable(cbo_Special_BoxId.SelectedValue, objId, ControlType.Plan);
                }
            }
            else if(name.Contains("Project"))
            {
                object objId = tab_Project_Info.Tag;
                if(objId != null)
                {
                    if(name.Contains("Add"))
                    {
                        string gch = GetAJCode(objId, txt_Project_Code.Text, 1);
                        if(!string.IsNullOrEmpty(gch))
                        {
                            int amount = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'"));
                            string unitCode = string.Empty;
                            string insertSql = $"INSERT INTO files_box_info VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            MessageBox.Show("添加案卷盒成功。");
                        }
                        else
                            MessageBox.Show("生成馆藏号失败，请检查是否预设生成规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(name.Contains("Delete"))
                    {
                        object boxId = cbo_Project_BoxId.SelectedValue;
                        if(boxId != null)
                        {
                            object _temp = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'");
                            if(_temp != null)
                            {
                                int currentBoxId = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM files_box_info WHERE pb_id='{boxId}'"));
                                if(Convert.ToInt32(_temp) > currentBoxId)
                                    MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM files_box_info WHERE pb_obj_id='{objId}' AND pb_id='{boxId}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE files_info SET fi_status = -1 WHERE fi_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SQLiteHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{boxId}'");

                                    MessageBox.Show("删除案卷盒成功。");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_Project_BoxId.SelectedValue, objId, ControlType.Plan_Project);
                }
            }
            else if(name.Contains("Topic"))
            {
                object objId = tab_Topic_Info.Tag;
                if(objId != null)
                {
                    if(name.Contains("Add"))
                    {
                        string gch = GetAJCode(objId, txt_Topic_Code.Text, 1);
                        if(!string.IsNullOrEmpty(gch))
                        {
                            int amount = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'"));
                            string unitCode = string.Empty;
                            string insertSql = $"INSERT INTO files_box_info VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            MessageBox.Show("添加案卷盒成功。");
                        }
                        else
                            MessageBox.Show("生成馆藏号失败，请检查是否预设生成规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(name.Contains("Delete"))
                    {
                        object boxId = cbo_Topic_BoxId.SelectedValue;
                        if(boxId != null)
                        {
                            object _temp = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'");
                            if(_temp != null)
                            {
                                int currentBoxId = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM files_box_info WHERE pb_id='{boxId}'"));
                                if(Convert.ToInt32(_temp) > currentBoxId)
                                    MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM files_box_info WHERE pb_obj_id='{objId}' AND pb_id='{boxId}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE files_info SET fi_status = -1 WHERE fi_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SQLiteHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{boxId}'");

                                    MessageBox.Show("删除案卷盒成功。");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan_Topic);
                    LoadFileBoxTable(cbo_Topic_BoxId.SelectedValue, objId, ControlType.Plan_Topic);
                }
            }
            else if(name.Contains("Subject"))
            {
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if(name.Contains("Add"))
                    {
                        string gch = GetAJCode(objId, txt_Subject_Code.Text, 1);
                        if(!string.IsNullOrEmpty(gch))
                        {
                            int amount = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'"));
                            string unitCode = string.Empty;
                            string insertSql = $"INSERT INTO files_box_info VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            MessageBox.Show("添加案卷盒成功。");
                        }
                        else
                            MessageBox.Show("生成馆藏号失败，请检查是否预设生成规则。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(name.Contains("Delete"))
                    {
                        object boxId = cbo_Subject_BoxId.SelectedValue;
                        if(boxId != null)
                        {
                            object _temp = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'");
                            if(_temp != null)
                            {
                                int currentBoxId = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM files_box_info WHERE pb_id='{boxId}'"));
                                if(Convert.ToInt32(_temp) > currentBoxId)
                                    MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM files_box_info WHERE pb_obj_id='{objId}' AND pb_id='{boxId}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE files_info SET fi_status = -1 WHERE fi_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SQLiteHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{boxId}'");

                                    MessageBox.Show("删除案卷盒成功。");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan_Topic_Subject);
                    LoadFileBoxTable(cbo_Subject_BoxId.SelectedValue, objId, ControlType.Plan_Topic_Subject);
                }
            }
        }

        private void Cbo_BoxId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if("cbo_Special_BoxId".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Special_Info.Tag, ControlType.Plan);
                object gcid = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM files_box_info WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Special_GCID.Text = GetValue(gcid);
            }
            else if("cbo_Project_BoxId".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Project_Info.Tag, ControlType.Plan_Project);
                object gcid = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM files_box_info WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Project_GCID.Text = GetValue(gcid);
            }
            else if("cbo_Topic_BoxId".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Topic_Info.Tag, ControlType.Plan_Topic);
                object gcid = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM files_box_info WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Topic_GCID.Text = GetValue(gcid);
            }
            else if("cbo_Subject_BoxId".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Subject_Info.Tag, ControlType.Plan_Topic_Subject);
                object gcid = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM files_box_info WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Subject_GCID.Text = GetValue(gcid);
            }
        }

        private void dgv_Special_FileList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
                SetFileDetail(ControlType.Plan, e.RowIndex);
            else if(name.Contains("Project"))
                SetFileDetail(ControlType.Plan_Project, e.RowIndex);
            else if(name.Contains("Topic"))
                SetFileDetail(ControlType.Plan_Topic, e.RowIndex);
            else if(name.Contains("Subject"))
                SetFileDetail(ControlType.Plan_Topic_Subject, e.RowIndex);
        }

        private void dgv_Subject_FileList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridView dataGridView = sender as DataGridView;
                if(dataGridView.Columns[e.ColumnIndex].Name.Contains("link"))
                {
                    string path = GetValue(dataGridView.CurrentCell.Value);
                    if(!string.IsNullOrEmpty(path))
                    {
                        if(MessageBox.Show("是否打开文件?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            if(File.Exists(path))
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start("Explorer.exe", path);
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("文件不存在。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                    }
                }
            }
        }

        private void 添加文件AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)((sender as ToolStripItem).GetCurrentParent() as ContextMenuStrip).Tag;
            object rootId = SQLiteHelper.ExecuteOnlyOneQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_name = '{UserHelper.GetUser().RealName}' AND bfi_code = '-1'");
            if(rootId != null)
            {
                Frm_AddFile_FileSelect frm = new Frm_AddFile_FileSelect(rootId);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    string fullPath = frm.SelectedFileName;
                    if(File.Exists(fullPath))
                    {
                        string savePath = Application.StartupPath + @"\TempBackupFolder\";
                        if(!Directory.Exists(savePath))
                            Directory.CreateDirectory(savePath);
                        string filePath = savePath + new FileInfo(fullPath).Name;
                        File.Copy(fullPath, filePath, true);
                        view.CurrentCell.Value = fullPath;
                        if(MessageBox.Show("已从服务器拷贝文件到本地，是否现在打开？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            System.Diagnostics.Process.Start("Explorer.exe", filePath);
                    }
                    else
                        MessageBox.Show("服务器不存在此文件。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
                MessageBox.Show("当前专项尚未导入数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgv_FileList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridView view = sender as DataGridView;
                if(view.Columns[e.ColumnIndex].Name.Contains("link"))
                {
                    view.ClearSelection();
                    view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Tag = view;
                    contextMenuStrip1.Show(MousePosition);
                }
            }
            
        }

        private void 删除文件DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)((sender as ToolStripItem).GetCurrentParent() as ContextMenuStrip).Tag;
            view.CurrentCell.Value = string.Empty;
        }

        private void btn_Project_Add_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Project"))
            {
                ClearText(project, true);
            }
            else if(name.Contains("Topic"))
            {
                ClearText(topic, true);
            }
            else if(name.Contains("Subject"))
            {
                ClearText(Subject, true);
            }
        }

        private void tab_Menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = tab_Menu.SelectedTab.Name;
            object param = null;
            if("project".Equals(tabName))
                param = txt_Project_Code.Tag;
            else if("topic".Equals(tabName))
                param = txt_Topic_Code.Tag;
            else if("Subject".Equals(tabName))
                param = txt_Subject_Code.Tag;
            if(param != null)
            {
                if((int)param == 0)
                    SetText("[数据来源：专项办]");
                else if((int)param == 1)
                    SetText("[数据来源：课题组]");
            }
            else
                SetText(string.Empty);
        }
    }
}
