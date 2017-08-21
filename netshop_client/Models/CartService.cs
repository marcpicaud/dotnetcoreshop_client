using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using netshop_client.Models;
using Newtonsoft.Json;

namespace netshop_client.Models
{
    public class CartService
    {
        HttpClient _client = new HttpClient();
        string BaseUri = "http://localhost:5000/api/cart/";

        // Get cart by ID
		public Cart Get(int id)
		{
            var res = _client.GetAsync(BaseUri+id).Result;
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Cart>(res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
		}

        // Get all carts
        public List<Cart> GetAll()
        {
            var res = _client.GetAsync(BaseUri).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<Cart>>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
            
        }

        // POST a new cart
        public bool Create(Cart cart)
        {
            var res = _client.PostAsync(BaseUri, new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json")).Result;
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		// PUT a cart
		public bool Edit(Cart cart)
		{
            var res = _client.PutAsync(BaseUri+cart.CartId, new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json")).Result;
			if (res.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        // DELETE a cart
        public bool Delete(int id)
        {
            var res = _client.DeleteAsync(BaseUri + id).Result;
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
