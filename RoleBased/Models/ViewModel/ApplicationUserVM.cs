using RoleBased.Enum;

namespace RoleBased.Models.ViewModel
{
    public class ApplicationUserVM
    {
        public string? userId { get; set; }
        public string? Fullname { get; set; }
        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? Phone { get; set; }

        public string Latitude { set; get; }

        public string Longitude { set; get; }

        public string Email { set; get; }   

        public GenderEnum Gender_Status { get; set; }

        public IFormFile File { get; set; }
        public string? ProfilePicture { get; set; }

        public string UserRoleId { get; set; }
        public string UserRole { get; set; }
        public int countUser { get; set; }
        public int AllUser { get; set; }
        public int filterUser { get; set; }
        public int filterMechanics { get; set; }

        public int TotalMaterials { get; set; }
        public int GetSpecificUserData { get; set; }

        public int AcceptedUsers { get; set; }
        public int RejectedUsers { get; set; }
    }
}
