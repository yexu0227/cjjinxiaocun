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

namespace CJCMS.Domain.Entity
{
    public class Category : IEntity
    {
        #region 私有属性
        /// <summary>
        /// 实体编号
        /// </summary>
        private string id = string.Empty;
        /// <summary>
        /// 分类名称
        /// </summary>
        private string categoryName = string.Empty;
        /// <summary>
        /// 表示此分类是否在首页显示
        /// </summary>
        private string exInfo = string.Empty;
        /// <summary>
        /// 首页显示顺序
        /// </summary>
        private int sortNum = -1;
        /// <summary>
        /// 父分类节点编号
        /// </summary>
        private string parentId = string.Empty;

        private string iconName = string.Empty;
        #endregion

        #region 公共属性
        public virtual string Id { get { return this.id; } set { this.id = value; } }
        public virtual string CategoryName { get { return this.categoryName; } set { this.categoryName = value; } }
        public virtual string ExInfo { get { return this.exInfo; } set { this.exInfo = value; } }
        public virtual int SortNum { get { return this.sortNum; } set { this.sortNum = value; } }
        public virtual string ParentId { get { return this.parentId; } set { this.parentId = value; } }
        public virtual string IconName { get { return this.iconName; } set { this.iconName = value; } }
        public virtual string Status { get; set; }
        #endregion

    }
}
