using CollegeManagementSystem.Models.Data;
using CollegeManagementSystem.Models.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CollegeManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser,ApplicationRole,AppDbContext,int>>()
                .AddRoleStore<RoleStore<ApplicationRole,AppDbContext,int>>();

            builder.Services.AddAuthorization(options =>
            {
                // enforces Authorization policy ( only authenticated users ) to access action methods 
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.FallbackPolicy = policy;

                // we define the name that we pase to [Authorize] attribute 
                options.AddPolicy("NotAuthorized", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        // that mean if the user is authenticated that action method or controller will not be accessible 
                        return !context.User.Identity.IsAuthenticated;
                    });
                });

            }); 

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting(); // indentifying action method based on route 

            app.UseAuthentication();    // Reading Identify cookie
            app.UseAuthorization();     // validates access permissions of the user 

            app.MapControllers();       // execute the filter pipline (action + method)

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllerRoute(
                    name:"areas",
                    pattern:"{area:exists}/{controller=Main}/{action=index}"
                    );

                endPoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run();
        }
    }
}
