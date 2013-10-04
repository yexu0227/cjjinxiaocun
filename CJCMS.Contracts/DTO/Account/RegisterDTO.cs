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
using CJCMS.Framework.DomainOuter;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CJCMS.Contracts.DTO
{
    public class RegisterDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLengthValidator(5, 50, Ruleset = "RuleSetA", MessageTemplate = "用户长度必须介于5~50个字符")]
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLengthValidator(6, 50, Ruleset = "RuleSetA", MessageTemplate = "邮箱长度必须介于6~50个字符")]
        public string Email { get; set; }
        /// <summary>
        /// 纯文本密码
        /// </summary>
        [StringLengthValidator(6, 15, Ruleset = "RuleSetA", MessageTemplate = "密码长度必须介于6~15字符")]
        public string PlantPwd { get; set; }
        /// <summary>
        /// 注册类型
        /// </summary>
        public RegistType AccountType { get; set; }
    }
}
