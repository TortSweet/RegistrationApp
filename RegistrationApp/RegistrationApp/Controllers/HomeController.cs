using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models;
using System.Diagnostics;
using RegistrationApp.Services.Abstraction;

namespace RegistrationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _service;

        public HomeController(IUserService service, ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index(string sortingProp)
        {
            var userList = _service.GetUsersList();

            var sortedList =

            ViewBag.NameSortParm = string.IsNullOrEmpty(sortingProp) ? "FullName" : "";
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortingProp) ? "Id" : "";
            ViewBag.CitySortParm = string.IsNullOrEmpty(sortingProp) ? "City" : "";
            ViewBag.AgeSortParm = string.IsNullOrEmpty(sortingProp) ? "Age" : "";
            ViewBag.EmailSortParm = string.IsNullOrEmpty(sortingProp) ? "Email" : "";
            ViewBag.PhoneNumberSortParm = string.IsNullOrEmpty(sortingProp) ? "PhoneNumber" : "";


            switch (sortingProp)
            {
                case "FullName":
                    userList = userList.OrderBy(item => item.FullName);
                    break;
                case "Id":
                    userList = userList.OrderBy(item => item.Id);
                    break;
                case "Age":
                    userList = userList.OrderBy(item => item.Age);
                    break;
                case "City":
                    userList = userList.OrderBy(item => item.City);
                    break;
                case "Email":
                    userList = userList.OrderBy(item => item.Email);
                    break;
                case "PhoneNumber":
                    userList = userList.OrderBy(item => item.PhoneNumber);
                    break;
                default:
                    break;
            }

            return View("Index", userList.ToArray());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser([FromForm] string fullName, int age, string city, string email,
            string phoneNumber)
        {

            _service.SaveUser(fullName, age, city, email, phoneNumber);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}