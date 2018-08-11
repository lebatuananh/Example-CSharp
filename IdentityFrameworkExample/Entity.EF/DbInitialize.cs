

namespace IdentityFrameworkExample.Entity.EF
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using System.Linq;

    public class DbInitialize
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitialize(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top Manager",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }

            if (!_context.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                }, "123654$");
                var userAdmin = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(userAdmin, "Admin");
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "staff",
                    FullName = "Staff",
                    Email = "staff@gmail.com",
                }, "123654$");
                var userStaff = await _userManager.FindByNameAsync("staff");
                await _userManager.AddToRoleAsync(userStaff, "Staff");
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "customer",
                    FullName = "Customer",
                    Email = "customer@gmail.com",
                }, "123654$");
                var userCustomer = await _userManager.FindByNameAsync("customer");
                await _userManager.AddToRoleAsync(userCustomer, "Customer");
            }
        }
    }
}
