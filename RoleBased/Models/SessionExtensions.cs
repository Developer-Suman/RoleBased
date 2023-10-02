
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RoleBased.Models;

public static class SessionExtensions
{
    public static IApplicationBuilder UseSession(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var signInManager = context.RequestServices.GetService<SignInManager<IdentityUser>>();

            // Access or modify session values as needed
            if (context.Request.Method == "POST" && context.Request.Path == "Account/Login")
            {
                var result = await signInManager.PasswordSignInAsync(context.Request.Form["username"], context.Request.Form["password"], false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Store credentials in session
                    var session = context.RequestServices.GetService<IHttpContextAccessor>().HttpContext.Session;
                    session.SetString("Username", context.Request.Form["username"]);
                    session.SetString("Password", context.Request.Form["password"]);
                }
            }

            await next.Invoke();
        });

        return app;
    }
}
