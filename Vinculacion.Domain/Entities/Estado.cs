namespace Vinculacion.Domain.Entities
{
    public class Estado
    {
        public decimal EstadoID { get; set; }
        public string Descripcion { get; set; } = null!;
        public string TablaEstado { get; set; } = null!;
    }

}