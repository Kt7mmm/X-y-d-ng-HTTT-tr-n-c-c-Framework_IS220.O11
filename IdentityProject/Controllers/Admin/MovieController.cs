using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Linq;
using Movie = IdentityProject.Models.Movie;

namespace IdentityProject.Controllers.Admin
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _MovieRepository;
        private readonly CinemaDbContext _context;
        public MovieController(IMovieRepository MovieRepository, CinemaDbContext context)
        {
            _MovieRepository = MovieRepository;
            _context = context;
        }

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
                mv_link_poster = form["mv_link_poster"],
                mv_link_trailer = form["mv_link_trailer"],
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
    }
}
