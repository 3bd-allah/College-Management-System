using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models.Entities
{
    public class GradeYear
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
