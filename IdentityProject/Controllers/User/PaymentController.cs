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

namespace Cinema.Controllers.User
{
    public class PaymentController : Controller

    {
        private readonly CinemaDbContext _context;
        private readonly IVnPayService _vnPayservice;

        public PaymentController( CinemaDbContext context, IVnPayService vnpayservice)
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
            var billid = GenerateRandomString(200);
            var date = DateTime.UtcNow;
            decimal valueDecimal;
            decimal.TryParse(form["total_payment"], out valueDecimal);
            
            var VNPayModel = new VnPaymentRequestModel
            {
                total = 50000,
                bi_date = date,
                cus_name = form["cus_name"],
                bi_id = billid
            };

            var bill = new Bill
            {
                bi_id = billid,
                cus_email = form["hidden_email"],
                bi_date = date,
                tk_count = int.TryParse(form["count"], out var count) ? count : 0,
                bi_value = valueDecimal
            };
            //var ticket = new Ticket
            //{
            //    tk_id = "T" + GenerateRandomString(200),
            //    mv_id = form["roomid"],
            //    sl_id = form["slotid"],
            //    r_id = form["roomid"],
            //    tk_value = form["movieid"],
            //    st_id = form["movieid"],
            //    tk_type = 
            //    bi_id = billid
            //    tk_available = 1
            //}

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
            if (respone == null || respone.VnPayResponseCode != "00")
            {
                return RedirectToAction("PaymentFail");
            }
            return RedirectToAction("PaymentSuccess");
        }

    }
}
