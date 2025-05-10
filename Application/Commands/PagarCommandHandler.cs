using CQRSDDDDataRest.Domain.Entities;
using CQRSDDDDataRest.Infrastructure.Persistence;
using CQRSDDDDataRest.Infrastructure.Repositories;

namespace CQRSDDDDataRest.Application.Commands
{
    public class PagarCommandHandler
    {
        private readonly ISqlServerPagoRepository _sqlServerRepository;
        private readonly IPostgresPagoRepository _postgresRepository;
        private readonly PostgresPagoService _postgresService;

        public PagarCommandHandler(ISqlServerPagoRepository sqlServerRepository, IPostgresPagoRepository postgresRepository,PostgresPagoService postgresService)
        {
            _sqlServerRepository = sqlServerRepository;
            _postgresRepository = postgresRepository;
            _postgresService = postgresService;
        }

        public async Task<int> HandleAsync(PagarCommand request)
        {
            var monto = Monto.Crear(request.Monto);
            var metodoPago = MetodoPago.Crear(request.MetodoPago);
            var pago = new Pago(request.ClienteId, monto, metodoPago);
            var id = await _sqlServerRepository.AgregarAsync(pago);
            await _postgresRepository.AgregarAsync(pago);
            return id;
        }

        public async Task<List<Pago>> ObtenerPagoPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("El id no puede ser nulo o vacío.", nameof(id));

            var pagosEnumerable = await _postgresService.ObtenerPagoPorClienteAsync(id);

            // Convertir a lista de forma segura y evitar null
            var pagosList = pagosEnumerable != null ? new List<Pago>(pagosEnumerable) : new List<Pago>();

            return pagosList;        
        }
    }   
}
