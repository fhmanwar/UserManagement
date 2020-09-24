using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.IsAvailable)
            {
                if (HttpContext.Session.GetString("lvl") == "Admin")
                {
                    return View();
                }
                //else if (HttpContext.Session.GetString("lvl") == "Trainer")
                //{
                //    return Redirect("/trainer");
                //}
                //else
                //{
                //    return Redirect("/employee");
                //}
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
