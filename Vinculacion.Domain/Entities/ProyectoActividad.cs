namespace Vinculacion.Domain.Entities
{
    public class ProyectoActividad
    {
        public decimal ProyectoID { get; set; }
        public decimal ActividadID { get; set; }
        public ProyectoVinculacion Proyecto { get; set; }
        public ActividadVinculacion Actividad { get; set; }
    }
}
