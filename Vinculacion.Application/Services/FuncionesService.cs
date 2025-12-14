using System.Text.RegularExpressions;

namespace Vinculacion.Application.Services
{
    public static class FuncionesService
    {
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
    }
}
