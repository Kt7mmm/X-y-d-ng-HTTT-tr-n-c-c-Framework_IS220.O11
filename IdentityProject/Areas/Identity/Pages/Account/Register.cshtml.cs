// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using Serilog;
using IdentityProject.Models;

namespace IdentityProject.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityProjectUser> _signInManager;
        private readonly UserManager<IdentityProjectUser> _userManager;
        private readonly IUserStore<IdentityProjectUser> _userStore;
        private readonly IUserEmailStore<IdentityProjectUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityProjectUser> userManager,
            IUserStore<IdentityProjectUser> userStore,
            SignInManager<IdentityProjectUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

       
        public string ReturnUrl { get; set; }

       
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

       
        public class InputModel
        {
            
            [Required(ErrorMessage = "Email không được để trống")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Độ dài email không hợp lệ")]
            [Display(Name = "Email")]
            public string Email { get; set; }




            [Required(ErrorMessage = "Mật khẩu không được để trống")]
            [StringLength(100,ErrorMessage = "Mật khẩu phải có độ dài ít nhất là 8 ký tự", MinimumLength = 8)]
            [DataType(DataType.Password, ErrorMessage = "Mật khẩu phải bao gồm ít nhất 1 chữ cái viết hoa, 1 ký tự đặc biệt và số")]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
            [DataType(DataType.Password,ErrorMessage = "Mật khẩu phải bao gồm ít nhất 1 chữ cái viết hoa, 1 ký tự đặc biệt và số")]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
            public string ConfirmPassword { get; set; }

         

            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "Họ tên không được để trống")]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "Độ dài tên không hợp lệ")]
            [DataType(DataType.Text,ErrorMessage = "Họ tên không hợp lệ")]
            public string cus_name { get; set; }

            [Required(ErrorMessage = "Số điện thoại không được để trống")]
            [RegularExpression(@"^0\d+$", ErrorMessage = "Số điện thoại phải bắt đầu từ số 0")]
            [Display(Name = "Số điện thoại")]
            [DataType(DataType.PhoneNumber, ErrorMessage ="Số điện thoại không hợp lệ")]
            [Phone]
            public string cus_phone { get; set; }

            [Required]
            public string cus_gender { get; set; }


            [Required(ErrorMessage = "Ngày sinh không được để trống")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            //[DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
            [customDayRange(ErrorMessage = "Ngày sinh không hợp lệ")]
            public DateTime cus_dob { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Log.Information("start");
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                
                if (Input.cus_gender != "" && Input.cus_name != "" && Input.cus_phone != "")
                {
                    DateTime? cus_dob = Input.cus_dob;
                    user.cus_name = Input.cus_name;
                    Log.Information(user.cus_name);

                    user.cus_phone = Input.cus_phone;
                    user.cus_gender = Input.cus_gender;
                    user.cus_point = 0;
                    user.cus_type = "Membership";

                    user.cus_dob = cus_dob;
                }
        
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            

            // If we got this far, something failed, redisplay form
            return Page();
        }


        private async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
        {

            //TODO
            //INSERT YOUR OWN MAIL SERVER CREDENTIALS
            // message.From = ?
            // message.Port = ?
            // message.Host = ?
            // smtpClient.Credentials = new NetworkCredential(?Username,?Password);
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("21522605@gm.uit.edu.vn");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = confirmLink;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("21522605@gm.uit.edu.vn", "Abc-112023");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private IdentityProjectUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityProjectUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityProjectUser)}'. " +
                    $"Ensure that '{nameof(IdentityProjectUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityProjectUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityProjectUser>)_userStore;
        }
    }
}
