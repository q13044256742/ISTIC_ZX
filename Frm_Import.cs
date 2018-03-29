using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Import : Form
    {
        /// <summary>
        /// 共计文件数
        /// </summary>
        private int count = 0;
        /// <summary>
        /// 导入成功数
        /// </summary>
        private int okCount = 0;
        /// <summary>
        /// 导入失败输
        /// </summary>
        private int noCount = 0;
        int indexCount = 0;
        public Frm_Import()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
      
        /// <summary>
        /// 路径选择
        /// </summary>
        private void btn_Import_Click(object sender, EventArgs e)
        {
            if(fbd_Data.ShowDialog() == DialogResult.OK)
            {
                txt_FilePath.Text = fbd_Data.SelectedPath;
                pro_Show.Value = pro_Show.Minimum;
            }
        }

        /// <summary>
        /// 开始读取
        /// </summary>
        private void btn_Import_Click_1(object sender, EventArgs e)
        {
            string sPath = txt_FilePath.Text;
            if(!string.IsNullOrEmpty(sPath))
            {
                if(SQLiteHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM backup_files_info WHERE bfi_name='{UserHelper.GetUser().RealName}' AND bfi_code='-1'") == 0)
                {
                    object IPAddress = null;
                    if(ServerHelper.GetConnectState(ref IPAddress))
                    {
                        string rootFolder = @"\\" + IPAddress + @"\共享文件夹\" + UserHelper.GetUser().RealName + @"\";
                        if(Directory.Exists(rootFolder))
                        {
                            if(MessageBox.Show("服务器已存在导入数据，此操作会删除现有文件，是否继续？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                Directory.Delete(rootFolder, true);
                            else
                                return;
                        }
                        btn_Import.Enabled = false;
                        count = okCount = noCount = indexCount = 0;
                        int totalFileAmount = Directory.GetFiles(sPath, "*", SearchOption.AllDirectories).Length;
                        pro_Show.Value = pro_Show.Minimum;
                        pro_Show.Maximum = totalFileAmount;

                        Directory.CreateDirectory(rootFolder);
                        string primaryKey = Guid.NewGuid().ToString();
                        SQLiteHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_code, bfi_name, bfi_date, bfi_userid) VALUES " +
                            $"('{primaryKey}', '{-1}', '{UserHelper.GetUser().RealName}', '{DateTime.Now.ToString("s")}', '{UserHelper.GetUser().UserId}')");
                        new Thread(delegate ()
                        {
                            CopyFile(sPath, rootFolder, primaryKey);

                            CopyDataTable(sPath);

                            MessageBox.Show($"读取完毕,共计{count}个文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            DialogResult = DialogResult.OK;
                            Close();
                            Thread.CurrentThread.Abort();
                        }).Start();
                    }
                    else
                        MessageBox.Show("访问备份服务器失败。", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(MessageBox.Show("当前专项已导入,是否删除当前数据记录。", "操作失败", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    SQLiteHelper.ExecuteNonQuery($"DELETE FROM backup_files_info WHERE bfi_userid='{UserHelper.GetUser().UserId}'");
                    MessageBox.Show("删除完毕，重新导入即可。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        /// <summary>
        /// 拷贝数据库
        /// 【课题组 >> 专项办】
        /// </summary>
        /// <param name="rootFolder">课题组数据库文件路径</param>
        private void CopyDataTable(string rootFolder)
        {
            if(Directory.Exists(rootFolder))
            {
                string[] files = Directory.GetFiles(rootFolder, "*.db");
                if(files != null && files.Length > 0)
                {
                    FileInfo fileInfo = new FileInfo(files[0]);
                    for(int i = 1; i < files.Length; i++)
                    {
                        FileInfo _fileInfo = new FileInfo(files[i]);
                        if(_fileInfo.LastWriteTime > fileInfo.LastWriteTime)
                            fileInfo = _fileInfo;
                    }
                    CopyDataTableInstince(fileInfo.FullName);
                }
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        private void CopyDataTableInstince(string dataBasePath)
        {
            SQLiteBackupHelper helper = new SQLiteBackupHelper(dataBasePath);

            DataTable projectTable = helper.ExecuteQuery($"SELECT * FROM project_info");
            for(int i = 0; i < projectTable.Rows.Count; i++)
            {
                DataRow row = projectTable.Rows[i];
                string insertSql = "INSERT INTO project_info VALUES(" +
                    $"'{row["pi_id"]}', '{row["pi_code"]}', '{row["pi_name"]}', '{row["pi_field"]}', '{row["pi_theme"]}', '{row["pi_funds"]}', '{GetFormatDate(row["pi_startdate"])}', '{GetFormatDate(row["pi_finishdate"])}', " +
                    $"'{row["pi_year"]}', '{row["pi_unit"]}', '{row["pi_province"]}', '{row["pi_unit_user"]}', '{row["pi_project_user"]}', '{row["pi_contacts"]}', '{row["pi_contacts_phone"]}', '{row["pi_introduction"]}', '{row["pi_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM project_info WHERE pi_id='{row["pi_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable topicTable = helper.ExecuteQuery($"SELECT * FROM topic_info");
            for(int i = 0; i < topicTable.Rows.Count; i++)
            {
                DataRow row = topicTable.Rows[i];
                string insertSql = "INSERT INTO topic_info VALUES(" +
                    $"'{row["ti_id"]}', '{row["ti_code"]}', '{row["ti_name"]}', '{row["ti_field"]}', '{row["ti_theme"]}', '{row["ti_funds"]}', '{GetFormatDate(row["ti_startdate"])}', '{GetFormatDate(row["ti_finishdate"])}'," +
                    $"'{row["ti_year"]}', '{row["ti_unit"]}', '{row["ti_province"]}', '{row["ti_unit_user"]}', '{row["ti_project_user"]}', '{row["ti_contacts"]}', '{row["ti_contacts_phone"]}', '{ row["ti_introduction"]}', '{row["ti_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM topic_info WHERE ti_id='{row["ti_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable subjectTable = helper.ExecuteQuery($"SELECT * FROM subject_info");
            for(int i = 0; i < subjectTable.Rows.Count; i++)
            {
                DataRow row = subjectTable.Rows[i];
                string insertSql = "INSERT INTO subject_info VALUES(" +
                    $"'{row["si_id"]}', '{row["si_code"]}', '{row["si_name"]}', '{row["si_field"]}', '{row["si_theme"]}', '{row["si_funds"]}', '{GetFormatDate(row["si_startdate"])}', '{GetFormatDate(row["si_finishdate"])}'," +
                    $"'{row["si_year"]}', '{row["si_unit"]}', '{row["si_province"]}', '{row["si_unit_user"]}', '{row["si_project_user"]}', '{row["si_contacts"]}', '{row["si_contacts_phone"]}', '{row["si_introduction"]}', '{row["si_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM subject_info WHERE si_id='{row["si_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable fileTable = helper.ExecuteQuery($"SELECT * FROM files_info");
            for(int i = 0; i < fileTable.Rows.Count; i++)
            {
                DataRow row = fileTable.Rows[i];
                string insertSql = "INSERT INTO files_info VALUES(" +
                    $"'{row["fi_id"]}', '{row["fi_code"]}', '{row["fi_stage"]}', '{row["fi_categor"]}', '{row["fi_name"]}', '{row["fi_user"]}', '{row["fi_type"]}', '{row["fi_secret"]}', '{row["fi_pages"]}'," +
                    $"'{row["fi_number"]}', '{GetFormatDate(row["fi_create_date"])}', '{row["fi_unit"]}', '{row["fi_carrier"]}', '{row["fi_format"]}', '{row["fi_form"]}', '{row["fi_link"]}', '{row["fi_status"]}', '{row["fi_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM files_info WHERE fi_id='{row["fi_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable lostTable = helper.ExecuteQuery($"SELECT * FROM files_lost_info");
            for(int i = 0; i < lostTable.Rows.Count; i++)
            {
                DataRow row = lostTable.Rows[i];
                string insertSql = $"INSERT INTO files_lost_info VALUES('{row["pfo_id"]}', '{row["pfo_categor"]}', '{row["pfo_name"]}', '{row["pfo_reason"]}', '{row["pfo_remark"]}', '{row["pfo_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM files_lost_info WHERE pfo_id='{row["pfo_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable tagTable = helper.ExecuteQuery($"SELECT * FROM files_tag_info");
            for(int i = 0; i < tagTable.Rows.Count; i++)
            {
                DataRow row = tagTable.Rows[i];
                string insertSql = $"INSERT INTO files_tag_info VALUES('{row["pt_id"]}', '{row["pt_code"]}', '{row["pt_name"]}', '{row["pt_term"]}', '{row["pt_secret"]}', '{row["pt_user"]}', '{row["pt_unit"]}', '{row["pt_obj_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM files_tag_info WHERE pt_id='{row["pt_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }

            DataTable boxTable = helper.ExecuteQuery($"SELECT * FROM files_box_info");
            for(int i = 0; i < boxTable.Rows.Count; i++)
            {
                DataRow row = boxTable.Rows[i];
                string insertSql = $"INSERT INTO files_box_info VALUES('{row["pb_id"]}', '{row["pb_box_number"]}', '{row["pb_gc_id"]}', '{row["pb_files_id"]}', '{row["pb_obj_id"]}', '{row["pb_unit_id"]}')";
                SQLiteHelper.ExecuteNonQuery($"DELETE FROM files_box_info WHERE pb_id='{row["pb_id"]}'");
                SQLiteHelper.ExecuteNonQuery(insertSql);
            }
        }

        private object GetFormatDate(object v) => v == null ? DateTime.Now.ToString("s") : Convert.ToDateTime(v).ToString("s");

        /// <summary>
        /// 拷贝文件到备份服务器
        /// </summary>
        /// <param name="sPath">源文件夹路径</param>
        /// <param name="rootFolder">目标文件夹基路径</param>
        private void CopyFile(string sPath, string rootFolder, string pid)
        {
            DirectoryInfo info = new DirectoryInfo(sPath);
            FileInfo[] file = info.GetFiles();
            count += file.Length;
            for(int i = 0; i < file.Length; i++)
            {
                string primaryKey = Guid.NewGuid().ToString();
                try
                {
                    SQLiteHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_code, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid) VALUES " +
                        $"('{primaryKey}', '{indexCount++.ToString().PadLeft(6, '0')}', '{file[i].Name}', '{rootFolder}', '{DateTime.Now.ToString("s")}', '{pid}', '{UserHelper.GetUser().UserId}')");
                    ServerHelper.UploadFile(file[i].FullName, rootFolder, file[i].Name);
                    okCount++;
                }
                catch(Exception)
                {
                    noCount++;
                }
                pro_Show.Value += 1;
            }
            DirectoryInfo[] infos = info.GetDirectories();
            for(int i = 0; i < infos.Length; i++)
            {
                string primaryKey = Guid.NewGuid().ToString();
                SQLiteHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_code, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid) VALUES " +
                        $"('{primaryKey}', '{indexCount++.ToString().PadLeft(6, '0')}', '{infos[i].Name}', '{rootFolder}', '{DateTime.Now.ToString("s")}', '{pid}', '{UserHelper.GetUser().UserId}')");
                CopyFile(infos[i].FullName, rootFolder + infos[i].Name + @"\", primaryKey);
            }
        }

        private void Frm_Import_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(okCount + noCount != count)
            {
                MessageBox.Show("请等待导入完毕,中途退出会导致数据错误。", "无法关闭", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.Cancel = true;
            }
        }
    }
}
