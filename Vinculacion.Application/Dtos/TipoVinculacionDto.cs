namespace Vinculacion.Application.Dtos
{
    public class TipoVinculacionDto
    {
        public decimal TipoVinculacionID { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Detalle { get; set; }
        public bool EsProyecto { get; set; }
    }
}
