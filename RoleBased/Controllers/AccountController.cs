using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;

namespace RoleBased.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAuthenticationServices _services;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UnitOfWorkRepoImpl _unitOfWorkRepo;
        private readonly IMaterialServicecs _materialService;
        

        public AccountController(IUserAuthenticationServices userAuthenticationServices,IMaterialServicecs materialServicecs, RoleManager<IdentityRole> identityRole, UnitOfWorkRepoImpl unitOfWorkRepoImpl)
        {
            _services = userAuthenticationServices;
            _roleManager = identityRole;
            _unitOfWorkRepo = unitOfWorkRepoImpl;
            _materialService = materialServicecs;
           
        }

        public async Task<IActionResult> UserRegistration()
        {

            List<DropDownCategoriesHelpher> dropDownCategoriesHelphers = new List<DropDownCategoriesHelpher>();
            dropDownCategoriesHelphers = _materialService.GetCategoriesDropDown();
            ViewBag.CategoryDropDown = new SelectList(dropDownCategoriesHelphers, "CategoriesId", "CategoriesName");




            var roles = await _roleManager.Roles.ToListAsync();
            var roleList = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(),

            });

            RegistrationModel model = new RegistrationModel
            {
                RoleList = roleList,
            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration(RegistrationModel registrationModel)
        {
            if(!ModelState.IsValid)
            {
                return View(registrationModel);
            }
            var result  = await _services.RegistrationAsync(registrationModel);
            TempData["msg"] = result.StatusCode;
            return RedirectToAction(nameof(UserRegistration));
        }


        public async Task<IActionResult>  Registration()
        {

            List<DropDownCategoriesHelpher> dropDownCategoriesHelphers = new List<DropDownCategoriesHelpher>();
            dropDownCategoriesHelphers = _materialService.GetCategoriesDropDown();
            ViewBag.CategoryDropDown = new SelectList(dropDownCategoriesHelphers, "CategoriesId", "CategoriesName");


            var roles = await _roleManager.Roles.ToListAsync();
            var roleList = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(),

            });

            RegistrationModel model = new RegistrationModel
            {
                RoleList = roleList,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {

            //if (!ModelState.IsValid)
            //{

            //    return View(registrationModel);

            //}
            //registrationModel.Role = "user";
            var result = await _services.RegistrationAsync(registrationModel);


            TempData["msg"] = result.StatusMessage;

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _services.LoginAsync(model);

            //HttpContext.Session.SetString("USERID", userId);
            HttpContext.Session.SetString("USERNAME", model.Username);





            if (result.StatusCode == 1 && User.IsInRole("Admin") )
            {
                return RedirectToAction("Display", "Admin");
            }

            else if (result.StatusCode == 1 && User.IsInRole("User"))
            {
                return RedirectToAction("Display", "Admin");
            }
            else if(result.StatusCode == 1 && User.IsInRole("Mechanics"))
            {
                return RedirectToAction("Display", "Admin");
            }
            else
            {
                
                return RedirectToAction(nameof(Login));
            }
            TempData["msg"] = result.StatusMessage;


        }

        //[Authorize]
        public async Task<IActionResult> Logout()
        {
            await _services.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationModel
            {
                Username = "Admin",
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = "Admin@123",
           

            };
            model.Role = "admin";
            var result = await _services.RegistrationAsync(model);
            return Ok(result);
        }

        //public async Task<IActionResult> FetchRoles()
        //{
        //    var roles = await _roleManager.Roles.ToListAsync();
        //    var roleList = roles.Select(r => new SelectListItem
        //    {
        //        Text = r.Name,
        //        Value = r.Id.ToString()
        //    });

        //    RegistrationModel model = new RegistrationModel
        //    {
        //        RoleList = roleList
        //    };

        //    return Ok(model);
        //}

        [HttpGet]
        public async Task<IActionResult> AddUserDetails()
        {
            return View();
        }

        





    }
}
