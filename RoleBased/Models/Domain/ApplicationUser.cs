

using Microsoft.AspNetCore.Identity;
using RoleBased.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBased.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {


        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? Phone { get; set; }

        public string Latitude { set; get; }

        public string Longitude { set; get; }

        public GenderEnum Gender_Status { get; set; }
        public int CategoriesId { get; set; }
        //[ForeignKey("CategoriesId")]
        //public virtual Categories CategoriesModel { get; set; }
        public string? Fullname { get; set; }
        public string? UserRoleId { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
