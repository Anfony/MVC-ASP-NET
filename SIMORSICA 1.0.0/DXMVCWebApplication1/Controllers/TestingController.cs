using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;


namespace DXMVCWebApplication1.Controllers
{
    [Authorize(Roles = RolesUsuarios.SUPERUSUARIO)]
    public class TestingController : Controller
    {
        //
        // GET: /Testing/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult VigenciaFileManagerPartial()
        {
            return PartialView("_VigenciaFileManagerPartial", TestingControllerControllerVigenciaFileManagerSettings.Model);
        }

        public FileStreamResult VigenciaFileManagerPartialDownload()
        {
            return FileManagerExtension.DownloadFiles("VigenciaFileManager", TestingControllerControllerVigenciaFileManagerSettings.Model);
        }
	}
    public class TestingControllerControllerVigenciaFileManagerSettings
    {
        public const string RootFolder = @"~\Content\Vigencias";//Content\Vigencia

        public static string Model { get { return RootFolder; } }
    }

}