using Gettit.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gettit.Web.Seed
{
    public static class DatabaseSeedUtilities
    {
        public static void UseDatabaseSeed(this WebApplication app)
        {
            SeedRoles(app);
        }

        public static void SeedRoles(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                using (var gettitDbContext = serviceScope.ServiceProvider.GetRequiredService<GettitDbContext>())
                {
                    gettitDbContext.Database.Migrate();

                    if (gettitDbContext.Roles.Count() == 0)
                    {
                        IdentityRole adminRole = new IdentityRole();
                        adminRole.Name = "Administrator";
                        adminRole.NormalizedName = adminRole.Name.ToUpper();

                        IdentityRole moderatorRole = new IdentityRole();
                        moderatorRole.Name = "Moderator";
                        moderatorRole.NormalizedName = moderatorRole.Name.ToUpper();

                        IdentityRole userRole = new IdentityRole();
                        userRole.Name = "User";
                        userRole.NormalizedName = userRole.Name.ToUpper();

                        gettitDbContext.Add(adminRole);
                        gettitDbContext.Add(moderatorRole);
                        gettitDbContext.Add(userRole);

                        gettitDbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
