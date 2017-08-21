using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using netshop_client.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netshop_client.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _service;

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
		public UserController(UserService userService)
        {
            _service = userService;
        }

        // INDEX PAGE
        public IActionResult Index()
        {
            setIsLogged();
            return View(_service.GetAll());
        }

		// LOGIN PAGE
		public IActionResult Login()
		{
            setIsLogged();
            return View();
		}

        // LOGIN ACTION
        public IActionResult LoginPost(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var res = _service.Login(user);
            if (res == null)
            {
                return NotFound();
            }
			CookieOptions options = new CookieOptions();
			options.Expires = DateTime.Now.AddDays(42);
            Response.Cookies.Append("netshoptoken", res.Token, options);
            Response.Cookies.Append("netshopuserId", res.UserId.ToString(), options);
            Response.Cookies.Append("netshopIsAdmin", res.Admin.ToString(), options);
            return RedirectToAction("Index");
        }

        //LOGOUT
        public IActionResult Logout()
        {
            Response.Cookies.Delete("netshoptoken");
            Response.Cookies.Delete("netshopIsAdmin");
            Response.Cookies.Delete("netshopuserId");
            return RedirectToAction("Index");
        }

        // CREATE FORM
        public IActionResult Create()
        {
            setIsLogged();
            return View();
        }

		// CREATE FORM
        [HttpPost]
		public IActionResult Create(int id, User user)
		{
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                if (_service.Create(user) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return BadRequest();
                }
            }
		}

        // DELETE user
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
            var user = _service.Get(id);
            if (user == null)
            {
                return NotFound();
            }
			return View(user);
		}


		// EDIT FORM
		[HttpPost]
		public IActionResult EditPost(int id, User user)
		{
			if (user == null)
			{
				return BadRequest();
			}
			else
			{
                if (_service.Edit(user) == true)
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
