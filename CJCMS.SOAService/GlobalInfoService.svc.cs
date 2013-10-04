using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CJCMS.Framework;

namespace CJCMS.SOAService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“GlobalInfoService”。
    public class GlobalInfoService : IGlobalInfoService
    {
        public void Get(string key, out object result)
        {
            GlobalInfo.Get(key,out result);
        }

        public void Add(string id, object o)
        {
            GlobalInfo.Add(id,o);
        }

        public void Remove(string id)
        {
            GlobalInfo.Remove(id);
        }
    }
}
