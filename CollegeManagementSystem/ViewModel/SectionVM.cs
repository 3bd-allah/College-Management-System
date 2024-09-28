using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.ViewModel
{
    public class SectionVM
    {
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId{ get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        public int InstructorId { get; set; }


        [Required(ErrorMessage = "Grade is required")]
        public int GradeId { get; set; }
    }
}
