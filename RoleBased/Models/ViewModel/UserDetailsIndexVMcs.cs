using RoleBased.Helpher;

namespace RoleBased.Models.ViewModel
{
    public class UserDetailsIndexVMcs
    {
        public UserDetailsIndexVMcs()
        {
            UserDetailsViewModel = new UserDetailsViewModel();
            UserDetailsViewModes =new List<UserDetailsViewModel>();
            
        }

        public UserDetailsViewModel UserDetailsViewModel { get; set; }
        public List<UserDetailsViewModel> UserDetailsViewModes { get; set;}
        public PaginatedList<UserDetailsViewModel> UserDetailsPaginatedList { get; set; }
    }
}
