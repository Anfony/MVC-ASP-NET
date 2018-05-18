using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// Catálogo de las Unidades Responsables
    /// </summary>
    public class URController : ApiController
    {
        private WebServicesModel webServicesModel;

        public URController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Obtiene todas las unidades responsables
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<UnidadesResponsablesDTO>))]
        public IHttpActionResult GetUR()
        {
            IQueryable<UnidadesResponsablesDTO> _datos = webServicesModel.GetUnidadesResponsables()
                                                            .Select(item => new UnidadesResponsablesDTO()
                                                            {
                                                                Id = item.Id,
                                                                Nombre = item.Nombre
                                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}