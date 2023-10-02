using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;

namespace RoleBased.Services.ServiceInterface
{
    public interface IUserDetailsServices
    {
        void save(UserDetailsViewModel userDetailsViewModel);
        void update(UserDetailsViewModel userDetailsViewModel);
        void delete(int id);
        ApplicationUser GetByUsername(string username);
        UserDetailsViewModel GetById(int userdetailsId);
        List<UserDetailsViewModel> GetAll();
    }
}
