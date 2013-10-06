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
    public class Supplier : IEntity
    {
        public virtual string Id { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string SupplierPhomeNum { get; set; }
        public virtual string SupplierQQ { get; set; }
        public virtual string SupplierFaxNum { get; set; }
        public virtual string SupplierAddress { get; set; }
        public virtual string SupplierZipCode { get; set; }
        public virtual string SupplierEmail { get; set; }
        public virtual decimal SupplierInitOwing { get; set; }
        public virtual decimal SupplierNowOwing { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual int Status { get; set; }
        public virtual string OtherInfo { get; set; }
    }
}
