using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSOnline.Models;

namespace TSOnline.Controllers
{
    public class NguoidungController : Controller
    {
        dbQLTraSuaDataContext data = new dbQLTraSuaDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection,KHACHHANG kh)
        {
            // Gán các giá tị người dùng nhập liệu cho các biến 
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh =String.Format("{0:MM/dd/yyyy}",collection["Ngaysinh"]);
            
           
                //Gán giá trị cho đối tượng được tạo mới (kh)
            kh.HoTen = hoten;
            kh.Taikhoan = tendn;
            kh.Matkhau = matkhau;
            kh.Email = email;
            kh.DiachiKH = diachi;
            kh.DienthoaiKH = dienthoai;
            kh.Ngaysinh =DateTime.Parse(ngaysinh);
            data.KHACHHANGs.InsertOnSubmit(kh);
            try
            {
                data.SubmitChanges();
            }
            catch (Exception e)
            {
                ViewBag.Thongbao = "fail";
            }

            ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
            return RedirectToAction("Dangnhap");
            
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            if(Session["Taikhoan"] != null)
            {
                return Redirect("/");
            }
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau) && matkhau.Length <= 18 && matkhau.Length >=6)
                {
                ViewData["Loi2"] = "Sai format";
                }
                else
                {
                    //Gán giá trị cho đối tượng được tạo mới (kh)
                    KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                    Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }else if (kh != null)
                    {
                        //ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("Index", "TraSua");
                    }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            return PartialView();
        }

        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            Session["Taikhoanadmin"] = null;
            return Redirect("/");
        }
        public ActionResult ThongTinCaNhan()
        {
            if (Session["Taikhoan"] != null)
            {
              
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            
        }
        [HttpPost]
        public ActionResult ThongTinCaNhan( FormCollection collection)
        {
            // Tạo một biến Ltin gán với đối tượng có id=id truyền vào
           
            KHACHHANG kh1 = (KHACHHANG)Session["Taikhoan"];
            var kh = data.KHACHHANGs.First(m => m.MaKH == kh1.MaKH);
            //vì ta sửa đối tượng lên Id của biến Ltin = Id chuyền vào .
            kh.HoTen = collection["HoTen"];
            kh.Taikhoan = collection["TaiKhoan"];
            kh.Matkhau = collection["MatKhau"];
            kh.Email= collection["Email"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            kh.DiachiKH = collection["DiaChi"];
            kh.DienthoaiKH = collection["DienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
           kh.Ngaysinh = DateTime.Parse(ngaysinh);
           
            // Thực hiện updat
            UpdateModel(kh);
            data.SubmitChanges();
            return this.ThongTinCaNhan();
        }
        public ActionResult HoaDon()
        {
            if (Session["Taikhoan"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

        }


    }

}