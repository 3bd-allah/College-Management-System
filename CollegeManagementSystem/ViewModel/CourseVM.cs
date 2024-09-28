using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.ViewModel
{
    public class CourseVM
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }


        [Required(ErrorMessage = "Credits is required")]
        [Range(1,3, ErrorMessage = "Credits must be between '1' and '3' hours ")]
        public int Credits { get; set; }
    }
}
