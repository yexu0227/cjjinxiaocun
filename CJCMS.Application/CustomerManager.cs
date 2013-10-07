using CJCMS.Contracts;
using CJCMS.Contracts.DTO.Customer;
using CJCMS.Contracts.Service;
using CJCMS.Domain.Entity;
using CJCMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Application
{
    public sealed class CustomerManager : ICustomerManager
    {
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="c"></param>
        public void AddCustomer(CustomerDTO c)
        {
            TValidationHelper<CustomerDTO>.TValidation(c);
            CustomerService service = new CustomerService();

            service.AddCustomer(AutoMapper.Mapper.Map<CustomerDTO, Customer>(c));
        }

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="c"></param>
        public void UpdateCustomer(CustomerInfo c)
        {
            TValidationHelper<CustomerInfo>.TValidation(c);
            CustomerService service = new CustomerService();

            service.UpdateCustomer(AutoMapper.Mapper.Map<CustomerInfo, Customer>(c));
        }

        /// <summary>
        /// 设置客户可用
        /// </summary>
        /// <param name="c"></param>
        public void SetCustomerOn(CustomerStatusDTO c) 
        {
            TValidationHelper<CustomerStatusDTO>.TValidation(c);
            CustomerService service = new CustomerService();

            service.SetCustomerOn(c.Id);
        }

        /// <summary>
        /// 设置客户不可用
        /// </summary>
        /// <param name="c"></param>
        public void SetCustomerOff(CustomerStatusDTO c)
        {
            TValidationHelper<CustomerStatusDTO>.TValidation(c);
            CustomerService service = new CustomerService();

            service.SetCustomerOff(c.Id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public IList<CustomerInfo> FetchCustomer(int index, int pagecount, out int totalCount)
        {
            CustomerService service = new CustomerService();

            return AutoMapper.Mapper.Map <IList<Customer>, IList<CustomerInfo>>(service.FetchAll(index, pagecount, out totalCount));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public IList<CustomerInfo> FetchCustomerByStatus(int status, int index, int pagecount, out int totalCount)
        {
            CustomerService service = new CustomerService();

            return AutoMapper.Mapper.Map<IList<Customer>, IList<CustomerInfo>>(service.FetchByStatus(status,index, pagecount, out totalCount));
        }

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        public void ImportCustomerFromExcel()
        { }

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        public void ExportCustomerToExcel()
        { }
    }
}
