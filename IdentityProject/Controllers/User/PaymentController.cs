using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityProject.Context;
using Cinema.Services;
using Cinema.Models;
using System;
using System.Text;
using Cinema.Migrations;
using Humanizer;
using Ticket = IdentityProject.Models.Ticket;
using Bill = IdentityProject.Models.Bill;
using Seat = IdentityProject.Models.Seat;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Xml.Schema;
using System.Net.WebSockets;
using IdentityProject.Repositories;
using IdentityProject.Models;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;
using IdentityProject.Migrations;
using NuGet.Packaging;
using System.Linq;
namespace Cinema.Controllers.User
{
    public class PaymentController : Controller

    {
        
        private readonly CinemaDbContext _context;
        private readonly IVnPayService _vnPayservice;

            public PaymentController( CinemaDbContext context,
                IVnPayService vnpayservice)
            {
                _context = context;
                _vnPayservice = vnpayservice;
            
            }
            public string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random = new Random();
                var stringBuilder = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                {
                    int randomIndex = random.Next(chars.Length);
                    stringBuilder.Append(chars[randomIndex]);
                }

                return stringBuilder.ToString();
            }
            [Authorize]
            [HttpPost]

        public IActionResult getPay(IFormCollection form)
        {

            decimal totalDCM;
            decimal.TryParse(form["total_payment"], out totalDCM);
            Console.WriteLine(totalDCM);

            double totalDB;
            double.TryParse(form["total_payment"], out totalDB);
            Console.WriteLine(totalDB);

            int counts;
            int.TryParse(form["count"], out counts);
            Console.WriteLine(counts);

            var email = form["hidden_email"];
            Console.WriteLine(email);


            var slot_id = form["slotid"];
            DateTime slottime;
            DateTime.TryParse(form["slottimeInput"], out slottime);
            Console.WriteLine(slottime);

            decimal slotprice;
            decimal.TryParse(form["slotprice"], out slotprice);
            Console.WriteLine(slotprice);

            var billid = GenerateRandomString(200);
            Console.WriteLine(billid);

            var date = DateTime.UtcNow;
            Console.WriteLine(date);

            var mv_name = form["moviename"];
            Console.WriteLine(mv_name);

            var mv_id = form["movieid"];
            Console.WriteLine(mv_id);

            var roomid = form["roomid"];
            Console.WriteLine(roomid);

            var seatstr = form["seatsJson"];
            var len = form["length"];

            int result = Int32.Parse(len.ToString());
            List<string> list = new List<string>();
            for (int y = 0; y < result; y++)
            {
                list.Add(form["data[" + y.ToString() + "]"]);
                Console.WriteLine(form["data[" + y.ToString() + "]"]);
            }
            String[] str = list.ToArray();

            //var seatIds = seatstr.Split(',');
            //Console.WriteLine(seatstr);

            //string[] parsedArr =
            //var seatsArray = JsonConvert.DeserializeObject<string[]>(seatstr);






            var bill = new Bill
            {
                bi_id = billid,
                cus_email = email,
                bi_date = date,
                tk_count = counts,
                bi_value = totalDCM
            };
            Console.WriteLine(1);

            _context.Database.BeginTransaction();
            Console.WriteLine(2);

            try
            {
                _context.Bills.Add(bill);
                _context.SaveChanges();
                Console.WriteLine(3);

                var ve = new List<Ticket>();
                foreach (var seatId in str)
                {
                    Console.WriteLine(seatId);

                    Seat seat = _context.Seats.Where(s => s.st_id == seatId).First();
                    Console.WriteLine(seat.r_id);

                    var tk_type = seat.st_type;
                    Console.WriteLine(tk_type);

                    var tk_price = slotprice;
                    decimal tk_value;
                    if (tk_type == "vip")
                    {
                        tk_value = tk_price * 1.5m;
                    }
                    else if (tk_type == "sweetbox")
                    {
                        tk_value = tk_price * 2.2m;
                    }
                    else
                    {
                        tk_value = tk_price;
                    }
                    ve.Add(new Ticket
                    {
                        tk_id = GenerateRandomString(50),
                    sl_id = slot_id,
                        st_id = seat.st_id,
                        mv_id = mv_id,
                        r_id = roomid,
                        tk_value = tk_value,
                        tk_type = tk_type,
                        bi_id = billid,
                        tk_available = 1
                    });
                }
                Console.WriteLine("sap");

                _context.AddRange(ve);
                _context.SaveChanges();
                _context.Database.CommitTransaction();



            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chèn vào bảng ticket: " + ex.Message);
            }

            var VNPayModel = new VnPaymentRequestModel
            {
                total = totalDB,
                bi_date = date,
                cus_name = form["hidden_email"],
                bi_id = billid
            };


            return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, VNPayModel));

        }



        [Authorize]
        public IActionResult PaymentFail()
        {
            return View();
        }
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [Authorize]
        public IActionResult handlePaymentReturn()
        {
            var respone = _vnPayservice.PaymentExecute(Request.Query);
            var billid = respone.OrderId;
            Console.WriteLine(billid);
            Console.WriteLine("test");



            if (respone == null || respone.VnPayResponseCode != "00")
            {
                if (billid != null)
                {
                    // Xóa các bản ghi từ bảng "ticket" với điều kiện bi_id = billid
                    var ticketsToDelete = _context.Tickets.Where(t => t.bi_id == billid);
                    _context.Tickets.RemoveRange(ticketsToDelete);

                    // Xóa các bản ghi từ bảng "bill" với điều kiện bi_id = billid
                    var billToDelete = _context.Bills.SingleOrDefault(b => b.bi_id == billid);
                    _context.Bills.Remove(billToDelete);

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();
                }
                return RedirectToAction("PaymentFail");
                

            }
            // Lấy danh sách tk_id từ bảng "tickets" và chuyển thành chuỗi JSON
            var ticketIds = _context.Tickets
                .Where(t => t.bi_id == billid)
                .Select(t => t.tk_id)
                .ToArray();
            string ticketIdsJson = JsonConvert.SerializeObject(ticketIds);
            Console.WriteLine(ticketIdsJson);

            // Lấy giá trị r_id từ bảng "tickets" cho bản ghi đầu tiên có bi_id = bill_id
            string roomId = _context.Tickets
                .Where(t => t.bi_id == billid)
                .Select(t => t.r_id)
                .FirstOrDefault();
            Console.WriteLine(roomId);

            // Lấy tên phim từ bảng "tickets" và liên kết với bảng "movies" thông qua mv_id
            string movieName = _context.Tickets
                .Join(_context.Movies, t => t.mv_id, m => m.mv_id, (t, m) => m.mv_name)
                .FirstOrDefault();


            // Lấy giá trị sl_start từ bảng "tickets" và liên kết với bảng "slots" thông qua sl_id
            DateTime slStart = _context.Tickets
                .Join(_context.Slots, t => t.sl_id, s => s.sl_id, (t, s) => s.sl_start)
                .FirstOrDefault();

            // Lấy danh sách st_id từ bảng "tickets" và chuyển thành chuỗi JSON
            var seatIds = _context.Tickets
                .Where(t => t.bi_id == billid)
                .Select(t => t.st_id)
                .ToArray();
            string seatIdsJson = JsonConvert.SerializeObject(seatIds);

            // Lấy giá trị bi_date từ bảng "bills" cho bản ghi có bi_id = bill_id
            DateTime transactionDate = _context.Bills
                .Where(b => b.bi_id == billid)
                .Select(b => b.bi_date)
                .FirstOrDefault();

            // Mã hóa số tiền theo định dạng phân cách hàng nghìn
            var money = respone.orderAmount;
            string total = money.ToString("N0");


            // Tạo đối tượng chứa các giá trị
            ViewData["ticketIdsJson"] = ticketIdsJson;
            ViewData["roomId"] = roomId;
            ViewData["movieName"] = movieName;
            ViewData["slStart"] = slStart;
            ViewData["roomId"] = roomId;
            ViewData["seatIdsJson"] = seatIdsJson;
            ViewData["transactionDate"] = transactionDate;
            ViewData["billid"] = billid;
            ViewData["total"] = total;

            return RedirectToAction("PaymentSuccess");
        }

    }
}
