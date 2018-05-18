using DXMVCWebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMVCWebApplication1.Negocio
{
    public class ValidaReintegroTesoFe
    {
        private DB_SENASICAEntities db;
        private int Pk_IdTesofe;
        private int IdIE;
        private int Fk_IdTipodeRecurso;
        private int Fk_IdTipoReembolso;
        private decimal Importe;
        private int IdAnio;

        private decimal ImporteTotal;
        private decimal ImporteDetalle;
        
        public ValidaReintegroTesoFe(int Pk_IdTesofe, int IdIE, int Fk_IdTipodeRecurso, int Fk_IdTipoReembolso, decimal Importe, int IdAnio)
        {
            db = new DB_SENASICAEntities();
            this.Pk_IdTesofe = Pk_IdTesofe;
            this.IdIE = IdIE;
            this.Fk_IdTipodeRecurso = Fk_IdTipodeRecurso;
            this.Fk_IdTipoReembolso = Fk_IdTipoReembolso;
            this.Importe = Importe;
            this.IdAnio = IdAnio;

            if (Pk_IdTesofe > 0)
            {
                var ImpTesofe = db.TESOFEs.Where(i => i.Pk_IdTesofe != Pk_IdTesofe && i.Fk_IdUnidadEjecutora == IdIE && i.Fk_IdTipodeRecurso == Fk_IdTipodeRecurso && i.Fk_IdTipoReembolso == Fk_IdTipoReembolso && i.Fk_IdAnio == IdAnio).Select(i => i.Importe);

                ImporteTotal = ImpTesofe.Count() == 0 ? Convert.ToDecimal(Importe) : Convert.ToDecimal(ImpTesofe.Sum()) + Convert.ToDecimal(Importe);
            }
            else
            {
                var ImpTesofe = db.TESOFEs.Where(i => i.Fk_IdUnidadEjecutora == IdIE && i.Fk_IdTipodeRecurso == Fk_IdTipodeRecurso && i.Fk_IdTipoReembolso == Fk_IdTipoReembolso && i.Fk_IdAnio == IdAnio).Select(i => i.Importe);

                ImporteTotal = ImpTesofe.Count() == 0 ? Convert.ToDecimal(Importe) : Convert.ToDecimal(ImpTesofe.Sum()) + Convert.ToDecimal(Importe);
            }

            // total del importe detalle
            var ImpDetalle = from TDetalle in db.TesofeDetalles
                             join tesofe in db.TESOFEs on TDetalle.Fk_IdTesofe equals tesofe.Pk_IdTesofe
                             where tesofe.Fk_IdUnidadEjecutora == IdIE && tesofe.Fk_IdTipodeRecurso == Fk_IdTipodeRecurso && tesofe.Fk_IdTipoReembolso == Fk_IdTipoReembolso && tesofe.Fk_IdAnio == IdAnio 
                             select TDetalle.ImporteDetalle;

            ImporteDetalle = ImpDetalle.Count() == 0 ? 0 : Convert.ToDecimal(ImpDetalle.Sum());
        }
        public string EsGastoValido()
        {
            string Novalido;
            //ESTA LOGIGA ESTA MAL, ANALIZAR TODOS LOS CASOS POSIBLES, POR EL MOMENTO NO ESTA FUNCIONANDO (LO COMENTE DE DONDE LO MANDO LLAMAR)

            if (0 == Pk_IdTesofe) // para crear registro nuevo
            {
                Novalido = "Valido";
                return Novalido;
            }

            if (ImporteDetalle == ImporteTotal) // cuando actualizo el registro y los origenes del reintegro
            {
                Novalido = "Valido";
                return Novalido;
            } 
            else
            {
                Novalido = "NoValido";
                return Novalido;
            }
        }
    }

    public class ValidaReintegroTesoFe_Detalle
    {
        private DB_SENASICAEntities db;

        private int Pk_IdTesofeDetalle;
        private int Fk_IdTesofe;
        private int Fk_IdProyectoPresupuesto;
        private decimal ImporteDetalle;
        private int IdAnio;

        private decimal IDetalle;
        private int T_Recurso;
        private int T_Reintegro;

        private decimal esFederal;
        private decimal esEstatal;
        private decimal esProductor;
        
        private decimal presFedAutorizado;
        private decimal presEstAutorizado;
        private decimal presProAutorizado;


        private int IdIE;
        private decimal ImporteTotal_TESOFE;
        private decimal ImporteTotal_TESOFE_Detalle;
        public ValidaReintegroTesoFe_Detalle(int Pk_IdTesofeDetalle, int Fk_IdTesofe, int Fk_IdProyectoPresupuesto, decimal ImporteDetalle, int IdAnio)
        {
            db = new DB_SENASICAEntities();

            this.Pk_IdTesofeDetalle = Pk_IdTesofeDetalle;
            this.Fk_IdTesofe = Fk_IdTesofe;
            this.Fk_IdProyectoPresupuesto = Fk_IdProyectoPresupuesto;
            this.ImporteDetalle = ImporteDetalle;
            this.IdAnio = IdAnio;

            T_Recurso = db.TESOFEs.Where(i => i.Pk_IdTesofe == Fk_IdTesofe).Select(i => i.Fk_IdTipodeRecurso).First();
            T_Reintegro = db.TESOFEs.Where(i => i.Pk_IdTesofe == Fk_IdTesofe).Select(i => i.Fk_IdTipoReembolso).First();
            IdIE = db.TESOFEs.Where(i => i.Pk_IdTesofe == Fk_IdTesofe).Select(i => i.Fk_IdUnidadEjecutora).First();

            if (Pk_IdTesofeDetalle > 0)
            {
                var Detalle = db.TesofeDetalles.Where(i => i.Pk_IdTesofeDetalle != Pk_IdTesofeDetalle && i.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto && i.TESOFE.Fk_IdTipodeRecurso == T_Recurso)
                                                .Select(i => i.ImporteDetalle);

                IDetalle = Detalle.Count() == 0 ? 0 + Convert.ToDecimal(ImporteDetalle) : Convert.ToDecimal(Detalle.Sum()) + Convert.ToDecimal(ImporteDetalle);
            }
            else
            {
                var Detalle = db.TesofeDetalles.Where(i => i.Fk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto && i.TESOFE.Fk_IdTipodeRecurso == T_Recurso)
                                                .Select(i => i.ImporteDetalle);

                IDetalle = Detalle.Count() == 0 ? 0 + Convert.ToDecimal(ImporteDetalle) : Convert.ToDecimal(Detalle.Sum()) + Convert.ToDecimal(ImporteDetalle);
            }
            
            // Obtiene el tipo de Recurso y el importe del Detalle
            if (T_Recurso == 1) // Federal
            {
                esFederal = IDetalle;
            }
            if (T_Recurso == 2) // Estatal
            {
                esEstatal = IDetalle;
            }
            if (T_Recurso == 3) // Productor
            {
                esProductor = IDetalle;
            }

            // tope para el importe de el proyecto que estoy intentando registrar
            var RF_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RF_Anual);
            var RE_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RE_Anual);
            var RP_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Pk_IdProyectoPresupuesto == Fk_IdProyectoPresupuesto).Select(i => i.RP_Anual);

            presFedAutorizado = RF_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RF_Autorizado.Sum());
            presEstAutorizado = RE_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RE_Autorizado.Sum());
            presProAutorizado = RP_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RP_Autorizado.Sum());

            //////  INICIO - SEGUNDA VALIDACIÓN /////////
            // Tope del importe TesoFe, no debo revasar este tipo de recurso
            var ImpTesofe = db.TESOFEs.Where(i => i.Fk_IdUnidadEjecutora == IdIE && i.Fk_IdTipodeRecurso == T_Recurso && i.Fk_IdTipoReembolso == T_Reintegro && i.Fk_IdAnio == IdAnio).Select(i => i.Importe);
            ImporteTotal_TESOFE = ImpTesofe.Count() == 0 ? 0 : Convert.ToDecimal(ImpTesofe.Sum());

            // total del importe detalle
            if (Pk_IdTesofeDetalle > 0)
            {
                var ImpDetalle = from TDetalle in db.TesofeDetalles
                                 join tesofe in db.TESOFEs on TDetalle.Fk_IdTesofe equals tesofe.Pk_IdTesofe
                                 where tesofe.Fk_IdUnidadEjecutora == IdIE && tesofe.Fk_IdTipodeRecurso == T_Recurso && tesofe.Fk_IdTipoReembolso == T_Reintegro && tesofe.Fk_IdAnio == IdAnio
                                        && TDetalle.Pk_IdTesofeDetalle != Pk_IdTesofeDetalle
                                 select TDetalle.ImporteDetalle;

                ImporteTotal_TESOFE_Detalle = ImpDetalle.Count() == 0 ? 0 + Convert.ToDecimal(ImporteDetalle) : Convert.ToDecimal(ImpDetalle.Sum()) + Convert.ToDecimal(ImporteDetalle);

            }
            else
            {
                var ImpDetalle = from TDetalle in db.TesofeDetalles
                                 join tesofe in db.TESOFEs on TDetalle.Fk_IdTesofe equals tesofe.Pk_IdTesofe
                                 where tesofe.Fk_IdUnidadEjecutora == IdIE && tesofe.Fk_IdTipodeRecurso == T_Recurso && tesofe.Fk_IdTipoReembolso == T_Reintegro && tesofe.Fk_IdAnio == IdAnio
                                 select TDetalle.ImporteDetalle;

                ImporteTotal_TESOFE_Detalle = ImpDetalle.Count() == 0 ? 0 + Convert.ToDecimal(ImporteDetalle) : Convert.ToDecimal(ImpDetalle.Sum()) + Convert.ToDecimal(ImporteDetalle);
            }
            //////  FIN - SEGUNDA VALIDACIÓN /////////
        }
        /// <summary>
        /// Regresa 'Valido' si el presupuesto que están reportando no supera al recurso Autorizado del proyecto
        /// </summary>
        /// <returns></returns>
        public string EsGastoValido()
        {
            string Novalido;
            int RF = T_Recurso == 1 ? 1 : 0;
            int RE = T_Recurso == 2 ? 1 : 0;
            int RP = T_Recurso == 3 ? 1 : 0;

            if (RF == 1) // Federal
            {
                if (esFederal <= presFedAutorizado && ImporteTotal_TESOFE_Detalle <= ImporteTotal_TESOFE)
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
                if (esEstatal <= presEstAutorizado && ImporteTotal_TESOFE_Detalle <= ImporteTotal_TESOFE)
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
                if (esProductor <= presProAutorizado && ImporteTotal_TESOFE_Detalle <= ImporteTotal_TESOFE)
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

    public class ValidaRegistroDuplicado
    {
        private DB_SENASICAEntities db;
        private int Pk_IdTesofe;
        private int IdIE;
        private int Fk_IdTipodeRecurso;
        private int? Fk_IdTipoReembolso;
        private decimal? Importe;
        private int IdAnio;

        private int TipoRecurso;
        private int TipoReintegro;

        private decimal presAnualFedAutorizado;
        private decimal presAnualEstAutorizado;
        private decimal presAnualProAutorizado;

        private int? IdTesofe;
        public ValidaRegistroDuplicado(int Pk_IdTesofe, int IdIE, int Fk_IdTipodeRecurso, int? Fk_IdTipoReembolso, decimal? Importe, int IdAnio)
        {
            db = new DB_SENASICAEntities();
            this.Pk_IdTesofe = Pk_IdTesofe;
            this.IdIE = IdIE;
            this.Fk_IdTipodeRecurso = Fk_IdTipodeRecurso;
            this.Fk_IdTipoReembolso = Fk_IdTipoReembolso;
            this.Importe = Importe;
            this.IdAnio = IdAnio;

            var TRecurso = db.TESOFEs.Where(i => i.Fk_IdUnidadEjecutora == IdIE && i.Fk_IdTipodeRecurso == Fk_IdTipodeRecurso && i.Fk_IdTipoReembolso == Fk_IdTipoReembolso && i.Fk_IdAnio == IdAnio )
                                        .Select(i => i.Fk_IdTipoReembolso);

            TipoReintegro = TRecurso.Count() == 0 ? 0 : Convert.ToInt32(TRecurso.First());

            var Id_Tesofe = db.TESOFEs.Where(i => i.Fk_IdUnidadEjecutora == IdIE && i.Fk_IdTipodeRecurso == Fk_IdTipodeRecurso && i.Fk_IdTipoReembolso == Fk_IdTipoReembolso && i.Fk_IdAnio == IdAnio)
                                        .Select(i => i.Pk_IdTesofe);
            IdTesofe = Id_Tesofe.Count() == 0 ? 0 : Id_Tesofe.First();

            // tope para el importe anual por comité
            var RF_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Fk_IdUnidadEjecutora == IdIE).Select(i => i.RF_Anual);
            var RE_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Fk_IdUnidadEjecutora == IdIE).Select(i => i.RE_Anual);
            var RP_Autorizado = db.ProyectoPresupuestoes.Where(i => i.Fk_IdUnidadEjecutora == IdIE).Select(i => i.RP_Anual);

            presAnualFedAutorizado = RF_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RF_Autorizado.Sum());
            presAnualEstAutorizado = RE_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RE_Autorizado.Sum());
            presAnualProAutorizado = RP_Autorizado.Count() == 0 ? 0 : Convert.ToDecimal(RP_Autorizado.Sum());
        }
        public string EsRegistroDuplicado()
        {
            string Novalido;

            if (TipoReintegro != Fk_IdTipoReembolso)
            {
                Novalido = "Valido";

                return Novalido;
            }

            if (TipoReintegro == Fk_IdTipoReembolso && IdTesofe == Pk_IdTesofe)
            {

                Novalido = "Valido";
                return Novalido;
            }

            else
            {
                Novalido = "NoValido";
                return Novalido;
            }
        }
    }
}