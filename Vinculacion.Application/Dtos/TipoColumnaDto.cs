

namespace Vinculacion.Application.Dtos
{
    public class TipoColumnaDto
    {
        public string NombreColumna { get; set; } = string.Empty;
        public string TipoDato { get; set; } = string.Empty;
        public bool EsNullable { get; set; }
    }
}

