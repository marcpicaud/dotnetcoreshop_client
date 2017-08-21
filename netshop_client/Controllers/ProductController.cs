using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using netshop_client.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netshop_client.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _service;

        private void setIsLogged()
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
        }

        // CONSTRUCTOR
		public ProductController(ProductService productService)
        {
            _service = productService;
        }

        // INDEX PAGE
        public IActionResult Index()
        {
            setIsLogged();
            return View(_service.GetAll());
        }

        // CREATE FORM
        public IActionResult Create()
        {
            setIsLogged();
            return View();
        }

		// CREATE FORM
        [HttpPost]
		public IActionResult Create(int id, Product product)
		{
            if (product == null)
            {
                return BadRequest();
            }
            else
            {
                if (_service.Create(product) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }
		}

        // DELETE product
        public IActionResult Delete(int id)
        {
            if (_service.Delete(id) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

		// EDIT FORM
        [HttpGet]
		public IActionResult Edit(int id)
		{
            setIsLogged();
            var product = _service.Get(id);
            if (product == null)
            {
                return NotFound();
            }
			return View(product);
		}


		// EDIT FORM
		[HttpPost]
		public IActionResult EditPost(Product product)
		{
			if (product == null)
			{
				return BadRequest();
			}
			else
			{
                if (_service.Edit(product) == true)
				{
					return RedirectToAction("Index");
				}
				else
				{
                    return NotFound();
				}
			}
		}

    }
}
