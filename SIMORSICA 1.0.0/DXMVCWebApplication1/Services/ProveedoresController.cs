using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// Proveedores
    /// </summary>
    public class ProveedoresController : ApiController
    {
        private WebServicesModel webServicesModel;

        public ProveedoresController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Devuelve todos los proveedores
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<ProveedoresDTO>))]
        public IHttpActionResult GetProveedores()
        {
            IQueryable<ProveedoresDTO> _datos = webServicesModel.GetProveedores_All()
                                                .Select(item => new ProveedoresDTO()
                                                {
                                                    Id = item.Id,
                                                    RazonSocial = item.RazonSocial,
                                                    RFC = item.RFC,
                                                    Estado = item.Estado,
                                                    Municipio = item.Municipio,
                                                    Colonia = item.Colonia,
                                                    Calle = item.Calle,
                                                    Telefono = item.Telefono,
                                                    Email = item.Email
                                                });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }

        /// <summary>
        /// Devuelve todos los proveedores del Estado solicitado
        /// </summary>
        /// <param name="Id">Id del Estado</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<ProveedoresDTO>))]
        public IHttpActionResult GetProveedoresByEstado(int Id)
        {
            IQueryable<ProveedoresDTO> _datos = webServicesModel.GetProveedores_ByEstado(Id)
                                                .Select(item => new ProveedoresDTO()
                                                {
                                                    Id = item.Id,
                                                    RazonSocial = item.RazonSocial,
                                                    RFC = item.RFC,
                                                    Estado = item.Estado,
                                                    Municipio = item.Municipio,
                                                    Colonia = item.Colonia,
                                                    Calle = item.Calle,
                                                    Telefono = item.Telefono,
                                                    Email = item.Email
                                                });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}