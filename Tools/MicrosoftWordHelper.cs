using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace 数据采集档案管理系统___加工版.Tools
{
    class MicrosoftWordHelper
    {
        /// <summary>
        /// 向指定Word中写入指定文本
        /// </summary>
        /// <param name="filePath">Word 所在路径</param>
        /// <param name="contexts">所需写入的内容</param>
        public static void WriteDocument(object filePath, string[] contexts)
        {
            Microsoft.Office.Interop.Word.Application app = null;
            Microsoft.Office.Interop.Word.Document doc = null;

            if (File.Exists((string)filePath))
                File.Delete((string)filePath);

            try
            {
                //构造数据
                List<EntityObject> datas = new List<EntityObject>();
                datas.Add(new EntityObject { Code = "2012AA103100", Name = "国家信息体系建设研究", User = "崔文健", Type = 0, PageSize = 1, FileAmount = 10, Date = DateTime.Now });
                datas.Add(new EntityObject { Code = "2012AA103100", Name = "国家信息体系建设研究", User = "崔文健", Type = 0, PageSize = 1, FileAmount = 10, Date = DateTime.Now });
                datas.Add(new EntityObject { Code = "2012AA103100", Name = "国家信息体系建设研究", User = "崔文健", Type = 0, PageSize = 1, FileAmount = 10, Date = DateTime.Now });
                datas.Add(new EntityObject { Code = "2012AA103100", Name = "国家信息体系建设研究", User = "崔文健", Type = 0, PageSize = 1, FileAmount = 10, Date = DateTime.Now });
                datas.Add(new EntityObject { Code = "2012AA103100", Name = "国家信息体系建设研究", User = "崔文健", Type = 0, PageSize = 1, FileAmount = 10, Date = DateTime.Now });

                int rows = datas.Count() + 1;//表格行数加1是为了标题栏
                int cols = 7;//表格列数
                object oMissing = Missing.Value;
                app = new Microsoft.Office.Interop.Word.Application();//创建word应用程序
                doc = app.Documents.Add();//添加一个word文档

                //输出大标题加粗加大字号水平居中
                app.Selection.Font.Bold = 700;
                app.Selection.Font.Size = 16;
                app.Selection.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                app.Selection.Text = "项目结构表";

                //换行添加表格
                object line = Microsoft.Office.Interop.Word.WdUnits.wdLine;
                app.Selection.MoveDown(ref line, oMissing, oMissing);
                app.Selection.TypeParagraph();//换行

                app.Selection.PageSetup.LeftMargin = 50f;
                app.Selection.PageSetup.RightMargin = 50f;
                app.Selection.PageSetup.PageWidth = 650f;  //页面宽度


                Microsoft.Office.Interop.Word.Range range = app.Selection.Range;
                Microsoft.Office.Interop.Word.Table table = app.Selection.Tables.Add(range, rows, cols, ref oMissing, ref oMissing);

                //设置表格的字体大小粗细
                table.Range.Font.Size = 10;
                table.Range.Font.Bold = 0;
                table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

                //设置表格标题
                int rowIndex = 1;
                table.Cell(rowIndex, 1).Range.Text = "重大专项项目（课题）档案材料/项目（课题）名称";
                table.Cell(rowIndex, 2).Range.Text = "承担单位/责任者";
                table.Cell(rowIndex, 3).Range.Text = "载体类型";
                table.Cell(rowIndex, 4).Range.Text = "页数";
                table.Cell(rowIndex, 5).Range.Text = "文件数";
                table.Cell(rowIndex, 6).Range.Text = "日期";
                table.Cell(rowIndex, 7).Range.Text = "备注";

                //循环数据创建数据行
                foreach (EntityObject eo in datas)
                {
                    rowIndex++;
                    table.Cell(rowIndex, 1).Range.Text = eo.Name;
                    table.Cell(rowIndex, 2).Range.Text = eo.User;
                    table.Cell(rowIndex, 3).Range.Text = eo.Type == 0 ? "电子" : "纸质";
                    table.Cell(rowIndex, 4).Range.Text = eo.PageSize.ToString();
                    table.Cell(rowIndex, 5).Range.Text = eo.FileAmount.ToString();
                    table.Cell(rowIndex, 6).Range.Text = eo.Date.ToLocalTime().ToString();
                    table.Cell(rowIndex, 7).Range.Text = eo.Remark;

                    //对表格中的班级、姓名，成绩单元格设置上下居中
                    table.Cell(rowIndex, 1).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    table.Cell(rowIndex, 4).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    table.Cell(rowIndex, 5).VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                }

                //导出到文件
                filePath += DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                doc.SaveAs(filePath,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();//关闭文档
                }
                if (app != null)
                {
                    app.Quit();//退出应用程序
                }
            }

            if (MessageBox.Show(filePath + " 合成完毕, 是否需要现在打开?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                Microsoft.Office.Interop.Word.Application _app = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document _doc = null;
                try
                {
                    object unknow = Type.Missing;
                    _app.Visible = true;
                    object file = filePath;
                    _doc = _app.Documents.Open(ref file,
                        ref unknow, ref unknow, ref unknow, ref unknow,
                        ref unknow, ref unknow, ref unknow, ref unknow,
                        ref unknow, ref unknow, ref unknow, ref unknow,
                        ref unknow, ref unknow, ref unknow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
    class EntityObject
    {
        private string code;
        private string name;
        private string user;
        private int type;
        private int pageSize;
        private int fileAmount;
        private DateTime date;
        private string remark;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string User { get => user; set => user = value; }
        public int Type { get => type; set => type = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public int FileAmount { get => fileAmount; set => fileAmount = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Remark { get => remark; set => remark = value; }
    }
}
