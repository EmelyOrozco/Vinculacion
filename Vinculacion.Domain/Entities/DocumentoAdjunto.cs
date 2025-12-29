namespace Vinculacion.Domain.Entities
{
    public class DocumentoAdjunto
    {
        public decimal DocumentoAdjuntoID { get; set; }

        public decimal? ProyectoID { get; set; }
        public decimal? ActividadID { get; set; }

        public string NombreOriginal { get; set; } = null!;
        public string NombreFisico { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string TipoMime { get; set; } = null!;
        public long Tamano { get; set; }

        public DateTime FechaSubida { get; set; }
        public decimal UsuarioID { get; set; }
    }
}