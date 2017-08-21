using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netshop_client.Models;

namespace netshop_client.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
			if (Request.Cookies["netshoptoken"] == null)
			{
				ViewData["isLogged"] = false;
			}
			else
			{
				ViewData["isLogged"] = true;
			}

			if (Request.Cookies["netshopIsAdmin"] == null || Request.Cookies["netshopIsAdmin"] == "False")
			{
				ViewData["isAdmin"] = false;
			}
			else
			{
				ViewData["isAdmin"] = true;
			}

            var productService = new ProductService();
            var categorySerivce = new CategoryService();

            List<Product> products = productService.GetAll().OrderBy(o=>o.CategoryId).ToList();
            List<Category> categories = categorySerivce.GetAll().OrderBy(o=>o.CategoryId).ToList();
            ViewData["categories"] = categories;
            ViewData["products"] = products;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
