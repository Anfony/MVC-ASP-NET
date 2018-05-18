using DXMVCWebApplication1.Common;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.IE_INOCUIDAD
                     + "," + RolesUsuarios.IE_MOVILIZACION
                     + "," + RolesUsuarios.IE_ANIMAL
                     + "," + RolesUsuarios.IE_ACUICOLA_PESQUERA
                     + "," + RolesUsuarios.IE_VEGETAL
                     + "," + RolesUsuarios.SUP_ESTATAL
                     + "," + RolesUsuarios.IE_PROGRAMAS_U
   )]
    public class SURIController : Controller
    {
        // GET: SURI
        public ActionResult Index()
        {
            return View();
        }
    }
}