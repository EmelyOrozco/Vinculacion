using ClosedXML.Excel;
using System.Data;
using System.Globalization;
using Vinculacion.Application.Dtos;
using Vinculacion.Application.Enums;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Constants;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services
{
    public class SubidaService: ISubidaService
    {
        private readonly ISubidaDetalleRepository _subidaDetalleRepository;
        private readonly ISubidaRepository _subidaRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly IPasantiaService _pasantiaService;
        private readonly ICharlaService _charlaService;

        public SubidaService(ISubidaDetalleRepository subidaDetalleRepository,
            ISubidaRepository subidaRepository, 
            IQueryRepository queryRepository,
            IPasantiaService pasantiaService,
            ICharlaService charlaService)
        {
            _subidaDetalleRepository = subidaDetalleRepository;
            _subidaRepository = subidaRepository;
            _queryRepository = queryRepository;
            _pasantiaService = pasantiaService;
            _charlaService = charlaService;
        }
        
        public async Task EjecutarSubida(TipoSubida tipoSubida,decimal contextoId, Stream archivo, CancellationToken cancellationToken)
        {
            var contexto = await GetTipoVinculacion((decimal)tipoSubida, contextoId);

            var subida = await GetSubidaAsync((decimal)tipoSubida);

            if (subida is null)
            {
                throw new Exception("La subida no existe");
            }

            if(!await FuncionesService.IsExcelAsync(archivo, cancellationToken))
            {
                throw new Exception("El archivo no es un archivo de Excel válido");
            }

            var dataTable = await DataTableFromExcelAsync(archivo, subida);

            var storeProcedureParameters = new List<Domain.Constants.StoredProcedureParameter>
             {
                 new()
                 {
                     Name = subida.Parametro,
                     Value = dataTable,
                     IsStructured = true,
                     UdttName = subida.UserDefinedType
                 },
                 new()
                 {
                     Name = "Id",
                     Value = contexto
                 }
                };

            await _queryRepository.ExecuteStoredProcedure(subida.Procedure,storeProcedureParameters,cancellationToken);
        }


        public async Task<SubidaCompleta> GetSubidaAsync(decimal tipoSubida)
        {
            var subida = await _subidaRepository.GetSubida(tipoSubida);

            if(subida is null)
            {
                throw new Exception("La subida no existe");
            }

            var detalle = await _subidaDetalleRepository.GetBySubidaIdAsync(subida.SubidaId);

            return new SubidaCompleta
            {
                SubidaId = subida.SubidaId,
                Procedure = subida.SProcedure,
                UserDefinedType = subida.UserDefinedType,
                Parametro = subida.Parametro,
                Detalle = detalle

            };
        }

        public async Task<decimal> GetTipoVinculacion(decimal tipoSubida, decimal contextoId)
        {
            return tipoSubida switch
            {
                (decimal)TipoSubida.Pasantia => await _pasantiaService.GetPasantiasActivasFinalizadas(contextoId),
                (decimal)TipoSubida.Charla => await _charlaService.GetCharlasActivasFinalizadas(contextoId),
                _ => throw new InvalidOperationException("Tipo de subida no soportado")
            };
        }

        public async Task<DataTable> DataTableFromExcelAsync(Stream archivo, SubidaCompleta subidaCompleta) 
        {
            DataTable dataTable = new DataTable();

            var tipoColumna = await ObtenerTipoDatoColumna(subidaCompleta.UserDefinedType, CancellationToken.None);

            foreach (var tipo in tipoColumna)
            {
                var detalle = subidaCompleta.Detalle.FirstOrDefault(x => string.Equals(x.ColumnaType,  tipo.NombreColumna, StringComparison.OrdinalIgnoreCase))
                    ?? throw new Exception($"No se encontró {tipo.NombreColumna}");

                Type dataType = GetDataType(tipo.TipoDato);

                DataColumn column = new(detalle.ColumnaType, dataType)
                {
                    AllowDBNull = tipo.EsNullable
                };

                dataTable.Columns.Add(column);
            }

            using (var workbook = new XLWorkbook(archivo))
            {
                var worksheet = workbook.Worksheet(1);

                var rows = worksheet.RowsUsed();

                foreach (var row in rows.Skip(1))
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (var detalle in subidaCompleta.Detalle)
                    {
                        ArgumentNullException.ThrowIfNull(detalle.ColumnaExcel);
                        ArgumentNullException.ThrowIfNull(detalle.ColumnaType);

                        int columnNumber = worksheet.ColumnsUsed().First(x => string.Equals(x.Cell(1).Value.ToString(), detalle.ColumnaExcel, StringComparison.OrdinalIgnoreCase)).ColumnNumber();
                        var cellValue = row.Cell(columnNumber).Value;
                        var tipoDato = tipoColumna.FirstOrDefault(x => string.Equals(x.NombreColumna, detalle.ColumnaType, StringComparison.OrdinalIgnoreCase)) ?? throw new Exception($"No se encontró el tipo de dato para la columna {detalle.ColumnaType}");
                        dataRow[detalle.ColumnaType] = ConvertValue(cellValue.ToString(), tipoDato.TipoDato);
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        private static Type GetDataType(string tipoDato) => tipoDato switch
        {
            "int" => typeof(int),
            "decimal" => typeof(decimal),
            "date" => typeof(DateTime),
            "datetime" => typeof(DateTime),
            "varchar" => typeof(string),
            "bit" => typeof(bool),
            "bigint" => typeof(long),
            "smallint" => typeof(short),
            "float" => typeof(float),
            "datetime2" => typeof(DateTime),
            "nvarchar" => typeof(string),

            _ => throw new InvalidOperationException($"Tipo de dato no soportado: {tipoDato}")
        };

        private static object? ConvertValue(string value, string tipoDato) => tipoDato switch
        {
            "int" => int.TryParse(value, out int intValue) ? intValue : null,
            "decimal" => decimal.TryParse(value, out decimal decimalValue) ? decimalValue : null,
            "date" => DateTime.TryParseExact(value, Formatos.Fechas, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue) ? dateValue : DBNull.Value,
            //"datetime" => DateTime.TryParseExact(value, Formatos.Fechas, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue) ? dateValue : DBNull.Value,
            "datetime" =>
            string.IsNullOrWhiteSpace(value)
                ? DateTime.Now
                : DateTime.TryParseExact(
                      value.Trim(),
                      FuncionesService.FechasDateTime,
                      CultureInfo.InvariantCulture,
                      DateTimeStyles.None,
                      out DateTime dateValue
                  )
                    ? dateValue
                    : DateTime.Now,

            "varchar" => value,
            "bit" => value.Trim().ToLower() switch
            {
                "si" => true,
                "sí" => true,
                "true" => true,
                "1" => true,
                "no" => false,
                "false" => false,
                "0" => false,
                _ => DBNull.Value
            },//"bit" => bool.TryParse(value, out bool boolValue) ? boolValue : DBNull.Value,
            "bigint" => long.TryParse(value, out long longValue) ? longValue : null,
            "smallint" => short.TryParse(value, out short shortValue) ? shortValue : null,
            "float" => float.TryParse(value, out float floatValue) ? floatValue : null,
            "datetime2" => DateTime.TryParseExact(value, Formatos.Fechas, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue) ? dateValue : null,
            "nvarchar" => value,
            _ => throw new InvalidOperationException($"Tipo de dato no soportado: {tipoDato}")
        };

        private async Task<IEnumerable<TipoColumnaDto>> ObtenerTipoDatoColumna(string sqlType, CancellationToken cancellationToken)
        {
            var parameter = new Dictionary<string, object?>
             {
                 { "NombreTipo", sqlType }
             };

            return await _queryRepository.ExecuteQuery<TipoColumnaDto>(DataType.ObtenerTipoDatoColumnaTipo, parameter, cancellationToken);
        }

        public async Task<ArchivoDescargaDto/*(Stream, string)*/> GenerarPlantillaExcel(TipoSubida tipoSubida, CancellationToken cancellationToken)
        {
            var subidaMasiva = await GetSubidaAsync((decimal)tipoSubida);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Plantilla");

            int columnIndex = 1;
            foreach (var detalle in subidaMasiva.Detalle)
            {
                worksheet.Cell(1, columnIndex).Value = detalle.ColumnaExcel;
                worksheet.Cell(1, columnIndex).Style.Font.Bold = true;
                columnIndex++;
            }

            IXLRange range = worksheet.Range(1, 1, 1, columnIndex - 1);
            IXLTable table = range.CreateTable();
            table.Theme = XLTableTheme.TableStyleMedium9;

            worksheet.Columns().AdjustToContents();

            //var stream = new MemoryStream();
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            //memoryStream.Position = 0;

            return new ArchivoDescargaDto
            {
                Contenido = memoryStream.ToArray(),
                NombreArchivo = $"Registro_{tipoSubida}.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            }; //(stream, "Registro Pasantia" + ".xlsx");
        }

    }
}
