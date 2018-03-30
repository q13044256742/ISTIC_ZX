﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版
{
    class DataGridViewStyleHelper
    {
        /// <summary>
        /// 默认表头字体大小
        /// </summary>
        private static float DefaultHeaderFontSize = 10f;
        /// <summary>
        /// 默认单元格字体大小
        /// </summary>
        private static float DefaultCellFontSize = 9f;

        /// <summary>
        /// 获取DataGridView默认表头样式
        /// </summary>
        /// <returns></returns>
        public static DataGridViewCellStyle GetHeaderStyle()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.Font = new System.Drawing.Font("微软雅黑", DefaultHeaderFontSize, System.Drawing.FontStyle.Bold);
            return style;
        }

        /// <summary>
        /// 设置指定列的宽度
        /// </summary>
        /// <param name="indexs">二维数组，指定列和指定宽度</param>
        public static void SetWidth(DataGridView dataGridView, List<KeyValuePair<int,int>> keyValue)
        {
            for (int i = 0; i < keyValue.Count; i++)
                dataGridView.Columns[keyValue[i].Key].Width = keyValue[i].Value;
        }

        /// <summary>
        /// 设置指定列的值为可点击样式
        /// </summary>
        /// <param name="indexs">指定列的列数（从0开始）</param>
        public static void SetLinkStyle(DataGridView dataGridView, int[] indexs)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new System.Drawing.Font("宋体", DefaultCellFontSize);
            style.ForeColor = System.Drawing.Color.Blue;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int j = 0; j < indexs.Length; j++)
            {
                dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
                dataGridView.Columns[indexs[j]].ReadOnly = true;
            }
        }

        /// <summary>
        /// 设置单元格文本对齐方式为居中
        /// </summary>
        /// <param name="indexs">指定列的列数（从0开始）</param>
        public static void SetAlignWithCenter(DataGridView dataGridView, int[] indexs)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int j = 0; j < indexs.Length; j++)
                dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
        }

        /// <summary>
        /// 设置单元格文本对齐方式为居中
        /// </summary>
        public static void SetAlignWithCenter(DataGridView dataGridView, string[] indexs)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for(int j = 0; j < indexs.Length; j++)
                dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
        }
    }
}
