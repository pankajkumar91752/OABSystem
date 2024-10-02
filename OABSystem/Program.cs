using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OABSystem.Areas.Identity.Data;
using OABSystem.Data;
namespace OABSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("OABSystemContextConnection") ?? throw new InvalidOperationException("Connection string 'OABSystemContextConnection' not found.");

            builder.Services.AddDbContext<OABSystemContext>(options =>options.UseSqlServer(connectionString));
           
            builder.Services.AddDefaultIdentity<OABSystemUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<OABSystemContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();
            app.MapRazorPages();
            app.MapAreaControllerRoute(name: "identity", areaName: "identity", pattern: "identity/Account/{action=login}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OABSystemContext>();
                context.Database.EnsureCreated(); // (Optional for development)

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<OABSystemUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var user = userManager.FindByEmailAsync("pankajkumar91752@gmail.com").Result;
                 roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                if (user != null && app.Environment.IsDevelopment())
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    var isadmin = userManager.IsInRoleAsync(user, "Admin").Result;


                }


                context.SeedAsync(scope.ServiceProvider).Wait();
            }
                app.Run();
        }
    }
}