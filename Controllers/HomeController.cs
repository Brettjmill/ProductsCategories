using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {
        private ProductsCategoriesContext db;
        public HomeController(ProductsCategoriesContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("ProductsHome", "Product");
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
