namespace Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas
{
    public class ActividadSubtareaCreateDto
    {
        public decimal EstadoID { get; set; }
        public string TituloSubtarea { get; set; } 
        public string? Detalle { get; set; }
        public decimal Orden { get; set; }
        public DateTime? FechaCompletado { get; set; }
    }

}
