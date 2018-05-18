namespace DXMVCWebApplication1.ViewModels
{
    public class CierreMensualVM
    {
        public int IdCierre { get; set; }
        public int IdCierreUpdate { get; set; }
        public int Fk_IdMes { get; set; }
        public int Fk_IdCampania { get; set; }
        public string Comentarios { get; set; }
        public string ComentariosUpdate { get; set; }
    }
}