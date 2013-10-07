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
using System.Xml;
using CJCMS.Contracts.Service;
using CJCMS.Contracts.DTO;
using CJCMS.Domain.Entity;
using CJCMS.Contracts.DTO.Category;
using CJCMS.Contracts.DTO.Product;
using CJCMS.Contracts.DTO.Customer;
using CJCMS.Contracts.DTO.Supplier;


namespace CJCMS.Application
{
    public class CoreManager : ICoreManager
    {
        public void BeforeDoCoreJob()
        {
            #region 用户
            //用户
            AutoMapper.Mapper.CreateMap<LogonDTO, Account>();
            AutoMapper.Mapper.CreateMap<Account, LogonResult>();
            AutoMapper.Mapper.CreateMap<RegisterDTO, Account>();
            AutoMapper.Mapper.CreateMap<Account, AccountInfo>();
            #endregion

            #region 顾客
            //顾客
            AutoMapper.Mapper.CreateMap<CustomerInfo, Customer>();
            AutoMapper.Mapper.CreateMap<CustomerDTO, Customer>();
            AutoMapper.Mapper.CreateMap<CustomerStatusDTO, Customer>();
            AutoMapper.Mapper.CreateMap<Customer, CustomerInfo>();
            #endregion

            #region 供应商
            //供应商
            AutoMapper.Mapper.CreateMap<SupplierInfo, Supplier>();
            AutoMapper.Mapper.CreateMap<SupplierDTO, Supplier>();
            AutoMapper.Mapper.CreateMap<SupplierStatusDTO, Supplier>();
            AutoMapper.Mapper.CreateMap<Supplier, SupplierInfo>();
            #endregion

            #region 分类
            //分类
            AutoMapper.Mapper.CreateMap<Category, CategoryInfo>();
            AutoMapper.Mapper.CreateMap<CategoryInfo, Category>();
            #endregion

            #region 产品
            //产品
            AutoMapper.Mapper.CreateMap<ProductInfo, Product>();
            AutoMapper.Mapper.CreateMap<ProductDTO, Product>();
            AutoMapper.Mapper.CreateMap<ProductInfo, Product>();
            #endregion
        }

        public void AfterDoCoreJob()
        {

        }
    }
}
