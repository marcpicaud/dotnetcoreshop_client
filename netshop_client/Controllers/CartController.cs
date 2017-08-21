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
	public class CartController : Controller
	{
		private readonly CartService _service;

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

        // Delete an item in a cart
        [HttpGet("/Cart/{idCart}/Remove/{idProduct}")]
        public IActionResult RemoveProduct(int idCart, int idProduct)
        {
            Cart cart = _service.Get(idCart);
            if (cart == null)
            {
                return NotFound();
            }

            var cartProducts = cart.ProductIds.Split(',').ToList();
            cartProducts.Remove(idProduct.ToString());
            if (cartProducts.Count() > 0)
            {
                cart.ProductIds = string.Join(",", cartProducts);   
            }
            else
            {
                cart.ProductIds = "";
            }

            if (_service.Edit(cart) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        // Add an item in a cart
		[HttpGet("/Cart/Add/{idProduct}")]
		public IActionResult AddProduct(int idProduct)
		{
            var requester = int.Parse(Request.Cookies["netshopuserId"]);
			
            // Init data
			var carts = _service.GetAll();
			var myCart = carts.Find((obj) => obj.UserId == requester);
			
            // if user has no cart, create it
			if (myCart == null)
			{
				_service.Create(new Cart { UserId = requester, ProductIds = "" });
				// Refresh data
				carts = _service.GetAll();
				myCart = carts.Find((obj) => obj.UserId == requester);
				if (myCart == null)
				{
					return NotFound();
				}
			}

			var cartProducts = myCart.ProductIds.Split(',').ToList();
            cartProducts.Add(idProduct.ToString());
			myCart.ProductIds = string.Join(",", cartProducts);
			if (_service.Edit(myCart) == true)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return NotFound();
			}
		}


		// CONSTRUCTOR
		public CartController(CartService cartService)
		{
			_service = cartService;
		}

		// INDEX PAGE
		public IActionResult Index()
		{
			setIsLogged();

            // User ID
            var requester = int.Parse(Request.Cookies["netshopuserId"]);

            // Init data
            var carts = _service.GetAll();
            var myCart = carts.Find((obj) => obj.UserId == requester);
            var products = new ProductService().GetAll();

            // if user has no cart, create it
            if (myCart == null) {
                _service.Create(new Cart{UserId=requester, ProductIds=""});
				// Refresh data
				carts = _service.GetAll();
				myCart = carts.Find((obj) => obj.UserId == requester);
				if (myCart == null)
				{
					return NotFound();
				}
            }

            var cartProducts = myCart.ProductIds.Split(',');
            ICollection<string> cartProductsName = new List<string>();
            double total = 0;
            for (int i = 0; i < cartProducts.Length; i++)
            {
                var tmp = products.Find((obj) => obj.ProductId == int.Parse(cartProducts[i]));
                cartProductsName.Add(tmp.ProductId +"-"+tmp.Name);
                total += tmp.Price;
            }
            ViewData["CartProducts"] = cartProductsName;
            ViewData["Total"] = total;

            return View(myCart);
		}

		// CREATE FORM
		[HttpPost]
		public IActionResult Create(int id, Cart cart)
		{
			if (cart == null)
			{
				return BadRequest();
			}
			else
			{
				if (_service.Create(cart) == true)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return BadRequest();
				}
			}
		}

		// DELETE cart
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


		// EDIT FORM (POST version)
		[HttpPost]
		public IActionResult EditPost(int id, Cart cart)
		{
			if (cart == null)
			{
				return BadRequest();
			}
			else
			{
				if (_service.Edit(cart) == true)
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
