using AiWand.Core.DTO.CodeDocument;
using AiWand.Core.Office;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AiWand.Service.CodeDocument
{
    public class CodeDocumentService : ICodeDocumentService
    {
        /// <summary>
        /// 生成代码文档
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns>文档连接</returns>
        public void Build(BuildInput input)
        {
            //判断是否已经生成文档

            //读取代码所在的路径
            string path = @"D:\Work\Zhidian\project\dev-ams102\wholesale";
            StringBuilder contents = new StringBuilder() ;
            if (Directory.Exists(path))
            {
                List<string> files = GetFile(path);
                List<string> fileTexts = new List<string>();
                foreach (var file in files)
                {
                    fileTexts.AddRange(File.ReadAllLines(file));

                    if (fileTexts.Count > 10000)
                    {
                        foreach (var fileText in fileTexts)
                        {
                            contents.AppendLine(fileText);
                        }
                        break;
                    }
                }
                if (contents.Length == 0)
                {
                    foreach (var fileText in fileTexts)
                    {
                        contents.AppendLine(fileText);
                    }
                }
            }

            //生成文档

            //保存文档信息
            string fileName = $"{input.CompanyName}[{input.CompanyAbb}]-({input.Language})";
            WordTestV2(fileName, input.CompanyName+"项目代码",contents.ToString());
        }

        List<string> GetFile(string path)
        {
            List<string> files = new List<string>();
            if (Directory.Exists(path))
            {
                List<string> dis = new List<string>();
                foreach (string di in Directory.GetDirectories(path))
                {
                    files.AddRange(Directory.GetFiles(di, $"*.cs"));

                    files.AddRange(GetFile(di));
                }
            }

            return files;
        }

        static void WordTestV2(string fileName, string title, string content)
        {
            var newFile2 = $"{fileName}.docx";
            using (var fs = new FileStream(newFile2, System.IO.FileMode.Create, FileAccess.Write))
            {
                XWPFDocument doc = WordHelper.Createm_DocxPage();
                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                //r0.AddBreak();
                r0.FontFamily = "microsoft yahei";
                r0.FontSize = 18;
                r0.IsBold = true;
                r0.SetText(title);

                var p2 = doc.CreateParagraph();
                p2.Alignment = ParagraphAlignment.LEFT;
                p2.IndentationFirstLine = 500;
                XWPFRun r2 = p2.CreateRun();
                r2.FontFamily = "·ÂËÎ";
                r2.FontSize = 12;
                r2.IsBold = true;
                r2.SetText(content);

                doc.Write(fs);
            }
        }
    }
}
