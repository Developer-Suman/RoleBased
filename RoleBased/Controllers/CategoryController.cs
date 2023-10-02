using Microsoft.AspNetCore.Mvc;
using RoleBased.Helpher;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;

namespace RoleBased.Controllers
{
    public class CategoryController : Controller
    {

        private readonly UnitOfWorkRepoImpl _unitOfWorkRepo;
        private readonly ICategoriesServices _categoriesServices;


        public CategoryController(UnitOfWorkRepoImpl unitOfWorkRepoImpl, ICategoriesServices categoriesServices)
        {
            _unitOfWorkRepo = unitOfWorkRepoImpl;
            _categoriesServices = categoriesServices;
            
        }
        [HttpGet]
        public IActionResult Category(int pageNumber = 1)
        {
            CategoryIndexVM categoryIndexVM = new CategoryIndexVM();

            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            categoryIndexVM.CategoryViewModel = categoryViewModel;
            categoryIndexVM.CategoryViewModels = _categoriesServices.GetAll();
            categoryIndexVM.CategoriesPaginatedList = PaginatedList<CategoryViewModel>.Create(categoryIndexVM.CategoryViewModels.AsQueryable(), pageNumber, 5);
            return View(categoryIndexVM);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();

        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryViewModel categoryViewModel)
        {
            _categoriesServices.save(categoryViewModel);

            return Json(true);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int materialsId)
        {
            try
            {
                _categoriesServices.delete(materialsId);
                return Json(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



    

        [HttpGet]
        public IActionResult UpdateCategory(int materialsId)
        {
            CategoryViewModel categoryViewModel = _categoriesServices.GetById(materialsId);
            return PartialView(categoryViewModel);
        }

        [HttpPost]
        public bool UpdateCategory(CategoryViewModel categoryViewModel)
        {
            try
            {
                _categoriesServices.update(categoryViewModel);
                return true;
                //return RedirectToAction("Category");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
