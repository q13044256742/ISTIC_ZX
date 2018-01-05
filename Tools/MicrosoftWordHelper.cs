using System;
using MSWord = Microsoft.Office.Interop.Word;
using System.IO;
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
            MSWord.Application wordApp = new MSWord.ApplicationClass();
            MSWord.Document wordDoc;

            if (File.Exists((string)filePath))
                File.Delete((string)filePath);

            //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            Object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);

            #region 页面设置、页眉图片和文字设置，最后跳出页眉设置

            //页面设置
            wordDoc.PageSetup.PaperSize = MSWord.WdPaperSize.wdPaperA4;//设置纸张样式为A4纸
            wordDoc.PageSetup.Orientation = MSWord.WdOrientation.wdOrientPortrait;//排列方式为垂直方向

            //设置页眉
            wordApp.ActiveWindow.View.Type = MSWord.WdViewType.wdNormalView;//普通视图（即页面视图）样式

            #endregion

            #region 页码设置并添加页码

            //为当前页添加页码
            MSWord.PageNumbers pns = wordApp.Selection.Sections[1].Headers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;//获取当前页的号码
            pns.NumberStyle = MSWord.WdPageNumberStyle.wdPageNumberStyleNumberInDash;//设置页码的风格，是Dash形还是圆形的
            pns.HeadingLevelForChapter = 0;
            pns.IncludeChapterNumber = false;
            pns.RestartNumberingAtSection = false;
            pns.StartingNumber = 0; //开始页页码？
            object pagenmbetal = MSWord.WdPageNumberAlignment.wdAlignPageNumberCenter;//将号码设置在中间
            object first = true;
            wordApp.Selection.Sections[1].Footers[MSWord.WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref pagenmbetal, ref first);

            #endregion
            wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
            wordApp.Selection.ParagraphFormat.FirstLineIndent = 30;//首行缩进的长度

            //写入普通文本
            for (int i = 0; i < contexts.Length; i++)
            {
                wordDoc.Paragraphs.Last.Range.Text = contexts[i];
            }

            wordApp.NormalTemplate.Saved = true;
            //WdSaveFormat为docx文档的保存格式
            object format = MSWord.WdSaveFormat.wdFormatDocumentDefault;
            wordDoc.SaveAs(ref filePath, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            wordDoc.Close(Type.Missing, Type.Missing, Type.Missing);
            wordApp.Quit(Type.Missing, Type.Missing, Type.Missing);

            if (MessageBox.Show(filePath + " 合成完毕, 是否需要现在打开?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                MSWord.Application app = new MSWord.Application();
                MSWord.Document doc = null;
                try
                {
                    object unknow = Type.Missing;
                    app.Visible = true;
                    object file = filePath;
                    doc = app.Documents.Open(ref file,
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
}
