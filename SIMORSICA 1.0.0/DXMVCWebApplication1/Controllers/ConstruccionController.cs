using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;

namespace DXMVCWebApplication1.Controllers
{
   [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
                         + "," + RolesUsuarios.IE_INOCUIDAD
                         + "," + RolesUsuarios.IE_MOVILIZACION
                         + "," + RolesUsuarios.IE_ANIMAL
                         + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.IE_VEGETAL
                         + "," + RolesUsuarios.UR_INOCUIDAD
                         + "," + RolesUsuarios.UR_MOVILIZACION
                         + "," + RolesUsuarios.UR_ANIMAL
                         + "," + RolesUsuarios.UR_ACUICOLA_PESQUERA
                         + "," + RolesUsuarios.UR_VEGETAL
        )] 
    public class ConstruccionController : Controller
    {
        //
        // GET: /Construccion/
        public ActionResult Index()
        {
            return View();
        }
	}
}