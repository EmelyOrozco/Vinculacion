

namespace Vinculacion.Application.Dtos
{
    public class ArchivoDescargaDto
    {
        public byte[] Contenido { get; set; } = default!;
        public string NombreArchivo { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
