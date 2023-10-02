using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;

namespace RoleBased.Services.ServiceImplementation
{
    public class CategoriesServicesImpl : ICategoriesServices
    {

        private readonly UnitOfWorkRepoImpl _unitofwork;

        public CategoriesServicesImpl(UnitOfWorkRepoImpl unitOfWorkRepoImpl)
        {
            _unitofwork = unitOfWorkRepoImpl;
            
        }
        public void delete(int id)
        {
            try
            {
                var categoriesToBeDeleted = _unitofwork.Repository<Categories>().getById(id);
                _unitofwork.Repository<Categories>().delete(categoriesToBeDeleted);
                _unitofwork.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<CategoryViewModel> GetAll()
        {
            try
            {
                List<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
                var displayAll = _unitofwork.Repository<Categories>().getAll();
                foreach (var category in displayAll)
                {
                    categoryViewModels.Add(new CategoryViewModel()
                    {
                        CategoriesId = category.CategoriesId,
                        CategoriesName = category.CategoriesName,
                        CreatedDate = category.CreatedDate,
                    });
                }
                return categoryViewModels;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public CategoryViewModel GetById(int id)
        {
            try
            {
                var categories = _unitofwork.Repository<Categories>().getById(id);
                CategoryViewModel categoryViewModel = new CategoryViewModel();

                categoryViewModel.CategoriesId = categories.CategoriesId;
                categoryViewModel.CategoriesName = categories.CategoriesName;

                return categoryViewModel;

            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ApplicationUser GetByUsername(string username)
        {
            try
            {
                var user = _unitofwork.Repository<ApplicationUser>().getAll().Where(x=>x.UserName == username).FirstOrDefault();
                return user;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void save(CategoryViewModel categoryViewModel)
        {
            try
            {
                Categories categories = new Categories()
                {
                    CategoriesId = categoryViewModel.CategoriesId,
                    CategoriesName = categoryViewModel.CategoriesName,
                    userId = categoryViewModel.userId,
                    CreatedDate = categoryViewModel.CreatedDate,
                };

                _unitofwork.Repository<Categories>().insert(categories);
                _unitofwork.Commit();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void update(CategoryViewModel categoryViewModel)
        {
            try
            {
                var materialsToBeUpdated = _unitofwork.Repository<Categories>().getById(categoryViewModel.CategoriesId);
                materialsToBeUpdated.CategoriesName = categoryViewModel.CategoriesName;
                //materialsToBeUpdated.CategoriesId = categoryViewModel.CategoriesId;

                _unitofwork.Repository<Categories>().update(materialsToBeUpdated);
                _unitofwork.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
