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

    public async Task<IActionResult> Index(int? pageNumber)
    {
        //int pageSize = 5; // Number of products per page, you can change this

        //// Get all products from the database
        //var productsQuery = _context.Products.AsQueryable();

        //// Apply null-checks for product properties
        //foreach (var product in productsQuery)
        //{
        //    product.Name = product.Name ?? "No Name Available";
        //    product.Description = product.Description ?? "No Description Available";
        //    product.ImageUrl = product.ImageUrl ?? "/images/default.jpg"; // Default image if ImageUrl is null
        //}

        //// Use the paginated list method
        ////var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, pageNumber ?? 1, pageSize);

        //return View(product);
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

}

