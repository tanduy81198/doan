using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace TSOnline.Controllers
{
    public class AdminController : Controller
    {
        dbQLTraSuaDataContext data = new dbQLTraSuaDataContext();
        // GET: /Admin/
        public ActionResult Loai()
        {
            var loai = from tt in data.LOAIs select tt;
            return View(loai);
        }
        public ActionResult Details(int id)
        {
            var Details_tin = data.LOAIs.Where(m => m.MaLoai == id).First();
            return View(Details_tin);
        }
        //Hàm Create (get )tạo khung cho người sử dụng nhập liệu
        public ActionResult Create()
        {
            return View();
        }
        //Hàm Create(Post) xử lý dữ liệu được chuyền về từ trang Create.aspx
        //và trả về kết quả
        [HttpPost]
        public ActionResult Create(FormCollection collection, LOAI ltin)
        {
            // Tạo biến CB_Loaitin và gán giá trị của người dùng nhập vào từ
            //form trong trang Create.aspx
            var CB_Loaitin = collection["TenLoai"];
            //Nếu CB_Loaitin có giá trị == null ( để trống )
            if (string.IsNullOrEmpty(CB_Loaitin))
            {
                ViewData["Loi"] = " Thể loại Tin không được để trống ";
            }
            else
            {
                ltin.TenLoai = CB_Loaitin;
                data.LOAIs.InsertOnSubmit(ltin);
                //Thực hiện tạo mới
                data.SubmitChanges();
                return RedirectToAction("Loai");
            }
            return this.Create();
        }
        // GET:Hàm Edit(get) t ruyền thông số của đối tượng sang trang Edit.aspx
        //Với thông số là id.
        public ActionResult Edits(int id)
        {
            var EB_tin = data.LOAIs.First(m => m.MaLoai == id);
            return View(EB_tin);
        }
        // POST: Hàm Edit(post) thực hiện update dữ liệu từ trang Edit.aspx
        //khi Click Submits
        [HttpPost]
        public ActionResult Edits(int id, FormCollection collection)
        {
            // Tạo một biến Ltin gán với đối tượng có id=id truyền vào
            var Ltin = data.LOAIs.First(m => m.MaLoai == id);
            var E_Loaitin = collection["TenLoai"];
            //vì ta sửa đối tượng lên Id của biến Ltin = Id chuyền vào .
            Ltin.MaLoai = id;
            if (string.IsNullOrEmpty(E_Loaitin))
            {
                ViewData["Loi"] = "Thể loại tin không được để trống ";
            }
            // Ngược lại gán các trường của biến Ltin bằng các giá trị
            //của người dùng nhập vào
            else
            {
                Ltin.TenLoai = E_Loaitin;
                // Thực hiện updat
                UpdateModel(Ltin);
                data.SubmitChanges();
                return RedirectToAction("Loai");
            }
            return this.Edits(id);
        }
        // GET: Hàm Delete ( get ) đưa dữ liệu của đối tượng cần xóa lên trang Delete
        // cho người dùng xem. Tham số truyền vào là id
        public ActionResult Deletes(int id)
        {
            var D_tin = data.LOAIs.First(m => m.MaLoai == id);
            return View(D_tin);
        }
        // POST: Hàm Delete ( post ) thực thi lệnh xóa đối tượng khi người dùng
        // click xóa từ trang Delete.aspx . Với tham số Id
        [HttpPost]
        public ActionResult Deletes(int id, FormCollection collection)
        {
            // Tạo biến D_Tin gán với dối dượng có ID bằng với ID tham số
            var D_tin = data.LOAIs.Where(m => m.MaLoai == id).First();
            //xóa
            data.LOAIs.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("Loai");
        }


        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] != null)
            {
                //lấy top bán chạy nhất
                List<TRASUA> list_banchay = new List<TRASUA>();
                var topbanchay = data.topbanchay().ToList();
                foreach (var item in topbanchay)
                {
                    TRASUA ts = data.TRASUAs.Where(a => a.MaTS == item.MaTS).FirstOrDefault();
                    list_banchay.Add(ts);
                }
                return View(list_banchay);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        public ActionResult TraSua(int? page)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                int pageNumber = (page ?? 1);
                int pageSize = 6;
                //return View(data.TRASUAs.ToList());
                return View(data.TRASUAs.ToList().OrderBy(n => n.MaTS).ToPagedList(pageNumber, pageSize));
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        // thêm mới sách
        [HttpGet]
        public ActionResult Themmoits()
        {
            if (Session["Taikhoanadmin"] != null)
            {
                TRASUA x = new TRASUA();

                //Dua du lieu vao dropdownList
                //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
                ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View(x);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        [HttpPost]

        public ActionResult Themmoits(TRASUA ts, HttpPostedFileBase fileupload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            //Kiem tra duong dan file
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileupload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileupload.SaveAs(path);
                    }
                    ts.Anhbia = fileName;
                    //Luu vao CSDL
                    data.TRASUAs.InsertOnSubmit(ts);
                    data.SubmitChanges();
                }
                return RedirectToAction("TraSua");
            }
        }
        public ActionResult Detail(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                TRASUA ts = data.TRASUAs.SingleOrDefault(n => n.MaTS == id);
                ViewBag.MaTS = ts.MaTS;
                if (ts == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ts);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }

        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                TRASUA ts = data.TRASUAs.SingleOrDefault(n => n.MaTS == id);
                ViewBag.MaTS = ts.MaTS;
                if (ts == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ts);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoa(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                TRASUA ts = data.TRASUAs.SingleOrDefault(n => n.MaTS == id);

                ViewBag.MaTS = ts.MaTS;
                if (ts == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                /* if (ctdh != null)
                 {
                     ViewBag.ThongBao = "Không thể xoá sản phẩm này vì còn tồn tại ở chi tiết đặt hàng";
                     return View();
                 }*/
                else
                {
                    data.TRASUAs.DeleteOnSubmit(ts);
                    data.SubmitChanges();
                }

                return RedirectToAction("TraSua");
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                TRASUA ts = data.TRASUAs.SingleOrDefault(n => n.MaTS == id);
                if (ts == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View(ts);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TRASUA ts)
        {
            //Dua du lieu vao dropdownload
            var anhbia = Request.Form["Anhbia1"];
            if (ModelState.IsValid)
            {
                ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                HttpPostedFileBase fileName = Request.Files["Anhbia"];
                //Luu ten fie, luu y bo sung thu vien using System.IO;
                if (fileName != null && fileName.FileName != "")
                {
                    string serverPath = HttpContext.Server.MapPath("~/img/");
                    string filePath = serverPath + fileName.FileName;
                    fileName.SaveAs(filePath);

                }

                ts.TenTS = Request.Form["TenTS"];
                ts.Giaban = long.Parse(Request.Form["Giaban"]);
                ts.Anhbia = fileName.FileName;
                ts.MaLoai = int.Parse(Request.Form["MaLoai"]);

                if (ts.Anhbia == null)
                {
                    ts.Anhbia = anhbia;
                }
                //Luu vao CSDL   
                UpdateModel(ts);
                data.SubmitChanges();
                return RedirectToAction("TraSua");
            }
            return View(ts);


        }
        public ActionResult LoadDoanhThu(DateTime ngaybatdau, DateTime ngayketthuc)
        {

            return PartialView();
        }
        public ActionResult KhachHang(int? page)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                var list_kh = data.KHACHHANGs.ToList();
                int pageNumber = (page ?? 1);
                int pageSize = 6;
                //return View(data.TRASUAs.ToList());
                return View(list_kh.ToPagedList(pageNumber, pageSize));
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        public ActionResult deleteKh(int id)
        {

            if (Session["Taikhoanadmin"] != null)
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.Makh = kh.MaKH;
                if (kh == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(kh);
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
        }
        [HttpPost, ActionName("deleteKh")]
        public ActionResult xoaKh(int id)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
        
         
                if (kh == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                
                else
                {
                    data.KHACHHANGs.DeleteOnSubmit(kh);
                    data.SubmitChanges();
                }

                return RedirectToAction("TraSua");
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
        }
        public ActionResult HoaDon(int? page)
        {
            if (Session["Taikhoanadmin"] != null)
            {
                int pageNumber = (page ?? 1);
                int pageSize = 6;
                //return View(data.TRASUAs.ToList());
                return View(data.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(pageNumber, pageSize));
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        public ActionResult DetailHoaDon(int id)
        {

           
            if (Session["Taikhoanadmin"] != null)
            {
            
                var Donhang = data.CHITIETDONTHANGs.Where(a => a.MaDonHang == id);
                ViewBag.tongtien = TongTien(id);
                return View(Donhang.ToList());
            }
            else if (Session["Taikhoan"] != null)
            {
                return RedirectToAction("Index", "TraSua");
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }
        private double TongTien(int id)
        {
            double tongtien = 0;
            var Donhang = data.CHITIETDONTHANGs.Where(a => a.MaDonHang == id);
            tongtien = (double)Donhang.Sum(n => n.ThanhTien);
            return tongtien;
        }
      


    }
}
