using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(AppDbContext _context,
            UserManager<ApplicationUser> _userManager)
        {
            this.context = _context;
            this.userManager = _userManager;
        }


        //public IActionResult Index(int id)
        //{
        //    Student? student = context.Students.Where(st => st.StudentId == id).SingleOrDefault();
        //    if (student == null)
        //        return NotFound();
        //    return View(student);   
        //}

        public async Task<IActionResult> Index(string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            Student? student = context.Students.Where(st => st.StudentId == user.Id).SingleOrDefault();
            if (student == null)
                return NotFound();
            return View(student);
        }

        public IActionResult StudentDetails(int id)
        {
            Student? student = context.Students
                .Include(st => st.GradeYear)
                .Where(s => s.StudentId == id)
                .SingleOrDefault();

            if(student is not null)
            {
                return View(student);
            }
            return Problem("Error!");
        }

        public IActionResult StudentSections(int id)
        {
            Student? student = context.Students
                .Include(s => s.GradeYear)
                .Where(stu => stu.StudentId == id)
                .SingleOrDefault();

            if(student is not null)
            {
                var studentSecitons = context.Sections
                    .Include(s => s.Course)
                    .Include(s => s.Instructor)
                    .Where(sec => sec.GradeId == student.GradeYeadID)
                    .ToList();
                return View(studentSecitons);
            }
            return NotFound();
        }

        public IActionResult StudentCourses(int id)
        {
            Student? student = context.Students.Where(stu => stu.StudentId == id).SingleOrDefault();

            var coursesIDs = context.Sections
                .Include(sec => sec.Course)
                .Where(s => s.GradeId == student.GradeYeadID)
                .Select(s => s.CourseId).ToList();

            List<Course> coursesOfStudent = new();

            foreach (int courseId in coursesIDs)
            {
                Course course = context.Courses.Where(c => c.CourseId == courseId).SingleOrDefault();
                if (course is not null)
                {
                    coursesOfStudent.Add(course);
                }
            }

            if (coursesOfStudent.Count == 0)
            {
                return NotFound();
            }

            return View(coursesOfStudent);
        }
    }
}
