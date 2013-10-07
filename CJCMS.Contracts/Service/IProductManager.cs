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
using CJCMS.Contracts.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJCMS.Contracts.Service
{
    public interface IProductManager
    {
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="p"></param>
        void AddProduct(ProductDTO p);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="p"></param>
        void UpdateProduct(ProductInfo p);

        /// <summary>
        /// 设置商品上线
        /// </summary>
        /// <param name="p"></param>
        void SetProductOn(ProductStatusDTO p);

        /// <summary>
        /// 设置商品下线
        /// </summary>
        /// <param name="p"></param>
        void SetProductOff(ProductStatusDTO p);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<ProductInfo> FetchProductByCategory(string categoryId,int index,int pagecount,out int totalCount);

        /// <summary>
        /// 按照商品名称模糊查询
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="name">商品名称</param>
        /// <param name="index">页码</param>
        /// <param name="pagecount">页大小</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        IList<ProductInfo> FetchProductByCategoryAndName(string categoryId,string name, int index, int pagecount, out int totalCount);

        /// <summary>
        /// 从Excel导入商品到数据库
        /// </summary>
        void ImportProductFromExcel();

        /// <summary>
        /// 从数据库导出到Excel
        /// </summary>
        void ExportProductToExcel();
    }
}
