using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DXMVCWebApplication1.Common;

namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO
         )]
    public class StatusAlcanceKardexController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();
        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ActionResult Index()
        {
            return View();
        }

        #region StatusAlcanceKardexGridView???_Deprecado
        DXMVCWebApplication1.Models.DB_SENASICAEntities db1 = new DXMVCWebApplication1.Models.DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult StatusAlcanceKardexGridViewPartial()
        {
            var model = db1.StatusAlcanceKardexes;
            return PartialView("_StatusAlcanceKardexGridViewPartial", model.ToList());
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
