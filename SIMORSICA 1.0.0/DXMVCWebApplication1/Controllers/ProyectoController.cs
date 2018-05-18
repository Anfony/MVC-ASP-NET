using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class ProyectoController : Controller
    {
        //
        // GET: /Proyecto/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.Proyectos report = new DXMVCWebApplication1.Reports.Proyectos();

        public ActionResult ProyectoReport1Partial()
        {
            return PartialView("_ProyectoReport1Partial", report);
        }

        public ActionResult ProyectoReport1PartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}