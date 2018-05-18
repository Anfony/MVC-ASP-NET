using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    public class EstadosController : ApiController
    {
        private WebServicesModel webServicesModel;

        public EstadosController()
        {
            webServicesModel = new WebServicesModel();
        }

        [ResponseType(typeof(IQueryable<EstadosDTO>))]
        public IHttpActionResult GetEstados()
        {
            IQueryable<EstadosDTO> _datos = webServicesModel.GetEstados()
                                            .Select(item => new EstadosDTO()
                                            {
                                                Id = item.Id,
                                                Nombre = item.Nombre
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}