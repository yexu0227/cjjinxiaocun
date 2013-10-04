using CJCMS.Contracts.DTO;
using CJCMS.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CJCMS.Web.Company
{
    public class CJAuthorizeAttribute : CJAuthorizeAttributeBase
    {
        public override bool CheckMission(string role)
        {
            FormsIdentity id = (FormsIdentity)contextbase.User.Identity;
            CJCMS.Contracts.DTO.LogonResult lr = (CJCMS.Contracts.DTO.LogonResult)Newtonsoft.Json.JsonConvert.DeserializeObject(id.Ticket.UserData, typeof(CJCMS.Contracts.DTO.LogonResult));
            string[] roles = lr.Role.Split(',');
            return roles.Contains(role);
        }
    }
}