
namespace Vinculacion.Domain.Entities
{
    public class SubidaCompleta
    {
        public decimal SubidaId { get; set; }

        public decimal TipoSubida { get; set; }

        public string Procedure { get; set; } = null!;

        public string UserDefinedType { get; set; } = null!;

        public string Parametro { get; set; } = null!;

        public IEnumerable<SubidaDetalle> Detalle { get; set; } = new List<SubidaDetalle>();
    }
}
