using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// Catálogo de los comités.
    /// 
    /// Cada uno de los registros tiene el nombre del comité, el nombre de la Unidad Responsable 
    /// y el Estado al que pertenece.
    /// </summary>
    public class IEController : ApiController
    {
        private WebServicesModel webServicesModel;

        public IEController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Obtiene todos los comités
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<IEDTO>))]
        public IHttpActionResult GetIE()
        {
            IQueryable<IEDTO> _datos = webServicesModel.GetComites_All()
                                            .Select(item => new IEDTO()
                                            {
                                                IdUR = item.IdUR,
                                                UnidadResponsable = item.UnidadResponsable,
                                                IdEstado = item.IdEstado,
                                                Estado = item.Estado,
                                                IdIE = item.IdComite,
                                                InstanciaEjecutora = item.Comite,
                                                SitioWeb = item.SitioWeb
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }


        /// <summary>
        /// Obtiene el comité solicitado y su Unidad Responsable correspondiente
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<IEDTO>))]
        public IHttpActionResult GetIE(int Id)
        {
            IQueryable<IEDTO> _datos = webServicesModel.GetComites_ByComite(Id)
                                            .Select(item => new IEDTO()
                                            {
                                                IdUR = item.IdUR,
                                                UnidadResponsable = item.UnidadResponsable,
                                                IdEstado = item.IdEstado,
                                                Estado = item.Estado,
                                                IdIE = item.IdComite,
                                                InstanciaEjecutora = item.Comite,
                                                SitioWeb = item.SitioWeb
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }

        /// <summary>
        /// Obtiene los comités pertenecientes a cierta Unidad Responsable
        /// </summary>
        /// <param name="Id">Id de la Unidad Responsable</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<IEDTO>))]
        public IHttpActionResult GetIEByUR(int Id)
        {
            IQueryable<IEDTO> _datos = webServicesModel.GetComites_ByUR(Id)
                                            .Select(item => new IEDTO()
                                            {
                                                IdUR = item.IdUR,
                                                UnidadResponsable = item.UnidadResponsable,
                                                IdEstado = item.IdEstado,
                                                Estado = item.Estado,
                                                IdIE = item.IdComite,
                                                InstanciaEjecutora = item.Comite,
                                                SitioWeb = item.SitioWeb
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }

        /// <summary>
        /// Obtiene los comités pertenecientes a un Estado
        /// </summary>
        /// <param name="Id">Id del Estado</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<IEDTO>))]
        public IHttpActionResult GetIEByEstado(int Id)
        {
            IQueryable<IEDTO> _datos = webServicesModel.GetComites_ByEstado(Id)
                                            .Select(item => new IEDTO()
                                            {
                                                IdUR = item.IdUR,
                                                UnidadResponsable = item.UnidadResponsable,
                                                IdEstado = item.IdEstado,
                                                Estado = item.Estado,
                                                IdIE = item.IdComite,
                                                InstanciaEjecutora = item.Comite,
                                                SitioWeb = item.SitioWeb
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}