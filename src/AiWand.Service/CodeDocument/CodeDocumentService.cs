using AiWand.Core.Data;
using AiWand.Core.DTO.CodeDocument;
using AiWand.Core.Office;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using AiWand.Core.Enums;
using AiWand.Core.Domain.CodeDocuments;
using AiWand.Data;

namespace AiWand.Service.CodeDocument
{
    public class CodeDocumentService : ICodeDocumentService
    {
        private readonly IRepository<Core.Domain.CodeDocuments.CodeDocument> _codeDocumentRepository;
        private readonly IRepository<CodePath> _codePathRepository;
        private readonly AiWandDataContext _dbContext;

        public CodeDocumentService(IRepository<Core.Domain.CodeDocuments.CodeDocument> codeDocumentRepository,
            IRepository<CodePath> codePathRepository,
            AiWandDataContext dbContext)
        {
            _codeDocumentRepository = codeDocumentRepository;
            _codePathRepository = codePathRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 生成代码文档
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns>文档连接</returns>
        public string Build(BuildInput input)
        {
            //判断是否已经生成文档
            var codeDocument = _codeDocumentRepository.Table.Where(d => d.CompanyName == input.CompanyName
                                                            && d.CompanyAbb == input.CompanyAbb
                                                            && d.Language == input.Language)
                                                            .FirstOrDefault();

            if (codeDocument != null)
            {
                return codeDocument.Url;
            }


            //读取代码内容
            CodeFileDto codeFile = GetContent(input);

            //生成文档
            string fileName = $"{input.CompanyName}[{input.CompanyAbb}]-({input.Language})";
            string docName = CreateDoc(fileName, input.CompanyName + "项目代码", codeFile.Content.ToString());

            //保存文档信息
            DateTime time = DateTime.Now;
            Core.Domain.CodeDocuments.CodeDocument document = new Core.Domain.CodeDocuments.CodeDocument
            {
                CompanyAbb = input.CompanyAbb,
                CompanyName = input.CompanyName,
                CreateTime = time,
                Creator = "sys",
                DocumentName = docName,
                DocumentNo = "",
                DocumentStatus = 1,
                Language = input.Language,
                Status = EStatus.正常,
                Url = ""
            };

            List<CodePath> codePaths = new List<CodePath>();
            for (int i = 0; i < codeFile.FileNames.Count; i++)
            {
                CodePath codePath = new CodePath
                {
                    DocumentId = document.Id,
                    CodeFile = codeFile.FileNames[i],
                    CodeFilePath = codeFile.FilePaths[i],
                    Status = EStatus.正常,
                    CreateTime = time,
                    Creator = "sys",
                };
                codePaths.Add(codePath);
            }
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                _codeDocumentRepository.Insert(document);
                _codePathRepository.Insert(codePaths);
                _dbContext.SaveChanges();

                tran.Commit();
            }
            return docName;
        }

        private CodeFileDto GetContent(BuildInput input)
        {
            string path = @"D:\python";

            if (Directory.Exists(path))
            {
                List<string> files = GetFile(path, input.Language);
                List<string> fileTexts = new List<string>();
                CodeFileDto codeFile = new CodeFileDto
                {
                    FilePaths = new List<string>(),
                    FileNames = new List<string>(),
                    Contents = new List<string>(),
                    Content = new StringBuilder()
                };
                foreach (var file in files)
                {
                    codeFile.FilePaths.Add(file);
                    codeFile.FileNames.Add(Path.GetFileName(file));
                    codeFile.Contents.AddRange(File.ReadAllLines(file));
                    codeFile.Content.AppendLine(File.ReadAllText(file));
                    if (codeFile.Contents.Count >= 4000)
                    {
                        break;
                    }
                }

                return codeFile;
            }

            return null;
        }

        List<string> GetFile(string path, string language)
        {
            //获取已经被使用的文件
            List<string> codePaths = _codePathRepository.Table.Select(c=>c.CodeFilePath).ToList();

            List<string> files = new List<string>();
            if (Directory.Exists(path))
            {
                List<string> dis = new List<string>();
                foreach (string di in Directory.GetDirectories(path))
                {
                    foreach (var item in FileExtension(language))
                    {
                        files.AddRange(Directory.GetFiles(di, $"*{item}", SearchOption.AllDirectories));
                    }
                }
                List<string> existFiles = new List<string>();
                foreach (var file in files)
                {
                    if (codePaths.Contains(file))
                    {
                        existFiles.Add(file);
                    }
                }
                foreach (var file in existFiles)
                {
                    files.Remove(file);
                }
            }

            return files;
        }

        private List<string> FileExtension(string language)
        {
            List<string> fileExtensions = new List<string>();
            if (language.ToUpper() == CodeLanguage.CSHARP)
            {
                fileExtensions.Add(LanguageExtension.CSHARP);
            }
            if (language.ToUpper() == CodeLanguage.JAVA)
            {
                fileExtensions.Add(LanguageExtension.JAVA);
            }
            if (language.ToUpper() == CodeLanguage.JAVASCRIPT)
            {
                fileExtensions.Add(LanguageExtension.JAVASCRIPT);
            }
            if (language.ToUpper() == CodeLanguage.PHP)
            {
                fileExtensions.Add(LanguageExtension.PHP);
            }
            if (language.ToUpper() == CodeLanguage.PYTHON)
            {
                fileExtensions.Add(LanguageExtension.PYTHON);
            }
            if (language.ToUpper() == CodeLanguage.CSS)
            {
                fileExtensions.Add(LanguageExtension.CSS);
            }
            if (language.ToUpper() == CodeLanguage.CPP)
            {
                fileExtensions.AddRange(LanguageExtension.CPP);
            }
            if (language.ToUpper() == CodeLanguage.C)
            {
                fileExtensions.AddRange(LanguageExtension.C);
            }
            return fileExtensions;
        }

        static string CreateDoc(string fileName, string title, string content)
        {
            var wordFileName = $"{fileName}.docx";
            using (var fs = new FileStream(wordFileName, System.IO.FileMode.Create, FileAccess.Write))
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
                r2.FontFamily = "宋体";
                r2.FontSize = 12;
                r2.SetText(content);

                doc.Write(fs);
            }

            return wordFileName;
        }
    }
}
