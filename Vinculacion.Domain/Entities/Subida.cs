
namespace Vinculacion.Domain.Entities
{
    public class Subida
    {
        public decimal SubidaId { get; set; }

        public string SProcedure { get; set; } = null!;

        public string UserDefinedType { get; set; } = null!;

        public string Parametro { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
