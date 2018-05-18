using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class RepActividadesController : Controller
    {
        //
        // GET: /RepActividades/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.RepActividades report = new DXMVCWebApplication1.Reports.RepActividades();

        public ActionResult RepActividadesReportPartial()
        {
            return PartialView("_RepActividadesReportPartial", report);
        }

        public ActionResult RepActividadesReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}