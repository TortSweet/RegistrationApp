using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Data.Entities;
using RegistrationApp.Models;
using RegistrationApp.Services.Abstraction;
using System.Diagnostics;

namespace RegistrationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;

        public HomeController(IUserService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "service must exist");
        }

        public async Task<IActionResult> Index(string sortingProp)
        {
            var userList = await _service.GetUsersListAsync(sortingProp);

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortingProp) ? "FullName" : "";
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortingProp) ? "Id" : "";
            ViewBag.CitySortParm = string.IsNullOrEmpty(sortingProp) ? "City" : "";
            ViewBag.AgeSortParm = string.IsNullOrEmpty(sortingProp) ? "Age" : "";
            ViewBag.EmailSortParm = string.IsNullOrEmpty(sortingProp) ? "Email" : "";
            ViewBag.PhoneNumberSortParm = string.IsNullOrEmpty(sortingProp) ? "PhoneNumber" : "";

            return View("Index", userList.ToArray());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserAsync(User newUser)
        {
            await _service.SaveUserAsync(newUser);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<JsonResult> CheckFullNameAsync([FromBody] string fullName)
        {
            var isValid = await _service.IsUserExistAsync(fullName);
            return Json(isValid);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}