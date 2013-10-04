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
using CJCMS.Contracts.DTO;

namespace CJCMS.Contracts.Service
{
    public interface IAccountManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logonInfo">登录信息</param>
        LogonResult Logon(LogonDTO logonInfo);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registInfo">注册信息</param>
        void Register(RegisterDTO registInfo);

        /// <summary>
        /// 根据注册邮箱禁用用户
        /// </summary>
        /// <param name="email"></param>
        void ForbiddenByEmail(string email);

        /// <summary>
        /// 根据用户编号禁用用户
        /// </summary>
        /// <param name="id"></param>
        void ForbiddenById(string id);

        /// <summary>
        /// 根据邮箱解禁
        /// </summary>
        /// <param name="email"></param>
        void UnForbiddenByEmail(string email);

        /// <summary>
        /// 根据用户编号解禁
        /// </summary>
        /// <param name="id"></param>
        void UnForbiddenById(string id);

        /// <summary>
        /// 分页遍历用户
        /// </summary>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        IList<AccountInfo> FetchAll(int index, int count);

        /// <summary>
        /// 根据部分用户名模糊查询用户
        /// </summary>
        /// <param name="partname">部分用户名</param>
        /// <param name="index">页序号</param>
        /// <param name="count">一页总数</param>
        /// <returns></returns>
        IList<AccountInfo> FetchByName(string partname, int index, int count);

        /// <summary>
        /// 根据邮箱获取用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        AccountInfo GetAccountByEmail(string email);

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        AccountInfo GetAccountById(string id);

        /// <summary>
        /// 检查邮箱是否注册过
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool ValidateEmail(string email);
    }
}
