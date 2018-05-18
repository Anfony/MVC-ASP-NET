using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using System;
using System.Linq;

namespace DXMVCWebApplication1.Negocio
{
    /// <summary>
    /// Realiza la validación de que una campaña esté creándose con montos que no excedan a los que se tienen 
    /// asignados a su proyecto correspondiente
    /// </summary>
    public class ValidaMontosCampania
    {
        private DB_SENASICAEntities db;

        private int IdCampania;

        private decimal presFedReportado;
        private decimal presEstReportado;
        private decimal presProReportado;

        private decimal presFedAsignado;
        private decimal presEstAsignado;
        private decimal presProAsignado;

        //Presupuesto usuado para gastos fijos de operación
        private decimal presFedGFO;
        private decimal presEstGFO;
        private decimal presProGFO;

        private const string OK = "OK";
        private const string EXCEDIDO = "NO VÁLIDO";

        public ValidaMontosCampania(int IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
            {
                db = new DB_SENASICAEntities();
                this.IdCampania = IdCampania;

                int? IdProyectoPresupuesto = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                        .Select(item => item.Fk_IdProyectoPresupuesto)
                                                        .First();

                //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                var _qReportadoFed = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecFed);
                var _qReportadoEst = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecEst);
                var _qReportadoPro = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecProp);

