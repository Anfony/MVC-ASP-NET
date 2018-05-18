using DXMVCWebApplication1.Common;
using DXMVCWebApplication1.Models;
using DXMVCWebApplication1.ViewModels;
using System;
using System.Linq;

namespace DXMVCWebApplication1.Negocio
{
    // ======================================================= PLANEACIÓN --> GASTOS FIJOS DE OPERACIÓN (S, U) ===============================================================

    /// <summary>
    /// 1. Para los Bienes o Servicios que son de tipo "Genérico", se valida que el recurso total no sobrepase el 2% de los recursos asignados al comité
    /// </summary>
    public class ValidaMontos_GFO
    {
        private DB_SENASICAEntities db;

        private int? Pk_IdProgramaAnualAdq;
        private int? _Fk_IdUnidadEjecutora;
        private int IdAnio;
        private bool? esGastoU;
        private decimal? porcentajeMaximo;
        private decimal? RecFed;
        private decimal? RecEst;

        private decimal presFed_Generico;
        private decimal presEst_Generico;

        private decimal? presFed_ByUnidadEjecutora;
        private decimal presEst_ByUnidadEjecutora;

        public ValidaMontos_GFO(int? Pk_IdProgramaAnualAdq, int? _Fk_IdUnidadEjecutora, int IdAnio, bool? esGastoU, decimal? RecFed, decimal? RecEst, decimal? porcentajeMaximo)
        {
            db = new DB_SENASICAEntities();
            this.Pk_IdProgramaAnualAdq = Pk_IdProgramaAnualAdq;
            this._Fk_IdUnidadEjecutora = _Fk_IdUnidadEjecutora;
            this.IdAnio = IdAnio;
            this.esGastoU = esGastoU;
            this.RecFed = RecFed;
            this.RecEst = RecEst;
            this.porcentajeMaximo = porcentajeMaximo;

            if (porcentajeMaximo != null)
            {
                if (Pk_IdProgramaAnualAdq > 0) // Cuando es un UPDATE del Registro de GFO
                {
                    var presFedGFO = db.ProgramaAnualAdqs.Where(i => i.Fk_IdAnio__SIS == IdAnio
                                                      && i.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                                                      && i.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq
                                                      && i.Fk_IdActividadXAccion == null
                                                      && i.Fk_IdAccionXCampania == null
                                                      && i.Fk_IdCampania__UE == null
                                                      && i.esGastoU == esGastoU
                                                      && i.BienOServ.porcentajeMaximo == 2)
                                                      .Select(i => i.RecFed);
                                   
                    var presEstGFO = db.ProgramaAnualAdqs.Where(i => i.Fk_IdAnio__SIS == IdAnio
                                                      && i.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                                                      && i.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq
                                                      && i.Fk_IdActividadXAccion == null
                                                      && i.Fk_IdAccionXCampania == null
                                                      && i.Fk_IdCampania__UE == null
                                                      && i.esGastoU == esGastoU
                                                      && i.BienOServ.porcentajeMaximo == 2)
                                                      .Select(i => i.RecEst);

                    // Obtiene el total de los Gastos Genéricos 2%, realizado por la Instancia Ejecutora
                    presFed_Generico = presFedGFO.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(presFedGFO.Sum()) + Convert.ToDecimal(RecFed);
                    presEst_Generico = presEstGFO.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(presEstGFO.Sum()) + Convert.ToDecimal(RecEst);
                }
                else // Cuando es un NEW Registro de GFO
                {

                    // Obtiene el Total de los GFO (S ó U)
                    var presFedGFO = db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == IdAnio
                                                                    && item.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                                                                    && item.Fk_IdActividadXAccion == null
                                                                    && item.Fk_IdAccionXCampania == null
                                                                    && item.Fk_IdCampania__UE == null
                                                                    && item.esGastoU == esGastoU
                                                                    && item.BienOServ.porcentajeMaximo == 2)
                                                        .Select(item => item.RecFed);

                    var presEstGFO = db.ProgramaAnualAdqs.Where(item => item.Fk_IdAnio__SIS == IdAnio
                                                                    && item.Fk_IdUnidadEjecutora__UE == _Fk_IdUnidadEjecutora
                                                                    && item.Fk_IdActividadXAccion == null
                                                                    && item.Fk_IdAccionXCampania == null
                                                                    && item.Fk_IdCampania__UE == null
                                                                    && item.esGastoU == esGastoU
                                                                    && item.BienOServ.porcentajeMaximo == 2)
                                                        .Select(item => item.RecEst);

                    // Obtiene el total de los Gastos Genéricos 2%, realizado por la Instancia Ejecutora
                    presFed_Generico = presFedGFO.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(presFedGFO.Sum()) + Convert.ToDecimal(RecFed);
                    presEst_Generico = presEstGFO.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(presEstGFO.Sum()) + Convert.ToDecimal(RecEst);
                }
                if (esGastoU == false) // GASTO S
                {
                    var RF_Anual = (from PP in db.ProyectoPresupuestoes
                                    join PA in db.ProyectoAutorizadoes on PP.Fk_IdProyectoAutorizado equals PA.Pk_IdProyectoAutorizado
                                    join SC in db.SubComponentes on PA.Fk_IdSubComponente__SIS equals SC.Pk_IdSubComponente
                                    join I in db.Incentivoes on SC.Fk_IdIncentivo__SIS equals I.Pk_IdIncentivo
                                    join C in db.Componentes on I.Fk_IdComponente equals C.Pk_IdComponente
                                    join P in db.Programas on C.Fk_IdPrograma__SIS equals P.Pk_IdPrograma
                                    where PP.Fk_IdAnio == IdAnio && PP.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora && P.Pk_IdPrograma == 1
                                    select PP.RF_Anual);

                    var RE_Anual = (from PP in db.ProyectoPresupuestoes
                                    join PA in db.ProyectoAutorizadoes on PP.Fk_IdProyectoAutorizado equals PA.Pk_IdProyectoAutorizado
                                    join SC in db.SubComponentes on PA.Fk_IdSubComponente__SIS equals SC.Pk_IdSubComponente
                                    join I in db.Incentivoes on SC.Fk_IdIncentivo__SIS equals I.Pk_IdIncentivo
                                    join C in db.Componentes on I.Fk_IdComponente equals C.Pk_IdComponente
                                    join P in db.Programas on C.Fk_IdPrograma__SIS equals P.Pk_IdPrograma
                                    where PP.Fk_IdAnio == IdAnio && PP.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora && P.Pk_IdPrograma == 1
                                    select PP.RE_Anual);

                    // Obtiene el total del presupuesto asignado que puedo utilizar como Gastos Genéricos (2 %) de la Instancia Ejecutora
                    presFed_ByUnidadEjecutora = RF_Anual.Count() == null ? 0 : Convert.ToDecimal(RF_Anual.Sum()) * Convert.ToDecimal(0.02);
                    presEst_ByUnidadEjecutora = RE_Anual.Count() == null ? 0 : Convert.ToDecimal(RE_Anual.Sum()) * Convert.ToDecimal(0.02);
                }
                else // GASTO U
                {
                    var RF_Anual = (from PP in db.ProyectoPresupuestoes
                                    join PA in db.ProyectoAutorizadoes on PP.Fk_IdProyectoAutorizado equals PA.Pk_IdProyectoAutorizado
                                    join SC in db.SubComponentes on PA.Fk_IdSubComponente__SIS equals SC.Pk_IdSubComponente
                                    join I in db.Incentivoes on SC.Fk_IdIncentivo__SIS equals I.Pk_IdIncentivo
                                    join C in db.Componentes on I.Fk_IdComponente equals C.Pk_IdComponente
                                    join P in db.Programas on C.Fk_IdPrograma__SIS equals P.Pk_IdPrograma
                                    where PP.Fk_IdAnio == IdAnio && PP.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora && P.Pk_IdPrograma == 38
                                    select PP.RF_Anual);

                    var RE_Anual = (from PP in db.ProyectoPresupuestoes
                                    join PA in db.ProyectoAutorizadoes on PP.Fk_IdProyectoAutorizado equals PA.Pk_IdProyectoAutorizado
                                    join SC in db.SubComponentes on PA.Fk_IdSubComponente__SIS equals SC.Pk_IdSubComponente
                                    join I in db.Incentivoes on SC.Fk_IdIncentivo__SIS equals I.Pk_IdIncentivo
                                    join C in db.Componentes on I.Fk_IdComponente equals C.Pk_IdComponente
                                    join P in db.Programas on C.Fk_IdPrograma__SIS equals P.Pk_IdPrograma
                                    where PP.Fk_IdAnio == IdAnio && PP.Fk_IdUnidadEjecutora == _Fk_IdUnidadEjecutora && P.Pk_IdPrograma == 38
                                    select PP.RE_Anual);

                    // Obtiene el total del presupuesto asignado que puedo utilizar como Gastos Genéricos (2 %) de la Instancia Ejecutora
                    presFed_ByUnidadEjecutora = RF_Anual.Count() == null ? 0 : Convert.ToDecimal(RF_Anual.Sum()) * Convert.ToDecimal(0.02);
                    presEst_ByUnidadEjecutora = RE_Anual.Count() == null ? 0 : Convert.ToDecimal(RE_Anual.Sum()) * Convert.ToDecimal(0.02);
                }                
            }
        }

