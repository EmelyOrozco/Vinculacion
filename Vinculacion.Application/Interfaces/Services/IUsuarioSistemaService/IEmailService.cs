
using System.Threading.Tasks;

namespace Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string to, string asunto, string body);
    }
}
