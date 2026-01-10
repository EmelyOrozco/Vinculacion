namespace Vinculacion.Domain.Entities
{
    public class TipoVinculacion
    {
        public decimal TipoVinculacionID { get; set; }
        public string Descripcion { get; set; }
        public string? Detalle { get; set; }
        public bool EsProyecto { get; set; }
    }
}