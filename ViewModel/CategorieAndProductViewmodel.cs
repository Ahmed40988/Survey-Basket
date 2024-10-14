using Handmades.Models;
namespace Handmade.ViewModel
{
    public class CategorieAndProductViewmodel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? SelectedCategoryID { get; set; }
    }
}
