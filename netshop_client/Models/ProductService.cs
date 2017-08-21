using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using netshop_client.Models;
using Newtonsoft.Json;

namespace netshop_client.Models
{
    public class ProductService
    {
        HttpClient _client = new HttpClient();
        string BaseUri = "http://localhost:5000/api/product/";

        // Get product by ID
		public Product Get(int id)
		{
            var res = _client.GetAsync(BaseUri+id).Result;
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Product>(res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
		}

        // Get all products
        public List<Product> GetAll()
        {
            var res = _client.GetAsync(BaseUri).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<Product>>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
            
        }

        // POST a new product
        public bool Create(Product product)
        {
            var res = _client.PostAsync(BaseUri, new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json")).Result;
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		// PUT a product
		public bool Edit (Product product)
		{
            var res = _client.PutAsync(BaseUri+product.ProductId, new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json")).Result;
			if (res.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
                System.Diagnostics.Debug.WriteLine(res.StatusCode);

				return false;
			}
		}

        // DELETE a product
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
