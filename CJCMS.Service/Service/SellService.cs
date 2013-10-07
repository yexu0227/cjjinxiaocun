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
    public class SellService
    {
        /// <summary>
        /// 添加销售单
        /// </summary>
        /// <param name="sell">销售单</param>
        /// <param name="sellItemList">销售单项列表</param>
        public void AddSell(Sell sell,IList<SellItem> sellItemList)
        {
            IRepository<Sell> ir = null;
            ir = AutofacManager<IRepository<Sell>>.GetConcrete<DefaultRepository<Sell>>();
            IRepository<SellItem> irSellItem = null;
            irSellItem = AutofacManager<IRepository<SellItem>>.GetConcrete<DefaultRepository<SellItem>>();
            NHibernateSessionManager.Instance.Session.BeginTransaction();
            try
            {
                ir.Add(sell);
                foreach (SellItem item in sellItemList)
                {
                    irSellItem.Add(item);
                }
                NHibernateSessionManager.Instance.Session.CommitTransaction();
            }
            catch (Exception ee)
            {
                NHibernateSessionManager.Instance.Session.RollbackTransaction();
                throw ee;
            }
        }

        /// <summary>
        /// 创建销售单
        /// </summary>
        /// <param name="buyIn"></param>
        public void AddSell(Sell sell)
        {
            IRepository<Sell> ir = null;
            ir = AutofacManager<IRepository<Sell>>.GetConcrete<DefaultRepository<Sell>>();
            ir.Add(sell);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 添加销售项目到销售单
        /// </summary>
        /// <param name="sellItemList"></param>
        public void AddSellItem(IList<SellItem> sellItemList)
        {
            IRepository<SellItem> irSellItem = null;
            irSellItem = AutofacManager<IRepository<SellItem>>.GetConcrete<DefaultRepository<SellItem>>();
            foreach (SellItem item in sellItemList)
            {
                irSellItem.Add(item);
            }
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 从销售单中删除销售项目
        /// </summary>
        /// <param name="sellItemList"></param>
        public void DeleteSellItem(IList<SellItem> sellItemList)
        {
            IRepository<SellItem> irSellItem = null;
            irSellItem = AutofacManager<IRepository<SellItem>>.GetConcrete<DefaultRepository<SellItem>>();
            foreach (SellItem item in sellItemList)
            {
                irSellItem.Delete(item);
            }
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        /// <summary>
        /// 设置销售单状态
        /// </summary>
        /// <param name="sellId"></param>
        /// <param name="sellStatus"></param>
        public void SetSellStatus(string sellId, int sellStatus)
        {
            IRepository<Sell> ir = null;
            ir = AutofacManager<IRepository<Sell>>.GetConcrete<DefaultRepository<Sell>>();
            Sell sell = ir.GetByKey(sellId);
            if (sell == null) { throw new Exception("have no object"); }
            sell.Status = sellStatus;
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 按照销售单状态查询销售单
        /// </summary>
        /// <param name="sellStatus"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="allCount"></param>
        /// <returns></returns>
        public IList<Sell> FetchAllByStatus(int sellStatus,int index, int count, ref int allCount)
        {
            IRepository<Sell> ir = null;
            ir = AutofacManager<IRepository<Sell>>.GetConcrete<DefaultRepository<Sell>>();
            allCount = ir.Count(a=>a.Status==sellStatus);
            return ir.Table.Where(a=>a.Status==sellStatus).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }
        /// <summary>
        /// 按照销售单据查询销售单项目
        /// </summary>
        /// <param name="sellId"></param>
        /// <returns></returns>
        public IList<SellItem> FetchAllSellItem(string sellId)
        {
            IRepository<SellItem> ir = null;
            ir = AutofacManager<IRepository<SellItem>>.GetConcrete<DefaultRepository<SellItem>>();
            return ir.Table.Where(a => a.SellId == sellId).ToList();
        }
    }
}
