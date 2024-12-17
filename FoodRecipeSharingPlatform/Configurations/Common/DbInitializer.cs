using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Enitities.Identity;
using Microsoft.AspNetCore.Identity;
using FoodRecipeSharingPlatform.Services.Helpers;
using FoodRecipeSharingPlatform.Enums;
namespace FoodRecipeSharingPlatform.Configurations.Common;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public DbInitializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }
    public async Task SeedingData()
    {
        Console.WriteLine("Seeding data...");
        foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
        {
            var name = role.GetEnumName();
            var roleInDb = _roleManager.Roles.SingleOrDefault(x => x.Name == role.GetEnumDisplayName());
            if (roleInDb == null)
            {
                await _roleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    NormalizedName = name.ToUpper()
                });
            }

        }

        if (await _userManager.FindByNameAsync("admin1") == null)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "vuthanhtam12062003@gmail.com",
                NormalizedEmail = "VUTHANHTAM12062003@GMAIL.COM",
                PhoneNumber = "0123456789",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                EmailConfirmed = true,
            };
            var createResult = await _userManager.CreateAsync(user, "Admin123!");
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new Exception($"Failed to create admin user: {errors}");
            }
            var roleAddResult = await _userManager.AddToRoleAsync(user, nameof(RoleEnum.Admin));
            if (!roleAddResult.Succeeded)
            {
                throw new Exception("Failed to assign role to admin user: " + string.Join(", ", roleAddResult.Errors.Select(e => e.Description)));
            }
            _context.SaveChanges();
        }
    }
}