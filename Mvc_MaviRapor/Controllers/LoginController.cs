using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_MaviRapor.Models;

namespace Mvc_MaviRapor.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            if ("admin".Equals(name) && "123".Equals(password))
            {
                Session["user"] = new User() { Login = name, Name = "Admin" };
                
            }
            return RedirectToAction("Anasayfa", "Home");
        }
    }
}