using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using ADPD_dotNET_Project.Controllers;
using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;

namespace ADPD_dotNET_Project.Tests
{
    public class CourseControllerTests
    {
        private Mock<ICourseRepository> _mockCourseRepo;
        private CourseController _controller;

        public CourseControllerTests()
        {
            _mockCourseRepo = new Mock<ICourseRepository>();
            _controller = new CourseController(_mockCourseRepo.Object);
        }

        private void SimulateSessionWithUser(int roleId)
        {
            var user = new User { UserId = 1, RoleId = roleId, Email = "test@user.com" };
            var json = JsonConvert.SerializeObject(user);

            var context = new DefaultHttpContext();
            context.Session = new TestSession();
            context.Session.SetString("CurrentUser", json);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = context
            };
        }

        [Fact]
        public void Index_ShouldRedirect_WhenUserNotLoggedIn()
        {
            var result = _controller.Index();
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Index_ShouldForbid_WhenUserIsNotAdmin()
        {
            SimulateSessionWithUser(roleId: 2);
            var result = _controller.Index();
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void Index_ShouldReturnView_WhenUserIsAdmin()
        {
            SimulateSessionWithUser(roleId: 1);
            _mockCourseRepo.Setup(repo => repo.GetAll()).Returns(new List<Course>());

            var result = _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~/Views/Manager/ManagerCourse.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void StudentCourses_ShouldRedirect_WhenNotLoggedIn()
        {
            var result = _controller.StudentCourses();
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void StudentCourses_ShouldForbid_WhenNotStudent()
        {
            SimulateSessionWithUser(1);
            var result = _controller.StudentCourses();
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void StudentCourses_ShouldReturnView_WhenStudent()
        {
            SimulateSessionWithUser(3);
            _mockCourseRepo.Setup(r => r.GetAll()).Returns(new List<Course>());

            var result = _controller.StudentCourses();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~/Views/Course/StudentCourses.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void FacultyCourses_ShouldForbid_WhenNotFaculty()
        {
            SimulateSessionWithUser(1);
            var result = _controller.FacultyCourses();
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void FacultyCourses_ShouldReturnView_WhenFaculty()
        {
            SimulateSessionWithUser(2);
            _mockCourseRepo.Setup(r => r.GetAll()).Returns(new List<Course>());

            var result = _controller.FacultyCourses();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("~/Views/Faculty/FacultyCourse.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void SaveCourse_ShouldCallAdd_WhenCourseIdIsZero()
        {
            var newCourse = new Course { CourseId = 0 };
            var result = _controller.SaveCourse(newCourse);

            _mockCourseRepo.Verify(r => r.Add(newCourse), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void SaveCourse_ShouldCallUpdate_WhenCourseIdExists()
        {
            var existing = new Course { CourseId = 1 };
            var result = _controller.SaveCourse(existing);

            _mockCourseRepo.Verify(r => r.Update(existing), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
