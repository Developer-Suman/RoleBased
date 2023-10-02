using RoleBased.Models.ViewModel;

namespace RoleBased.Services.ServiceInterface
{
    public interface IUserAuthenticationServices
    {
        Task<Status> LoginAsync(LoginModel loginModel);
        Task<Status> RegistrationAsync(RegistrationModel registrationModel);
        //Task<Status> RegistrationAsyncFromUser(RegistrationModel registrationModel);

        //Task GetUserDetails();
        Task LogoutAsync();
    }
}