                presFedReportado = _qReportadoFed.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoFed.Sum());
                presEstReportado = _qReportadoEst.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoEst.Sum());
                presProReportado = _qReportadoPro.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoPro.Sum());

                //Obtiene los presupuestos Federal, Estatal y Propios asignados en la campaña
                var _qAsignadoFed = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecFed);
                var _qAsignadoEst = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecEst);
                var _qAsignadoPro = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecPro);

                presFedAsignado = _qAsignadoFed.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoFed.First());
                presEstAsignado = _qAsignadoEst.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoEst.First());
                presProAsignado = _qAsignadoPro.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoPro.First());

                //Obtiene el presupuesto asignado en Gastos Fijos de Operación
                var _qGFOFed = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecFed != null).Select(item => item.Monto);
                var _qGFOEst = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecEst != null).Select(item => item.Monto);
                var _qGFOPro = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecProp != null).Select(item => item.Monto);

                presFedGFO = _qGFOFed.Count() == 0 ? 0 : _qGFOFed.Sum();
                presEstGFO = _qGFOEst.Count() == 0 ? 0 : _qGFOEst.Sum();
                presProGFO = _qGFOPro.Count() == 0 ? 0 : _qGFOPro.Sum();
            }
        }
        /// <summary>
        /// Regresa 'true' si el presupuesto que están reportando en la campaña no supera al presupuesto
        /// que está asignado a la campaña correspondientes
        /// </summary>
        /// <returns></returns>
        public bool EsCampaniaValida()
        {
            return ((presFedReportado + presFedGFO) == presFedAsignado || (presFedReportado + presFedGFO) == 0)
                && ((presEstReportado + presEstGFO) == presEstAsignado || (presEstReportado + presEstGFO) == 0)
                && ((presProReportado + presProGFO) == presProAsignado || (presProReportado + presProGFO) == 0);
        }

        //[Get]: Presupuesto Asignado a la campaña (de acuerdo al proyecto autorizado)
        public decimal? GetPresAsignadoFed() { return presFedAsignado; }
        public decimal? GetPresAsignadoEst() { return presEstAsignado; }
        public decimal? GetPresAsignadoPro() { return presProAsignado; }

        //[Get]: Presupuesto Reportado en la campaña (lo que están reportando en los detalles de la campaña)
        public decimal? GetPresReportadoFed() { return presFedReportado; }
        public decimal? GetPresReportadoEst() { return presEstReportado; }
        public decimal? GetPresReportadoPro() { return presProReportado; }

        //[Get]: Presupuesto Reportado para Gastos Fijos de Operación con dinero de la misma campaña
        public decimal? GetPresGFOFed() { return presFedGFO; }
        public decimal? GetPresGFOEst() { return presEstGFO; }
        public decimal? GetPresGFOPro() { return presProGFO; }

        // Status de los presupuesto
        public string StatusRecFed() { return (presFedReportado + presFedGFO) == presFedAsignado || (presFedReportado + presFedGFO) == 0 ? OK : EXCEDIDO; }
        public string StatusRecEst() { return (presEstReportado + presEstGFO) == presEstAsignado || (presEstReportado + presEstGFO) == 0 ? OK : EXCEDIDO; }
        public string StatusRecPro() { return (presProReportado + presProGFO) == presProAsignado || (presProReportado + presProGFO) == 0 ? OK : EXCEDIDO; }
    }
    public class ValidaMontosCampania_Repro
    {
        private DB_SENASICAEntities db;

        private int IdCampania;

        private decimal presFedReportado;
        private decimal presEstReportado;
        private decimal presProReportado;

        private decimal presFedAsignado;
        private decimal presEstAsignado;
        private decimal presProAsignado;

        //Presupuesto usuado para gastos fijos de operación
        private decimal presFedGFO;
        private decimal presEstGFO;
        private decimal presProGFO;

        private const string OK = "OK";
        private const string EXCEDIDO = "NO VÁLIDO";

        public ValidaMontosCampania_Repro(int IdCampania)
        {
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));

            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                db = new DB_SENASICAEntities();
                this.IdCampania = IdCampania;

                int? IdProyectoPresupuesto = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                        .Select(item => item.Fk_IdProyectoPresupuesto)
                                        .First();

                //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                var _qReportadoFed = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecFed);
                var _qReportadoEst = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecEst);
                var _qReportadoPro = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecProp);

                presFedReportado = _qReportadoFed.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoFed.Sum());
                presEstReportado = _qReportadoEst.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoEst.Sum());
                presProReportado = _qReportadoPro.Count() == 0 ? 0 : Convert.ToDecimal(_qReportadoPro.Sum());

                //Obtiene los presupuestos Federal, Estatal y Propios asignados en la campaña
                var _qAsignadoFed = db.Campania_Repro.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecFed);
                var _qAsignadoEst = db.Campania_Repro.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecEst);
                var _qAsignadoPro = db.Campania_Repro.Where(item => item.Pk_IdCampania == IdCampania).Select(item => item.RecPro);

                presFedAsignado = _qAsignadoFed.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoFed.First());
                presEstAsignado = _qAsignadoEst.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoEst.First());
                presProAsignado = _qAsignadoPro.Count() == 0 ? 0 : Convert.ToDecimal(_qAsignadoPro.First());

                //Obtiene el presupuesto asignado en Gastos Fijos de Operación
                var _qGFOFed = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecFed != null).Select(item => item.Monto);
                var _qGFOEst = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecEst != null).Select(item => item.Monto);
                var _qGFOPro = db.ProgramasGastosTransversales.Where(item => item.Fk_IdProyectoPresupuesto == IdProyectoPresupuesto && item.ProgramaAnualAdq.RecProp != null).Select(item => item.Monto);

                presFedGFO = _qGFOFed.Count() == 0 ? 0 : _qGFOFed.Sum();
                presEstGFO = _qGFOEst.Count() == 0 ? 0 : _qGFOEst.Sum();
                presProGFO = _qGFOPro.Count() == 0 ? 0 : _qGFOPro.Sum();
            }
        }
        /// <summary>
        /// Regresa 'true' si el presupuesto que están reportando en la campaña no supera al presupuesto
        /// que está asignado a la campaña correspondientes
        /// </summary>
        /// <returns></returns>
        public bool EsCampaniaValida()
        {
            return ((presFedReportado + presFedGFO) == presFedAsignado || (presFedReportado + presFedGFO) == 0)
                    && ((presEstReportado + presEstGFO) == presEstAsignado || (presEstReportado + presEstGFO) == 0)
                    && ((presProReportado + presProGFO) == presProAsignado || (presProReportado + presProGFO) == 0);
        }

        //[Get]: Presupuesto Asignado a la campaña (de acuerdo al proyecto autorizado)
        public decimal? GetPresAsignadoFed() { return presFedAsignado; }
        public decimal? GetPresAsignadoEst() { return presEstAsignado; }
        public decimal? GetPresAsignadoPro() { return presProAsignado; }

        //[Get]: Presupuesto Reportado en la campaña (lo que están reportando en los detalles de la campaña)
        public decimal? GetPresReportadoFed() { return presFedReportado; }
        public decimal? GetPresReportadoEst() { return presEstReportado; }
        public decimal? GetPresReportadoPro() { return presProReportado; }

        //[Get]: Presupuesto Reportado para Gastos Fijos de Operación con dinero de la misma campaña
        public decimal? GetPresGFOFed() { return presFedGFO; }
        public decimal? GetPresGFOEst() { return presEstGFO; }
        public decimal? GetPresGFOPro() { return presProGFO; }

        // Status de los presupuesto
        public string StatusRecFed() { return (presFedReportado + presFedGFO) == presFedAsignado || (presFedReportado + presFedGFO) == 0 ? OK : EXCEDIDO; }
        public string StatusRecEst() { return (presEstReportado + presEstGFO) == presEstAsignado || (presEstReportado + presEstGFO) == 0 ? OK : EXCEDIDO; }
        public string StatusRecPro() { return (presProReportado + presProGFO) == presProAsignado || (presProReportado + presProGFO) == 0 ? OK : EXCEDIDO; }
    }
}