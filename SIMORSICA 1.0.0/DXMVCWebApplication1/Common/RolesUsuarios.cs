namespace DXMVCWebApplication1.Common
{
    public class RolesUsuarios
    {
        // Roles
        public const string SUPERUSUARIO = "Superusuario";
        public const string COORDINADOR_CAMPANIA = "Coordinador de Campaña";
        public const string TECNICO_OPERATIVO = "Técnico Operativo";

        ////////////////////////////////////////////////////////////////////////////////////////////

        //USUARIOS DE SUPERVISIÓN
        public const string SUP_NACIONAL = "Supervisor Nacional";
        public const string SUP_REGIONAL = "Supervisor Regional";
        public const string SUP_ESTATAL = "Supervisor Estatal";

        public const string SUP_AUDITOR = "Auditor";

        public const string ESTATAL_PRES = "Presupuesto Estatal";
        public const string REPRESENTANTE_ESTATAL = "Representante Estatal";

        //UNIDADES RESPONSABLES
        public const string UR_INOCUIDAD = "Administrador de Unidad Responsable de Inocuidad";
        public const string UR_MOVILIZACION = "Administrador de Unidad Responsable de Movilizacion";
        public const string UR_ANIMAL = "Administrador de Unidad Responsable de Salud Animal";
        public const string UR_ACUICOLA_PESQUERA = "Administrador de Unidad Responsable de Sanidad Acuicola y Pesquera";
        public const string UR_VEGETAL = "Administrador de Unidad Responsable de Sanidad Vegetal";
        public const string UR_UPV = "Administrador de Unidad Responsable de la Unidad de Promocion y Vinculacion";

        // INSTANCIAS EJECUTORAS
        public const string IE_INOCUIDAD = "Administrador de Instancia Ejecutora de Inocuidad";
        public const string IE_MOVILIZACION = "Administrador de Instancia Ejecutora de Movilizacion";
        public const string IE_ANIMAL = "Administrador de Instancia Ejecutora de Salud Animal";
        public const string IE_ACUICOLA_PESQUERA = "Administrador de Instancia Ejecutora de Sanidad Acuicola y Pesquera";
        public const string IE_VEGETAL = "Administrador de Instancia Ejecutora de Sanidad Vegetal";

        public const string IE_PROGRAMAS_U = "Administrador Programas U";

        //Roles por PUESTO
        public const string PUESTO_AUXILIAR_ADMINISTRATIVO = "Auxiliar Administrativo";
        public const string PUESTO_AUXILIAR_CAMPO = "Auxiliar de Campo";
        public const string PUESTO_AUXILIAR_INFORMATICA = "Auxiliar de Informática";
        public const string PUESTO_COOR_ADMINISTRATIVO = "Coordinador Administrativo";
        public const string PUESTO_COOR_PROYECTO = "Coordinador de Proyecto";
        public const string PUESTO_GERENTE = "Gerente";
        public const string PUESTO_PROF_ADMI_ESTATAL_SISTEMAS_INFORMATICOS = "Profesional Administrador Estatal del Sistema Informático";
        public const string PUESTO_PROF_ADMINISTRATIVO = "Profesional Administrativo";
        public const string PUESTO_PROF_PROYECTO = "Profesional del Proyecto";
        public const string PUESTO_PROF_RESP_INFORMATICA = "Profesional Responsable de Informática";
        public const string PUESTO_PROF_TECNICO_CALIDAD_MEJORA_PROCESOS = "Profesional Técnico de Calidad y Mejora de Procesos";
        public const string PUESTO_PROF_TEC_CAPACITACION_DIVULGACION = "Profesional Técnico de Capacitación y Divulgación";
        public const string PUESTO_SECRETARIA = "Secretaria";
        public const string PUESTO_COOR_REGIONAL = "Coordinador Regional";

        //Roles Normalizados
        public const string ROL_AIE = "Administrador de Instancia Ejecutora";
        public const string ROL_AUR = "Administrador de Unidad Responsable";
        public const string ROL_REG = "Regional";
    }
}