using IdentityProject.Context;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Linq;
using Movie = IdentityProject.Models.Movie;

namespace IdentityProject.Controllers.Admin
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly CinemaDbContext _context;
        public MovieController(IMovieRepository MovieRepository, CinemaDbContext context)
        {
            _MovieRepository = MovieRepository;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách phim";
            ViewData["Movies"] = await _MovieRepository.GetAll();

            return View("~/Views/Admin/Movie/List.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm phim";
            ViewData["Types"] = _context.MovieTypes.OrderBy(p => p.type_name).ToList();

            return View("~/Views/Admin/Movie/Add.cshtml");
        }
        [HttpPost]
        public IActionResult AddMovie(IFormCollection form)
        {
            var chosenTypes = Request.Form["movie_type[]"].ToArray();

            Movie movie = new Movie()
            {
                mv_id = form["mv_id"],
                mv_name = form["mv_name"],
                mv_cap = form["mv_cap"],
                mv_detail = form["mv_detail"],
                mv_duration = TimeSpan.Parse(form["mv_duration"]),
                mv_end = DateTime.Parse(form["mv_end"]),
                mv_start = DateTime.Parse(form["mv_start"]),
                mv_link_poster = "/template/upload/poster/" + (string)form["mv_link_poster"],
                mv_link_trailer = "/template/upload/trailer/" + (string)form["mv_link_trailer"],
                mv_restrict = form["mv_restrict"]
            };

            bool result = _MovieRepository.Create(movie, chosenTypes);

            return RedirectToAction("List", "Movie", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Movie movie = await _MovieRepository.GetMovie(id);

            ViewData["Title"] = "Chỉnh sửa phim " + "-- " + movie.mv_name + " --";
            ViewData["Types"] = _context.MovieTypes.OrderBy(p => p.type_name).ToList();
            ViewData["ChosenTypes"] = _context.ChooseTypes.OrderBy(p => p.type_id).Where(s => s.mv_id == id);
            ViewData["Movie"] = movie;

            return View("~/Views/Admin/Movie/Edit.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> EditMovie(IFormCollection form)
        {
            var chosenTypes = Request.Form["movie_type[]"].ToArray();

            Movie movie = await _MovieRepository.GetMovie(form["mv_id"]);
            movie.mv_id = form["mv_id"];
            movie.mv_name = form["mv_name"];
            movie.mv_cap = form["mv_cap"];
            movie.mv_detail = form["mv_detail"];
            movie.mv_duration = TimeSpan.Parse(form["mv_duration"]);
            movie.mv_end = DateTime.Parse(form["mv_end"]);
            movie.mv_start = DateTime.Parse(form["mv_start"]);
            movie.mv_link_poster = form["mv_link_poster"];
            movie.mv_link_trailer = form["mv_link_trailer"];
            movie.mv_restrict = form["mv_restrict"];

            bool result = _MovieRepository.Update(movie, chosenTypes);

            ViewData["Title"] = "Danh sách phim";

            return RedirectToAction("List", "Movie", new { area = "" });
        }

        public IActionResult Delete(string id)
        {
            bool result = _MovieRepository.Destroy(id);

            ViewData["Title"] = "Danh sách phim";

            return RedirectToAction("List", "Movie", new { area = "" });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult StorePoster(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string filename = Path.GetFileName(file.FileName);

                    string folderPath = Path.Combine(hostingEnvironment.WebRootPath, "template", "upload", "poster");
                    string imagePath = Path.Combine(folderPath, filename);

                    // Tạo thư mục nếu nó chưa tồn tại
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Trả về đường dẫn ảnh cho phía client
                    return Json(new { success = true, imagePath = "/template/upload/poster/" + filename });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return Json(new { success = false, errorMessage = "Upload file poster lỗi trong Controller" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult StoreTrailer(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string filename = Path.GetFileName(file.FileName);

                    string folderPath = Path.Combine(hostingEnvironment.WebRootPath, "template", "upload", "trailer");
                    string trailerPath = Path.Combine(folderPath, filename);

                    // Tạo thư mục nếu nó chưa tồn tại
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var stream = new FileStream(trailerPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Trả về đường dẫn file cho phía client
                    return Json(new { success = true, trailerPath = "/template/upload/trailer/" + filename });
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return Json(new { success = false, errorMessage = "Upload file trailer lỗi trong Controller" });
        }
    }
}
