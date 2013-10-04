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
using CJCMS.Contracts.DTO;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using CJCMS.Domain.Service;
using CJCMS.Domain.Entity;
using CJCMS.Framework.Security;
using CJCMS.Framework.Logging;
using CJCMS.Contracts;
using CJCMS.Framework.Utils;

namespace CJCMS.Application
{
    public class AccountManager : IAccountManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logonInfo">登录信息</param>
        public LogonResult Logon(LogonDTO logonInfo)
        {
            ///验证用户输入
            TValidationHelper<LogonDTO>.TValidation(logonInfo);

            AccountService service = new AccountService();
            Account a=null;
            try
            {
                a= service.GetAccountByEmail(logonInfo.Email);
            }
            catch
            {
                LogHelper.WriteLog(logonInfo.Email + " 不存在此邮箱用户时强制登陆");
                throw new AccountNullException("不存在该用户");
            }

            
            return AutoMapper.Mapper.Map<Account, LogonResult>(service.Logon(AutoMapper.Mapper.Map<LogonDTO, Account>(logonInfo)));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registInfo">注册信息</param>
        public void Register(RegisterDTO registInfo)
        {
            ///验证用户输入
            TValidationHelper<RegisterDTO>.TValidation(registInfo);

            AccountService service = new AccountService();
            if (service.ValidateEmail(registInfo.Email))
            {
                LogHelper.WriteLog(registInfo.Email + " 存在此邮箱用户，尝试反复注册");
                throw new EmailUnableException("此邮箱已经注册过");
            }
            service.Register(AutoMapper.Mapper.Map<RegisterDTO, Account>(registInfo));
        }

        /// <summary>
        /// 根据注册邮箱禁用用户
        /// </summary>
        /// <param name="email"></param>
        public void ForbiddenByEmail(string email) 
        {
            if (!Utils.IsValidEmail(email))
            {
                LogHelper.WriteLog(email +"格式有问题");
                throw new EmailValidateException("邮箱格式有误");
            }
            AccountService service = new AccountService();
            Account a = null;
            try
            {
                a = service.GetAccountByEmail(email);
            }
            catch
            {
                LogHelper.WriteLog(email + "不存在此邮箱的用户，任然操作禁用");
                throw new AccountNullException("不存在此邮箱的用户");
            }
            service.ForbiddenAccount(a.Id);
        }

        /// <summary>
        /// 根据用户编号禁用用户
        /// </summary>
        /// <param name="id"></param>
        public void ForbiddenById(string id)
        {
            AccountService service = new AccountService();

            try
            {
                service.GetAccountById(id);
            }
            catch
            {
                LogHelper.WriteLog(id + "不存在此编号的用户，任然操作禁用");
                throw new AccountNullException("不存在此编号的用户");
            }
            service.ForbiddenAccount(id);
        }

        /// <summary>
        /// 根据邮箱解禁
        /// </summary>
        /// <param name="email"></param>
        public void UnForbiddenByEmail(string email)
        {
            if (!Utils.IsValidEmail(email))
            {
                LogHelper.WriteLog(email + "格式有问题");
                throw new EmailValidateException("邮箱格式有误");
            }
            AccountService service = new AccountService();
            Account a = null;
            try
            {
                a = service.GetAccountByEmail(email);
            }
            catch
            {
                LogHelper.WriteLog(email + "不存在此邮箱的用户，任然操作禁用");
                throw new AccountNullException("不存在此邮箱的用户");
            }
            service.UnForbiddenAccount(a);
        }

        /// <summary>
        /// 根据用户编号解禁
        /// </summary>
        /// <param name="id"></param>
        public void UnForbiddenById(string id)
        {
            AccountService service = new AccountService();

            try
            {
                service.GetAccountById(id);
            }
            catch
            {
                LogHelper.WriteLog(id + "不存在此编号的用户，任然操作禁用");
                throw new AccountNullException("不存在此编号的用户");
            }
            service.ForbiddenAccount(id);
        }

        /// <summary>
        /// 分页遍历用户
        /// </summary>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        public IList<AccountInfo> FetchAll(int index, int count)
        {
            AccountService service = new AccountService();
            
            return AutoMapper.Mapper.Map<IList<Account>, IList<AccountInfo>>(service.FetchAccount(index, count));
        }

        /// <summary>
        /// 根据部分用户名模糊查询用户
        /// </summary>
        /// <param name="partname">部分用户名</param>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        public IList<AccountInfo> FetchByName(string partname, int index, int count)
        {
            AccountService service = new AccountService();
           
            return AutoMapper.Mapper.Map<IList<Account>, IList<AccountInfo>>(service.FetchAccountByAccountName(partname,index, count));
        }

        /// <summary>
        /// 根据邮箱获取用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public AccountInfo GetAccountByEmail(string email)
        {
            if (!Utils.IsValidEmail(email))
            {
                LogHelper.WriteLog(email + "格式有问题");
                throw new EmailValidateException("邮箱格式有误");
            }
            AccountService service = new AccountService();
            try
            {
                return AutoMapper.Mapper.Map<Account, AccountInfo>(service.GetAccountByEmail(email));
            }
            catch
            {
                throw new AccountNullException("不存在此邮箱的用户");
            }
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public AccountInfo GetAccountById(string id)
        {
            AccountService service = new AccountService();
            try
            {
                return AutoMapper.Mapper.Map<Account, AccountInfo>(service.GetAccountById(id));
            }
            catch
            {
                throw new AccountNullException("不存在此编号的用户");
            }
        }

        /// <summary>
        /// 检查邮箱是否注册过
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ValidateEmail(string email)
        {
            if (!Utils.IsValidEmail(email))
            {
                LogHelper.WriteLog(email + "格式有问题");
                throw new EmailValidateException("邮箱格式有误");
            }
            AccountService service = new AccountService();
            return service.ValidateEmail(email);
        }
    }
}
