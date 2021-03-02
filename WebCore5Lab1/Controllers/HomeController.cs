using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebCore5Lab1.ActionFilters;
using WebCore5Lab1.Models;

namespace WebCore5Lab1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        //[AllowAnonymous]
        [ServiceFilter(typeof(RequiredClaimFilter))]
        public IActionResult Index()
        {
            TempData["Test"] = "這是一個測試的 TempData 資料";

            return View();
        }

        [ServiceFilter(typeof(RequiredClaimFilter))]
        [HttpPost]
        public IActionResult Index(IFormCollection forms)
        {
            var form = forms;

            ViewBag.UserID = form["txtUserID"];
            ViewBag.MyTitle = form["txtTitle"];

            return View();
        }

        public IActionResult Privacy()
        {
            return Unauthorized("401 未授權！請確認您的帳號是否有檢視此資源的權限。");
        }

        
        public IActionResult About()
        {
            return View();
            /*
            if(User.Identity.Name == "gelis")
            {
                return View();
            }
            else
            {
                return Unauthorized("401 未授權！請確認您的帳號是否有檢視此資源的權限。");
            }
            */
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UnauthorizePage()
        {
            return View();
        }

        public IActionResult ResultForbid()
        {
            return Forbid();
        }
    }
}
