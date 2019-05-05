using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core
{
    public class Result
    {
        public Result()
        { }

        public Result(bool isSuccessed)
        {
            IsSuccessed = isSuccessed;
        }
        public string Code
        {
            get
            {
                if (IsSuccessed == false)
                {
                    return "500";
                }
                else
                {

                    return "200";
                }
            }
        }

        public string Message { get; set; }

        public Object Data { get; set; }

        public bool IsSuccessed { get; set; }
    }
}
