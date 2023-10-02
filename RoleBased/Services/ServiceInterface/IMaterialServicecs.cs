using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;

namespace RoleBased.Services.ServiceInterface
{
    public interface IMaterialServicecs
    {
        void save(MaterialsViewModel materialsViewModel);
        void update(MaterialsViewModel materialsViewModel);
        void delete(int id);
        ApplicationUser GetByUsername(string username);
        MaterialsViewModel GetById(int materialsId);
        List<MaterialsViewModel> GetByUserId();
        List<DropDownCategoriesHelpher> GetCategoriesDropDown();
        List<MaterialsViewModel> GetAll();
    }
}
