using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;


namespace senasica.Controllers
{
    public class inicioController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        public ActionResult Index()
        {
            var productors = db.Productors.Include(p => p.Estado).Include(p => p.Municipio);
            return View(productors.ToList());
        }
    }
}