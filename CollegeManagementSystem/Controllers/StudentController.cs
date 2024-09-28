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
            Student? student = context.Students.Where(stu => stu.StudentId == id).SingleOrDefault();
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
            if (student is not null)
            {
                var studentCourses= context.Sections.Include(c => c.Course).Where(sec => sec.GradeId == student.GradeYeadID).ToList();
                return View(studentCourses);
            }
            return NotFound();
        }
    }
}
