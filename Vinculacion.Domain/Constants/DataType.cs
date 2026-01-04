
namespace Vinculacion.Domain.Constants
{
    public static class DataType
    {

        public const string ObtenerProcedures = @"
        SELECT 
            name
        FROM 
            sys.procedures
        ORDER BY 
            name
            ";

        public const string ObtenerTipos = @"
        SELECT name
        FROM sys.table_types
        ORDER BY name;
            ";

        public const string ObtnerColumnasTipos = @"
        SELECT
        c.name AS NombreColumna
        FROM
            sys.table_types tt
        INNER JOIN
            sys.columns c ON tt.type_table_object_id = c.object_id
        INNER JOIN
            sys.types t ON c.user_type_id = t.user_type_id
        WHERE
            tt.name = @NombreTipo
        ORDER BY
            c.name
    ";

        public const string ObtenerTipoDatoColumnaTipo = @"
        SELECT
            c.name AS NombreColumna,
            t.name AS TipoDato,
            c.is_nullable AS EsNullable
        FROM
            sys.table_types tt
        INNER JOIN
            sys.columns c ON tt.type_table_object_id = c.object_id
        INNER JOIN
            sys.types t ON c.user_type_id = t.user_type_id
        WHERE
            tt.name = @NombreTipo
    ";
    }
}
