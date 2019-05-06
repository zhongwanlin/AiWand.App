using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.DTO.CodeDocument
{
    /// <summary>
    /// 代码文件
    /// </summary>
    public class CodeFileDto
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public List<string> FilePaths { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public List<string> FileNames { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 文件内容语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 文件内容（每一行）
        /// </summary>
        public List<string> Contents { get; set; }

        /// <summary>
        /// 文件内容（所有内容）
        /// </summary>
        public StringBuilder Content { get; set; }
    }
}
