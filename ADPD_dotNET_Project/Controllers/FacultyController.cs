using Microsoft.AspNetCore.Mvc;
using ADPD_dotNET_Project.Facade;

namespace ADPD_dotNET_Project.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ICourseFacade _courseFacade;

        public FacultyController(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        // Giao diện giảng viên xem danh sách môn học của họ
        public IActionResult FacultyCourse()
        {
            var courses = _courseFacade.GetAllCourses(); // Sau này có thể lọc theo FacultyId
            return View(courses);
        }
    }
}
