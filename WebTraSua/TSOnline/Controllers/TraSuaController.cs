using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSOnline.Models;
using PagedList;
using PagedList.Mvc;

namespace TSOnline.Controllers
{
    public class TraSuaController : Controller
    {
        dbQLTraSuaDataContext data = new dbQLTraSuaDataContext();
        // GET: TraSua

        private List<TRASUA> trasuamoi(int count)
        {
            return data.TRASUAs.OrderByDescending(a => a.MaTS).Take(count).ToList();
        }

        public ActionResult Index(int ? page , string search)
        {
            // tao số sp trên trang
            int pageSize = 6;
            int pageNum = (page ?? 1);
            //lấy top bán chạy nhất
            var tsmoi = listAll(search, pageNum, pageSize);
            return View(tsmoi.ToPagedList(pageNum,pageSize));
        }
        public IEnumerable<TRASUA> listAll(string search, int? page, int pageSize)
        {
            IQueryable<TRASUA> model = data.TRASUAs;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenTS.Contains(search));
            }
           
            return model;
        }
        public ActionResult GetTop()
        {
            //lấy top bán chạy nhất
            List<TRASUA> list_banchay = new List<TRASUA>();
            var topbanchay = data.topbanchay().ToList();
            foreach (var item in topbanchay)
            {
                TRASUA ts = data.TRASUAs.Where(a => a.MaTS == item.MaTS).SingleOrDefault();
                list_banchay.Add(ts);
            }
            return PartialView(list_banchay);
        }
       
        public ActionResult List_Banner()
        {
            List<LoadBannerResult> list_banner = data.LoadBanner().ToList();
            
            return PartialView(list_banner);
        }
        public ActionResult Loai()
        {
            var loai = from LOAI in data.LOAIs select LOAI;
            return PartialView(loai);
        }
        public ActionResult Details(int id)
        {
            var trasua = data.TRASUAs.Where(a=> a.MaTS == id).FirstOrDefault();
            ViewBag.TenSP = data.LOAIs.Where(a => a.MaLoai == trasua.MaLoai).FirstOrDefault().TenLoai;
            return View(trasua);
        }
        public ActionResult DS_Loai()
        {
            var list_loai = data.LOAIs.ToList();
            return PartialView(list_loai);
        }
        public ActionResult Related(int id)
        {
            var list_related = data.TRASUAs.Where(a => a.MaLoai == id).ToList();
            return PartialView(list_related);
        }
        

    }
}