using Vinculacion.Application.Interfaces.Services.IFileStorageService;

namespace Vinculacion.API.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<(string ruta, string nombreFisico)> SaveAsync(
            IFormFile file,
            string carpeta)
        {
            var basePath = Path.Combine(_env.WebRootPath, "uploads", carpeta);

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var extension = Path.GetExtension(file.FileName);
            var nombreFisico = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(basePath, nombreFisico);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var rutaRelativa = Path.Combine("uploads", carpeta, nombreFisico)
                .Replace("\\", "/");

            return (rutaRelativa, nombreFisico);
        }
    }
}
