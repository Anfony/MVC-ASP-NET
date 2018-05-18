using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DXMVCWebApplication1.Controllers
{
    public class FirmasAutorizadasController : Controller
    {
        private DB_SENASICAEntities db = new DB_SENASICAEntities();

        // GET: FirmasAutorizadas
        public ActionResult Index(string nombreReporte, string nombreEstado, bool successfulSave = false)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FIRMAS_AUTORIZADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            FirmasAutorizadasVM firmaAutorizada = null;

            ViewData["successfulSave"] = successfulSave;

            bool existeFirmaAutorizada = ExisteFirmaAutrizada(nombreReporte, nombreEstado);

            ViewData["existenFirmas"] = existeFirmaAutorizada;
            ViewData["nombreReporte"] = nombreReporte;
            ViewData["nombreEstado"] = nombreEstado;

            if (existeFirmaAutorizada)
            {
                int IdFirmaAutorizada = getIdFirmaAurotizada(nombreReporte, nombreEstado);

                var _queryDatosFirma1_Puesto = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 1).Select(d => d.Puesto);
                var _queryDatosFirma1_Firmante = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 1).Select(d => d.NombreFirmante);

                var _queryDatosFirma2_Puesto = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 2).Select(d => d.Puesto);
                var _queryDatosFirma2_Firmante = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 2).Select(d => d.NombreFirmante);

                var _queryDatosFirma3_Puesto = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 3).Select(d => d.Puesto);
                var _queryDatosFirma3_Firmante = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == 3).Select(d => d.NombreFirmante);

                firmaAutorizada = new FirmasAutorizadasVM
                {
                    Puesto1 = _queryDatosFirma1_Puesto.Count() == 0 ? "" : _queryDatosFirma1_Puesto.First(),
                    Firmante1 = _queryDatosFirma1_Firmante.Count() == 0 ? "" : _queryDatosFirma1_Firmante.First(),
                    Puesto2 = _queryDatosFirma2_Puesto.Count() == 0 ? "" : _queryDatosFirma2_Puesto.First(),
                    Firmante2 = _queryDatosFirma2_Firmante.Count() == 0 ? "" : _queryDatosFirma2_Firmante.First(),
                    Puesto3 = _queryDatosFirma3_Puesto.Count() == 0 ? "" : _queryDatosFirma3_Puesto.First(),
                    Firmante3 = _queryDatosFirma3_Firmante.Count() == 0 ? "" : _queryDatosFirma3_Firmante.First(),
                };
            }

            return View(firmaAutorizada);
        }

        /// <summary>
        /// De acuerdo a reporte y estado ingresado por el usuario, busca si tiene asociado firmas
        /// </summary>
        /// <param name="nombreReporte"></param>
        /// <param name="nombreEstado"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ObtieneFirmas(string nombreReporte, string nombreEstado)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FIRMAS_AUTORIZADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            return RedirectToAction("Index", new { nombreReporte, nombreEstado });
        }

        /// <summary>
        /// Crea - Actualiza las firmas autorizadas de un reporte
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ActionResult CreateUpdateFirmas(FirmasAutorizadasVM item)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FIRMAS_AUTORIZADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            if(!ExisteFirmaAutrizada(item.nombreReporte, item.nombreEstado))
            {
                //se genera un registro en SIS.FirmasAutorizadas
                FirmasAutorizada firmaAutorizada = new FirmasAutorizada { Fk_IdEstado = db.Estadoes.Where(e => e.Nombre == item.nombreEstado).Select(e => e.Pk_IdEstado).First(),
                                                                            Fk_IdReportes = db.Reportes.Where(r => r.NombreReporte == item.nombreReporte).Select(r => r.Pk_IdReportes).First()
                                                                        };

                var model = db.FirmasAutorizadas;
                model.Add(firmaAutorizada);
                db.SaveChanges();
            }

            int IdFirmaAutorizada = getIdFirmaAurotizada(item.nombreReporte, item.nombreEstado);

            //Create/Update -- Firmas

            if(item.Puesto1 != null || item.Firmante1 != null)
                AgregaDatosFirma(item.Puesto1 == null ? "" : item.Puesto1, item.Firmante1 == null ? "" : item.Firmante1, 1, IdFirmaAutorizada);

            if (item.Puesto2 != null || item.Firmante2 != null)
                AgregaDatosFirma(item.Puesto2 == null ? "" : item.Puesto2, item.Firmante2 == null ? "" : item.Firmante2, 2, IdFirmaAutorizada);

            if (item.Puesto3 != null || item.Firmante3 != null)
                AgregaDatosFirma(item.Puesto3 == null ? "" : item.Puesto3, item.Firmante3 == null ? "" : item.Firmante3, 3, IdFirmaAutorizada);

            return RedirectToAction("Index", new { nombreReporte = "", nombreEstado = "", successfulSave = true });
        }

        /// <summary>
        /// Agrega los datos de la firma de un reporte
        /// </summary>
        /// <param name="Puesto">Puesto de la persona a firmar</param>
        /// <param name="Nombre">Nombre de la persona a firmar</param>
        /// <param name="Orden">Orden de aparición de la firma</param>
        /// <param name="IdFirmaAutorizada">Referencia a la firma autorizada</param>
        private void AgregaDatosFirma(string Puesto, string Nombre, int Orden, int IdFirmaAutorizada)
        {
            #region Obtiene permisos del usuario logeado
            Dictionary<string, bool> permisos = Senasica.GetPermisoPantallaXUsuario(((UserData)Session["DataUsuario"]).RolesUsuario[0].ToString(), ListaPantallas.SIS_FIRMAS_AUTORIZADAS);
            ViewData["Lectura"] = permisos["Lectura"];
            ViewData["Escritura"] = permisos["Escritura"];
            #endregion

            if (ExisteDatosFirma(IdFirmaAutorizada, Orden))
            {
                //Se actualiza la firma
                var model = db.DatosFirmas;
                int IdDatoFirma = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada && d.Orden == Orden).Select(d => d.Pk_IdFirmas).First();

                var modelItem = model.FirstOrDefault(it => it.Pk_IdFirmas == IdDatoFirma);
                modelItem.Puesto = Puesto;
                modelItem.NombreFirmante = Nombre;
                UpdateModel(modelItem);
                db.SaveChanges();
            }
            else
            {
                //Se crea la firma
                DatosFirma datoFirma = new DatosFirma
                {
                    Puesto = Puesto,
                    NombreFirmante = Nombre,
                    Orden = Orden,
                    Fk_IdFirmasAutorizadas = IdFirmaAutorizada
                };
                var model = db.DatosFirmas;
                model.Add(datoFirma);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Pregunta si existe una firma autorizada para cierto reporte y cierto estado. También se asegura que existan sus detalles de las firmas (SIS.DatosFirmas)
        /// </summary>
        /// <param name="nombreReporte"></param>
        /// <param name="nombreEstado"></param>
        /// <returns></returns>
        private bool ExisteFirmaAutrizada(string nombreReporte, string nombreEstado)
        {
            bool existeFirmasAsociadas = false;

            var _query = db.FirmasAutorizadas.Where(f => f.Reporte.NombreReporte == nombreReporte && f.Estado.Nombre == nombreEstado);

            if (_query.Count() == 0) existeFirmasAsociadas = false;
            else
            {
                int IdFirmaAutorizada = getIdFirmaAurotizada(nombreReporte, nombreEstado);
                var _queryAux = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmaAutorizada);

                existeFirmasAsociadas = _queryAux.Count() == 0 ? false : true;
            }

            return existeFirmasAsociadas;
        }

        /// <summary>
        /// Pregunta si existe datos de alguna firma en el orden ingresado para cierto reporte y cierto estado
        /// </summary>
        /// <param name="IdFirmasAutorizadas"></param>
        /// <param name="Orden"></param>
        /// <returns></returns>
        private bool ExisteDatosFirma(int IdFirmasAutorizadas, int Orden)
        {
            var _query = db.DatosFirmas.Where(d => d.Fk_IdFirmasAutorizadas == IdFirmasAutorizadas && d.Orden == Orden);

            return _query.Count() == 0 ? false : true;
        }

        /// <summary>
        /// Obtiene el Id de la Firma autorizada de acuerdo al reporte y estado
        /// </summary>
        /// <param name="nombreReporte"></param>
        /// <param name="nombreEstado"></param>
        /// <returns></returns>
        private int getIdFirmaAurotizada(string nombreReporte, string nombreEstado)
        {
            int IdFirmaAurotizada;

            if (nombreReporte != null && nombreEstado != null)
                IdFirmaAurotizada = db.FirmasAutorizadas.Where(f => f.Reporte.NombreReporte == nombreReporte && f.Estado.Nombre == nombreEstado).Select(f => f.Pk_IdFirmasAutorizadas).First();
            else IdFirmaAurotizada = -1;

            return IdFirmaAurotizada;
        }
    }
}