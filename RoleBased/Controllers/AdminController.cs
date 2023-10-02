using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;
using System.Linq;

namespace RoleBased.Controllers
{
    public class AdminController : Controller
    {
        private readonly IApplicationUserService  _userService;
        private readonly UnitOfWorkRepoImpl _unitOfWorkRepoImpl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDb _db;
        private readonly IMaterialServicecs _materialServicecs;

        public AdminController(IMaterialServicecs materialServicecs, UserManager<ApplicationUser> userManager,ApplicationDb applicationDb,IApplicationUserService applicationUserService, RoleManager<IdentityRole> roleManager, UnitOfWorkRepoImpl unitOfWorkRepoImpl, IHttpContextAccessor httpContextAccessor)
        {
            _userService = applicationUserService;
            _unitOfWorkRepoImpl = unitOfWorkRepoImpl;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = applicationDb;
            _materialServicecs = materialServicecs;
       

        }

    
        public async Task< IActionResult>  Display()
        {

            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userDetails = _userService.GetById(userId);

            var role = _db.Roles.Where(x=>x.Id == userDetails.UserRoleId).Select(r=>r.Name).FirstOrDefault();

            

            var countUserByRoleId = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getAll().Where(x=>x.UserRoleId == userDetails.UserRoleId).Count();

            ApplicationUserVM applicationUserVM = new ApplicationUserVM();
            applicationUserVM.countUser = countUserByRoleId;
            applicationUserVM.UserRole = role;

            var filterMechanics = _userService.GetAll().Where(x => x.UserRoleId == "83d2a1f5-f368-4bf6-8f6c-1c36ceaafe8b").Count();
              
            var filterUsers = _userService.GetAll().Where(x => x.UserRoleId == "16e6789f-3169-4d70-817f-fd081e3900f1").Count();


         

            applicationUserVM.filterMechanics = filterMechanics;
            applicationUserVM.filterUser = filterUsers;

            var Totalmaterials = _materialServicecs.GetAll().ToList().Count();
            applicationUserVM.TotalMaterials = Totalmaterials;

            var getSpecificUserData = _materialServicecs.GetByUserId().ToList().Count();
            applicationUserVM.GetSpecificUserData = getSpecificUserData;



            int AcceptedUser = _materialServicecs.GetAll().Where(x=>x.userId== userId && x.Status == Enum.MaterialStatus.Approved).Count();
            applicationUserVM.AcceptedUsers = AcceptedUser;

            int RejectedUser = _materialServicecs.GetAll().Where(x=>x.userId == userId).Count(x=>x.Status == Enum.MaterialStatus.Rejected );
            applicationUserVM.RejectedUsers= RejectedUser;








            var alluser =  _userService.GetAll().ToList().Count();
            applicationUserVM.AllUser = alluser;


            return View(applicationUserVM);
        }

      

        [HttpGet]
        public async Task<IActionResult> EditProfilePicture()
        {
            ApplicationUserVM applicationUserVM = new ApplicationUserVM();
            applicationUserVM.Name = _httpContextAccessor.HttpContext.User.Identity.Name;
            return View(applicationUserVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfilePicture(ApplicationUserVM applicationUserVM)
        {
            try
            {
                _userService.update(applicationUserVM);
                return Json(true);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(false);
            }
        }

        
        public async Task<IActionResult> DisplayUserDetails()
        {
            try
            {
                var Name = _httpContextAccessor.HttpContext.User.Identity.Name;
                var userDetails = _userService.GetByUsername(Name);

                ApplicationUserVM applicationUserVM = new ApplicationUserVM
                {
                    Fullname = userDetails.Fullname,
                    Name = Name,
                    Address = userDetails.Address,
                    Phone = userDetails.Phone,
                    Gender_Status = userDetails.Gender_Status,
                    ProfilePicture = userDetails.ProfilePicture,
                    Latitude = userDetails.Latitude,
                    Longitude = userDetails.Longitude,
                    Email = userDetails.Email,
                    
                };

                return View(applicationUserVM);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(false);
            }
        }

        public IActionResult DisplayMap()
        {
            return View();
        }
    }
}
