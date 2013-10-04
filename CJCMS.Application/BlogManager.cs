using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJCMS.Contracts.DTO.Blog;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using CJCMS.Domain.Entity.Blog;
using CJCMS.Contracts.Service.Blog;
using CJCMS.Domain.Service.Blog;

namespace CJCMS.Application
{
    public class BlogManager:IBlogService
    {
        public void Add(BlogDspModel b)
        {
            ValidationResults results = Validation.Validate<BlogDspModel>(b);
            
            string msg = string.Empty;
            if (!results.IsValid)
            {
                foreach (ValidationResult vr in results)
                {
                    msg += vr.Message;
                }
                throw new Exception(msg);
            }
            BlogService service = new BlogService();
            AutoMapper.Mapper.CreateMap<BlogDspModel, BlogPost>();
            service.Add(AutoMapper.Mapper.Map<BlogDspModel, BlogPost>(b));
        }

        public BlogDspModel GetByKey(string id)
        {
            BlogService service = new BlogService();
            return AutoMapper.Mapper.Map<BlogPost, BlogDspModel>(service.GetByKey(id));
        }

        public IList<BlogDspModel> Fetch()
        {
           AutoMapper.Mapper.CreateMap<BlogPost, BlogDspModel>();
           BlogService service = new BlogService();
           return AutoMapper.Mapper.Map<IList<BlogPost>, IList<BlogDspModel>>(service.Fetch());
        }
    }
}
