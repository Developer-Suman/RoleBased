using RoleBased.Enum;

namespace RoleBased.Models.ViewModel
{
    public class UserDetailsViewModel
    {
        public int userdetailsId { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public GenderEnum Gender_Status { get; set; }
    }
}
