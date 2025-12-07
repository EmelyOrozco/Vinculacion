namespace Vinculacion.Domain.Entities
{
    public class ActorEmpresaClasificacion
    {
        public decimal ActorExternoID { get; set; }
        public decimal ClasificacionID { get; set; }
        public ActorExterno ActorExterno { get; set; }
        public ClasificacionEmpresa ClasificacionEmpresa { get; set; }
    }
}
