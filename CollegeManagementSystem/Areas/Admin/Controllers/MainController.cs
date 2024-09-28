using CollegeManagementSystem.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MainController : Controller
    {
        private readonly AppDbContext context;

        public MainController(AppDbContext _context)
        {
            this.context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageStudents()
        {
            return RedirectToAction(nameof(StudentController.GetAll),"Student");
        }

        public IActionResult ManageSections()
        {
            return RedirectToAction(nameof(SectionController.GetAllSections),"Section");
        }   

        public IActionResult ManageCourses()
        {
            return RedirectToAction(nameof(CourseController.GetAllCourses),"Course");
        }
    }
}
