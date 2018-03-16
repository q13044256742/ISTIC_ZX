﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Properties;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_AddFile_FileSelect : Form
    {
        public string SelectedFileName;
        private ImageList imageList;
        public Frm_AddFile_FileSelect(object rootId)
        {
            InitializeComponent();
            object[] objs = SQLiteHelper.ExecuteRowsQuery($"SELECT bfi_id, bfi_name, bfi_path FROM backup_files_info WHERE bfi_id='{rootId}'");
            TreeNode treeNode = new TreeNode()
            {
                Name = GetValue(objs[0]),
                Text = GetValue(objs[1]),
                Tag = GetValue(objs[2])
            };
            treeNode.Expand();
            tv_file.Nodes.Add(treeNode);
            InitialTree(rootId, treeNode);
        }

        private string GetValue(object v) => v == null ? string.Empty : v.ToString();

        private void InitialTree(object parentId, TreeNode parentNode)
        {
            List<object[]> list = SQLiteHelper.ExecuteColumnsQuery($"SELECT bfi_id, bfi_name, bfi_path FROM backup_files_info WHERE bfi_pid='{parentId}' ORDER BY bfi_code DESC", 3);
            for(int i = 0; i < list.Count; i++)
            {
                TreeNode treeNode = new TreeNode()
                {
                    Name = GetValue(list[i][0]),
                    Text = GetValue(list[i][1]),
                    Tag = GetValue(list[i][2])
                };
                parentNode.Nodes.Add(treeNode);
                InitialTree(treeNode.Name, treeNode);
            }
            if(list.Count == 0)
                parentNode.ImageIndex = parentNode.SelectedImageIndex = 2;
        }

        private void Frm_AddFile_FileSelect_Load(object sender, EventArgs e)
        {
            imageList = new ImageList();
            //0：文件夹关闭 1：文件夹打开 2：文件
            imageList.Images.AddRange(new System.Drawing.Image[] {
                Resources._33, Resources._34, Resources._7
            });
            tv_file.ImageList = imageList;
            tv_file.ImageIndex = 1;
        }


        private void tv_file_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lbl_filename.Text = e.Node.Text;
            SelectedFileName = e.Node.Tag + e.Node.Text;
        }

        private void btn_sure_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}