using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace netshop_client.Models
{
	public class CookieMiddleware
	{
		private readonly RequestDelegate _next;

		public CookieMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
            if (context.Request.Path != "/user/login" &&
                context.Request.Path != "/user/Create" &&
                context.Request.Path != "/user/loginpost" &&
                context.Request.Cookies["netshoptoken"] == null)
			{
				context.Response.Redirect("http://localhost:5001/user/login");
				return;
			}
			await _next.Invoke(context);
		}

	}
}
    