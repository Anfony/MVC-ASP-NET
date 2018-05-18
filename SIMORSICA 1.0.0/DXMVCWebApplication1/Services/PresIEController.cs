using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// Este WebService devuelve el presupuesto total que se le fue asignado a cada comité.
    /// 
    /// El presupuesto total asignado a partir de todos los programas que el comité tiene validado para poder trabajar, de 
    /// estos se le sumará el recurso federal dando como resultado el presupuesto total
    /// </summary>
    public class PresIEController : ApiController
    {
        private WebServicesModel webServicesModel;

        public PresIEController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Obtiene todos los comités con su respectivo presupuesto.
        /// </summary>
        /// <param name="Anio">Año del cual se pide el presupuesto asignado al comité</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<PresComitesDTO>))]
        public IHttpActionResult GetPresIE(int Anio)
        {
            IQueryable<PresComitesDTO> _datos = webServicesModel.GetPresComite_All(Anio)
                                                .Select(item => new PresComitesDTO()
                                                {
                                                    Id = item.IdComite,
                                                    InstanciaEjecutora = item.Comite,
                                                    PresupuestoOtorgado = item.RecursoAsignado
                                                });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }

        /// <summary>
        /// Devuelve el presupuesto asignado al comité solicitado
        /// </summary>
        /// <param name="Anio">Año del cual se pide el presupuesto asignado al comité</param>
        /// <param name="Id">Identificador único del comité</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<PresComitesDTO>))]
        public IHttpActionResult GetPresIEById(int Anio, int Id)
        {
            IQueryable<PresComitesDTO> _datos = webServicesModel.GetPresComite_ByComite(Anio, Id)
                                                .Select(item => new PresComitesDTO()
                                                {
                                                    Id = item.IdComite,
                                                    InstanciaEjecutora = item.Comite,
                                                    PresupuestoOtorgado = item.RecursoAsignado
                                                });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}