using Microsoft.AspNetCore.Identity;

namespace CollegeManagementSystem.Models.IdentityEntities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string StudentName{ get; set; }
    }
}
