
namespace Vinculacion.Domain.Entities
{
    public class CharlaVinculacion
    {
       public decimal CharlaParticipanteID { get; set; }
       public decimal CharlaID { get; set; }
       
       public string Correo { get; set; }
       
       public string Matricula { get; set; }

       public string Carrera { get; set; }

       public decimal Calificacion { get; set; }
        
       public string Aprendizaje { get; set; }
        
       public bool DeseaMasCharlas { get; set; }
       
       public string Observaciones { get; set; }
        
       public DateTime FechaRegistro { get; set; }
    }
}
