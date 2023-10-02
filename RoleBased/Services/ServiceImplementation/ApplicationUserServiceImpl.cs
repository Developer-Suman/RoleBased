using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RoleBased.Enum;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;

namespace RoleBased.Services.ServiceImplementation
{
    public class ApplicationUserServiceImpl : IApplicationUserService
    {
        private readonly UnitOfWorkRepoImpl _unitOfWorkRepoImpl;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUserServiceImpl(UnitOfWorkRepoImpl unitOfWorkRepoImpl, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWorkRepoImpl = unitOfWorkRepoImpl;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        

        }
        public void delete(int userId)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUserVM> GetAll()
        {
            try
            {
                List<ApplicationUserVM> applicationUserVMs = new List<ApplicationUserVM>();
                var applicationUser = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getAll();


                

                foreach (var item in applicationUser)
                {
                    //var user = _userManager.FindByIdAsync(item.Id).Result;
                    //var UserRoles = _userManager.GetRolesAsync(user);


                    applicationUserVMs.Add(new ApplicationUserVM
                    {
                        userId = item.Id,
                        UserRoleId = item.UserRoleId,
                        
                        ProfilePicture = item.ProfilePicture,
                        Fullname = item.Fullname,
                        Name = item.Name,


                    });
                }


                return applicationUserVMs;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ApplicationUserVM GetById(string userId)
        {
            try
            {
                var userData = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getByString(userId);
                ApplicationUserVM applicationUserVM = new ApplicationUserVM()
                {
                    Name = userData.Name,
                    Address = userData.Address,
                    Phone = userData.Phone,
                    Fullname = userData.Fullname,
                    Email = userData.Email,
                    UserRoleId = userData.UserRoleId,
                    userId = userData.Id
                };

                return applicationUserVM;
                

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void save(ApplicationUserVM applicationUser)
        {
            throw new NotImplementedException();
        }


        private string UploadFile(ApplicationUserVM applicationUser)
        {
            string filename = null;
            if(applicationUser.File != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + applicationUser.File.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicationUser.File.CopyTo(fileStream);
                }

            }

            return filename;

        }

        public void update(ApplicationUserVM applicationUser)
        {
            try
            {
                applicationUser.userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                applicationUser.ProfilePicture = UploadFile(applicationUser);


                ApplicationUserVM applicationUserVM = new ApplicationUserVM()
                {
                    userId = applicationUser.userId,
                    ProfilePicture = applicationUser.ProfilePicture,
                    Fullname = applicationUser.Fullname,
                    Address = applicationUser.Address,
                    Phone = applicationUser.Phone,
                    Gender_Status = applicationUser.Gender_Status
                };

                var appUser = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getByString(applicationUserVM.userId);

                if (appUser != null)
                {
                    // Update the entity with data from the view model
                    appUser.ProfilePicture = applicationUserVM.ProfilePicture;
                    appUser.Fullname = applicationUserVM.Fullname;
                    appUser.Phone = applicationUserVM.Phone;
                    appUser.Address = applicationUserVM.Address;
                    appUser.Gender_Status = applicationUser.Gender_Status;

                    // Save the changes
                    _unitOfWorkRepoImpl.Repository<ApplicationUser>().update(appUser);
                    _unitOfWorkRepoImpl.Commit();
                }
                else
                {
                    // Handle the case where the entity does not exist
                    // You can throw an exception or return an appropriate response
                }



                //var applicationUserTobeUpdated = _mapper.Map<ApplicationUser>(applicationUserVM);
                //if (applicationUserVM != null)
                //{
                //    _unitOfWorkRepoImpl.Repository<ApplicationUser>().update(appUser);
                //    _unitOfWorkRepoImpl.Commit();

                //}

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ApplicationUser GetByUsername(string username)
        {
            try
            {
                var user = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getAll().Where(x=>x.UserName == username).FirstOrDefault();
                return user;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
