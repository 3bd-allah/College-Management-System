using CollegeManagementSystem.Controllers;
using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class StudentController : Controller
    {
        private readonly AppDbContext context;

        public StudentController(AppDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult GetAll()
        {
            var students = context.Students.Include(stu => stu.GradeYear).ToList();
            return View(students);
        }

        public IActionResult StudentDetails(int id)
        {
            Student? student = context.Students.Include(st => st.GradeYear).Where(s => s.StudentId == id).SingleOrDefault();
            if (student is not null)
            {
                return View(student);
            }
            return NoContent();
        }


        // delete 
        public IActionResult DeleteStudent(int id)
        {
            Student? studentToDelete = context.Students.Where(st => st.StudentId == id).SingleOrDefault();

            if(studentToDelete is not null)
            {
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(StudentController.GetAll));
        }
    }
}
