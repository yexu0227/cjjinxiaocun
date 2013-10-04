using CJCMS.Contracts.DTO.Category;
using CJCMS.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CJCMS.SOAService
{
    public class CategoryManager : ICategoryManager
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        public void AddCategory(CategoryDTO ca) { }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        public void UpdateCategory(CategoryInfo ca) { }

        /// <summary>
        /// 删除分类 根据编号
        /// </summary>
        /// <param name="id">分类编号</param>
        public void DeleteCategory(string id) { }

        /// <summary>
        /// 遍历分类
        /// </summary>
        public IList<CategoryInfo> FetchAll() { 
            //CategoryManager
            return new List<CategoryInfo>(); 
        
        }
    }
}