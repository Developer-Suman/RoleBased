using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;

namespace RoleBased.Services.ServiceInterface
{
    public interface ICategoriesServices
    {
        void save(CategoryViewModel categoryViewModel);
        void update(CategoryViewModel categoryViewModel);
        void delete(int id);
        ApplicationUser GetByUsername(string username);

        CategoryViewModel GetById(int id);
        List<CategoryViewModel> GetAll();
    }
}
