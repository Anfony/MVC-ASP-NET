using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// Devuelve los comités con sus respectivos proyectos asignados
    /// </summary>
    public class ProyectosIEController : ApiController
    {
        private WebServicesModel webServicesModel;

        public ProyectosIEController()
        {
            webServicesModel = new WebServicesModel();
        }

        [ResponseType(typeof(IQueryable<ProyectosIEDTO>))]
        public IHttpActionResult GetProyectosIEById(int Anio, int Id)
        {
            IQueryable<ProyectosIEDTO> _datos = webServicesModel.GetProyectosAutorizadosComitesById(Anio, Id)
                                                .Select(item => new ProyectosIEDTO()
                                                {
                                                    Id = item.Id,
                                                    InstanciaEjecutora = item.Comite,
                                                    ProyectoAutorizado = item.ProyectoAutorizado,
                                                    RecursoOtorgado = item.RecursoAsignado
                                                });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}