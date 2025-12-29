
namespace Vinculacion.Domain.Constants
{
    public class StoredProcedureParameter
    {
        public required string Name { get; set; }
        public required object Value { get; set; }
        public bool IsStructured { get; set; } = false;
        public string? UdttName { get; set; }
    }
}
