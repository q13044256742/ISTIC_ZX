using System.Collections.Generic;
using System.Drawing;
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
                    Name = "setContextPath",
                    Text = "指定全文路径",
                    Image = Resources.png_0281
                }
             
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list, LeftMenu_Click);
        }

        private void LeftMenu_Click(object sender, System.EventArgs e)
        {
            Control control = null;
            if (sender is Panel)
                control = sender as Control;
            else
                control = (sender as Control).Parent;
            foreach (Form item in MdiChildren)
                item.WindowState = FormWindowState.Minimized;
            if ("userManager".Equals(control.Name))
            {
                foreach (Form item in MdiChildren)
                    if(item is Frm_UserManager)
                    {
                        item.Activate();
                        item.WindowState = FormWindowState.Normal;
                        return;
                    }
                new Frm_UserManager { MdiParent = this }.Show();
            }
            else if ("unitManager".Equals(control.Name))
            {
                foreach (Form item in MdiChildren)
                    if (item is Frm_UnitManage)
                    {
                        item.Activate();
                        item.WindowState = FormWindowState.Normal;
                        return;
                    }
                new Frm_UnitManage { MdiParent = this }.Show();
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
        }

        private void Frm_Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}                       
