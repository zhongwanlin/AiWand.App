using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiWand.Core.Caching.Redis
{
    public class Configuration
    {
        public Configuration()
        {

        }

        public List<ConfigItem> ConfigItems { get; set; }

        public override string ToString()
        {
            if (ConfigItems != null && ConfigItems.Count > 1)
            {
                List<String> configItemStrings = new List<string>();
                foreach (var item in ConfigItems)
                {
                    configItemStrings.Add(item.ToString());
                }
                return String.Join<String>(",", configItemStrings);
            }
            else if (ConfigItems != null && ConfigItems.Count == 1)
            {
                return ConfigItems[0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
