﻿using System;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_UserManager : Form
    {
        public Frm_UserManager()
        {
            InitializeComponent();
        }

        private void Frm_UserManager_Load(object sender, EventArgs e)
        {
            cbo_UserList_SearchType.SelectedIndex = 0;
        }
    }
}
