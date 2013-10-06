using CJCMS.Data;
using CJCMS.Domain.Entity;
using CJCMS.Framework.Autofac;
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

namespace CJCMS.Domain.Service
{
    public class BuyInService
    {
        /// <summary>
        /// 添加进货单
        /// </summary>
        /// <param name="BuyIn">进货单</param>
        /// <param name="BuyInItemList">进货单项列表</param>
        public void AddBuyIn(BuyIn BuyIn, IList<BuyInItem> BuyInItemList)
        {
            IRepository<BuyIn> ir = null;
            ir = AutofacManager<IRepository<BuyIn>>.GetConcrete<DefaultRepository<BuyIn>>();
            IRepository<BuyInItem> irBuyInItem = null;
            irBuyInItem = AutofacManager<IRepository<BuyInItem>>.GetConcrete<DefaultRepository<BuyInItem>>();
            NHibernateSessionManager.Instance.Session.BeginTransaction();
            try
            {
                ir.Add(BuyIn);
                foreach (BuyInItem item in BuyInItemList)
                {
                    irBuyInItem.Add(item);
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
        /// 添加进货项目到进货单
        /// </summary>
        /// <param name="BuyInItemList"></param>
        public void AddBuyInItem(IList<BuyInItem> BuyInItemList)
        {
            IRepository<BuyInItem> irBuyInItem = null;
            irBuyInItem = AutofacManager<IRepository<BuyInItem>>.GetConcrete<DefaultRepository<BuyInItem>>();
            foreach (BuyInItem item in BuyInItemList)
            {
                irBuyInItem.Add(item);
            }
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 从进货单中删除进货项目
        /// </summary>
        /// <param name="BuyInItemList"></param>
        public void DeleteBuyInItem(IList<BuyInItem> BuyInItemList)
        {
            IRepository<BuyInItem> irBuyInItem = null;
            irBuyInItem = AutofacManager<IRepository<BuyInItem>>.GetConcrete<DefaultRepository<BuyInItem>>();
            foreach (BuyInItem item in BuyInItemList)
            {
                irBuyInItem.Delete(item);
            }
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        /// <summary>
        /// 设置进货单状态
        /// </summary>
        /// <param name="BuyInId"></param>
        /// <param name="BuyInStatus"></param>
        public void SetBuyInStatus(string BuyInId, int BuyInStatus)
        {
            IRepository<BuyIn> ir = null;
            ir = AutofacManager<IRepository<BuyIn>>.GetConcrete<DefaultRepository<BuyIn>>();
            BuyIn BuyIn = ir.GetByKey(BuyInId);
            if (BuyIn == null) { throw new Exception("have no object"); }
            BuyIn.Status = BuyInStatus;
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 按照进货单状态查询进货单
        /// </summary>
        /// <param name="BuyInStatus"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="allCount"></param>
        /// <returns></returns>
        public IList<BuyIn> FetchAllByStatus(int buyInStatus, int index, int count, ref int allCount)
        {
            IRepository<BuyIn> ir = null;
            ir = AutofacManager<IRepository<BuyIn>>.GetConcrete<DefaultRepository<BuyIn>>();
            allCount = ir.Count(a => a.Status == buyInStatus);
            return ir.Table.Where(a => a.Status == buyInStatus).Skip((index - 1) * count).Take(count).OrderBy(k => k.Created).ToList();
        }
        /// <summary>
        /// 按照进货单据查询进货单项目
        /// </summary>
        /// <param name="BuyInId"></param>
        /// <returns></returns>
        public IList<BuyInItem> FetchAllBuyInItem(string buyInId)
        {
            IRepository<BuyInItem> ir = null;
            ir = AutofacManager<IRepository<BuyInItem>>.GetConcrete<DefaultRepository<BuyInItem>>();
            return ir.Table.Where(a => a.BuyInId == buyInId).ToList();
        }
    }
}
