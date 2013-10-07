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
    public class CustomerService
    {
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="p"></param>
        public void AddCustomer(Customer p)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            ir.Add(p);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="p"></param>
        public void UpdateCustomer(Customer p)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            Customer c = ir.GetByKey(p.Id);
            if (c == null) { throw new Exception("have no object"); }
            ir.Update(p);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 分页遍历客户
        /// </summary>
        /// <param name="index">页号</param>
        /// <param name="count">页大小</param>
        /// <param name="allCount">总数</param>
        /// <returns></returns>
        public IList<Customer> FetchAll(int index, int count, out int allCount)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            allCount = ir.Count(a => a.Id != null);
            return ir.Table.Where(a => a.Id != null).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }

        /// <summary>
        /// 在分类下按照客户名称模糊查询
        /// </summary>
        /// <param name="name">客户名称查询Key</param>
        /// <param name="index">页号</param>
        /// <param name="count">页大小</param>
        /// <param name="allCount">总数</param>
        /// <returns></returns>
        public IList<Customer> FetchByName(string name, int index, int count, out int allCount)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            allCount = ir.Count(a => a.Id != null && a.CustomerName.Contains(name));
            return ir.Table.Where(a => a.Id != null && a.CustomerName.Contains(name)).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }

        /// <summary>
        /// 在分类下按照客户状态
        /// </summary>
        /// <param name="name">客户状态</param>
        /// <param name="index">页号</param>
        /// <param name="count">页大小</param>
        /// <param name="allCount">总数</param>
        /// <returns></returns>
        public IList<Customer> FetchByStatus(int status, int index, int count, out int allCount)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            allCount = ir.Count(a => a.Id != null && a.Status==status);
            return ir.Table.Where(a => a.Id != null && a.Status==status).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }

        /// <summary>
        /// 指定客户上线
        /// </summary>
        /// <param name="customerId">客户编号</param>
        public void SetCustomerOn(string customerId)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            Customer c = ir.GetByKey(customerId);
            if (c == null) { throw new Exception("have no object"); }
            c.Status = CustomerStatus.OnLine;
            ir.Update(c);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 指定客户下线
        /// </summary>
        /// <param name="customerId">客户编号</param>
        public void SetCustomerOff(string customerId)
        {
            IRepository<Customer> ir = null;
            ir = AutofacManager<IRepository<Customer>>.GetConcrete<DefaultRepository<Customer>>();
            Customer c = ir.GetByKey(customerId);
            if (c == null) { throw new Exception("have no object"); }
            c.Status = CustomerStatus.OffLine;
            ir.Update(c);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
    }
}
