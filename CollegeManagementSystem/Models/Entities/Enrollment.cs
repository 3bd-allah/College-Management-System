using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementSystem.Models.Entities
{
    [PrimaryKey("StudentId", "SectionId")]
    public class Enrollment
    {

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
