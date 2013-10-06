using CJCMS.Data;
using CJCMS.Domain.Entity;
using CJCMS.Framework.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Domain.Service
{
    public class FinanceService
    {
        /// <summary>
        /// 财务进账
        /// </summary>
        /// <param name="f"></param>
        public void FinanceIn(Finance f)
        {
            IRepository<Finance> ir = null;
            ir = AutofacManager<IRepository<Finance>>.GetConcrete<DefaultRepository<Finance>>();
            ir.Add(f);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 财务出账
        /// </summary>
        /// <param name="f"></param>
        public void FinanceOut(Finance f)
        {
            IRepository<Finance> ir = null;
            ir = AutofacManager<IRepository<Finance>>.GetConcrete<DefaultRepository<Finance>>();
            ir.Add(f);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 按照单据进行财务统计
        /// </summary>
        /// <param name="ticketId"></param>
        public void FinanceCountByTicket(string ticketId)
        { 
        
        }

        /// <summary>
        /// 根据时间周期进行财务统计
        /// </summary>
        /// <param name="time"></param>
        public void FinanceCountByTime(string time)
        { 
        
        }
    }
}
