
namespace Vinculacion.Domain.Entities
{
    public class SubidaDetalle
    {
        public decimal SubidaId { get; set; }

        public string ColumnaExcel { get; set; } = null!;

        public string ColumnaType { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
