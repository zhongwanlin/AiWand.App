using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Domain.CodeDocuments
{
    public class CodeDocumentPath : BaseDomain
    {
        /// <summary>
        /// 文档Id
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// 代码文件路径
        /// </summary>
        public string CodeFilePath { get; set; }

        public string CodeFile { get; set; }
    }
}
