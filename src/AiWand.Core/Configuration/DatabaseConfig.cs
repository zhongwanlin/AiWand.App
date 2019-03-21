using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Configuration
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DatabaseConfig
    {
        public static DatabaseConfig DbConfig { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// SQL脚本路径
        /// </summary>
        public string SqlScriptPath { get; }

        /// <summary>
        /// 数据提供器
        /// </summary>
        public string Provider { get; set; }

        public DatabaseConfig()
        {
            //从配置中心获取数据库配置
            ConnectionString = "server=127.0.0.1;PORT=3306;database=aiwand;uid=root;pwd=zwl@AP.)0706439;Connection Timeout=60;Allow Zero Datetime=True;Allow User Variables=True;pooling=true;min pool size=5;max pool size=512;SslMode=None;";
            SqlScriptPath = string.Empty;
            Provider = "MySqlProvider";
            DbConfig = this;
        }
    }
}
