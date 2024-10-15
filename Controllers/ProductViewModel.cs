using Handmades.Models;

public class ProductViewModel
{
    public List<Product> Products { get; set; } = new List<Product>();
    public Product NewProduct { get; set; } = new Product();
}