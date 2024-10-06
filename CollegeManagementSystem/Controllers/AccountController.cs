using CollegeManagementSystem.Areas.Admin.Controllers;
using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.Entities;
using CollegeManagementSystem.Models.Enums;
using CollegeManagementSystem.Models.IdentityEntities;
using CollegeManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CollegeManagementSystem.Controllers
{

	[AllowAnonymous]
	public class AccountController : Controller
	{
		// this class used for create new user (Repository layer)
		private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> singInManager;
		private readonly RoleManager<ApplicationRole> roleManager;
        private readonly AppDbContext context;

		public AccountController(UserManager<ApplicationUser> _userManger, 
			SignInManager<ApplicationUser> _singInManager,
			RoleManager<ApplicationRole> _roleManager,
			AppDbContext _context)
        {
            this.userManager = _userManger;
            this.singInManager = _singInManager;
			this.roleManager = _roleManager;
			this.context = _context;
        }

        [HttpGet]
		[Authorize("NotAuthorized")]
        public IActionResult Register()
		{
			ViewBag.Grades = context.Grades.ToList();
			return View();
		}

		[HttpPost]	
		public async Task <IActionResult> Register(RegisterVM register)
		{
			
			// Check for validation errors 
			if(ModelState.IsValid == false)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors)
													.Select(temp => temp.ErrorMessage);
				return View(register);
			}

			// create user object
			ApplicationUser user = new ApplicationUser 
			{ 
				Email = register.Email , 
				PasswordHash= register.Password, 
				UserName= register.Email, 
				StudentName= register.Name
			};

			// add user to 'AspNet Users table' if not exist 
			IdentityResult result = await userManager.CreateAsync(user, register.Password);
			
			if(result.Succeeded)
			{
				if (register.UserType == UserTypeOptions.Admin)
				{
					//check for 'Admin' role
					if( await roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
					{
						ApplicationRole adminRole = new ApplicationRole { 
							Name = UserTypeOptions.Admin.ToString() 
						};

						await roleManager.CreateAsync (adminRole);
							

						// Add user to 'Admin' role 
						await userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
						return RedirectToAction(nameof(MainController.Index), "Main", new {area = "Admin"});
					}
				}
				else
				{
					// check for student role 
					if(await roleManager.FindByNameAsync(UserTypeOptions.Student.ToString()) is null){
						// create 'Student' role
						ApplicationRole studentRole = new ApplicationRole
						{
							Name = UserTypeOptions.Student.ToString()
						};
						// add 'Student' role in 'AspNetRoles' table 
						await roleManager.CreateAsync(studentRole);
					}


					await userManager.AddToRoleAsync(user, UserTypeOptions.Student.ToString());
					Student studentToAddInDB = new Student
					{
						StudentId = user.Id,
						Name = user.StudentName,
						Email = user.Email,
						Password = register.Password,
						GradeYeadID = register.GradeId,
					};
					await context.Students.AddAsync(studentToAddInDB);

					await context.SaveChangesAsync();
				}

				await singInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(nameof(CollegeManagementSystem.Controllers.StudentController.Index),
					"Student",
					new {email = user.Email});
			}
			else
			{
				foreach(IdentityError error in result.Errors)
				{
					ModelState.AddModelError("Register", error.Description);
				}
				return View(register);
			}
		}

		[HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
        public async Task<IActionResult>Login(LoginVM loginVM, string returnUrl)
		{
			// check for validation
			if(!ModelState.IsValid)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(err => err.ErrorMessage);
				return View(loginVM);
			}

			// sign in business logic 
			var result = await singInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, 
				isPersistent: false, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				ApplicationUser user = await userManager.FindByEmailAsync(loginVM.Email);
				if (user is not null)
				{
					if(await userManager.IsInRoleAsync(user, "Admin"))
					{
						return RedirectToAction(nameof(HomeController.Index), "Home", new
						{
							area ="Admin"
						});
					}
				}
				if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
				{
					return LocalRedirect(returnUrl);
				}
				return RedirectToAction(nameof(HomeController.Privacy),"Home");
			}

			ModelState.AddModelError("Login", "In correct email or password");
			return View(loginVM);

		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginVM loginVM)
        {
            // check for validation
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values
					.SelectMany(temp => temp.Errors)
					.Select(err => err.ErrorMessage);
                return View(loginVM);
            }

            // sign in business logic 
            var result = await singInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(loginVM.Email);
                if (user is not null)
                {
                    if (await userManager.IsInRoleAsync(user, "Admin"))
                    {
						return RedirectToAction(nameof(MainController.Index), "Main", new {area = "Admin"});
                    }

					return RedirectToAction(nameof(CollegeManagementSystem.Controllers.StudentController.Index),
						"Student", 
						new {email = user.Email});
                }
            }

            ModelState.AddModelError("Login", "In correct email or password");
            return View(loginVM);

        }

        public async Task <IActionResult> Logout()
		{
			await singInManager.SignOutAsync();
			return RedirectToAction(nameof(HomeController.Privacy), "Home");
		}

		public async Task<IActionResult> IsEmailNotExists(string email)
		{
			
			ApplicationUser? user = await userManager.FindByEmailAsync(email);

			if(user is null)
			{
				return Json(true);
			}
			else
			{
				return Json(false);
			}
		}
	}
}
