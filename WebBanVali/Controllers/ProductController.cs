using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebBanVali.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanVali.Controllers
{
    public class ProductController : Controller
    {
        WebBanVaLiEntities1 db = new WebBanVaLiEntities1();
        // GET: Product
        public ActionResult Index(int? page)
        {
            int pagesize = 12; // so san pham tren mot trang
            int pagenumber = (page ?? 1); // so trang 
            List<tDanhMucSP> lstSanPham = db.tDanhMucSPs.ToList();
            return View(lstSanPham.ToPagedList(pagenumber,pagesize));
        }
        public PartialViewResult CategoryPartial()
        {
            return PartialView(db.tLoaiSPs.ToList());
        }
        public ViewResult ProductsByCategory(string MaLoai = "vali")
        {
            List<tDanhMucSP> lstProducts = db.tDanhMucSPs.Where(n => n.MaLoai == MaLoai).OrderBy(n => n.MaLoai).ToList();
            if (lstProducts.Count == 0)
            {
                ViewBag.Products = "Khong co san pham nay";
                ViewBag.lstProducts = db.tDanhMucSPs.ToList();
            }
            return View(lstProducts);
        }
        public ViewResult ChiTietSanPham(string MaSP)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpGet]

        public ViewResult ThemSP()
        {
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux.ToList().OrderBy(n => n.ChatLieu), "MaChatLieu", "ChatLieu");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs.ToList().OrderBy(n => n.KichThuoc), "MaKichThuoc", "KichThuoc");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes.ToList().OrderBy(n => n.HangSX), "MaHangSX", "HangSX");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias.ToList().OrderBy(n => n.TenNuoc), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs.ToList().OrderBy(n => n.Loai), "MaLoai", "Loai");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs.ToList().OrderBy(n => n.TenLoai), "MaDT", "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ThemSP(tDanhMucSP sanpham)
        {
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux.ToList().OrderBy(n => n.ChatLieu), "MaChatLieu", "ChatLieu");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs.ToList().OrderBy(n => n.KichThuoc), "MaKichThuoc", "KichThuoc");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes.ToList().OrderBy(n => n.HangSX), "MaHangSX", "HangSX");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias.ToList().OrderBy(n => n.TenNuoc), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs.ToList().OrderBy(n => n.Loai), "MaLoai", "Loai");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs.ToList().OrderBy(n => n.TenLoai), "MaDT", "TenLoai");

            if (ModelState.IsValid)
            {
                db.tDanhMucSPs.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }

        [HttpGet]

        public ActionResult SuaSP(string MaSP)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.Find(MaSP);
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux.ToList().OrderBy(n => n.ChatLieu), "MaChatLieu", "ChatLieu");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs.ToList().OrderBy(n => n.KichThuoc), "MaKichThuoc", "KichThuoc");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes.ToList().OrderBy(n => n.HangSX), "MaHangSX", "HangSX");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias.ToList().OrderBy(n => n.TenNuoc), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs.ToList().OrderBy(n => n.Loai), "MaLoai", "Loai");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs.ToList().OrderBy(n => n.TenLoai), "MaDT", "TenLoai");
            return View(sanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SuaSP(tDanhMucSP sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ViewResult XoaSP(string MaSP)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("XoaSP")]
        public ActionResult XacNhanXoa(string MaSP)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP);
            var anhsp = from p in db.tAnhSPs
                        where p.MaSP == sanpham.MaSP
                        select p;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.tAnhSPs.RemoveRange(anhsp);
            db.tDanhMucSPs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*[HttpPost]
        public ActionResult SearchResults(FormCollection f, int? page)
        {
            string searchkey = f["txtsearch"].ToString();
            ViewBag.searchkey = searchkey;
            List<tDanhMucSP> lstSearchResults = db.tDanhMucSPs.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            if (lstSearchResults.Count == 0)
            {
                ViewBag.thongbao = "Không có sản phầm bạn tìm";
                return View(db.tDanhMucSPs.ToList().ToPagedList(pagenumber, pagesize));
            }
            ViewBag.thongbao = "Đã tìm thấy" + lstSearchResults.Count.ToString() + " sản phẩm";
            return View(lstSearchResults.ToPagedList(pagenumber, pagesize));
        }*/

        [HttpGet]
        public ActionResult SearchResults(string searchkey, int? page)
        {
            ViewBag.SearchKey = searchkey;
            List<tDanhMucSP> lstSearchResults=db.tDanhMucSPs.Where(n=>n.TenSP.Contains(searchkey)).ToList();
            int pagesize = 12; // so san pham tren mot trang
            int pagenumber = (page ?? 1); // so trang 
            if(lstSearchResults.Count == 0)
            {
                ViewBag.ThongBao = "Khong co san pham can tim";
                return View(db.tDanhMucSPs.ToList().ToPagedList(pagenumber, pagesize));
            }
            ViewBag.ThongBao = "Da tim thay" + lstSearchResults.Count.ToString() + "san pham";
            return View(lstSearchResults.OrderBy(n=>n.TenSP).ToPagedList(pagenumber, pagesize));
        }
    }
}