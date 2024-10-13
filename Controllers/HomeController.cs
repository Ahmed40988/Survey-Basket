using Microsoft.AspNetCore.Mvc;
using Handmade.Models;
using System.Collections.Generic;
using System.Linq;
using Handmades.Models;
using Handmade.ViewModel;

public class HomeController : Controller
{
    private readonly DataDbContext _context;

    public HomeController(DataDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
       List<Product> Products = _context.Products.ToList();


        foreach (var product in Products)
        {
            product.Name = product.Name ?? "No Name Available";
            product.Description = product.Description ?? "No Description Available";
            product.ImageUrl = product.ImageUrl ?? "/images/default.jpg"; // صورة افتراضية إذا كانت الصورة null
        }

        return View(Products);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ID == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    public IActionResult categorie(int id) 
    {
        var categories = _context.Categories.ToList();


        List<Product> products = new List<Product>();
        if (id != null)
        {
            products = _context.Products.Where(p => p.Category_ID == id).ToList();
        }

        var model = new CategorieAndProductViewmodel
        {
            Categories = categories,
            Products = products,
            SelectedCategoryID = id
        };

        return View(model);
    }
}



