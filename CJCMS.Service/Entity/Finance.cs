﻿// 作者:					曹军 
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
    public class Finance : IEntity
    {
        /// <summary>
        /// 财务编号
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 财务时间
        /// </summary>
        public virtual DateTime Time { get; set; }
        /// <summary>
        /// 财务涉及单据号
        /// </summary>
        public virtual string TicketId{get;set;}
        /// <summary>
        /// 财务发生原因
        /// </summary>
        public virtual string Resone { get; set; }
        /// <summary>
        /// 财务发生对象
        /// </summary>
        public virtual string TargetUserName { get; set; }
        /// <summary>
        /// 此次财务收入额
        /// </summary>
        public virtual decimal InMoney { get; set; }
        /// <summary>
        /// 此次财务支出额
        /// </summary>
        public virtual decimal OutMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string OtherInfo { get; set; }
    }
}
