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

        public IActionResult Index(string sortingProp)
        {
            var userList = _service.GetUsersList();

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortingProp) ? "FullName" : "";
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortingProp) ? "Id" : "";
            ViewBag.CitySortParm = string.IsNullOrEmpty(sortingProp) ? "City" : "";
            ViewBag.AgeSortParm = string.IsNullOrEmpty(sortingProp) ? "Age" : "";
            ViewBag.EmailSortParm = string.IsNullOrEmpty(sortingProp) ? "Email" : "";
            ViewBag.PhoneNumberSortParm = string.IsNullOrEmpty(sortingProp) ? "PhoneNumber" : "";


            userList = SortUsers(userList, sortingProp);

            return View("Index", userList.ToArray());
        }

        private IQueryable<User> SortUsers(IQueryable<User> userList, string property)
        {
            userList = property switch
            {
                "FullName" => userList.OrderBy(item => item.FullName),
                "Id" => userList.OrderBy(item => item.Id),
                "Age" => userList.OrderBy(item => item.Age),
                "City" => userList.OrderBy(item => item.City),
                "Email" => userList.OrderBy(item => item.Email),
                "PhoneNumber" => userList.OrderBy(item => item.PhoneNumber),
                _ => userList
            };

            return userList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(User newUser)
        {
            var result = _service.SaveUser(newUser);

            return RedirectToAction("Index", result);
        }
        [HttpPost]
        public JsonResult CheckFullName([FromBody] string fullName)
        {
            var isValid = _service.IsUserExist(fullName);
            return Json(isValid);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}