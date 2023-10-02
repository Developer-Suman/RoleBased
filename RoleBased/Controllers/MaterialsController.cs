using Algorithm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoleBased.Helpher;
using RoleBased.Models.Domain;
using RoleBased.Models.ViewModel;
using RoleBased.Repository.RepoImplementation;
using RoleBased.Services.ServiceInterface;
using System.Security.Claims;


namespace RoleBased.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialServicecs _materialsServices;
        private readonly UnitOfWorkRepoImpl _unitOfWorkRepo;
        private readonly ICategoriesServices _categoriesServices;

        public MaterialsController(UnitOfWorkRepoImpl unitOfWorkRepoImpl, ICategoriesServices categoriesServices, IMaterialServicecs materialServicecs)
        {
            _unitOfWorkRepo = unitOfWorkRepoImpl;
            _materialsServices = materialServicecs;
            _categoriesServices = categoriesServices;

        }
        public IActionResult Index()
        {
            //var username = HttpContext.Session.GetString("Username");
            //var password = HttpContext.Session.GetString("Password");
            //var userId = HttpContext.Session.GetString("USERID");
            var userName = HttpContext.Session.GetString("USERNAME");
            return View();
        }



        [HttpGet]
        public PartialViewResult DetailsPage(int Id)
        {
            var model = _unitOfWorkRepo.Repository<Material>().getById(Id);

            MaterialsViewModel materialsViewModel = new MaterialsViewModel
            {
                materialsId = model.materialsId,
                materialName = model.materialName,
                Amount = model.Amount,
                CreatedDate = model.CreatedDate,
                Image = model.Image,

            };

            MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM
            {
                MaterialsViewModel = materialsViewModel,

            };




            //return PartialView("~/Views/Shared/_DetailsPage.cshtml", materialsIndexVM);
            return PartialView("ShowMaterials", materialsIndexVM);
        }

        [HttpGet]
        public ActionResult ShowMaterials(string status, int materialsId, int pageNumber = 1)
        {


            MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();

            MaterialsViewModel materialsViewModel = new MaterialsViewModel();

            materialsIndexVM.MaterialsViewModel = materialsViewModel;


            List<Material> AllMaterials = _unitOfWorkRepo.Repository<Material>().getAll().ToList();

            Similarities algo = new Similarities(AllMaterials);
            List<int> MaterialsId = algo.GetSimilarProducts(materialsId);


            List<Material> recommendProducts = AllMaterials.Where(p => MaterialsId.Contains(p.materialsId)).Cast<Material>().ToList();





            materialsIndexVM.MaterialsViewModels = _materialsServices.GetAll();
            //materialsIndexVM.MaterialsViewModel = new MaterialsViewModel();
            materialsIndexVM.MaterialspaginatedList = PaginatedList<MaterialsViewModel>.Create(materialsIndexVM.MaterialsViewModels.AsQueryable(), pageNumber, 5);
            materialsIndexVM.RecommendProduct = recommendProducts;
            materialsIndexVM.UseStatus = status;
            return View(materialsIndexVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            MaterialsViewModel materialsViewModel = new MaterialsViewModel();
            materialsViewModel.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();
            materialsIndexVM.MaterialsViewModel = materialsViewModel;

            return View(materialsIndexVM);
        }
        [HttpPost]
        public IActionResult Create(MaterialsIndexVM materialsIndexVM)
        {
            try
            {

                _materialsServices.save(materialsIndexVM.MaterialsViewModel);
                return Json(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [HttpGet]
        public IActionResult Materials(int pageNumber = 1)
        {
            List<DropDownCategoriesHelpher> dropDownCategoriesHelphers = new List<DropDownCategoriesHelpher>();
            dropDownCategoriesHelphers = _materialsServices.GetCategoriesDropDown();
            ViewBag.CategoryDropDown = new SelectList(dropDownCategoriesHelphers, "CategoriesId", "CategoriesName");



            MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();

            MaterialsViewModel materialsViewModel = new MaterialsViewModel();
            materialsViewModel.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();
            materialsIndexVM.MaterialsViewModel = materialsViewModel;


            materialsIndexVM.MaterialsViewModels = _materialsServices.GetByUserId();
            //materialsIndexVM.MaterialsViewModel = new MaterialsViewModel();
            materialsIndexVM.MaterialspaginatedList = PaginatedList<MaterialsViewModel>.Create(materialsIndexVM.MaterialsViewModels.AsQueryable(), pageNumber, 5);
            return View(materialsIndexVM);
        }

        [HttpPost]
        public IActionResult DeleteMaterials(int materialsId)
        {
            try
            {
                _materialsServices.delete(materialsId);
                return Json(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(false);
            }
        }




        [HttpGet]
        public IActionResult MaterialsUpdate(int materialsId)
        {
            MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();
            materialsIndexVM.MaterialsViewModel = _materialsServices.GetById(materialsId);
            //materialsViewModel = _materialsServices.GetById(materialsId);
            return PartialView(materialsIndexVM);

        }

        [HttpPost]
        public IActionResult MaterialsUpdate(MaterialsViewModel materialsViewModel)
        {
            try
            {
                _materialsServices.update(materialsViewModel);
                TempData["SuccessMessage"] = "Materials Update Success";
                return RedirectToAction("Materials");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
        }


        [HttpGet]
        public IActionResult GetMaterialsDetails(int materialsId)
        {

            try
            {
                MaterialsIndexVM materialsIndexVM = new MaterialsIndexVM();
                materialsIndexVM.MaterialsViewModel = new MaterialsViewModel();

                var materialsDetails = _unitOfWorkRepo.Repository<Material>().getById(materialsId);
                var userDetails = _unitOfWorkRepo.Repository<ApplicationUser>().getByString(materialsDetails.UserId);
                materialsIndexVM.MaterialsViewModel = _materialsServices.GetById(materialsId);
                materialsIndexVM.MaterialsViewModel.Username = userDetails.UserName;
                materialsIndexVM.MaterialsViewModel.IsActive = materialsDetails.IsActive;


                List<Material> AllMaterials = _unitOfWorkRepo.Repository<Material>().getAll().ToList();

                Similarities algo = new Similarities(AllMaterials);
                List<int> MaterialsId = algo.GetSimilarProducts(materialsId);


                List<Material> recommendProducts = AllMaterials.Where(p => MaterialsId.Contains(p.materialsId)).Cast<Material>().ToList();

                string name = "Suman";

                materialsIndexVM.RecommendProduct = recommendProducts;
                materialsIndexVM.UseStatus = name;



                return PartialView(materialsIndexVM);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public bool GetMaterialsDetails(MaterialsIndexVM materialsIndexVM)
        {
            try
            {
                var materials = _unitOfWorkRepo.Repository<Material>().getById(materialsIndexVM.MaterialsViewModel.materialsId);
                materials.Status = materialsIndexVM.MaterialsViewModel.Status;








                _unitOfWorkRepo.Repository<Material>().update(materials);



                _unitOfWorkRepo.Commit();

                MaterialsIndexVM materialsIndexVM1 = new MaterialsIndexVM();
                //materialsIndexVM1.RecommendProduct = recommendProducts;
                materialsIndexVM1.UseStatus = "Approve";

                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //[HttpPost]
        //public bool GwtMaterialsDetails(int materialsId)
        //{
        //    try
        //    {
        //        var materials = _unitOfWorkRepo.Repository<Material>().getById(materialsId);

        //        if(materials.IsActive)
        //        {
        //            materials.IsActive = false;
        //        }
        //        else
        //        {
        //            materials.IsActive = true;
        //        }

        //        _unitOfWorkRepo.Repository<Material>().update(materials);
        //        //TempData["key"] = materials.IsActive ? "Accept" : "Reject";

        //        _unitOfWorkRepo.Commit();
        //        return true;

        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}


    }
}
