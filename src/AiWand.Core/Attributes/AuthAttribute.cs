using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : Attribute
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        public AuthAttribute() : this("")
        {

        }
        public AuthAttribute(string code)
        {
            ModuleCode = code;
        }
    }
}
