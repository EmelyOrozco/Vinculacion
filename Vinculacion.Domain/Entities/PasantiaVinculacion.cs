
namespace Vinculacion.Domain.Entities
{
    public class PasantiaVinculacion
    {
        public decimal PasantiaVinculacionId { get; set; }
        public string NombreEstudiante { get; set; }

        public string Matricula { get; set; }

        public string CorreoEstudiante { get; set; }

        public string Cedula { get; set; }

        public string EmpresaNombre { get; set; }

        public string TelefonoEmpresa { get; set; }

        public string DireccionEmpresa { get; set; }

        public decimal EstatusId { get; set; }

        public string NombreDepartamento { get; set; }

        public string ResponsableEmpresa { get; set; }

        public string CorreoResponsable { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
