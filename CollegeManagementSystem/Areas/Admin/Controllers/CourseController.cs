using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext context;

        public CourseController(AppDbContext _context)
        {
            this.context = _context;
        }

        //Read Courses
        public IActionResult GetAllCourses()
        {
            var courses = context.Courses.ToList();
            return View(courses);
        }

        public IActionResult CourseDetails(int id)
        {
            Course? course = context.Courses.Where(c => c.CourseId == id).SingleOrDefault();
            if(course is not null)
            {
                return View(course);
            }
            return NotFound();
        }


        // Create Course
        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(CourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(CourseController.AddCourse));
            }
            Course courseToAdd = new Course
            {
                CourseName = courseVM.CourseName,
                Credits = courseVM.Credits
            };
            context.Courses.Add(courseToAdd);
            context.SaveChanges();
            return RedirectToAction(nameof(CourseController.GetAllCourses));
        }


        // Update Courses

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course? courseToUpdate = context.Courses
                .Where(c => c.CourseId == id)
                .SingleOrDefault();

            CourseVM courseVM = new CourseVM
            {
                CourseId = courseToUpdate.CourseId,
                CourseName = courseToUpdate.CourseName,
                Credits = courseToUpdate.Credits
            };

            if(courseToUpdate is not null)
            {
                return View(courseVM);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCourse(CourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(EditCourse),"Course");
            }

            Course? updatedCourse = context.Courses.Where(c => c.CourseId == courseVM.CourseId).SingleOrDefault();
            if(updatedCourse is not null)
            {
                updatedCourse.CourseName = courseVM.CourseName;
                updatedCourse.Credits = courseVM.Credits;
                context.Courses.Update(updatedCourse);
                context.SaveChanges();
                return RedirectToAction(nameof(CourseController.GetAllCourses), "Course");
            }
            return NotFound();
        }

        // Delete Course => Delete 
        public IActionResult DeleteCourse (int id)
        {
            Course? courseToDelete = context.Courses.Where(c => c.CourseId == id).SingleOrDefault();
            if(courseToDelete is not null)
            {
                context.Courses.Remove(courseToDelete);
                context.SaveChanges();
                return RedirectToAction(nameof(CourseController.GetAllCourses));
            }
            return NotFound();
        }
    }
}
