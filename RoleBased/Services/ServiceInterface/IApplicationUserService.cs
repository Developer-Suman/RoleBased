using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;

namespace RoleBased.Services.ServiceInterface
{
    public interface IApplicationUserService
    {
        void save(ApplicationUserVM applicationUser);
        void update(ApplicationUserVM applicationUser);
        void delete(int userId);
        ApplicationUserVM GetById(string userId);
        ApplicationUser GetByUsername(string username);
        List<ApplicationUserVM> GetAll();
    }
}
