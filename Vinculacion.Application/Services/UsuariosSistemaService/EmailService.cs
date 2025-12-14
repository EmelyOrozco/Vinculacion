using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string to, string asunto, string body)
        {
            var clientEmail = new SmtpClient(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]))
            {
                Credentials = new NetworkCredential(
                    _configuration["Email:User"],
                    _configuration["Email:Password"]
                ),
                EnableSsl = bool.Parse(_configuration["Email:Ssl"])
            };

            var message = new MailMessage(_configuration["Email:User"], to, asunto, body);
            message.IsBodyHtml = true;

            await clientEmail.SendMailAsync(message);

            return true;
        }

    }
}
