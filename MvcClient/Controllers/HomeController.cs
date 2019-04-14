using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Newtonsoft.Json.Linq;

namespace MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult>  GetApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token"); //混合模式

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);

                var content = await client.GetStringAsync("http://localhost:5001/api/values");

                ViewBag.Json = JArray.Parse(content).ToString();

                return View("json");
            }
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");//这将清除本地cookie，然后重定向到IdentityServer。IdentityServer将清除其cookie，然后为用户提供返回MVC应用程序的链接
        }
    }
}