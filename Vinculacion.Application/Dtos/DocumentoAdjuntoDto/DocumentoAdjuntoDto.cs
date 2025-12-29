namespace Vinculacion.Application.Dtos.DocumentoAdjuntoDto
{
    public class DocumentoAdjuntoDto
    {
        public decimal DocumentoAdjuntoID { get; set; }
        public string NombreOriginal { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string TipoMime { get; set; } = null!;
        public DateTime FechaSubida { get; set; }
    }
}
