using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class UnAuthAttribute : Attribute
    {
        public UnAuthAttribute()
        {

        }
    }
}
