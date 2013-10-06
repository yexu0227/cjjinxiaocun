using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Domain.ValueObject
{
   public sealed  class SupplierStatus
    {
        /// <summary>
        /// 上线
        /// </summary>
        public const int OnLine = 1;

        /// <summary>
        /// 下线
        /// </summary>
        public const int OffLine = 0;
    }
}
