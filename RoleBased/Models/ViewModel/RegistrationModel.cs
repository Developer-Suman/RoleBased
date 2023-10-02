using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoleBased.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RoleBased.Models.ViewModel
{
    public class RegistrationModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public string Latitude { set; get; }

        public string Longitude { set; get; }

        public int Categories_Id { get; set; }
        //public List<Categories> CategoriesModels { get; set; }

        //public CategoryViewModel CategoryViewModel { get; set; }
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
