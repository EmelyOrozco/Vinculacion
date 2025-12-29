using Microsoft.AspNetCore.Http;

namespace Vinculacion.Application.Interfaces.Services.IFileStorageService
{
    public interface IFileStorageService
    {
        Task<(string ruta, string nombreFisico)> SaveAsync(
            IFormFile file,
            string carpeta
        );
    }
}
