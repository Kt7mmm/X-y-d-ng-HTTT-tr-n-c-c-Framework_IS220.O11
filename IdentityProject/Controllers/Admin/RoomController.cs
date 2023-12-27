using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _RoomRepository;
        public RoomController(IRoomRepository RoomRepository)
        {
            _RoomRepository = RoomRepository;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách phòng chiếu";
            ViewData["Rooms"] = await _RoomRepository.GetAll();

            return View("~/Views/Admin/Room/List.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm phòng chiếu";

            return View("~/Views/Admin/Room/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(Room Room)
        {
            bool result = _RoomRepository.Create(Room);


            return RedirectToAction("List", "Room", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Room Room = await _RoomRepository.GetRoom(id);

            ViewData["Title"] = "Chỉnh sửa phòng chiếu";

            return View("~/Views/Admin/Room/Edit.cshtml", Room);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Room modifiedData)
        {
            Room room = await _RoomRepository.GetRoom(modifiedData.r_id);

            room.r_capacity = modifiedData.r_capacity;

            bool result = _RoomRepository.Update(room);

            ViewData["Title"] = "Danh sách phòng chiếu";

            return RedirectToAction("List", "Room", new { area = "" });
        }

        public IActionResult Delete(string id)
        {
            bool result = _RoomRepository.Destroy(id);

            ViewData["Title"] = "Danh sách phòng chiếu";

            return RedirectToAction("List", "Room", new { area = "" });
        }
    }
}
