//using AutoMapper;
//using RoleBased.Models.Domain;
//using RoleBased.Models.ViewModel;
//using RoleBased.Repository.RepoImplementation;
//using RoleBased.Services.ServiceInterface;

//namespace RoleBased.Services.ServiceImplementation
//{
    

//    public class UserDetailsServices : IUserDetailsServices
//    {
//        private readonly UnitOfWorkRepoImpl _UnitOfWorkRepoImpl;
//        private readonly IMapper _mapper;

//        public UserDetailsServices(UnitOfWorkRepoImpl unitOfWorkRepoImpl, IMapper mapper)
//        {
//            _UnitOfWorkRepoImpl = unitOfWorkRepoImpl;
//            _mapper = mapper;
            
//        }
//        public void delete(int id)
//        {
//            try
//            {
//                var userDetailsToBeDelated = _UnitOfWorkRepoImpl.Repository<UserDetails>().getById(id);
//                _UnitOfWorkRepoImpl.Repository<UserDetails>().delete(userDetailsToBeDelated);
//                _UnitOfWorkRepoImpl.Commit();


//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw;
//            }
//        }

//        public List<UserDetailsViewModel> GetAll()
//        {
//            try
//            {
//                List<UserDetailsViewModel> userDetailsViewModels = new List<UserDetailsViewModel>();
//                var userDetails = _UnitOfWorkRepoImpl.Repository<UserDetailsViewModel>().getAll();
//                foreach(var item in userDetails)
//                {
//                    userDetailsViewModels.Add(new UserDetailsViewModel()
//                    {
//                        userdetailsId = item.userdetailsId,
//                        Address = item.Address,
//                        Phone = item.Phone,
//                        Fullname = item.Fullname,
//                        Gender_Status = item.Gender_Status,
//                        Latitude = item.Latitude,
//                        Longitude = item.Longitude,

//                    });
//                }
//                return userDetailsViewModels;

//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw;
//            }
//        }

//        public UserDetailsViewModel GetById(int userdetailsId)
//        {
//            try
//            {
//                var userdetails = _UnitOfWorkRepoImpl.Repository<UserDetails>().getById(userdetailsId);
//                UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel();
//                userDetailsViewModel.userdetailsId = userdetailsId;
//                userDetailsViewModel.Address = userdetails.Address;
//                userDetailsViewModel.Phone = userdetails.Phone;
//                userDetailsViewModel.Gender_Status = userdetails.Gender_Status;
//                userDetailsViewModel.Latitude = userdetails.Latitude;
//                userDetailsViewModel.Longitude = userdetails.Longitude;

//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw;
//            }
//        }

//        public ApplicationUser GetByUsername(string username)
//        {
//            throw new NotImplementedException();
//        }

//        public void save(UserDetailsViewModel userDetailsViewModel)
//        {
//            throw new NotImplementedException();
//        }

//        public void update(UserDetailsViewModel userDetailsViewModel)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
