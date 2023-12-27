using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers.Admin
{
    [Authorize]
    public class UploadController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public string StorePoster()
        {
            Console.WriteLine("hey");
            string result = String.Empty;
            var files = Request.Form.Files;
            foreach (IFormFile source in files)
            {
                string filename = source.Name + "jpeg";
                string imagepath = Path.Combine(Directory.GetCurrentDirectory() + "\\template\\upload\\poster\\" + filename);
                try
                {
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    var stream = System.IO.File.Create(imagepath);
                }
                catch (Exception ex)
                {
                }
            }
            return "ok";
        }
    }
}
