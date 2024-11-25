using Microsoft.AspNetCore.Mvc;

namespace NewTeam6.Controllers
{
    public class PersonalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
