namespace Vinculacion.Domain.Entities
{
    public class Carrera
    {
        public decimal CarreraID { get; set; }
        public decimal EscuelaID { get; set; }
        public string Descripcion { get; set; } = null!;
        public Escuela Escuela { get; set; } = null!;
    }
}
