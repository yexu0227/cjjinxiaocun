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
using CJCMS.Domain.Entity;
using CJCMS.Data;
using CJCMS.Framework.Autofac;

namespace CJCMS.Domain.Service
{
    public class CategoryService
    {
        #region 分类添加
        public void AddCategory(Category ca)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            ir.Add(ca);
            NHibernateSessionManager.Instance.Session.CommitChanges();
            //ir.Persist();
        }
        #endregion

        #region 分类修改
        public void UpdateCategory(Category ca)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            Category c = ir.GetByKey(ca.Id);
            if (c == null) { throw new Exception("have no object"); }
            if (c.Status == "master") { throw new Exception("系统基础数据不能修改"); }
            ir.Update(ca);
            NHibernateSessionManager.Instance.Session.CommitChanges();
            //ir.Persist();
        }
        #endregion 

        #region 删除分类（级联）
        public void DeleteCategory(string id)
        {
            IRepository<Category> ir3 = null;
            ir3 = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            Category c= ir3.GetByKey(id);
            if (c == null) { throw new Exception("have no object"); }
            if (c.Status == "master") { throw new Exception("系统基础数据不能删除"); }
            ir3.Delete(c);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        #endregion 

        #region 遍历分类
        public IList<Category> FetchAll()
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return  ir.Table.ToList();
        }
        #endregion

        #region 分页遍历分类
        public IList<Category> FetchAll(int index,int count)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Where(a => a.Id!=null).Skip((index - 1) * count).Take(count).OrderBy(k => k.SortNum).ToList();
        }
        #endregion

        #region 查找一个分类
        public Category GetOneCategoryById(string id)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            ir.GetByKey(id);
            return ir.Table.Single(a => a.Id == id);
        }
        #endregion 

        #region 查找一个分类
        public Category GetOneCategoryByName(string name)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Single(a => a.CategoryName == name);
        }
        #endregion 

        #region 根据父编号获取下面的所有分类
        public IList<Category> FetchAllByPCid(string pid)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Where(a=> a.ParentId == pid).OrderBy(k=>k.SortNum).ToList();
        }

        public IList<Category> FetchAllByPCidUI(string pid)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Where(a => a.ParentId == pid&&a.Status=="on").OrderBy(k => k.SortNum).ToList();
        }
        #endregion

        #region 查找一个分类下的分类数目
        public int GetCountByPid(string id)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Count(a=>a.ParentId==id);
        }
        #endregion 
    
        /// <summary>
        /// 按照名称模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<Category> FetchCategoryListByName(string name,int index,int count)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Where(a => a.CategoryName.Contains(name)).Skip((index-1)*count).Take(count).OrderBy(k => k.SortNum).ToList();
        }

        /// <summary>
        /// 按照名称获取总数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SearchByNameCount(string name)
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Count(a=>a.CategoryName.Contains(name));
        }

        public int AllCount()
        {
            IRepository<Category> ir = null;
            ir = AutofacManager<IRepository<Category>>.GetConcrete<DefaultRepository<Category>>();
            return ir.Table.Count();
        }
    }
}