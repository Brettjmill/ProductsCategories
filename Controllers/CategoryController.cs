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
    public class CategoryController : Controller
    {
        private ProductsCategoriesContext db;
        public CategoryController(ProductsCategoriesContext context)
        {
            db = context;
        }

        [HttpGet("/categories")]
        public IActionResult CategoriesHome()
        {
            List<Category> allCategories = db.Categories.ToList();
            ViewBag.AllCategories = allCategories;

            return View("HomeCategory");
        }

        [HttpPost("/categories/add")]
        public IActionResult AddCategory(Category newCategory)
        {
            if(ModelState.IsValid == false)
            {
                return CategoriesHome();
            }

            db.Categories.Add(newCategory);
            db.SaveChanges();

            return CategoriesHome();
        }

        [HttpGet("/categories/{categoryId}")]
        public IActionResult CategoryDetails(int categoryId)
        {
            Category category = db.Categories
                .Include(c => c.Associations)
                .ThenInclude(a => a.Product)
                .FirstOrDefault(c => c.CategoryId == categoryId);

            if(category == null)
            {
                return CategoriesHome();
            }

            List<Product> unusedProducts = db.Products
                .Include(c => c.Associations)
                .Where(c => c.Associations
                .Any(p => p.CategoryId != category.CategoryId)|| c.Associations.Count == 0)
                .ToList();
            ViewBag.UnusedProducts = unusedProducts;

            return View("DetailCategory", category);
        }

        [HttpPost("/categories/addProd")]
        public IActionResult AddProduct(Association assoc)
        {
            
            db.Associations.Add(assoc);
            db.SaveChanges();

            return Redirect($"{assoc.CategoryId}");
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