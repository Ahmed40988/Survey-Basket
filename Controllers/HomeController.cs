using Microsoft.AspNetCore.Mvc;
using Handmade.Models; 
using System.Collections.Generic;
using System.Linq;
using Handmades.Models;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        var products = GetProducts(); 
        return View(products);
    }

    
    public IActionResult Details(int id)
    {
        var product = GetProducts().FirstOrDefault(p => p.Id == id); 
        return View(product);
    }

    // This method simulates a database of products
    private List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.00m, ImageUrl = "/images/15629f7d7e227.jpg" },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 15.00m, ImageUrl = "/images/product2.jpg" },
            new Product { Id = 3, Name = "Product 3", Description = "Description 3", Price = 20.00m, ImageUrl = "/images/product3.jpg" },
            new Product { Id = 4, Name = "Product 4", Description = "Description 4", Price = 25.00m, ImageUrl = "/images/product4.jpg" },
            new Product { Id = 5, Name = "Product 5", Description = "Description 5", Price = 30.00m, ImageUrl = "/images/product5.jpg" },
            new Product { Id = 6, Name = "Product 6", Description = "Description 6", Price = 35.00m, ImageUrl = "/images/product6.jpg" },
            new Product { Id = 7, Name = "Product 7", Description = "Description 7", Price = 40.00m, ImageUrl = "/images/product7.jpg" },
            new Product { Id = 8, Name = "Product 8", Description = "Description 8", Price = 45.00m, ImageUrl = "/images/product8.jpg" },
            new Product { Id = 9, Name = "Product 9", Description = "Description 9", Price = 50.00m, ImageUrl = "/images/product9.jpg" },
            new Product { Id = 10, Name = "Product 10", Description = "Description 10", Price = 55.00m, ImageUrl = "/images/product10.jpg" },
        };
    }
}

