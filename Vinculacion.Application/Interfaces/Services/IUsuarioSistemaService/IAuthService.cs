namespace Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(string codigoEmpleado, string password);
    }
}
