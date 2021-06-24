using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers
{
    public class ProductController : Controller
    {
        private ProductsCategoriesContext db;
        public ProductController(ProductsCategoriesContext context)
        {
            db = context;
        }

        [HttpGet("/products")]
        public IActionResult ProductsHome()
        {
            List<Product> allProducts = db.Products.ToList();
            ViewBag.AllProducts = allProducts;

            return View("HomeProduct");
        }

        [HttpPost("/products/add")]
        public IActionResult AddProduct(Product newProduct)
        {
            if(ModelState.IsValid == false)
            {
                return ProductsHome();
            }

            db.Products.Add(newProduct);
            db.SaveChanges();

            return ProductsHome();
        }

        [HttpGet("/products/{productId}")]
        public IActionResult ProductDetails(int productId)
        {
            Product product = db.Products
                .Include(p => p.Associations)
                .ThenInclude(a => a.Category)
                .FirstOrDefault(p => p.ProductId == productId);

            if(product == null)
            {
                return ProductsHome();
            }

            List<Category> unusedCategories = db.Categories
                .Include(c => c.Associations)
                .Where(c => c.Associations
                .Any(a => a.ProductId != product.ProductId)|| c.Associations.Count == 0)
                .ToList();
            ViewBag.UnusedCategories = unusedCategories;

            return View("DetailProduct", product);
        }

        [HttpPost("/products/addCat")]
        public IActionResult AddCategory(Association assoc)
        {
            
            db.Associations.Add(assoc);
            db.SaveChanges();

            return Redirect($"{assoc.ProductId}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}