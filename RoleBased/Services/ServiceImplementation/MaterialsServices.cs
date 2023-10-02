
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;

namespace RoleBased.Services.ServiceImplementation
{
    public class MaterialsServices : IMaterialServicecs
    {
        private readonly UnitOfWorkRepoImpl _unitOfWorkRepoImpl;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public object User { get; private set; }

        public MaterialsServices(UnitOfWorkRepoImpl unitOfWorkRepoImpl, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWorkRepoImpl = unitOfWorkRepoImpl;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public void delete(int id)
        {
            try
            {
                var materialsToBeDeleted = _unitOfWorkRepoImpl.Repository<Material>().getById(id);
                _unitOfWorkRepoImpl.Repository<Material>().delete(materialsToBeDeleted);
                _unitOfWorkRepoImpl.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<MaterialsViewModel> GetByUserId()
        {
            try
            {
                List<MaterialsViewModel> materialsViewModels = new List<MaterialsViewModel>();
                //var materialsModels = _unitOfWorkRepoImpl.Repository<Material>().getAll();
                var CurrentuserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var materialsToDisplay = _unitOfWorkRepoImpl.Repository<Material>().getAll().Where(x=>x.UserId == CurrentuserId);

                foreach ( var material in materialsToDisplay)
                {

                   
                    materialsViewModels.Add(new MaterialsViewModel()
                    {
                        materialsId = material.materialsId,
                        materialName = material.materialName,
                        Amount  = material.Amount,
                        Image = material.Image,
                        CreatedDate = material.CreatedDate,
                        Status = material.Status,


                    });
                }
                return materialsViewModels;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
        }

    


        public MaterialsViewModel GetById(int materialsId)
        {
            try
            {
                var materials = _unitOfWorkRepoImpl.Repository<Material>().getById(materialsId);
                MaterialsViewModel materialsViewModel = _mapper.Map<MaterialsViewModel>(materials);
                return materialsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
        }

        public void save(MaterialsViewModel materialsViewModel)
        {
            try
            {
                materialsViewModel.Image = UploadFile(materialsViewModel);
                Material material = new Material()
                {
                    Image = materialsViewModel.Image,
                    materialName = materialsViewModel.materialName,
                    materialsId = materialsViewModel.materialsId,
                    Amount = materialsViewModel.Amount,
                    CatagoriesId = materialsViewModel.CatagoriesId,
                    UserId = materialsViewModel.userId,
                    Status = materialsViewModel.Status
                    
                };
                //var departmentToBeInserted = _mapper.Map<Material>(materialsViewModel);
                _unitOfWorkRepoImpl.Repository<Material>().insert(material);
                _unitOfWorkRepoImpl.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        private string UploadFile(MaterialsViewModel materialsViewModel)
        {
            string filename = null;
            if (materialsViewModel.File != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "_" + materialsViewModel.File.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    materialsViewModel.File.CopyTo(fileStream);
                }
            }
            else
            {
                filename = materialsViewModel.Image;
            }

            return filename;
        }




        public void update(MaterialsViewModel materialsViewModel)
        {
            try
            {
                materialsViewModel.Image = UploadFile(materialsViewModel);
                var MaterialsToBeUpated = _mapper.Map<Material>(materialsViewModel);
                _unitOfWorkRepoImpl.Repository<Material>().update(MaterialsToBeUpated);
                _unitOfWorkRepoImpl.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ApplicationUser GetByUsername(string username)
        {
            try
            {
                var user = _unitOfWorkRepoImpl.Repository<ApplicationUser>().getAll().Where(x=>x.UserName == username).FirstOrDefault();
                return user;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //public List<MaterialsViewModel> GetAll()
        //{
        //    try
        //    {
        //        List<MaterialsViewModel> materialsViewModels = new List<MaterialsViewModel>();
        //        var displayAll = _unitOfWorkRepoImpl.Repository<Material>().getAll().ToList();
        //        foreach(var material in displayAll)
        //        {
        //            materialsViewModels.Add(new MaterialsViewModel()
        //            {
        //                materialsId = material.materialsId,
        //                Image = material.Image,
        //                Amount = material.Amount,
        //                CategoriesId = material.CatagoriesId,
        //                CreatedDate = material.CreatedDate,
        //                materialName = material.materialName,
        //                Status = material.Status
        //            });
                    

        //        }
        //        return materialsViewModels;

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        public List<DropDownCategoriesHelpher> GetCategoriesDropDown()
        {
            var categoryDetails = _unitOfWorkRepoImpl.Repository<Categories>().getAll();
            List<DropDownCategoriesHelpher> dropDownCategoriesHelphers = new List<DropDownCategoriesHelpher>();
            foreach(var item in  categoryDetails)
            {
                dropDownCategoriesHelphers.Add(new DropDownCategoriesHelpher()
                {
                    CategoriesId = item.CategoriesId,
                    CategoriesName = item.CategoriesName,

                });
            }
            return dropDownCategoriesHelphers;
        }

        public List<MaterialsViewModel> GetAll()
        {
            try
            {
                List<MaterialsViewModel> materialsViewModels = new List<MaterialsViewModel>();
                var displayAll = _unitOfWorkRepoImpl.Repository<Material>().getAll();
                foreach (var material in displayAll)
                {
                    materialsViewModels.Add(new MaterialsViewModel()
                    {
                        materialsId = material.materialsId,
                        Image = material.Image,
                        Amount = material.Amount,
                        CatagoriesId = material.CatagoriesId,
                        CreatedDate = material.CreatedDate,
                        materialName = material.materialName,
                        Status = material.Status
                    });


                }
                return materialsViewModels;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
