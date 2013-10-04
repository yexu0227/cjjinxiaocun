using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CJCMS.SOAService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IGlobalInfoService”。
    [ServiceContract]
    public interface IGlobalInfoService
    {
        [OperationContract]
        void Get(string key, out object result);

        [OperationContract]
        void Add(string id,object o);

        [OperationContract]
        void Remove(string id);
    }
}
