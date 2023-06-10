using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class WebScrapingController : Controller
    {
        // GET: WebScraping
        public ActionResult Maletas()
        {
            ML.Result result = BL.WebScraping.Metodo();
            ML.Producto producto = new ML.Producto();
            if (result.Correct)
            {
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                return View(producto);
            }
        }
    }
}