using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Tools;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Export : Form
    {
        private List<DataRow> list;

        public Frm_Export()
        {
            InitializeComponent();
            list = new List<DataRow>();
        }

        private void Frm_Export_Load(object sender, EventArgs e)
        {
            LoadFileInfo(UserHelper.GetUser().UserSpecialId);
            List<object[]> projectIds = SQLiteHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{UserHelper.GetUser().UserSpecialId}'", 1);
            for(int i = 0; i < projectIds.Count; i++)
            {
                LoadFileInfo(projectIds[i][0]);
                List<object[]> pro_top_ids = SQLiteHelper.ExecuteColumnsQuery($"SELECT ti_id FROM topic_info WHERE ti_obj_id='{projectIds[i][0]}'", 1);
                for(int j = 0; j < pro_top_ids.Count; j++)
                {
                    LoadFileInfo(pro_top_ids[j][0]);
                    List<object[]> pro_top_sub_ids = SQLiteHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{pro_top_ids[j][0]}'", 1);
                    for(int k = 0; k < pro_top_sub_ids.Count; k++)
                        LoadFileInfo(pro_top_sub_ids[k][0]);
                }
            }
            List<object[]> topicIds = SQLiteHelper.ExecuteColumnsQuery($"SELECT ti_id FROM topic_info WHERE ti_obj_id='{UserHelper.GetUser().UserSpecialId}'", 1);
            for(int i = 0; i < topicIds.Count; i++)
            {
                LoadFileInfo(topicIds[i][0]);
                List<object[]> pro_top_sub_ids = SQLiteHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{topicIds[i][0]}'", 1);
                for(int k = 0; k < pro_top_sub_ids.Count; k++)
                    LoadFileInfo(pro_top_sub_ids[k][0]);
            }

            pro_Show.Maximum = list.Count;
        }

        private void LoadFileInfo(object pid)
        {
            DataTable table = SQLiteHelper.ExecuteQuery($"SELECT * FROM files_info WHERE fi_obj_id='{pid}'");
            list.AddRange(GetArray(table));
        }

        private DataRow[] GetArray(DataTable table)
        {
            DataRow[] _row = new DataRow[table.Rows.Count];
            table.Rows.CopyTo(_row, 0);
            return _row;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            Object filename = "合成word示例.docx";
            Object filefullname = Application.StartupPath + "\\" + filename;

            MicrosoftWordHelper.WriteDocument(filefullname.ToString(), list, pro_Show);
        }
    }
}
