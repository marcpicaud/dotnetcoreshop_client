using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using netshop_client.Models;
using Newtonsoft.Json;

namespace netshop_client.Models
{
    public class UserService
    {
        HttpClient _client = new HttpClient();
        string BaseUri = "http://localhost:5000/api/user/";

        // Get user by ID
		public User Get(int id)
		{
            var res = _client.GetAsync(BaseUri+id).Result;
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<User>(res.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return null;
            }
		}

        public User Login(User user)
        {
            var res = _client.PostAsync(BaseUri+"login", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<User>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
            
        }

        public User getByToken(string token)
        {
			var res = _client.GetAsync(BaseUri+token).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<User>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
        }

        // Get all users
        public List<User> GetAll()
        {
            var res = _client.GetAsync(BaseUri).Result;
			if (res.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<User>>(res.Content.ReadAsStringAsync().Result);
			}
			else
			{
				return null;
			}
            
        }

        // POST a new user
        public bool Create(User user)
        {
            var res = _client.PostAsync(BaseUri, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
            if (res.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		// PUT a new user
		public bool Edit(User user)
		{
            var res = _client.PutAsync(BaseUri+user.UserId, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")).Result;
			if (res.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        // DELETE a user
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
