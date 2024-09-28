using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email should be in a proper format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
