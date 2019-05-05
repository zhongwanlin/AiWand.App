using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.DTO.CodeDocument
{
    public class BuildInput
    {
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
    }
}
