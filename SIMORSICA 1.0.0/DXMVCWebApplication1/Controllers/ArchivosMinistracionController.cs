using DevExpress.Web.ASPxUploadControl.Internal;
using DevExpress.Web.Mvc;
using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace DXMVCWebApplication1.Controllers
{
    public class ArchivosMinistracionController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        [ValidateInput(false)]
        public ActionResult Index(int? IdMinistracionesXComite)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.UE_MINISTRACION_COMITE);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            Session["IdMinistracionesXComite"] = IdMinistracionesXComite != null ? IdMinistracionesXComite : 0;
            ViewData["IdMinistracionesXComite"] = IdMinistracionesXComite != null ? IdMinistracionesXComite : 0;
            IQueryable<string> _query;

            if (IdMinistracionesXComite == null) ViewData["existeArchivo"] = false;
            else
            {
                _query = db.MinistracionesXComites.Where(c => c.Pk_IdMinistracionesXComite == IdMinistracionesXComite).Select(c => c.Documento);
                ViewData["existeArchivo"] = _query.Count() == 0 || _query.First() == null ? false : true;
            }
            return PartialView();
        }

        public DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xml", ".pdf" },
            MaxFileSize = 4000000
        };

        /// <summary>
        /// Empieza la carga del Archivo
        /// </summary>
        /// <returns></returns>
        public ActionResult CargaArchivo(string TypeFile)
        {
            UploadControlExtension.GetUploadedFiles("UploadArchivo", ValidationSettings, FileUploadCompleteArchivo);

            return null;
        }

        public void FileUploadCompleteArchivo(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                int IdMinistracionesXComite = Convert.ToInt32(Session["IdMinistracionesXComite"]);
                var _query = db.MinistracionesXComites.Where(c => c.Pk_IdMinistracionesXComite == IdMinistracionesXComite).Select(c => c.Documento);
                bool existeArchivo = _query.Count() == 0 || _query.First() == null ? false : true;

                if (existeArchivo) EliminaArchivo(IdMinistracionesXComite, "Archivo");

                string nombreArchivo = IdMinistracionesXComite + "_Ministración_" + Path.GetFileNameWithoutExtension(e.UploadedFile.FileName) + Path.GetExtension(e.UploadedFile.FileName);
                string directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoMinistracion"].ToString();
                string rutadearchivo = directorio + nombreArchivo;

                e.UploadedFile.SaveAs(rutadearchivo);
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;

                if (urlResolver != null) e.CallbackData = urlResolver.ResolveClientUrl(rutadearchivo) + "?refresh=" + Guid.NewGuid().ToString();

                var model = db.MinistracionesXComites;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdMinistracionesXComite == IdMinistracionesXComite);

                if (modelItem != null)
                {
                    modelItem.Documento = nombreArchivo;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }


        public void EliminaArchivo(int? IdMinistracionesXComite, string TypeFile)
        {
            IQueryable<string> _query;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoMinistracion"].ToString();
            _query = db.MinistracionesXComites.Where(c => c.Pk_IdMinistracionesXComite == IdMinistracionesXComite).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (!(directorio == "" || nombreArchivo == ""))
            {
                System.IO.File.Delete(directorio + nombreArchivo);

                var model = db.MinistracionesXComites;
                var modelItem = model.FirstOrDefault(item => item.Pk_IdMinistracionesXComite == IdMinistracionesXComite);

                if (modelItem != null)
                {
                    modelItem.Documento = null;
                    UpdateModel(modelItem);
                    db.SaveChanges();
                }
            }
        }

        public ActionResult DescargaArchivo(int? IdMinistracionesXComite)
        {
            IQueryable<string> _query;
            string contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
            string directorio;
            string nombreArchivo;

            directorio = System.Configuration.ConfigurationManager.AppSettings["DirectorioArchivoMinistracion"].ToString();
            _query = db.MinistracionesXComites.Where(c => c.Pk_IdMinistracionesXComite == IdMinistracionesXComite).Select(c => c.Documento);
            nombreArchivo = _query.Count() == 0 || _query.First() == null ? "" : _query.First();

            if (directorio == "" || nombreArchivo == "" || !System.IO.File.Exists(directorio + nombreArchivo)) return PartialView("FileNotFound");

            string rutadearchivo = directorio + nombreArchivo;
            return File(rutadearchivo, contentType, nombreArchivo);
        }
    }
}