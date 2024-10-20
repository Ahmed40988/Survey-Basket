using Handmades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;  // Make sure this is added if you're using Newtonsoft.Json for serialization
using Handmade.Helpers;
public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") ?? new List<Cart>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int productID, Product productName, decimal price, string imageUrl, int quantity)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") ?? new List<Cart>();

        var cartItem = cart.FirstOrDefault(p => p.Product_ID == productID);
        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            cart.Add(new Cart { Product_ID = productID, Product = productName, Price = price, ImageUrl = imageUrl, Quantity = quantity });
        }

         HttpContext.Session.SetObjectAsJson("Cart", cart);
        return RedirectToAction("Index");
    }
}
