using ADPD_dotNET_Project.Facade;
using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ADPD_dotNET_Project.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ICourseFacade _courseFacade;
        private readonly ICourseRepository _courseRepository;

        public ManagerController(ICourseFacade courseFacade, ICourseRepository courseRepository)
        {
            _courseFacade = courseFacade;
            _courseRepository = courseRepository;
        }

        public IActionResult ManagerCourse()
        {
            var courses = _courseFacade.GetAllCourses();
            return View(courses);
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseFacade.AddCourse(course);
                TempData["Message"] = "Thêm khóa học thành công!";
            }
            return RedirectToAction("ManagerCourse");
        }


        [HttpPost]
        public IActionResult UpdateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseFacade.UpdateCourse(course);
                TempData["Message"] = "Cập nhật khóa học thành công!";
            }
            return RedirectToAction("ManagerCourse");
        }

        [HttpPost]
        public IActionResult SaveCourse(Course course)
        {
            if (course.CourseId == 0)
            {
                _courseRepository.Add(course);
                TempData["Message"] = "Thêm khóa học thành công!";
            }
            else
            {
                _courseRepository.Update(course);
                TempData["Message"] = "Cập nhật khóa học thành công!";
            }
            return RedirectToAction("ManagerCourse");
        }


        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            _courseFacade.DeleteCourse(id);
            TempData["Message"] = "Xóa khóa học thành công!";
            return RedirectToAction("ManagerCourse");
        }
    }
}
