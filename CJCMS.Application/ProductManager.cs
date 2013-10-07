using CJCMS.Contracts;
using CJCMS.Contracts.DTO.Product;
using CJCMS.Contracts.Service;
using CJCMS.Domain.Entity;
using CJCMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Application
{
    public sealed class ProductManager : IProductManager
    {
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(ProductDTO p)
        {
            TValidationHelper<ProductDTO>.TValidation(p);
            ProductService service = new ProductService();

            service.AddProduct(AutoMapper.Mapper.Map<ProductDTO, Product>(p));
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(ProductInfo p)
        {
            TValidationHelper<ProductInfo>.TValidation(p);
            ProductService service = new ProductService();

            service.UpdateProduct(AutoMapper.Mapper.Map<ProductInfo, Product>(p));
        }

        /// <summary>
        /// 设置商品上线
        /// </summary>
        /// <param name="p"></param>
        public void SetProductOn(ProductStatusDTO p)
        {
            TValidationHelper<ProductStatusDTO>.TValidation(p);
            ProductService service = new ProductService();

            service.SetProductOn(p.Id);
        }

        /// <summary>
        /// 设置商品下线
        /// </summary>
        /// <param name="p"></param>
        public void SetProductOff(ProductStatusDTO p)
        {
            TValidationHelper<ProductStatusDTO>.TValidation(p);
            ProductService service = new ProductService();

            service.SetProductOff(p.Id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public IList<ProductInfo> FetchProductByCategory(string categoryId, int index, int pagecount, out int totalCount)
        {
            ProductService service = new ProductService();

            return AutoMapper.Mapper.Map<IList<Product>, IList<ProductInfo>>(service.FetchAllByCategory(categoryId, index, pagecount, out totalCount));
        }

        /// <summary>
        /// 按照商品名称模糊查询
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="name">商品名称</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public IList<ProductInfo> FetchProductByCategoryAndName(string categoryId, string name, int index, int pagecount, out int totalCount)
        {
            ProductService service = new ProductService();

            return AutoMapper.Mapper.Map<IList<Product>, IList<ProductInfo>>(service.FetchAllByCategoryAndName(categoryId,name, index, pagecount, out totalCount));
        }

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        public void ImportProductFromExcel()
        {
        
        }

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        public void ExportProductToExcel()
        { 
        
        }
    }
}
