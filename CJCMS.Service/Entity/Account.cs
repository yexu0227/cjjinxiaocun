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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJCMS.Data;
using CJCMS.Framework.Security;

namespace CJCMS.Domain.Entity
{
    public sealed class Account : IEntity
    {
        #region 私有属性
        /// <summary>
        /// 实体编号
        /// </summary>
        private string id = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        private string name = string.Empty;
        /// <summary>
        /// 角色
        /// </summary>
        private string role = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime created = DateTime.Now;
        
        /// <summary>
        /// 用户类型
        /// </summary>
        private string type = string.Empty;
        /// <summary>
        /// 用户注册邮箱
        /// </summary>
        private string email = string.Empty;
        /// <summary>
        /// 是否被禁用
        /// </summary>
        private bool isForbidden = false;

        private string telNum = string.Empty;

        private string homePhone = string.Empty;
        #endregion

        #region 公共属性
        public virtual string Id { get { return this.id; } set { this.id = value; } }
        public virtual string Name { get { return this.name; } set { this.name = value; } }
        public virtual string Role { get { return this.role; } set { this.role = value; } }
        public virtual DateTime Created { get { return this.created; } set { this.created = value; } }
        public virtual byte[] Salts { get; set; }
        public virtual byte[] Pwd { get; set; }
        public virtual string Email { get { return this.email; } set { this.email = value; } }
        public virtual bool IsForbidden { get { return this.isForbidden; } set { this.isForbidden = value; } }
        public virtual string AccountType { get { return this.type; } set { this.type = value; } }
        public virtual string PlantPwd { get; set; }
        public virtual string TelNum { get { return this.telNum; } set { this.telNum = value; } }
        public virtual string HomePhone { get { return this.homePhone; } set { this.homePhone = value; } }
        #endregion
    }
}
