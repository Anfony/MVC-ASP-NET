using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DXMVCWebApplication1.Controllers
{
   [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)] 
    public class ReportsUEController : Controller
    {
        //
        // GET: /ReportsUE/
        public ActionResult Index()
        {
            return View();
        }

        DXMVCWebApplication1.Reports.ReportsUE report = new DXMVCWebApplication1.Reports.ReportsUE();

        public ActionResult ReportsUEReportPartial()
        {
            return PartialView("_ReportsUEReportPartial", report);
        }

        public ActionResult ReportsUEReportPartialExport()
        {
            return DocumentViewerExtension.ExportTo(report, Request);
        }
	}
}