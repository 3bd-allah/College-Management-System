using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int Credits { get; set; }

    }
}
