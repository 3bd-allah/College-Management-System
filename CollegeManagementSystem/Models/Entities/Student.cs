using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementSystem.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        // naviagtion property
        [ForeignKey("GradeYear")]
        public int GradeYeadID { get; set; }
        public GradeYear GradeYear { get; set; } = null!;
    }
}
