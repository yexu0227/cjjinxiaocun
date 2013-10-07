using CJCMS.Contracts;
using CJCMS.Contracts.DTO.Supplier;
using CJCMS.Contracts.Service;
using CJCMS.Domain.Entity;
using CJCMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Application
{
    public sealed class SupplierManager : ISupplierManager
    {
        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="c"></param>
        public void AddSupplier(SupplierDTO c)
        {
            TValidationHelper<SupplierDTO>.TValidation(c);
            SupplierService service = new SupplierService();

            service.AddSupplier(AutoMapper.Mapper.Map<SupplierDTO, Supplier>(c));
        }

        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="c"></param>
        public void UpdateSupplier(SupplierInfo c)
        {
            TValidationHelper<SupplierInfo>.TValidation(c);
            SupplierService service = new SupplierService();

            service.UpdateSupplier(AutoMapper.Mapper.Map<SupplierInfo, Supplier>(c));
        }

        /// <summary>
        /// 设置供应商可用
        /// </summary>
        /// <param name="c"></param>
        public void SetSupplierOn(SupplierStatusDTO c)
        {
            TValidationHelper<SupplierStatusDTO>.TValidation(c);
            CustomerService service = new CustomerService();

            service.SetCustomerOn(c.Id);
        }

        /// <summary>
        /// 设置供应商不可用
        /// </summary>
        /// <param name="c"></param>
        public void SetSupplierOff(SupplierStatusDTO c)
        {
            TValidationHelper<SupplierStatusDTO>.TValidation(c);
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
        public IList<SupplierInfo> FetchSupplier(int index, int pagecount, out int totalCount)
        {
            SupplierService service = new SupplierService();

            return AutoMapper.Mapper.Map<IList<Supplier>, IList<SupplierInfo>>(service.FetchAll(index, pagecount, out totalCount));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public IList<SupplierInfo> FetchSupplierByStatus(int status, int index, int pagecount, out int totalCount)
        {
            SupplierService service = new SupplierService();

            return AutoMapper.Mapper.Map<IList<Supplier>, IList<SupplierInfo>>(service.FetchByStatus(status,index, pagecount, out totalCount));
        }

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        public void ImportSupplierFromExcel()
        { 
        
        }

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        public void ExportSupplierToExcel()
        { 
        
        }
    }
}
