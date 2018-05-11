﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_AddFile : Form
    {
        private DataGridView view;
        private object key;
        private object fileId;
        public object parentId;
        public Frm_AddFile(DataGridView view, object key, object fileId)
        {
            InitializeComponent();
            this.view = view;
            this.key = key;
            if(fileId != null)
            {
                Text = "编辑文件";
                this.fileId = fileId;
            }
        }

        private void Frm_AddFile_Load(object sender, EventArgs e)
        {
            //阶段
            cbo_stage.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            cbo_stage.DisplayMember = "dd_name";
            cbo_stage.ValueMember = "dd_id";
            //类别
            LoadCategorByStage(cbo_stage.SelectedValue);
            //默认焦点
            cbo_stage.Focus();
            //编辑状态加载信息
            LoadFileInfo(fileId);
        }

        private void LoadFileInfo(object fileId)
        {
            DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT * FROM files_info WHERE fi_id='{fileId}'");
            if(row != null)
            {
                cbo_stage.SelectedValue = row["fi_stage"];
                Cbo_stage_SelectionChangeCommitted(null, null);
                cbo_categor.SelectedValue = row["fi_categor"];
                txt_fileCode.Text = GetValue(row["fi_code"]);
                txt_fileName.Text = GetValue(row["fi_name"]);
                txt_user.Text = GetValue(row["fi_user"]);
                SetFileRadio(pal_type, row["fi_type"]);
                SetFileRadio(pal_secret, row["fi_secret"]);
                num_page.Value = Convert.ToInt32(row["fi_pages"]);
                DateTime dateTime = Convert.ToDateTime(row["fi_create_date"]);
                if(dateTime != DateTime.MinValue)
                    dtp_date.Value = dateTime;
                txt_unit.Text = GetValue(row["fi_unit"]);
                SetFileCheckBox(pal_carrier, row["fi_carrier"]);
                SetFileRadio(pal_form, row["fi_form"]);
                txt_link.Text = GetValue(row["fi_link"]);
                txt_Remark.Text = GetValue(row["fi_remark"]);
            }
        }

        private void SetFileCheckBox(Panel panel, object id)
        {
            foreach(CheckBox item in panel.Controls)
            {
                if(panel.Tag.Equals(id))
                    item.Checked = true;
                else if(item.Tag.Equals(id))
                {
                    item.Checked = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 选中指定ID的单选框
        /// </summary>
        private void SetFileRadio(Panel panel, object id)
        {
            foreach(RadioButton item in panel.Controls)
            {
                if(item.Tag.Equals(id))
                {
                    item.Checked = true;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取选定单选框的ID
        /// </summary>
        private object GetFileRadio(Panel panel)
        {
            object result = null;
            foreach(Control item in panel.Controls)
            {
                if(item is RadioButton)
                {
                    if((item as RadioButton).Checked)
                    {
                        result = item.Tag;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取选定复选框的ID
        /// </summary>
        private object GetFileCheckBox(Panel panel)
        {
            int index = 0;
            object id = null;
            foreach(Control item in panel.Controls)
            {
                if(item is CheckBox)
                {
                    CheckBox cb = item as CheckBox;
                    if(cb.Checked)
                    {
                        index++;
                        id = cb.Tag;
                    }
                }
            }
            return index > 1 ? panel.Tag : id;
        }

        /// <summary>
        /// 根据阶段加载类别
        /// </summary>
        private void LoadCategorByStage(object stageValue)
        {
            string querySql = $"SELECT dd_id, dd_name||' '||extend_3 AS dd_name FROM data_dictionary WHERE dd_pId='{stageValue}' ORDER BY dd_sort";
            cbo_categor.DataSource = SQLiteHelper.ExecuteQuery(querySql);
            cbo_categor.DisplayMember = "dd_name";
            cbo_categor.ValueMember = "dd_id";
        }

        /// <summary>
        /// 根据类别加载文件名称
        /// </summary>
        private void LoadFileNameByCategor(ComboBox comboBox)
        {
            object key = comboBox.Text.Split(' ')[0];
            object value = comboBox.SelectedValue;

            object[] fileName = SQLiteHelper.ExecuteSingleColumnQuery($"SELECT fi_name FROM files_info WHERE fi_categor='{value}' AND fi_obj_id='{parentId}'");
            txt_fileName.Items.Clear();
            txt_fileName.Items.AddRange(fileName);
            txt_fileName.Text = GetValue(SQLiteHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{value}'"));

            int amount = SQLiteHelper.ExecuteCountQuery($"SELECT COUNT(fi_id) FROM files_info WHERE fi_categor='{value}' AND fi_obj_id='{parentId}'");
            txt_fileCode.Text = key + "-" + (amount + 1).ToString().PadLeft(2, '0');

        }

        /// <summary>
        /// 阶段下拉切换事件
        /// </summary>
        private void Cbo_stage_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            LoadCategorByStage(cbo_stage.SelectedValue);
            Cbo_categor_SelectedIndexChanged(sender, e);
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
        
        /// <summary>
        /// 文件类别下拉事件
        /// </summary>
        private void Cbo_categor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_categor.SelectedIndex != -1)
            {
                int index = cbo_categor.SelectedIndex;
                int maxIndex = cbo_categor.Items.Count;
                if(index == maxIndex - 1)
                {
                    cbo_categor.Tag = cbo_categor.SelectedValue;
                    cbo_categor.DropDownStyle = ComboBoxStyle.DropDown;
                }
                else
                {
                    cbo_categor.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                LoadFileNameByCategor(cbo_categor);
            }
        }

        //private ExeToWinForm form;

        /// <summary>
        /// 打开文件
        /// </summary>
        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            object[] rootId = SQLiteHelper.ExecuteSingleColumnQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_code = '-1'");
            if(rootId.Length > 0)
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
                        txt_link.Text = fullPath;
                        txt_link.Tag = frm.SelectedFileId;
                        
                        //尝试获取页数
                        try
                        {
                            string format = Path.GetExtension(fullPath).ToLower();
                            if(format.Contains("doc") || format.Contains("docx"))
                                num_page.Value = (int)GetFilePageCount.GetFilePageCountInstince().GetWordPageCount(fullPath);
                            else if(format.Contains("pdf"))
                                num_page.Value = (int)GetFilePageCount.GetFilePageCountInstince().GetPDFPageCount(fullPath);
                        } catch(Exception) { }

                        if(MessageBox.Show("已从服务器拷贝文件到本地，是否现在打开？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("Explorer.exe", filePath);
                            //if(form != null)
                            //    form.Stop();
                            //WindowState = FormWindowState.Maximized;
                            //pal_ShowData.Visible = true;
                            //pal_ShowData.Controls.Clear();

                            //form = new ExeToWinForm(pal_ShowData, string.Empty);
                            //form.Start(fullPath);
                        }
                    }
                    else
                        MessageBox.Show("服务器不存在此文件。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
                MessageBox.Show("当前专项尚未导入数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        /// <summary>
        /// 添加信息到指定表格
        /// </summary>
        private object SaveFileInfo(DataGridViewRow row, bool isAdd)
        {
            object primaryKey = Guid.NewGuid().ToString();
            row.Cells[key + "id"].Value = row.Index + 1;
            row.Cells[key + "stage"].Value = cbo_stage.SelectedValue;
            SetCategorByStage(cbo_stage.SelectedValue, row, key);
            row.Cells[key + "categor"].Value = cbo_categor.SelectedValue ?? cbo_categor.Tag;
            object categorName = cbo_categor.SelectedIndex == cbo_categor.Items.Count - 1 ? cbo_categor.Text : null;
            row.Cells[key + "name"].Value = txt_fileName.Text;
            row.Cells[key + "code"].Value = txt_fileCode.Text;
            row.Cells[key + "user"].Value = txt_user.Text;
            row.Cells[key + "type"].Value = GetFileRadio(pal_type);
            row.Cells[key + "secret"].Value = GetFileRadio(pal_secret);
            row.Cells[key + "pages"].Value = num_page.Value;
            row.Cells[key + "date"].Value = dtp_date.Value.ToString("yyyyMMdd");
            row.Cells[key + "unit"].Value = txt_unit.Text;
            row.Cells[key + "carrier"].Value = GetFileCheckBox(pal_carrier);
            row.Cells[key + "form"].Value = GetFileRadio(pal_form);
            object format = Path.GetExtension(txt_link.Text).Replace(".", string.Empty);
            row.Cells[key + "link"].Value = txt_link.Text;
            if(isAdd)
            {
                object stage = row.Cells[key + "stage"].Value;
                object categor = row.Cells[key + "categor"].Value;
                object code = row.Cells[key + "code"].Value;
                object name = row.Cells[key + "name"].Value;
                object user = row.Cells[key + "user"].Value;
                object type = row.Cells[key + "type"].Value;
                object secret = row.Cells[key + "secret"].Value;
                object pages = row.Cells[key + "pages"].Value;
                DateTime date = DateTime.Now;
                string _date = GetValue(row.Cells[key + "date"].Value);
                if(!string.IsNullOrEmpty(_date))
                {
                    if(_date.Length == 4)
                        _date = _date + "-" + date.Month + "-" + date.Day;
                    else if(_date.Length == 6)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + date.Day;
                    else if(_date.Length == 8)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                    DateTime.TryParse(_date, out date);
                }
                object unit = row.Cells[key + "unit"].Value;
                object carrier = row.Cells[key + "carrier"].Value;
                object form = row.Cells[key + "form"].Value;
                object link = row.Cells[key + "link"].Value;
                object fileId = txt_link.Tag;
                object remark = txt_Remark.Text;
                string insertSql = "INSERT INTO files_info (" +
                "fi_id, fi_code, fi_stage, fi_categor, fi_categor_name, fi_code, fi_name, fi_user, fi_type, fi_secret, fi_pages, fi_create_date, fi_unit, fi_carrier, fi_format, fi_form, fi_link, fi_file_id, fi_obj_id, fi_sort, fi_remark) " +
                $"VALUES( '{primaryKey}', '{code}', '{stage}', '{categor}', '{categorName}', '{code}', '{name}', '{user}', '{type}', '{secret}', '{pages}', '{date.ToString("s")}', '{unit}', '{carrier}', '{format}', '{form}', '{link}', '{fileId}', '{parentId}', '{row.Index}', '{remark}');";
                if(fileId != null)
                {
                    int value = link == null ? 0 : 1;
                    insertSql += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
                }
                SQLiteHelper.ExecuteNonQuery(insertSql);

                row.Cells[key + "id"].Tag = primaryKey;
            }
            else
            {
                primaryKey = row.Cells[key + "id"].Tag;
                object stage = row.Cells[key + "stage"].Value;
                object categor = row.Cells[key + "categor"].Value;
                object code = row.Cells[key + "code"].Value;
                object name = row.Cells[key + "name"].Value;
                object user = row.Cells[key + "user"].Value;
                object type = row.Cells[key + "type"].Value;
                object secret = row.Cells[key + "secret"].Value;
                object pages = row.Cells[key + "pages"].Value;
                DateTime date = DateTime.Now;
                string _date = GetValue(row.Cells[key + "date"].Value);
                if(!string.IsNullOrEmpty(_date))
                {
                    if(_date.Length == 4)
                        _date = _date + "-" + date.Month + "-" + date.Day;
                    else if(_date.Length == 6)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-"+ date.Day;
                    else if(_date.Length == 8)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                    DateTime.TryParse(_date, out date);
                }
                object unit = row.Cells[key + "unit"].Value;
                object carrier = row.Cells[key + "carrier"].Value;
                object form = row.Cells[key + "form"].Value;
                object link = row.Cells[key + "link"].Value;
                object fileId = txt_link.Tag;
                object remark = txt_Remark.Text;
                string updateSql = "UPDATE files_info SET " +
                    $"fi_stage = '{stage}', " +
                    $"fi_categor = '{categor}', " +
                    $"fi_categor_name = '{categorName}', " +
                    $"fi_code = '{code}', " +
                    $"fi_name = '{name}', " +
                    $"fi_user = '{user}', " +
                    $"fi_type = '{type}', " +
                    $"fi_secret = '{secret}', " +
                    $"fi_pages = '{pages}', " +
                    $"fi_create_date = '{date.ToString("s")}', " +
                    $"fi_unit = '{unit}', " +
                    $"fi_carrier = '{carrier}', " +
                    $"fi_format = '{format}', " +
                    $"fi_form = '{form}', " +
                    $"fi_link = '{link}', " +
                    $"fi_remark = '{remark}', " +
                    $"fi_file_id = '{fileId}' " +
                    $"WHERE fi_id = '{primaryKey}';";
                if(fileId != null)
                {
                    int value = link == null ? 0 : 1;
                    updateSql += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
                }
                SQLiteHelper.ExecuteNonQuery(updateSql);
                MessageBox.Show("数据已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return primaryKey;
        }
        
        /// <summary>
        /// 保存(更新)
        /// </summary>
        private void Btn_Save_Add_Click(object sender, EventArgs e)
        {
            if(CheckDatas())
            {
                if(Text.Contains("新增"))
                {
                    fileId = SaveFileInfo(view.Rows[view.Rows.Add()], true);
                    ResetControl();
                }
                else if(Text.Contains("编辑"))
                    UpdateFileInfo();
                WindowState = FormWindowState.Normal;
                //if(form != null)
                //{
                //    form.Stop();
                //    form = null;
                //}
            }
            else
                MessageBox.Show("请检查数据是否正确", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private bool CheckDatas()
        {
            bool result = true;
            //页数
            NumericUpDown pagesCell = num_page;
            if(pagesCell.Value == 0)
            {
                errorProvider1.SetError(pagesCell, "提示：页数不能为0。");
                result = false;
            }
            else
                errorProvider1.SetError(pagesCell, null);
            //文件名
            string nameValue = txt_fileName.Text.Trim();
            if(string.IsNullOrEmpty(nameValue))
            {
                errorProvider1.SetError(txt_fileName, "提示：文件名不能为空。");
                result = false;
            }
            else if(Text.Contains("新增"))
            {
                int _count = SQLiteHelper.ExecuteCountQuery($"SELECT COUNT(fi_id) FROM files_info WHERE fi_name='{nameValue}' AND fi_obj_id='{parentId}'");
                if(_count > 0)
                {
                    errorProvider1.SetError(txt_fileName, "提示：文件名已存在，请重新输入。");
                    result = false;
                }
                else
                    errorProvider1.SetError(txt_fileName, null);
            }
            //编号
            if(string.IsNullOrEmpty(txt_fileCode.Text.Trim()))
            {
                errorProvider1.SetError(txt_fileCode, "提示：编号不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(txt_fileCode, null);
            //文件类型
            int count = 0;
            foreach(RadioButton item in pal_type.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_type, "提示：文件类型不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(pal_type, null);
            //密级
            count = 0;
            foreach(RadioButton item in pal_secret.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_secret, "提示：密级不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(pal_secret, null);
            //载体
            count = 0;
            foreach(CheckBox item in pal_carrier.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_carrier, "提示：载体不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(pal_carrier, null);
            //形态
            count = 0;
            foreach(RadioButton item in pal_form.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_form, "提示：文件形态不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(pal_form, null);

            //存放单位
            if(string.IsNullOrEmpty(txt_unit.Text.Trim()))
            {
                errorProvider1.SetError(txt_unit, "提示：存放单位不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(txt_unit, null);
            return result;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private void UpdateFileInfo()
        {
            foreach(DataGridViewRow row in view.Rows)
            {
                if(fileId.Equals(row.Cells[key + "id"].Tag))
                {
                    SaveFileInfo(row, false);
                    break;
                }
            }
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, object key)
        {
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;
            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SQLiteHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = categorCell.Items[0] };
        }
        
        /// <summary>
        /// 重置控件
        /// </summary>
        private void ResetControl()
        {
            foreach(Control item in Controls)
            {
                if(!(item is Label))
                {
                    if(item is TextBox || item is DateTimePicker)
                        item.ResetText();
                    else if(item is NumericUpDown)
                        (item as NumericUpDown).Value = 0;
                    else if(item is ComboBox)
                    {
                        if(!item.Name.Equals("cbo_stage") && !item.Name.Equals("txt_fileName"))
                        {
                            (item as ComboBox).SelectedIndex = 0;
                        }
                    }
                }
            }
            txt_unit.Text = UserHelper.GetUser().UserUnitName;
        }

        private void Frm_AddFile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Enter)
            {
                Btn_Save_Add_Click(null, null);
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            ResetControl();
            if(Text.Contains("编辑"))
                LoadFileInfo(fileId);
        }

        private void btn_Quit_Click(object sender, EventArgs e)
        {
            if(fileId == null)
            {
                if(MessageBox.Show("尚未保存当前数据，是否确认退出？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    Close();
            }
            else
                Close();
        }

        private void txt_fileName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object fileName = txt_fileName.SelectedItem;
            if(fileName != null)
            {
                Hide();
                DataRow row = SQLiteHelper.ExecuteSingleRowQuery($"SELECT fi_id FROM files_info WHERE fi_name='{fileName}'");
                new Frm_AddFile(view, key, row["fi_id"]).ShowDialog();
            }
        }
    }
}
