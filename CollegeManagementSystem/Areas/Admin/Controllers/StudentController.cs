using CollegeManagementSystem.Controllers;
using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class StudentController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public StudentController(AppDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            this.context = _context;
            userManager = _userManager;
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
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student? studentToDelete = context.Students.Where(st => st.StudentId == id).SingleOrDefault();

            if(studentToDelete is not null)
            {
                string studentEmail = studentToDelete.Email;
                ApplicationUser? userToDelete = await userManager.FindByEmailAsync(studentEmail);

                if(userToDelete is not null)
                {
                    await userManager.DeleteAsync(userToDelete);
                }
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(StudentController.GetAll));
        }
    }
}
