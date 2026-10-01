using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlbanSachContext _context;

        public HomeController(QlbanSachContext context)
        {
            _context = context;
        }

        private List<Dictionary<string, object>> GetChuDesAsync()
        {
            var query = _context.ChuDes.Select(c => new {
                Mcd = c.Mcd,
                TenChuDe = c.TenChuDe
            }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }
        private List<Dictionary<string, object>> GetChuDesCoSach()
        {
            var query = _context.ChuDes.Select(c => new
            {
                TenChuDe = c.TenChuDe,
                TongSoSach = c.Saches.Count(),
                TongLuotBan = c.Saches.Sum(s => (int?)s.SoLuongBan) ?? 0
            }
            ).Where(x => x.TongSoSach > 0).ToList();

            var vmList = query.ToList();
            var resultList = vmList.Select(item => item.GetType().GetProperties().ToDictionary(
                p => p.Name,
                p => p.GetValue(item, null) ?? "NULL"
                )
            ).ToList();
            return resultList;
        }

        private List<Dictionary<string, object>> GetSachs_NXB()
        {
            var query = _context.Saches.Select(s => new {
                TenSach = s.TenSach,
                DonGia = s.DonGia,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : "Không rõ"
            }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }

        private List<Dictionary<string, object>> GetSachs_NXB_TG()
        {
            var query = _context.Saches.Select(s => new {
                TenSach = s.TenSach,
                DonGia = s.DonGia,
                TenNXB = s.MnxbNavigation != null ? s.MnxbNavigation.TenNhaXuatBan : "Không rõ",
                TacGia = s.ThamGia.Select(tg => tg.MtgNavigation.TenTacGia).FirstOrDefault() ?? "Chưa cập nhật"
            }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }

        private List<Dictionary<string, object>> GetSachs_Moi()
        {
            var query = _context.Saches
                .OrderByDescending(s => s.Ms)
                .Take(10)
                .Select(s => new {
                    TenSach = s.TenSach,
                    GiaBan = s.DonGia,
                    SoLuongTon = s.SoLanXem
                }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }

        private List<Dictionary<string, object>> GetSachs_BanChay()
        {
            var query = _context.Saches
                .OrderByDescending(s => s.SoLuongBan)
                .Take(10)
                .Select(s => new {
                    TenSach = s.TenSach,
                    GiaBan = s.DonGia,
                    SoLuongBan = s.SoLuongBan
                }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }

        private List<Dictionary<string, object>> GetQuangCaos_ConHan()
        {
            var targetDate = new DateTime(2020, 09, 15);
            var query = _context.QuangCaos
                .Where(qc => qc.NgayHetHan >= targetDate)
                .Select(qc => new {
                    TieuDe = qc.TenCty,
                    Link = qc.Href,
                    NgayHetHan = qc.NgayHetHan
                }).ToList();

            return query.Select(item => item.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(item, null) ?? "NULL")
            ).ToList();
        }
        public IActionResult QueryDemo(int? id)
        {
            List<SachQuery> queries = new List<SachQuery>
            {
        new SachQuery { Id = 1, QueryName = "Lấy tất cả danh mục chủ đề" },
        new SachQuery { Id = 2, QueryName = "Lấy danh mục chủ đề có sách" },
        new SachQuery { Id = 3, QueryName = "Lấy danh mục sách có tt NXB, không mô tả" },
        new SachQuery { Id = 4, QueryName = "Lấy danh mục sách có tt NXB, tác giả, không mô tả" },
        new SachQuery { Id = 5, QueryName = "Lấy danh mục sách mới" },
        new SachQuery { Id = 7, QueryName = "Lấy danh mục sách bán chạy" },
        new SachQuery { Id = 9, QueryName = "Lấy danh mục quảng cáo còn hạn" }
            };

            ViewBag.Queries = new SelectList(queries, "Id", "QueryName", id);
            if (id.HasValue && id.Value == 1)
            {
                return View(GetChuDesAsync());
            }
            if (id.HasValue && id.Value == 2)
            { 
                return View(GetChuDesCoSach()); 
            }
            if (id.HasValue && id.Value == 3)
            {
                return View(GetSachs_NXB());
            }
            if (id.HasValue && id.Value == 4)
            { 
                return View(GetSachs_NXB_TG()); 
            }
            if (id.HasValue && id.Value == 5)
            {
                return View(GetSachs_Moi());
            }

            if (id.HasValue && id.Value == 7)
            {
                return View(GetSachs_BanChay());
            }

            if (id.HasValue && id.Value == 9)
            {
                return View(GetQuangCaos_ConHan());
            }
            return View(null);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
