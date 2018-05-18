using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    public class TipoActividadController : ApiController
    {
        private WebServicesModel webServicesModel;

        public TipoActividadController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Obtiene el catálogo de Tipo de Actividad para el año presupuestal del sistema
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<TipoActividadDTO>))]
        public IHttpActionResult GetTipoActividad()
        {
            int IdAnioPres = 3;   //FuncionesAuxiliares.GetCurrent_IdAnioPresupuestal();

            IQueryable<TipoActividadDTO> _datos = webServicesModel.VW_TipoActividad
                                            .Where(item => item.IdAnio == IdAnioPres)
                                            .Select(item => new TipoActividadDTO()
                                            {
                                                Id = item.IdTipoActividad,
                                                ConceptoApoyo = item.ConceptoApoyo,
                                                ProyectoAutorizado = item.ProyectoAutorizado,
                                                TipoAccion = item.TipoAccion,
                                                TipoActividad = item.TipoActividad,
                                                UnidadMedida = item.UnidadMedida,
                                                Anio = item.Anio
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }

        /// <summary>
        /// Obtiene el Tipo de Actividad por su Id
        /// 
        /// Cada Tipo de Actividad le corresponde un reporte dinámico
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<TipoActividadDTO>))]
        public IHttpActionResult GetTipoActividad(int Id)
        {
            IQueryable<TipoActividadDTO> _datos = webServicesModel.VW_TipoActividad
                                            .Where(item => item.IdTipoActividad == Id)
                                            .Select(item => new TipoActividadDTO()
                                            {
                                                Id = item.IdTipoActividad,
                                                ConceptoApoyo = item.ConceptoApoyo,
                                                ProyectoAutorizado = item.ProyectoAutorizado,
                                                TipoAccion = item.TipoAccion,
                                                TipoActividad = item.TipoActividad,
                                                UnidadMedida = item.UnidadMedida,
                                                Anio = item.Anio
                                            });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}