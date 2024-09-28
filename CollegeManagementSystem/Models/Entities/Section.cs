using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementSystem.Models.Entities
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }
        public string Title { get; set; }

        // naviagtion property
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; } 

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [ForeignKey("Grade")] 
        public int GradeId { get; set; }
        public GradeYear Grade { get; set; }

    }
}
