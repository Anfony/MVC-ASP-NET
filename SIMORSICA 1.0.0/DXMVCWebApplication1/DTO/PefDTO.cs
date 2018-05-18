namespace DXMVCWebApplication1.DTO
{
    public class PefDTO
    {
        public string Estado { get; set; }

        public decimal? Tran_SistemaInformatico { get; set; }
        public decimal? Tran_Capacitacion { get; set; }
        public decimal? Tran_Divulgacion { get; set; }
        public decimal? Tran_EmerSanitarias { get; set; }
        public decimal? Tran_Total { get; set; }

        public decimal? Comp_Curantenaria_InspVig { get; set; }
        public decimal? Comp_Fitozoosanitaria { get; set; }
        public decimal? Comp_Curantenaria_Vig { get; set; }
        public decimal? Comp_Inocuidad { get; set; }
        public decimal? Comp_Total { get; set; }

        public decimal? GtoInversion { get; set; }
        public decimal? GtoOperacion { get; set; }
        public decimal? PresAutorizado { get; set; }
    }
}