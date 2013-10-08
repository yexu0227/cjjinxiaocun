// 作者:					曹军 
// 邮件：               869722304@qq.com(仅仅支持商业合作洽谈)
// 创建时间:			    2012-08-8
// 最后修改时间:			2012-08-11
// 
// 未经修改的文件版权属于原作者所有，但是你可以阅读，修改，调试。本项目不建议商用，不能确保稳定性。
// 同时由于项目Bug引起的一切问题，原作者概不负责。
//
// 本项目所引用的所有类库，仍然遵循其原本的协议，不得侵害其版权。
//
// 您一旦下载就视为您已经阅读此声明。
//
// 您不可以移除项目中任何声明。
using CJCMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Domain.Entity
{
    public class SellItem : IEntity
    {
        /// <summary>
        /// 销售项目编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string CustomerId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public virtual string ProductId { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public virtual string CategoryId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 商品计量单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal SinglePrice { get; set; }
        /// <summary>
        /// 主销售实体编号
        /// </summary>
        public virtual string SellId { get; set; }
        /// <summary>
        /// 商品数
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string OtherInfo { get; set; }
    }
}
