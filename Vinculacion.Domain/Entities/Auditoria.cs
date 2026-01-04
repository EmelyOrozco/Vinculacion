
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Vinculacion.Domain.Entities
{
    public class Auditoria
    {
        public decimal AuditoriaID { get; set; }
        public decimal UsuarioID { get; set; }

        public DateTime FechaHora { get; set; }

        public string Entidad { get; set; } = null!;

        public string Accion { get; set; } = null!;

        public string DetalleAntes { get; set; } = null!;

        public string DetalleDespues { get; set; } = null!;
    }
}
