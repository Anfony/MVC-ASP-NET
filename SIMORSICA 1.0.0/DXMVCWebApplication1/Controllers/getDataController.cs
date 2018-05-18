using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.ViewsModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using System.Collections.Generic;

namespace DXMVCWebApplication1.Controllers
{
    public class getDataController : Controller
    {

        // GET: getData
        public ActionResult Index()
        {
            return View();
        }

        public string getEstadoNombreSpatial(double lng, double lat)
        {
            return DXMVCWebApplication1.Models.Senasica.getEstadoNombreSpatial(lng, lat);
        }
        public string getMunicipioNombreSpatial(double lng, double lat)
        {
            return DXMVCWebApplication1.Models.Senasica.getMunicipioNombreSpatial(lng, lat);
        }
        public string getMunicipioSpatial(double lng, double lat)
        {
            var datos = DXMVCWebApplication1.Models.Senasica.getMunicipioSpatial(lng, lat);
            return datos;
        }

        public int esperarJavaScript()
        {
            return 0;
        }

    }
}