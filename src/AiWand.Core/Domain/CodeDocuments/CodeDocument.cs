using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Domain.CodeDocuments
{
    /// <summary>
    /// 代码文档信息
    /// </summary>
    public class CodeDocument : BaseDomain
    {
        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentNo { get; set; }
        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// 文档路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string CompanyAbb { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadTimes { get; set; }

        /// <summary>
        /// 文档状态
        /// </summary>
        public int DocumentStatus { get; set; }

    }
}
