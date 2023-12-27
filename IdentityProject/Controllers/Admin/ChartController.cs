using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace cinema.Controllers.Admin
{
    [Authorize]
    public class ChartController : Controller
    {
        private readonly CinemaDbContext _context;
        public ChartController(CinemaDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult LineChartYear()
        {
            Recalculate();

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
            Recalculate();

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


        public void Recalculate()
        {
            if (_context.Years.Any())
            {
                _context.Months.RemoveRange(_context.Months);
                _context.Years.RemoveRange(_context.Years);
                _context.SaveChanges();
            }

            IEnumerable<Bill> bills = _context.Bills.ToList();
            string year = "";
            string month = "";
            string idmonth = "";

            foreach (var bill in bills)
            {
                year = (bill.bi_date.ToString("yyyyMMddHHmmss")).Substring(0, 4);
                month = (bill.bi_date.ToString("yyyyMMddHHmmss")).Substring(4, 2);
                idmonth = year + "_" + month;

                Year result = null;
                if (_context.Years.Where(s => s.yre_id == year).Any()) 
                {
                    result = _context.Years.Where(s => s.yre_id == year).First();
                }

                int.TryParse(year, out int parsedYear);
                if (result == null)
                {
                    _context.Years.Add(new Year
                    {
                        yre_id = year,
                        yre_year = parsedYear,
                        yre_count = bill.tk_count,
                        yre_value = bill.bi_value
                    });

                    _context.SaveChanges();
                }
                else
                {
                    result.yre_count += bill.tk_count;
                    result.yre_value += bill.bi_value;
                    _context.Years.Update(result);
                    _context.SaveChanges();
                }

                Month result2 = null;
                if (_context.Months.Where(s => s.mre_id == idmonth).Any())
                {
                    result2 = _context.Months.Where(s => s.mre_id == idmonth).First();
                }
                int.TryParse(month, out int parsedMonth);
                if (result2 == null)
                {
                    _context.Months.Add(new Month
                    {
                        mre_id = idmonth,
                        mre_month = parsedMonth,
                        mre_yre_id = year,
                        mre_count = bill.tk_count,
                        mre_value = bill.bi_value
                    });

                    _context.SaveChanges();
                }
                else
                {
                    result2.mre_count += bill.tk_count;
                    result2.mre_value += bill.bi_value;
                    _context.Months.Update(result2);
                    _context.SaveChanges();
                }
            }

        }
    }
}
