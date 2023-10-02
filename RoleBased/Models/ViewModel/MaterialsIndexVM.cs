using RoleBased.Enum;
using RoleBased.Helpher;
using RoleBased.Models.Domain;

namespace RoleBased.Models.ViewModel
{
    public class MaterialsIndexVM
    {
        public MaterialsIndexVM()
        {
            MaterialsViewModel = new MaterialsViewModel();
            MaterialsViewModels =new List<MaterialsViewModel>();
            RecommendProduct = new List<Material>();
         


        }

        public List<Material> RecommendProduct { get; set; }

        public string UseStatus { get; set; }

        public MaterialsViewModel MaterialsViewModel { get; set; }
        public List<MaterialsViewModel> MaterialsViewModels { get; set; }

        public PaginatedList<MaterialsViewModel> MaterialspaginatedList { get; set; }
    }
}
