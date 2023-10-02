using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Services.ServiceInterface;
using System.Data;
using System.Security.Claims;

namespace RoleBased.Services.ServiceImplementation
{
    public class UserAuthenticationService : IUserAuthenticationServices
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        //private ISession _session => _contextAccessor.HttpContext.Session;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;

        }

        //public Task GetUserDetails()
        //{
        //    var user = HttpContext.User;
        //    var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var userName = user.FindFirstValue(ClaimTypes.Name);
        //    var userEmail = user.FindFirstValue(ClaimTypes.Email);
        //}

        public async Task<Status> LoginAsync(LoginModel loginModel)
        {
            var status = new Status();
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.StatusMessage = "Invalid Username";
                return status;

            }

            //_session.SetString("UserName", user.UserName);

            //var userName = _session.GetString("UserName");

            //Session["UserName"] = user.UserName;

            // we will match the password
            if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                status.StatusCode = 0;
                status.StatusMessage = " Invalid Password";
                return status;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, true, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, userRole));

                }

                //if(userRoles.Contains("admin"))
                //{
                

                //}


                status.StatusCode = 1;
                status.StatusMessage = "Logged In Successsfully";
              
                return status;
            }

            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.StatusMessage = "User Locked Out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.StatusMessage = "Error in Logging In";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
           
        }



        //public async Task<Status> RegistrationAsyncFromUser(RegistrationModel registrationModel)
        //{
        //    var status = new Status();
        //    var userExist = await _userManager.FindByNameAsync(registrationModel.Username);
        //    if (userExist != null)
        //    {
        //        status.StatusCode = 0;
        //        status.StatusMessage = "User Already exists";
        //        return status;
        //    }



        //    ApplicationUser user = new ApplicationUser
        //    {
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        Name = registrationModel.Name,
        //        Email = registrationModel.Email,
        //        UserName = registrationModel.Username,
        //        CategoriesId = registrationModel.Categories_Id,
        //        EmailConfirmed = true,


        //        UserRoleId = !string.IsNullOrEmpty(registrationModel.Role) ? registrationModel.Role : "16e6789f-3169-4d70-817f-fd081e3900f1",

        //        //Admin
        //        //UserRoleId = registrationModel.Role,


        //    };

        //    var result = await _userManager.CreateAsync(user, registrationModel.Password);
        //    if (!result.Succeeded)
        //    {
        //        status.StatusCode = 0;
        //        status.StatusMessage = "User Creation Failed";
        //        return status;
        //    }

        //    //Roles Mnagement




        //    var role = await _roleManager.FindByIdAsync(registrationModel.Role);
        //    if (role != null)
        //    {
        //        await _userManager.AddToRoleAsync(user, role.Name);

        //    }
        //    else
        //    {
        //        await _userManager.AddToRoleAsync(user, "User");

        //    }





        //    //Admin
        //    //if (!await _roleManager.RoleExistsAsync("Admin"))
        //    //{
        //    //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    //}
        //    //if (await _roleManager.RoleExistsAsync(registrationModel.Role))
        //    //{
        //    //    await _userManager.AddToRoleAsync(user, registrationModel.Role);
        //    //}

        //    status.StatusCode = 1;
        //    status.StatusMessage = "User has registered successfully";
        //    return status;
        //}

        public async Task<Status> RegistrationAsync(RegistrationModel registrationModel)
        {
            var status = new Status();
            var userExist = await _userManager.FindByNameAsync(registrationModel.Username);
            if (userExist != null)
            {
                status.StatusCode = 0;
                status.StatusMessage = "User Already exists";
                return status;
            }



            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = registrationModel.Name,
                Email = registrationModel.Email,
                UserName = registrationModel.Username,
                CategoriesId = registrationModel.Categories_Id,
                Latitude = registrationModel.Latitude,
                Longitude = registrationModel.Longitude,
                EmailConfirmed = true,
              

                UserRoleId = !string.IsNullOrEmpty(registrationModel.Role) ? registrationModel.Role : "16e6789f-3169-4d70-817f-fd081e3900f1",

                //Admin
                //UserRoleId = registrationModel.Role,


            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.StatusMessage = "User Creation Failed";
                return status;
            }

            //Roles Mnagement




            var role = await _roleManager.FindByIdAsync(registrationModel.Role);
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);

            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");

            }





            //Admin
            //if (!await _roleManager.RoleExistsAsync("Admin"))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //}
            //if (await _roleManager.RoleExistsAsync(registrationModel.Role))
            //{
            //    await _userManager.AddToRoleAsync(user, registrationModel.Role);
            //}

            status.StatusCode = 1;
            status.StatusMessage = "User has registered successfully";
            return status;
        }

        

        
    }
}
