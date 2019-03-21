using AiWand.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Domain
{
    /// <summary>
    /// 验证码
    /// </summary>
    public partial class VerificationCode : BaseEntity
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EStatus Status { get; set; }
    }
}
