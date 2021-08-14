using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PoolStudio.WEB.Controllers
{
    public class UsuariosController : Controller
    {
        //[HttpGet]
        public IActionResult Index(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
