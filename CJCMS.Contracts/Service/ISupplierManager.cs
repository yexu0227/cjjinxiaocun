using CJCMS.Contracts.DTO.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Contracts.Service
{
    public interface ISupplierManager
    {
        /// <summary>
        /// 添加供应商
        /// </summary>
        /// <param name="c"></param>
        void AddSupplier(SupplierDTO c);

        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="c"></param>
        void UpdateSupplier(SupplierInfo c);

        /// <summary>
        /// 设置供应商可用
        /// </summary>
        /// <param name="c"></param>
        void SetSupplierOn(SupplierStatusDTO c);

        /// <summary>
        /// 设置供应商不可用
        /// </summary>
        /// <param name="c"></param>
        void SetSupplierOff(SupplierStatusDTO c);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<SupplierInfo> FetchSupplier(int index, int pagecount, out int totalCount);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<SupplierInfo> FetchSupplierByStatus(int status, int index, int pagecount, out int totalCount);

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        void ImportSupplierFromExcel();

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        void ExportSupplierToExcel();
    }
}
