namespace Vinculacion.Domain.Entities
{
    public class Escuela
    {
        public decimal EscuelaID { get; set; }
        public decimal FacultadID { get; set; }
        public string Descripcion { get; set; } = null!;
        public Facultad Facultad { get; set; } = null!;
    }
}
