using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace cinema.Controllers.Admin
{
    public class ChartController : Controller
    {
        private readonly CinemaDbContext _context;
        public ChartController(CinemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LineChartYear()
        {
            ViewData["Title"] = "Biểu đồ đường doanh số năm gần đây";
            List<BlogLineChart> list = new List<BlogLineChart>();

            var result = _context.Years.OrderBy(p => p.yre_year).ToList();

            foreach (var val in result)
            {
                list.Add(new BlogLineChart { DonVi = (val.yre_year).ToString(), SoVe = val.yre_count, DoanhThu = (float?)val.yre_value });
            }

            return View("~/Views/Admin/Revenue/LineChart_Year.cshtml", list);
        }

        [HttpGet]
        public IActionResult LineChartMonth()
        {
            ViewData["Title"] = "Biểu đồ đường doanh số các tháng trong năm";

            List<BlogLineChart> list = new List<BlogLineChart>();

            var result =
                        from m in _context.Months
                        join y in _context.Years
                        on m.mre_yre_id equals y.yre_id
                        orderby y.yre_year, m.mre_month
                        select new { m.mre_month, m.mre_count, m.mre_value, y.yre_year };

            foreach (var val in result)
            {
                list.Add(new BlogLineChart { 
                                            DonVi = ((val.mre_month).ToString() + "/" + (val.yre_year).ToString()), 
                                            SoVe = val.mre_count, 
                                            DoanhThu = (float?)val.mre_value });
            }

            return View("~/Views/Admin/Revenue/LineChart_Month.cshtml", list);
        }

    }
}
