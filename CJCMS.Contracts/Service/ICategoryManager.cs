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
using CJCMS.Contracts.DTO.Category;

namespace CJCMS.Contracts.Service
{
    public interface ICategoryManager
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        void AddCategory(CategoryInfo ca);
        
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        void UpdateCategory(CategoryInfo ca);

        void UpdateCategoryRoot(string id, string newpid);

        /// <summary>
        /// 删除分类 根据编号
        /// </summary>
        /// <param name="id">分类编号</param>
        void DeleteCategory(string id);

        /// <summary>
        /// 遍历分类
        /// </summary>
        IList<CategoryInfo> FetchAll();


        IList<CategoryInfo> FetchAll(int index, int count);
        /// <summary>
        /// 根据父分类获取分类列表
        /// </summary>
        IList<CategoryInfo> FetchCategoryListById(string pid);
        
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CategoryInfo GetOneById(string id);

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        CategoryInfo GetOneByName(string name);

        /// <summary>
        /// 获取分类下条数
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        int GetCount(string pid);

        IList<CategoryInfo> FetchCategoryListByName(string name, int index, int count);

        int SearchByNameCount(string name);

        int AllCount();
    }
}
