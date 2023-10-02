using AutoMapper;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;

namespace RoleBased.Automapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Material, MaterialsViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserVM>().ReverseMap();

        }
        
    }
}
