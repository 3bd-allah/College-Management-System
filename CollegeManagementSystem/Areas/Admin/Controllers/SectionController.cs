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
    public class SectionController : Controller
    {
        private readonly AppDbContext context;

        public SectionController(AppDbContext _context)
        {
            this.context = _context;
        }


        // Read Sections  => Read
        public IActionResult GetAllSections()
        {
            var sections = context.Sections
                .Include(s => s.Course)
                .Include(s => s.Instructor)
                .Include(s => s.Grade)
                .ToList();
            return View(sections);
        }


        // Add Section  => Create 
        [HttpGet]
        public IActionResult AddSection()
        {
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Instructors = context.Instructors.ToList();
            ViewBag.Grades = context.Grades.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult AddSection(SectionVM sectionVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(SectionController.AddSection),sectionVM);
            }
            Section sectionToAdd = new Section
            {
                Title = sectionVM.Title,
                InstructorId = sectionVM.InstructorId,
                CourseId = sectionVM.CourseId,
                GradeId = sectionVM.GradeId
            };
            context.Sections.Add(sectionToAdd);
            context.SaveChanges();
            return RedirectToAction(nameof(SectionController.GetAllSections));
        }



        // Edit Section => Update 
        [HttpGet]
        public IActionResult EditSection()
        {
            
            return View();
        }

        public IActionResult EditSection(SectionVM sectionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sectionVM);
            }

            return RedirectToAction(nameof(SectionController.GetAllSections));
        }

    }
}
