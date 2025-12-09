using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class AuthService: IAuthService
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;
        public AuthService(IUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string codigoEmpleado, string password)
        {
            var result = await _usersService.ValidateUserAsync(codigoEmpleado, password);

            if(!result.IsSuccess || result.Data == null)
                return null;

            var user = result.Data;
            var secretKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Nombre", user.NombreCompleto),
                new Claim("Username", user.Usuario)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                audience: _configuration["Jwt:Audience"],
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
