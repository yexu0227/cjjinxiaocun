using CJCMS.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CJCMS.Web.Company.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult News(string id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Tag(string id)
        {
            return View();
        }
    }
}
