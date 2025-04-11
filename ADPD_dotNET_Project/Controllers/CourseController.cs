using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;
using Newtonsoft.Json;

namespace ADPD_dotNET_Project.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            var currentUserJson = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(currentUserJson)) return RedirectToAction("Login", "User");

            var user = JsonConvert.DeserializeObject<User>(currentUserJson);
            if (user.RoleId != 1) return Forbid(); // Chặn nếu không phải Admin

            var courses = _courseRepository.GetAll();
            return View("~/Views/Manager/ManagerCourse.cshtml", courses);
        }

        [HttpPost]
        public IActionResult SaveCourse(Course course)
        {
            if (course.CourseId == 0)
            {
                _courseRepository.Add(course);
            }
            else
            {
                _courseRepository.Update(course);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }

        // ================== STUDENT ==================

        // Xem khóa học (chỉ Student)
        public IActionResult StudentCourses()
        {
            var currentUserJson = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(currentUserJson)) return RedirectToAction("Login", "User");

            var user = JsonConvert.DeserializeObject<User>(currentUserJson);
            if (user.RoleId != 3) return Forbid(); // Chặn nếu không phải Student

            var courses = _courseRepository.GetAll();
            return View("~/Views/Course/StudentCourses.cshtml", courses);
        }

        public IActionResult FacultyCourses()
        {
            var currentUserJson = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(currentUserJson)) return RedirectToAction("Login", "User");

            var user = JsonConvert.DeserializeObject<User>(currentUserJson);
            if (user.RoleId != 2) return Forbid(); 

            var courses = _courseRepository.GetAll();
            return View("~/Views/Faculty/FacultyCourse.cshtml", courses); 
        }
        /*
        public IActionResult AssignCourse()
        {
            var             var courses = _courseRepository.GetAll(); // Lấy danh sách tất cả các khóa học
students = _studentRepository.GetAll(); // Lấy danh sách sinh viên từ Student Repository

            var viewModel = new AssignCourseViewModel
            {
                Courses = courses,
                Students = students
            };

            return View(viewModel);
        }*/
    }
}
