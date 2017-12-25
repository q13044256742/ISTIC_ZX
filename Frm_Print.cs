using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    public partial class Frm_Print : Form
    {
        public Frm_Print()
        {
            InitializeComponent();
        }

        private void Frm_Print_Load(object sender, EventArgs e)
        {
            for (int i = 2; i <= 8; i++)
                cbo_PrintSize.Items.Add(i + "0mm");
            cbo_PrintSize.SelectedIndex = 0;
        }

        private void cbo_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            cbo_BKB.Checked = cbo_FM.Checked = cbo_JNWJ.Checked = cbo_CheckAll.Checked;
        }
    }
}
