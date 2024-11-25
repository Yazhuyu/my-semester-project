using Microsoft.AspNetCore.Mvc;
using NewTeam6.Models;
using System.Diagnostics;

namespace NewTeam6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ��ܭ���
        public IActionResult Index()
        {
            return View();
        }

        // ��ܵ��U����
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult FrontPage()
        {
            return View();
        }
        public IActionResult Checklist()
        {
            return View("/Views/FrontPage/Checklist.cshtml");
        }
        public IActionResult Remind()
        {
            return View("/Views/FrontPage/Remind.cshtml");
        }
        public IActionResult Personal()
        {
            return View("/Views/FrontPage/Personal.cshtml");
        }
    }
}
