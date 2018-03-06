using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>
        /// 当前专项ID
        /// </summary>
        private object SpecialId;

        public Frm_Wroking(object SpecialId)
        {
            this.SpecialId = SpecialId;
            this.tabPages = new Dictionary<string, TabPage>();
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void Frm_Wroking_Load(object sender, EventArgs e)
        {
            //初始化选项卡
            foreach(TabPage item in tab_Menu.TabPages)
            {
                tabPages.Add(item.Name, item);
                tab_Menu.TabPages.Remove(item);
            }
            ShowTabPageByName("special", 0);
            //根据计划ID加载计划基本信息
            DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT spi_code, spi_name, spi_unit, spi_intro FROM SPECIAL_INFO WHERE spi_id='{SpecialId}'");
            txt_Special_Code.Text = GetValue(row["spi_code"]);
            txt_Special_Name.Text = GetValue(row["spi_name"]);
            txt_Special_Unit.Text = GetValue(row["spi_unit"]);
            txt_Special_Intro.Text = GetValue(row["spi_intro"]);
            tab_Special_Info.Tag = SpecialId;
            LoadFileInfoById(dgv_Special_FileList, "dgv_Special_FL_", SpecialId);
            //【阶段】
            InitialStageList(dgv_Special_FileList.Columns["dgv_Special_FL_stage"]);
            InitialStageList(dgv_Project_FileList.Columns["dgv_Project_FL_stage"]);
            InitialStageList(dgv_Topic_FileList.Columns["dgv_Topic_FL_stage"]);
            InitialStageList(dgv_subTopic_FileList.Columns["dgv_subTopic_FL_stage"]);
            //【文件类别】
            InitialCategorList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialCategorList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialCategorList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialCategorList(dgv_subTopic_FileList, "dgv_subTopic_FL_");
            //【文件类型】
            InitialTypeList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialTypeList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialTypeList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialTypeList(dgv_subTopic_FileList, "dgv_subTopic_FL_");
            //【密级】
            InitialSecretList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialSecretList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialSecretList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialSecretList(dgv_subTopic_FileList, "dgv_subTopic_FL_");
            //【载体】
            InitialCarrierList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialCarrierList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialCarrierList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialCarrierList(dgv_subTopic_FileList, "dgv_subTopic_FL_");
            //【文件格式】
            InitialFormatList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialFormatList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialFormatList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialFormatList(dgv_subTopic_FileList, "dgv_subTopic_FL_");
            //文件形态
            InitialFormList(dgv_Special_FileList, "dgv_Special_FL_");
            InitialFormList(dgv_Project_FileList, "dgv_Project_FL_");
            InitialFormList(dgv_Topic_FileList, "dgv_Topic_FL_");
            InitialFormList(dgv_subTopic_FileList, "dgv_subTopic_FL_");

            //下拉框默认
            cbo_Special_HasNext.SelectedIndex = 0;
            cbo_Project_HasNext.SelectedIndex = 0;
            cbo_Topic_HasNext.SelectedIndex = 0;
        }

        /// <summary>
        /// 根据指定ID加载文件相关信息
        /// </summary>
        /// <param name="pid">对象ID</param>
        /// <param name="key">关键字</param>
        private void LoadFileInfoById(DataGridView dataGridView, string key, object pid)
        {
            dataGridView.Rows.Clear();
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
            if(!string.IsNullOrEmpty(tabName) && tabPages.TryGetValue(tabName, out TabPage tabPage))
            {
                tab_Menu.TabPages.Insert(tabIndex, tabPage);
            }
            //删除指定索引后的选项卡
            for(int i = 0; i < tab_Menu.TabPages.Count; i++)
                if(i > tabIndex)
                    tab_Menu.TabPages[i].Tag = true;
            foreach(TabPage item in tab_Menu.TabPages)
                if(item.Tag != null)
                {
                    tab_Menu.TabPages.Remove(item);
                    item.Tag = null;
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
            if(index == 0 || index == 1)
            {
                ShowTabPageByName(string.Empty, 0);
            }
            else
            {
                object SpecialId = tab_Special_Info.Tag;
                if(index == 2)//项目
                {
                    ShowTabPageByName("project", 1);
                }
                else if(index == 3)//课题
                {
                    ShowTabPageByName("topic", 1);
                }
            }
        }

        /// <summary>
        /// 有无子课题事件
        /// </summary>
        private void Cbo_Topic_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Topic_HasNext.SelectedIndex;
            if(index == 0)
            {
                ShowTabPageByName("topic", 2);
            }
            else
            {
                ShowTabPageByName("subtopic", 3);
            }
        }

        /// <summary>
        /// 提交项目信息
        /// </summary>
        private void btn_XM_Submit_Click(object sender, EventArgs e)
        {
            string xmCode = txt_Special_Code.Text;
            string jhName = txt_Special_Unit.Text;
            if(xmCode == null || "".Equals(xmCode))
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
                        MessageBox.Show("文件保存成功。");
                    }
                    else if(index == 1)
                    {
                        ModifyFileValid(dgv_Special_FileValid, objId, "dgv_Special_FV_");
                        MessageBox.Show("文件核查信息保存成功！");
                    }
                    else if(index == 2)
                    {
                        object aid = txt_Special_AJ_Code.Tag;
                        string code = txt_Special_AJ_Code.Text;
                        string _name = txt_Special_AJ_Name.Text;
                        string term = txt_Special_AJ_Term.Text;
                        string secret = txt_Special_AJ_Secret.Text;
                        string user = txt_Special_AJ_User.Text;
                        string unit = txt_Special_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO files_tag_info VALUES ('{aid}','{code}','{_name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SQLiteHelper.ExecuteNonQuery(insertSql);
                            txt_Special_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE files_tag_info SET pt_code='{code}',pt_name='{_name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SQLiteHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                    else if(index == 3)
                    {
                        object boxId = cbo_Special_BoxId.SelectedValue;
                        if(boxId != null)
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

                                MessageBox.Show("保存案卷盒成功。");
                                LoadFileBoxTable(boxId, objId, ControlType.Plan);
                            }

                        }
                        else
                            MessageBox.Show("请先添加案卷盒。");
                    }
                }
                else
                    MessageBox.Show("请先保存基础信息！", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 课题提交操作
        /// </summary>
        private void btn_KT_Submit_Click(object sender, EventArgs e)
        {
            string ktCode = txt_KT_Code.Text;
            string xmName = txt_XM_Name.Text;
            if(ktCode == null || "".Equals(ktCode))
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
            if(zktCode == null || "".Equals(zktCode))
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
            else if("dgv_subTopic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_subTopic_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Topic_Subtopic;
                if("dgv_subTopic_FL_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("dgv_subTopic_FL_categor".Equals(columnName))
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
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subtopic)
                SetCategorByStage(comboBox.SelectedValue, dgv_subTopic_FileList.CurrentRow, "dgv_subTopic_FL_");
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
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subtopic)
                SetNameByCategor(comboBox.SelectedValue, dgv_subTopic_FileList.CurrentRow, "dgv_subTopic_FL_");
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

        private void Frm_Wroking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("请确保所有数据均已保存！", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void Cbo_Project_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Project_HasNext.SelectedIndex;
            if(index == 0 || index == 1)
            {
                ShowTabPageByName("project", 1);
            }
            else if(index == 2)//课题
            {
                ShowTabPageByName("topic", 2);
            }
            else if(index == 3)//子课题
            {
                ShowTabPageByName("subtopic", 2);
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
                Frm_AddFile frm = new Frm_AddFile(dgv_Special_FileList, "dgv_Special_FL_");
                frm.ShowDialog();
            }
        }

        private void Tab_Info_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                int index = tab_Special_Info.SelectedIndex;
                object objid = tab_Special_Info.Tag;
                if(objid != null)
                {
                    if(index == 1)
                        LoadFileValidList(dgv_Special_FileValid, objid, "dgv_Special_FV_");
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
                            //txt_Special_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            //txt_Special_AJ_Name.Text = lbl_JH_Name.Text;
                            txt_Special_AJ_Secret.Text = GetMaxSecretById(objid);
                            //txt_Special_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            //txt_Special_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                    else if(index == 3)
                    {
                        LoadBoxList(objid, ControlType.Plan);
                        LoadFileBoxTable(cbo_Special_BoxId.SelectedValue, objid, ControlType.Plan);
                    }
                }
            }
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
                string GCID = GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM files_box_info WHERE pb_id='{pbId}'"));
                txt_Special_GCID.Text = GCID;
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
        private void LoadFileValidList(DataGridView dataGridView, object objid, string key)
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
                    }
                    else
                        MessageBox.Show("请先添加案卷盒！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 添加/删除案卷盒
        /// </summary>
        private void lnk_BoxId_Edit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Special"))
            {
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if(name.Contains("Add"))
                    {
                        int amount = Convert.ToInt32(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM files_box_info WHERE pb_obj_id='{objId}'"));
                        string gch = txt_Special_GCID.Text;
                        string unitCode = string.Empty;
                        string insertSql = $"INSERT INTO files_box_info VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SQLiteHelper.ExecuteNonQuery(insertSql);
                        MessageBox.Show("添加案卷盒成功。");
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
        }

        private void cbo_BoxId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if("cbo_Special_BoxId".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Special_Info.Tag, ControlType.Plan);
            }
        }
    }
}
