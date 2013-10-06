using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Domain.ValueObject
{
    public sealed class SellStatus
    {
        /// <summary>
        /// 有欠款
        /// </summary>
        public const int HasDebt = 0;

        /// <summary>
        /// 发货中
        /// </summary>
        public const int SendOut = 1;

        /// <summary>
        /// 销售完成
        /// </summary>
        public const int Finished = 2;

        /// <summary>
        /// 销售退货中
        /// </summary>
        public const int CustomerNeedReturn = 3;

    }
}
