namespace RoleBased.Models.Domain
{
    public class Categories
    {
        public int CategoriesId { get;set; }
        public string CategoriesName { get;set;}
        public string userId { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
