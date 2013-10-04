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
using CJCMS.Framework.Security;

namespace CJCMS.Domain.Service
{
    public class AccountService
    {
        #region 用户注册
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="a">用户实体</param>
        public void Register(Account a)
        { 
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            byte[] salt = null;
            byte[] pwd = null;
            HashHelper.SaltAndHashPassword(a.PlantPwd, out salt, out pwd);
            a.Id = Guid.NewGuid().ToString();
            a.Salts = salt;
            a.Pwd = pwd;
            a.Role = "Member";
            ir.Add(a);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="a">用户实体</param>
        /// <returns></returns>
        public Account Logon(Account a)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            Account k= ir.Table.Where(b => b.Email == a.Email).First();
            if (!HashHelper.ValidatePassword(a.PlantPwd, k.Salts, k.Pwd))
            {
                return null;
            }
            return k;
        }
        #endregion 

        #region 检查邮箱是否存在
        /// <summary>
        /// 检查邮箱是否存在 true:存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        { 
          IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            return ir.Table.Where(b => b.Email == email).Count() > 0;
        }
        #endregion 

        #region 用户禁用/解禁
        /// <summary>
        /// 禁用该用户
        /// </summary>
        /// <param name="a">用户实体</param>
        public void ForbiddenAccount(string id)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            Account a= ir.GetByKey(id);
            a.IsForbidden = true;
            ir.Update(a);
            NHibernateSessionManager.Instance.Session.CommitChanges();
        }

        /// <summary>
        /// 解禁该用户
        /// </summary>
        /// <param name="a">用户实体</param>
        public void UnForbiddenAccount(Account a)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            a.IsForbidden = false;
            ir.Update(a);
        }
        #endregion

        #region 用户查找
        /// <summary>
        /// 根据用户邮箱查找用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public Account GetAccountByEmail(string email)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            return ir.Table.Where(b => b.Email == email).First();
        }
        /// <summary>
        /// 根据用户编号查询
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public Account GetAccountById(string id)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            return ir.GetByKey(id);
        }
        /// <summary>
        /// 分页遍历用户按照时间升序
        /// </summary>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        public List<Account> FetchAccount(int index, int count)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            return ir.Table.Skip((index-1) * count).Take(count).OrderBy(a => a.Created).ToList();
        }
        /// <summary>
        /// 根据用户名模糊查询
        /// </summary>
        /// <param name="name">用户名片段</param>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        public List<Account> FetchAccountByAccountName(string name, int index, int count)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            return ir.Table.Where(a=>a.Name.Contains(name)).Skip((index - 1) * count).Take(count).OrderBy(a => a.Created).ToList();
        }

        /// <summary>
        /// 获取用户总数
        /// </summary>
        /// <param name="hasForbidden">是否包含禁用用户</param>
        /// <returns></returns>
        public int CountAllAccount(bool hasForbidden)
        {
            IRepository<Account> ir = null;
            ir = AutofacManager<IRepository<Account>>.GetConcrete<DefaultRepository<Account>>();
            if (!hasForbidden)
            {
                return ir.Table.Count(a => a.IsForbidden == false);
            }
            else
            {
                return ir.Table.Count();
            }
        }
        #endregion 
    }
}
