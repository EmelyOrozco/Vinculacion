

namespace Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion
{
    public class EstudianteDto
    {
        public decimal? RecintoID { get; set; }

        public decimal? EscuelaID { get; set; }

        public decimal? CarreraID { get; set; }

        public string NombreCompleto { get; set; }

        public string Correo { get; set; }

        public string TelefonoContacto { get; set; }

        public decimal? TipoRelacion { get; set; }

        public string Matricula { get; set; }
    }
}
