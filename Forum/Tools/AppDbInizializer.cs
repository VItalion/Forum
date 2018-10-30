using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Forum.Models;
using Forum.Repositories;

namespace Forum.Tools
{
    public class AppDbInizializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = new IdentityRole("admin");
            var userRole = new IdentityRole("user");

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser() { Email = "user.forum.mail.test@gmail.com", UserName = "user.forum.mail.test@gmail.com", EmailConfirmed = true };
            var pwd = "UberPwd3123";

            var testUser = new ApplicationUser() { Email = "test@gmail.com", UserName = "test@gmail.com", EmailConfirmed = true };
            var testPwd = "testPassword1";

            var result = userManager.Create(admin, pwd);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, userRole.Name);
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            var testResult = userManager.Create(testUser, testPwd);
            if (testResult.Succeeded)
            {
                userManager.AddToRole(testUser.Id, userRole.Name);
            }

            base.Seed(context);
        }
    }
}