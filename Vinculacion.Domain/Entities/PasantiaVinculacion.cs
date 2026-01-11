
namespace Vinculacion.Domain.Entities
{
    public class PasantiaVinculacion
    {
        public decimal PasantiaVinculacionId { get; set; }

        public decimal PasantiaID { get; set; }
        public required string NombreEstudiante { get; set; }

        public required string Matricula { get; set; }

        public required string CorreoEstudiante { get; set; }

        public required string Cedula { get; set; }

        public string? EmpresaNombre { get; set; }

        public string? TelefonoEmpresa { get; set; }

        public string? DireccionEmpresa { get; set; }

        public required decimal EstatusId { get; set; }

        public string? NombreDepartamento { get; set; }

        public string? ResponsableEmpresa { get; set; }

        public string? CorreoResponsable { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
