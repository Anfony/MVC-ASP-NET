using DXMVCWebApplication1.DTO;
using DXMVCWebApplication1.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DXMVCWebApplication1.Services
{
    /// <summary>
    /// PEF (Presupuesto de Egresos de la Federación)
    /// </summary>
    public class PEFController : ApiController
    {
        private WebServicesModel webServicesModel;

        public PEFController()
        {
            webServicesModel = new WebServicesModel();
        }

        /// <summary>
        /// Devuelve el Presupuesto de Egresos de la Federación de acuerdo al año que se está solicitando
        /// </summary>
        /// <param name="Anio">Año dle PEF</param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<PefDTO>))]
        public IHttpActionResult GetPEF(int Anio)
        {
            IQueryable<PefDTO> _datos = webServicesModel.GetPEFXEstados(Anio)
                                        .Select(item => new PefDTO()
                                        {
                                            Estado = item.Estado,

                                            Tran_SistemaInformatico = item.SistemaInform,
                                            Tran_Capacitacion = item.Capacitacion,
                                            Tran_Divulgacion = item.Divulgacion,
                                            Tran_EmerSanitarias = item.Emergencias,
                                            Tran_Total = item.Gtotransv,

                                            Comp_Curantenaria_InspVig = item.Comp_CampFitoZoo,
                                            Comp_Fitozoosanitaria = item.Comp_InspVig,
                                            Comp_Curantenaria_Vig = item.Comp_VigCuarentenaria,
                                            Comp_Inocuidad = item.Comp_IAAP,
                                            Comp_Total = item.Componentes,

                                            GtoInversion = item.GtoInversion,
                                            GtoOperacion = item.GtoOperacion,
                                            PresAutorizado = item.Importe
                                        });

            if (_datos == null || _datos.Count() == 0) return NotFound();

            return Ok(_datos);
        }
    }
}