using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Vinculacion.Application.Dtos
{
    public class SubidaExcelRequest
    {
        [Required]
        public IFormFile Archivo { get; set; } = default!;
    }
}
