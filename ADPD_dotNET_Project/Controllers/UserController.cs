using Microsoft.AspNetCore.Mvc;
using ADPD_dotNET_Project.Facade;
using Microsoft.AspNetCore.Http;

namespace ADPD_dotNET_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IUserFacade userFacade, IHttpContextAccessor httpContextAccessor)
        {
            _userFacade = userFacade;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string fullName, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ViewBag.Error = "FullName cannot be empty.";
                return View();
            }

            if (_userFacade.RegisterUser(username, password, fullName, email))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Tài khoản đã tồn tại.";
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userFacade.AuthenticateUser(username, password);
            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));

                // Điều hướng theo Role
                switch (user.RoleId)
                {
                    case 1: // Admin
                        return RedirectToAction("Index", "Course"); // hoặc Dashboard Admin
                    case 3: // Student
                        return RedirectToAction("StudentCourses", "Course"); // Giao diện sinh viên
                    default:
                        return RedirectToAction("Login");
                }
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }


        [HttpPost]
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }


    }
}
