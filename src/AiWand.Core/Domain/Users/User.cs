using AiWand.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Domain
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public partial class User : BaseDomain
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 当前登录IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        public string LastIP { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public EUserType UserType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
