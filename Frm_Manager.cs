using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using 数据采集档案管理系统___加工版.Properties;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Manager : Form
    {
        public Frm_Manager()
        {
            InitializeComponent();
        }

        private void Frm_Manager_Load(object sender, System.EventArgs e)
        {

            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.AddRange(new CreateKyoPanel.KyoPanel[]
            {
                new CreateKyoPanel.KyoPanel
                {
                    Name = "userManager",
                    Text = "用户信息管理",
                    Image = Resources.png_0222
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name = "unitManager",
                    Text = "专项基本信息",
                    Image = Resources.png_0228
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name = "dictionaryManage",
                    Text = "字典管理",
                    Image = Resources.png_0289
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name = "setContextPath",
                    Text = "指定全文路径",
                    Image = Resources.png_0281
                }
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list, LeftMenu_Click);

            CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("dictionaryManage", false)[0] as Panel, new List<CreateKyoPanel.KyoPanel>()
            {
                new CreateKyoPanel.KyoPanel()
                {
                    Name = "dic_plan",
                    Text = "计划字典"
                },
                new CreateKyoPanel.KyoPanel()
                {
                    Name = "dic_file",
                    Text = "文件字典"
                },
                new CreateKyoPanel.KyoPanel()
                {
                    Name = "dic_unit",
                    Text = "单位字典"
                },
                new CreateKyoPanel.KyoPanel()
                {
                    Name = "dic_normal",
                    Text = "标准字典"
                }
            }, Sub_Menu_Click);

            //默认打开用户管理
            new Frm_UserManage { MdiParent = this }.Show();
        }

        private void Sub_Menu_Click(object sender, System.EventArgs e)
        {
            Control control = null;
            if(sender is Panel)
                control = sender as Control;
            else
                control = (sender as Control).Parent;
            foreach(Form item in MdiChildren)
                item.Close();
            string key = null;
            if("dic_plan".Equals(control.Name))
                key = "D752F90E-A5BC-4C4F-91FD-C4EA250B61DA";
            else if("dic_file".Equals(control.Name))
                key = "08C7AD07-F7D0-4EAC-AA03-1155351C9B3D";
            else if("dic_unit".Equals(control.Name))
                key = "421E381F-237C-4395-A08B-0E20435AE91B";
            else if("dic_normal".Equals(control.Name))
                key = "19B6FF50-3C10-4B19-9C34-7EE25FA0996B";
            new Frm_DictionaryManage(key) { MdiParent = this }.Show();
        }

        private void LeftMenu_Click(object sender, System.EventArgs e)
        {
            Control control = null;
            if (sender is Panel)
                control = sender as Control;
            else
                control = (sender as Control).Parent;
            foreach(Form item in MdiChildren)
                item.Close();
            if ("userManager".Equals(control.Name))
            {
                foreach (Form item in MdiChildren)
                    if(item is Frm_UserManage)
                    {
                        item.Activate();
                        item.WindowState = FormWindowState.Maximized;
                        return;
                    }
                new Frm_UserManage { MdiParent = this }.Show();
            }
            else if ("unitManager".Equals(control.Name))
            {
                foreach (Form item in MdiChildren)
                    if (item is Frm_UnitManage)
                    {
                        item.Activate();
                        item.WindowState = FormWindowState.Maximized;
                        return;
                    }
                new Frm_UnitManage(null) { MdiParent = this }.Show();
            }
            else if ("setContextPath".Equals(control.Name))
            {
                foreach (Form item in MdiChildren)
                    if (item is Frm_SetContextPath)
                    {
                        item.Activate();
                        item.WindowState = FormWindowState.Normal;
                        return;
                    }
                new Frm_SetContextPath { MdiParent = this }.Show();
            }
            //else if("dictionaryManage".Equals(control.Name))
            //{
            //    foreach(Form item in MdiChildren)
            //    {
            //        if(item is Frm_DictionaryManage)
            //        {
            //            item.Activate();
            //            item.WindowState = FormWindowState.Maximized;
            //            return;
            //        }
            //    }
            //    new Frm_DictionaryManage() { MdiParent = this }.Show();
            //}
        }

        private void Frm_Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}                       
