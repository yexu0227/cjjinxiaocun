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
    public class Product : IEntity
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 商品分类编号
        /// </summary>
        public virtual string CategoryId { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 商品计量单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 进价
        /// </summary>
        public virtual decimal InPrice { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public virtual decimal SingleOutPrice { get; set; }
        /// <summary>
        /// 批发价
        /// </summary>
        public virtual decimal MultiOutPrice { get; set; }
        /// <summary>
        /// 初始库存量
        /// </summary>
        public virtual int InitCount { get; set; }
        /// <summary>
        /// 当前库存数
        /// </summary>
        public virtual int NowCount { get; set; }
        /// <summary>
        /// 库存上限
        /// </summary>
        public virtual int UpperCount { get; set; }
        /// <summary>
        /// 库存下限
        /// </summary>
        public virtual int LowerCount { get; set; }
        /// <summary>
        /// 商品最后更新时间
        /// </summary>
        public virtual DateTime Updated { get; set; }
        /// <summary>
        /// 商品创建时间
        /// </summary>
        public virtual DateTime Created { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string OtherInfo { get; set; }
    }
}
