using CJCMS.Application;
using CJCMS.Contracts.DTO;
using CJCMS.Contracts.DTO.Category;
using CJCMS.Contracts.DTO.Product;
using CJCMS.Framework.JqueryDataTable;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CJCMS.Web.Company.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        #region 系统初始化页面
        [CJAuthorize(Role ="Admin,Call")]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 分类管理
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(FormCollection form)
        {
            CategoryInfo a = new CategoryInfo();
            a.CategoryName = form[""];
            a.ExInfo = form[""];
            a.IconName = form[""];
            a.ParentId = form[""];
            a.SortNum = Int32.Parse(form[""]);
            if (form["status"] == null)
            {
                a.Status = "off";
            }
            else
            {
                a.Status = form["status"];
            }
            CategoryManager cm = new CategoryManager();
            string info = string.Empty;
            try
            {
                cm.AddCategory(a);
                info = "";
            }
            catch (Exception ee)
            {
                info = ee.Message;
            }

            return Content(info);
        }

        public ActionResult UpdateCategory(string id)
        {
            CategoryManager cm = new CategoryManager();
            CategoryInfo a = cm.GetOneById(id);
            return View(a);
        }


        public ActionResult CategoryList()
        {
            return View();
        }

        public ActionResult GetCategoryList()
        {
            CategoryManager am = new CategoryManager();
            Int32 index = 1;
            int pagecount = Int32.Parse(Request["iDisplayLength"]);
            if (Request["iDisplayStart"] != null)
            {
                index = Int32.Parse(Request["iDisplayStart"]) / pagecount + 1;
            }
            if (Request["sSearch"].Trim().ToString().Equals(string.Empty))
            {
                JqueryDataTableHelper jdt = new JqueryDataTableHelper();
                return Content(jdt.ToJqueryDataTableJson(am.FetchAll(index, pagecount), Request["sEcho"], pagecount, am.AllCount()));
            }
            else
            {
                JqueryDataTableHelper jdt = new JqueryDataTableHelper();
                return Content(jdt.ToJqueryDataTableJson(am.FetchCategoryListByName(Request["sSearch"], index, pagecount), Request["sEcho"], pagecount, am.SearchByNameCount(Request["sSearch"].Trim())));
            }
        }

        [HttpPost]
        public ActionResult DeleteCategory(string id)
        {

            CategoryManager cm = new CategoryManager();
            string info = string.Empty;
            try
            {
                cm.DeleteCategory(id);
                info = "success";
            }
            catch (Exception ee)
            {
                info = ee.Message;
            }

            return Content(info);
        }
        #endregion

        #region 后台菜单操作
        public ActionResult MenuPartial()
        {
            IList<CategoryDTO> categorylist = new List<CategoryDTO>();
            return View(categorylist);
        }

        [HttpPost]
        public ActionResult Menu(string id)
        {
            CategoryManager c = new CategoryManager();
            IList<CategoryInfo> categorylist = c.FetchByPCategoryId(id);
            return Content(GetMenuString(categorylist));
        }

        private string GetMenuString(IList<CategoryInfo> categorylist)
        {
            if (categorylist == null || categorylist.Count < 1) return string.Empty;
            string menu = "<ul class=\'sub-menu\'>";
            foreach (CategoryInfo info in categorylist) {
                menu += "<li ><a class=\'ajaxify\' href=\'"+info.ExInfo+"\'><i class=\""+info.IconName+"\"></i>"+info.CategoryName+"</a></li>";
            }
            menu += "</ul>";
            return menu;
        }
        #endregion

        #region 数据字典管理
        public ActionResult DataCenter()
        {
            CategoryManager cm = new CategoryManager();
            //string treeinfo = "{ id:-1, pId:0, name:1, open:true }";
            string treeinfo = "";
            IList<CategoryInfo> list = cm.FetchAll();
            foreach (CategoryInfo ca in list)
            {
                treeinfo += "{ id:'" + ca.Id + "', pId:'" + ca.ParentId + "', name:'" + ca.CategoryName + "',csort:'" + ca.SortNum + "',cicon:'" + ca.IconName + "',cinfo:'" + ca.ExInfo + "',cstatus:'" + ca.Status + "'},";
            }
            //string treeinfo = "{ id:1, pId:0, name:12, open:true},{ id:11, pId:1, name:32}";
            ViewBag.TreeContent =treeinfo;
            //return JavaScript(treeinfo);
            return View();
        }

        [HttpPost]
        public ActionResult DataCenterManager(FormCollection form)
        {
            CategoryInfo a = new CategoryInfo();
            if (form["cid"] != null && form["cid"] != string.Empty)
            {

                a.Id = form["cid"];
            }
            a.CategoryName = form["cname"];
            a.ExInfo = form["cinfo"];
            a.IconName = form["cicon"];
            a.ParentId = form["pId"];
            a.SortNum = Int32.Parse(form["csort"]);
            if (form["cstatus"] == null)
            {
                a.Status = "off";
            }
            else
            {
                a.Status = form["cstatus"];
            }
            CategoryManager cm = new CategoryManager();
            string info = string.Empty;
            try
            {
                if (a.Id ==null||a.Id==string.Empty)
                {
                    cm.AddCategory(a);
                }
                else
                {
                    cm.UpdateCategory(a);
                }
                info = "success";
            }
            catch (Exception ee)
            {
                info = ee.Message;
            }
            return Content(info);
        }
        public ActionResult TreeContent()
        {
            return View();
        }

        public ActionResult ChangeCategoryRoot(string id,string newpid)
        {
            CategoryManager cm = new CategoryManager();
            string info = string.Empty;
            try
            {
                cm.UpdateCategoryRoot(id, newpid);
                info = "success";
            }
            catch (Exception ee)
            {
                info = ee.Message;
            }
            return Content(info);
        }
        #endregion

        #region 上传接口
        [HttpPost]
        public ActionResult UploadImageRespUrl()
        {
            String aspxUrl = "";

            //文件保存目录路径
            String savePath = "../upload/";

            //文件保存目录URL
            String saveUrl = aspxUrl + "../upload/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("images", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
            HttpPostedFileBase imgFile = null;
            if (Request.Files["Filedata"] != null)
            {
                imgFile = Request.Files["Filedata"];
            }
            if (Request.Files["imgFile"] != null)
            {
                imgFile = Request.Files["imgFile"];
            }
            if (imgFile == null)
            {
                Json("请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                Json("上传目录不存在。");
            }

            //String dirName = context.Request.QueryString["dir"];
            String dirName = "";
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "images";
            }
            if (!extTable.ContainsKey(dirName))
            {
                Json("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                Json("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                Json("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;
            return Content(fileUrl); ;
        }

        [HttpPost]
        public ActionResult UploadImgKindEditor()
        {
            String aspxUrl = "";

            //文件保存目录路径
            String savePath = "../upload/";

            //文件保存目录URL
            String saveUrl = aspxUrl + "../upload/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;

            HttpPostedFileBase imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;

            return Json(hash);
           
        }


        [HttpPost]
        public ActionResult UploadFileKindEditor()
        {
            String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

            //文件保存目录路径
            String savePath = "../upload/";

            //文件保存目录URL
            String saveUrl = aspxUrl + "../upload/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;

            HttpPostedFileBase imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            //context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            //context.Response.Write(JsonMapper.ToJson(hash));
            //context.Response.End();
            return Json(hash);
        }

        private ActionResult showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            return Json(hash);
        }
        #endregion

    }
}
