using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeManagementSystem.ViewModel
{
    public class RegisterVM
	{

        [Required(ErrorMessage ="Name is required")]
		[MinLength(3, ErrorMessage ="Name must be greater than '3' chars.")]
        [MaxLength(30, ErrorMessage = "Name must be Less than '30' chars.")]
        public string Name{ get; set; } = null!;

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Remote(action: "IsEmailNotExists", controller:"Account", ErrorMessage ="Email is already in use")]
		public string Email{ get; set; } = null!;

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password{ get; set; } = null!;

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("Password", ErrorMessage ="Password and Confirm password must be match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; } = null!;

		
        //[Required(ErrorMessage = "Grade is required")]
		public int GradeId { get; set; }	

        [Required(ErrorMessage = "User role is required")]
        [BindProperty]
        public UserTypeOptions UserType { get; set; } = UserTypeOptions.Student;
    }
}
