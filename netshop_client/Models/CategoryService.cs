using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using netshop_client.Models;
using Newtonsoft.Json;

namespace netshop_client.Models
{
    public class CategoryService
    {
        HttpClient _client = new HttpClient();
        string BaseUri = "http://localhost:5000/api/category/";

        // Get category by ID
		public Category Get(int id)
		{
            var res = _client.GetAsync(BaseUri+id).Result;
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Category>(res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
		}

        // Get all categories
        public List<Category> GetAll()
        {
            var res = _client.GetAsync(BaseUri).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<Category>>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
            
        }

        // POST a new category
        public bool Create(Category category)
        {
            var res = _client.PostAsync(BaseUri, new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json")).Result;
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		// PUT a category
		public bool Edit(Category category)
		{
            var res = _client.PutAsync(BaseUri+category.CategoryId, new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json")).Result;
			if (res.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        // DELETE a category
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
