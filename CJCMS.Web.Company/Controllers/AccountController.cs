using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CJCMS.Web.Company.Models;
using CJCMS.Contracts.DTO;
using System.Collections.Specialized;
using System.Text;
using CJCMS.Contracts.Service;
using CJCMS.Application;
using CJCMS.Framework.Security;
using Newtonsoft.Json;

namespace CJCMS.Web.Company.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogonDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IAccountManager ac = new AccountManager();
                LogonResult lr=  ac.Logon(model);
                if (lr!=null)
                {
                    //记录用户登录信息
                    AuthenticationHelper.SetAuthenticalCookie(AuthenticationHelper.CreateAuthenticationTicket(model.Email, JsonConvert.SerializeObject(lr), model.IsRemember));
                    
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Admin");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            RegisterDTO model = new RegisterDTO();
            model.Email=form["Email"];
            model.Name=form["username"];
            model.PlantPwd = form["password"];
            try
            {
                // 尝试注册用户
                IAccountManager ac = new AccountManager();
                ac.Register(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ee)
            {
                // 如果我们进行到这一步时某个地方出错，则重新显示表单
                return Content(ee.Message);
            }
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // 在某些出错情况下，ChangePassword 将引发异常，
                // 而不是返回 false。
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "当前密码不正确或新密码无效。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入不同的用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入不同的电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        #endregion
    }




//    public class DbMembershipProvider : MembershipProvider 
//     { 
//         private string applicationName; 
//         private MembershipPasswordFormat passwordFormat; 

//         /// <summary> 
//         /// Initializes the provider 
//         /// </summary> 
//         /// <param name="name">Configuration name</param> 
//         /// <param name="config">Configuration settings</param> 
//        public override void Initialize(string name, NameValueCollection config) 
//         { 
//             //if (config == null) 
//             //{ 
//             //    throw new ArgumentNullException("config"); 
//             //} 

//             //if (String.IsNullOrEmpty(name)) 
//             //{ 
//             //    name = "DbMembershipProvider"; 
//             //} 
//             //applicationName = config["applicationName"]; 
//             //passwordFormat = MembershipPasswordFormat.Hashed; 

//             base.Initialize(name, config); 

//         } 

//         /// <summary> 
//         /// Add new user to database 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="password"></param> 
//         /// <param name="email"></param> 
//         /// <param name="passwordQuestion"></param> 
//         /// <param name="passwordAnswer"></param> 
//         /// <param name="isApproved"></param> 
//         /// <param name="providerUserKey"></param> 
//         /// <param name="status"></param> 
//         /// <returns></returns> 
//        public override MembershipUser CreateUser(string username, string password, string email, 
//                                                   string passwordQuestion, string passwordAnswer, bool isApproved, 
//                                                   object providerUserKey, out MembershipCreateStatus status) 
//         {
//             IAccountManager accountManager = new AccountManager();
//             accountManager.Logon(

//             status = MembershipCreateStatus.Success; 

//             return GetMembershipUser(username, email); 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="password"></param> 
//         /// <param name="newPasswordQuestion"></param> 
//         /// <param name="newPasswordAnswer"></param> 
//         /// <returns></returns> 
//        public override bool ChangePasswordQuestionAndAnswer(string username, string password, 
//                                                              string newPasswordQuestion, string newPasswordAnswer) 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="answer"></param> 
//         /// <returns></returns> 
//        public override string GetPassword(string username, string answer) 
//         { 
//             throw new NotImplementedException(); 
//         } 
         
         
//         /// <summary> 
//         /// Change the password if the old password matches what is stored 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="oldPassword"></param> 
//         /// <param name="newPassword"></param> 
//         /// <returns></returns> 
//        public override bool ChangePassword(string username, string oldPassword, string newPassword) 
//         { 
//             //MVCUser mvcuser = GetUserByUserName(username); 
//             //if (mvcuser == null) 
//             //    return false; 
//             //if (mvcuser.Password == HashPassword(oldPassword)) 
//             //{ 
//             //    mvcuser.Password = HashPassword(newPassword); 
//             //    db.SubmitChanges(); 
//             //    return true; 
//             //} 
//             return false; 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="answer"></param> 
//         /// <returns></returns> 
//        public override string ResetPassword(string username, string answer) 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Update User Data (not password) 
//         /// </summary> 
//         /// <param name="user"></param> 
//        public override void UpdateUser(MembershipUser user) 
//         { 
//             //var a = from muser in db.MVCUser where muser.UserName == user.UserName select muser; 
//             //if (a.Count() == 0) 
//             //    return; 
//             //MVCUser mvcuser = a.First(); 
//             //mvcuser.EmailAddress = user.Email; 
//             //db.SubmitChanges(); 
//             //string connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString; 
//             //string providerName = ConfigurationManager.ConnectionStrings[connStringName].ProviderName; 
//             //DbProviderFactory provider = DbProviderFactories.GetFactory(providerName); 

//             //using (DbConnection conn = provider.CreateConnection()) 
//             //{ 
//             //    conn.ConnectionString = connString; 

//             //    using (DbCommand cmd = conn.CreateCommand()) 
//             //    { 
//             //        cmd.CommandText = "UPDATE " + tablePrefix + "Users SET emailAddress = " + parmPrefix + "email WHERE userName = " + parmPrefix + "name"; 
//             //        cmd.CommandType = CommandType.Text; 

//             //        conn.Open(); 

//             //        DbParameter dpName = provider.CreateParameter(); 
//             //        dpName.ParameterName = parmPrefix + "name"; 
//             //        dpName.Value = user.UserName; 
//             //        cmd.Parameters.Add(dpName); 
//             //        DbParameter dpEmail = provider.CreateParameter(); 
//             //        dpEmail.ParameterName = parmPrefix + "email"; 
//             //        dpEmail.Value = user.Email; 
//             //        cmd.Parameters.Add(dpEmail); 

//             //        cmd.ExecuteNonQuery(); 
//             //    } 
//             //} 
//        } 

//         /// <summary> 
//         /// Check username and password 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="password"></param> 
//         /// <returns></returns> 
//        public override bool ValidateUser(string username, string password) 
//         { 

//             //var a = from muser in db.MVCUser where muser.UserName == username && muser.Password == HashPassword(password) select muser; 
//             //if (a == null || a.Count() == 0) 
//             //    return false; 
//             return true; 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="userName"></param> 
//         /// <returns></returns> 
//        public override bool UnlockUser(string userName) 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Get User by providerUserKey 
//         /// </summary> 
//         /// <param name="providerUserKey"></param> 
//         /// <param name="userIsOnline"></param> 
//         /// <returns></returns> 
//        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) 
//         { 
//             return GetUser(providerUserKey.ToString(), userIsOnline); 
//         } 

//         /// <summary> 
//         /// Get User by username 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="userIsOnline"></param> 
//         /// <returns></returns> 
//        public override MembershipUser GetUser(string username, bool userIsOnline) 
//         { 

//             //MVCUser mvcuser = GetUserByUserName(username); 
//             //if (mvcuser == null) 
//             //    return null; 
//             return null;
//         } 

//         /// <summary> 
//         /// Retrieve UserName for given email 
//         /// </summary> 
//         /// <param name="email"></param> 
//         /// <returns></returns> 
//        public override string GetUserNameByEmail(string email) 
//         { 
//             //var a = from muser in db.MVCUser where muser.EmailAddress == email select muser; 

//             //if (a.Count() == 0) 
//             //    return null; 
//             //return a.First().UserName; 
//             return "";
//         } 

//         /// <summary> 
//         /// Delete user from database 
//         /// </summary> 
//         /// <param name="username"></param> 
//         /// <param name="deleteAllRelatedData"></param> 
//         /// <returns></returns> 
//        public override bool DeleteUser(string username, bool deleteAllRelatedData) 
//         { 
//             //var a = from muser in db.MVCUser where muser.UserName == username select muser; 
//             //if (a.Count() == 0) 
//             //    return false; 
//             //db.MVCUser.DeleteOnSubmit(a.First()); 
//             //db.SubmitChanges(); 
//             return true; 
//         } 

//         /// <summary> 
//         /// Return all users in MembershipUserCollection 
//         /// </summary> 
//         /// <param name="pageIndex"></param> 
//         /// <param name="pageSize"></param> 
//         /// <param name="totalRecords"></param> 
//         /// <returns></returns> 
//        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) 
//         { 
//             MembershipUserCollection users = new MembershipUserCollection();
//             totalRecords = 0;
//             //var a = from muser in db.MVCUser select muser; 
//             //foreach (MVCUser user in a) 
//             //{ 
//             //    users.Add(GetMembershipUser(user.UserName, user.EmailAddress)); 
//             //} 
//             //totalRecords = users.Count; 
//             return users; 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <returns></returns> 
//        public override int GetNumberOfUsersOnline() 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="usernameToMatch"></param> 
//         /// <param name="pageIndex"></param> 
//         /// <param name="pageSize"></param> 
//         /// <param name="totalRecords"></param> 
//         /// <returns></returns> 
//        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, 
//                                                                  out int totalRecords) 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//         /// <param name="emailToMatch"></param> 
//         /// <param name="pageIndex"></param> 
//         /// <param name="pageSize"></param> 
//         /// <param name="totalRecords"></param> 
//         /// <returns></returns> 
//        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, 
//                                                                   out int totalRecords) 
//         { 
//             throw new NotImplementedException(); 
//         } 

//         /// <summary> 
//         /// Can password be retrieved via email? 
//         /// </summary> 
//        public override bool EnablePasswordRetrieval 
//         { 
//             get { return false; } 
//         } 

//         /// <summary> 
//         /// Hardcoded to false 
//         /// </summary> 
//        public override bool EnablePasswordReset 
//         { 
//             get { return false; } 
//         } 

//         /// <summary> 
//         /// Hardcoded to false 
//         /// </summary> 
//        public override bool RequiresQuestionAndAnswer 
//         { 
//             get { return false; } 
//         } 

//         /// <summary> 
//         /// Returns the application name as set in the web.config 
//         /// otherwise returns BlogEngine.  Set will throw an error. 
//         /// </summary> 
//        public override string ApplicationName 
//         { 
//             get { return applicationName; } 
//             set { throw new NotImplementedException(); } 
//         } 

//         /// <summary> 
//         /// Hardcoded to 5 
//         /// </summary> 
//        public override int MaxInvalidPasswordAttempts 
//         { 
//             get { return 5; } 
//         } 

//         /// <summary> 
//         /// Not implemented 
//         /// </summary> 
//        public override int PasswordAttemptWindow 
//         { 
//             get { throw new NotImplementedException(); } 
//         } 

//         /// <summary> 
//         /// Hardcoded to false 
//         /// </summary> 
//        public override bool RequiresUniqueEmail 
//         { 
//             get { return false; } 
//         } 

//         /// <summary> 
//         /// Password format (Clear or Hashed) 
//         /// </summary> 
//        public override MembershipPasswordFormat PasswordFormat 
//         { 
//             get { return passwordFormat; } 
//         } 

//         /// <summary> 
//         /// Hardcoded to 4 
//         /// </summary> 
//        public override int MinRequiredPasswordLength 
//         { 
//             get { return 4; } 
//         } 

//         /// <summary> 
//         /// Hardcoded to 0 
//         /// </summary> 
//        public override int MinRequiredNonAlphanumericCharacters 
//         { 
//             get { return 0; } 
//         } 

//         /// <summary> 
//         /// Not Implemented 
//         /// </summary> 
//        public override string PasswordStrengthRegularExpression 
//         { 
//             get { throw new NotImplementedException(); } 
//         } 

//         private MembershipUser GetMembershipUser(string userName, string email) 
//         { 
//             MembershipUser user = new MembershipUser( 
//                                     "DbMembershipProvider",                       // Provider name 
//                                    userName,                   // Username 
//                                    userName,                   // providerUserKey 
//                                    email,                      // Email 
//                                    String.Empty,               // passwordQuestion 
//                                    String.Empty,               // Comment 
//                                    true,                       // isApproved 
//                                    false,                      // isLockedOut 
//                                    DateTime.Now,               // creationDate 
//                                    DateTime.Now,                  // lastLoginDate 
//                                    DateTime.Now,               // lastActivityDate 
//                                    DateTime.Now,               // lastPasswordChangedDate 
//                                    new DateTime(1980, 1, 1)    // lastLockoutDate 
//                                ); 
//             return user; 
//         }        /// <summary> 
       
      
//}
}
