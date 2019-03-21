using AiWand.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Domain
{
    public abstract class BaseDomain : BaseEntity
    {

        /// <summary>
        /// 记录创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 记录修改人
        /// </summary>
        public string Reviser { get; set; }

        /// <summary>
        /// 记录修改时间
        /// </summary>
        public DateTime? ReviseTime { get; set; }

        /// <summary>
        /// 记录状态
        /// </summary>
        public EStatus Status { get; set; }
    }
}
