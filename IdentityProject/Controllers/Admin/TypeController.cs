using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers.Admin
{
    [Authorize]
    public class TypeController : Controller
    {

        private readonly IMovieTypeRepository _MovieTypeRepository;
        public TypeController(IMovieTypeRepository MovieTypeRepository)
        {
            _MovieTypeRepository = MovieTypeRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách thể loại phim";
            ViewData["Types"] = await _MovieTypeRepository.GetAll();

            return View("~/Views/Admin/Type/List.cshtml");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm thể loại phim";

            return View("~/Views/Admin/Type/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(MovieType type)
        {

            bool result = _MovieTypeRepository.Create(type);


            return RedirectToAction("List", "Type", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            MovieType Type = await _MovieTypeRepository.GetMovieType(id);

            ViewData["Title"] = "Chỉnh sửa thể loại phim";

            return View("~/Views/Admin/Type/Edit.cshtml",Type);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MovieType modifiedData)
        {
            MovieType Type = await _MovieTypeRepository.GetMovieType(modifiedData.type_id);
            Type.type_name = modifiedData.type_name;
            bool result = _MovieTypeRepository.Update(Type);

            ViewData["Title"] = "Danh sách thể loại phim";

            return RedirectToAction("List", "Type", new { area = "" });
        }

        public IActionResult Delete(string id)
        {
            bool result = _MovieTypeRepository.Destroy(id);

            ViewData["Title"] = "Danh sách thể loại phim";

            return RedirectToAction("List", "Type", new { area = "" });
        }
    }
}
