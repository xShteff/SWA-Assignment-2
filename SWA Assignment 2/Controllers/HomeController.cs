using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SWA_Assignment_2.Users;

namespace SWA_Assignment_2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult GetJson()
        {
            return Json(new Person{Name = new Name{ FirstName = "Nico", LastName = "Sandival"}});
        }
    }
}
