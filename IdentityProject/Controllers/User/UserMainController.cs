using Microsoft.AspNetCore.Mvc;
using IdentityProject.Context;
using Microsoft.AspNetCore.Authorization;
using Movie = IdentityProject.Models.Movie;
using Slot = IdentityProject.Models.Slot;

using IdentityProject.Repositories;
using Microsoft.EntityFrameworkCore;
using IdentityProject.Models;
using System.Text.Json;
using IdentityProject.Data;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using IdentityProject.Areas.Identity.Data;
//using Cinema.Migrations;

namespace Cinema.Controllers.User
{
    public class UserMainController : Controller
    {
        private readonly CinemaDbContext _context;

        //private readonly IdentityProjectContext _context2;


        //lấy signed in user
        private readonly UserManager<IdentityProjectUser> _userManager;

        public UserMainController(CinemaDbContext context,
                                UserManager<IdentityProjectUser> userManager,
                                IdentityProjectContext context2)
        {
            _context = context;
            //_context2 = context2;
            _userManager = userManager;
        }

        public IActionResult getMenu()
        {
            ViewData["Title"] = "Trang chủ đặt vé phim";
            ViewData["Movies_rc"] = _context.Movies.OrderBy(p => p.mv_start)
                                                .Where(s => s.mv_start <= DateTime.Now)
                                                .Where(s => s.mv_end >= DateTime.Now)
                                                .ToList();

            ViewData["Movies_cm"] = _context.Movies.OrderBy(p => p.mv_start)
                                                .Where(s => s.mv_start > DateTime.Today)
                                                .ToList();

            return View("~/Views/User/Menu.cshtml");
        }

        public IActionResult getFilm_rc()
        {
            ViewData["Title"] = "Phim đang chiếu";
            ViewData["Movies"] = _context.Movies.OrderBy(p => p.mv_start)
                                                .Where(s => s.mv_start <= DateTime.Now)
                                                .Where(s => s.mv_end >= DateTime.Now)
                                                .ToList();

            return View("~/Views/User/Film_rc.cshtml");
        }

        public IActionResult getFilm_cm()
        {
            ViewData["Title"] = "Phim sắp chiếu";
            ViewData["Movies"] = _context.Movies.OrderBy(p => p.mv_start)
                                                .Where(s => s.mv_start > DateTime.Today)
                                                .ToList();
            return View("~/Views/User/Film_cm.cshtml");
        }

        public IActionResult getFilm_detail(string id)
        {
            ViewData["Title"] = "Phim sắp chiếu";

            ViewData["Movie"] = _context.Movies.Where(s => s.mv_id == id).First();
            ViewData["Types"] = from ChooseTypes in _context.ChooseTypes
                                join MovieTypes in _context.MovieTypes
                                on ChooseTypes.type_id equals MovieTypes.type_id
                                where ChooseTypes.mv_id == id
                                orderby MovieTypes.type_name
                                select new
                                {
                                    MovieTypes.type_name
                                };

            return View("~/Views/User/Film_detail.cshtml");
        }

        public IActionResult getBooking(string id)
        {
            ViewData["Title"] = "Đặt vé";
            ViewData["Slots"] = _context.Slots.OrderBy(p => p.sl_start)
                                                .Where(s => s.mv_id == id)
                                                .Where(s => s.sl_start > DateTime.Today)
                                                .ToList();
            ViewData["Movie"] = _context.Movies.Where(s => s.mv_id == id).First();

            return View("~/Views/User/Booking.cshtml");
        }

        public IActionResult getLogin()
        {
            ViewData["Title"] = "Đặt vé";

            return View("~/Views/User/Login.cshtml");
        }
        public IActionResult getSlotData(string id)
        {
            Slot slot = _context.Slots.FirstOrDefault(s => s.sl_id == id);

            if (slot != null)
            {
                string allseat = (new Cinema.Helper.Helper(_context)).setseat(id);
                return Json(new { slot, allseat });
            }
            else
            {
                return Json(new { error = "Phim hiện không có suất chiều nào." });
            }
        }

        [Authorize(Roles = "Admin,User")]

        //[HttpPost]
        //public async Task<IActionResult> getInvoice(IFormCollection form)
        //{

        //    //var user = await _userManager.GetUserAsync(User);
        //    //ViewData["User"] = user;
        //    //ViewData["UserEmail"] = user.Email;
        //    //ViewData["UserPhone"] = user.cus_phone;
        //    //ViewData["UserName"] = user.cus_name;


        //    string[] seats = Request.Form["tickets[]"].ToArray();

        //    string slot = (string)form["slot"];
        //    string room = (string)form["room"];
        //    string movieid = form["movieid"];
        //    Movie movie = _context.Movies.Where(s => s.mv_id == movieid).First();

        //    float totalpay = float.Parse(form["totalpay"]);
        //    int count = seats.Length;

        //    bool check = true;

        //    if (seats == null)
        //    {
        //        TempData["ErrorMessage"] = "Vui lòng đặt ghế trước khi thanh toán !";

        //        RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //    }

        //    foreach (string seat in seats)
        //    {

