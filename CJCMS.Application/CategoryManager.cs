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
using CJCMS.Contracts.Service;
using CJCMS.Contracts.DTO.Category;
using CJCMS.Domain.Service;
using CJCMS.Contracts;
using CJCMS.Domain.Entity;

namespace CJCMS.Application
{
    public class CategoryManager:ICategoryManager
    {
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        public void AddCategory(CategoryInfo ca)
        {
            ca.Id = Guid.NewGuid().ToString();
            ///验证用户输入
            TValidationHelper<CategoryInfo>.TValidation(ca);
            CategoryService service = new CategoryService();
            
            service.AddCategory(AutoMapper.Mapper.Map<CategoryInfo, Category>(ca));
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="ca">分类信息</param>
        public void UpdateCategory(CategoryInfo ca)
        {
            ///验证用户输入
            TValidationHelper<CategoryInfo>.TValidation(ca);

            CategoryService service = new CategoryService();
            service.UpdateCategory(AutoMapper.Mapper.Map<CategoryInfo, Category>(ca));
        }

        public void UpdateCategoryRoot(string id, string newpid)
        {
            CategoryService service = new CategoryService();
            Category ca = service.GetOneCategoryById(id);
            ca.ParentId = newpid;
            service.UpdateCategory(ca);
        }

        /// <summary>
        /// 删除分类 根据编号
        /// </summary>
        /// <param name="id">分类编号</param>
        public void DeleteCategory(string id)
        {
            CategoryService service = new CategoryService();

            service.DeleteCategory(id);

        }

        /// <summary>
        /// 遍历分类
        /// </summary>
        public IList<CategoryInfo> FetchAll()
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>( service.FetchAll());
        }

        public IList<CategoryInfo> FetchAll(int index,int count)
        {
            CategoryService service = new CategoryService();
           
            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>(service.FetchAll(index,count));
        }

        /// <summary>
        /// 根据父分类编号获得所有子分类
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<CategoryInfo> FetchByPCategoryId(string pid)
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>(service.FetchAllByPCid(pid));
        }

        /// <summary>
        /// 根据父分类编号获得显示状态子分类
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<CategoryInfo> FetchByPCategoryIdUI(string pid)
        {
            CategoryService service = new CategoryService();

            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>(service.FetchAllByPCidUI(pid));
        }

        /// <summary>
        /// 根据父分类获取分类列表
        /// </summary>
        public IList<CategoryInfo> FetchCategoryListById(string pid)
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>(service.FetchAllByPCid(pid));
        }

        public IList<CategoryInfo> FetchCategoryListByName(string name,int index,int count)
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<IList<Category>, IList<CategoryInfo>>(service.FetchCategoryListByName(name,index,count));
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryInfo GetOneById(string id)
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<Category, CategoryInfo>(service.GetOneCategoryById(id));
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CategoryInfo GetOneByName(string name)
        {
            CategoryService service = new CategoryService();
            
            return AutoMapper.Mapper.Map<Category, CategoryInfo>(service.GetOneCategoryByName(name));
        }

        /// <summary>
        /// 获取分类下条数
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public int GetCount(string pid)
        {
            CategoryService service = new CategoryService();
            return service.GetCountByPid(pid);
        }


        public int SearchByNameCount(string name)
        {
            CategoryService service = new CategoryService();
            return service.SearchByNameCount(name);
        }

        public int AllCount()
        {
            CategoryService service = new CategoryService();
            return service.AllCount();
        }

       
    }
}
