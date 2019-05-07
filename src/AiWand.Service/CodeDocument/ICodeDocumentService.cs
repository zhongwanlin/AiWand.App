using AiWand.Core.DTO.CodeDocument;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Service.CodeDocument
{
    public interface ICodeDocumentService
    {
        /// <summary>
        /// 生成代码文档
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns>文档连接</returns>
        string Build(BuildInput input);

        List<Core.Domain.CodeDocuments.CodeDocument> GetCodeDocuments(string companyName, string language);
    }
}