        /// <summary>
        /// Regresa '1' si el presupuesto que están reportando en el GFO no supera al presupuesto
        /// que está asignado a la Instancia Ejecutora
        /// </summary>
        /// <returns></returns>
        public string EsGastoGenericoValido()
        {
            string Novalido;
            int RF = RecFed != null ? 1 : 0;
            int RE = RecEst != null ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (presFed_Generico <= presFed_ByUnidadEjecutora)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RE == 1) // -- Estatal
            {
                if (presEst_Generico <= presEst_ByUnidadEjecutora)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            return "";
        }
    }

    /// <summary>
    /// Valida si el recurso Asignado al BienoServ de los GFO, Es mayor o igual, a los recursos aportados por las campañas
    /// </summary>
    public class ValidaMontos_Autorizado_Solicitado_GFO
    {
        private DB_SENASICAEntities db;

        private int? Pk_IdProgramasGastosTransversales;
        private int? Fk_IdProgramaAnualAdq;
        private int? Fk_IdUnidadEjecutora__UE;
        private int Fk_IdBienOServ__SIS;
        private bool? esGastoU;
        private decimal? Monto;

        private decimal presFedAutorizado;
        private decimal presEstAutorizado;
        private decimal presProAutorizado;

        private decimal _presFed_Sol;
        private decimal _presEst_Sol;
        private decimal _presPro_Sol;

        private int _RF;
        private int _RE;
        private int _RP;

        public ValidaMontos_Autorizado_Solicitado_GFO(int? Pk_IdProgramasGastosTransversales, int? Fk_IdProgramaAnualAdq, int? Fk_IdUnidadEjecutora__UE, int Fk_IdBienOServ__SIS, bool? esGastoU, decimal? Monto)
        {
            db = new DB_SENASICAEntities();

            this.Pk_IdProgramasGastosTransversales = Pk_IdProgramasGastosTransversales;
            this.Fk_IdProgramaAnualAdq = Fk_IdProgramaAnualAdq;
            this.Fk_IdUnidadEjecutora__UE = Fk_IdUnidadEjecutora__UE;
            this.Fk_IdBienOServ__SIS = Fk_IdBienOServ__SIS;
            this.esGastoU = esGastoU;
            this.Monto = Monto;

            //obtiene los presupuestos Federal, Estatal y Propios Autorizado en el BienOServ Transversal
            var _qFed = db.ProgramaAnualAdqs.Where(item => item.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                                            && item.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                                            && item.Fk_IdCampania__UE == null
                                                            && item.RecFed != null
                                                            && item.esGastoU == esGastoU)
                                            .Select(item => item.RecFed);

            var _qEst = db.ProgramaAnualAdqs.Where(item => item.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                                            && item.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                                            && item.Fk_IdCampania__UE == null
                                                            && item.RecEst != null
                                                            && item.esGastoU == esGastoU)
                                            .Select(item => item.RecEst);

            var _qPro = db.ProgramaAnualAdqs.Where(item => item.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                                            && item.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                                            && item.Fk_IdCampania__UE == null
                                                            && item.RecProp != null
                                                            && item.esGastoU == esGastoU)
                                            .Select(item => item.RecProp);

            presFedAutorizado = _qFed.Count() == 0 ? 0 : Convert.ToDecimal(_qFed.Sum());
            presEstAutorizado = _qEst.Count() == 0 ? 0 : Convert.ToDecimal(_qEst.Sum());
            presProAutorizado = _qPro.Count() == 0 ? 0 : Convert.ToDecimal(_qPro.Sum());

            //obtiene los presupuestos Federal, Estatal y Propios Solicitado por el BienOServ Transversal
            if (Pk_IdProgramasGastosTransversales > 0) // Cuando es un UPDATE del Registro de GFO
            {
                var qGFO_Fed = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PGT.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                     && PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecFed != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);

                var qGFO_Est = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PGT.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                     && PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecEst != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);

                var qGFO_Pro = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PGT.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                     && PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecProp != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);


                var RF = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecFed).First();
                var RE = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecEst).First();
                var RP = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecProp).First();

                _RF = RF != null ? 1 : 0;
                _RE = RE != null ? 1 : 0;
                _RP = RP != null ? 1 : 0;

                if (_RF == 1)
                {
                    _presFed_Sol = qGFO_Fed.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Fed.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RE == 1)
                {
                    _presEst_Sol = qGFO_Est.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Est.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RP == 1)
                {
                    _presPro_Sol = qGFO_Pro.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Pro.Sum()) + Convert.ToDecimal(Monto);
                }
            }
            else // Cuando es un NEW Registro de GFO
            {
                var qGFO_Fed = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecFed != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);

                var qGFO_Est = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecEst != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);

                var qGFO_Pro = from PGT in db.ProgramasGastosTransversales
                               join PAA in db.ProgramaAnualAdqs on PGT.Fk_IdProgramaAnualAdq equals PAA.Pk_IdProgramaAnualAdq
                               where (PAA.Fk_IdUnidadEjecutora__UE == Fk_IdUnidadEjecutora__UE
                                     && PAA.Fk_IdBienOServ__SIS == Fk_IdBienOServ__SIS
                                     && PAA.Fk_IdCampania__UE == null
                                     && PAA.RecProp != null
                                     && PAA.esGastoU == esGastoU)
                               select (PGT.Monto);


                var RF = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecFed).First();
                var RE = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecEst).First();
                var RP = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecProp).First();

                _RF = RF != null ? 1 : 0;
                _RE = RE != null ? 1 : 0;
                _RP = RP != null ? 1 : 0;

                if (_RF == 1)
                {
                    _presFed_Sol = qGFO_Fed.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Fed.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RE == 1)
                {
                    _presEst_Sol = qGFO_Est.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Est.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RP == 1)
                {
                    _presPro_Sol = qGFO_Pro.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(qGFO_Pro.Sum()) + Convert.ToDecimal(Monto);
                }
            }
        }
        /// <summary>
        /// Regresa 'Valido' si el presupuesto que están reportando en el GFO no supera al Autorizado
        /// que está asignado al BienOServ
        /// </summary>
        /// <returns></returns>
        public string EsGastoValido()
        {
            string Novalido;
            int RF = _RF == 1 ? 1 : 0;
            int RE = _RE == 1 ? 1 : 0;
            int RP = _RP == 1 ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (_presFed_Sol <= presFedAutorizado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RE == 1) // -- Estatal
            {
                if (_presEst_Sol <= presEstAutorizado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RP == 1) // Productor
            {
                if (_presPro_Sol <= presProAutorizado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            return "";
        }
    }

    /// <summary>
    /// 2. Para la campaña que se registre, debe tener recurso Asignado
    /// 3. No debe sobrepasar el recurso Asignado a la campaña
    /// </summary>    
    public class ValidaMontos_DetalleGFO
    {
        private DB_SENASICAEntities db;

        private int? Pk_IdProgramasGastosTransversales;
        private int? Fk_IdProgramaAnualAdq;
        private int Fk_IdProyectoPresupuesto;
        private decimal? Monto;

        private int _RF;
        private int _RE;
        private int _RP;

        //Presupuesto usuado para gastos fijos de operación
        private decimal _presFedGFO;
        private decimal _presEstGFO;
        private decimal _presProGFO;

        private decimal presFedReportado;
        private decimal presEstReportado;
        private decimal presProReportado;

        private decimal? presFed_ByProyecto;
        private decimal? presEst_ByProyecto;
        private decimal? presPro_ByProyecto;

        public ValidaMontos_DetalleGFO(int? Pk_IdProgramasGastosTransversales, int? Fk_IdProgramaAnualAdq, int Fk_IdProyectoPresupuesto, decimal? Monto)
        {
            db = new DB_SENASICAEntities();
            this.Pk_IdProgramasGastosTransversales = Pk_IdProgramasGastosTransversales;
            this.Fk_IdProgramaAnualAdq = Fk_IdProgramaAnualAdq;
            this.Fk_IdProyectoPresupuesto = Fk_IdProyectoPresupuesto;
            this.Monto = Monto;


            var IdCampania = db.Campanias.Where(item => item.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.Pk_IdCampania);
            
            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania.First()));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(Convert.ToInt32(IdCampania.First())))
            {

                //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                var _qReportadoFed = db.ProgramaAnualAdqs.Where(item => item.Campania.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecFed);
                var _qReportadoEst = db.ProgramaAnualAdqs.Where(item => item.Campania.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecEst);
                var _qReportadoPro = db.ProgramaAnualAdqs.Where(item => item.Campania.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecProp);

                presFedReportado = _qReportadoFed.Count() == null ? 0 : Convert.ToDecimal(_qReportadoFed.Sum());
                presEstReportado = _qReportadoEst.Count() == null ? 0 : Convert.ToDecimal(_qReportadoEst.Sum());
                presProReportado = _qReportadoPro.Count() == null ? 0 : Convert.ToDecimal(_qReportadoPro.Sum());
            }

            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(Convert.ToInt32(IdCampania.First())))
            {

                //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                var _qReportadoFed = db.ProgramaAnualAdq_Repro.Where(item => item.Campania_Repro.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecFed);
                var _qReportadoEst = db.ProgramaAnualAdq_Repro.Where(item => item.Campania_Repro.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecEst);
                var _qReportadoPro = db.ProgramaAnualAdq_Repro.Where(item => item.Campania_Repro.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(item => item.RecProp);

                presFedReportado = _qReportadoFed.Count() == null ? 0 : Convert.ToDecimal(_qReportadoFed.Sum());
                presEstReportado = _qReportadoEst.Count() == null ? 0 : Convert.ToDecimal(_qReportadoEst.Sum());
                presProReportado = _qReportadoPro.Count() == null ? 0 : Convert.ToDecimal(_qReportadoPro.Sum());
            }

            if (Pk_IdProgramasGastosTransversales > 0) // Cuando es un UPDATE del Registro de GFO
            {
                var _qGFOFed = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                                                            && it.ProgramaAnualAdq.RecFed != null)
                                                                .Select(it => it.Monto);

                var _qGFOEst = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                                                            && it.ProgramaAnualAdq.RecEst != null)
                                                                .Select(it => it.Monto);

                var _qGFOPro = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.Pk_IdProgramasGastosTransversales != Pk_IdProgramasGastosTransversales
                                                                            && it.ProgramaAnualAdq.RecProp != null)
                                                                .Select(it => it.Monto);

                var RF = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecFed).First();
                var RE = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecEst).First();
                var RP = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecProp).First();

                _RF = RF != null ? 1 : 0;
                _RE = RE != null ? 1 : 0;
                _RP = RP != null ? 1 : 0;

                if (_RF == 1)
                {
                    _presFedGFO = _qGFOFed.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOFed.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RE == 1)
                {
                    _presEstGFO = _qGFOEst.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOEst.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RP == 1)
                {
                    _presProGFO = _qGFOPro.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOPro.Sum()) + Convert.ToDecimal(Monto);
                }
            }
            else // Cuando es un NEW Registro de GFO
            {

                var _qGFOFed = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.ProgramaAnualAdq.RecFed != null)
                                                                .Select(it => it.Monto);

                var _qGFOEst = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.ProgramaAnualAdq.RecEst != null)
                                                                .Select(it => it.Monto);

                var _qGFOPro = db.ProgramasGastosTransversales.Where(it => it.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto
                                                                            && it.ProgramaAnualAdq.RecProp != null)
                                                                .Select(it => it.Monto);

                var RF = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecFed).First();
                var RE = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecEst).First();
                var RP = db.ProgramaAnualAdqs.Where(item => item.Pk_IdProgramaAnualAdq == Fk_IdProgramaAnualAdq).Select(item => item.RecProp).First();

                _RF = RF != null ? 1 : 0;
                _RE = RE != null ? 1 : 0;
                _RP = RP != null ? 1 : 0;

                if (_RF == 1)
                {
                    _presFedGFO = _qGFOFed.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOFed.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RE == 1)
                {
                    _presEstGFO = _qGFOEst.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOEst.Sum()) + Convert.ToDecimal(Monto);
                }
                if (_RP == 1)
                {
                    _presProGFO = _qGFOPro.Count() == 0 ? 0 + Convert.ToDecimal(Monto) : Convert.ToDecimal(_qGFOPro.Sum()) + Convert.ToDecimal(Monto);
                }
            }

            var RF_Anual = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RF_Anual);
            var RE_Anual = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RE_Anual);
            var RP_Anual = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RP_Anual);

            // Obtiene el total del presupuesto asignado al PP
            presFed_ByProyecto = RF_Anual.Count() == null ? 0 : Convert.ToDecimal(RF_Anual.First());
            presEst_ByProyecto = RE_Anual.Count() == null ? 0 : Convert.ToDecimal(RE_Anual.First());
            presPro_ByProyecto = RP_Anual.Count() == null ? 0 : Convert.ToDecimal(RP_Anual.First());


        }

        /// <summary>
        /// Regresa '1' si el presupuesto que están reportando en el GFO no supera al presupuesto
        /// que está asignado a la Instancia Ejecutora
        /// </summary>
        /// <returns></returns>
        public string EsGastoGenericoValido()
        {
            string Novalido;
            int RF = _RF == 1 ? 1 : 0;
            int RE = _RE == 1 ? 1 : 0;
            int RP = _RP == 1 ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (_presFedGFO + presFedReportado <= presFed_ByProyecto)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RE == 1) // -- Estatal
            {
                if (_presEstGFO + presEstReportado <= presEst_ByProyecto)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RP == 1) // Productor
            {
                if (_presProGFO + presProReportado <= presPro_ByProyecto)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            return "";
        }
    }

    /// <summary>
    /// --------------- CAMPAÑAS SIN REPROGRAMAR--------------------
    /// 1. Para dar de alta un Gasto, de acuerdo al tipo de recurso que se seleccione, la campaña debe tener recurso asignado
    /// 2. de lo contrario no se registrará en el sistema
    /// 3. El recurso que se registrará, no debe sobrepasar el recurso Asignado a la campaña
    /// </summary>
    
    // ======================================================= PLANEACIÓN --> PROGRAMAS DE TRABAJO (S, U) ===============================================================
    public class ValidaMontos_GRC
    {
        private DB_SENASICAEntities db;

        private int? Pk_IdProgramaAnualAdq;
        private int IdCampania;
        private decimal? RecFed;
        private decimal? RecEst;
        private decimal? RecProp;

        private decimal presFedReportado;
        private decimal presEstReportado;
        private decimal presProReportado;

        private decimal presFedAsignado;
        private decimal presEstAsignado;
        private decimal presProAsignado;

        private decimal presFedGFO;
        private decimal presEstGFO;
        private decimal presProGFO;

        public ValidaMontos_GRC(int? Pk_IdProgramaAnualAdq, int IdCampania, decimal? RecFed, decimal? RecEst, decimal? RecProp)
        {

            db = new DB_SENASICAEntities();
            this.IdCampania = IdCampania;
            this.Pk_IdProgramaAnualAdq = Pk_IdProgramaAnualAdq;
            this.RecFed = RecFed;
            this.RecEst = RecEst;
            this.RecProp = RecProp;

            int? IdProyectoPresupuesto = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => item.Fk_IdProyectoPresupuesto)
                                                    .First();

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaSin_Repro(IdCampania))
                {
                if (Pk_IdProgramaAnualAdq > 0) // Cuando es un UPDATE del Registro de GRC
                {
                    //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                    var _qReportadoFed = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecFed);
                    var _qReportadoEst = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecEst);
                    var _qReportadoPro = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecProp);

                    presFedReportado = _qReportadoFed.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(_qReportadoFed.Sum()) + Convert.ToDecimal(RecFed);
                    presEstReportado = _qReportadoEst.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(_qReportadoEst.Sum()) + Convert.ToDecimal(RecEst);
                    presProReportado = _qReportadoPro.Count() == null ? 0 + Convert.ToDecimal(RecProp) : Convert.ToDecimal(_qReportadoPro.Sum()) + Convert.ToDecimal(RecProp);
                }
                else // Cuando es un NEW Registro de GRC
                {
                    //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                    var _qReportadoFed = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecFed);
                    var _qReportadoEst = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecEst);
                    var _qReportadoPro = db.ProgramaAnualAdqs.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecProp);

                    presFedReportado = _qReportadoFed.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(_qReportadoFed.Sum()) + Convert.ToDecimal(RecFed);
                    presEstReportado = _qReportadoEst.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(_qReportadoEst.Sum()) + Convert.ToDecimal(RecEst);
                    presProReportado = _qReportadoPro.Count() == null ? 0 + Convert.ToDecimal(RecProp) : Convert.ToDecimal(_qReportadoPro.Sum()) + Convert.ToDecimal(RecProp);
                }

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
        public string EsGasto_CampaniaValida()
        { 
            string Novalido;
            int RF = RecFed != null ? 1 : 0;
            int RE = RecEst != null ? 1 : 0;
            int RP = RecProp != null ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (presFedReportado + presFedGFO <= presFedAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RE == 1) // -- Estatal
            {
                if (presEstReportado + presEstGFO <= presEstAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RP == 1) // Productor
            {
                if (presProReportado + presProGFO <= presProAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            return "";
        }
    }
    /// <summary>
    /// --------------- CAMPAÑAS EN REPROGRAMACIÓN --------------------
    /// 1. Para dar de alta un Gasto, de acuerdo al tipo de recurso que se seleccione, la campaña debe tener recurso asignado
    /// 2. de lo contrario no se registrará en el sistema
    /// 3. El recurso que se registrará, no debe sobrepasar el recurso Asignado a la campaña
    /// </summary>
    public class ValidaMontos_GRC_EnRepro
    {
        private DB_SENASICAEntities db;

        private int? Pk_IdProgramaAnualAdq;
        private int IdCampania;
        private decimal? RecFed;
        private decimal? RecEst;
        private decimal? RecProp;

        private decimal presFedReportado;
        private decimal presEstReportado;
        private decimal presProReportado;

        private decimal presFedAsignado;
        private decimal presEstAsignado;
        private decimal presProAsignado;

        private decimal presFedGFO;
        private decimal presEstGFO;
        private decimal presProGFO;

        public ValidaMontos_GRC_EnRepro(int? Pk_IdProgramaAnualAdq, int IdCampania, decimal? RecFed, decimal? RecEst, decimal? RecProp)
        {

            db = new DB_SENASICAEntities();
            this.IdCampania = IdCampania;
            this.Pk_IdProgramaAnualAdq = Pk_IdProgramaAnualAdq;
            this.RecFed = RecFed;
            this.RecEst = RecEst;
            this.RecProp = RecProp;

            int? IdProyectoPresupuesto = db.Campanias.Where(item => item.Pk_IdCampania == IdCampania)
                                                    .Select(item => item.Fk_IdProyectoPresupuesto)
                                                    .First();

            var StatusActual = Senasica.GetStatusActualCampania(Convert.ToInt32(IdCampania));
            if (StatusActual == FuncionesAuxiliares.GetStatusCampaniaEn_Repro(IdCampania))
            {
                if (Pk_IdProgramaAnualAdq > 0) // Cuando es un UPDATE del Registro de GRC
                {
                    //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                    var _qReportadoFed = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecFed);
                    var _qReportadoEst = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecEst);
                    var _qReportadoPro = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania && item.Pk_IdProgramaAnualAdq != Pk_IdProgramaAnualAdq).Select(item => item.RecProp);

                    presFedReportado = _qReportadoFed.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(_qReportadoFed.Sum()) + Convert.ToDecimal(RecFed);
                    presEstReportado = _qReportadoEst.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(_qReportadoEst.Sum()) + Convert.ToDecimal(RecEst);
                    presProReportado = _qReportadoPro.Count() == null ? 0 + Convert.ToDecimal(RecProp) : Convert.ToDecimal(_qReportadoPro.Sum()) + Convert.ToDecimal(RecProp);
                }
                else // Cuando es un NEW Registro de GRC
                {
                    //obtiene los presupuestos Federal, Estatal y Propios solicitados en la campaña
                    var _qReportadoFed = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecFed);
                    var _qReportadoEst = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecEst);
                    var _qReportadoPro = db.ProgramaAnualAdq_Repro.Where(item => item.Fk_IdCampania__UE == IdCampania).Select(item => item.RecProp);

                    presFedReportado = _qReportadoFed.Count() == null ? 0 + Convert.ToDecimal(RecFed) : Convert.ToDecimal(_qReportadoFed.Sum()) + Convert.ToDecimal(RecFed);
                    presEstReportado = _qReportadoEst.Count() == null ? 0 + Convert.ToDecimal(RecEst) : Convert.ToDecimal(_qReportadoEst.Sum()) + Convert.ToDecimal(RecEst);
                    presProReportado = _qReportadoPro.Count() == null ? 0 + Convert.ToDecimal(RecProp) : Convert.ToDecimal(_qReportadoPro.Sum()) + Convert.ToDecimal(RecProp);
                }

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
        public string EsGasto_CampaniaValida()
        {
            string Novalido;
            int RF = RecFed != null ? 1 : 0;
            int RE = RecEst != null ? 1 : 0;
            int RP = RecProp != null ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (presFedReportado + presFedGFO <= presFedAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RE == 1) // -- Estatal
            {
                if (presEstReportado + presEstGFO <= presEstAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            if (RP == 1) // Productor
            {
                if (presProReportado + presProGFO <= presProAsignado)
                {
                    Novalido = "Valido";
                }
                else
                {
                    Novalido = "NoValido";
                }
                return Novalido;
            }

            return "";
        }
    }

}