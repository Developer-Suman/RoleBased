
using Microsoft.AspNetCore.Identity;
using RoleBased.Enum;
using System.ComponentModel.DataAnnotations;

namespace RoleBased.Models.ViewModel
{
    public class MaterialsViewModel
    {
      
        [Key]
        public int materialsId { get; set; }

        public string Username { get; set; }

        public string userId { get;set; }

        public string ShowStatus { get; set; }

        public MaterialStatus Status { get; set; }

        public int CatagoriesId { get;set; }
       
        [Required]
        public string materialName { get; set;}
        [Required]
        public int Amount { get;set; }
        public string? Image { get; set; }
        public IFormFile? File {  get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
