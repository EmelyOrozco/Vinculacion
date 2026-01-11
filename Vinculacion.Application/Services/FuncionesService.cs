using System.Text.RegularExpressions;
using Vinculacion.Application.Enums;

namespace Vinculacion.Application.Services
{
    public static class FuncionesService
    {
        private static readonly string[] ExcelSignatures =
        {
            "D0CF11E0", // .xls
            "504B0304"  // .xlsx
        };

        public static bool ValidarIdentificacion(decimal? tipo, string? numero)
        {
            return tipo switch
            {
                (decimal)TipoIdentificacion.Cedula => ValidateCedula(numero),
                (decimal)TipoIdentificacion.Pasaporte => ValidatePassaport(numero),
                (decimal)TipoIdentificacion.RNC => ValidateRNC(numero),
                _ => false
            };
        }

        public static bool ValidateCedula(string? cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return false;
            }
            cedula = cedula.Replace("-", "").Trim();

            if (!Regex.IsMatch(cedula, @"^\d{11}$"))
            {
                return false;
            }

            if (cedula.Substring(0, 3) == "000")
            {
                return false;
            }

            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                int digito = cedula[i] - '0';
                int multiplicador = (i % 2 == 0) ? 1 : 2;

                int resultado = digito * multiplicador;

                if (resultado > 9)
                    resultado = (resultado / 10) + (resultado % 10);

                suma += resultado;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;

            return digitoVerificador == (cedula[10] - '0');
        }

        public static bool ValidatePassaport(string? pasaporte)
        {
            if (string.IsNullOrWhiteSpace(pasaporte))
                return false;

            pasaporte = pasaporte.Trim().ToUpper();

            if (!Regex.IsMatch(pasaporte, @"^[A-Z0-9]{6,9}$"))
                return false;

            return true;
        }

        public static bool ValidateRNC(string? rnc)
        {
            if (string.IsNullOrWhiteSpace(rnc))
                return false;

            rnc = rnc.Replace("-", "").Trim();

            if (!Regex.IsMatch(rnc, @"^\d{9}$"))
                return false;

            int[] peso = { 7, 9, 8, 6, 5, 4, 3, 2 };
            int suma = 0;

            for (int i = 0; i < 8; i++)
            {
                suma += (rnc[i] - '0') * peso[i];
            }

            int resto = suma % 11;
            int digitoVerificador;

            if (resto == 0)
                digitoVerificador = 2;
            else if (resto == 1)
                digitoVerificador = 1;
            else
                digitoVerificador = 11 - resto;

            if (digitoVerificador != (rnc[8] - '0'))
                return false;

            return true;
        }


        public static async Task<bool> IsExcelAsync( Stream stream, CancellationToken cancellationToken)
        {
            var header = new byte[4];

            stream.Position = 0;
            await stream.ReadExactlyAsync(header.AsMemory(0, 4), cancellationToken);
            stream.Position = 0;

            var hex = Convert.ToHexString(header);

            return ExcelSignatures.Any(sig => hex.StartsWith(sig));
        }

        public static bool DebeNotificar(double? dias)
        {
            return dias == 7 || dias == 3 || dias == 14 || dias < 0;
        }

        public static string ObtenerTitulo(string tipo, double? dias)
        {
            if (dias < 0)
                return $"{tipo} vencido";

            if (dias == 3)
                return $"{tipo} próximo a vencer (urgente)";

            return $"{tipo} próximo a vencer";
        }

        public static readonly string[] FechasDateTime =
        {
            "M/d/yyyy h:mm:ss tt",  
            "MM/dd/yyyy hh:mm:ss tt",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-ddTHH:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "M/d/yyyy"
        };

    }
}
