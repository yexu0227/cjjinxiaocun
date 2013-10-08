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
    public class Customer : IEntity
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 客户全名
        /// </summary>
        public virtual string CustomerName{get;set;}
        /// <summary>
        /// 联系人名称
        /// </summary>
        public virtual string ContactName { get; set; }
        /// <summary>
        /// 联系人联系电话
        /// </summary>
        public virtual string CustomerPhomeNum { get; set; }
        /// <summary>
        /// 联系人QQ
        /// </summary>
        public virtual string CustomerQQ { get; set; }
        /// <summary>
        /// 联系人传真号
        /// </summary>
        public virtual string CustomerFaxNum { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public virtual string CustomerAddress { get; set; }
        /// <summary>
        /// 客户邮编
        /// </summary>
        public virtual string CustomerZipCode { get; set; }
        /// <summary>
        /// 客户邮箱
        /// </summary>
        public virtual string CustomerEmail { get; set; }
        /// <summary>
        /// 初始化欠款额
        /// </summary>
        public virtual decimal CustomerInitOwing { get; set; }
        /// <summary>
        /// 当前欠款额
        /// </summary>
        public virtual decimal CustomerNowOwing { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime Updated { get; set; }
        /// <summary>
        /// 创建客户时间
        /// </summary>
        public virtual DateTime Created { get; set; }
        /// <summary>
        /// 客户状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string OtherInfo { get; set; }
    }
}
