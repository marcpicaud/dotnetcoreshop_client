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
    public class CategoryController : Controller
    {
        private readonly CategoryService _service;

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
		public CategoryController(CategoryService categoryService)
        {
            _service = categoryService;
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
		public IActionResult Create(int id, Category category)
		{
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
                if (_service.Create(category) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }
		}

        // DELETE category
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
            var category = _service.Get(id);
            if (category == null)
            {
                return NotFound();
            }
			return View(category);
		}


        // EDIT FORM (POST version)
		[HttpPost]
		public IActionResult EditPost(int id, Category category)
		{
			if (category == null)
			{
				return BadRequest();
			}
			else
			{
                if (_service.Edit(category) == true)
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
