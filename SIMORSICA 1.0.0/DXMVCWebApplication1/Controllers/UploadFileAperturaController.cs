using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DXMVCWebApplication1.Controllers
{
    public class UploadFileAperturaController : Controller
    {
        DB_SENASICAEntities db = new DB_SENASICAEntities();
        //
        // GET: /UploadFileApertura/
        public ActionResult Index(int Pk_IdAperturas)
        {
            Session["Pk_IdAperturas"] = Pk_IdAperturas != null ? Pk_IdAperturas : 0;
            ViewData["Pk_IdAperturas"] = Pk_IdAperturas != null ? Pk_IdAperturas : 0;

            IQueryable<string> _query;

            if (Pk_IdAperturas == null) ViewData["existeArchivo"] = false;
            else
            {
                _query = db.Aperturas.Where(c => c.Pk_IdAperturas == Pk_IdAperturas).Select(c => c.Documento);
                ViewData["existeArchivo"] = _query.Count() == 0 || _query.First() == null ? false : true;
            }
            return PartialView();
        }

        public DevExpress.Web.ASPxUploadControl.ValidationSettings Validation = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".pdf" },
            MaxFileSize = 4000000
        };

        public ActionResult Upload(string TypeFile)
        {
            UploadControlExtension.GetUploadedFiles("UploadArchivo", Validation, FileUploadCompleteArchivo);
            return null;
        }

        public void FileUploadCompleteArchivo(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int Pk_IdAperturas = Convert.ToInt32(Session["Pk_IdAperturas"]);
                var _query = db.Aperturas.Where(c => c.Pk_IdAperturas == Pk_IdAperturas).Select(c => c.Documento);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(Pk_IdAperturas, "Archivo");

                string nombreArchivo = Pk_IdAperturas + "_Apertura_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoApertura"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.Aperturas;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdAperturas == Pk_IdAperturas);

                if (modelItem != null)
                {
                    modelItem.Documento = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public ActionResult Download(int? Pk_IdAperturas)
        {
            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoApertura"].ToString();
            _query = db.Aperturas.Where(c => c.Pk_IdAperturas == Pk_IdAperturas).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return PartialView("FileNotFound");

            string rutadearchivo = directorio + nombreArchivo;
            return File(rutadearchivo, contentType, nombreArchivo);
        }

        public void EliminaArchivo(int? Pk_IdAperturas, string TypeFile)
        {
            IQueryable<string> _query;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoApertura"].ToString();
            _query = db.Aperturas.Where(c => c.Pk_IdAperturas == Pk_IdAperturas).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (!(directorio == "" || nombreArchivo == ""))
            {
                System.IO.File.Delete(directorio + nombreArchivo);

                var model = db.Aperturas;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdAperturas == Pk_IdAperturas);

                if (modelItem != null)
                {
                    modelItem.Documento = null;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }
	}
}