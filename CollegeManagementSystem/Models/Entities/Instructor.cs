using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models.Entities
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string Name { get; set; }

    }
}
