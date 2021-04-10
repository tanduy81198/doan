using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSOnline.Models;

namespace TSOnline.Controllers
{
    public class CategoryController : Controller
    {
        dbQLTraSuaDataContext data = new dbQLTraSuaDataContext();
        // GET: TraSua


        // GET: Category
        public ActionResult Index(int ? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
          
            var list_ts = data.TRASUAs.ToList();
            return View(list_ts.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Category_Partialview()
        {
            var list_category = data.LOAIs.ToList();
            
            return PartialView(list_category);
        }

        public ActionResult Category_Shop(int? page, int id)
        {
            // tao số sp trên trang
            int pageSize = 9;
            int pageNum = (page ?? 1);
            //lấy top bán chạy nhất
            var tsmoi = data.TRASUAs.ToList().Where(a => a.MaLoai == id).ToList();
            ViewBag.TenLoai = data.LOAIs.Where(a => a.MaLoai == id).FirstOrDefault().TenLoai;
            return View(tsmoi.ToPagedList(pageNum, pageSize));
        }
    }
}