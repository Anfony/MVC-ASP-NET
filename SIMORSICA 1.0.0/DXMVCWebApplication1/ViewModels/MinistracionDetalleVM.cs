namespace DXMVCWebApplication1.ViewModels
{
    public class MinistracionDetalleVM
    {
        public int Pk_IdMinistracionDetalle { get; set; }
        public int Fk_IdMinistracionesXComite { get; set; }
        public int Fk_IdCampania { get; set; }
        public decimal ImporteDetalle { get; set; }
    }
}