        //        switch (seat)
        //        {
        //            case "A2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "A1").Any())
        //                    && !(this.checkSeat(seats, "A1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng A1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "B2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "B1").Any())
        //                    && !(this.checkSeat(seats, "B1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng B1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "C2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "C1").Any())
        //                    && !(this.checkSeat(seats, "C1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng C1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "D2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "D1").Any())
        //                    && !(this.checkSeat(seats, "D1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng D1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "E2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "E1").Any())
        //                    && !(this.checkSeat(seats, "E1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng E1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "F2":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "F1").Any())
        //                    && !(this.checkSeat(seats, "F1")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng F1";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "A11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "A12").Any())
        //                    && !(this.checkSeat(seats, "A12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng A12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "B11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "B12").Any())
        //                    && !(this.checkSeat(seats, "B12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng B12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "C11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "C12").Any())
        //                    && !(this.checkSeat(seats, "C12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng C12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "D11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "D12").Any())
        //                    && !(this.checkSeat(seats, "D12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng D12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "E11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "E12").Any())
        //                    && !(this.checkSeat(seats, "E12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng E12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //            case "F11":
        //                if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "F12").Any())
        //                    && !(this.checkSeat(seats, "F12")))
        //                {
        //                    TempData["ErrorMessage"] = "Không được để trống ghế trong cùng F12";

        //                    return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
        //                }
        //                break;
        //        }
        //    }

        //    ViewData["Title"] = "Thanh toán hóa đơn";
        //    ViewData["Seats"] = JsonSerializer.Serialize(seats); ;
        //    ViewData["Slot"] = slot;
        //    ViewData["Room"] = room;
        //    ViewData["Movie"] = movie;
        //    ViewData["Totalpay"] = totalpay;
        //    ViewData["Count"] = count;

        //    return View("~/Views/User/Invoice.cshtml");
        //}
        public IActionResult getInvoice(IFormCollection form)
        {



            string[] seats = Request.Form["tickets[]"].ToArray();

            string slot = (string)form["slot"];
            Slot slots = _context.Slots.Where(s => s.sl_id == slot).First();
            string room = (string)form["room"];
            string movieid = form["movieid"];
            Movie movie = _context.Movies.Where(s => s.mv_id == movieid).First();
            float totalpay = float.Parse(form["totalpay"]);
            int count = seats.Length;

            bool check = true;

            if (seats == null)
            {
                TempData["ErrorMessage"] = "Vui lòng đặt ghế trước khi thanh toán !";
                TempData["ErrorMessage"] = "Vui lòng đặt ghế trước khi thanh toán !";

                RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
            }

            foreach (string seat in seats)
            {

                switch (seat)
                {
                    case "A2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "A1").Any())
                            && !(this.checkSeat(seats, "A1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng A1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "B2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "B1").Any())
                            && !(this.checkSeat(seats, "B1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng B1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "C2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "C1").Any())
                            && !(this.checkSeat(seats, "C1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng C1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "D2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "D1").Any())
                            && !(this.checkSeat(seats, "D1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng D1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "E2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "E1").Any())
                            && !(this.checkSeat(seats, "E1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng E1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "F2":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "F1").Any())
                            && !(this.checkSeat(seats, "F1")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng F1";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "A11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "A12").Any())
                            && !(this.checkSeat(seats, "A12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng A12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "B11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "B12").Any())
                            && !(this.checkSeat(seats, "B12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng B12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "C11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "C12").Any())
                            && !(this.checkSeat(seats, "C12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng C12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "D11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "D12").Any())
                            && !(this.checkSeat(seats, "D12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng D12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "E11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "E12").Any())
                            && !(this.checkSeat(seats, "E12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng E12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                    case "F11":
                        if (!(_context.Tickets.Where(s => s.sl_id == slot).Where(s => s.st_id == "F12").Any())
                            && !(this.checkSeat(seats, "F12")))
                        {
                            TempData["ErrorMessage"] = "Không được để trống ghế trong cùng F12";

                            return RedirectToAction("getBooking", "UserMain", new { area = "", id = movieid });
                        }
                        break;
                }
            }

            ViewData["Title"] = "Thanh toán hóa đơn";
            ViewData["Seats"] = JsonSerializer.Serialize(seats); ;
            ViewData["Slot"] = slots;
            ViewData["Room"] = room;
            ViewData["Movie"] = movie;
            ViewData["Totalpay"] = totalpay;
            ViewData["Count"] = count;

            return View("~/Views/User/Invoice.cshtml");
        }
        public bool checkSeat(string[] arrayseat, string seat)
        {
            foreach (string checkseat in arrayseat){
                if (checkseat.Equals(seat))
                    return true;
            }
            return false;
        }

        public IActionResult getInfo()
        {
            Customer user = _context.Customers.Where(s => s.cus_name == "Trần Minh Hoàng").First();
            string email = user.cus_email;
            Bill[] bills = _context.Bills.Where(s => s.cus_email == email).OrderByDescending(p => p.bi_date).ToArray();
            var tickets = (from ticket in _context.Tickets
                           join bill in _context.Bills on ticket.bi_id equals bill.bi_id
                           join movie in _context.Movies on ticket.mv_id equals movie.mv_id
                           where bill.cus_email == email
                           orderby bill.bi_date descending
                           select new
                           {
                               tk_id = ticket.tk_id,
                               bi_id = ticket.bi_id,
                               st_id = ticket.st_id,
                               sl_id = ticket.sl_id,
                               tk_value = ticket.tk_value,
                               r_id = ticket.r_id,
                               tk_type = ticket.tk_type,
                               mv_name = movie.mv_name
                           }).ToList();


            ViewData["Title"] = "Thông tin cá nhân";
            ViewData["User"] = user;
            ViewData["Bills"] = bills;
            ViewData["Tickets"] = tickets;

            return View("~/Views/User/Info.cshtml");
        }

        public IActionResult postInfo(IFormCollection form)
        {
            Customer user = _context.Customers.Where(s => s.cus_email == (string)form["email"]).First();

            user.cus_name = form["name"];
            user.cus_dob = DateTime.Parse(form["birth"]);
            user.cus_phone = form["phone"];
            user.cus_gender = form["gender"];

            _context.Customers.Update(user);
            _context.SaveChanges();

            return RedirectToAction("getInfo", "UserMain", new { area = "" });
        }
    }
}
