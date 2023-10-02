using RoleBased.Helpher;

namespace RoleBased.Models.ViewModel
{
    public class CategoryIndexVM
    {

        public CategoryIndexVM() 
        {
            CategoryViewModel = new CategoryViewModel();
            CategoryViewModels = new List<CategoryViewModel>();
        }
        public CategoryViewModel CategoryViewModel { get; set; }
        public List<CategoryViewModel> CategoryViewModels { get; set; }

        public PaginatedList<CategoryViewModel> CategoriesPaginatedList { get; set; }
    }
}
