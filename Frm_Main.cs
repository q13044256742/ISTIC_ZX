using System;
using System.Drawing;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void btn_WorkLog_Click(object sender, EventArgs e)
        {
            Frm_WorkLog frm_WorkLog = new Frm_WorkLog();
            frm_WorkLog.MdiParent = this;
            frm_WorkLog.Show();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if(item is MdiClient)
                {
                    item.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void btn_MyWork_Click(object sender, EventArgs e)
        {
            Frm_MyWork frm_MyWork = new Frm_MyWork();
            frm_MyWork.MdiParent = this;
            frm_MyWork.Show();
        }
    }
}
