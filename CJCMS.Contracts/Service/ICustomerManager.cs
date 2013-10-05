using CJCMS.Contracts.DTO.Customer;
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

namespace CJCMS.Contracts.Service
{
    public interface ICustomerManager
    {
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="c"></param>
        void AddCustomer(CustomerDTO c);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="c"></param>
        void UpdateCustomer(CustomerInfo c);

        /// <summary>
        /// 设置客户可用
        /// </summary>
        /// <param name="c"></param>
        void SetCustomerOn(CustomerStatusDTO c);

        /// <summary>
        /// 设置客户不可用
        /// </summary>
        /// <param name="c"></param>
        void SetCustomerOff(CustomerStatusDTO c);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<CustomerInfo> FetchCustomer(int index, int pagecount, out int totalCount);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<CustomerInfo> FetchCustomerByStatus(int status, int index, int pagecount, out int totalCount);

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        void ImportCustomerFromExcel();

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        void ExportCustomerToExcel();
    }
}
