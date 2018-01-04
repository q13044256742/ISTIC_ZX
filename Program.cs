using System;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Login());
            //Application.Run(new Frm_Main());
        }
    }
}
