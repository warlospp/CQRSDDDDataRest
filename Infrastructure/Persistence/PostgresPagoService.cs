using System.Text.Json;
using Microsoft.Extensions.Options;
using CQRSDDDDataRest.Domain.Entities;
using PagosCQRSDDDDataRest.Infrastructure.Persistence;
using System.Text.Json.Serialization;

namespace CQRSDDDDataRest.Infrastructure.Persistence
{
    public class PostgresPagoService
    {

        private readonly HttpClient _httpClient;
        private readonly PostgRESTSettings _settings;

        public PostgresPagoService(HttpClient httpClient, IOptions<PostgRESTSettings> options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }


        public async Task<List<Pago>> ObtenerPagoPorClienteAsync(string clienteId)
        {
            if (string.IsNullOrWhiteSpace(clienteId))
                throw new ArgumentException("El clienteId no puede ser nulo o vacío.", nameof(clienteId));

            var url = $"{_settings.uri}{_settings.funcion}?{_settings.parametro}={Uri.EscapeDataString(clienteId)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new StringOrNumberConverter() }
            };

            var pagosDto = JsonSerializer.Deserialize<List<PagoDto>>(json, options);

            if (pagosDto == null || pagosDto.Count == 0)
                return new List<Pago>();

            var pagos = pagosDto.Select(dto => PagoMapper.MapearPagoDtoADominio(dto)).ToList();

            return pagos;
        }

        public class PagosResponse
        {
            public PagoDto Result { get; set; }
            public int Count { get; set; }
        }

        public class StringOrNumberConverter : JsonConverter<string>
        {
            public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    return reader.GetDecimal().ToString();
                }
                else
                {
                    throw new JsonException($"Unexpected token parsing string. Token: {reader.TokenType}");
                }
            }

            public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value);
            }
        }
    }

    
}
