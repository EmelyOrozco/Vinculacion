using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Vinculacion.Application.Enums;

namespace Vinculacion.Application.Dtos
{
    public class SubidaExcelRequest
    {
        [Required]

        public TipoSubida TipoSubida { get; set; }

        public decimal ContextoId { get; set; }
        public IFormFile Archivo { get; set; } = default!;
    }
}
