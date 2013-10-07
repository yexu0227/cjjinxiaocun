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
using CJCMS.Domain.Entity;
using CJCMS.Domain.ValueObject;
using CJCMS.Framework.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Domain.Service
{
    public class ProductService
    {
        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Product p)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            ir.Add(p);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Product p)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            Product c = ir.GetByKey(p.Id);
            if (c == null) { throw new Exception("have no object"); }
            ir.Update(p);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 分页遍历商品
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="index">页号</param>
        /// <param name="count">页大小</param>
        /// <param name="allCount">总数</param>
        /// <returns></returns>
        public IList<Product> FetchAllByCategory(string categoryId, int index, int count,out int allCount)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            allCount = ir.Count(a => a.CategoryId == categoryId);
            return ir.Table.Where(a => a.CategoryId == categoryId).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }
        /// <summary>
        /// 在分类下按照商品名称模糊查询
        /// </summary>
        /// <param name="categoryId">产品分类编号</param>
        /// <param name="name">产品名称查询Key</param>
        /// <param name="index">页号</param>
        /// <param name="count">页大小</param>
        /// <param name="allCount">总数</param>
        /// <returns></returns>
        public IList<Product> FetchAllByCategoryAndName(string categoryId, string name,int index, int count, out int allCount)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            allCount = ir.Count(a => a.CategoryId == categoryId && a.ProductName.Contains(name));
            return ir.Table.Where(a => a.CategoryId == categoryId&&a.ProductName.Contains(name)).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }

        /// <summary>
        /// 指定商品上线
        /// </summary>
        /// <param name="productId">商品编号</param>
        public void SetProductOn(string productId)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            Product c = ir.GetByKey(productId);
            if (c == null) { throw new Exception("have no object"); }
            c.Status = ProductStatus.OnLine;
            ir.Update(c);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 指定商品下线
        /// </summary>
        /// <param name="productId">商品编号</param>
        public void SetProductOff(string productId)
        {
            IRepository<Product> ir = null;
            ir = AutofacManager<IRepository<Product>>.GetConcrete<DefaultRepository<Product>>();
            Product c = ir.GetByKey(productId);
            if (c == null) { throw new Exception("have no object"); }
            c.Status = ProductStatus.OffLine;
            ir.Update(c);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
    }
}
