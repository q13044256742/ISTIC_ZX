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
    public partial class Frm_IdentityChoose : Form
    {
        public object Identity;

        public Frm_IdentityChoose()
        {
            InitializeComponent();
        }

        private void Frm_IdentityChoose_Load(object sender, EventArgs e)
        {
            Identity = null;
            cbo_ChooseIdentity.Items.AddRange(new string[]
            {
                "核高基","集成电路","宽带移动","数控机床","油气","核电","水专项","转基因","新药","传染病"
            });
            cbo_ChooseIdentity.SelectedIndex = 0;
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            Identity = cbo_ChooseIdentity.SelectedItem;
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void Frm_IdentityChoose_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("请选择您的身份");
            e.Cancel = true;
        }
    }
}
