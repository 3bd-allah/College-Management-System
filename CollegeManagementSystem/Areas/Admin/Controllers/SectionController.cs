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
        public IActionResult EditSection(int id)
        {
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Instructors = context.Instructors.ToList();
            ViewBag.Grades = context.Grades.ToList();

            Section? sectionToUpdate = context.Sections.Where(sec => sec.SectionId == id).SingleOrDefault();

            if(sectionToUpdate is not null)
            {
                SectionVM sectionVM = new SectionVM
                {
                    SectionId = sectionToUpdate.SectionId,
                    Title = sectionToUpdate.Title,
                    InstructorId = sectionToUpdate.InstructorId,
                    CourseId  = sectionToUpdate.CourseId,
                    GradeId = sectionToUpdate.GradeId
                };
                return View(sectionVM);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditSection(SectionVM sectionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sectionVM);
            }

            Section? updatedSection = context.Sections
                .Where(sec => sec.SectionId == sectionVM.SectionId)
                .SingleOrDefault();
            if(updatedSection is not null)
            {
                updatedSection.Title = sectionVM.Title;
                updatedSection.CourseId = sectionVM.CourseId;
                updatedSection.InstructorId = sectionVM.InstructorId;
                updatedSection.GradeId = sectionVM.GradeId;
                context.Sections.Update(updatedSection);
                context.SaveChanges();
                return RedirectToAction(nameof(SectionController.GetAllSections));
            }
            return NotFound();

        }

        public IActionResult DeleteSection(int id)
        {
            Section? section = context.Sections.Where(sec => sec.SectionId == id).SingleOrDefault();
            if (section != null)
            {
                context.Sections.Remove(section);
                context.SaveChanges();
                return RedirectToAction(nameof(SectionController.GetAllSections));
            }
            return NotFound();
        }
    }
}
