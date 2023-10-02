

using Microsoft.AspNetCore.Identity;
using RoleBased.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBased.Models.Domain
{
    public class Material
    {
        [Key]
        public int materialsId { get; set; }
        public string UserId { get; set; }
        //public IdentityUser User { get; set; }
        [Required]
        public string materialName { get; set; }
        public int CatagoriesId { get; set; }
        
        [Required]
        public int Amount { get;set; }
        [Required]
        public string Image { get; set; }

        public MaterialStatus Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set;} = DateTime.Now;
        public bool IsActive { get;set; }
    }
}
